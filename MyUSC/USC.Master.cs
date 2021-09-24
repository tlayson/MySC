using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Configuration;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Web.SessionState;
using MyUSC.Classes;
using MyUSC.Classes.MyOrg;

namespace MyUSC
{
	public enum SelectedPage
	{ 
		Home = 0,
		Teams,
		News,
		Forums,
		Friends,
		Profile,
		Help,
		About,
		Admin
	};

    public partial class USCMaster : System.Web.UI.MasterPage
    {
#region SetVariables
        private const string _PAGENAME = "Site.Master";
		public string g_strConnectionString;
		public string g_strForumConnectionString;
		public LocStrings g_locStrings;
		public SiteAdmin g_SiteAdmin;
		public AccountsList g_AccountsList;
		public MessagesList g_MsgList;
		public FriendsList g_FriendsList;
		public FriendRequestList g_FriendRequests;
		public RSSFeedList g_RSSFeedList = null;
		public NewsMenuList g_NewsMenuList = null;
		public OrganizationList g_OrgList = null;
		public VenueList g_VenueList = null;

		public CookieHelper g_cookieHelper = null;
#endregion

#region Localization constants
		//English localization string indexes
		const int langENUS = 1;
		const long enusIdxMasterAccountFinish = 1001;
		const long enusIdxMasterHPLAdvertise = 1006;
		const long enusIdxMasterLbn0 = 1079;
		const long enusIdxMasterLbn1 = 1080;
		const long enusIdxMasterMenuLimit = 1190;
		const long enusIdxMasterUnderConst = 1192;
		const long enusIdxMasterTTAdvertise = 1247;
		const long enusIdxMasterTTBtnLogout = 1248;
		const long enusIdxMasterLblMyUSC = 1249;
		const long enusIdxMasterLblCorporate = 1252;
		const long enusIdxMasterUnderCtx = 1257;
		const long enusIdxMasterLbl0 = 1;
		const long enusIdxMasterMsg0 = 126;
		const long enusIdxMasterMsgSportsNews = 129;
		const long enusIdxMasterMsgSportsNewsPage = 240;
		const long enusIdxMasterMsgFriendsPage = 328;
		const long enusIdxMasterBtnLogout = 415;
		const long enusIdxMasterLbl2 = 416;
		const long enusIdxMasterMsgLoginAlt = 417;
		const long enusIdxMasterLbl1 = 419;
		const long enusIdxMasterTTDdlStates = 420;

		const long enusIdxFriendsMsg34 = 1000;
		const long enusIdxFriendsMsg19 = 319;
		
		public string GetLocalizedString( long index, string page, string objName )
		{
			string strRet = "";

			if( null == g_locStrings )
			{
				InitVariables();
			}

			//Determine the language
			Int32 _LANGUAGE = GetCurrentLanguage();

			// If this is English and we know the index, use the faster method.
			if (langENUS == _LANGUAGE && index > 0 )
			{
				strRet = g_locStrings.GetValue(index);
			}
			else
			{
				strRet = g_locStrings.GetValue(page, _LANGUAGE, objName);
			}
			return strRet;
		}

		public string GetSiteSetting(long index, string keyName)
		{
			string strRet = "";

			if (null == g_SiteAdmin)
			{
				InitVariables();
			}

			//Determine the language
			Int32 _LANGUAGE = GetCurrentLanguage();

			// If this is English and we know the index, use the faster method.
			if (langENUS == _LANGUAGE && index > 0)
			{
				strRet = g_SiteAdmin.GetValue(index);
			}
			else
			{
				strRet = g_SiteAdmin.GetValue(_LANGUAGE, keyName);
			}
			return strRet;
		}
#endregion

#region Init
		protected USCMaster()
		{
			InitVariables();
		}

