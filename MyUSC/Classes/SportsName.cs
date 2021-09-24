using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using MyUSC.Classes;

namespace MyUSC.Classes
{
	public class SportName : SportsMenuItem
	{
		private long m_lSportNameID;
		private DateTime m_dtSeasonStart;
		private DateTime m_dtSeasonEnd;
		private bool m_bActive;

		private void Init()
		{
			m_lSportNameID = 0;
			m_dtSeasonStart = DateTime.Now;
			m_dtSeasonEnd = DateTime.Now;
			m_bActive = true;
		}

		public SportName()
		{
			Init();
		}

#region Update
		/*
CREATE PROCEDURE sp_UpdateSportsName 
	@SportsNameID bigint, 
	@RSSID bigint,
	@Name nvarchar(50),
	@Description nvarchar(50),
	@LogoURL nvarchar(500),
	@Sequence float,
	@Active bit,
	@Creator nvarchar(50),
	@Update nvarchar(max),
	@Website nvarchar(500),
	@RSSNotes nvarchar(200),
	@Key bigint 
 */
		public bool Update()
		 {
			bool fRet = false;

			SqlParameter[] paramArray = new SqlParameter[10];
			paramArray[0] = new SqlParameter("@SportsNameID", ID);
			paramArray[1] = new SqlParameter("@RSSID", RSSFeedKey);
			paramArray[2] = new SqlParameter("@Name", Name);
			paramArray[3] = new SqlParameter("@Description", Description);
			paramArray[4] = new SqlParameter("@LogoURL", LogoUrl);
			paramArray[5] = new SqlParameter("@Sequence", Sequence);
			paramArray[6] = new SqlParameter("@Active", SQLBitFromBool(Active));
			paramArray[7] = new SqlParameter("@Update", LastUpdate);
			paramArray[8] = new SqlParameter("@Website", Website);
			paramArray[9] = new SqlParameter("@RSSNotes", RSSNotes);
			paramArray[10] = new SqlParameter("@Key", Key);

			if (ExecuteSPNoValue("sp_UpdateSportsName", paramArray))
			{
				fRet = true;
			}

			return fRet;
		 }
#endregion

		#region Accessors
		public long ID
		{
			get
			{
				return this.m_lSportNameID;
			}
			set
			{
				this.m_lSportNameID = value;
			}
		}


		public DateTime SeasonStart
		{
			get
			{
				return this.m_dtSeasonStart;
			}
			set
			{
				this.m_dtSeasonStart = value;
			}
		}

		public DateTime SeasonEnd
		{
			get
			{
				return this.m_dtSeasonEnd;
			}
			set
			{
				this.m_dtCreateDate = value;
			}
		}

		public bool Active
		{
			get
			{
				return this.m_bActive;
			}
			set
			{
				this.m_bActive = value;
			}
		}

#endregion
	}

	public sealed class SportNameList : USCBaseList
	{
#region Column Constants
		const int colKey = 0;
		const int colSportNameID = 1;
		const int colRSSFeedKey = 2;
		const int colName = 3;
		const int colDesc = 4;
		const int colLogoUrl = 5;
		const int colSequence = 6;
		const int colSeasonStart = 7;
		const int colSeasonEnd = 8;
		const int colLanguage = 9;
		const int colActive = 10;
		const int colCreator = 11;
		const int colCreateDate = 12;
		const int colLastUpdate = 13;
		const int colWebsite = 14;
		const int colRSSNotes = 15;
#endregion

		private static volatile SportNameList instance = null;
		private static object syncRoot = new object();

		const string strSQLGetAllSportNames = "SELECT * FROM SportName";

		private SportNameList()
		{
		}

		public static SportNameList Instance
		{
			get
			{
				if (instance == null)
				{
					lock (syncRoot)
					{
						if (instance == null)
							instance = new SportNameList();
					}
				}

				return instance;
			}
		}

		public Hashtable htSportName;
		public SortedDictionary<long, object> m_sdSportNames = new SortedDictionary<long, object>();

		public void Init(string cnxString, bool fForce)
		{
			m_strConnectionString = cnxString;
			if (null == htSportName || fForce)
			{
				htSportName = new Hashtable();
				Load();
			}
		}

