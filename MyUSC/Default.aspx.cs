using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Xml.XPath;
using MyUSC.Classes;

namespace MyUSC
{
	public partial class _Default : USCPageBase
    {
		protected _Default()
		{
			_PAGENAME = "Default";
			_pgUSCMaster = (USCMaster)Master;
		}

#region PageLoad
		protected void DisplayAnnouncements()
		{
			string strAnnounce = GetSiteSetting(SiteAdmin.saKeyHomeAnnouncement, "HomeAnnouncement");
			if( 0 < strAnnounce.Length )
			{
				pnlAnnounce.Visible = true;

				lblAnnounceText.Text = strAnnounce;

				//StringBuilder sb = new StringBuilder();
				//sb.Append("If you are experiencing issues with the KeepLoggedIn option not working and/or you have to sign in again after refreshing your browser,");
				//sb.Append("you will likely be able to fix the issue by enabling your browser to use cookies.");
			}
			else
			{
				pnlAnnounce.Visible = false;
			}
		}

		protected void DisplayTopStories()
		{
			string strURL = GetSiteSetting(SiteAdmin.saKeyHomeNewsURL, "HomeNewsURL");
			//string strURL = "http://sports.espn.go.com/espn/rss/news";
			try
			{
				XmlDocument xmlRSSDocument = new XmlDocument();
				xmlRSSDocument.Load(strURL);
				xmlRSSDisplay.XPathNavigator = xmlRSSDocument.CreateNavigator();
			}
			catch( Exception ex )
			{
				EvtLog.WriteException("Default.DisplayTopStories failure", ex, 0);
			}

		}

		protected void Page_Load(object sender, EventArgs e)
        {
			UserAccount acct = GetActiveUser();
			if (null == acct)
			{
				//RedirectToLoginPage();
				chkMakeDefaultPage.Enabled = false;
				chkMakeDefaultPage.Visible = false;
			}
			else
			{
				chkMakeDefaultPage.Checked = false;
				if (acct.DefaultPage.ToLower() == "home")
				{
					chkMakeDefaultPage.Checked = true;
				}
				Master.SelectMenuItem(SelectedPage.Home);
			}
			DisplayAnnouncements();
			DisplayTopStories();
        }
#endregion

		protected void OnClickMakeHome(object sender, EventArgs e)
		{
			UserAccount acct = GetActiveUser();
			if (null != acct)
			{
				if (chkMakeDefaultPage.Checked)
				{
					acct.DefaultPage = "home";
				}
				else
				{
					acct.DefaultPage = "friends";
				}
				acct.Update();
			}
		}

		protected void OnClickHomeNews(object sender, ImageClickEventArgs e)
		{
			Response.Redirect("/SportsNews.aspx");
		}

		protected void OnClickHomeTeams(object sender, ImageClickEventArgs e)
		{
			Response.Redirect("/MyTeams/Dashboard.aspx");
		}

		protected void OnClickHomeFriends(object sender, ImageClickEventArgs e)
		{
			Response.Redirect("/Friends.aspx");
		}

    }
}
