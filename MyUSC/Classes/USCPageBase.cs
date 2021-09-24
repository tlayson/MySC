using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.SessionState;
using MyUSC.Classes;
using MyUSC.Classes.MyOrg;

namespace MyUSC.Classes
{
	public class USCPageBase : System.Web.UI.Page
	{
		#region SetVariables
		protected string _PAGENAME = "";
		protected string _USERTYPE = "";
		protected USCMaster _pgUSCMaster= null;
		#endregion

		protected USCPageBase()
		{
		}

		public string PageName
		{
			get
			{
				return this._PAGENAME;
			}
		}

		public USCMaster MasterPg
		{
			get
			{
				return this._pgUSCMaster;
			}
		}

		public virtual void ProcessChildClick( int nCmd )
		{
		}

#region Localization Stuff
		public string GetLocalizedString(long index, string page, string objName)
		{
			return ((USCMaster)Master).GetLocalizedString(index, page, objName);
		}

		public string GetSiteSetting(long index, string keyName)
		{
			return ((USCMaster)Master).GetSiteSetting(index, keyName);
		}
#endregion

#region Login
		public void RedirectToLoginPage()
		{
			Response.Redirect("~/LoginMSC.aspx");
		}

#region Function AutoLogin
		protected bool AutoLogin()
		{
			bool fRet = false;
			bool fKeepLoggedIn = GetCookieUserKeepLoggedIn();
			if( fKeepLoggedIn )
			{
				string strUserName = GetCookieUserName();
				UserAccount userAcct = _pgUSCMaster.g_AccountsList.GetAccountByUserName(strUserName);
				ClearSessionVariables();
				if (null != userAcct)
				{
					if (GetCookieUserPswd() == userAcct.Password)
					{
						fRet = true;
						ProcessLogin(userAcct);
					}
					else
					{
						// For debugging only
						USCEncrypt usce = new USCEncrypt();
						string strDecryptedCookiePswd = usce.DecryptString(GetCookieUserPswd());
						string strDecryptedUserPswd = usce.DecryptString(userAcct.Password);
					}
				}
			}
			return fRet;
		}
#endregion

		protected string BuildFriendDetailsLink(long friendID, string txt)
		{
			string strAnchorTag = "<a class=\"medSiteColorTxt\"  href=\"FriendDetails.aspx?FriendID=";
			StringBuilder sbLink = new StringBuilder();
			sbLink.Append(strAnchorTag).Append(friendID).Append("\">");
			sbLink.Append(txt).Append("</a>");
			return sbLink.ToString();
		}

		protected UserAccount.UserTypes CurrentUserType()
		{
			UserAccount.UserTypes type = UserAccount.UserTypes.Anonymous;
			UserAccount acct = GetActiveUser();
			if( null != acct )
			{
				type = acct.UserType;
			}
			return type;
		}
		
		protected bool IsSuperUser()
		{
			bool fRet = false;
			UserAccount.UserTypes type = CurrentUserType();
			if( type == UserAccount.UserTypes.SuperUser )
			{
				fRet = true;
			}
			return fRet;
		}

		protected bool IsUserAdmin()
		{
			bool fRet = false;
			UserAccount.UserTypes type = CurrentUserType();
			if (type == UserAccount.UserTypes.Admin || type == UserAccount.UserTypes.SuperUser)
			{
				fRet = true;
			}
			return fRet;
		}

		protected void RedirectToLoginPage( string strReturnURL )
		{
			StringBuilder sb = new StringBuilder();
			sb.Append( "/LoginMSC.aspx" );

			if( strReturnURL.Length > 0 )
			{
				sb.Append( "?URL=" ).Append( Server.UrlEncode( strReturnURL ) );
			}
			Response.Redirect( sb.ToString() );
		}

		protected bool LoginAndReturn( bool fRedirect, string strReturnURL )
		{
			bool fRet = GetUserLoggedIn();
			if( !fRet && fRedirect )
			{
				RedirectToLoginPage( strReturnURL );
			}
			return fRet;
		}

		protected bool IsUserLoggedIn(bool fRedirect)
		{
			bool fRet = GetUserLoggedIn();
			if( !fRet && fRedirect )
			{
				RedirectToLoginPage();
			}
			return fRet;
		}

