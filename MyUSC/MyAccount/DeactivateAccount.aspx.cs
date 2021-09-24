using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MyUSC.Classes;

namespace MyUSC.MyAccount
{
	public partial class DeactivateAccount : USCPageBase
	{
		protected DeactivateAccount()
		{
			_PAGENAME = "DeactivateAccount";
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
				lblConfirmHead.Text = "Please confirm that you want to deactivate your account";

				StringBuilder sb = new StringBuilder();
				sb.Append("If you deactivate your account, you will not be able to reactivate it without contacting support.  ");
				sb.Append("Please confirm that you wish to deactivate your account at this time.");
				lblConfirmText.Text = sb.ToString();
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
		protected void OnClickOK(object sender, ImageClickEventArgs e)
		{
			// Get the users account
			UserAccount acct = GetActiveUser();

			// Set the values
			if (null == acct)
			{
				acct.UserType = UserAccount.UserTypes.Inactive;
				acct.UpdateUserType();

				EmailUtil email = new EmailUtil( Master.g_SiteAdmin );

				StringBuilder sb = new StringBuilder();
				sb.Append("Hello ").Append(acct.DisplayName()).Append(",<br><br><br>");
				sb.Append("We recieved a request to deactivate your MySportsConnect.net account.  If you requested the account deactivation, please disregard this email.  We are sorry to see you go.<br><br><br>");
				sb.Append("If you didn't request your account to be deactivated, please contact support immediately and let us know.<br><br><br>");
				sb.Append("http://www.mysportsconnect.net/support.aspx").Append("<br><br><br>");
				sb.Append("Thank you<br>");
				sb.Append("MySportsConnect Staff");

				email.SendInfoMail( acct.Email, "Account deactivated", sb.ToString() );

				Master.Logout(true);
			}
		}

		protected void OnClickCancel(object sender, ImageClickEventArgs e)
		{
			Response.Redirect("/MyAccount/MyAccount.aspx");
		}
#endregion
	}
}