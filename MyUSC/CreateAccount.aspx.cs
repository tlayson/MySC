using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MyUSC.Classes;

namespace MyUSC
{

	public partial class CreateAccountPage : USCPageBase
	{
		protected CreateAccountPage()
		{
			_PAGENAME = "CreateAccount";
			_pgUSCMaster = (USCMaster)Master;
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			// One time settings
			if (!IsPostBack)
			{
				// Clear out any residual session variables
				Master.Logout( false );
				SetInitialValues();
				Master.SelectMenuItem(SelectedPage.Profile);
			}
			SetFocus( txtUserName );
		}

#region SetInitialValues
		private void SetInitialValues()
		{
			// Name panel
			txtUserName.Text = "";
			txtPassword.Text = "";
			txtPswdConfirm.Text = "";
			txtSecurityQuestion.Text = "";
			txtSecurityAnswer.Text = "";
			txtTitle.Text = "";
			txtFirstName.Text = "";
			txtMI.Text = "";
			txtLastName.Text = "";
			txtSuffix.Text = "";
			txtEmail.Text = "";
		}
#endregion

#region GetDataValues
		/// <summary>
		/// Get data from controls.
		/// 
		/// Assumes that the controls have been previously validated.
		/// </summary>
		private void GetDataValues( UserAccount acct )
		{
			// Set the values
			if (null != acct)
			{
				// Name panel
				if( acct.UserName.Length == 0 )
				{
					acct.UserName = txtUserName.Text;
				}
				
				if( txtPassword.Text.Length > 0 )
				{
					acct.Password = txtPassword.Text;
				}

				acct.SecurityQuestion = txtSecurityQuestion.Text;
				acct.SecurityAnswer = txtSecurityAnswer.Text;
				acct.Title = txtTitle.Text;
				acct.First = txtFirstName.Text;
				acct.MI = txtMI.Text;
				acct.Last = txtLastName.Text;
				acct.Suffix = txtSuffix.Text;
				acct.Email = txtEmail.Text;

				acct.BirthDate = dsBirthDay.ToString();

				acct.DefaultPage = "friends";
			}
		}

#endregion

#region Validate Sections
		bool ValidateNameSection()
		{
			bool fSectionValid = true;

			// If this is a new account, validate the username and existence of password
			string strTemp = txtUserName.Text;
			if (strTemp.Length >= 5 && USCBase.IsAlphaNumericString( strTemp ))
			{
				if( !Master.g_AccountsList.IsUserNameAvailable( strTemp ) )
				{
					Master.AlertUser("That user name is already in use.  Please try another one.");
					SetFocus( txtUserName );
					fSectionValid = false;
				}
			}
			else
			{
				Master.AlertUser("Your user name must be at least 5 characters long and contain only letters and numbers.");
				SetFocus(txtUserName);
				fSectionValid = false;
			}

			if( fSectionValid && !(txtPassword.Text.Length > 0 && txtPswdConfirm.Text.Length > 0))
			{
				Master.AlertUser("You must provide a password and confirmation for new accounts.");
				SetFocus(txtPassword);
				fSectionValid = false;
			}

			// Confirm the passwords match and are valid
			if (fSectionValid && (txtPassword.Text.Length > 0 || txtPswdConfirm.Text.Length > 0))
			{
				if( txtPassword.Text != txtPswdConfirm.Text )
				{
					Master.AlertUser("Your password and confirmation do not match.  Please reenter them.");
					SetFocus(txtPassword);
					fSectionValid = false;
				}
				else
				{
					if (!USCBase.IsPasswordString(txtPassword.Text))
					{
						Master.AlertUser("Your password can contain only letters, numbers and certain special characters (!@#$%^&*).  Please try again.");
						SetFocus(txtPassword);
						fSectionValid = false;
					}
				}
			}

			// Verify first name
			if (fSectionValid)
			{
				if( fSectionValid && txtFirstName.Text.Length == 0 )
				{
					Master.AlertUser("Please enter your first name.");
					SetFocus( txtFirstName );
					fSectionValid = false;
				}
			}

			// Verify we have an email address
			if( fSectionValid )
			{
				int indexAt = txtEmail.Text.IndexOf('@');
				int indexPeriod = txtEmail.Text.IndexOf('.');
				if( 0 > indexAt || 0 > indexPeriod )
				{
					Master.AlertUser("Please enter a valid email address.");
					SetFocus(txtEmail);
					fSectionValid = false;
				}
			}

			// Verify age certification
			if (fSectionValid)
			{
				if( chkCertifyAge.Checked != true )
				{
					Master.AlertUser("Please verify that you are at least 14 years old.");
					SetFocus(chkCertifyAge);
					fSectionValid = false;
				}
			}

			// Verify birth date
			if (fSectionValid)
			{
				if( dsBirthDay.Month.Length <= 0 || dsBirthDay.Day.Length <= 0 || dsBirthDay.Year.Length <= 0 )
				{
					fSectionValid = false;
					SetFocus( dsBirthDay );
					Master.AlertUser( "Please select a valid birth date." );
				}
			}

			// SecurityQuestion and Answer
			if (fSectionValid && (txtSecurityQuestion.Text.Length == 0 || txtSecurityAnswer.Text.Length == 0))
			{
				if (txtSecurityQuestion.Text.Length == 0 || txtSecurityAnswer.Text.Length == 0)
				{
					Master.AlertUser("Please provide a security question and answer, so we can help if you forget your password.");
					SetFocus(txtSecurityQuestion);
					fSectionValid = false;
				}
			}

			return fSectionValid;
		}

		bool ValidateAllSections()
		{
			bool fSectionsValid = true;
			// Only section we are currently validating
			if (!ValidateNameSection())
			{
				fSectionsValid = false;
			}
			return fSectionsValid;
		}
#endregion

#region Create
		protected void SendVerifyEmail( UserAccount acct )
		{
			StringBuilder sb = new StringBuilder();

			sb.Append( "Congratulations {0} {1}!  Welcome to MySportsConnect.net.  In order to complete your registration, we need to verify the email address you used to register.  " );
			sb.Append("This will allow us to ensure we can contact you if there is an issue with your account.  It will also enable you to use features that will be coming in the future.");
			sb.Append("<br><br>");
			sb.Append("To verify your email address, go to <a href=\"http://www.mysportsconnect.net/Verify/VerifyEmail.aspx?key={2}\">verify your account</a>.");
			sb.Append("<br><br>");
			sb.Append("MySportsConnect.net staff");
			sb.Append("<br><br>");

			string strEmailBody = String.Format(sb.ToString(), acct.First, acct.Last, acct.Key);

			EmailUtil email = new EmailUtil( Master.g_SiteAdmin );
			email.SendInfoMail( acct.Email, "Please verify your email address", strEmailBody );
		}

		protected void OnClickCreateAccount(object sender, ImageClickEventArgs e)
		{
			if( ValidateAllSections() )
			{
				// Get the users account
				UserAccount acct = new UserAccount();
				GetDataValues( acct );
				AccountsList al = Master.g_AccountsList;
				//if( al.Add( acct ) )
				if( al.AddUser( acct ) )
				{
					SendVerifyEmail( acct );
					Response.Redirect("/Verify/AccountSuccess.aspx", true);
				}
				else
				{
					Master.AlertUser("There was a problem creating the account.  Please try again or go to the support page.");
				}
			}
		}

#endregion
	}
}