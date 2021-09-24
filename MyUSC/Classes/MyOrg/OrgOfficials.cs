using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.UI.WebControls;
using System.Web;
using System.Web.Security;

namespace MyUSC.Classes.MyOrg
{
	public class OrgOfficial : USCBaseItem
	{
#region Member Variables
		private long m_lOrgID;
		private long m_lMemberID;
		private string m_strTitle;
		private bool m_fShowInfo;
		private bool m_fShowExtInfo;
		private string m_strExtEmail;
		private string m_strExtPhone;
		private string m_strExtAddress;
		private string m_strNote;
		private bool m_fDeleted;
#endregion

#region Init
		protected void InitOrgOfficial()
		{
			m_lOrgID = -1;
			m_lMemberID = -1;
			m_strTitle = "";
			m_fShowInfo = false;
			m_fShowExtInfo = true;
			m_strExtEmail = "";
			m_strExtPhone = "";
			m_strExtAddress = "";
			m_strNote = "";
			m_fDeleted = false;
		}

		public OrgOfficial()
		{
			InitOrgOfficial();
		}
#endregion

#region Accessors
		public long OrgOfficialIDX
		{
			get
			{
				return this.m_lKey;
			}
			set
			{
				this.m_lKey = value;
			}
		}

		public long OrgID
		{
			get
			{
				return this.m_lOrgID;
			}
			set
			{
				this.m_lOrgID = value;
			}
		}

		public long MemberID
		{
			get
			{
				return this.m_lMemberID;
			}
			set
			{
				this.m_lMemberID = value;
			}
		}

		public string Title
		{
			get
			{
				return this.m_strTitle;
			}
			set
			{
				this.m_strTitle = value;
			}
		}

		public bool ShowInfo
		{
			get
			{
				return this.m_fShowInfo;
			}
			set
			{
				this.m_fShowInfo = value;
			}
		}

		public bool ShowExtInfo
		{
			get
			{
				return this.m_fShowExtInfo;
			}
			set
			{
				this.m_fShowExtInfo = value;
			}
		}

		public string ExtEmail
		{
			get
			{
				return this.m_strExtEmail;
			}
			set
			{
				this.m_strExtEmail = value;
			}
		}

		public string ExtPhone
		{
			get
			{
				return this.m_strExtPhone;
			}
			set
			{
				this.m_strExtPhone = value;
			}
		}

		public string ExtAddress
		{
			get
			{
				return this.m_strExtAddress;
			}
			set
			{
				this.m_strExtAddress = value;
			}
		}

		public string Note
		{
			get
			{
				return this.m_strNote;
			}
			set
			{
				this.m_strNote = value;
			}
		}

		public bool Deleted
		{
			get
			{
				return this.m_fDeleted;
			}
			set
			{
				this.m_fDeleted = value;
			}
		}

#endregion

#region Update

/*
CREATE PROCEDURE sp_UpdateOrgOfficial
	@OrgID bigint, 
	@MemberID bigint, 
	@Title nvarchar(50),
	@ShowInfo bit,
	@ShowExtInfo bit,
	@ExtEmail nvarchar(50),
	@ExtPhone nvarchar(50),
	@ExtAddress nvarchar(200),
	@Note nvarchar(200),
	@Deleted bit,
	@Update nvarchar(max),
	@Key bigint
*/
		public bool Update( UserAccount acct )
		{
			bool fRet = false;
			string strClean = acct.UserName + " -- " + DateTime.Now.ToString();
			this.LastUpdate = Sanitize(strClean);


			SqlParameter[] paramArray = new SqlParameter[12];
			paramArray[0] = new SqlParameter("@OrgID", OrgID);
			paramArray[1] = new SqlParameter("@MemberID", this.MemberID);
			paramArray[2] = new SqlParameter("@Title", Sanitize(USCBase.Truncate(this.Title, 49, true)));
			paramArray[3] = new SqlParameter("@ShowInfo", SQLBitFromBool(this.ShowInfo));
			paramArray[4] = new SqlParameter("@ShowExtInfo", SQLBitFromBool(this.ShowExtInfo));
			paramArray[5] = new SqlParameter("@ExtEmail", Sanitize(USCBase.Truncate(this.ExtEmail, 49, true)));
			paramArray[6] = new SqlParameter("@ExtPhone", Sanitize(USCBase.Truncate(this.ExtPhone, 49, true)));
			paramArray[7] = new SqlParameter("@ExtAddress", Sanitize(USCBase.Truncate(this.ExtAddress, 199, true)));
			paramArray[8] = new SqlParameter("@Note", Sanitize(USCBase.Truncate(this.Note, 199, true)));
			paramArray[9] = new SqlParameter("@Deleted", SQLBitFromBool(this.Deleted));
			paramArray[10] = new SqlParameter("@Update", this.LastUpdate);
			paramArray[11] = new SqlParameter("@Key", Key);

			if (ExecuteSPNoValue("sp_UpdateOrgOfficial", paramArray))
			{
				fRet = true;
			}

			return fRet;
		}
#endregion
	}

