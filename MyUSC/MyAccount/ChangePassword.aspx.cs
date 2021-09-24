using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MyUSC.Classes;

namespace MyUSC
{
	public partial class ChangePassword : USCPageBase
	{
		protected ChangePassword()
		{
			_PAGENAME = "ChangePassword";
			_pgUSCMaster = (USCMaster)Master;
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			// One time settings
			if (!IsPostBack)
			{
				IsUserLoggedIn(true);
				Master.SelectMenuItem(SelectedPage.Profile);
				chkShowPswd.Visible = false;
			}
		}

		private bool ValidatePassword()
		{
			bool fValid = true;

			if( txtPassword.Text.Length > 7 || txtConfirm.Text.Length > 7 )
			{
				if (txtPassword.Text != txtConfirm.Text)
				{
					lblError.Text = "Your password and confirmation do not match.  Please re-enter them.";
					SetFocus(txtPassword);
					fValid = false;
				}
				else
				{
					if (!USCBase.IsPasswordString(txtPassword.Text))
					{
						lblError.Text = "Your password can contain only letters, numbers and certain special characters (!@#$%^&*).  Please try again.";
						SetFocus(txtPassword);
						fValid = false;
					}
				}
			}
			else
			{
				lblError.Text = "Your password must be at least 8 characters long.  Please try again.";
				SetFocus(txtPassword);
				fValid = false;
			}
			return fValid;
		}

		protected void LeavePage()
		{
			Response.Redirect("/MyAccount/MyAccount.aspx");
		}

		protected void OnClickUpdate(object sender, ImageClickEventArgs e)
		{
			UserAccount acct = GetActiveUser();

			acct.Password = txtPassword.Text;
			acct.UpdatePassword();
			SetSessionMessage("Password changed successfully!");
			LeavePage();
		}

		protected void OnClickCancel(object sender, ImageClickEventArgs e)
		{
			LeavePage();
		}

		protected void OnChangedShowPswd(object sender, EventArgs e)
		{
			TextBoxMode tm = TextBoxMode.Password;

			if (chkShowPswd.Checked)
			{
				tm = TextBoxMode.SingleLine;
			}

			txtPassword.TextMode = tm;
			txtConfirm.TextMode = tm;
		}
	}
}