		protected void InitVariables()
		{
			// Get the Web application configuration object.
			Configuration config = WebConfigurationManager.OpenWebConfiguration("/MyUSC");

			// Get the conectionStrings section.
			ConnectionStringsSection csSection = config.ConnectionStrings;
			// Get the actual string
			g_strConnectionString = csSection.ConnectionStrings[0].ConnectionString;
			g_strForumConnectionString = csSection.ConnectionStrings[2].ConnectionString;

			g_cookieHelper = new CookieHelper( this );

			//Global localization strings
			g_SiteAdmin = SiteAdmin.Instance;
			g_SiteAdmin.Init(g_strConnectionString, false);
			g_locStrings = LocStrings.Instance;
			g_locStrings.Init(g_strConnectionString, false);
			g_AccountsList = AccountsList.Instance;
			g_AccountsList.Init(g_strConnectionString, g_strForumConnectionString, false);

			g_MsgList = MessagesList.Instance;
			g_MsgList.Init(g_strConnectionString, false);
			g_FriendsList = FriendsList.Instance;
			g_FriendsList.Init(g_strConnectionString, false);
			g_FriendRequests = FriendRequestList.Instance;
			g_FriendRequests.Init(g_strConnectionString, false);

			g_RSSFeedList = RSSFeedList.Instance;
			g_RSSFeedList.Init(g_strConnectionString, false);
			g_NewsMenuList = NewsMenuList.Instance;
			g_NewsMenuList.Init(g_strConnectionString, false);

			g_OrgList = OrganizationList.Instance;
			g_OrgList.Init(g_strConnectionString, false);

			g_VenueList = VenueList.Instance;
			g_VenueList.Init(g_strConnectionString, false);
		}

		// Used to determine if this is the production instance or the development instance on the same server.
		// This should never be an issue on dev only machines.
		public bool IsLocalInstance()
		{
			bool fRet = false;
			if (null != g_strConnectionString)
			{
				if (g_strConnectionString.Contains("MyUSCLocal"))
				{
					fRet = true;
				}
			}

			return fRet;
		}

		public Organization GetOrgByID(long orgID)
		{
			return (Organization)g_OrgList.htOrgList[orgID]; ;
		}

#endregion

#region Reset

		public void ResetAll()
		{
			UserAccount acct = GetActiveUser();
			if (UserAccount.UserTypes.SuperUser == acct.UserType)
			{
				ResetUsers();
				ResetSports();
				ResetSiteSettings();
				ResetLocalization();
				ResetRSS();
			}
			else
			{
				string strErr = "An unauthorized user attempted to reset all caches - " + acct.UserName;
				EvtLog.WriteEvent(strErr, EventLogEntryType.Error, 69, 0);
			}
		}

		public void ResetUsers()
		{
			UserAccount acct = GetActiveUser();
			if (UserAccount.UserTypes.SuperUser == acct.UserType)
			{
				g_AccountsList.Init(g_strConnectionString, g_strForumConnectionString, false);
				g_MsgList.Init(g_strConnectionString, false);
				g_FriendsList.Init(g_strConnectionString, false);
				g_FriendRequests.Init(g_strConnectionString, false);
			}
			else
			{
				string strErr = "An unauthorized user attempted to reset the users cache - " + acct.UserName;
				EvtLog.WriteEvent(strErr, EventLogEntryType.Error, 69, 0);
			}
		}

		public void ResetSports()
		{
			UserAccount acct = GetActiveUser();
			if (UserAccount.UserTypes.SuperUser == acct.UserType)
			{
				//g_SportsNameList.Init(g_strConnectionString, true);
				//g_SportsTypeList.Init(g_strConnectionString, true);
				//g_SportsDivList.Init(g_strConnectionString, true);
				//g_SportsTeamList.Init(g_strConnectionString, true);
				g_NewsMenuList.Init(g_strConnectionString, true);
			}
			else
			{
				string strErr = "An unauthorized user attempted to reset the sports cache - " + acct.UserName;
				EvtLog.WriteEvent(strErr, EventLogEntryType.Error, 69, 0);
			}
		}

