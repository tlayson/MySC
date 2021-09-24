using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MyUSC.Classes;

namespace MyUSC.MyTeams
{
	public partial class PickUsername : USCPageBase
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				
			}

		}

		protected bool CheckUsername()
		{
			bool fRet = false;
			string strCheckName = txtUsername.Text.Trim();
			if (strCheckName.Length >= 5 && USCBase.IsAlphaNumericString( strCheckName ))
			{
				// The name is already in use.
				if( !Master.g_AccountsList.IsUserNameAvailable( strCheckName ) )
				{
					lblNameTaken.Visible = true;
					SetFocus( txtUsername );
				}
				else
				{
					lblNameAvailable.Visible = true;
					fRet = true;
				}
			}
			else
			{
				Master.AlertUser("Your user name must be at least 5 characters long and contain only letters and numbers.");
				SetFocus(txtUsername);
			}

			return fRet;
		}

		protected void OnClickCheckUsername(object sender, EventArgs e)
		{
			lblNameAvailable.Visible = false;
			lblNameTaken.Visible = false;
			CheckUsername();
		}

		protected void OnClickSuggest(object sender, EventArgs e)
		{
			string strSuggest = "";
			UserAccount acct = GetActiveUser();
			if( null != acct )
			{
				strSuggest = acct.First + acct.Last;
			}
			else
			{
				strSuggest = "";
			}

			strSuggest += acct.Key;

			txtUsername.Text = strSuggest;
		}

		protected bool CheckPassword()
		{
			bool fRet = true;

			if( txtPassword.Text.Length == 0 && txtConfirmPswd.Text.Length == 0 )
			{
				Master.AlertUser("You must provide a password and confirmation for your new account.");
				SetFocus(txtPassword);
			}
			else
			{
				// Confirm the passwords match and are valid
				if( txtPassword.Text != txtConfirmPswd.Text )
				{
					Master.AlertUser("Your password and confirmation do not match.  Please reenter them.");
					SetFocus(txtPassword);
				}
				else
				{
					if (!USCBase.IsPasswordString(txtPassword.Text))
					{
						Master.AlertUser("Your password can contain only letters, numbers and certain special characters (!@#$%^&*).  Please try again.");
						SetFocus(txtPassword);
					}
				}
			}

			return fRet;
		}

		protected void OnClickDone(object sender, EventArgs e)
		{
			if( CheckUsername() )
			{
				if( CheckPassword() )
				{
					UserAccount acct = GetActiveUser();
					if( null != acct )
					{
						acct.UserName = txtUsername.Text.Trim();
						acct.Password = txtPassword.Text;

						USCEncrypt usce = new USCEncrypt();
						acct.Password = usce.EncryptString(acct.Password);
					
						acct.Update();

						Response.Redirect( "~/MyTeams/Dashboard.aspx" );
					}
				}
			}
		}
	}
}