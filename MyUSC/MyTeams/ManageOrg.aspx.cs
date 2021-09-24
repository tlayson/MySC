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
	public partial class ManageOrg : USCPageBase
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
							Master.PageHeading = "Manage Organization - " + org.OrgName;
							EnablePageOptions(acct.AccountID, lOrgID);
						}
						EnablePageOptions(acct.AccountID, lOrgID);
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

		// Enable features based on the users access level
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

		protected void OnClickEditDetails(object sender, EventArgs e)
		{
			long orgID = GetSessionOrgID();
			string strURL = "~/MyTeams/OrgDetails.aspx?OrgID=" + orgID;
			Response.Redirect( strURL );
		}

		protected void OnClickEditOptions(object sender, EventArgs e)
		{
			long orgID = GetSessionOrgID();
			string strURL = "~/MyTeams/OrgOptions.aspx?OrgID=" + orgID;
			Response.Redirect(strURL);
		}

		protected void OnClickEditPermissions(object sender, EventArgs e)
		{
			long orgID = GetSessionOrgID();
			string strURL = "~/MyTeams/OrgPermissions.aspx?OrgID=" + orgID;
			Response.Redirect(strURL);
		}

		protected void OnClickEditAffiliates(object sender, EventArgs e)
		{
			long orgID = GetSessionOrgID();
			string strURL = "~/MyTeams/ManageAffiliates.aspx?OrgID=" + orgID;
			Response.Redirect(strURL);
		}

		protected void OnClickEditNews(object sender, EventArgs e)
		{
			long orgID = GetSessionOrgID();
			string strURL = "~/MyTeams/ManageNews.aspx?OrgID=" + orgID;
			Response.Redirect(strURL);
		}
	}
}