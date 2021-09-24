using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MyUSC.Classes;

namespace MyUSC.Admin.Super
{
	public partial class NewsMenu : USCPageBase
	{
		protected NewsMenu()
		{
			_PAGENAME = "NewsMenu";
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

				SetDataValues( lKey );
			}
			Master.EnableAdminMenu();
			Master.SelectMenuItem(SelectedPage.Admin);
		}
#endregion

#region PopulateDropDownLists
		void PopulateDropDownLists()
		{
			ddlLevelItems.Items.Clear();
			ListItem li = new ListItem("None", "0");
			ddlLevelItems.Items.Add(li);

			foreach (DictionaryEntry de in Master.g_NewsMenuList.htNewsMenu)
			{
				NewsMenuItem menuItem = (NewsMenuItem)de.Value;
				string strDisplay = menuItem.Key.ToString() + " - " + menuItem.Name;

				ListItem menuListItem = new ListItem(strDisplay, menuItem.Key.ToString());
				ddlLevelItems.Items.Insert(1, menuListItem);
			}

			ddlParent.Items.Clear();
			ListItem liParent = new ListItem("Root item", "0");
			ddlParent.Items.Add(liParent);

			foreach (DictionaryEntry de in Master.g_NewsMenuList.htNewsMenu)
			{
				NewsMenuItem menuItem = (NewsMenuItem)de.Value;
				if( menuItem.MenuDepth < 4 )
				{
					string strDisplay = menuItem.Key.ToString() + " - " + menuItem.Name;

					ListItem menuListItem = new ListItem(strDisplay, menuItem.Key.ToString());
					ddlParent.Items.Insert(1, menuListItem);
				}
			}

			ddlRSSFeeds.Items.Clear();
			ListItem liNoFeed = new ListItem("None", "0");
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
			lblKey.Text = "-";
			txtDisplayName.Text = "";
			txtDescription.Text = "";
			txtLogoURL.Text = "";
			txtSequence.Text = "1";
			txtWebsite.Text = "";
			txtNotes.Text = "";
			chkIsActive.Checked = true;
			lblLastUpdate.Text = "";
			lblMenuDepth.Text = "-";
		}

		private void SetDataValues( long lKey )
		{
			SetInitialValues();
			if( 0 < lKey )
			{
				NewsMenuItem item = Master.g_NewsMenuList.GetNewsMenuItem(lKey);
				lblKey.Text = item.Key.ToString();
				txtDisplayName.Text = item.Name;
				txtDescription.Text = item.Description;
				txtLogoURL.Text = item.LogoUrl;
				txtSequence.Text = item.Sequence.ToString();
				txtWebsite.Text = item.Website;
				txtNotes.Text = item.Notes;
				chkIsActive.Checked = item.Active;
				lblLastUpdate.Text = item.LastUpdate;
				ddlRSSFeeds.SelectedValue = item.RSSID.ToString();
				ddlParent.SelectedValue = item.ParentID.ToString();
				lblMenuDepth.Text = item.MenuDepth.ToString();
				ddlTarget.SelectedValue = item.Target;
			}
		}
#endregion

#region Button Clicks
		bool GetDataValues(NewsMenuItem newsMenuItem, bool fNew)
		{
			bool fRet = true;
			lblSequenceError.Text = "";

			newsMenuItem.ParentID = 0;
			newsMenuItem.MenuDepth = 1;

			string parentSel = ddlParent.SelectedValue;
			long lParentKey = Convert.ToInt64(parentSel);
			if (lParentKey > 0)
			{
				NewsMenuItem mi = Master.g_NewsMenuList.GetNewsMenuItem(lParentKey);
				if( null != mi )
				{
					newsMenuItem.ParentID = mi.Key;
					newsMenuItem.MenuDepth = mi.MenuDepth+1;
				}
			}

			if (fRet && txtDisplayName.Text.Length < 3)
			{
				lblDisplayNameError.Text = "Please enter a valid display name.";
				SetFocus( txtDisplayName );
				fRet = false;
			}
			else
			{
				newsMenuItem.Name = txtDisplayName.Text;
			}

			newsMenuItem.Description = txtDescription.Text;

			string rssSel = ddlRSSFeeds.SelectedValue;
			newsMenuItem.RSSID = Convert.ToInt64(rssSel);

			newsMenuItem.LogoUrl = txtLogoURL.Text;

			newsMenuItem.Sequence = 1;
			if (fRet && txtSequence.Text.Length > 0)
			{
				string strSequence = txtSequence.Text;
				if (!USCBase.IsDecimalString(strSequence))
				{
					SetFocus(txtSequence);
					lblSequenceError.Text = "Please enter a valid (number) sequence.";
					fRet = false;
				}
				else
				{
					newsMenuItem.Sequence = (float)Convert.ToDecimal(strSequence);
				}
			}

			newsMenuItem.Website = txtWebsite.Text;
			newsMenuItem.Notes = txtNotes.Text;
			newsMenuItem.Active = chkIsActive.Checked;
			newsMenuItem.Target = ddlTarget.SelectedValue;

			return fRet;
		}

		protected void OnClickSave(object sender, EventArgs e)
		{
			long lKey = Convert.ToInt64(hf1.Value);
			if( lKey > 0 )
			{
				NewsMenuItem newsMenuItem = Master.g_NewsMenuList.GetNewsMenuItem( lKey );
				if (GetDataValues(newsMenuItem, false))
				{
					string strUpdate = GetCookieUserName() + " -- " + DateTime.Now.ToShortDateString();
					newsMenuItem.LastUpdate = strUpdate;
					newsMenuItem.Update();
				}
			}
			else
			{
				NewsMenuItem newsMenuItem = new NewsMenuItem();
				if (GetDataValues(newsMenuItem, true))
				{
					Master.g_NewsMenuList.Add(newsMenuItem);

					string strDisplay = newsMenuItem.Key.ToString() + " - " + newsMenuItem.Name;
					ListItem rssListItem = new ListItem(strDisplay, newsMenuItem.Key.ToString());
					ddlLevelItems.Items.Add(rssListItem);
					ddlLevelItems.SelectedValue = newsMenuItem.Key.ToString();
				}
			}
		}
#endregion

#region events
		protected void OnSelChangeItems(object sender, EventArgs e)
		{
			string strSel = ddlLevelItems.SelectedValue;
			hf1.Value = strSel;
			long lKey = Convert.ToInt64(strSel);
			SetDataValues(lKey);
		}
#endregion

	}
}