		protected void ProcessLogin(UserAccount userAcct)
		{
			userAcct.LoginAttempts = 0;
			// Call even for auto-login to refresh expiration dates.
			SetCookieVariables(userAcct);
			userAcct.UpdateUserLastLogin();
			
			string strURL = GetSessionReturnURL();
			if( strURL.Length > 0 )
			{
				// Clear the URL
				SetSessionReturnURL( "" );
				Response.Redirect( strURL, false );
			}
			else
			{
				RedirectToDefault( userAcct );
			}
		}

		protected void RedirectToDefault( UserAccount userAcct )
		{
			string strRedirect = "Friends.aspx";
			if (!userAcct.AcceptedTOU)
			{
				strRedirect = "TOU.aspx";
			}
			else if (userAcct.DefaultPage.ToLower() == "home")
			{
				strRedirect = "default.aspx";
			}
			else if (userAcct.DefaultPage.ToLower() == "friends")
			{
				strRedirect = "friends.aspx";
			}
			else if (userAcct.DefaultPage.ToLower() == "dashboard")
			{
				strRedirect = "~/MyTeams/Dashboard.aspx";
			}
			else if (userAcct.DefaultPage.ToLower() == "sportsnews")
			{
				strRedirect = "SportsNews.aspx";
			}
			else if (userAcct.DefaultPage.ToLower() == "forums")
			{
				strRedirect = "/YForum/Forum.aspx";
			}
			Response.Redirect(strRedirect, false);
		}

#endregion

#region Cookie_Variables
		public void WriteCookieValue(string key, string value)
		{
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
		}

		public void WriteCookieValueMin(string key, string value, int nMin)
		{
			HttpCookie cookie = Request.Cookies[key];

			if (cookie == null)
			{
				cookie = new HttpCookie(key);
			}

			//cookie.Domain = "MySportsConnect.net";
			cookie[key] = value;
			cookie.Expires = DateTime.Now.AddMinutes(nMin);
			Response.Cookies.Set(cookie);
		}

		public int ReadCookieInt(string key)
		{
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
		}

		public bool ReadCookieBool(string key)
		{
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
		}

		public long ReadCookieLong(string key)
		{
			long lRet = -1;
			HttpCookie cookie = Request.Cookies[key];
			if (cookie != null)
			{
				string strValue = Convert.ToString(cookie.Value);
				if (null != strValue)
				{
					int nIndex = strValue.IndexOf('=');
					strValue = strValue.Substring(nIndex + 1);
					lRet = Convert.ToInt64(strValue);
				}
			}
			return lRet;
		}

		public string ReadCookieString(string key)
		{
			string strRet = "";
			HttpCookie cookie = Request.Cookies[key];
			if (cookie != null)
			{
				string strValue = Convert.ToString(cookie.Value);
				if (null != strValue)
				{
					int nIndex = strValue.IndexOf('=');
					strRet = strValue.Substring(nIndex + 1);
				}
			}
			return strRet;
		}

		public long GetCookieUserID()
		{
			return ReadCookieLong("MSC_UserID");
		}

		public void SetCookieUserID(long lIn)
		{
			WriteCookieValue( "MSC_UserID", Convert.ToString(lIn) );
		}

		public void SetCookieUserLoggedIn(bool fIn)
		{
			WriteCookieValueMin("MSC_UserLoggedIn", Convert.ToString(fIn), 30);
		}

		public bool GetCookieUserLoggedIn()
		{
			return ReadCookieBool("MSC_UserLoggedIn");
		}

		public string GetCookieUserName()
		{
			return ReadCookieString("MSC_UserName");
		}

		public void SetCookieUserName(string strIn)
		{
			WriteCookieValue("MSC_UserName", strIn);
		}

		public string GetCookieUserPswd()
		{
			return ReadCookieString("MSC_UserPswd");
		}

		public void SetCookieUserPswd(string strIn)
		{
			WriteCookieValue("MSC_UserPswd", strIn);
		}

		public string GetCookieUserLanguage()
		{
			return ReadCookieString("MSC_UserLanguage");
		}

		public void SetCookieUserLanguage(int  nIn)
		{
			WriteCookieValue("MSC_UserLanguage", Convert.ToString(nIn));
		}

		public string GetCookieUserEmail()
		{
			return ReadCookieString("MSC_UserEmail");
		}

		public void SetCookieUserEmail(string strIn)
		{
			WriteCookieValue("MSC_UserEmail", strIn);
		}

		public string GetCookieUserFirstName()
		{
			return ReadCookieString("MSC_UserFirstName");
		}

		public void SetCookieUserFirstName(string strIn)
		{
			WriteCookieValue("MSC_UserFirstName", strIn);
		}

