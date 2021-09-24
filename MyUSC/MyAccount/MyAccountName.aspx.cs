using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MyUSC.Classes;

namespace MyUSC.MyAccount
{
	public partial class MyAccountName : USCPageBase
	{
		protected MyAccountName()
		{
			_PAGENAME = "AccountName";
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
				lblUsernameValue.Text = acct.UserName;
				txtTitle.Text = acct.Title;
				txtSuffix.Text = acct.Suffix;
				txtFirst.Text = acct.First;
				txtMI.Text = acct.MI;
				txtLast.Text = acct.Last;
				txtNickname.Text = acct.NickName;
				chkIncludeNickname.Checked = acct.Preferences.ShowNickname;
				txtEmail.Text = acct.Email;
				txtBirthDate.Text = acct.BirthDate;
				dsBirthDay.SetDate( acct.BirthDate );
				txtSecQuest.Text = acct.SecurityQuestion;
				txtSecAnswer.Text = acct.SecurityAnswer;
				chkProvide.Checked = acct.Preferences.ProvideSecurityQuestion;
				if( !acct.Preferences.ProvideSecurityQuestion )
				{
					ddlChooseSQ.SelectedValue = acct.SecurityQuestion;
				}
			}
		}
#endregion

#region Page_Load
		protected void FillDropDowns()
		{
			ddlChooseSQ.Items.Add(new ListItem("Mother's maiden name","Mother's maiden name"));
			ddlChooseSQ.Items.Add(new ListItem("First pet's name","First pet's name"));
			ddlChooseSQ.Items.Add(new ListItem("City where you were born","City where you were born"));
			ddlChooseSQ.Items.Add(new ListItem("Your high school mascot","Your high school mascot"));
			ddlChooseSQ.Items.Add(new ListItem("Father's middle name","Father's middle name"));
			ddlChooseSQ.Items.Add(new ListItem("Favorite TV show","Favorite TV show"));
			ddlChooseSQ.Items.Add(new ListItem("Exercise you hate the most", "Exercise you hate the most"));
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			if( !IsPostBack )
			{
				FillDropDowns();
				SetDataValues();
				Master.SelectMenuItem(SelectedPage.Profile);
			}
		}
#endregion

#region Button Clicks

#region Validate Sections
		bool ValidateNameSection()
		{
			bool fSectionValid = true;

			// SecurityQuestion and Answer
			if (fSectionValid && (txtSecQuest.Text.Length > 0 || txtSecAnswer.Text.Length > 0))
			{
				if (txtSecQuest.Text.Length == 0 || txtSecAnswer.Text.Length == 0)
				{
					Master.AlertUser("Please provide a security question and answer, so we can help if you forget your password.");
					SetFocus(txtSecQuest);
					fSectionValid = false;
				}
			}

			// Verify we have an email address
			if (fSectionValid)
			{
				int indexAt = txtEmail.Text.IndexOf('@');
				int indexPeriod = txtEmail.Text.IndexOf('.');
				if (0 > indexAt || 0 > indexPeriod)
				{
					Master.AlertUser("Please enter a valid email address.");
					SetFocus(txtEmail);
					fSectionValid = false;
				}

			}

			return fSectionValid;
		}

#endregion

		protected bool GetData( UserAccount acct )
		{
			bool fRet = true;

			acct.Title = txtTitle.Text;
			acct.Suffix = txtSuffix.Text;
			acct.First = txtFirst.Text;
			acct.MI = txtMI.Text;
			acct.Last = txtLast.Text;
			acct.NickName = txtNickname.Text;
			acct.Preferences.ShowNickname = chkIncludeNickname.Checked;
			acct.Email = txtEmail.Text;
			//acct.BirthDate = txtBirthDate.Text;
			acct.BirthDate = dsBirthDay.ToString();
			if( chkProvide.Checked )
			{
				acct.SecurityQuestion = txtSecQuest.Text;
			}
			else
			{
				ListItem li = ddlChooseSQ.SelectedItem;
				acct.SecurityQuestion = li.Text;
			}
			acct.SecurityAnswer = txtSecAnswer.Text;

			return fRet;
		}

		protected void OnClickSave(object sender, ImageClickEventArgs e)
		{
			if( ValidateNameSection() )
			{
				// Get the users account
				UserAccount acct = GetActiveUser();

				if(GetData( acct ))
				{
					acct.Update();
					acct.UpdatePreferences();
					Response.Redirect("/MyAccount/MyAccount.aspx");
				}
			}

		}

		protected void OnClickCancel(object sender, ImageClickEventArgs e)
		{
			Response.Redirect("/MyAccount/MyAccount.aspx");
		}
#endregion

		protected void OnClickChangePswd(object sender, ImageClickEventArgs e)
		{
			Response.Redirect("/MyAccount/ChangePassword.aspx");
		}
	}
}