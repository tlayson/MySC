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
	public class NewsMenuItem : USCBaseItem
	{
#region Members
		private long   m_lParentID;
		private string m_strName;
		private string m_strDescription;
		private long m_lRSSID;
		private string m_strWebsite;
		private string m_strNotes;
		private string m_strLogoUrl;
		protected float m_flSequence;
		private bool m_bActive;
		private int m_nMenuDepth;
		private string m_strTarget;
#endregion

#region Init
		private void Init()
		{
			m_lParentID = 0;
			m_strName = "";
			m_strDescription = "";
			m_lRSSID = 0;
			m_strWebsite = "";
			m_strNotes = "";
			m_strLogoUrl = "";
			m_flSequence = 0;
			m_bActive = true;
			m_nMenuDepth = 0;
			m_strTarget = "";
		}

		public NewsMenuItem()
		{
			Init();
		}
#endregion

#region Update

/*
@ParentID bigint, 
	@Name nvarchar(50),
	@Description nvarchar(50),
	@RSSID bigint,
	@Website nvarchar(500),
	@Notes nvarchar(200),
	@LogoURL nvarchar(100),
	@Sequence float,
	@Language int,
	@Active bit,
	@MenuDepth int,
	@Update nvarchar(max),
	@Key bigint
*/
		public bool Update()
		{
			bool fRet = false;

			SqlParameter[] paramArray = new SqlParameter[14];
			paramArray[0] = new SqlParameter("@ParentID", ParentID);
			paramArray[1] = new SqlParameter("@Name", Name);
			paramArray[2] = new SqlParameter("@Description", Description);
			paramArray[3] = new SqlParameter("@RSSID", RSSID);
			paramArray[4] = new SqlParameter("@Website", this.Website);
			paramArray[5] = new SqlParameter("@Notes", this.Notes);
			paramArray[6] = new SqlParameter("@LogoURL", LogoUrl);
			paramArray[7] = new SqlParameter("@Sequence", Sequence);
			paramArray[8] = new SqlParameter("@Language", Language);
			paramArray[9] = new SqlParameter("@Active", SQLBitFromBool(Active));
			paramArray[10] = new SqlParameter("@MenuDepth", MenuDepth);
			paramArray[11] = new SqlParameter("@Target", Target);
			paramArray[12] = new SqlParameter("@Update", LastUpdate);
			paramArray[13] = new SqlParameter("@Key", Key);

			if (ExecuteSPNoValue("sp_UpdateNewsMenu", paramArray))
			{
				fRet = true;
			}

			return fRet;
		}
#endregion

#region Accessors
		public string Target
		{
			get
			{
				return this.m_strTarget;
			}
			set
			{
				this.m_strTarget = value;
			}
		}

		public long ParentID
		{
			get
			{
				return this.m_lParentID;
			}
			set
			{
				this.m_lParentID = value;
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

		public long RSSID
		{
			get
			{
				return this.m_lRSSID;
			}
			set
			{
				this.m_lRSSID = value;
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

		public string LogoUrl
		{
			get
			{
				return this.m_strLogoUrl;
			}
			set
			{
				this.m_strLogoUrl = value;
			}
		}

		public float Sequence
		{
			get
			{
				return this.m_flSequence;
			}
			set
			{
				this.m_flSequence = value;
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

		public int MenuDepth
		{
			get
			{
				return this.m_nMenuDepth;
			}
			set
			{
				this.m_nMenuDepth = value;
			}
		}
#endregion

	}

	public sealed class NewsMenuList : USCBaseList
	{
#region Column Constants
		const int colKey = 0;
		const int colParentID = 1;
		const int colName = 2;
		const int colDesc = 3;
		const int colRSSID = 4;
		const int colWebsite = 5;
		const int colNotes = 6;
		const int colUrl = 7;
		const int colSequence = 8;
		const int colLanguage = 9;
		const int colActive = 10;
		const int colMenuDepth = 11;
		const int colTarget = 12;
		const int colCreator = 13;
		const int colCreateDate = 14;
		const int colLastUpdate = 15;
#endregion

		private static volatile NewsMenuList instance = null;
		private static object syncRoot = new object();

		const string strSQLGetAllNewsMenu = "SELECT * FROM NewsMenu";

		private NewsMenuList()
		{
		}

		public static NewsMenuList Instance
		{
			get
			{
				if (instance == null)
				{
					lock (syncRoot)
					{
						if (instance == null)
							instance = new NewsMenuList();
					}
				}

				return instance;
			}
		}

		public Hashtable htNewsMenu;
		public SortedDictionary<long, object> m_sdNewsMenuLvl1Items = new SortedDictionary<long, object>();
		public SortedDictionary<long, object> m_sdNewsMenuLvl2Items = new SortedDictionary<long, object>();
		public SortedDictionary<long, object> m_sdNewsMenuLvl3Items = new SortedDictionary<long, object>();
		public SortedDictionary<long, object> m_sdNewsMenuLvl4Items = new SortedDictionary<long, object>();

		public void Init(string cnxString, bool fForce)
		{
			m_strConnectionString = cnxString;
			if (null == htNewsMenu || fForce)
			{
				htNewsMenu = new Hashtable();
				Load();
			}
		}

#region Load
		public bool Load()
		{
			bool fRet = false;

			htNewsMenu.Clear();
			m_sdNewsMenuLvl1Items.Clear();
			m_sdNewsMenuLvl2Items.Clear();
			m_sdNewsMenuLvl3Items.Clear();
			m_sdNewsMenuLvl4Items.Clear();

			SqlConnection sqlConn = null;
			DataSet locStrDS = new DataSet();

			try
			{
				sqlConn = new SqlConnection(m_strConnectionString);
				sqlConn.Open();
				SqlDataAdapter daLocStrings = new SqlDataAdapter(strSQLGetAllNewsMenu, sqlConn);
				daLocStrings.Fill(locStrDS, "NewsMenu");
			}
			catch (Exception ex)
			{
				EvtLog.WriteException("NewsMenu.Load failure", ex, 0);
				return false;
			}
			finally
			{
				sqlConn.Close();
			}

			DataRowCollection dra = locStrDS.Tables["NewsMenu"].Rows;
			foreach (DataRow dr in dra)
			{
				NewsMenuItem mi = new NewsMenuItem();
				fRet = ReadNewsMenuItem(dr, mi);
				if (fRet)
				{
					htNewsMenu.Add(mi.Key, mi);
				}
			}
			fRet = true;

			// Create sorted lists
			foreach (DictionaryEntry de in htNewsMenu)
			{
				NewsMenuItem item = (NewsMenuItem)de.Value;
				long lKey = 0;
				// Subsequent level items will be sorted on display
				if( item.MenuDepth == 1 )
				{
					lKey = Convert.ToInt64(item.Sequence);
				}
				else
				{
					lKey = Convert.ToInt64(item.Key);
				}

				switch( item.MenuDepth )
				{
					case 1:
					{
						m_sdNewsMenuLvl1Items.Add(lKey, item);
						break;
					}
					case 2:
					{
						m_sdNewsMenuLvl2Items.Add(lKey, item);
						break;
					}
					case 3:
					{
						m_sdNewsMenuLvl3Items.Add(lKey, item);
						break;
					}
					case 4:
					{
						m_sdNewsMenuLvl4Items.Add(lKey, item);
						break;
					}
				}
			}

			return fRet;
		}

		private bool ReadNewsMenuItem(DataRow dr, NewsMenuItem mi)
		{
			bool fRet = true;
			try
			{
				mi.ConnectionString = m_strConnectionString;
				mi.Key = ObjectToLong(dr.ItemArray[colKey]);
				mi.ParentID = ObjectToLong(dr.ItemArray[colParentID]);
				mi.Name = ObjectToString(dr.ItemArray[colName]);
				mi.Description = ObjectToString(dr.ItemArray[colDesc]);
				mi.RSSID = ObjectToLong(dr.ItemArray[colRSSID]);
				mi.Website = ObjectToString(dr.ItemArray[colWebsite]);
				mi.Notes = ObjectToString(dr.ItemArray[colNotes]);
				mi.LogoUrl = ObjectToString(dr.ItemArray[colUrl]);
				mi.Sequence = ObjectToFloat(dr.ItemArray[colSequence]);
				mi.Language = ObjectToInt(dr.ItemArray[colLanguage]);
				mi.Active = ObjectToBool(dr.ItemArray[colActive]);
				mi.MenuDepth = ObjectToInt(dr.ItemArray[colMenuDepth]);
				mi.Target = ObjectToString(dr.ItemArray[colTarget]);

				mi.Creator = ObjectToString(dr.ItemArray[colCreator]);
				mi.CreateDate = ObjectToDateTime(dr.ItemArray[colCreateDate]);
				mi.LastUpdate = ObjectToString(dr.ItemArray[colLastUpdate]);

			}
			catch (Exception ex)
			{
				EvtLog.WriteException("ReadNewsMenuItem.Read failure", ex, 0);
				fRet = false;
			}
			return fRet;
		}
#endregion Load

		public int Count
		{
			get
			{
				return htNewsMenu.Count;
			}
		}

		public NewsMenuItem GetNewsMenuItem(long index)
		{
			return (NewsMenuItem)htNewsMenu[index];
		}

/*
	ALTER PROCEDURE [dbo].[sp_AddNewsMenu]
	@ParentID bigint, 
	@Name nvarchar(50),
	@Description nvarchar(50),
	@RSSID bigint,
	@Website nvarchar(500),
	@Notes nvarchar(200),
	@LogoURL nvarchar(100),
	@Sequence float,
	@Language int,
	@Active bit,
	@MenuDepth int,
	@Creator nvarchar(50),
	@CreationDate datetime2(7)
*/
		public bool Add(NewsMenuItem menuItem)
		{
			bool fRet = false;

			SqlParameter[] paramArray = new SqlParameter[13];
			paramArray[0] = new SqlParameter("@ParentID", menuItem.ParentID);
			paramArray[1] = new SqlParameter("@Name", menuItem.Name);
			paramArray[2] = new SqlParameter("@Description", menuItem.Description);
			paramArray[3] = new SqlParameter("@RSSID", menuItem.RSSID);
			paramArray[4] = new SqlParameter("@Website", menuItem.Website);
			paramArray[5] = new SqlParameter("@Notes", menuItem.Notes);
			paramArray[6] = new SqlParameter("@LogoURL", menuItem.LogoUrl);
			paramArray[7] = new SqlParameter("@Sequence", menuItem.Sequence);
			paramArray[8] = new SqlParameter("@Language", menuItem.Language);
			paramArray[9] = new SqlParameter("@Active", SQLBitFromBool(menuItem.Active));
			paramArray[10] = new SqlParameter("@MenuDepth", menuItem.MenuDepth);
			paramArray[11] = new SqlParameter("@Target", menuItem.Target);
			paramArray[12] = new SqlParameter("@Creator", menuItem.Creator);

			menuItem.Key = ExecuteSPInsert("sp_AddNewsMenu", paramArray);
			if (menuItem.Key > 0)
			{
				htNewsMenu.Add(menuItem.Key, menuItem);
				fRet = true;
			}

			return fRet;
		}

		public bool Update(NewsMenuItem menuItem)
		{
			return menuItem.Update();
		}

		public bool Delete(NewsMenuItem menuItem)
		{
			// We don't really want to delete it, just make inactive.
			menuItem.Active = false;
			return menuItem.Update();
		}

	}

}