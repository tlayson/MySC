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
	public class Event : USCBaseItem
	{
		public enum EventTypes
		{
			Undefined = 0,
			Game = 1,
			Practice = 2,
			PracticeGame = 3,
			Tournament = 4,
			Playoff = 5,
			Race = 6,
			Match = 7,
			Meet = 8,
			Jamboree = 9,
			Ride = 10,
			Workout = 11,
			Meeting = 12,
			Party = 13,
			Other = 100
		}

		public string EventTypeToString()
		{
			string strRet = "";

			switch( m_nEventType )
			{
				case EventTypes.Game:
				{
					strRet = "Game";
					break;
				}
				case EventTypes.Practice:
				{
					strRet = "Practice";
					break;
				}
				case EventTypes.PracticeGame:
				{
					strRet = "Practice Game";
					break;
				}
				case EventTypes.Tournament:
				{
					strRet = "Tournament";
					break;
				}
				case EventTypes.Playoff:
				{
					strRet = "Playoff";
					break;
				}
				case EventTypes.Race:
				{
					strRet = "Race";
					break;
				}
				case EventTypes.Match:
				{
					strRet = "Match";
					break;
				}
				case EventTypes.Meet:
				{
					strRet = "Meet";
					break;
				}
				case EventTypes.Jamboree:
				{
					strRet = "Jamboree";
					break;
				}
				case EventTypes.Ride:
				{
					strRet = "Ride";
					break;
				}
				case EventTypes.Workout:
				{
					strRet = "Workout";
					break;
				}
				case EventTypes.Meeting:
				{
					strRet = "Meeting";
					break;
				}
				case EventTypes.Party:
				{
					strRet = "Party";
					break;
				}
				case EventTypes.Other:
				{
					strRet = "Other";
					break;
				}
			}

			return strRet;
		}

		public void SetEventType( string strValue )
		{
			EventType = EventTypes.Undefined;
			switch( strValue )
			{
				case "Game":
				{
					EventType = EventTypes.Game;
					break;
				}
				case "Practice":
				{
					EventType = EventTypes.Practice;
					break;
				}
				case "Practice Game":
				{
					EventType = EventTypes.PracticeGame;
					break;
				}
				case "Tournament":
				{
					EventType = EventTypes.Tournament;
					break;
				}
				case "Playoff":
				{
					EventType = EventTypes.Playoff;
					break;
				}
				case "Race":
				{
					EventType = EventTypes.Race;
					break;
				}
				case "Match":
				{
					EventType = EventTypes.Match;
					break;
				}
				case "Meet":
				{
					EventType = EventTypes.Meet;
					break;
				}
				case "Jamboree":
				{
					EventType = EventTypes.Jamboree;
					break;
				}
				case "Ride":
				{
					EventType = EventTypes.Ride;
					break;
				}
				case "Workout":
				{
					EventType = EventTypes.Workout;
					break;
				}
				case "Meeting":
				{
					EventType = EventTypes.Meeting;
					break;
				}
				case "Party":
				{
					EventType = EventTypes.Party;
					break;
				}
				case "Other":
				{
					EventType = EventTypes.Other;
					break;
				}
			}
		}

		public void SetEventTypeByValue( string strValue )
		{
			EventType = EventTypes.Undefined;
			switch( strValue )
			{
				case "1":
				{
					EventType = EventTypes.Game;
					break;
				}
				case "2":
				{
					EventType = EventTypes.Practice;
					break;
				}
				case "3":
				{
					EventType = EventTypes.PracticeGame;
					break;
				}
				case "4":
				{
					EventType = EventTypes.Tournament;
					break;
				}
				case "5":
				{
					EventType = EventTypes.Playoff;
					break;
				}
				case "6":
				{
					EventType = EventTypes.Race;
					break;
				}
				case "7":
				{
					EventType = EventTypes.Match;
					break;
				}
				case "8":
				{
					EventType = EventTypes.Meet;
					break;
				}
				case "9":
				{
					EventType = EventTypes.Jamboree;
					break;
				}
				case "10":
				{
					EventType = EventTypes.Ride;
					break;
				}
				case "11":
				{
					EventType = EventTypes.Workout;
					break;
				}
				case "12":
				{
					EventType = EventTypes.Meeting;
					break;
				}
				case "13":
				{
					EventType = EventTypes.Party;
					break;
				}
				case "100":
				{
					EventType = EventTypes.Other;
					break;
				}
			}
		}

#region Member Variables
/*
	EventID bigint NOT NULL IDENTITY,
	OrgID bigint NOT NULL,
	VenueID bigint NOT NULL,
	SeasonID bigint NOT NULL,
	EventType int NOT NULL,
	EventName nvarchar(250) NOT NULL,
	AltLocation nvarchar(50) NOT NULL,
	EventDate datetime2(7) NOT NULL,
	OpponentID bigint NOT NULL,
	Opponent nvarchar(50) NOT NULL,
	HomeAway nvarchar(10) NOT NULL,
	Uniform nvarchar(20) NOT NULL,
	EventResult nvarchar(50) NOT NULL,
	Comments nvarchar(max),
	URL nvarchar(250),
	RequestResponse bit NOT NULL,
	ResponseLevel int NOT NULL,
	SendReminders bit NOT NULL,
	ReminderLevel int NOT NULL,
	ReminderDays int NOT NULL,
	EditLevel int NOT NULL,
	ViewLevel int NOT NULL,
	ReservedInt int NOT NULL,
	ReservedLong bigint NOT NULL,
	ReservedString nvarchar(50) NOT NULL,
	Deleted bit NOT NULL DEFAULT(0),
	CreationUser nvarchar(50),
	CreationDate datetime2(7) DEFAULT(getdate()),
	LastUpdate nvarchar(max),
 */
		private long m_lOrgID;
		private long m_lVenueID;
		private long m_lSeasonID;
		private EventTypes m_nEventType;
		private string m_strEventName;
		private string m_strAltLocation;
		private DateTime m_dtEventDate;
		private long m_lOpponentID;
		private string m_strOpponent;
		private string m_strHomeAway;
		private string m_strUniform;
		private string m_strResult;
		private string m_strComments;
		private string m_strURL;
		private bool m_fRequestResponse;
		private int m_nResponseLevel;
		private bool m_fSendReminder;
		private int m_nReminderLevel;
		private int m_nReminderDays;
		private int m_nEditLevel;
		private int m_nViewLevel;
		private int m_nReminderSent;
		private long m_lReserved;
		private string m_strReserved;
		private bool m_fDeleted;
#endregion

#region Init
		protected void InitEvent()
		{
			m_lOrgID = -1;
			m_lVenueID = -1;
			m_lSeasonID = -1;
			m_nEventType = EventTypes.Undefined;
			m_strEventName = "";
			m_strAltLocation = "";
			m_dtEventDate = DateTime.MinValue;
			m_lOpponentID = -1;
			m_strOpponent = "";
			m_strHomeAway = "";
			m_strUniform = "";
			m_strResult = "";
			m_strComments = "";
			m_strURL = "";
			m_fRequestResponse = true;
			m_fSendReminder = true;
			m_nResponseLevel = -1;
			m_nReminderLevel = -1;
			m_nReminderDays = -1;
			m_nEditLevel = -1;
			m_nViewLevel = -1;
			m_nReminderSent = -1;
			m_lReserved = -1;
			m_strReserved = "";
			m_fDeleted = false;
		}

		public Event()
		{
			InitEvent();
		}

#endregion

#region Accessors
		public long EventID
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

		public long VenueID
		{
			get
			{
				return this.m_lVenueID;
			}
			set
			{
				this.m_lVenueID = value;
			}
		}

		public long SeasonID
		{
			get
			{
				return this.m_lSeasonID;
			}
			set
			{
				this.m_lSeasonID = value;
			}
		}

		public EventTypes EventType
		{
			get
			{
				return this.m_nEventType;
			}
			set
			{
				this.m_nEventType = value;
			}
		}

		public string EventName
		{
			get
			{
				return this.m_strEventName;
			}
			set
			{
				this.m_strEventName = value;
			}
		}

		public string AltLocation
		{
			get
			{
				return this.m_strAltLocation;
			}
			set
			{
				this.m_strAltLocation = value;
			}
		}

		public DateTime EventDate
		{
			get
			{
				return this.m_dtEventDate;
			}
			set
			{
				this.m_dtEventDate = value;
			}
		}

		public long OpponentID
		{
			get
			{
				return this.m_lOpponentID;
			}
			set
			{
				this.m_lOpponentID = value;
			}
		}

		public string Opponent
		{
			get
			{
				return this.m_strOpponent;
			}
			set
			{
				this.m_strOpponent = value;
			}
		}

		public string HomeAway
		{
			get
			{
				return this.m_strHomeAway;
			}
			set
			{
				this.m_strHomeAway = value;
			}
		}

		public string Uniform
		{
			get
			{
				return this.m_strUniform;
			}
			set
			{
				this.m_strUniform = value;
			}
		}

		public string EventResult
		{
			get
			{
				return this.m_strResult;
			}
			set
			{
				this.m_strResult = value;
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

		public string URL
		{
			get
			{
				return this.m_strURL;
			}
			set
			{
				this.m_strURL = value;
			}
		}

		public bool RequestResponse
		{
			get
			{
				return this.m_fRequestResponse;
			}
			set
			{
				this.m_fRequestResponse = value;
			}
		}

		public int ResponseLevel
		{
			get
			{
				return this.m_nResponseLevel;
			}
			set
			{
				this.m_nResponseLevel = value;
			}
		}

		public bool SendReminder
		{
			get
			{
				return this.m_fSendReminder;
			}
			set
			{
				this.m_fSendReminder = value;
			}
		}

		public int ReminderLevel
		{
			get
			{
				return this.m_nReminderLevel;
			}
			set
			{
				this.m_nReminderLevel = value;
			}
		}

		public int ReminderDays
		{
			get
			{
				return this.m_nReminderDays;
			}
			set
			{
				this.m_nReminderDays = value;
			}
		}

		public int EditLevel
		{
			get
			{
				return this.m_nEditLevel;
			}
			set
			{
				this.m_nEditLevel = value;
			}
		}

		public int ViewLevel
		{
			get
			{
				return this.m_nViewLevel;
			}
			set
			{
				this.m_nViewLevel = value;
			}
		}

		public int ReminderSent
		{
			get
			{
				return this.m_nReminderSent;
			}
			set
			{
				this.m_nReminderSent = value;
			}
		}

		public long ReservedLong
		{
			get
			{
				return this.m_lReserved;
			}
			set
			{
				this.m_lReserved = value;
			}
		}

		public string ReservedString
		{
			get
			{
				return this.m_strReserved;
			}
			set
			{
				this.m_strReserved = value;
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
CREATE PROCEDURE sp_UpdateOrgEvent
	@OrgID bigint, 
	@VenueID bigint, 
	@SeasonID bigint, 
	@EventType int,
	@EventName nvarchar(250),
	@AltLocation nvarchar(50),
	@EventDate datetime2(7),
	@OpponentID bigint, 
	@Opponent nvarchar(50),
	@HomeAway nvarchar(10),
	@Uniform nvarchar(20),
	@EventResult nvarchar(50),
	@Comments nvarchar(max),
	@URL nvarchar(250),
	@RequestResponse bit,
	@ResponseLevel int,
	@SendReminders bit,
	@ReminderLevel int,
	@ReminderDays int,
	@EditLevel int,
	@ViewLevel int,
	@Deleted bit,
	@Update nvarchar(max),
	@EventID bigint
*/
		public bool Update(UserAccount acct)
		{
			bool fRet = false;
			try
			{
				string strClean = acct.UserName + " -- " + DateTime.Now.ToString();
				this.LastUpdate = Sanitize(strClean);

				SqlParameter[] paramArray = new SqlParameter[25];
				paramArray[0] = new SqlParameter("@OrgID", OrgID);
				paramArray[1] = new SqlParameter("@VenueID", VenueID);
				paramArray[2] = new SqlParameter("@SeasonID", SeasonID);
				paramArray[3] = new SqlParameter("@EventType", EventType);
				paramArray[4] = new SqlParameter("@EventName", Sanitize(USCBase.Truncate(this.EventName, 249, true)));
				paramArray[5] = new SqlParameter("@AltLocation", Sanitize(USCBase.Truncate(this.AltLocation, 49, true)));
				paramArray[6] = new SqlParameter("@EventDate", this.EventDate);
				paramArray[7] = new SqlParameter("@OpponentID", OpponentID);
				paramArray[8] = new SqlParameter("@Opponent", Sanitize(USCBase.Truncate(this.Opponent, 49, true)));
				paramArray[9] = new SqlParameter("@HomeAway", Sanitize(USCBase.Truncate(this.HomeAway, 9, true)));
				paramArray[10] = new SqlParameter("@Uniform", Sanitize(USCBase.Truncate(this.Uniform, 19, true)));
				paramArray[11] = new SqlParameter("@EventResult", Sanitize(USCBase.Truncate(this.EventResult, 49, true)));
				paramArray[12] = new SqlParameter("@Comments", Sanitize(this.Comments));
				paramArray[13] = new SqlParameter("@URL", Sanitize(USCBase.Truncate(this.URL, 249, true)));
				paramArray[14] = new SqlParameter("@RequestResponse", SQLBitFromBool(this.RequestResponse));
				paramArray[15] = new SqlParameter("@ResponseLevel", ResponseLevel);
				paramArray[16] = new SqlParameter("@SendReminders", SQLBitFromBool(this.SendReminder));
				paramArray[17] = new SqlParameter("@ReminderLevel", ReminderLevel);
				paramArray[18] = new SqlParameter("@ReminderDays", ReminderDays);
				paramArray[19] = new SqlParameter("@EditLevel", EditLevel);
				paramArray[20] = new SqlParameter("@ViewLevel", ViewLevel);
				paramArray[21] = new SqlParameter("@ReminderSent", ReminderSent);
				paramArray[22] = new SqlParameter("@Deleted", SQLBitFromBool(this.Deleted));
				paramArray[23] = new SqlParameter("@Update", this.LastUpdate);
				paramArray[24] = new SqlParameter("@EventID", EventID);

				if (ExecuteSPNoValue("sp_UpdateOrgEvent", paramArray))
				{
					fRet = true;
				}
			}
			catch (Exception ex)
			{
				string strErr = "Error updating event " + EventID + " for org = " + OrgID;
				short sCat = 0;
				if (IsLocalInstance())
				{
					strErr += " [Local] ";
					sCat = 99;
				}
				EvtLog.WriteException(strErr, ex, EventErrors.ErrorType.OrgUpdateEvents, sCat);
				fRet = false;
			}

			return fRet;
		}
#endregion
	}

	public class EventList : USCBaseList
	{
#region Column Constants
/*
	EventID bigint NOT NULL IDENTITY,
	OrgID bigint NOT NULL,
	VenueID bigint NOT NULL,
	SeasonID bigint NOT NULL,
	EventType int NOT NULL,
	EventName nvarchar(250) NOT NULL,
	AltLocation nvarchar(50) NOT NULL,
	EventDate datetime2(7) NOT NULL,
	OpponentID bigint NOT NULL,
	Opponent nvarchar(50) NOT NULL,
	HomeAway nvarchar(10) NOT NULL,
	Uniform nvarchar(20) NOT NULL,
	EventResult nvarchar(50) NOT NULL,
	Comments nvarchar(max),
	URL nvarchar(250),
	RequestResponse bit NOT NULL,
	ResponseLevel int NOT NULL,
	SendReminders bit NOT NULL,
	ReminderLevel int NOT NULL,
	ReminderDays int NOT NULL,
	EditLevel int NOT NULL,
	ViewLevel int NOT NULL,
	ReservedInt int NOT NULL,
	ReservedLong bigint NOT NULL,
	ReservedString nvarchar(50) NOT NULL,
	Deleted bit NOT NULL DEFAULT(0),
	CreationUser nvarchar(50),
	CreationDate datetime2(7) DEFAULT(getdate()),
	LastUpdate nvarchar(max),
 */
		const int colKey = 0;
		const int colOrgID = 1;
		const int colVenueID = 2;
		const int colSeasonID = 3;
		const int colEventType = 4;
		const int colEventName = 5;
		const int colAltLocation = 6;
		const int colEventDate = 7;
		const int colOpponentID = 8;
		const int colOpponent = 9;
		const int colHomeAway = 10;
		const int colUniforms = 11;
		const int colEventResult = 12;
		const int colComments = 13;
		const int colURL = 14;
		const int colRequestResponse = 15;
		const int colResponseLevel = 16;
		const int colSendReminder = 17;
		const int colReminderLevel = 18;
		const int colReminderDays = 19;
		const int colEditLevel = 20;
		const int colViewLevel = 21;
		const int colReminderSent = 22;
		const int colReservedLong = 23;
		const int colReservedString = 24;
		const int colDeleted = 25;
		const int colCreator = 26;
		const int colCreateDate = 27;
		const int colLastUpdate = 28;
#endregion

#region Init
		private long m_lOrgID;
		public SortedDictionary<long, object> m_sdOrgEvents = new SortedDictionary<long, object>();

		public EventList()
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
			if ( fForce)
			{
				OrgID = orgID;
				Load();
			}
		}
#endregion

#region Load
		// Used by the notification service
		public bool LoadAll( string cnxString )
		{
			bool fRet = false;

			m_strConnectionString = cnxString;
			m_sdOrgEvents.Clear();

			SqlDataReader reader = null;
			SqlConnection sqlConn = null;

			DateTime dtNow = DateTime.Now;

			SqlParameter[] paramArray = new SqlParameter[1];
			paramArray[0] = new SqlParameter("@StartDate", OrgID);
			paramArray[1] = new SqlParameter("@EndDate", OrgID);

			if (ExecuteSPRows("sp_GetAllFutureEvents", null, out reader, out sqlConn) && null != reader)
			{
				try
				{
					while (reader.Read())
					{
						IDataRecord dr = (IDataRecord)reader;
						Event evt = new Event();
						fRet = ReadEvent(dr, evt);
						if (fRet)
						{
							long lIndex = m_sdOrgEvents.Count + 1;
							m_sdOrgEvents.Add( lIndex, evt );
						}
					}
					reader.Close();
					sqlConn.Close();
					fRet = true;
				}
				catch (Exception ex)
				{
					string strErr = "Error loading Org events for " + OrgID;
					short sCat = 0;
					if (IsLocalInstance())
					{
						strErr += " [Local] ";
						sCat = 99;
					}
					EvtLog.WriteException(strErr, ex, EventErrors.ErrorType.OrgLoadEvents, sCat);
					fRet = false;
				}
			}

			return fRet;
		}

		public bool Load()
		{
			bool fRet = false;

			m_sdOrgEvents.Clear();

			if (0 > OrgID)
			{
				return false;
			}

			SqlDataReader reader = null;
			SqlConnection sqlConn = null;
			SqlParameter[] paramArray = new SqlParameter[1];
			paramArray[0] = new SqlParameter("@OrgID", OrgID);

			if (ExecuteSPRows("sp_GetOrgEvents", paramArray, out reader, out sqlConn) && null != reader)
			{
				try
				{
					while (reader.Read())
					{
						IDataRecord dr = (IDataRecord)reader;
						Event evt = new Event();
						fRet = ReadEvent(dr, evt);
						if (fRet)
						{
							long lIndex = m_sdOrgEvents.Count + 1;
							m_sdOrgEvents.Add( lIndex, evt );
						}
					}
					reader.Close();
					sqlConn.Close();
					fRet = true;
				}
				catch (Exception ex)
				{
					string strErr = "Error loading Org events for " + OrgID;
					short sCat = 0;
					if (IsLocalInstance())
					{
						strErr += " [Local] ";
						sCat = 99;
					}
					EvtLog.WriteException(strErr, ex, EventErrors.ErrorType.OrgLoadEvents, sCat);
					fRet = false;
				}
			}

			return fRet;
		}

		private bool ReadEvent(IDataRecord dr, Event evt)
		{
			bool fRet = true;
			try
			{
				evt.ConnectionString = m_strConnectionString;
				evt.EventID = ObjectToLong(dr[colKey]);
				evt.OrgID = ObjectToLong(dr[colOrgID]);
				evt.VenueID = ObjectToLong(dr[colVenueID]);
				evt.SeasonID = ObjectToLong(dr[colSeasonID]);
				evt.EventType = (Event.EventTypes)ObjectToInt(dr[colEventType]);
				evt.EventName = ObjectToString(dr[colEventName]);
				evt.AltLocation = ObjectToString(dr[colAltLocation]);
				evt.EventDate = ObjectToDateTime(dr[colEventDate]);
				evt.OpponentID = ObjectToLong(dr[colOpponentID]);
				evt.Opponent = ObjectToString(dr[colOpponent]);
				evt.HomeAway = ObjectToString(dr[colHomeAway]);
				evt.Uniform = ObjectToString(dr[colUniforms]);
				evt.EventResult = ObjectToString(dr[colEventResult]);
				evt.Comments = ObjectToString(dr[colComments]);
				evt.URL = ObjectToString(dr[colURL]);
				evt.RequestResponse = ObjectToBool(dr[colRequestResponse]);
				evt.ResponseLevel = ObjectToInt(dr[colResponseLevel]);
				evt.SendReminder = ObjectToBool(dr[colSendReminder]);
				evt.ReminderLevel = ObjectToInt(dr[colReminderLevel]);
				evt.ReminderDays = ObjectToInt(dr[colReminderDays]);
				evt.EditLevel = ObjectToInt(dr[colEditLevel]);
				evt.ViewLevel = ObjectToInt(dr[colViewLevel]);
				evt.ReminderSent = ObjectToInt(dr[colReminderSent]);
				evt.ReservedLong = ObjectToLong(dr[colReservedLong]);
				evt.ReservedString = ObjectToString(dr[colResponseLevel]);

				evt.Deleted = ObjectToBool(dr[colDeleted]);
				evt.Creator = ObjectToString(dr[colCreator]);
				evt.CreateDate = ObjectToDateTime(dr[colCreateDate]);
				evt.LastUpdate = ObjectToString(dr[colLastUpdate]);
			}
			catch (Exception ex)
			{
				EvtLog.WriteException("MyOrgEvent.Read failure", ex, 0);
				fRet = false;
			}
			return fRet;
		}
#endregion Load

#region Counters
		public int Count
		{
			get
			{
				return m_sdOrgEvents.Count;
			}
		}

		public Event GetEvent(long index)
		{
			Event evt = null;
			if( index > 0 )
			{
				foreach (KeyValuePair<long, object> kvp in m_sdOrgEvents)
				{
					Event tmp = (Event)kvp.Value;
					if (tmp.EventID == index)
					{
						evt = tmp;
						break;
					}
				}
			}
			return evt;
		}
#endregion

#region Manage
/*
CREATE PROCEDURE sp_AddOrgEvent
	@OrgID bigint, 
	@VenueID bigint, 
	@SeasonID bigint, 
	@EventType int,
	@EventName nvarchar(250),
	@AltLocation nvarchar(50),
	@EventDate datetime2(7),
	@OpponentID bigint, 
	@Opponent nvarchar(50),
	@HomeAway nvarchar(10),
	@Uniform nvarchar(20),
	@EventResult nvarchar(50),
	@Comments nvarchar(max),
	@URL nvarchar(250),
	@RequestResponse bit,
	@ResponseLevel int,
	@SendReminders bit,
	@ReminderLevel int,
	@ReminderDays int,
	@EditLevel int,
	@ViewLevel int,
	@Deleted bit,
	@Creator nvarchar(50)
*/
		public bool Add(Event evt)
		{
			bool fRet = false;

			try
			{
				SqlParameter[] paramArray = new SqlParameter[24];
				paramArray[0] = new SqlParameter("@OrgID", evt.OrgID);
				paramArray[1] = new SqlParameter("@VenueID", evt.VenueID);
				paramArray[2] = new SqlParameter("@SeasonID", evt.SeasonID);
				paramArray[3] = new SqlParameter("@EventType", evt.EventType);
				paramArray[4] = new SqlParameter("@EventName", Sanitize(USCBase.Truncate(evt.EventName, 249, true)));
				paramArray[5] = new SqlParameter("@AltLocation", Sanitize(USCBase.Truncate(evt.AltLocation, 49, true)));
				paramArray[6] = new SqlParameter("@EventDate", evt.EventDate);
				paramArray[7] = new SqlParameter("@OpponentID", evt.OpponentID);
				paramArray[8] = new SqlParameter("@Opponent", Sanitize(USCBase.Truncate(evt.Opponent, 49, true)));
				paramArray[9] = new SqlParameter("@HomeAway", Sanitize(USCBase.Truncate(evt.HomeAway, 9, true)));
				paramArray[10] = new SqlParameter("@Uniform", Sanitize(USCBase.Truncate(evt.Uniform, 19, true)));
				paramArray[11] = new SqlParameter("@EventResult", Sanitize(USCBase.Truncate(evt.EventResult, 49, true)));
				paramArray[12] = new SqlParameter("@Comments", Sanitize(evt.Comments));
				paramArray[13] = new SqlParameter("@URL", Sanitize(USCBase.Truncate(evt.URL, 249, true)));
				paramArray[14] = new SqlParameter("@RequestResponse", SQLBitFromBool(evt.RequestResponse));
				paramArray[15] = new SqlParameter("@ResponseLevel", evt.ResponseLevel);
				paramArray[16] = new SqlParameter("@SendReminders", SQLBitFromBool(evt.SendReminder));
				paramArray[17] = new SqlParameter("@ReminderLevel", evt.ReminderLevel);
				paramArray[18] = new SqlParameter("@ReminderDays", evt.ReminderDays);
				paramArray[19] = new SqlParameter("@EditLevel", evt.EditLevel);
				paramArray[20] = new SqlParameter("@ViewLevel", evt.ViewLevel);
				paramArray[21] = new SqlParameter("@ReminderSent", evt.ReminderSent);
				paramArray[22] = new SqlParameter("@Deleted", SQLBitFromBool(evt.Deleted));
				paramArray[23] = new SqlParameter("@Creator", Sanitize(USCBase.Truncate(evt.Creator, 49, true)));

				evt.Key = ExecuteSPInsert("sp_AddOrgEvent", paramArray);
				if (evt.Key > 0)
				{
					long lIndex = m_sdOrgEvents.Count + 1;
					m_sdOrgEvents.Add( lIndex, evt );
					fRet = true;
				}
			}
			catch (Exception ex)
			{
				string strErr = "Error adding event for org = " + OrgID;
				short sCat = 0;
				if (IsLocalInstance())
				{
					strErr += " [Local] ";
					sCat = 99;
				}
				EvtLog.WriteException(strErr, ex, EventErrors.ErrorType.OrgAddEvents, sCat);
				fRet = false;
			}

			return fRet;
		}

		public bool Update(Event evt, UserAccount acct)
		{
			return evt.Update(acct);
		}

		public bool Delete(Event evt, UserAccount acct)
		{
			// We don't really want to delete it, just make inactive.
			evt.Deleted = true;
			return evt.Update(acct);
		}
#endregion

	}

}