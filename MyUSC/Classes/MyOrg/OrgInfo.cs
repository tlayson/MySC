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
	public class OrgInfo : USCBaseItem
	{
#region Member Variables
		private long m_lOrgID;
		private string m_strNews;
		private string m_strRes1;
		private string m_strRes2;
		private long m_lRes1;
		private long m_lRes2;
		private long m_lRes3;
#endregion

#region Init
		protected void InitOrgInfo( long orgID )
		{
			m_lOrgID = orgID;
			m_strNews = "";
			m_strRes1 = "";
			m_strRes2 = "";
			m_lRes1 = -1;
			m_lRes2 = -1;
			m_lRes3 = -1;
		}

		public OrgInfo( long orgID, string strCnx )
		{
			m_strConnectionString = strCnx;
			InitOrgInfo( orgID );
		}
#endregion

#region Accessors
		public long InfoIdx
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

		public long LongRes1
		{
			get
			{
				return this.m_lRes1;
			}
			set
			{
				this.m_lRes1 = value;
			}
		}

		public long LongRes2
		{
			get
			{
				return this.m_lRes2;
			}
			set
			{
				this.m_lRes2 = value;
			}
		}

		public long LongRes3
		{
			get
			{
				return this.m_lRes3;
			}
			set
			{
				this.m_lRes3 = value;
			}
		}

		public string News
		{
			get
			{
				return this.m_strNews;
			}
			set
			{
				this.m_strNews = value;
			}
		}

		public string StringRes1
		{
			get
			{
				return this.m_strRes1;
			}
			set
			{
				this.m_strRes1 = value;
			}
		}

		public string StringRes2
		{
			get
			{
				return this.m_strRes2;
			}
			set
			{
				this.m_strRes2 = value;
			}
		}

#endregion

#region Update

/*
CREATE PROCEDURE sp_UpdateOrgInfo
	@OrgID bigint, 
	@News nvarchar(1024),
	@String2 nvarchar(1024),
	@String3 nvarchar(1024),
	@Long1 bigint,
	@Long2 bigint,
	@Long3 bigint,
	@LastUpdate nvarchar(1024),
	@InfoIdx bigint
*/
		public bool Update(UserAccount acct)
		{
			bool fRet = false;
			string strClean = acct.UserName + " -- " + DateTime.Now.ToString();
			this.LastUpdate = Sanitize(strClean);

			SqlParameter[] paramArray = new SqlParameter[9];
			paramArray[0] = new SqlParameter("@OrgID", OrgID);
			paramArray[1] = new SqlParameter("@News", Sanitize(USCBase.Truncate(this.News, 1023, true)));
			paramArray[2] = new SqlParameter("@String2", Sanitize(USCBase.Truncate(this.StringRes1, 1023, true)));
			paramArray[3] = new SqlParameter("@String3", Sanitize(USCBase.Truncate(this.StringRes2, 1023, true)));
			paramArray[4] = new SqlParameter("@Long1", this.LongRes1);
			paramArray[5] = new SqlParameter("@Long2", this.LongRes2);
			paramArray[6] = new SqlParameter("@Long3", this.LongRes3);
			paramArray[7] = new SqlParameter("@LastUpdate", this.LastUpdate);
			paramArray[8] = new SqlParameter("@InfoIdx", Key);

			if (ExecuteSPNoValue("sp_UpdateOrgInfo", paramArray))
			{
				fRet = true;
			}

			return fRet;
		}
#endregion

#region Load
	public void Load()
	{
		SqlDataReader reader = null;
		SqlConnection sqlConn = null;
		SqlParameter[] paramArray = new SqlParameter[1];
		paramArray[0] = new SqlParameter("@OrgID", OrgID);

		if (ExecuteSPRows("sp_GetOrgInfo", paramArray, out reader, out sqlConn) && null != reader)
		{
			try
			{
				bool fLoaded = false;
				while (reader.Read())
				{
					IDataRecord dr = (IDataRecord)reader;
					ConnectionString = m_strConnectionString;
					Key = ObjectToLong(dr[0]);
					OrgID = ObjectToLong(dr[1]);
					News = ObjectToString(dr[2]);
					StringRes1 = ObjectToString(dr[3]);
					StringRes2 = ObjectToString(dr[4]);
					LongRes1 = ObjectToLong(dr[5]);
					LongRes2 = ObjectToLong(dr[6]);
					LongRes3 = ObjectToLong(dr[7]);
					LastUpdate = ObjectToString(dr[8]);
					fLoaded = true;
				}
				reader.Close();
				sqlConn.Close();

				if( !fLoaded )
				{
					Add();
				}
			}
			catch (Exception ex)
			{
				String strIntro = "Error loading Info for org ID = " + OrgID;
				EvtLog.WriteException( strIntro, ex, 1 );
			}
		}
	}
#endregion

#region Add
	public void Add()
	{
		SqlParameter[] paramArray = new SqlParameter[8];
		paramArray[0] = new SqlParameter("@OrgID", OrgID);
		paramArray[1] = new SqlParameter("@News", Sanitize(USCBase.Truncate(News, 1023, true)));
		paramArray[2] = new SqlParameter("@String2", Sanitize(USCBase.Truncate(StringRes1, 1023, true)));
		paramArray[3] = new SqlParameter("@String3", Sanitize(USCBase.Truncate(StringRes2, 1023, true)));
		paramArray[4] = new SqlParameter("@Long1", LongRes1);
		paramArray[5] = new SqlParameter("@Long2", LongRes2);
		paramArray[6] = new SqlParameter("@Long3", LongRes3);
		paramArray[7] = new SqlParameter("@LastUpdate", Sanitize(USCBase.Truncate(LastUpdate, 1023, true)));

		Key = ExecuteSPInsert("sp_AddOrgInfo", paramArray);
	}
#endregion

#region Delete
	public void Delete()
	{
		SqlParameter[] paramArray = new SqlParameter[1];
		paramArray[0] = new SqlParameter("@OrgID", OrgID);
		ExecuteSPNoValue("sp_DeleteOrgInfo", paramArray);
	}
#endregion

	}

}