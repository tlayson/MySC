using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MyUSC.Classes;
using CuteEditor;
using CuteEditor.ImageEditor;

namespace MyUSC
{
	public partial class Support : USCPageBase
	{
		protected Support()
		{
			_PAGENAME = "Support";
			_pgUSCMaster = (USCMaster)Master;
		}

		void SetInitialValues()
		{
			if( IsUserLoggedIn( false ) )
			{
				UserAccount acct = Master.GetActiveUser();
				if (null != acct)
				{
					txtFirstName.Text = acct.First;
					txtLastName.Text = acct.Last;
					txtEmail.Text = acct.Email;
					btnLogin.Visible = false;
				}
			}
			else
			{
				btnLogin.Visible = true;
			}
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			// One time settings
			if (!IsPostBack)
			{
				Master.SelectMenuItem(SelectedPage.Help);
				SetInitialValues();
			}
		}

		bool ValidateData()
		{
			bool fRet = true;
			// First Name
			if( txtFirstName.Text.Length == 0 )
			{
				Master.AlertUser("Please enter a first name.");
				SetFocus( txtFirstName );
				fRet = false;
			}

			// Last Name
			if (fRet && txtLastName.Text.Length == 0)
			{
				Master.AlertUser("Please enter a last name.");
				SetFocus(txtLastName);
				fRet = false;
			}

			// Email
			if (fRet)
			{
				int indexAt = txtEmail.Text.IndexOf('@');
				int indexPeriod = txtEmail.Text.IndexOf('.');
				if (0 > indexAt || 0 > indexPeriod)
				{
					Master.AlertUser("Please enter a valid email address.");
					SetFocus(txtEmail);
					fRet = false;
				}
			}

			// Description
			if (fRet && txtDescription.Text.Length == 0)
			{
				Master.AlertUser("Please enter a description.");
				SetFocus(txtDescription);
				fRet = false;
			}

			// Details
			if (fRet && txtDetails.Text.Length == 0)
			{
				Master.AlertUser("Please enter some details.");
				SetFocus(txtDetails);
				fRet = false;
			}

			return fRet;
		}

		bool GetData( SupportRequest sptReq )
		{
			bool fRet = true;
			sptReq.FirstName = txtFirstName.Text;
			sptReq.LastName = txtLastName.Text;
			sptReq.Email = txtEmail.Text;
			sptReq.Phone = txtPhone.Text;
			sptReq.Browser = txtBrowser.Text;
			sptReq.Description = txtDescription.Text;
			sptReq.Details = txtDetails.Text;
			return fRet;
		}

		protected void OnClickSubmit(object sender, ImageClickEventArgs e)
		{
			SupportRequest sptReq = new SupportRequest();
			sptReq.ConnectionString = Master.g_strConnectionString;

			if( ValidateData() )
			{
				if (IsUserLoggedIn(false))
				{
					UserAccount acct = Master.GetActiveUser();
					if (null != acct)
					{
						sptReq.AccountID = acct.Key;
					}
				}
				if (GetData(sptReq))
				{
					// Send message
					EmailUtil email = new EmailUtil( Master.g_SiteAdmin );
					StringBuilder sbDetails = new StringBuilder();
					sbDetails.AppendLine("From :" + sptReq.FirstName + " " + sptReq.LastName ).AppendLine("");
					sbDetails.AppendLine("Email :" + sptReq.Email).AppendLine("");
					sbDetails.AppendLine("Phone :" + sptReq.Phone).AppendLine("");
					sbDetails.AppendLine("Browser :" + sptReq.Browser).AppendLine("");
					sbDetails.AppendLine("Details :" + sptReq.Details).AppendLine("");

					email.SendSupportMail(sptReq.Email, email.SupportAccount, sptReq.Description, sbDetails.ToString());
					sptReq.EmailSent = true;
					sptReq.Add();
					Response.Redirect("~/SupportSuccess.aspx");
				}
			}
		}

		protected void OnClickCancel(object sender, ImageClickEventArgs e)
		{
			UserAccount acct = Master.GetActiveUser();
			if (null != acct)
			{
				RedirectToDefault( acct );
			}
			else
			{
				RedirectToLoginPage();
			}
		}

		protected void OnClickLogin(object sender, ImageClickEventArgs e)
		{
			RedirectToLoginPage();
		}
	}
}