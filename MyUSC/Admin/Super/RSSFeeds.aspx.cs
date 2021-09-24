using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Design;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using System.Xml;
using System.Xml.XPath;
using MyUSC.Classes;

namespace MyUSC.Admin.Super
{
#region Thumbnail
	public class WebsiteThumbnailImage
	{
		Bitmap m_Bitmap;
		string m_Url;
		int m_BrowserWidth;
		int m_BrowserHeight;
		int m_ThumbnailWidth;
		int m_ThumbnailHeight;

		public WebsiteThumbnailImage(string Url, int BrowserWidth, int BrowserHeight, int ThumbnailWidth, int ThumbnailHeight )
		{
			m_Bitmap = null;
			m_Url = Url;
			m_BrowserWidth = BrowserWidth;
			m_BrowserHeight = BrowserHeight;
			m_ThumbnailWidth = ThumbnailWidth;
			m_ThumbnailHeight = ThumbnailHeight;
		}

		public Bitmap GenerateWebSiteThumbnailImage()
		{
			Thread m_thread = new Thread(new ThreadStart(_GenerateWebSiteThumbnailImage));
			m_thread.SetApartmentState(ApartmentState.STA);
			m_thread.Start();
			m_thread.Join();
			return m_Bitmap;
		}

		private void _GenerateWebSiteThumbnailImage()
		{
		  WebBrowser m_WebBrowser = new WebBrowser();
		  m_WebBrowser.ScrollBarsEnabled = false;
		  m_WebBrowser.Navigate(m_Url);

		  m_WebBrowser.ClientSize = new Size(this.m_BrowserWidth, this.m_BrowserHeight);
		  m_WebBrowser.ScrollBarsEnabled = false;
		  m_Bitmap = new Bitmap(m_WebBrowser.Bounds.Width, m_WebBrowser.Bounds.Height);
		  m_WebBrowser.BringToFront();
		  m_WebBrowser.DrawToBitmap(m_Bitmap, m_WebBrowser.Bounds);
		  m_Bitmap = (Bitmap)m_Bitmap.GetThumbnailImage(m_ThumbnailWidth, m_ThumbnailHeight, null, IntPtr.Zero);

		  //m_WebBrowser.DocumentCompleted += new WebBrowserDocument_CompletedEventHandler(WebBrowser_DocumentCompleted);
		  //while (m_WebBrowser.ReadyState != WebBrowserReadyState.Complete)
		  //	Application.DoEvents();
		  m_WebBrowser.Dispose();
		}

/*
		private void WebBrowser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
		{
			WebBrowser m_WebBrowser = (WebBrowser)sender;
			m_WebBrowser.ClientSize = new Size(this.m_BrowserWidth, this.m_BrowserHeight);
			m_WebBrowser.ScrollBarsEnabled = false;
			m_Bitmap = new Bitmap(m_WebBrowser.Bounds.Width, m_WebBrowser.Bounds.Height);
			m_WebBrowser.BringToFront();
			m_WebBrowser.DrawToBitmap(m_Bitmap, m_WebBrowser.Bounds);
			m_Bitmap = (Bitmap)m_Bitmap.GetThumbnailImage(m_ThumbnailWidth, m_ThumbnailHeight, null, IntPtr.Zero);
		}
*/
  }
#endregion

	public partial class RSSFeeds : USCPageBase
	{
		protected RSSFeeds()
		{
			_PAGENAME = "RSSFeeds";
			_pgUSCMaster = (USCMaster)Master;
		}

#region PAGE_LOAD
		protected void Page_Load(object sender, EventArgs e)
		{
			// Make sure the user should be here
			if (!IsSuperUser())
			{
				Master.Logout(true);
			}

			long lKey = 0;
			string strKey = Request.QueryString["Key"];
			if (null != strKey && strKey.Length > 0)
			{
				lKey = Convert.ToInt64(strKey);
				hf1.Value = strKey;
			}

			// One time settings
			if (!IsPostBack)
			{
				PopulateDropDownLists();

				SetDataValues(lKey);
			}
			Master.EnableAdminMenu();
			Master.SelectMenuItem(SelectedPage.Admin);
		}
#endregion

#region PopulateDropDownLists
		void PopulateDropDownLists()
		{
			ddlRSSFeeds.Items.Clear();

			ListItem li = new ListItem( "New", "0" );
			ddlRSSFeeds.Items.Add(li);

			foreach (DictionaryEntry de in Master.g_RSSFeedList.htRSSFeed)
			{
				RSSFeed rssFeed = (RSSFeed)de.Value;
				string strDisplay = rssFeed.Key.ToString() + " - " + rssFeed.Name;

				ListItem rssListItem = new ListItem(strDisplay, rssFeed.Key.ToString());
				ddlRSSFeeds.Items.Insert(1, rssListItem);
			}
		}
#endregion

#region SetValues
		private void SetInitialValues()
		{
			lblDisplayNameError.Text = "";
			txtDisplayName.Text = "";
			txtDescription.Text = "";
			txtURL.Text = "";
			txtWebsite.Text = "";
			txtNotes.Text = "";
			chkUseWebsite.Checked = false;
		}

