using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;

namespace MyUSC.Classes
{
	public class SupportRequest : USCBaseItem
	{
#region Member Variables
		private long m_nAccountID;
		private bool m_bEmailSent;
		private string m_strFirstName;
		private string m_strLastName;
		private string m_strEmail;
		private string m_strPhone;
		private string m_strBrowser;
		private string m_strDescription;
		private string m_strDetails;
#endregion

#region INIT
		public SupportRequest()
		{
			Init();
		}

		private void Init()
		{
			m_nAccountID = -1;
			m_bEmailSent = false;
			m_strFirstName = "";
			m_strLastName = "";
			m_strEmail = "";
			m_strPhone = "";
			m_strBrowser = "";
			m_strDescription = "";
			m_strDetails = "";
		}
#endregion

#region Accessors
		public long AccountID
		{
			get
			{
				return this.m_nAccountID;
			}
			set
			{
				this.m_nAccountID = value;
			}
		}

		public bool EmailSent
		{
			get
			{
				return this.m_bEmailSent;
			}
			set
			{
				this.m_bEmailSent = value;
			}
		}

		public string FirstName
		{
			get
			{
				return this.m_strFirstName;
			}
			set
			{
				this.m_strFirstName = value;
			}
		}

		public string LastName
		{
			get
			{
				return this.m_strLastName;
			}
			set
			{
				this.m_strLastName = value;
			}
		}

		public string Email
		{
			get
			{
				return this.m_strEmail;
			}
			set
			{
				this.m_strEmail = value;
			}
		}

		public string Phone
		{
			get
			{
				return this.m_strPhone;
			}
			set
			{
				this.m_strPhone = value;
			}
		}

		public string Browser
		{
			get
			{
				return this.m_strBrowser;
			}
			set
			{
				this.m_strBrowser = value;
			}
		}

		public string Description
		{
			get
			{
				return this.m_strDescription;
			}
			set
			{
				this.m_strDescription = value;
			}
		}

		public string Details
		{
			get
			{
				return this.m_strDetails;
			}
			set
			{
				this.m_strDetails = value;
			}
		}

#endregion

		private string BuildAddString()
		{
			const string strSQLAddSR = "INSERT INTO SupportRequests(userID,EmailSent,FirstName,LastName,Email,Phone,Browser,[Description],Details,CreationUser,CreationDate,LastUpdateUserTime) VALUES (";
			StringBuilder sbSQLQuery = new StringBuilder(strSQLAddSR);
			sbSQLQuery.Append(AccountID);
			sbSQLQuery.Append(",").Append(SQLBitFromBool(EmailSent));

			string strClean = Sanitize(USCBase.Truncate(FirstName, 49, true));
			sbSQLQuery.Append(",'").Append(strClean).Append("'");
			strClean = Sanitize(USCBase.Truncate(LastName, 49, true));
			sbSQLQuery.Append(",'").Append(strClean).Append("'");
			strClean = Sanitize(USCBase.Truncate(Email, 49, true));
			sbSQLQuery.Append(",'").Append(strClean).Append("'");
			strClean = Sanitize(USCBase.Truncate(Phone, 49, true));
			sbSQLQuery.Append(",'").Append(strClean).Append("'");
			strClean = Sanitize(USCBase.Truncate(Browser, 49, true));
			sbSQLQuery.Append(",'").Append(strClean).Append("'");
			strClean = Sanitize(USCBase.Truncate(Description, 49, true));
			sbSQLQuery.Append(",'").Append(strClean).Append("'");
			strClean = Sanitize(USCBase.Truncate(Details, 2999, true));
			sbSQLQuery.Append(",'").Append(strClean).Append("'");

			// Creator Etc
			sbSQLQuery.Append(",'system'");
			sbSQLQuery.Append(",").Append("GETDATE()");

			strClean = "system -- " + "" + DateTime.Now.ToString();
			sbSQLQuery.Append(",'").Append(Sanitize(strClean)).Append("'");
			sbSQLQuery.Append(")");

			return sbSQLQuery.ToString();
		}

		public bool Add()
		{
			bool fRet = false;
			string strSQLQuery = BuildAddString();
			try
			{
				long lInsertID = ExecuteSQLInsert( strSQLQuery );
				if( 0 > lInsertID )
				{
					EvtLog.WriteEvent("SupportRequest: Failed to write support request.", EventLogEntryType.Error, 0, 0);
				}
			}
			catch( Exception ex )
			{
				EvtLog.WriteException("SupportRequest.Add failure", ex, 0);
			}
			return fRet;
		}
	}
}