		public void ResetSiteSettings()
		{
			UserAccount acct = GetActiveUser();
			if( UserAccount.UserTypes.SuperUser == acct.UserType )
			{
				g_SiteAdmin.Init(g_strConnectionString, true);
			}
			else
			{
				string strErr = "An unauthorized user attempted to reset the site settings cache - " + acct.UserName;
				EvtLog.WriteEvent(strErr, EventLogEntryType.Error, 69, 0);
			}
		}

		public void ResetLocalization()
		{
			UserAccount acct = GetActiveUser();
			if (UserAccount.UserTypes.SuperUser == acct.UserType)
			{
				g_locStrings.Init(g_strConnectionString, true);
			}
			else
			{
				string strErr = "An unauthorized user attempted to reset the localization cache - " + acct.UserName;
				EvtLog.WriteEvent(strErr, EventLogEntryType.Error, 69, 0);
			}
		}

		public void ResetRSS()
		{
			UserAccount acct = GetActiveUser();
			if (UserAccount.UserTypes.SuperUser == acct.UserType)
			{
				g_RSSFeedList.Init(g_strConnectionString, true);
			}
			else
			{
				string strErr = "An unauthorized user attempted to reset the RSS cache - " + acct.UserName;
				EvtLog.WriteEvent(strErr, EventLogEntryType.Error, 69, 0);
			}
		}
#endregion

		public void SetHidden1(string strIn)
		{
			HiddenField1.Value = strIn;
		}

		public void SetHidden2(string strIn)
		{
			HiddenField2.Value = strIn;
		}

#region PageLoad
		protected override void OnPreRender(EventArgs e)
		{
			// This method is here to allow the MyTeams menus to set state before page rendering.
			base.OnPreRender(e);
		}

		protected void RegisterMetrics()
		{
			string strIP = Request.UserHostAddress;
			string strURL = Request.RawUrl;

			try
			{
				int nrows = 0;
				SqlParameter[] paramArray = new SqlParameter[2];
				paramArray[0] = new SqlParameter( "@URL", SQLHelper.Sanitize( strIP ) );
				paramArray[1] = new SqlParameter( "@IP", SQLHelper.Sanitize( USCBase.Truncate( strURL, 49, false) ) );

				SQLHelper.ExecuteSPNoValue("sp_AddPageView", g_strConnectionString, paramArray, out nrows);
			}
			catch (Exception ex)
			{
				string strErr = "RegisterMetrics failure";
				short sCat = 0;
				EvtLog.WriteException(strErr, ex, EventErrors.ErrorType.AccountAdd, sCat);
			}
		}

		public void HideAds()
		{
			pnlAdsRight.Visible = false;
			btnLogOff.Visible = false;
			pnlAdsTopCenter.Visible = false;
			pnlAdsTopCenter.Width = 0;
			pnlAdsTopRight.Visible = false;
			pnlAdsTopRight.Width = 0;
		}
		
		protected void Page_Load(object sender, EventArgs e)
        {
			// Initialize variables
			InitVariables();

            Response.AppendHeader("P3P", "CP=\"CAO PSA OUR\"");

			RegisterMetrics();

            if (!IsPostBack)
            {
            }
            else //is postback
            {
                //Scroller scrollerControl = (Scroller)this.FindControl("Scroller1"); 
                //scrollerControl.ChangeHeadline();

            }//end not is post back

			EnableAdminMenu();
			EnableUserMenus();

			// Get the Web application configuration object.
			Configuration config = WebConfigurationManager.OpenWebConfiguration("/MyUSC");
			AppSettingsSection appSettings = config.AppSettings;
			string strSiteEnabled = appSettings.Settings["SiteEnabled"].Value.ToString();
			if ("0" == strSiteEnabled)
			{
				Response.Redirect("SiteDown.aspx");
			}
        }

		public void SetDefaultButton()
		{
		}
#endregion

#region OldAds
		protected void AdRotator1_AdCreated(object sender, AdCreatedEventArgs e)
		{
			/*
			string adName = e.AlternateText;
			String strRootPath = Server.MapPath("~");
			USCBase.UpdateImpressions(strRootPath, adName, "1");
			*/
		}

