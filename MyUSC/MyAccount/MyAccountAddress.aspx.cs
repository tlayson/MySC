using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MyUSC.Classes;

namespace MyUSC.MyAccount
{
	public partial class MyAccountAddress : USCPageBase
	{
		protected MyAccountAddress()
		{
			_PAGENAME = "AccountAddress";
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
				txtAddress1.Text = acct.Address1;
				txtAddress2.Text = acct.Address2;
				txtCity.Text = acct.City;
				txtZip.Text = acct.Zip;
				ddlState.SelectedValue = acct.State;
				ddlCountry.SelectedValue = acct.Country;
			}
		}
#endregion

#region PopulateDropDownLists
		void PopulateDropDownLists()
		{
			var lstState = Master.GetSiteSetting(SiteAdmin.saKeyStates, "StateNames");
			List<string> lstStates = new List<string>();
			lstStates = lstState.Trim().Split(',').ToList();
			ddlState.Items.Clear();

			for (int intCounter = 0; intCounter < lstStates.Count; intCounter++)
			{
				string strState = lstStates[intCounter].Trim();

				ddlState.Items.Add(new ListItem(strState, strState));
			}

			ddlCountry.Items.Add(new ListItem("United States", "United States"));
			ddlCountry.Items.Add(new ListItem("Cananda", "Cananda"));
			ddlCountry.Items.Add(new ListItem("Other", "Other"));
		}
#endregion


#region Page_Load
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				PopulateDropDownLists();
				SetDataValues();
				Master.SelectMenuItem(SelectedPage.Profile);
			}
		}
#endregion

#region Button Clicks
		bool ValidateAddressSection()
		{
			bool fSectionValid = true;
			return fSectionValid;
		}

		protected bool GetData(UserAccount acct)
		{
			bool fRet = true;

			acct.Address1 = txtAddress1.Text;
			acct.Address2 = txtAddress2.Text;
			acct.City = txtCity.Text;
			acct.Zip = txtZip.Text;
			acct.State = ddlState.SelectedValue;
			acct.Country = ddlCountry.SelectedValue;

			return fRet;
		}

		protected void OnClickOK(object sender, ImageClickEventArgs e)
		{
			if (ValidateAddressSection())
			{
				// Get the users account
				UserAccount acct = GetActiveUser();

				if (GetData(acct))
				{
					acct.Update();
					Response.Redirect("/MyAccount/MyAccount.aspx");
				}
			}
		}

		protected void OnClickCancel(object sender, ImageClickEventArgs e)
		{
			Response.Redirect("/MyAccount/MyAccount.aspx");
		}
#endregion
	}
}