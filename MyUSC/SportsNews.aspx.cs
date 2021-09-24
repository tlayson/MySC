using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Xml.XPath;
using MyUSC.Classes;

namespace MyUSC
{
	public partial class SportsNews : USCPageBase
	{
		string m_strDisplayURL = "SportsNews.aspx?";

		protected SportsNews()
		{
			_PAGENAME = "SportsNews";
			_pgUSCMaster = (USCMaster)Master;
		}

		private bool BuildLevel4(MenuItem miParent, NewsMenuItem parentItem)
		{
			bool fRet = true;
			try
			{
				foreach (KeyValuePair<long, object> kvp in Master.g_NewsMenuList.m_sdNewsMenuLvl4Items)
				{
					NewsMenuItem newsMenuItem = (NewsMenuItem)kvp.Value;
					if (null != newsMenuItem && newsMenuItem.Active)
					{
						if (parentItem.Key == newsMenuItem.ParentID)
						{
							string strRSS = Convert.ToString(newsMenuItem.RSSID);
							string strURL = m_strDisplayURL + "NewsMenuID=" + newsMenuItem.Key;

							MenuItem mi = new MenuItem(newsMenuItem.Name);
							mi.NavigateUrl = strURL;
							mi.Value = strRSS;
							if (newsMenuItem.Target.Length > 0)
							{
								mi.Target = newsMenuItem.Target;
							}
							miParent.ChildItems.Add(mi);
						}
					}
				}
			}
			catch (Exception ex)
			{
				EvtLog.WriteException("SportsNews.BuildLevel4 failure", ex, 0);
				return false;
			}
			return fRet;
		}

		private bool BuildLevel3(MenuItem miParent, NewsMenuItem parentItem)
		{
			bool fRet = true;
			try
			{
				foreach (KeyValuePair<long, object> kvp in Master.g_NewsMenuList.m_sdNewsMenuLvl3Items)
				{
					NewsMenuItem newsMenuItem = (NewsMenuItem)kvp.Value;
					if (null != newsMenuItem && newsMenuItem.Active)
					{
						if (parentItem.Key == newsMenuItem.ParentID)
						{
							string strRSS = Convert.ToString(newsMenuItem.RSSID);
							string strURL = m_strDisplayURL + "NewsMenuID=" + newsMenuItem.Key;

							MenuItem mi = new MenuItem(newsMenuItem.Name);
							mi.NavigateUrl = strURL;
							mi.Value = strRSS;
							if (newsMenuItem.Target.Length > 0)
							{
								mi.Target = newsMenuItem.Target;
							}

							BuildLevel4(mi, newsMenuItem);

							miParent.ChildItems.Add(mi);
						}
					}
				}
			}
			catch (Exception ex)
			{
				EvtLog.WriteException("SportsNews.BuildLevel3 failure", ex, 0);
				return false;
			}
			return fRet;
		}

		protected void BuildLevel2(MenuItem miParent, NewsMenuItem parentItem)
		{
			try
			{
				foreach (KeyValuePair<long, object> kvp in Master.g_NewsMenuList.m_sdNewsMenuLvl2Items)
				{
					NewsMenuItem newsMenuItem = (NewsMenuItem)kvp.Value;
					if (null != newsMenuItem && newsMenuItem.Active)
					{
						if (parentItem.Key == newsMenuItem.ParentID)
						{
							string strRSS = Convert.ToString(newsMenuItem.RSSID);
							string strURL = m_strDisplayURL + "NewsMenuID=" + newsMenuItem.Key;

							MenuItem mi = new MenuItem(newsMenuItem.Name);
							mi.Enabled = true;
							if( newsMenuItem.RSSID > 0 )
							{
								mi.NavigateUrl = strURL;
							}
/*
							RSSFeed feed = Master.g_RSSFeedList.GetRSSFeed( newsMenuItem.RSSID );
							if( null == feed || (feed.Url.Length == 0 && !feed.UseWebsite) )
							{
								mi.Enabled = false;
							}
*/
							mi.Value = strRSS;
							if (newsMenuItem.Target.Length > 0)
							{
								mi.Target = newsMenuItem.Target;
							}

							BuildLevel3(mi, newsMenuItem);

							miParent.ChildItems.Add(mi);
						}
					}
				}
			}
			catch (Exception ex)
			{
				EvtLog.WriteException("SportsNews.BuildLevel2 failure", ex, 0);
			}
		}