		private void SetDataValues(long lKey)
		{
			SetInitialValues();
			if (0 < lKey)
			{
				RSSFeed rssFeed = Master.g_RSSFeedList.GetRSSFeed( lKey );
				lblKey.Text = rssFeed.Key.ToString();
				txtDisplayName.Text = rssFeed.Name;
				txtDescription.Text = rssFeed.Description;
				txtURL.Text = rssFeed.Url;
				txtWebsite.Text = rssFeed.Website;
				txtNotes.Text = rssFeed.Notes;
				chkUseWebsite.Checked = rssFeed.UseWebsite;
			}

			//ddlRSSFeeds.SelectedValue = lKey.ToString();
		}
#endregion

#region Button Clicks
		bool GetDataValues(RSSFeed rssFeed, bool fNew)
		{
			bool fRet = true;
			lblDisplayNameError.Text = "";

			if (txtDisplayName.Text.Length < 3)
			{
				lblDisplayNameError.Text = "Please enter a valid display name.";
				SetFocus(txtDisplayName);
				fRet = false;
			}
			else
			{
				rssFeed.Name = txtDisplayName.Text;
			}

			rssFeed.Description = txtDescription.Text;
			rssFeed.Url = txtURL.Text;
			rssFeed.Website = txtWebsite.Text;
			rssFeed.Notes = txtNotes.Text;
			rssFeed.UseWebsite = chkUseWebsite.Checked;

			return fRet;
		}

		protected void OnClickSave(object sender, EventArgs e)
		{
			long lKey = Convert.ToInt64(hf1.Value);
			if (lKey > 0)
			{
				RSSFeed rssFeed = Master.g_RSSFeedList.GetRSSFeed(lKey);
				if (GetDataValues(rssFeed, false))
				{
					string strUpdate = GetCookieUserName() + " -- " + DateTime.Now.ToShortDateString();
					rssFeed.LastUpdate = strUpdate;
					rssFeed.Update();
				}
			}
			else
			{
				RSSFeed rssFeed = new RSSFeed();
				if (GetDataValues(rssFeed, true))
				{
					Master.g_RSSFeedList.Add(rssFeed);

					string strDisplay = rssFeed.Key.ToString() + " - " + rssFeed.Name;
					ListItem rssListItem = new ListItem(strDisplay, rssFeed.Key.ToString());
					ddlRSSFeeds.Items.Add(rssListItem);
					ddlRSSFeeds.SelectedValue = rssFeed.Key.ToString();
				}
			}
		}

		protected void OnSelChangeSelect(object sender, EventArgs e)
		{
			string strSel = ddlRSSFeeds.SelectedValue;
			hf1.Value = strSel;
			long lKey = Convert.ToInt64(strSel);
			SetDataValues(lKey);
		}

		protected bool LoadFeed( RSSFeed rssFeed )
        {
			StringBuilder sbRSSDetails = new StringBuilder();
            try
            {
				sbRSSDetails.AppendLine().Append("RSS Key = ").Append(rssFeed.Key.ToString()).AppendLine();
				sbRSSDetails.Append("RSS Name = ").Append(rssFeed.Name).AppendLine();
				sbRSSDetails.Append("RSS URL = ").Append(rssFeed.Url).AppendLine();
				
                // Need to figure out how to load from XPath
                //XPathDocument xdoc = new XPathDocument( strURL );
                XmlDocument xmlRSSDocument = new XmlDocument();
				xmlRSSDocument.Load( rssFeed.Url );
            }
            catch (Exception ex)
            {
				EvtLog.WriteRSSException("RSSVerify load failure", sbRSSDetails.ToString(), ex, EventErrors.ErrorType.RSSFeed);
                return false;
            }

            return true;
        }

		protected void AddRSSErrorHeader(Table tbl)
		{
			TableRow tr = new TableRow();
			TableCell td1 = new TableCell();
			TableCell td2 = new TableCell();
			TableCell td3 = new TableCell();

			td1.Text = "RSS Key";
			td1.CssClass = "tdInput";
			td2.Text = "Name";
			td1.CssClass = "tdInput";
			td3.Text = "URL";
			td1.CssClass = "tdInput";

			tr.Controls.Add( td1 );
			tr.Controls.Add( td2 );
			tr.Controls.Add( td3 );

			tbl.Controls.Add( tr );
		}

		protected void AddRSSError(Table tbl, RSSFeed rssFeed)
		{
			TableRow tr = new TableRow();

			TableCell td1 = new TableCell();
			TableCell td2 = new TableCell();
			TableCell td3 = new TableCell();

			td1.Text = rssFeed.Key.ToString();
			td1.CssClass = "tdInput";
			td2.Text = rssFeed.Name;
			td1.CssClass = "tdInput";
			td3.Text = rssFeed.Url;
			td1.CssClass = "tdInput";

			tr.Controls.Add(td1);
			tr.Controls.Add(td2);
			tr.Controls.Add(td3);

			tbl.Controls.Add(tr);
		}

