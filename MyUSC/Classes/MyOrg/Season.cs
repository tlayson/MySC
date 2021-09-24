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
	public class Season : USCBaseItem
	{
#region Member Variables
/*
	SeasonID bigint NOT NULL IDENTITY,
	OrgID bigint NOT NULL,
	SeasonName nvarchar(250) NOT NULL,
	SeasonStart datetime2(7),
	Comments nvarchar(200),
	Deleted bit NOT NULL DEFAULT(0),
 */
		private long m_lOrgID;
		private string m_strSeasonName;
		private DateTime m_dtSeasonDate;
		private string m_strComments;
		private bool m_fDeleted;
		private bool m_fIsDefault;
		private bool m_fShare;
#endregion

#region Init
		protected void InitSeason()
		{
			m_lOrgID = -1;
			m_strSeasonName = "";
			m_dtSeasonDate = DateTime.Now;
			m_strComments = "";
			m_fDeleted = false;
			m_fIsDefault = false;
			m_fShare = false;
		}

		public Season()
		{
			InitSeason();
		}
#endregion

#region Accessors
		public long SeasonID
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

		public string SeasonName
		{
			get
			{
				return this.m_strSeasonName;
			}
			set
			{
				this.m_strSeasonName = value;
			}
		}

		public DateTime SeasonDate
		{
			get
			{
				return this.m_dtSeasonDate;
			}
			set
			{
				this.m_dtSeasonDate = value;
			}
		}

		public string Comments
		{
			get
			{
				return this.m_strComments;
			}
			set
			{
				this.m_strComments = value;
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

		public bool IsDefault
		{
			get
			{
				return this.m_fIsDefault;
			}
			set
			{
				this.m_fIsDefault = value;
			}
		}

		public bool Share
		{
			get
			{
				return this.m_fShare;
			}
			set
			{
				this.m_fShare = value;
			}
		}
#endregion

#region Update

/*
CREATE PROCEDURE sp_UpdateOrgSeason
	@OrgID bigint, 
	@SeasonName nvarchar(250),
	@SeasonStart datetime,
	@Comments nvarchar(200),
	@IsDefault bit,
	@Share bit,
	@Deleted bit,
	@Update nvarchar(max),
	@Key bigint
*/
		public bool Update( UserAccount acct )
		{
			bool fRet = false;
			try
			{
				string strClean = acct.UserName + " -- " + DateTime.Now.ToString();
				this.LastUpdate = Sanitize(strClean);


				SqlParameter[] paramArray = new SqlParameter[9];
				paramArray[0] = new SqlParameter("@OrgID", OrgID);
				paramArray[3] = new SqlParameter("@SeasonName", Sanitize(USCBase.Truncate(this.SeasonName, 249, true)));
				paramArray[1] = new SqlParameter("@SeasonStart", this.SeasonDate);
				paramArray[3] = new SqlParameter("@Comments", Sanitize(USCBase.Truncate(this.Comments, 199, true)));
				paramArray[4] = new SqlParameter("@IsDefault", SQLBitFromBool(this.IsDefault));
				paramArray[5] = new SqlParameter("@Share", SQLBitFromBool(this.Share));
				paramArray[6] = new SqlParameter("@Deleted", SQLBitFromBool(this.Deleted));
				paramArray[7] = new SqlParameter("@Update", this.LastUpdate);
				paramArray[8] = new SqlParameter("@Key", Key);

				if (ExecuteSPNoValue("sp_UpdateOrgSeason", paramArray))
				{
					fRet = true;
				}
			}
			catch (Exception ex)
			{
				string strErr = "Season.Update failure";
				short sCat = 0;
				if (IsLocalInstance())
				{
					strErr += " [Local] ";
					sCat = 99;
				}
				EvtLog.WriteException(strErr, ex, EventErrors.ErrorType.MyOrgGeneric, sCat);
				fRet = false;
			}

			return fRet;
		}
#endregion

	}

	public sealed class SeasonList : USCBaseList
	{
#region Column Constants
		/*
	SeasonID bigint NOT NULL IDENTITY,
	OrgID bigint NOT NULL,
	SeasonName nvarchar(250) NOT NULL,
	SeasonStart datetime2(7),
	Comments nvarchar(200),
	IsDefault bit NOT NULL,
	Share bit NOT NULL,
	Deleted bit NOT NULL DEFAULT(0),
	CreationUser nvarchar(50),
	CreationDate datetime2(7) DEFAULT(getdate()),
	LastUpdate nvarchar(max),
 */
		const int colKey = 0;
		const int colOrgID = 1;
		const int colSeasonName = 2;
		const int colSeasonStart = 3;
		const int colComments = 4;
		const int colIsDefault = 5;
		const int colShare = 6;
		const int colDeleted = 7;
		const int colCreator = 8;
		const int colCreateDate = 9;
		const int colLastUpdate = 10;
#endregion

		private long m_lOrgID;

		public SeasonList()
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

		public Hashtable htSeasons;

		public void Init(string cnxString, long orgID, bool fForce)
		{
			m_strConnectionString = cnxString;
			if (null == htSeasons || fForce)
			{
				OrgID = orgID;
				htSeasons = new Hashtable();
				Load();
			}
		}

#region Load
		public bool Load()
		{
			bool fRet = false;

			htSeasons.Clear();

			if( 0 > OrgID )
			{
				return false;
			}

			SqlConnection sqlConn = null;
			DataSet locStrDS = new DataSet();

			try
			{
				StringBuilder sbSQLQuery = new StringBuilder("SELECT * FROM MyOrgSeason WHERE OrgID=");
				sbSQLQuery.Append(this.OrgID);

				sqlConn = new SqlConnection(m_strConnectionString);
				sqlConn.Open();
				SqlDataAdapter daOrgMembers = new SqlDataAdapter(sbSQLQuery.ToString(), sqlConn);
				daOrgMembers.Fill(locStrDS, "MyOrgSeason");
			}
			catch (Exception ex)
			{
				EvtLog.WriteException("MyOrgSeason.Load failure", ex, 0);
				return false;
			}
			finally
			{
				sqlConn.Close();
			}

			DataRowCollection dra = locStrDS.Tables["MyOrgSeason"].Rows;
			foreach (DataRow dr in dra)
			{
				Season season = new Season();
				fRet = ReadSeason(dr, season);
				if (fRet)
				{
					htSeasons.Add(season.Key, season);
				}
			}
			fRet = true;

			return fRet;
		}

/*
		const int colKey = 0;
		const int colOrgID = 1;
		const int colSeasonName = 2;
		const int colSeasonStart = 3;
		const int colComments = 4;
		const int colIsDefault = 5;
		const int colShare = 6;
		const int colDeleted = 7;
		const int colCreator = 8;
		const int colCreateDate = 9;
		const int colLastUpdate = 10;
*/
		private bool ReadSeason(DataRow dr, Season season)
		{
			bool fRet = true;
			try
			{
				season.ConnectionString = m_strConnectionString;
				season.Key = ObjectToLong(dr.ItemArray[colKey]);
				season.OrgID = ObjectToLong(dr.ItemArray[colOrgID]);
				season.SeasonName = ObjectToString(dr.ItemArray[colSeasonName]);
				season.SeasonDate = ObjectToDateTime(dr.ItemArray[colSeasonStart]);
				season.Comments = ObjectToString(dr.ItemArray[colComments]);
				season.IsDefault = ObjectToBool(dr.ItemArray[colIsDefault]);
				season.Share = ObjectToBool(dr.ItemArray[colShare]);
				season.Deleted = ObjectToBool(dr.ItemArray[colDeleted]);
				season.Creator = ObjectToString(dr.ItemArray[colCreator]);
				season.CreateDate = ObjectToDateTime(dr.ItemArray[colCreateDate]);
				season.LastUpdate = ObjectToString(dr.ItemArray[colLastUpdate]);

			}
			catch (Exception ex)
			{
				string strErr = "MyOrgSeason.Read failure";
				short sCat = 0;
				if (IsLocalInstance())
				{
					strErr += " [Local] ";
					sCat = 99;
				}
				EvtLog.WriteException(strErr, ex, EventErrors.ErrorType.MyOrgGeneric, sCat);
				fRet = false;
			}
			return fRet;
		}
#endregion Load

		public int Count
		{
			get
			{
				return htSeasons.Count;
			}
		}

		public Season GetSeason(long index)
		{
			return (Season)htSeasons[index];
		}

/*
CREATE PROCEDURE sp_AddOrgSeason
	@OrgID bigint, 
	@SeasonName nvarchar(250),
	@SeasonStart datetime,
	@Comments nvarchar(200),
	@IsDefault bit,
	@Share bit,
	@Creator nvarchar(50)
*/
		public bool Add(Season season)
		{
			bool fRet = false;

			try
			{
				SqlParameter[] paramArray = new SqlParameter[7];
				paramArray[0] = new SqlParameter("@OrgID", season.OrgID);
				paramArray[1] = new SqlParameter("@SeasonName", Sanitize(USCBase.Truncate(season.SeasonName, 249, true)));
				paramArray[2] = new SqlParameter("@SeasonStart", season.SeasonDate);
				paramArray[3] = new SqlParameter("@Comments", Sanitize(USCBase.Truncate(season.Comments, 199, true)));
				paramArray[4] = new SqlParameter("@IsDefault", SQLBitFromBool(season.IsDefault));
				paramArray[5] = new SqlParameter("@Share", SQLBitFromBool(season.Share));
				paramArray[6] = new SqlParameter("@Creator", Sanitize(USCBase.Truncate(season.Creator, 49, true)));

				season.Key = ExecuteSPInsert("sp_AddOrgSeason", paramArray);
				if (season.Key > 0)
				{
					htSeasons.Add(season.Key, season);
					fRet = true;
				}
			}
			catch (Exception ex)
			{
				string strErr = "SeasonList.Add failure";
				short sCat = 0;
				if (IsLocalInstance())
				{
					strErr += " [Local] ";
					sCat = 99;
				}
				EvtLog.WriteException(strErr, ex, EventErrors.ErrorType.MyOrgGeneric, sCat);
				fRet = false;
			}

			return fRet;
		}

		public bool Update(Season season, UserAccount acct)
		{
			return season.Update(acct);
		}

		public bool Delete(Season season, UserAccount acct)
		{
			// We don't really want to delete it, just make inactive.
			season.Deleted = false;
			return season.Update(acct);
		}

	}

}