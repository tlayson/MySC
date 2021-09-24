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
	public class EventResponse : USCBaseItem
	{
		public enum ResponseTypes
		{
			Undefined = 0,
			Yes = 1,
			No = 2,
			Maybe = 3,
			NoResponse = 4
		}

#region Member Variables
		private long m_lOrgID;
		private long m_lEventID;
		private long m_lMemberID;
		private ResponseTypes m_nResponse;
		private string m_strNotes;
		private DateTime m_dtResponseDate;
#endregion

		public void SetResponseFromString( string strResponse )
		{
			strResponse = strResponse.ToLower();

			if( "yes" == strResponse )
			{
				m_nResponse = ResponseTypes.Yes;
			}
			else if( "no" == strResponse )
			{
				m_nResponse = ResponseTypes.No;
			}
			else if( "maybe" == strResponse )
			{
				m_nResponse = ResponseTypes.Maybe;
			}
			else
			{
				m_nResponse = ResponseTypes.NoResponse;
			}
		}

		public string ResponseTypeToString()
		{
			string strRet = "";

			switch( m_nResponse )
			{
				case ResponseTypes.Yes:
				{
					strRet = "Yes";
					break;
				}
				case ResponseTypes.No:
				{
					strRet = "No";
					break;
				}
				case ResponseTypes.Maybe:
				{
					strRet = "Maybe";
					break;
				}
				case ResponseTypes.NoResponse:
				{
					strRet = "No Response";
					break;
				}
			}

			return strRet;
		}


#region Init
		protected void InitRSVP()
		{
			m_lOrgID = -1;
			m_lEventID = -1;
			m_lMemberID = -1;
			m_nResponse = ResponseTypes.Undefined;
			m_strNotes = "";
			m_dtResponseDate = DateTime.Now;
		}

		public EventResponse()
		{
			InitRSVP();
		}
#endregion

#region Accessors
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

		public long EventID
		{
			get
			{
				return this.m_lEventID;
			}
			set
			{
				this.m_lEventID = value;
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

		public ResponseTypes Response
		{
			get
			{
				return this.m_nResponse;
			}
			set
			{
				this.m_nResponse = value;
			}
		}

		public string Notes
		{
			get
			{
				return this.m_strNotes;
			}
			set
			{
				this.m_strNotes = value;
			}
		}

		public DateTime ResponseDate
		{
			get
			{
				return this.m_dtResponseDate;
			}
			set
			{
				this.m_dtResponseDate = value;
			}
		}

#endregion

#region Update

/*
CREATE PROCEDURE sp_UpdateOrgEventResponse
	@Response int,
	@Notes nvarchar(250),
	@Update nvarchar(max),
	@EventID bigint,
	@MemberID bigint
*/
		public bool Update(UserAccount acct)
		{
			bool fRet = false;
			try
			{
				string strClean = acct.UserName + " -- " + DateTime.Now.ToString();
				this.LastUpdate = Sanitize(strClean);

				SqlParameter[] paramArray = new SqlParameter[5];
				paramArray[0] = new SqlParameter("@Response", this.Response);
				paramArray[1] = new SqlParameter("@Notes", Sanitize(USCBase.Truncate(this.Notes, 249, true)));
				paramArray[2] = new SqlParameter("@ResponseDate", this.ResponseDate);
				paramArray[3] = new SqlParameter("@EventID", EventID);
				paramArray[4] = new SqlParameter("@MemberID", MemberID);

				if (ExecuteSPNoValue("sp_UpdateOrgEventResponse", paramArray))
				{
					fRet = true;
				}
			}
			catch (Exception ex)
			{
				string strErr = "EventResponse.Update failure";
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

	public class EventResponseList : USCBaseList
	{
#region Column Constants
/*
	OrgID bigint NOT NULL,
	EventID bigint NOT NULL,
	MemberID bigint NOT NULL,
	Response int NOT NULL,
	Notes nvarchar(250) NOT NULL,
	ResponseDate datetime2(7)
*/
		const int colOrgID = 0;
		const int colEventID = 1;
		const int colMemberID = 2;
		const int colResponse = 3;
		const int colNotes = 4;
		const int colResponseDate = 5;
#endregion

#region Init
		private long m_lOrgID;
		public SortedDictionary<long, object> m_sdOrgEventResponse = new SortedDictionary<long, object>();

		public EventResponseList()
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

		public void Init(string cnxString, long orgID, bool fForce)
		{
			m_strConnectionString = cnxString;
			if (null == m_sdOrgEventResponse || fForce)
			{
				OrgID = orgID;
				Load();
			}
		}
#endregion

#region Load
		public bool Load()
		{
			bool fRet = false;

			m_sdOrgEventResponse.Clear();

			if (0 > OrgID)
			{
				return false;
			}

			SqlDataReader reader = null;
			SqlConnection sqlConn = null;
			SqlParameter[] paramArray = new SqlParameter[1];
			paramArray[0] = new SqlParameter("@OrgID", OrgID);

			if (ExecuteSPRows("sp_LoadOrgEventResponse", paramArray, out reader, out sqlConn) && null != reader)
			{
				try
				{
					while (reader.Read())
					{
						IDataRecord dr = (IDataRecord)reader;
						EventResponse evt = new EventResponse();
						fRet = ReadEventResponse(dr, evt);
						if (fRet)
						{
							long lIndex = m_sdOrgEventResponse.Count + 1;
							m_sdOrgEventResponse.Add( lIndex, evt );
						}
					}
					reader.Close();
					sqlConn.Close();
					fRet = true;
				}
				catch (Exception ex)
				{
					string strErr = "Error loading Org EventResponse for " + OrgID;
					short sCat = 0;
					if (IsLocalInstance())
					{
						strErr += " [Local] ";
						sCat = 99;
					}
					EvtLog.WriteException(strErr, ex, EventErrors.ErrorType.MyOrgGeneric, sCat);
					fRet = false;
				}
			}

			return fRet;
		}

/*
		const int colOrgID = 0;
		const int colEventID = 1;
		const int colMemberID = 2;
		const int colResponse = 3;
		const int colNotes = 4;
		const int colResponseDate = 5;
*/
		private bool ReadEventResponse(IDataRecord dr, EventResponse evt)
		{
			bool fRet = true;
			try
			{
				evt.ConnectionString = m_strConnectionString;
				evt.OrgID = ObjectToLong(dr[colOrgID]);
				evt.EventID = ObjectToLong(dr[colEventID]);
				evt.MemberID = ObjectToLong(dr[colMemberID]);
				evt.Response = (EventResponse.ResponseTypes)ObjectToInt(dr[colResponse]);
				evt.Notes = ObjectToString(dr[colNotes]);
				evt.ResponseDate = ObjectToDateTime(dr[colResponseDate]);
			}
			catch (Exception ex)
			{
				string strErr = "ReadEventResponse failure";
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

#region Misc
		public int Count
		{
			get
			{
				return m_sdOrgEventResponse.Count;
			}
		}

		public EventResponse GetEventResponse( long lEventID, long lMemberID )
		{
			EventResponse evtr = null;

			foreach( KeyValuePair<long, object> kvp in m_sdOrgEventResponse )
			{
				EventResponse evt = (EventResponse)kvp.Value;
				if( null != evt && lEventID == evt.EventID && lMemberID == evt.MemberID )
				{
					evtr = evt;
					break;
				}
			}

			return evtr;
		}
#endregion

#region Add
/*
CREATE PROCEDURE sp_AddOrgEventResponse
	@OrgID bigint, 
	@EventID bigint, 
	@MemberID bigint, 
	@Response int,
	@Notes nvarchar(250)
*/
		public bool Add(EventResponse evt)
		{
			bool fRet = false;

			try
			{
				SqlParameter[] paramArray = new SqlParameter[5];
				paramArray[0] = new SqlParameter("@OrgID", evt.OrgID);
				paramArray[1] = new SqlParameter("@EventID", evt.EventID);
				paramArray[2] = new SqlParameter("@MemberID", evt.MemberID);
				paramArray[3] = new SqlParameter("@Response", evt.Response);
				paramArray[4] = new SqlParameter("@Notes", Sanitize(USCBase.Truncate(evt.Notes, 249, true)));

				evt.Key = ExecuteSPInsert("sp_AddOrgEventResponse", paramArray);
				if (evt.Key > 0)
				{
					long lIndex = m_sdOrgEventResponse.Count + 1;
					m_sdOrgEventResponse.Add( lIndex, evt );
					fRet = true;
				}
			}
			catch (Exception ex)
			{
				string strErr = "EventResponse.Add failure";
				short sCat = 0;
				if (IsLocalInstance())
				{
					strErr += " [Local] ";
					sCat = 99;
				}
				EvtLog.WriteException(strErr, ex, EventErrors.ErrorType.AccountUpdate, sCat);
				fRet = false;
			}

			return fRet;
		}

		public bool Update(EventResponse evt, UserAccount acct)
		{
			return evt.Update(acct);
		}

		public void RemoveResponse( long lEventID, long lMemberID )
		{
			foreach( KeyValuePair<long, object> kvp in m_sdOrgEventResponse )
			{
				long lIndex = (long)kvp.Key;
				EventResponse evt = (EventResponse)kvp.Value;
				if( null != evt && lEventID == evt.EventID && lMemberID == evt.MemberID )
				{
					m_sdOrgEventResponse.Remove( lIndex );
					break;
				}
			}
		}

		public bool Delete( EventResponse evt, UserAccount acct )
		{
			bool fRet = false;

			try
			{
				int rowsAffected = 0;

				SqlParameter[] paramArray = new SqlParameter[3];
				paramArray[0] = new SqlParameter("@OrgID", evt.OrgID);
				paramArray[1] = new SqlParameter("@EventID", evt.EventID);
				paramArray[2] = new SqlParameter("@MemberID", evt.MemberID);

				if( SQLHelper.ExecuteSPNoValue( "sp_DeleteOrgEventResponse", m_strConnectionString, paramArray, out rowsAffected ) && rowsAffected > 0 )
				{
					RemoveResponse( evt.EventID, evt.MemberID );
					fRet = true;
				}
			}
			catch (Exception ex)
			{
				string strErr = "EventResponse.Delete failure";
				short sCat = 0;
				if (IsLocalInstance())
				{
					strErr += " [Local] ";
					sCat = 99;
				}
				EvtLog.WriteException(strErr, ex, EventErrors.ErrorType.AccountUpdate, sCat);
				fRet = false;
			}

			return fRet;
		}
#endregion
	}

}