using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.SessionState;
using MyUSC.Classes;
using MyUSC.Classes.MyOrg;

namespace MyUSC.Classes
{
	public class CookieHelper
	{
		protected USCMaster m_page;

		public CookieHelper( USCMaster master )
		{
			m_page = master;
		}

		public void WriteCookieValue( string key, string value )
		{
			Int32 nCookieDuration = 30;
			HttpCookie cookie = m_page.Request.Cookies[key];

			if (cookie == null)
			{
				cookie = new HttpCookie(key);
			}

			//cookie.Domain = "MySportsConnect.net";
			cookie[key] = value;
			cookie.Expires = DateTime.Now.AddDays(nCookieDuration);
			m_page.Response.Cookies.Set(cookie);
		}

		public void WriteCookieValueMin( string key, string value, int nMin)
		{
			HttpCookie cookie = m_page.Request.Cookies[key];

			if (cookie == null)
			{
				cookie = new HttpCookie(key);
			}

			//cookie.Domain = "MySportsConnect.net";
			cookie[key] = value;
			cookie.Expires = DateTime.Now.AddMinutes(nMin);
			m_page.Response.Cookies.Set(cookie);
		}

		public int ReadCookieInt( string key)
		{
			int nRet = -1;
			HttpCookie cookie = m_page.Request.Cookies[key];
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

		public bool ReadCookieBool( string key)
		{
			bool fRet = false;
			HttpCookie cookie = m_page.Request.Cookies[key];
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

		public long ReadCookieLong( string key)
		{
			long lRet = -1;
			HttpCookie cookie = m_page.Request.Cookies[key];
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

		public string ReadCookieString( string key)
		{
			string strRet = "";
			HttpCookie cookie = m_page.Request.Cookies[key];
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

		public void ExpireCookies()
		{
			m_page.Response.Cookies["MSC_UserLoggedIn"].Expires = DateTime.Now.AddDays(-1);
			m_page.Response.Cookies["MSC_UserKeepLoggedIn"].Expires = DateTime.Now.AddDays(-1);
			m_page.Response.Cookies["MSC_UserID"].Expires = DateTime.Now.AddDays(-1);
			m_page.Response.Cookies["MSC_UserName"].Expires = DateTime.Now.AddDays(-1);
			m_page.Response.Cookies["MSC_UserPswd"].Expires = DateTime.Now.AddDays(-1);
			m_page.Response.Cookies["MSC_UserLanguage"].Expires = DateTime.Now.AddDays(-1);
			m_page.Response.Cookies["MSC_UserEmail"].Expires = DateTime.Now.AddDays(-1);
			m_page.Response.Cookies["MSC_UserFirstName"].Expires = DateTime.Now.AddDays(-1);
			m_page.Response.Cookies["MSC_UserLastName"].Expires = DateTime.Now.AddDays(-1);
		}

	}
}