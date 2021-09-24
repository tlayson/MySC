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
	public partial class OrgDetails : USCPageBase
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			string strOrgID = Request.QueryString["OrgID"];
			if (!IsPostBack)
			{
				long lOrgID = 0;
				if (null != strOrgID && long.TryParse(strOrgID, out lOrgID))
				{
					Master.SetMenuState(lOrgID, false);

					UserAccount acct = Master.GetActiveUser();
					if (UserHasViewAccess(acct.AccountID, lOrgID, OrgPageID.Manage))
					{
						SetSessionOrgID(lOrgID);
						Organization org = (Organization)Master.g_OrgList.htOrgList[lOrgID];
						if (!IsPostBack && null != org)
						{
							Master.PageHeading = "Organization Details - " + org.OrgName;
							EnablePageOptions(acct.AccountID, lOrgID);
							ddlOrgState.LoadStates();
							DisplayOrgDetails(org);
						}
					}
					else
					{
						Master.AlertUser("You do not have permission to view this page.");
						Response.Redirect("~/MyTeams/Dashboard.aspx");
					}
				}
				else
				{
					Master.AlertUser("No organization specified.");
					Response.Redirect("~/MyTeams/Dashboard.aspx");
				}
				Master.SelectMTMenuItemOnPageLoad(MyTeamsPage.Manage);
			}
		}

/*
				<asp:DropDownList ID="ddlOrgType" runat="server">
					<asp:ListItem Text="Organization" Value="1"></asp:ListItem>
					<asp:ListItem Text="Region" Value="2"></asp:ListItem>
					<asp:ListItem Text="State" Value="3"></asp:ListItem>
					<asp:ListItem Text="District" Value="4"></asp:ListItem>
					<asp:ListItem Text="League" Value="5"></asp:ListItem>
					<asp:ListItem Text="Division" Value="6"></asp:ListItem>
					<asp:ListItem Selected="True" Text="Team" Value="7"></asp:ListItem>
					<asp:ListItem Text="Other" Value="10"></asp:ListItem>
				</asp:DropDownList>
*/
		private void DisplayOrgType( OrgTypes ot )
		{
			DropDownList ddl = ddlOrgType;
			// Clear current selection
			ddl.SelectedIndex = -1;

			switch( ot )
			{
				case OrgTypes.Organization:
				{
					ddl.SelectedIndex = 0;
					break;
				}
				case OrgTypes.Region:
				{
					ddl.SelectedIndex = 1;
					break;
				}
				case OrgTypes.State:
				{
					ddl.SelectedIndex = 2;
					break;
				}
				case OrgTypes.District:
				{
					ddl.SelectedIndex = 3;
					break;
				}
				case OrgTypes.League:
				{
					ddl.SelectedIndex = 4;
					break;
				}
				case OrgTypes.Division:
				{
					ddl.SelectedIndex = 5;
					break;
				}
				case OrgTypes.Team:
				{
					ddl.SelectedIndex = 6;
					break;
				}
				case OrgTypes.Other:
				{
					ddl.SelectedIndex = 7;
					break;
				}
			}
		}

		protected void DisplayOrgDetails(Organization org)
		{
			txtOrgName.Text = org.OrgName;
			UserAccount owner = GetUserByID( org.OwnerAccountID );
			if( null != owner )
			{
				lblOrgAdmin.Text = owner.DisplayName();
			}
			DisplayOrgType( org.OrgType );
			txtOrgDescription.Text = org.OrgDescription;
			txtOrgWebsite.Text = org.URL;
			txtOrgAddress1.Text = org.Address1;
			txtOrgAddress2.Text = org.Address2;
			txtOrgCity.Text = org.City;
			ddlOrgState.SelectedValue = org.State;
			txtOrgCountry.Text = org.Country;
			txtOrgZip.Text = org.Zip;
		}

		protected void EnablePageOptions( long acctID, long orgID )
		{
			if( !UserHasMemberAccess( acctID, orgID, OrgPageID.Manage ) )
			{
			}

			if( !UserHasEditAccess( acctID, orgID, OrgPageID.Manage ) )
			{
			}

			if( !UserHasAdminAccess( acctID, orgID, OrgPageID.Manage ) )
			{
			}
		}

		protected bool GetData( long orgID )
		{
			bool fRet = false;
			Organization org = Master.g_OrgList.GetOrganization( orgID );
			UserAccount acct = Master.GetActiveUser();
			if( null != org && null != acct )
			{
				org.OrgName = txtOrgName.Text;
				org.OrgDescription = txtOrgDescription.Text;
				org.URL = txtOrgWebsite.Text;
				org.Address1 = txtOrgAddress1.Text;
				org.Address2 = txtOrgAddress2.Text;
				org.City = txtOrgCity.Text;
				org.State = ddlOrgState.SelectedValue;
				org.Country = txtOrgCountry.Text;
				org.Zip = txtOrgZip.Text;

				int nIndex = ddlOrgType.SelectedIndex;
				org.OrgType = OrgTypes.Team;
				switch (nIndex)
				{
					case 0:
					{
						org.OrgType = OrgTypes.Organization;
						break;
					}
					case 1:
					{
						org.OrgType = OrgTypes.Region;
						break;
					}
					case 2:
					{
						org.OrgType = OrgTypes.State;
						break;
					}
					case 3:
					{
						org.OrgType = OrgTypes.District;
						break;
					}
					case 4:
					{
						org.OrgType = OrgTypes.League;
						break;
					}
					case 5:
					{
						org.OrgType = OrgTypes.Division;
						break;
					}
					case 6:
					{
						org.OrgType = OrgTypes.Team;
						break;
					}
					case 7:
					{
						org.OrgType = OrgTypes.Other;
						break;
					}
				}

				org.Update( acct );
			}

			return fRet;
		}


		protected void OnClickOK(object sender, EventArgs e)
		{
			long orgID = GetSessionOrgID();
			GetData( orgID );
			string strURL = "~/MyTeams/ManageOrg.aspx?OrgID=" + orgID;
			Response.Redirect( strURL );
		}

		protected void OnClickCancel(object sender, EventArgs e)
		{
			long orgID = GetSessionOrgID();
			string strURL = "~/MyTeams/ManageOrg.aspx?OrgID=" + orgID;
			Response.Redirect( strURL );
		}
	}
}