using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MyUSC.Classes;

namespace MyUSC
{
	public partial class ForgotPswd : USCPageBase
	{
		protected ForgotPswd()
		{
			_PAGENAME = "ForgotPswd";
			_pgUSCMaster = (USCMaster)Master;
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			pnlSecurityError.Visible = false;
			pnlEmailFailure.Visible = false;
		}

		protected void OnClickCheckUserName(object sender, EventArgs e)
		{
			pnlSecurityError.Visible = false;
			pnlEmailFailure.Visible = false;

			string strUser = txtUserName.Text;
			if( strUser.Length == 0 )
			{
				Master.AlertUser("Please enter a user name.");
				SetFocus(txtUserName);
				return;
			}
			else
			{
				UserAccount acct = Master.g_AccountsList.GetAccountByUserName( strUser );
				if (null == acct)
				{
					Master.AlertUser("No account exists with that user name.");
					SetFocus(txtUserName);
					return;
				}
				else
				{
					lblSecurityQuestion.Text = acct.SecurityQuestion;
				}
			}
		}

		private string BuildPswdEmail( string strTempPswd )
		{
			StringBuilder sb = new StringBuilder();

			sb.AppendLine("Hello,").AppendLine("");

			sb.AppendLine("Here is your new temporary password.  Your password was changed at your request.  If this is not the case, please contact support immediately.").AppendLine("<br><br>");
			sb.AppendLine("Please go to http://www.mysportsconnect.net and login with your temporary password.  Once you have successfully logged in, go to the accounts page and reset your password.").AppendLine("<br><br>");
			sb.Append("Temporary password : ").AppendLine(strTempPswd).AppendLine("<br><br>");
			sb.AppendLine("Thank you<br>").AppendLine("MySportsConnect.net support").AppendLine("<br><br>");
			sb.AppendLine("NOTE: Please do not reply to this email as this account is not monitored.");

			return sb.ToString();
		}

		protected void OnClickSubmit(object sender, ImageClickEventArgs e)
		{
			pnlSecurityError.Visible = false;
			pnlEmailFailure.Visible = false;
			string strUser = txtUserName.Text;
			string strSecurityAnswer = txtSecurityAnswer.Text.ToLower();
			UserAccount acct = Master.g_AccountsList.GetAccountByUserName(strUser);
			if (null == acct)
			{
				Master.AlertUser("No account exists with that user name.");
				SetFocus(txtUserName);
				return;
			}
			else
			{
				string strUserAnswer = acct.SecurityAnswer.ToLower();
				if( strUserAnswer == strSecurityAnswer )
				{
					// Send email with reset password and redirect.
					string strNewPassword = Master.GetSiteSetting( SiteAdmin.saKeyLostPassword, "Password For Lost Password Email" );
					if( null != strNewPassword && strNewPassword.Length > 0 )
					{
						EmailUtil email = new EmailUtil( Master.g_SiteAdmin );
						string strEmailBody = BuildPswdEmail(strNewPassword);

						if( !email.SendInfoMail( acct.Email, "Forgotten password", strEmailBody ))
						{
							pnlEmailFailure.Visible = true;
							return;
						}

						acct.Password = strNewPassword;
						acct.UpdatePassword();
						Master.AlertUser( "Your password has been changed.  Look for an email shortly in your registered email account." );
						Response.Redirect( "~/MyAccount/PswdSent.aspx" );
					}
				}
				else
				{
					pnlSecurityError.Visible = true;
					SetFocus(txtSecurityAnswer);
				}
			}
		}

		protected void OnClickCancel(object sender, ImageClickEventArgs e)
		{
			RedirectToLoginPage();
		}
	}
}