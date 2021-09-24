using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MyUSC.Classes;

namespace MyUSC.Verify
{
	public partial class VerifyEmail : USCPageBase
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			string strUserID = Request.QueryString["key"];
			StringBuilder sb = new StringBuilder();
			if (null != strUserID && strUserID.Length > 0)
			{
				long lID = Convert.ToInt64(strUserID);
				UserAccount acct = Master.g_AccountsList.GetAccountByKey(lID);
				if( null != acct )
				{
					acct.ConnectionString = Master.g_strConnectionString;
					if( acct.UpdateUserVerified() )
					{
						sb.Append("Congratulations!  Your email address was successfully verified.<br><br>");
						sb.Append("We recommend visiting the Profile page and update yours.  It will help others find you and let you tailor some aspects of your site experience.<br><br>");
						sb.Append("Thank you for joining MySportsConnect.net.  We hope you enjoy your time with us.<br><br>");
					}
					else
					{
						sb.Append( "There was an error updating your account.  Please try reopening the link from the email you received.  If the problem persists, please contact support." );
					}
				}
				else
				{
					sb.Append("Unable to find the specified account.  Please try reopening the link from the email you received.  If the problem persists, please contact support.");
				}
			}
			else
			{
				sb.Append("The URL you used is invalid.  Please try reopening the link from the email you received.  If the problem persists, please contact support.");
			}

			litAccountSuccess.Text = sb.ToString();

			Master.SelectMenuItem(SelectedPage.Profile);
		}

		protected void OnClickOK(object sender, ImageClickEventArgs e)
		{
			RedirectToLoginPage();
		}
	}
}