		public string GetCookieUserLastName()
		{
			return ReadCookieString("MSC_UserLastName");
		}

		public void SetCookieUserLastName(string strIn)
		{
			WriteCookieValue("MSC_UserLastName", strIn);
		}

		public bool GetCookieUserKeepLoggedIn()
		{
			return ReadCookieBool("MSC_UserKeepLoggedIn");
		}

		public void SetCookieUserKeepLoggedIn(bool fIn)
		{
			WriteCookieValue("MSC_UserKeepLoggedIn", Convert.ToString(fIn));
		}

		public void ExpireCookies()
		{
			Response.Cookies["MSC_UserLoggedIn"].Expires = DateTime.Now.AddDays(-1);
			Response.Cookies["MSC_UserKeepLoggedIn"].Expires = DateTime.Now.AddDays(-1);
			Response.Cookies["MSC_UserID"].Expires = DateTime.Now.AddDays(-1);
			Response.Cookies["MSC_UserName"].Expires = DateTime.Now.AddDays(-1);
			Response.Cookies["MSC_UserPswd"].Expires = DateTime.Now.AddDays(-1);
			Response.Cookies["MSC_UserLanguage"].Expires = DateTime.Now.AddDays(-1);
			Response.Cookies["MSC_UserEmail"].Expires = DateTime.Now.AddDays(-1);
			Response.Cookies["MSC_UserFirstName"].Expires = DateTime.Now.AddDays(-1);
			Response.Cookies["MSC_UserLastName"].Expires = DateTime.Now.AddDays(-1);
		}

		protected void SetCookieVariables(UserAccount userAcct)
		{
			SetCookieUserID(userAcct.Key);
			SetCookieUserName(userAcct.UserName);
			SetCookieUserPswd(userAcct.Password);
			SetCookieUserLoggedIn(true);
			SetCookieUserKeepLoggedIn(userAcct.Preferences.KeepLoggedIn);
			SetSessionVariables(userAcct);
		}
#endregion

#region User State
		public bool GetUserLoggedIn()
		{
			// Always check for a cookie first
			bool fUserLoggedIn = GetCookieUserLoggedIn();

			// Cookie may not exist, so check session
			if( !fUserLoggedIn )
			{
				fUserLoggedIn = GetSessionUserLoggedIn();
			}

			return fUserLoggedIn;
		}

		public UserAccount GetUserFromID( long UID )
		{
			return ((USCMaster)Master).GetUserFromID( UID );
		}

		public string GetActiveUserName()
		{
			// Always check for a cookie first
			string strUsername = GetCookieUserName();

			// Cookie may not exist, so check session
			if( strUsername.Length == 0 )
			{
				strUsername = GetSessionUserName();
			}

			return strUsername;
		}

		public UserAccount GetActiveUser()
		{
			UserAccount acct = (UserAccount)((USCMaster)Master).g_AccountsList.htAccountsList[GetActiveUserID()];
			return acct;
		}

		public long GetActiveUserID()
		{
			// Always check for a cookie first
			long lUserID = GetCookieUserID();

			// Cookie may not exist, so check session
			if( lUserID <= 0 )
			{
				lUserID = GetSessionUserID();
			}

			return lUserID;
		}

#endregion

#region Session_Variables
		public void SetSessionVariables(UserAccount userAcct)
		{
			SetSessionUserID( userAcct.Key);
			SetSessionUserLoggedIn(true);
			SetSessionUserName(userAcct.UserName);
		}

		public void DeleteSessionVariables()
		{
			Session.RemoveAll();
		}