		protected void AdRotator4_AdCreated(object sender, AdCreatedEventArgs e)
		{
			/*
			string adName = e.AlternateText;
			String strRootPath = Server.MapPath("~");
			USCBase.UpdateImpressions(strRootPath, adName, "4");
			*/
		}

		protected void AdRotator5_AdCreated(object sender, AdCreatedEventArgs e)
		{
			/*
			string adName = e.AlternateText;
			String strRootPath = Server.MapPath("~");
			USCBase.UpdateImpressions(strRootPath, adName, "5");
			*/
		}

		protected void AdRotator6_AdCreated(object sender, AdCreatedEventArgs e)
		{
			/*
			string adName = e.AlternateText;
			String strRootPath = Server.MapPath("~");
			USCBase.UpdateImpressions(strRootPath, adName, "6");
			*/
		}

#endregion


#region Menus
		public void EnableUserMenus()
		{
/*
			UserAccount acct = GetActiveUser();
			if (null != acct)
			{
				// If we don't have a nav menu, we are calling for the second time.
				if( null != NavMenu )
				{
					MenuItem item = NavMenu.FindItem("Teams");
					if( null != item )
					{
						item.Enabled = true;
						item.Selectable = true;
					}

					item = NavMenu.FindItem("Forums");
					if( null != item )
					{
						item.Enabled = true;
						item.Selectable = true;
					}

					item = NavMenu.FindItem("Friends");
					if( null != item )
					{
						item.Enabled = true;
						item.Selectable = true;
					}

					item = NavMenu.FindItem("Profile");
					if( null != item )
					{
						item.Enabled = true;
						item.Selectable = true;
					}

				}
			}
*/
		}

		public void EnableAdminMenu()
		{
			UserAccount acct = GetActiveUser();
			if (null != acct)
			{
				// If we don't have a nav menu, we are calling for the second time.
				if( null != NavMenu )
				{
					MenuItem item = NavMenu.FindItem("Admin");
					if (acct.IsAdmin())
					{
						if( null == item )
						{
							//<asp:MenuItem Text="Admin" NavigateUrl="~/Admin/AdminMain.aspx" ToolTip="Administrative pages" Enabled="False" Selectable="False"></asp:MenuItem>
							item = new MenuItem( "Admin" );
							item.NavigateUrl = "~/Admin/AdminMain.aspx";
							item.ToolTip = "Administrative pages";
							NavMenu.Items.Add( item );
						}
						item.Enabled = true;
						item.Selectable = true;
					}
					else
					{
						if (null != item)
						{
						item.Enabled = false;
						item.Selectable = false;
					}
				}
			}
		}
		}

#endregion

#region SelectMenuItem
		public void SelectMenuItem( SelectedPage page )
		{
			if( null != NavMenu && NavMenu.Items.Count > 0)
			{
				switch (page)
				{
					case SelectedPage.Home:
					{
						NavMenu.FindItem("Home").Selected = true;
						break;
					}
					case SelectedPage.Teams:
					{
						NavMenu.FindItem("Teams").Selected = true;
						break;
					}
					case SelectedPage.News:
					{
						NavMenu.FindItem("News").Selected = true;
						break;
					}
					case SelectedPage.Forums:
					{
						NavMenu.FindItem("Forums").Selected = true;
						break;
					}
					case SelectedPage.Friends:
					{
						NavMenu.FindItem("Friends").Selected = true;
						break;
					}
					case SelectedPage.Profile:
					{
						NavMenu.FindItem("Profile").Selected = true;
						break;
					}
					case SelectedPage.Help:
					{
						NavMenu.FindItem("Help").Selected = true;
						break;
					}
					case SelectedPage.About:
					{
						NavMenu.FindItem("About").Selected = true;
						break;
					}
					case SelectedPage.Admin:
					{
						NavMenu.FindItem("Admin").Selected = true;
						break;
					}
					default:
					{
						NavMenu.FindItem("Home").Selected = true;
						break;
					}
				}
			} 
			
		}
#endregion

#region AlertUser
        public void AlertUser(string strMessage)
        {
            strMessage = USCBase.ReplaceString(strMessage, "'", "`");
            string strAlertUser = null;
            strAlertUser = "<script type=" + "\"text/javascript\">";
            strAlertUser = strAlertUser + "alert('" + strMessage + "');";
            strAlertUser = strAlertUser + "</script>";
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "AlertUser", strAlertUser, false);
        }
#endregion

#region Cookie_Variables
		public void WriteCookieValue(string key, string value)
		{
			g_cookieHelper.WriteCookieValue( key, value );
/*
			var ExpirationDays = GetSiteSetting(SiteAdmin.saKeyExpirationDays, "Expiration Days");
			Int32 intExpirationDays = Convert.ToInt32(ExpirationDays);

			HttpCookie cookie = Request.Cookies[key];

			if (cookie == null)
			{
				cookie = new HttpCookie(key);
			}

			//cookie.Domain = "MySportsConnect.net";
			cookie[key] = value;
			cookie.Expires = DateTime.Now.AddDays(intExpirationDays);
			Response.Cookies.Set(cookie);
*/
		}

