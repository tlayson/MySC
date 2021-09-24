using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MyUSC.Classes
{
	public class SportType : SportsMenuItem
	{
		private long m_lSportTypeID;
		private long m_lSportNameKey;

		private void Init()
		{
			m_lSportTypeID = 0;
			m_lSportNameKey = 0;
		}

		public SportType()
		{
			Init();
		}

#region Update
/*
CREATE PROCEDURE sp_UpdateSportsType 
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

			SqlParameter[] paramArray = new SqlParameter[11];
			paramArray[0] = new SqlParameter("@SportTypeID", ID);
			paramArray[1] = new SqlParameter("@SportsNameID", SportNameKey);
			paramArray[2] = new SqlParameter("@RSSID", RSSFeedKey);
			paramArray[3] = new SqlParameter("@Name", Name);
			paramArray[4] = new SqlParameter("@Description", Description);
			paramArray[5] = new SqlParameter("@LogoURL", LogoUrl);
			paramArray[6] = new SqlParameter("@Update", LastUpdate);
			paramArray[7] = new SqlParameter("@Website", Website);
			paramArray[8] = new SqlParameter("@RSSNotes", RSSNotes);
			paramArray[9] = new SqlParameter("@Sequence", Sequence);
			paramArray[10] = new SqlParameter("@Key", Key);

			if (ExecuteSPNoValue("sp_UpdateSportsType", paramArray))
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
				return this.m_lSportTypeID;
			}
			set
			{
				this.m_lSportTypeID = value;
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

#endregion
	}

	public sealed class SportTypeList : USCBaseList
	{
#region Column Constants
		const int colKey = 0;
		const int colSportTypeID = 1;
		const int colSportNameKey = 2;
		const int colRSSFeedKey = 3;
		const int colName = 4;
		const int colDesc = 5;
		const int colLogoUrl = 6;
		const int colLanguage = 7;
		const int colCreator = 8;
		const int colCreateDate = 9;
		const int colLastUpdate = 10;
		const int colWebsite = 11;
		const int colRSSNotes = 12;
		const int colSequence = 13;
#endregion

		private static volatile SportTypeList instance = null;
		private static object syncRoot = new object();

		const string strSQLGetAllSportTypes = "SELECT * FROM SportType";

		private SportTypeList()
		{
		}

		public static SportTypeList Instance
		{
			get
			{
				if (instance == null)
				{
					lock (syncRoot)
					{
						if (instance == null)
							instance = new SportTypeList();
					}
				}

				return instance;
			}
		}

		public Hashtable htSportType;
		public SortedDictionary<long, object> m_sdSportTypes = new SortedDictionary<long, object>();

		public void Init(string cnxString, bool fForce)
		{
			m_strConnectionString = cnxString;
			if (null == htSportType || fForce)
			{
				htSportType = new Hashtable();
				Load();
			}
		}

		public bool Load()
		{
			bool fRet = false;

			htSportType.Clear();
			m_sdSportTypes.Clear();

			SqlConnection sqlConn = null;
			DataSet locStrDS = new DataSet();

			try
			{
				sqlConn = new SqlConnection(m_strConnectionString);
				sqlConn.Open();
				SqlDataAdapter daLocStrings = new SqlDataAdapter(strSQLGetAllSportTypes, sqlConn);
				daLocStrings.Fill(locStrDS, "SportType");
			}
			catch (Exception ex)
			{
				EvtLog.WriteException("SportType.Load failure", ex, 0);
				return false;
			}
			finally
			{
				sqlConn.Close();
			}

			DataRowCollection dra = locStrDS.Tables["SportType"].Rows;
			foreach (DataRow dr in dra)
			{
				SportType st = new SportType();
				fRet = ReadSportType(dr, st);
				if (fRet)
				{
					htSportType.Add(st.Key, st);
				}
			}
			fRet = true;

			// Create sorted list
			foreach (DictionaryEntry de in htSportType)
			{
				SportType sportType = (SportType)de.Value;
				m_sdSportTypes.Add(sportType.ID, sportType);
			}

			return fRet;
		}

		private bool ReadSportType(DataRow dr, SportType st)
		{
			bool fRet = true;
			try
			{
				st.ConnectionString = m_strConnectionString;
				st.Key = ObjectToLong(dr.ItemArray[colKey]);
				st.ID = ObjectToLong(dr.ItemArray[colSportTypeID]);
				st.SportNameKey = ObjectToLong(dr.ItemArray[colSportNameKey]);
				st.RSSFeedKey = ObjectToLong(dr.ItemArray[colRSSFeedKey]);
				st.Name = ObjectToString(dr.ItemArray[colName]);
				st.Description = ObjectToString(dr.ItemArray[colDesc]);
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
				EvtLog.WriteException("SportType.Read failure", ex, 0);
				fRet = false;
			}
			return fRet;
		}

		public int Count
		{
			get
			{
				return htSportType.Count;
			}
		}

		public SportType GetSportType(long index)
		{
			return (SportType)htSportType[index];
		}

		public SportType GetSportTypeByID(long lID)
		{
			SportType sportType = null;

			foreach (DictionaryEntry de in htSportType)
			{
				SportType sportTypeT = (SportType)de.Value;
				if (sportTypeT.ID == lID)
				{
					// No need to keep looking.
					sportType = sportTypeT;
					break;
				}
			}

			return sportType;
		}

		/*
		CREATE PROCEDURE sp_AddSportsType
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
		public bool Add(SportType sportType)
		{
			bool fRet = false;

			SqlParameter[] paramArray = new SqlParameter[11];
			paramArray[0] = new SqlParameter("@SportTypeID", sportType.ID);
			paramArray[1] = new SqlParameter("@SportsNameID", sportType.SportNameKey);
			paramArray[2] = new SqlParameter("@RSSID", sportType.RSSFeedKey);
			paramArray[3] = new SqlParameter("@Name", sportType.Name);
			paramArray[4] = new SqlParameter("@Description", sportType.Description);
			paramArray[5] = new SqlParameter("@LogoURL", sportType.LogoUrl);
			paramArray[6] = new SqlParameter("@Creator", sportType.Creator);
			paramArray[7] = new SqlParameter("@Update", sportType.LastUpdate);
			paramArray[8] = new SqlParameter("@Website", sportType.Website);
			paramArray[9] = new SqlParameter("@RSSNotes", sportType.RSSNotes);
			paramArray[10] = new SqlParameter("@Sequence", sportType.Sequence);

			Object retObj = null;
			if (ExecuteSPValue("sp_AddSportsType", paramArray, out retObj))
			{
				if (null != retObj)
				{
					sportType.Key = Convert.ToInt64(retObj);
					if( sportType.ID == 0 )
					{
						sportType.ID = sportType.Key;
						sportType.Update();
					}
					fRet = true;
				}
			}
			
			return fRet;
		}

		public bool Update(SportType sportType)
		{
			return sportType.Update();
		}

		public bool Delete(SportType zip)
		{
			bool fRet = false;
			return fRet;
		}

	}
}