		protected void BuildMenu()
		{
			try
			{
				UserAccount acct = GetActiveUser();

				foreach (KeyValuePair<long, object> kvp in Master.g_NewsMenuList.m_sdNewsMenuLvl1Items)
				{
					NewsMenuItem newsMenuItem = (NewsMenuItem)kvp.Value;
					if( null != newsMenuItem && newsMenuItem.Active )
					{
						int nKey = Convert.ToInt32(newsMenuItem.Key);
						// If there is no account, show everything.  Otherwise limit to their selections.
						if (null == acct || acct.Preferences.htNewsMenuItems.Contains(nKey))
						{
							string strRSS = Convert.ToString(newsMenuItem.RSSID);
							string strURL = m_strDisplayURL + "NewsMenuID=" + newsMenuItem.Key;

							MenuItem mi = new MenuItem(newsMenuItem.Name);
							mi.NavigateUrl = strURL;
							mi.Value = strRSS;
							if (newsMenuItem.Target.Length > 0)
							{
								mi.Target = newsMenuItem.Target;
							}

							BuildLevel2(mi, newsMenuItem);

							NewSportsMenu.Items.Add(mi);
						}
					}
				}
			}
			catch (Exception ex)
			{
				EvtLog.WriteException("SportsNews.BuildMenu failure", ex, 0);
			}
		}
		
		protected void DisplayDefaultPage()
		{
			string strURL = GetSiteSetting(SiteAdmin.saKeyHomeNewsURL, "HomeNewsURL");
			try
			{
				XmlDocument xmlRSSDocument = new XmlDocument();
				xmlRSSDocument.Load(strURL);
				xmlRSSDisplay.XPathNavigator = xmlRSSDocument.CreateNavigator();
				lblName.Text = "Today's top stories";
				lblParent.Text = "";
				pnlDisplayXML.Visible = true;
			}
			catch( Exception ex )
			{
				EvtLog.WriteException("SportsNews.DisplayDefaultPage failure", ex, 0);
			}
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				//here we check to see if the user is logged in
				chkMakeDefaultPage.Checked = false;
				UserAccount acct = GetActiveUser();
				if (null == acct)
				{
					//RedirectToLoginPage();
					chkMakeDefaultPage.Enabled = false;
					chkMakeDefaultPage.Visible = false;
				}
				else if (acct.DefaultPage.ToLower() == "sportsnews")
				{
					chkMakeDefaultPage.Checked = true;
				}

				BuildMenu();
			}