		public void WriteCookieValueMin(string key, string value, int nMin)
		{
			g_cookieHelper.WriteCookieValueMin(key, value, nMin);
/*
			HttpCookie cookie = Request.Cookies[key];

			if (cookie == null)
			{
				cookie = new HttpCookie(key);
			}

			//cookie.Domain = "MySportsConnect.net";
			cookie[key] = value;
			cookie.Expires = DateTime.Now.AddMinutes( nMin );
			Response.Cookies.Set(cookie);
*/
		}

		public int ReadCookieInt(string key)
		{
			return g_cookieHelper.ReadCookieInt( key );
/*
			int nRet = -1;
			HttpCookie cookie = Request.Cookies[key];
			if (cookie != null)
			{
				string strValue = Convert.ToString(cookie.Value);
				if (null != strValue)
				{
					int nIndex = strValue.IndexOf('=');
					strValue = strValue.Substring(nIndex + 1);
					nRet = Convert.ToInt32(strValue);
				}
			}
			return nRet;
*/
		}

		public bool ReadCookieBool(string key)
		{
			return g_cookieHelper.ReadCookieBool(key);
/*
			bool fRet = false;
			HttpCookie cookie = Request.Cookies[key];
			if (cookie != null)
			{
				string strValue = Convert.ToString(cookie.Value);
				if (null != strValue)
				{
					int nIndex = strValue.IndexOf('=');
					strValue = strValue.Substring(nIndex + 1);
					fRet = Convert.ToBoolean(strValue);
				}
			}
			return fRet;
*/
		}

		public long ReadCookieLong(string key)
		{
			return g_cookieHelper.ReadCookieLong(key);
/*
			long lRet = -1;
			HttpCookie cookie = Request.Cookies[key];
			if (cookie != null)
			{
				string strValue = Convert.ToString(cookie.Value);
				if( null != strValue )
				{
					int nIndex = strValue.IndexOf('=');
					strValue = strValue.Substring(nIndex + 1);
					lRet = Convert.ToInt64(strValue);
				}
			}
			return lRet;
*/
		}

		public string ReadCookieString(string key)
		{
			return g_cookieHelper.ReadCookieString(key);
/*
			string strRet = "";
			HttpCookie cookie = Request.Cookies[key];
			if (cookie != null)
			{
				string strValue = Convert.ToString(cookie.Value);
				if( null != strValue )
				{
					int nIndex = strValue.IndexOf('=');
					strRet = strValue.Substring(nIndex + 1);
				}
			}
			return strRet;
*/
		}

		public long GetCookieUserID()
		{
			return g_cookieHelper.ReadCookieLong("MSC_UserID");
		}

