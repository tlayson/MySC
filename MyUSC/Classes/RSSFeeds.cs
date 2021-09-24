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
	public class RSSFeed : USCBaseItem
	{
		private string m_strName;
		private string m_strUrl;
		private string m_strDescription;
		private string m_strWebsite;
		private string m_strNotes;
		private bool m_bUseWebsite;

		private void Init()
		{
			m_strName = "";
			m_strDescription = "";
			m_strUrl = "";
			m_strWebsite = "";
			m_strNotes = "";
			m_bUseWebsite = false;
		}

		public RSSFeed()
		{
			Init();
		}

#region Update
		/*
	@Name nvarchar(50),
	@URL nvarchar(500),
	@Description nvarchar(50),
	@Update nvarchar(max),
	@Notes nvarchar(200),
	@Website nvarchar(500),
	@UseWebsite bit,
	@Key bigint
 */
		public bool Update()
		{
			bool fRet = false;

			SqlParameter[] paramArray = new SqlParameter[8];
			paramArray[0] = new SqlParameter("@Name", Name);
			paramArray[1] = new SqlParameter("@URL", Url);
			paramArray[2] = new SqlParameter("@Description", Description);
			paramArray[3] = new SqlParameter("@Update", LastUpdate);
			paramArray[4] = new SqlParameter("@Notes", this.Notes);
			paramArray[5] = new SqlParameter("@Website", this.Website);
			paramArray[6] = new SqlParameter("@UseWebsite", SQLBitFromBool(UseWebsite));
			paramArray[7] = new SqlParameter("@Key", Key);

			if (ExecuteSPNoValue("sp_UpdateRSSFeed", paramArray))
			{
				fRet = true;
			}

			return fRet;
		}
#endregion

#region Accessors
		public bool UseWebsite
		{
			get
			{
				return this.m_bUseWebsite;
			}
			set
			{
				this.m_bUseWebsite = value;
			}
		}

		public string Website
		{
			get
			{
				return this.m_strWebsite;
			}
			set
			{
				this.m_strWebsite = value;
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

		public string Name
		{
			get
			{
				return this.m_strName;
			}
			set
			{
				this.m_strName = value;
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

		public string Url
		{
			get
			{
				return this.m_strUrl;
			}
			set
			{
				this.m_strUrl = value;
			}
		}

#endregion
	}

	public sealed class RSSFeedList : USCBaseList
	{
#region Column Constants
		const int colKey = 0;
		const int colName = 1;
		const int colUrl = 2;
		const int colDesc = 3;
		const int colCreator = 4;
		const int colCreateDate = 5;
		const int colLastUpdate = 6;
		const int colNotes = 7;
		const int colWebsite = 8;
		const int colUseWebsite = 9;
#endregion

		private static volatile RSSFeedList instance = null;
		private static object syncRoot = new object();

		const string strSQLGetAllRSSFeeds = "SELECT * FROM RSSFeeds";

		private RSSFeedList()
		{
		}

		public static RSSFeedList Instance
		{
			get
			{
				if (instance == null)
				{
					lock (syncRoot)
					{
						if (instance == null)
							instance = new RSSFeedList();
					}
				}

				return instance;
			}
		}

		public Hashtable htRSSFeed;

		public void Init(string cnxString, bool fForce)
		{
			m_strConnectionString = cnxString;
			if (null == htRSSFeed || fForce)
			{
				htRSSFeed = new Hashtable();
				Load();
			}
		}

		public bool Load()
		{
			bool fRet = false;

			htRSSFeed.Clear();

			SqlConnection sqlConn = null;
			DataSet locStrDS = new DataSet();

			try
			{
				sqlConn = new SqlConnection(m_strConnectionString);
				sqlConn.Open();
				SqlDataAdapter daLocStrings = new SqlDataAdapter(strSQLGetAllRSSFeeds, sqlConn);
				daLocStrings.Fill(locStrDS, "RSSFeeds");
			}
			catch (Exception ex)
			{
				EvtLog.WriteException("RSSFeeds.Load failure", ex, 0);
				return false;
			}
			finally
			{
				sqlConn.Close();
			}

			DataRowCollection dra = locStrDS.Tables["RSSFeeds"].Rows;
			foreach (DataRow dr in dra)
			{
				RSSFeed feed = new RSSFeed();
				fRet = ReadRSSFeed(dr, feed);
				if (fRet)
				{
					htRSSFeed.Add(feed.Key, feed);
				}
			}
			fRet = true;

			return fRet;
		}

		/*
ALTER PROCEDURE [dbo].[sp_AddRSSFeed]
	@Name nvarchar(50),
	@URL nvarchar(500),
	@Description nvarchar(50),
	@Creator nvarchar(50),
	@Update nvarchar(max),
	@Notes nvarchar(200),
	@Website nvarchar(500),
	@UseWebsite bit
		 */
		private bool ReadRSSFeed(DataRow dr, RSSFeed feed)
		{
			bool fRet = true;
			try
			{
				feed.ConnectionString = m_strConnectionString;
				feed.Key = ObjectToLong(dr.ItemArray[colKey]);
				feed.Name = ObjectToString(dr.ItemArray[colName]);
				feed.Description = ObjectToString(dr.ItemArray[colDesc]);
				feed.Url = ObjectToString(dr.ItemArray[colUrl]);
				feed.Creator = ObjectToString(dr.ItemArray[colCreator]);
				feed.CreateDate = ObjectToDateTime(dr.ItemArray[colCreateDate]);
				feed.LastUpdate = ObjectToString(dr.ItemArray[colLastUpdate]);
				feed.Notes = ObjectToString(dr.ItemArray[colNotes]);
				feed.Website = ObjectToString(dr.ItemArray[colWebsite]);
				feed.UseWebsite = ObjectToBool(dr.ItemArray[colUseWebsite]);
			}
			catch (Exception ex)
			{
				EvtLog.WriteException("RSSFeeds.Read failure", ex, 0);
				fRet = false;
			}
			return fRet;
		}

		public int Count
		{
			get
			{
				return htRSSFeed.Count;
			}
		}

		public RSSFeed GetRSSFeed(long index)
		{
			return (RSSFeed)htRSSFeed[index];
		}

		/*
ALTER PROCEDURE [dbo].[sp_AddRSSFeed]
	@Name nvarchar(50),
	@URL nvarchar(500),
	@Description nvarchar(50),
	@Creator nvarchar(50),
	@Update nvarchar(max),
	@Notes nvarchar(200),
	@Website nvarchar(500),
	@UseWebsite bit
		 */
		public bool Add(RSSFeed rssFeed)
		{
			bool fRet = false;

			SqlParameter[] paramArray = new SqlParameter[8];
			paramArray[0] = new SqlParameter("@Name", rssFeed.Name);
			paramArray[1] = new SqlParameter("@URL", rssFeed.Url);
			paramArray[2] = new SqlParameter("@Description", rssFeed.Description);
			paramArray[3] = new SqlParameter("@Creator", rssFeed.Creator);
			paramArray[4] = new SqlParameter("@Update", rssFeed.LastUpdate);
			paramArray[5] = new SqlParameter("@Notes", rssFeed.Notes);
			paramArray[6] = new SqlParameter("@Website", rssFeed.Website);
			paramArray[7] = new SqlParameter("@UseWebsite", SQLBitFromBool(rssFeed.UseWebsite));

			rssFeed.Key = ExecuteSPInsert( "sp_AddRSSFeed", paramArray );
			if (rssFeed.Key > 0)
			{
				htRSSFeed.Add( rssFeed.Key, rssFeed );
				fRet = true;
			}

			return fRet;
		}

		public bool Update(RSSFeed rssFeed)
		{
			return rssFeed.Update();
		}

		public bool Delete(RSSFeed zip)
		{
			bool fRet = false;
			return fRet;
		}

	}
}