		public bool Load()
		{
			bool fRet = false;

			htSportName.Clear();
			m_sdSportNames.Clear();

			SqlConnection sqlConn = null;
			DataSet locStrDS = new DataSet();

			try
			{
				sqlConn = new SqlConnection(m_strConnectionString);
				sqlConn.Open();
				SqlDataAdapter daLocStrings = new SqlDataAdapter(strSQLGetAllSportNames, sqlConn);
				daLocStrings.Fill(locStrDS, "SportName");
			}
			catch (Exception ex)
			{
				EvtLog.WriteException("SportName.Load failure", ex, 0);
				return false;
			}
			finally
			{
				sqlConn.Close();
			}

			DataRowCollection dra = locStrDS.Tables["SportName"].Rows;
			foreach (DataRow dr in dra)
			{
				SportName sn = new SportName();
				fRet = ReadSportName(dr, sn);
				if (fRet)
				{
					htSportName.Add(sn.Key, sn);
				}
			}
			fRet = true;

			// Create sorted list
			foreach (DictionaryEntry de in htSportName)
			{
				SportName sportName = (SportName)de.Value;
				long lID = Convert.ToInt64(sportName.Sequence);
				m_sdSportNames.Add(lID, sportName);
			}

			return fRet;
		}

		private bool ReadSportName(DataRow dr, SportName sn)
		{
			bool fRet = true;
			try
			{
				sn.ConnectionString = m_strConnectionString;
				sn.Key = ObjectToLong(dr.ItemArray[colKey]);
				sn.ID = ObjectToLong(dr.ItemArray[colSportNameID]);
				sn.RSSFeedKey = ObjectToLong(dr.ItemArray[colRSSFeedKey]);
				sn.Name = ObjectToString(dr.ItemArray[colName]);
				sn.Description = ObjectToString(dr.ItemArray[colDesc]);
				sn.LogoUrl = ObjectToString(dr.ItemArray[colLogoUrl]);
				sn.Sequence = ObjectToFloat(dr.ItemArray[colSequence]);
				sn.SeasonStart = ObjectToDateTime(dr.ItemArray[colSeasonStart]);
				sn.SeasonEnd = ObjectToDateTime(dr.ItemArray[colSeasonEnd]);
				sn.Language = ObjectToInt(dr.ItemArray[colLanguage]);
				sn.Active = ObjectToBool(dr.ItemArray[colActive]);

				sn.Creator = ObjectToString(dr.ItemArray[colCreator]);
				sn.CreateDate = ObjectToDateTime(dr.ItemArray[colCreateDate]);
				sn.LastUpdate = ObjectToString(dr.ItemArray[colLastUpdate]);

				sn.Website = ObjectToString(dr.ItemArray[colWebsite]);
				sn.RSSNotes = ObjectToString(dr.ItemArray[colRSSNotes]);
			}
			catch (Exception ex)
			{
				EvtLog.WriteException("SportName.Read failure", ex, 0);
				fRet = false;
			}
			return fRet;
		}

		public int Count
		{
			get
			{
				return htSportName.Count;
			}
		}

		public SportName GetSportName(long index)
		{
			return (SportName)htSportName[index];
		}

		public SportName GetSportNameByID(long lID)
		{
			SportName sportName = null;

			foreach (DictionaryEntry de in htSportName)
			{
				SportName sportNameT = (SportName)de.Value;
				if (sportNameT.ID == lID)
				{
					// No need to keep looking.
					sportName = sportNameT;
					break;
				}
			}

			return sportName;
		}

		/*
		 CREATE PROCEDURE sp_AddSportsName
			@SportsNameID bigint, 
			@RSSID bigint,
			@Name nvarchar(50),
			@Description nvarchar(50),
			@LogoURL nvarchar(500),
			@Sequence float,
			@Creator nvarchar(50),
			@Update nvarchar(max),
			@Website nvarchar(500),
			@RSSNotes nvarchar(200)
		 */
		public bool Add(SportName sportName)
		{
			bool fRet = false;

			SqlParameter[] paramArray = new SqlParameter[10];
			paramArray[0] = new SqlParameter("@SportsNameID", sportName.ID);
			paramArray[1] = new SqlParameter("@RSSID", sportName.RSSFeedKey);
			paramArray[2] = new SqlParameter("@Name", sportName.Name);
			paramArray[3] = new SqlParameter("@Description", sportName.Description);
			paramArray[4] = new SqlParameter("@LogoURL", sportName.LogoUrl);
			paramArray[5] = new SqlParameter("@Sequence", sportName.Sequence);
			paramArray[6] = new SqlParameter("@Creator", sportName.Creator);
			paramArray[7] = new SqlParameter("@Update", sportName.LastUpdate);
			paramArray[8] = new SqlParameter("@Website", sportName.Website);
			paramArray[9] = new SqlParameter("@RSSNotes", sportName.RSSNotes);

			Object retObj = null;
			if (ExecuteSPValue("sp_AddSportsName", paramArray, out retObj))
			{
				if (null != retObj)
				{
					sportName.Key = Convert.ToInt64(retObj);
					if( sportName.ID == 0 )
					{
						sportName.ID = sportName.Key;
						sportName.Update();
					}
					fRet = true;
				}
			}
			
			return fRet;
		}

		public bool Update(SportName sportName)
		{
			return sportName.Update();
		}

		public bool Delete(SportName sportName)
		{
			bool fRet = false;
			return fRet;
		}

	}
}