		public void ClearSessionVariables()
		{
			SetSessionPageSection("");
			SetSessionMessage( "" );
			SetSessionUserID( 0 );
			SetSessionUserLoggedIn(false);
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

		public long GetSessionValue1()
		{
			long lRet = -1;
			object obj = Session["MSC_Value1"];
			if (null != obj)
			{
				string strValue = obj.ToString();
				lRet = Convert.ToInt64(strValue);
			}
			return lRet;
		}

		public void SetSessionValue1(long lIn)
		{
			Session["MSC_Value1"] = Convert.ToString(lIn);
		}

		public long GetSessionValue2()
		{
			long lRet = -1;
			object obj = Session["MSC_Value2"];
			if (null != obj)
			{
				string strValue = obj.ToString();
				lRet = Convert.ToInt64(strValue);
			}
			return lRet;
		}

		public void SetSessionValue2(long lIn)
		{
			Session["MSC_Value2"] = Convert.ToString(lIn);
		}

		public long GetSessionValue3()
		{
			long lRet = -1;
			object obj = Session["MSC_Value3"];
			if (null != obj)
			{
				string strValue = obj.ToString();
				lRet = Convert.ToInt64(strValue);
			}
			return lRet;
		}

		public void SetSessionValue3(long lIn)
		{
			Session["MSC_Value3"] = Convert.ToString(lIn);
		}

		public long GetSessionValue4()
		{
			long lRet = -1;
			object obj = Session["MSC_Value4"];
			if (null != obj)
			{
				string strValue = obj.ToString();
				lRet = Convert.ToInt64(strValue);
			}
			return lRet;
		}

		public void SetSessionValue4(long lIn)
		{
			Session["MSC_Value4"] = Convert.ToString(lIn);
		}

		public long GetSessionOrgID()
		{
			long lRet = 0;
			object obj = Session["MSC_OrgID"];
			if (null != obj)
			{
				string strValue = obj.ToString();
				lRet = Convert.ToInt64(strValue);
			}
			return lRet;
		}

		public void SetSessionOrgID(long lIn)
		{
			Session["MSC_OrgID"] = Convert.ToString(lIn);
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
		
		public string GetSessionQuery()
		{
			string strRet = "";
			object obj = Session["MSC_Query"];
			if (null != obj)
			{
				strRet = obj.ToString();
			}
			return strRet;
		}

		public void SetSessionQuery(string strValue)
		{
			Session["MSC_Query"] = strValue;
		}
		
		public string GetSessionReturnURL()
		{
			string strRet = "";
			object obj = Session["MSC_ReturnURL"];
			if (null != obj)
			{
				strRet = obj.ToString();
			}
			return strRet;
		}

		public void SetSessionReturnURL(string strValue)
		{
			Session["MSC_ReturnURL"] = strValue;
		}

		public string GetSessionPageSection()
		{
			string strRet = "";
			object obj = Session["MSC_PageSection"];
			if (null != obj)
			{
				strRet = obj.ToString();
			}
			return strRet;
		}

		public void SetSessionPageSection(string strValue)
		{
			Session["MSC_PageSection"] = strValue;
		}

		public string GetSessionMessage()
		{
			string strRet = "";
			object obj = Session["MSC_Message"];
			if (null != obj)
			{
				strRet = obj.ToString();
			}
			return strRet;
		}

		public void SetSessionMessage(string strValue)
		{
			Session["MSC_Message"] = strValue;
		}

#endregion

#region MyOrg
		public bool UserHasViewAccess( long acctID, long orgID, OrgPageID pid )
		{
			bool fRet = false;
			Organization org =  GetOrgByID( orgID );
			if( null != org )
			{
				fRet = org.AllowGuestViews;

				OrgMember member = GetOrgMember(acctID, orgID);
				if ( null != member  && member.MemberType != OrgAccessTypes.Banned )
				{
					OrgPageOptions opo = org.orgPageOptionList.GetOrgPageOption( pid );
					if( null != opo )
					{
						if( member.MemberType <= opo.ViewLevel )
						{
							fRet = true;
						}
					}
				}
			}
			return fRet;
		}

		public bool UserHasMemberAccess(long acctID, long orgID, OrgPageID pid)
		{
			bool fRet = false;
			Organization org = GetOrgByID(orgID);
			if (null != org)
			{
				OrgMember member = GetOrgMember(acctID, orgID);
				if (null != member)
				{
					OrgPageOptions opo = org.orgPageOptionList.GetOrgPageOption(pid);
					if (null != opo)
					{
						if (member.MemberType <= opo.AccessLevel)
						{
							fRet = true;
						}
					}
				}
			}
			return fRet;
		}

		public bool UserHasEditAccess(long acctID, long orgID, OrgPageID pid)
		{
			bool fRet = false;
			Organization org = GetOrgByID(orgID);
			if (null != org)
			{
				fRet = org.AllowGuestViews;

				OrgMember member = GetOrgMember(acctID, orgID);
				if (null != member)
				{
					OrgPageOptions opo = org.orgPageOptionList.GetOrgPageOption(pid);
					if (null != opo)
					{
						if (member.MemberType <= opo.EditLevel)
						{
							fRet = true;
						}
					}
				}
			}
			return fRet;
		}

		public bool UserHasAdminAccess(long acctID, long orgID, OrgPageID pid)
		{
			bool fRet = false;
			Organization org = GetOrgByID(orgID);
			if (null != org)
			{
				fRet = org.AllowGuestViews;

				OrgMember member = GetOrgMember(acctID, orgID);
				if (null != member)
				{
					OrgPageOptions opo = org.orgPageOptionList.GetOrgPageOption(pid);
					if (null != opo)
					{
						if (member.MemberType <= opo.AdminLevel)
						{
							fRet = true;
						}
					}
				}
			}
			return fRet;
		}

		public OrgAccessTypes GetOrgMemberType(string strAcct, long orgID)
		{
			OrgAccessTypes type = OrgAccessTypes.Undefined;
			long lID = 0;
			if (long.TryParse(strAcct, out lID))
			{
				type = GetOrgMemberType(lID, orgID);
			}
			return type;
		}

		public OrgAccessTypes GetOrgMemberType(long acctID, long orgID)
		{
			OrgAccessTypes type = OrgAccessTypes.Undefined;
			OrgMember member = GetOrgMember(acctID, orgID);
			if (null != member)
			{
				type = member.MemberType;
			}
			return type;
		}

		public OrgMember GetOrgMember(string strAcct, long orgID)
		{
			OrgMember member = null;
			long lID = 0;
			if (long.TryParse(strAcct, out lID))
			{
				member = GetOrgMember(lID, orgID);
			}
			return member;
		}

		public OrgMember GetOrgMember(long acctID, long orgID)
		{
			OrgMember member = null;
			Organization org = ((USCMaster)Master).g_OrgList.GetOrganization(orgID);
			if (null != org)
			{
				member = org.GetOrganizationMember( acctID );
			}
			return member;
		}

		public bool IsMember(string strAcct, long orgID)
		{
			bool fRet = false;
			long lID = 0;
			if (long.TryParse(strAcct, out lID))
			{
				fRet = IsMember(lID, orgID);
			}
			return fRet;
		}

		public bool IsMember(long acctID, long orgID)
		{
			bool fRet = false;
			OrgAccessTypes type = GetOrgMemberType(acctID, orgID);

			if( type <= OrgAccessTypes.Member && type != OrgAccessTypes.Undefined )
			{
				fRet = true;
			}
			return fRet;
		}

		public UserAccount GetUserByID(string strAcct)
		{
			UserAccount acct = null;
			long lID = 0;
			if (long.TryParse(strAcct, out lID))
			{
				acct = GetUserByID(lID);
			}
			return acct;
		}

		public UserAccount GetUserByID(long acctID)
		{
			UserAccount acct = (UserAccount)((USCMaster)Master).g_AccountsList.htAccountsList[acctID];
			return acct;
		}

		public Organization GetOrgByID(string strOrgID)
		{
			Organization org = null;
			long lID = 0;
			if (long.TryParse(strOrgID, out lID))
			{
				org = GetOrgByID(lID);
			}
			return org;
		}

		public Organization GetOrgByID(long orgID)
		{
			Organization org = (Organization)((USCMaster)Master).g_OrgList.htOrgList[orgID];
			return org;
		}

#endregion

#region SQL
		// Used to determine if this is the production instance or the development instance on the same server.
		// This should never be an issue on dev only machines.
		public bool IsLocalInstance()
		{
			bool fRet = false;
			if( null != _pgUSCMaster )
			{
				if (null != _pgUSCMaster.g_strConnectionString)
				{
					if (_pgUSCMaster.g_strConnectionString.Contains("MyUSCLocal"))
					{
						fRet = true;
					}
				}
			}

			return fRet;
		}

		protected string Sanitize(string line)
		{
			return SQLHelper.Sanitize( line );
		}

		protected string ObjectToString( Object obj )
		{
			return SQLHelper.ObjectToString( obj );
		}

		protected int ObjectToInt(Object obj)
		{
			return SQLHelper.ObjectToInt(obj);
		}

		protected long ObjectToLong(Object obj)
		{
			return SQLHelper.ObjectToLong(obj);
		}

		protected float ObjectToFloat(Object obj)
		{
			return SQLHelper.ObjectToFloat(obj);
		}
		protected bool ObjectToBool(Object obj)
		{
			return SQLHelper.ObjectToBool(obj);
		}
		protected DateTime ObjectToDateTime(Object obj)
		{
			return SQLHelper.ObjectToDateTime(obj);
		}

#endregion
	}

	public class USCControlBase : System.Web.UI.UserControl
	{
		protected USCControlBase()
		{
		}
	}

}