	public class OrgOfficialsList : USCBaseList
	{
#region Column Constants
/*
	OfficialsIdx bigint NOT NULL IDENTITY,
	OrgID bigint NOT NULL,
	MemberID bigint NOT NULL,
	Title nvarchar(50),
	ShowInfo bit NOT NULL,
	ShowExtInfo bit NOT NULL,
	ExtEmail nvarchar(50),
	ExtPhone nvarchar(50),
	ExtAddress nvarchar(200),
	Note nvarchar(200),
	Deleted bit NOT NULL DEFAULT(0),
 */
		const int colKey = 0;
		const int colOrgID = 1;
		const int colMemberID = 2;
		const int colTitle = 3;
		const int colShowInfo = 4;
		const int colShowExtInfo = 5;
		const int colExtEmail = 6;
		const int colExtPhone = 7;
		const int colExtAddress = 8;
		const int colNote = 9;
		const int colDeleted = 10;
		const int colCreator = 11;
		const int colCreateDate = 12;
		const int colLastUpdate = 13;
#endregion

		private long m_lOrgID;

		public OrgOfficialsList()
		{
			m_lOrgID = -1;
		}

		public long OrgID
		{
			get
			{
				return this.m_lOrgID;
			}
			set
			{
				this.m_lOrgID = value;
			}
		}

		public Hashtable htOrgOfficials;

		public void Init(string cnxString, long orgID, bool fForce)
		{
			m_strConnectionString = cnxString;
			if (null == htOrgOfficials || fForce)
			{
				OrgID = orgID;
				htOrgOfficials = new Hashtable();
				Load();
			}
		}

#region Load
		public bool Load()
		{
			bool fRet = false;

			htOrgOfficials.Clear();

			if (0 > OrgID)
			{
				return false;
			}

			StringBuilder sbSQLQuery = new StringBuilder("SELECT * FROM MyOrgOfficials WHERE OrgID=");
			sbSQLQuery.Append(this.OrgID);

			SqlConnection sqlConn = null;
			DataSet locStrDS = new DataSet();

			try
			{
				sqlConn = new SqlConnection(m_strConnectionString);
				sqlConn.Open();
				SqlDataAdapter daOrgMembers = new SqlDataAdapter(sbSQLQuery.ToString(), sqlConn);
				daOrgMembers.Fill(locStrDS, "MyOrgOfficials");
			}
			catch (Exception ex)
			{
				EvtLog.WriteException("MyOrgOfficials.Load failure", ex, 0);
				return false;
			}
			finally
			{
				sqlConn.Close();
			}

			DataRowCollection dra = locStrDS.Tables["MyOrgOfficials"].Rows;
			foreach (DataRow dr in dra)
			{
				OrgOfficial oo = new OrgOfficial();
				fRet = ReadOrgOfficial(dr, oo);
				if (fRet)
				{
					htOrgOfficials.Add(oo.Key, oo);
				}
			}
			fRet = true;

			return fRet;
		}

/*
 		const int colKey = 0;
		const int colOrgID = 1;
		const int colMemberID = 2;
		const int colTitle = 3;
		const int colShowInfo = 4;
		const int colShowExtInfo = 5;
		const int colExtEmail = 6;
		const int colExtPhone = 7;
		const int colExtAddress = 8;
		const int colNote = 9;
		const int colDeleted = 10;
		const int colCreator = 11;
		const int colCreateDate = 12;
		const int colLastUpdate = 13;
 */
		private bool ReadOrgOfficial(DataRow dr, OrgOfficial oo)
		{
			bool fRet = true;
			try
			{
				oo.ConnectionString = m_strConnectionString;

				oo.Key = ObjectToLong(dr.ItemArray[colKey]);
				oo.OrgID = ObjectToLong(dr.ItemArray[colOrgID]);
				oo.MemberID = ObjectToLong(dr.ItemArray[colMemberID]);
				oo.Title = ObjectToString(dr.ItemArray[colTitle]);
				oo.ShowInfo = ObjectToBool(dr.ItemArray[colShowInfo]);
				oo.ShowExtInfo = ObjectToBool(dr.ItemArray[colShowExtInfo]);
				oo.ExtEmail = ObjectToString(dr.ItemArray[colExtEmail]);
				oo.ExtPhone = ObjectToString(dr.ItemArray[colExtPhone]);
				oo.ExtAddress = ObjectToString(dr.ItemArray[colExtAddress]);
				oo.Note = ObjectToString(dr.ItemArray[colNote]);
				oo.Deleted = ObjectToBool(dr.ItemArray[colDeleted]);
				oo.Creator = ObjectToString(dr.ItemArray[colCreator]);
				oo.CreateDate = ObjectToDateTime(dr.ItemArray[colCreateDate]);
				oo.LastUpdate = ObjectToString(dr.ItemArray[colLastUpdate]);

			}
			catch (Exception ex)
			{
				EvtLog.WriteException("MyOrgOfficials.Read failure", ex, 0);
				fRet = false;
			}
			return fRet;
		}
#endregion Load

