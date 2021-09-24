using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MyUSC.Classes;
using MyUSC.Classes.MyOrg;

namespace MyUSC.MyTeams
{
	public partial class CreateOrg : USCPageBase
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			Master.PageHeading = "Create Organization";

			UserAccount acct = Master.GetActiveUser();
			lblOwnerName.Text = acct.DisplayName();

			Master.SetMenuState( 0, true );
			Master.SelectMTMenuItemOnPageLoad(MyTeamsPage.Dashboard);
		}

/*
		private string m_strOrgName;
		private string m_strOrgDescription;
		private OrgTypes m_orgType;
		private long m_lOwnerAccountID;
		private int m_nLanguage;
		private string m_strLogoURL;
 */
		protected void OnClickNext(object sender, EventArgs e)
		{

			if( txtOrgName.Text.Length == 0 )
			{
				txtOrgName.Focus();
				return;
			}

			// Create the org
			UserAccount acct = Master.GetActiveUser();
			Organization org = new Organization();
			org.ConnectionString = Master.g_strConnectionString;
			org.OwnerAccountID = acct.AccountID;

			org.OrgName = txtOrgName.Text;
			org.OrgDescription = txtOrgDesc.Text;
			org.OrgType = (OrgTypes)Convert.ToInt32( ddOrgType.SelectedValue );
			org.Language = 1;
			org.URL = txtWebsite.Text;
			org.Address1 = txtAddress1.Text;
			org.Address2 = txtAddress2.Text;
			org.City = txtCity.Text;
			org.State = txtState.Text;
			org.Zip = txtPostalCode.Text;
			org.Country = txtCountry.Text;
			org.Phone = txtPhone.Text;
			org.Cell = txtCell.Text;
			org.Fax = txtFax.Text;
			org.Creator = acct.UserName;

			if( Master.g_OrgList.AddOrg( org, acct ) )
			{
				String strRootPath = Server.MapPath("~");
				org.CreateOrgFolders(strRootPath);

				// Go to the options page
				string strRedir = "~/MyTeams/OrgOptions.aspx?Wiz=1&OrgID=" + org.OrgID + "&Wiz=1";
				Response.Redirect( strRedir );
			}

		}

		protected void OnClickCancel(object sender, EventArgs e)
		{
			Response.Redirect("~/MyTeams/Dashboard.aspx");
		}
	}
}