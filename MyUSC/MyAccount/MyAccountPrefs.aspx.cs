using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MyUSC.Classes;

namespace MyUSC.MyAccount
{
	public partial class MyAccountPrefs : USCPageBase
	{
		protected MyAccountPrefs()
		{
			_PAGENAME = "AccountPrefs";
			_pgUSCMaster = (USCMaster)Master;
		}

#region SetDataValues
		private void SetDataValues()
		{
			// Get the users account
			UserAccount acct = GetActiveUser();

			// Set the values
			if (null == acct)
			{
				RedirectToLoginPage();
			}
			else
			{
				chkDisableDeleteFriends.Checked = acct.Preferences.DeleteFriendsWarning;
				chkDisableDeleteFriendsMsgs.Checked = acct.Preferences.DeleteMsgWarning;
				chkReceiveCommentEmails.Checked = acct.Preferences.SendCommentsEmail;
				chkOffersFromUs.Checked = acct.Preferences.OffersFromUs;
				chkOffersFromPartners.Checked = acct.Preferences.OffersFromPartners;
				ddlHomePage.SelectedValue = acct.DefaultPage;
			}
		}
#endregion

#region Page_Load
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				SetDataValues();
				Master.SelectMenuItem(SelectedPage.Profile);
			}
		}
#endregion

#region Button Clicks
		protected bool GetData(UserAccount acct)
		{
			bool fRet = true;

			acct.Preferences.DeleteFriendsWarning = chkDisableDeleteFriends.Checked;
			acct.Preferences.DeleteMsgWarning = chkDisableDeleteFriendsMsgs.Checked;
			acct.Preferences.SendCommentsEmail = chkReceiveCommentEmails.Checked;
			acct.Preferences.OffersFromUs = chkOffersFromUs.Checked;
			acct.Preferences.OffersFromPartners = chkOffersFromPartners.Checked;
			acct.DefaultPage = ddlHomePage.SelectedValue;

			return fRet;
		}

		protected void OnClickOK(object sender, ImageClickEventArgs e)
		{
			// Get the users account
			UserAccount acct = GetActiveUser();

			if (GetData(acct))
			{
				acct.Update();
				acct.UpdatePreferences();
				Response.Redirect("/MyAccount/MyAccount.aspx");
			}
		}

		protected void OnClickCancel(object sender, ImageClickEventArgs e)
		{
			Response.Redirect("/MyAccount/MyAccount.aspx");
		}
#endregion
	}
}