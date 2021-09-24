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
	public class SportTeam : SportsMenuItem
	{
		private long m_lSportTeamID;
		private long m_lSportNameKey;
		private long m_lSportTypeKey;
		private long m_lSportDivisionKey;
		private string m_strSortName;

		private void Init()
		{
			m_lSportTeamID = 0;
			m_lSportNameKey = 0;
			m_lSportTypeKey = 0;
			m_lSportDivisionKey = 0;
			m_strSortName = "";
		}

		public SportTeam()
		{
			Init();
		}

#region Update
		/*
 CREATE PROCEDURE sp_UpdateSportsTeam
	@SportTeamID bigint,
	@DivisionID bigint,
	@SportTypeID bigint,
	@SportsNameID bigint,
	@RSSID bigint,
	@Name nvarchar(50),
	@Description nvarchar(50),
	@LogoURL nvarchar(500),
	@Update nvarchar(max),
	@Website nvarchar(500),
	@RSSNotes nvarchar(200),
	@Sequence float,
	@Key bigint
		 */

		public bool Update()
		{
			bool fRet = false;

			SqlParameter[] paramArray = new SqlParameter[13];
			paramArray[0] = new SqlParameter("@SportTeamID", ID);
			paramArray[1] = new SqlParameter("@DivisionID", SportDivisionKey);
			paramArray[2] = new SqlParameter("@SportTypeID", SportTypeKey);
			paramArray[3] = new SqlParameter("@SportsNameID", SportNameKey);
			paramArray[4] = new SqlParameter("@RSSID", RSSFeedKey);
			paramArray[5] = new SqlParameter("@Name", Name);
			paramArray[6] = new SqlParameter("@Description", Description);
			paramArray[7] = new SqlParameter("@LogoURL", LogoUrl);
			paramArray[8] = new SqlParameter("@Update", LastUpdate);
			paramArray[9] = new SqlParameter("@Website", Website);
			paramArray[10] = new SqlParameter("@RSSNotes", RSSNotes);
			paramArray[11] = new SqlParameter("@Sequence", Sequence);
			paramArray[12] = new SqlParameter("@Key", Key);

			if (ExecuteSPNoValue("sp_UpdateSportsTeam", paramArray))
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
				return this.m_lSportTeamID;
			}
			set
			{
				this.m_lSportTeamID = value;
			}
		}

		public long SportNameKey
		{
			get
			{
				return this.m_lSportNameKey;
			}
			set
			{
				this.m_lSportNameKey = value;
			}
		}

		public long SportTypeKey
		{
			get
			{
				return this.m_lSportTypeKey;
			}
			set
			{
				this.m_lSportTypeKey = value;
			}
		}

		public long SportDivisionKey
		{
			get
			{
				return this.m_lSportDivisionKey;
			}
			set
			{
				this.m_lSportDivisionKey = value;
			}
		}

		public string SortName
		{
			get
			{
				return this.m_strSortName;
			}
			set
			{
				this.m_strSortName = value;
			}
		}
