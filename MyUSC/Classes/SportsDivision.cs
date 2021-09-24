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
	public class SportDivision : SportsMenuItem
	{
		private long m_lSportDivisionID;
		private long m_lSportTypeKey;
		private long m_lSportNameKey;

		private void Init()
		{
			m_lSportDivisionID = 0;
			m_lSportTypeKey = 0;
			m_lSportNameKey = 0;
		}

		public SportDivision()
		{
			Init();
		}

#region Update
		/*
 CREATE PROCEDURE sp_UpdateSportsDivision
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

			SqlParameter[] paramArray = new SqlParameter[12];
			paramArray[0] = new SqlParameter("@DivisionID", ID);
			paramArray[1] = new SqlParameter("@SportTypeID", SportTypeKey);
			paramArray[2] = new SqlParameter("@SportsNameID", SportNameKey);
			paramArray[3] = new SqlParameter("@RSSID", RSSFeedKey);
			paramArray[4] = new SqlParameter("@Name", Name);
			paramArray[5] = new SqlParameter("@Description", Description);
			paramArray[6] = new SqlParameter("@LogoURL", LogoUrl);
			paramArray[7] = new SqlParameter("@Update", LastUpdate);
			paramArray[8] = new SqlParameter("@Website", Website);
			paramArray[9] = new SqlParameter("@RSSNotes", RSSNotes);
			paramArray[10] = new SqlParameter("@Sequence", Sequence);
			paramArray[11] = new SqlParameter("@Key", Key);

			if (ExecuteSPNoValue("sp_UpdateSportsDivision", paramArray))
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
				return this.m_lSportDivisionID;
			}
			set
			{
				this.m_lSportDivisionID = value;
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

	public sealed class SportDivisionList : USCBaseList
	{
#region Column Constants
		const int colKey = 0;
		const int colSportDivisionID = 1;
		const int colSportTypeKey = 2;
		const int colSportNameKey = 3;
		const int colRSSFeedKey = 4;
		const int colName = 5;
		const int colDesc = 6;
		const int colLogoUrl = 7;
		const int colLanguage = 8;
		const int colCreator = 9;
		const int colCreateDate = 10;
		const int colLastUpdate = 11;
		const int colWebsite = 12;
		const int colRSSNotes = 13;
		const int colSequence = 14;
#endregion

		private static volatile SportDivisionList instance = null;
		private static object syncRoot = new object();

		const string strSQLGetAllSportDivisions = "SELECT * FROM SportDivision";

		private SportDivisionList()
		{
		}

		public static SportDivisionList Instance
		{
			get
			{
				if (instance == null)
				{
					lock (syncRoot)
					{
						if (instance == null)
							instance = new SportDivisionList();
					}
				}

				return instance;
			}
		}

		public Hashtable htSportDivision;
		public SortedDictionary<long, object> m_sdSportDivisions = new SortedDictionary<long, object>();

		public void Init(string cnxString, bool fForce)
		{
			m_strConnectionString = cnxString;
			if (null == htSportDivision || fForce)
			{
				htSportDivision = new Hashtable();
				Load();
			}
		}

		public bool Load()
		{
			bool fRet = false;

			htSportDivision.Clear();
			m_sdSportDivisions.Clear();

			SqlConnection sqlConn = null;
			DataSet locStrDS = new DataSet();

			try
			{
				sqlConn = new SqlConnection(m_strConnectionString);
				sqlConn.Open();
				SqlDataAdapter daLocStrings = new SqlDataAdapter(strSQLGetAllSportDivisions, sqlConn);
				daLocStrings.Fill(locStrDS, "SportDivision");
			}
			catch (Exception ex)
			{
				EvtLog.WriteException("SportDivision.Load failure", ex, 0);
				return false;
			}
			finally
			{
				sqlConn.Close();
			}

			DataRowCollection dra = locStrDS.Tables["SportDivision"].Rows;
			foreach (DataRow dr in dra)
			{
				SportDivision sd = new SportDivision();
				fRet = ReadSportDivision(dr, sd);
				if (fRet)
				{
					htSportDivision.Add(sd.Key, sd);
				}
			}
			fRet = true;

			foreach (DictionaryEntry de in htSportDivision)
			{
				SportDivision sportDivision = (SportDivision)de.Value;
				m_sdSportDivisions.Add(sportDivision.ID, sportDivision);
			}

			return fRet;
		}

		private bool ReadSportDivision(DataRow dr, SportDivision sportDivision)
		{
			bool fRet = true;
			try
			{
				sportDivision.ConnectionString = m_strConnectionString;
				sportDivision.Key = ObjectToLong(dr.ItemArray[colKey]);
				sportDivision.ID = ObjectToLong(dr.ItemArray[colSportDivisionID]);
				sportDivision.SportTypeKey = ObjectToLong(dr.ItemArray[colSportTypeKey]);
				sportDivision.SportNameKey = ObjectToLong(dr.ItemArray[colSportNameKey]);
				sportDivision.RSSFeedKey = ObjectToLong(dr.ItemArray[colRSSFeedKey]);
				sportDivision.Name = ObjectToString(dr.ItemArray[colName]);
				sportDivision.Description = ObjectToString(dr.ItemArray[colDesc]);
				sportDivision.LogoUrl = ObjectToString(dr.ItemArray[colLogoUrl]);
				sportDivision.Language = ObjectToInt(dr.ItemArray[colLanguage]);

				sportDivision.Creator = ObjectToString(dr.ItemArray[colCreator]);
				sportDivision.CreateDate = ObjectToDateTime(dr.ItemArray[colCreateDate]);
				sportDivision.LastUpdate = ObjectToString(dr.ItemArray[colLastUpdate]);

				sportDivision.Website = ObjectToString(dr.ItemArray[colWebsite]);
				sportDivision.RSSNotes = ObjectToString(dr.ItemArray[colRSSNotes]);
				sportDivision.Sequence = ObjectToFloat(dr.ItemArray[colSequence]);
			}
			catch (Exception ex)
			{
				EvtLog.WriteException("SportDivision.Read failure", ex, 0);
				fRet = false;
			}
			return fRet;
		}

		public int Count
		{
			get
			{
				return htSportDivision.Count;
			}
		}

		public SportDivision GetSportDivision(long index)
		{
			return (SportDivision)htSportDivision[index];
		}

		public SportDivision GetDivisionByID(long lID)
		{
			SportDivision division = null;

			foreach (DictionaryEntry de in htSportDivision)
			{
				SportDivision divT = (SportDivision)de.Value;
				if (divT.ID == lID)
				{
					// No need to keep looking.
					division = divT;
					break;
				}
			}

			return division;
		}

		/*
		 CREATE PROCEDURE sp_AddSportsDivision
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
		public bool Add(SportDivision sportDivision)
		{
			bool fRet = false;

			SqlParameter[] paramArray = new SqlParameter[12];
			paramArray[0] = new SqlParameter("@DivisionID", sportDivision.ID);
			paramArray[1] = new SqlParameter("@SportTypeID", sportDivision.SportTypeKey);
			paramArray[2] = new SqlParameter("@SportsNameID", sportDivision.SportNameKey);
			paramArray[3] = new SqlParameter("@RSSID", sportDivision.RSSFeedKey);
			paramArray[4] = new SqlParameter("@Name", sportDivision.Name);
			paramArray[5] = new SqlParameter("@Description", sportDivision.Description);
			paramArray[6] = new SqlParameter("@LogoURL", sportDivision.LogoUrl);
			paramArray[7] = new SqlParameter("@Creator", sportDivision.Creator);
			paramArray[8] = new SqlParameter("@Update", sportDivision.LastUpdate);
			paramArray[9] = new SqlParameter("@Website", sportDivision.Website);
			paramArray[10] = new SqlParameter("@RSSNotes", sportDivision.RSSNotes);
			paramArray[11] = new SqlParameter("@Sequence", sportDivision.Sequence);

			Object retObj = null;
			if (ExecuteSPValue("sp_AddSportsDivision", paramArray, out retObj))
			{
				if (null != retObj)
				{
					sportDivision.Key = Convert.ToInt64(retObj);
					if (sportDivision.ID == 0)
					{
						sportDivision.ID = sportDivision.Key;
						sportDivision.Update();
					}
					fRet = true;
				}
			}

			return fRet;
		}

		public bool Update(SportDivision sportDivision)
		{
			return sportDivision.Update();
		}

		public bool Delete(SportDivision zip)
		{
			bool fRet = false;
			return fRet;
		}

	}
}