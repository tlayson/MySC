using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MyUSC.Classes
{
	public class SiteSetting : USCBaseItem
	{
		private string m_KeyName;
		private string m_Value;

		private void Init()
		{
			m_KeyName = "";
			m_Value = "";
		}

		public SiteSetting()
		{
			Init();
		}

		#region Accessors

		public string KeyName
		{
			get
			{
				return this.m_KeyName;
			}
			set
			{
				this.m_KeyName = value;
			}
		}

		public string Value
		{
			get
			{
				return this.m_Value;
			}
			set
			{
				this.m_Value = value;
			}
		}

		#endregion
	}

	public class SiteAdmin : USCBaseList
	{
		private static volatile SiteAdmin instance = null;
		private static object syncRoot = new object();
		private string m_strCnxString;

#region Public keys
		public const long saKeyPreLogin = 1;
		public const long saKeyLanguages = 2;
		public const long saKeyImagePath = 3;
		public const long saKeyFriendsLimit = 5;
		public const long saKeyFeedsLimit = 5;
		public const long saKeyUserImageUrl = 6;
		public const long saKeyValidImgTypes = 7;
		public const long saKeyDisplayPhotoFolder = 8;
		public const long saKeyMaxUserPhotoSize = 9;
		public const long saKeyStates = 10;
		public const long saKeyMaxLoginAttempts = 11;
		public const long saKeyRequireStrongPswd = 12;
		public const long saKeyUserMediaFolder = 13;
		public const long saKeyMaxFindFriends = 14;
		public const long saKeyDaysOfBlogs = 15;
		public const long saKeyAboutMsg = 16;
		public const long saKeyBetaKey = 17;
		public const long saKeyMissionStatement = 18;
		public const long saKeyNumBlogsToDisplay = 19;
		public const long saKeyNumBlogsFromFriends = 20;
		public const long saKeyEmailSrvr = 21;
		public const long saKeySMTPPort = 22;
		public const long saKeyEmailSender = 23;
		public const long saKeyEmailCredsPswd = 24;
		public const long saKeyEmailFrom = 25;
		public const long saKeyEmailSSL = 26;
		public const long saKeyWebUrl = 27;
		public const long saKeyEmailDisplayName = 28;
		public const long saKeyLostPassword = 29;
		public const long saKeyNewMsgClr = 30;
		public const long saKeyAdvertiseEmailList = 31;
		public const long saKeySiteVersion = 32;
		public const long saKeyExpirationDays = 33;
		public const long saKeyOrgFolders = 34;
		public const long saKeyOrgLogo = 35;
		public const long saKeyOrgMedia = 36;
		public const long saKeyInfoEmail = 37;
		public const long saKeyInfoPswd = 38;
		public const long saKeySupportEmail = 39;
		public const long saKeySupportPswd = 40;
		public const long saKeySMTPAddress = 41;
		public const long saKeyEmailPort = 42;
		public const long saKeyEmailEnableSSL = 43;
		public const long saKeySiteColor = 44;
		public const long saKeyHomeAnnouncement = 45;
		public const long saKeyHomeNewsURL = 46;
#endregion

#region Column Constants
		const int colKey = 0;
		const int colLanguage = 1;
		const int colKeyName = 2;
		const int colValue = 3;
		const int colCreator = 4;
		const int colCreateDate = 5;
		const int colLastUpdate = 6;
#endregion

		const string strSQLGetAllStrings = "SELECT * FROM SiteAdministration";

		private SiteAdmin()
		{
		}

		public static SiteAdmin Instance
		{
			get
			{
				if (instance == null)
				{
					lock (syncRoot)
					{
						if (instance == null)
							instance = new SiteAdmin();
					}
				}

				return instance;
			}
		}

		public Hashtable htSiteAdministrationStrings;

		public void Init(string cnxString, bool fForce)
		{
			m_strCnxString = cnxString;
			if (null == htSiteAdministrationStrings || fForce)
			{
				htSiteAdministrationStrings = new Hashtable();
				Load();
			}
		}

		public string CnxString
		{
			get
			{
				return this.m_strCnxString;
			}
			set
			{
				this.m_strCnxString = value;
			}
		}

		public bool Load()
		{
			bool fRet = false;

			htSiteAdministrationStrings.Clear();

			SqlConnection sqlConn = null;
			DataSet locStrDS = new DataSet();

			try
			{
				sqlConn = new SqlConnection(m_strCnxString);
				sqlConn.Open();
				SqlDataAdapter daSiteSettings = new SqlDataAdapter(strSQLGetAllStrings, sqlConn);
				daSiteSettings.Fill(locStrDS, "SiteAdministration");
			}
			catch (Exception ex)
			{
				EvtLog.WriteException("SiteAdministration.Load failure", ex, 0);
				return false;
			}
			finally
			{
				sqlConn.Close();
			}

			DataRowCollection dra = locStrDS.Tables["SiteAdministration"].Rows;
			foreach (DataRow dr in dra)
			{
				SiteSetting ls = new SiteSetting();
				fRet = ReadSiteSetting(dr, ls);
				if (fRet)
				{
					htSiteAdministrationStrings.Add(ls.Key, ls);
				}
			}
			fRet = true;

			return fRet;
		}

		private bool ReadSiteSetting(DataRow dr, SiteSetting siteSetting)
		{
			bool fRet = true;
			try
			{
				siteSetting.Key = (long)dr.ItemArray[colKey];
				siteSetting.Language = (int)dr.ItemArray[colLanguage];
				siteSetting.KeyName = dr.ItemArray[colKeyName].ToString();
				siteSetting.Value = dr.ItemArray[colValue].ToString();
				siteSetting.Creator = dr.ItemArray[colCreator].ToString();
				siteSetting.LastUpdate = dr.ItemArray[colLastUpdate].ToString();
			}
			catch (Exception ex)
			{
				EvtLog.WriteException("SiteAdministration.Read failure", ex, 0);
				fRet = false;
			}
			return fRet;
		}

		public int Count
		{
			get
			{
				return htSiteAdministrationStrings.Count;
			}
		}

		public string GetValue(long index)
		{
			string strRet = "";
			SiteSetting ls = (SiteSetting)htSiteAdministrationStrings[index];
			if (null != ls)
			{
				strRet = ls.Value;
			}

			return strRet;
		}

		public string GetValue(int lang, string keyName)
		{
			string strRet = "";
			foreach (DictionaryEntry de in htSiteAdministrationStrings)
			{
				SiteSetting ss = (SiteSetting)de.Value;
				if (ss.Language == lang && ss.KeyName == keyName)
				{
					strRet = ss.Value;
					break;
				}
			}
			return strRet;
		}
	}
}