			bool fLoaded = true;
			Master.SelectMenuItem(SelectedPage.News);
			bool fNoSelection = false;
			string strRSSURL = Request.QueryString["RSSURL"];
			string strNewsMenuID = Request.QueryString["NewsMenuID"];
			NewsMenuItem newsMenuItem = null;
			long lRSSID = 0;
			if (null != strNewsMenuID && strNewsMenuID.Length > 0)
			{
				pnlDisplayHTML.Visible = false;
				pnlDisplayXML.Visible = false;
				pnlError.Visible = false;

				string strImgURL = "~/Images/";
				lblParent.Text = "";

				long lKey = Convert.ToInt64(strNewsMenuID);
				newsMenuItem = Master.g_NewsMenuList.GetNewsMenuItem(lKey);
				if (null != newsMenuItem)
				{
					lRSSID = newsMenuItem.RSSID;
					lblName.Text = newsMenuItem.Name;
					strImgURL += newsMenuItem.LogoUrl;
					if (newsMenuItem.ParentID > 0)
					{
						NewsMenuItem parentItem = Master.g_NewsMenuList.GetNewsMenuItem(newsMenuItem.ParentID);
						if (null != parentItem)
						{
							lblParent.Text = parentItem.Name;
						}
					}
				}
				else
				{
					fNoSelection = true;
				}

				if (fNoSelection)
				{
					pnlDisplayHTML.Visible = false;
					pnlDisplayXML.Visible = false;
					pnlError.Visible = false;
					lblName.Visible = false;
					lblParent.Visible = false;
					imgLogo.Visible = false;
				}
				else
				{
					imgLogo.ImageUrl = strImgURL;

					if (lRSSID > 0)
					{
						fLoaded = false;
						pnlDisplayXML.Visible = true;

						RSSFeed feed = Master.g_RSSFeedList.GetRSSFeed(lRSSID);
						if (null != feed )
						{
							if( feed.UseWebsite )
							{
								fLoaded = DisplayWebsite( feed.Website );
							}
							else if( feed.Url.Length > 0 )
							{
								try
								{
									// Need to figure out how to load from XPath
									//XPathDocument xdoc = new XPathDocument( strURL );
									XmlDocument xmlRSSDocument = new XmlDocument();
									xmlRSSDocument.Load(feed.Url);
									XmlNode root = xmlRSSDocument.FirstChild;
									XmlNode feedContent = root.NextSibling;
									if( null == feedContent && root.Name == "rss" )
									{
										feedContent = root;
									}
									// Make sure we're at the doc.
									string strT = feedContent.OuterXml;
									if (strT[1] == '?')
									{
										feedContent = feedContent.NextSibling;
									}
									XmlAttribute attr = feedContent.Attributes["xmlns"];
									if (null != attr)
									{
										StringReader stringReader = new StringReader(xmlRSSDocument.OuterXml);
										XmlTextReader xmlReader = new XmlTextReader(stringReader);
										xmlReader.Namespaces = false; //A trick to handle special xmlns attributes as regular

										//Build DOM
										XmlDocument xmlDocument = new XmlDocument();
										xmlDocument.Load(xmlReader);

										//Do the job
										xmlDocument.DocumentElement.RemoveAttribute("xmlns");

										//Prepare a writer
										StringWriter stringWriter = new StringWriter();
										XmlTextWriter xmlWriter = new XmlTextWriter(stringWriter);

										//Optional: Make an output nice ;)
										xmlWriter.Formatting = Formatting.Indented;
										xmlWriter.IndentChar = ' ';
										xmlWriter.Indentation = 2;

										//Build output
										xmlDocument.Save(xmlWriter);
										xmlRSSDocument = xmlDocument;
									}

									xmlRSSDisplay.XPathNavigator = xmlRSSDocument.CreateNavigator();
									//xmlRSSDisplay.Document = xmlRSSDocument;
									fLoaded = true;
								}
								catch (Exception ex)
								{
									string strError = "SportsNews RSS failure for RSSID = ";
									strError += lRSSID;
									strError += "   Feed URL = ";
									strError += feed.Url;
									EvtLog.WriteException(strError, ex, EventErrors.ErrorType.RSSFeed);
								}
							}

						}
					}
					else
					{
						// Set error message
						string strLoadError = "We currently do not have an RSS feed for this selection.  If you are aware of one, please contact support and let us know.";
						lblDisplayError.Text = strLoadError;

						pnlDisplayHTML.Visible = false;
						pnlDisplayXML.Visible = false;
						pnlError.Visible = true;
					}
				}
			}
			else if ((null != strRSSURL && strRSSURL.Length > 0))
			{
				fLoaded = DisplayWebsite(strRSSURL);
			}
			else if ((null == strRSSURL && null == strNewsMenuID))
			{
				DisplayDefaultPage();
				fLoaded = true;
			}


			if (!fLoaded)
			{
				// Set error message
				string strLoadError = "There was a problem loading the RSS feed.  If this problem persists, please notify support.";
				lblDisplayError.Text = strLoadError;

				pnlDisplayHTML.Visible = false;
				pnlDisplayXML.Visible = false;
				pnlError.Visible = true;
			}
		}

		protected bool DisplayWebsite( string strURL )
		{
			bool fLoaded = false;
			pnlDisplayHTML.Visible = true;
			pnlDisplayXML.Visible = false;
			pnlError.Visible = false;

			string strIFrame = "<iframe src=\"{0}\" name=\"ifHTMLDisplay\" frameborder=\"0\" width=\"1200px\" height=\"900px\"></iframe>";
			strIFrame = String.Format(strIFrame, strURL);
			Literal litHTMLDisplay = new Literal();
			litHTMLDisplay.Text = strIFrame;
			pnlDisplayHTML.Controls.Add(litHTMLDisplay);
			fLoaded = true;

			return fLoaded;
		}

		protected void OnCheckMakeDefaultPage(object sender, EventArgs e)
		{
			UserAccount acct = GetActiveUser();
			if (null != acct)
			{
				if (chkMakeDefaultPage.Checked)
				{
					acct.DefaultPage = "sportsnews";
				}
				else
				{
					acct.DefaultPage = "friends";
				}
				acct.Update();
			}

		}

	}
}