		public void SetCookieUserID(long lIn)
		{
			g_cookieHelper.WriteCookieValue("MSC_UserID", Convert.ToString(lIn));
		}

		public void SetCookieUserLoggedIn(bool fIn)
		{
			g_cookieHelper.WriteCookieValueMin("MSC_UserLoggedIn", Convert.ToString(fIn), 30);
		}

		public bool GetCookieUserLoggedIn()
		{
			return g_cookieHelper.ReadCookieBool("MSC_UserLoggedIn");
		}

		public string GetCookieUserName()
		{
			return g_cookieHelper.ReadCookieString("MSC_UserName");
		}

		public void SetCookieUserName(string strIn)
		{
			g_cookieHelper.WriteCookieValue("MSC_UserName", strIn);
		}

		public string GetCookieUserPswd()
		{
			return g_cookieHelper.ReadCookieString("MSC_UserPswd");
		}

		public void SetCookieUserPswd(string strIn)
		{
			g_cookieHelper.WriteCookieValue("MSC_UserPswd", strIn);
		}

		public string GetCookieUserLanguage()
		{
			return g_cookieHelper.ReadCookieString("MSC_UserLanguage");
		}

		public void SetCookieUserLanguage(int nIn)
		{
			g_cookieHelper.WriteCookieValue("MSC_UserLanguage", Convert.ToString(nIn));
		}

		public string GetCookieUserEmail()
		{
			return g_cookieHelper.ReadCookieString("MSC_UserEmail");
		}

		public void SetCookieUserEmail(string strIn)
		{
			g_cookieHelper.WriteCookieValue("MSC_UserEmail", strIn);
		}

		public string GetCookieUserFirstName()
		{
			return g_cookieHelper.ReadCookieString("MSC_UserFirstName");
		}

		public void SetCookieUserFirstName(string strIn)
		{
			g_cookieHelper.WriteCookieValue("MSC_UserFirstName", strIn);
		}

		public string GetCookieUserLastName()
		{
			return g_cookieHelper.ReadCookieString("MSC_UserLastName");
		}

		public void SetCookieUserLastName(string strIn)
		{
			g_cookieHelper.WriteCookieValue("MSC_UserLastName", strIn);
		}

		public bool GetCookieUserKeepLoggedIn()
		{
			return g_cookieHelper.ReadCookieBool("MSC_UserKeepLoggedIn");
		}

		public void SetCookieUserKeepLoggedIn(bool fIn)
		{
			g_cookieHelper.WriteCookieValue("MSC_UserKeepLoggedIn", Convert.ToString(fIn));
		}

		public void ExpireCookies()
		{
			g_cookieHelper.ExpireCookies();
/*
			Response.Cookies["MSC_UserLoggedIn"].Expires = DateTime.Now.AddDays(-1);
			Response.Cookies["MSC_UserKeepLoggedIn"].Expires = DateTime.Now.AddDays(-1);
			Response.Cookies["MSC_UserID"].Expires = DateTime.Now.AddDays(-1);
			Response.Cookies["MSC_UserName"].Expires = DateTime.Now.AddDays(-1);
			Response.Cookies["MSC_UserPswd"].Expires = DateTime.Now.AddDays(-1);
			Response.Cookies["MSC_UserLanguage"].Expires = DateTime.Now.AddDays(-1);
			Response.Cookies["MSC_UserEmail"].Expires = DateTime.Now.AddDays(-1);
			Response.Cookies["MSC_UserFirstName"].Expires = DateTime.Now.AddDays(-1);
			Response.Cookies["MSC_UserLastName"].Expires = DateTime.Now.AddDays(-1);
*/
		}

#endregion
		
#region Account Access
		public string GetUserName()
		{
			return GetActiveUserName();
		}

		public long GetUserID()
		{
			return GetActiveUserID();
		}

		public UserAccount GetUserFromID( long UID )
		{
			UserAccount acct = (UserAccount)g_AccountsList.htAccountsList[UID];
			return acct;
		}

