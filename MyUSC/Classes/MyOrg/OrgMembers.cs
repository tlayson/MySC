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
	public class OrgMember : USCBaseItem
	{
#region Member Variables
		private long m_lOrgID;
		private long m_lUserID;
		private OrgAccessTypes m_nMemberType;
		private string m_strNumber;
		private string m_strPositions;
		private string m_strNote;
		private bool m_fDeleted;
		private bool m_fAcceptedInvite;
#endregion

#region Init
		protected void InitOrgMember()
		{
			m_lOrgID = -1;
			m_lUserID = -1;
			m_nMemberType = OrgAccessTypes.Guest;
			m_strNumber = "";
			m_strPositions = "";
			m_strNote = "";
			m_fDeleted = false;
			m_fAcceptedInvite = false;
		}

		public OrgMember()
		{
			InitOrgMember();
		}
#endregion

#region Accessors
		public long OrgMemberID
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

		public long UserID
		{
			get
			{
				return this.m_lUserID;
			}
			set
			{
				this.m_lUserID = value;
			}
		}

		public OrgAccessTypes MemberType
		{
			get
			{
				return this.m_nMemberType;
			}
			set
			{
				this.m_nMemberType = value;
			}
		}

		public string Number
		{
			get
			{
				return this.m_strNumber;
			}
			set
			{
				this.m_strNumber = value;
			}
		}

		public string Positions
		{
			get
			{
				return this.m_strPositions;
			}
			set
			{
				this.m_strPositions = value;
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

		public bool AcceptedInvite
		{
			get
			{
				return this.m_fAcceptedInvite;
			}
			set
			{
				this.m_fAcceptedInvite = value;
			}
		}
#endregion

#region Update

/*
CREATE PROCEDURE sp_UpdateOrgMember
	@OrgID bigint, 
	@UserID bigint,
	@MemberType int,
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


			SqlParameter[] paramArray = new SqlParameter[9];
			paramArray[0] = new SqlParameter("@OrgID", OrgID);
			paramArray[1] = new SqlParameter("@UserID", this.UserID);
			paramArray[2] = new SqlParameter("@MemberType", this.MemberType);
			paramArray[3] = new SqlParameter("@Number", Sanitize(USCBase.Truncate(this.Number, 9, true)));
			paramArray[4] = new SqlParameter("@Positions", Sanitize(USCBase.Truncate(this.Positions, 149, true)));
			paramArray[5] = new SqlParameter("@Note", Sanitize(USCBase.Truncate(this.Note, 199, true)));
			paramArray[6] = new SqlParameter("@Deleted", SQLBitFromBool(this.Deleted));
			paramArray[7] = new SqlParameter("@Update", this.LastUpdate);
			paramArray[8] = new SqlParameter("@Key", Key);

			if (ExecuteSPNoValue("sp_UpdateOrgMember", paramArray))
			{
				fRet = true;
			}

			return fRet;
		}
#endregion

	}

	public class OrgMemberList : USCBaseList
	{
#region Column Constants
		const int colKey = 0;
		const int colOrgID = 1;
		const int colUserID = 2;
		const int colMemberType = 3;
		const int colNumber = 4;
		const int colPositions = 5;
		const int colNote = 6;
		const int colDeleted = 7;
		const int colAcceptedInvite = 8;
		const int colCreator = 9;
		const int colCreateDate = 10;
		const int colLastUpdate = 11;
#endregion
		private long m_lOrgID;

		public OrgMemberList()
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

//		public Hashtable htOrgMembers;
		public SortedDictionary<long, object> m_sdOrgMembers = new SortedDictionary<long, object>();

		public void Init(string cnxString, long orgID, bool fForce)
		{
			m_strConnectionString = cnxString;
			OrgID = orgID;
			Load();
		}

#region Load
		private bool Load()
		{
			bool fRet = false;

			m_sdOrgMembers.Clear();

			if (0 > OrgID)
			{
				return false;
			}

			SqlDataReader reader = null;
			SqlConnection sqlConn = null;
			SqlParameter[] paramArray = new SqlParameter[1];
			paramArray[0] = new SqlParameter("@OrgID", OrgID);

			if (ExecuteSPRows("sp_GetOrgMembers", paramArray, out reader, out sqlConn) && null != reader)
			{
				try
				{
					while (reader.Read())
					{
						IDataRecord dr = (IDataRecord)reader;
						OrgMember om = new OrgMember();
						fRet = ReadOrgMember(dr, om);
						if (fRet)
						{
							long lIndex = m_sdOrgMembers.Count + 1;
							m_sdOrgMembers.Add( lIndex, om );
						}
					}
					reader.Close();
					sqlConn.Close();
					fRet = true;
				}
				catch (Exception ex)
				{
					String strIntro = "Error loading Org members for " + OrgID;
					EvtLog.WriteException( strIntro, ex, 1 );
				}
			}

			return fRet;
		}

		private bool ReadOrgMember(IDataRecord dr, OrgMember om)
		{
			bool fRet = true;
			try
			{

				om.ConnectionString = m_strConnectionString;
				om.Key = ObjectToLong(dr[colKey]);
				om.OrgID = ObjectToLong(dr[colOrgID]);
				om.UserID = ObjectToLong(dr[colUserID]);
				om.MemberType = (OrgAccessTypes)ObjectToInt(dr[colMemberType]);
				om.Number = ObjectToString(dr[colNumber]);
				om.Positions = ObjectToString(dr[colPositions]);
				om.Note = ObjectToString(dr[colNote]);
				om.Deleted = ObjectToBool(dr[colDeleted]);
				om.AcceptedInvite = ObjectToBool(dr[colAcceptedInvite]);
				om.Creator = ObjectToString(dr[colCreator]);
				om.CreateDate = ObjectToDateTime(dr[colCreateDate]);
				om.LastUpdate = ObjectToString(dr[colLastUpdate]);
			}
			catch (Exception ex)
			{
				EvtLog.WriteException("ReadOrgMember.Read failure", ex, 0);
				fRet = false;
			}
			return fRet;
		}
#endregion Load

		public int Count
		{
			get
			{
				return m_sdOrgMembers.Count;
			}
		}

		public OrgMember GetOrgMember(long index)
		{
			OrgMember member = null;
			foreach (KeyValuePair<long, object> kvp in m_sdOrgMembers)
			{
				OrgMember om = (OrgMember)kvp.Value;
				if( null != om && om.UserID == index )
				{
					member = om;
					break;
				}
			}
			return member;
		}

/*
CREATE PROCEDURE sp_AddOrgMember
	@OrgID bigint, 
	@UserID bigint, 
	@MemberType int, 
	@Note nvarchar(200),
	@Creator nvarchar(50)
*/
		public bool Add(OrgMember orgMember)
		{
			bool fRet = false;

			SqlParameter[] paramArray = new SqlParameter[7];
			paramArray[0] = new SqlParameter("@OrgID", orgMember.OrgID);
			paramArray[1] = new SqlParameter("@UserID", orgMember.UserID);
			paramArray[2] = new SqlParameter("@MemberType", orgMember.MemberType);
			paramArray[3] = new SqlParameter("@Number", Sanitize(USCBase.Truncate(orgMember.Number, 9, true)));
			paramArray[4] = new SqlParameter("@Positions", Sanitize(USCBase.Truncate(orgMember.Positions, 149, true)));
			paramArray[5] = new SqlParameter("@Note", Sanitize(USCBase.Truncate(orgMember.Note, 199, true)));
			paramArray[6] = new SqlParameter("@Creator", Sanitize(USCBase.Truncate(orgMember.Creator, 49, true)));

			orgMember.Key = ExecuteSPInsert("sp_AddOrgMember", paramArray);
			if (orgMember.Key > 0)
			{
				long lIndex = m_sdOrgMembers.Count + 1;
				m_sdOrgMembers.Add( lIndex, orgMember );
				fRet = true;
			}

			return fRet;
		}

		public bool Update(OrgMember orgMember, UserAccount acct)
		{
			return orgMember.Update( acct );
		}

		public bool Delete(OrgMember orgMember, UserAccount acct)
		{
			// We don't really want to delete it, just make inactive.
			orgMember.Deleted = true;
			return orgMember.Update( acct );
		}

	}

}