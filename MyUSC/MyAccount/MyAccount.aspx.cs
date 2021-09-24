using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MyUSC.Classes;

namespace MyUSC.MyAccount
{
	public partial class MyAccount : USCPageBase
	{
		protected MyAccount()
		{
			_PAGENAME = "Account";
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
				//Name
				lblUserNameValue.Text = acct.UserName;
				lblNameValue.Text = acct.DisplayName();
				lblEMailValue.Text = acct.Email;
				lblSecurityQuestionValue.Text = acct.SecurityQuestion;
				lblSecurityAnswerValue.Text = acct.SecurityAnswer;

				//Address
				lblAddress1Value.Text = acct.Address1;
				lblAddress2Value.Text = acct.Address2;
				lblCityValue.Text = acct.City;
				lblStateValue.Text = acct.State;
				lblZipValue.Text = acct.Zip;
				lblCountryValue.Text = acct.Country;

				//Sports
				lblSportsInterestValue.Text = acct.Preferences.Interests;

				//Prefs
				lblStartPageValue.Text = acct.DefaultPage;
				lblUsValue.Text = acct.Preferences.OffersFromUs.ToString();
				lblPartnerValue.Text = acct.Preferences.OffersFromPartners.ToString();

				//Photo
				if (acct.PhotoFile.Length > 0)
				{
					string strImageURL = "~/UsersFolder/" + acct.UserName + "/DisplayPhoto/" + acct.PhotoFile;
					imgUserPhoto.ImageUrl = strImageURL;
				}
			}
		}
#endregion

#region Page_Load
		protected void Page_Load(object sender, EventArgs e)
		{
			Response.Cache.SetCacheability(HttpCacheability.NoCache);
			Response.Cache.SetNoServerCaching();

			if (!IsPostBack)
			{
				SetDataValues();
				Master.SelectMenuItem(SelectedPage.Profile);
			}
		}
#endregion

#region ButtonClicks
		protected void OnClickName(object sender, ImageClickEventArgs e)
		{
			Response.Redirect("/MyAccount/MyAccountName.aspx");
		}

		protected void OnClickAddress(object sender, ImageClickEventArgs e)
		{
			Response.Redirect("/MyAccount/MyAccountAddress.aspx");
		}

		protected void OnClickSports(object sender, ImageClickEventArgs e)
		{
			Response.Redirect("/MyAccount/MyAccountSports.aspx");
		}

		protected void OnClickPreferences(object sender, ImageClickEventArgs e)
		{
			Response.Redirect("/MyAccount/MyAccountPrefs.aspx");
		}

		protected void OnClickPhoto(object sender, ImageClickEventArgs e)
		{
			Response.Redirect("/MyAccount/MyAccountPhoto.aspx");
		}
#endregion

	}
}