#endregion
	}

	public sealed class SportTeamList : USCBaseList
	{
#region Column Constants
		const int colKey = 0;
		const int colSportTeamID = 1;
		const int colSportNameKey = 2;
		const int colSportTypeKey = 3;
		const int colSportDivisionKey = 4;
		const int colRSSFeedKey = 5;
		const int colDesc = 6;
		const int colName = 7;
		const int colSortName = 8;
		const int colLogoUrl = 9;
		const int colLanguage = 10;
		const int colCreator = 11;
		const int colCreateDate = 12;
		const int colLastUpdate = 13;
		const int colWebsite = 14;
		const int colRSSNotes = 15;
		const int colSequence = 16;
#endregion

		private static volatile SportTeamList instance = null;
		private static object syncRoot = new object();

		const string strSQLGetAllSportTeams = "SELECT * FROM SportTeam";

		private SportTeamList()
		{
		}

		public static SportTeamList Instance
		{
			get
			{
				if (instance == null)
				{
					lock (syncRoot)
					{
						if (instance == null)
							instance = new SportTeamList();
					}
				}

				return instance;
			}
		}

		public Hashtable htSportTeam;
		public SortedDictionary<long, object> m_sdSportTeams = new SortedDictionary<long, object>();

		public void Init(string cnxString, bool fForce)
		{
			m_strConnectionString = cnxString;
			if (null == htSportTeam || fForce)
			{
				htSportTeam = new Hashtable();
				Load();
			}
		}

		public bool Load()
		{
			bool fRet = false;

			htSportTeam.Clear();
			m_sdSportTeams.Clear();

			SqlConnection sqlConn = null;
			DataSet locStrDS = new DataSet();

			try
			{
				sqlConn = new SqlConnection(m_strConnectionString);
				sqlConn.Open();
				SqlDataAdapter daLocStrings = new SqlDataAdapter(strSQLGetAllSportTeams, sqlConn);
				daLocStrings.Fill(locStrDS, "SportTeam");
			}
			catch (Exception ex)
			{
				EvtLog.WriteException("SportTeam.Load failure", ex, 0);
				return false;
			}
			finally
			{
				sqlConn.Close();
			}

			DataRowCollection dra = locStrDS.Tables["SportTeam"].Rows;
			foreach (DataRow dr in dra)
			{
				SportTeam st = new SportTeam();
				fRet = ReadSportTeam(dr, st);
				if (fRet)
				{
					htSportTeam.Add(st.Key, st);
				}
			}
			fRet = true;

			foreach (DictionaryEntry de in htSportTeam)
			{
				SportTeam sportTeam = (SportTeam)de.Value;
				m_sdSportTeams.Add(sportTeam.ID, sportTeam);
			}

			return fRet;
		}

		private bool ReadSportTeam(DataRow dr, SportTeam st)
		{
			bool fRet = true;
			try
			{
				st.ConnectionString = m_strConnectionString;
				st.Key = ObjectToLong(dr.ItemArray[colKey]);
				st.ID = ObjectToLong(dr.ItemArray[colSportTeamID]);
				st.SportNameKey = ObjectToLong(dr.ItemArray[colSportNameKey]);
				st.SportTypeKey = ObjectToLong(dr.ItemArray[colSportTypeKey]);
				st.SportDivisionKey = ObjectToLong(dr.ItemArray[colSportDivisionKey]);
				st.RSSFeedKey = ObjectToLong(dr.ItemArray[colRSSFeedKey]);
				st.Description = ObjectToString(dr.ItemArray[colDesc]);
				st.Name = ObjectToString(dr.ItemArray[colName]);
				st.SortName = ObjectToString(dr.ItemArray[colSortName]);
				st.LogoUrl = ObjectToString(dr.ItemArray[colLogoUrl]);
				st.Language = ObjectToInt(dr.ItemArray[colLanguage]);

				st.Creator = ObjectToString(dr.ItemArray[colCreator]);
				st.CreateDate = ObjectToDateTime(dr.ItemArray[colCreateDate]);
				st.LastUpdate = ObjectToString(dr.ItemArray[colLastUpdate]);

				st.Website = ObjectToString(dr.ItemArray[colWebsite]);
				st.RSSNotes = ObjectToString(dr.ItemArray[colRSSNotes]);
				st.Sequence = ObjectToFloat(dr.ItemArray[colSequence]);
			}
			catch (Exception ex)
			{
				EvtLog.WriteException("SportTeam.Read failure", ex, 0);
				fRet = false;
			}
			return fRet;
		}

		public int Count
		{
			get
			{
				return htSportTeam.Count;
			}
		}

		public SportTeam GetTeamByID(long lID)
		{
			SportTeam team = null;

			foreach (DictionaryEntry de in htSportTeam)
			{
				SportTeam teamT = (SportTeam)de.Value;
				if (teamT.ID == lID)
				{
					// No need to keep looking.
					team = teamT;
					break;
				}
			}

			return team;
		}

		public SportTeam GetSportTeam(long index)
		{
			return (SportTeam)htSportTeam[index];
		}

		/*
		CREATE PROCEDURE sp_AddSportsTeam
			@SportTeamID bigint,
			@DivisionID bigint,
			@SportTypeID bigint,
			@SportsNameID bigint,
			@RSSID bigint,
			@Name nvarchar(50),
			@Description nvarchar(50),
			@LogoURL nvarchar(500),
			@Creator nvarchar(50),
			@Update nvarchar(max),
			@Website nvarchar(500),
			@RSSNotes nvarchar(200),
			@Sequence float 
		 */
		public bool Add(SportTeam sportTeam)
		{
			bool fRet = false;

			SqlParameter[] paramArray = new SqlParameter[13];
			paramArray[0] = new SqlParameter("@SportTeamID", sportTeam.ID);
			paramArray[1] = new SqlParameter("@DivisionID", sportTeam.SportDivisionKey);
			paramArray[2] = new SqlParameter("@SportTypeID", sportTeam.SportTypeKey);
			paramArray[3] = new SqlParameter("@SportsNameID", sportTeam.SportNameKey);
			paramArray[4] = new SqlParameter("@RSSID", sportTeam.RSSFeedKey);
			paramArray[5] = new SqlParameter("@Name", sportTeam.Name);
			paramArray[6] = new SqlParameter("@Description", sportTeam.Description);
			paramArray[7] = new SqlParameter("@LogoURL", sportTeam.LogoUrl);
			paramArray[8] = new SqlParameter("@Creator", sportTeam.Creator);
			paramArray[9] = new SqlParameter("@Update", sportTeam.LastUpdate);
			paramArray[10] = new SqlParameter("@Website", sportTeam.Website);
			paramArray[11] = new SqlParameter("@RSSNotes", sportTeam.RSSNotes);
			paramArray[12] = new SqlParameter("@Sequence", sportTeam.Sequence);

			Object retObj = null;
			if (ExecuteSPValue("sp_AddSportsTeam", paramArray, out retObj))
			{
				if (null != retObj)
				{
					sportTeam.Key = Convert.ToInt64(retObj);
					if (sportTeam.ID == 0)
					{
						sportTeam.ID = sportTeam.Key;
						sportTeam.Update();
					}
					fRet = true;
				}
			}

			return fRet;
		}

		public bool Update(SportTeam sportTeam)
		{
			return sportTeam.Update();
		}

		public bool Delete(SportTeam zip)
		{
			bool fRet = false;
			return fRet;
		}

	}
}