		public UserAccount GetActiveUser()
		{
			UserAccount acct = (UserAccount)g_AccountsList.htAccountsList[GetUserID()];
			return acct;
		}

		public int GetCurrentLanguage()
		{
			int nLanguage = 1;
			UserAccount acct = GetActiveUser();
			if( null != acct )
			{
				nLanguage = acct.Language;
			}
			return nLanguage;
		}
#endregion

#region User State
		public bool GetUserLoggedIn()
		{
			// Always check for a cookie first
			bool fUserLoggedIn = GetCookieUserLoggedIn();

			// Cookie may not exist, so check session
			if (!fUserLoggedIn)
			{
				fUserLoggedIn = GetSessionUserLoggedIn();
			}

			return fUserLoggedIn;
		}

		public string GetActiveUserName()
		{
			// Always check for a cookie first
			string strUsername = GetCookieUserName();

			// Cookie may not exist, so check session
			if (strUsername.Length == 0)
			{
				strUsername = GetSessionUserName();
			}

			return strUsername;
		}

		public long GetActiveUserID()
		{
			// Always check for a cookie first
			long lUserID = GetCookieUserID();

			// Cookie may not exist, so check session
			if (lUserID <= 0)
			{
				lUserID = GetSessionUserID();
			}

			return lUserID;
		}

#endregion

#region Session_Variables
		public void DeleteSessionVariables()
		{
			Session.RemoveAll();
		}

		public void ClearSessionVariables()
		{
			SetSessionUserID(0);
			SetSessionUserLoggedIn(false);
			SetSessionUserID(0);
			SetSessionUserName("");
		}

		public long GetSessionUserID()
		{
			long lRet = 0;
			object obj = Session["MSC_UserID"];
			if (null != obj)
			{
				string strValue = obj.ToString();
				lRet = Convert.ToInt64(strValue);
			}
			return lRet;
		}

		public void SetSessionUserID(long lIn)
		{
			Session["MSC_UserID"] = Convert.ToString(lIn);
		}

		public void SetSessionUserLoggedIn(bool fIn)
		{
			Session["MSC_UserLoggedIn"] = Convert.ToString(fIn);
		}

		public bool GetSessionUserLoggedIn()
		{
			bool fRet = false;
			object obj = Session["MSC_UserLoggedIn"];
			if (null != obj)
			{
				string strValue = obj.ToString();
				fRet = Convert.ToBoolean(strValue);
			}
			return fRet;
		}

		public string GetSessionUserName()
		{
			string strRet = "";
			object obj = Session["MSC_UserName"];
			if (null != obj)
			{
				strRet = obj.ToString();
			}
			return strRet;
		}

		public void SetSessionUserName(string strValue)
		{
			Session["MSC_UserName"] = strValue;
		}

#endregion

#region Clicks
		public void Logout(bool fRedirect)
		{
			ExpireCookies();
			ClearSessionVariables();
			if (fRedirect)
			{
				Response.Redirect("/LoginMSC.aspx");
			}
		}

		protected void OnClickLogoff(object sender, ImageClickEventArgs e)
		{
			Logout( true );
		}

		protected void OnClickHome(object sender, ImageClickEventArgs e)
		{
			Response.Redirect("http://www.mysportsconnect.net");
		}
        #endregion

        protected void OnClickAd1(object sender, ImageClickEventArgs e)
        {
			Response.Redirect("http://www.boombah.com");
		}

		protected void OnClickAd2(object sender, ImageClickEventArgs e)
        {
			Response.Redirect("http://www.nike.com");
		}

		protected void OnClickAd3(object sender, ImageClickEventArgs e)
        {
			Response.Redirect("http://www.pssbl.com");
		}

        protected void OnClickAd4(object sender, ImageClickEventArgs e)
        {
			Response.Redirect("http://www.rawlings.com");
		}

		protected void OnClickAd5(object sender, ImageClickEventArgs e)
        {
			Response.Redirect("http://www.starter.com");
		}
	}
}