		protected void OnClickVerifyRSSFeeds(object sender, EventArgs e)
        {
			int nErrorCount = 0;
			int nFeedCount = 0;

			tblRSSErrors.Controls.Clear();
			AddRSSErrorHeader( tblRSSErrors );
			StringBuilder sbRSSDetails = new StringBuilder();

			EvtLog.WriteEvent("Beginning RSS feed verification.", System.Diagnostics.EventLogEntryType.Information, 2500, 0);

			foreach( DictionaryEntry de in Master.g_RSSFeedList.htRSSFeed )
			{
				RSSFeed rssFeed = (RSSFeed)de.Value;
				nFeedCount++;
				if( !rssFeed.UseWebsite )
				{
					bool fSuccess = LoadFeed(rssFeed);
					if (!fSuccess)
					{
						nErrorCount++;
						AddRSSError(tblRSSErrors, rssFeed);

						sbRSSDetails.Append(rssFeed.Key.ToString());
						sbRSSDetails.Append(",").Append(rssFeed.Name);
						sbRSSDetails.Append(",").Append(rssFeed.Url).AppendLine();

						if( nErrorCount >= 50 )
						{
							break;
						}
					}
				}
			}

			RSSFailureNotifications( sbRSSDetails.ToString() );

			string strComplete = "Completed RSS feed verification. Total feeds verified = " + nFeedCount + " with " + nErrorCount + " errors.";
			EvtLog.WriteEvent(strComplete, System.Diagnostics.EventLogEntryType.Information, 2500, 1);
		}

		protected bool DisplayWebsite(string strURL)
		{
			bool fLoaded = false;
			/*
			pnlDisplayHTML.Visible = true;
			pnlDisplayXML.Visible = false;
			pnlError.Visible = false;
			*/
			string strIFrame = "<iframe src=\"{0}\" name=\"ifHTMLDisplay\" frameborder=\"0\" width=\"1200px\" height=\"900px\"></iframe>";
			strIFrame = String.Format(strIFrame, strURL);
			Literal litHTMLDisplay = new Literal();
			litHTMLDisplay.Text = strIFrame;
			//pnlDisplayHTML.Controls.Add(litHTMLDisplay);
			fLoaded = true;

			return fLoaded;
		}

		public static Bitmap GetWebSiteThumbnail(string Url, int BrowserWidth, int BrowserHeight, int ThumbnailWidth, int ThumbnailHeight)
		{
			WebsiteThumbnailImage thumbnailGenerator = new WebsiteThumbnailImage(Url, BrowserWidth, BrowserHeight, ThumbnailWidth, ThumbnailHeight);
			return thumbnailGenerator.GenerateWebSiteThumbnailImage();
		}

		// Navigates to the given URL if it is valid.
		private void Navigate(String address)
		{
			if (String.IsNullOrEmpty(address)) return;
			if (address.Equals("about:blank")) return;
			if (!address.StartsWith("http://") &&
				!address.StartsWith("https://"))
			{
				address = "http://" + address;
			}
			try
			{
				// Create a WebBrowser instance. 
				WebBrowser webBrowser1 = new WebBrowser();

				webBrowser1.Navigate(new Uri(address), true);
			}
			catch (System.UriFormatException)
			{
				return;
			}
		}

		protected void OnClickTestNews(object sender, EventArgs e)
		{
			string strSel = ddlRSSFeeds.SelectedValue;
			hf1.Value = strSel;
			long lKey = Convert.ToInt64(strSel);
			RSSFeed rssFeed = Master.g_RSSFeedList.GetRSSFeed(lKey);
			if (GetDataValues(rssFeed, false))
			{
				if (rssFeed.UseWebsite)
				{
					WebsiteThumbnailImage wti = new WebsiteThumbnailImage(rssFeed.Website, 800, 400, 800, 400 );
					Bitmap bmp = wti.GenerateWebSiteThumbnailImage();
					//Write to temp location to display.
					//imgDisplay.ImageUrl = 
					//Navigate( rssFeed.Website );
				}
				else
				{ 
					
				}
			}

		}

		protected void RSSFailureNotifications(string strRSSFailures)
		{
			string strFilePath = "c:\\TBSC\\Web\\RSSLoadFailures.csv";
			StreamWriter writer = new StreamWriter( strFilePath );
			writer.WriteLine("Key,Name,URL");
			writer.WriteLine(strRSSFailures);
			writer.Flush();
			writer.Close();

			//EmailUtil email = new EmailUtil(Master.g_SiteAdmin);
			//email.SendInfoMail("tlayson@hotmail.com", "RSS Load failures", strRSSFailures);

		}
#endregion

	}
}