		public int Count
		{
			get
			{
				return htOrgOfficials.Count;
			}
		}

		public OrgMember GetOrgOfficial(long index)
		{
			return (OrgMember)htOrgOfficials[index];
		}

/*
CREATE PROCEDURE sp_AddOrgOfficial
	@OrgID bigint, 
	@MemberID bigint, 
	@Title nvarchar(50),
	@ShowInfo bit,
	@ShowExtInfo bit,
	@ExtEmail nvarchar(50),
	@ExtPhone nvarchar(50),
	@ExtAddress nvarchar(200),
	@Note nvarchar(200),
	@Creator nvarchar(50)
*/
		public bool Add(OrgOfficial orgOfficial)
		{
			bool fRet = false;

			SqlParameter[] paramArray = new SqlParameter[10];
			paramArray[0] = new SqlParameter("@OrgID", orgOfficial.OrgID);
			paramArray[1] = new SqlParameter("@MemberID", orgOfficial.MemberID);
			paramArray[2] = new SqlParameter("@Title", Sanitize(USCBase.Truncate(orgOfficial.Title, 49, true)));
			paramArray[3] = new SqlParameter("@ShowInfo", SQLBitFromBool(orgOfficial.ShowInfo));
			paramArray[4] = new SqlParameter("@ShowExtInfo", SQLBitFromBool(orgOfficial.ShowExtInfo));
			paramArray[5] = new SqlParameter("@ExtEmail", Sanitize(USCBase.Truncate(orgOfficial.ExtEmail, 49, true)));
			paramArray[6] = new SqlParameter("@ExtPhone", Sanitize(USCBase.Truncate(orgOfficial.ExtPhone, 49, true)));
			paramArray[7] = new SqlParameter("@ExtAddress", Sanitize(USCBase.Truncate(orgOfficial.ExtAddress, 199, true)));
			paramArray[8] = new SqlParameter("@Note", Sanitize(USCBase.Truncate(orgOfficial.Note, 199, true)));
			paramArray[9] = new SqlParameter("@Creator", Sanitize(USCBase.Truncate(orgOfficial.Creator, 49, true)));

			orgOfficial.Key = ExecuteSPInsert("sp_AddOrgOfficial", paramArray);
			if (orgOfficial.Key > 0)
			{
				htOrgOfficials.Add(orgOfficial.Key, orgOfficial);
				fRet = true;
			}

			return fRet;
		}

		public bool Update(OrgOfficial orgOfficial, UserAccount acct)
		{
			return orgOfficial.Update(acct);
		}

		public bool Delete(OrgOfficial orgOfficial, UserAccount acct)
		{
			// We don't really want to delete it, just make inactive.
			orgOfficial.Deleted = false;
			return orgOfficial.Update(acct);
		}

	}

}