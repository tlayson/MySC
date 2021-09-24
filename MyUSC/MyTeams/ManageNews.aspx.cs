using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MyUSC.Classes;
using MyUSC.Classes.MyOrg;
using CuteEditor;
using CuteEditor.ImageEditor;

namespace MyUSC.MyTeams
{
	public partial class ManageNews : USCPageBase
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
					if (UserHasEditAccess(acct.AccountID, lOrgID, OrgPageID.Manage))
					{
						SetSessionOrgID(lOrgID);
						Organization org = (Organization)Master.g_OrgList.htOrgList[lOrgID];
						if (!IsPostBack && null != org)
						{
							Master.PageHeading = "Manage News - " + org.OrgName;
							EnablePageOptions(acct.AccountID, lOrgID);
							txtNews.Text = org.orgInfo.News;
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


		protected void OnClickOK(object sender, ImageClickEventArgs e)
		{
			long orgID = GetSessionOrgID();
			Organization org = (Organization)Master.g_OrgList.htOrgList[orgID];
			org.orgInfo.News = txtNews.Text;
			UserAccount acct = Master.GetActiveUser();
			org.orgInfo.Update( acct );
			string strURL = "~/MyTeams/ManageOrg.aspx?OrgID=" + orgID;
			Response.Redirect( strURL );
		}

		protected void OnClickCancel(object sender, ImageClickEventArgs e)
		{
			long orgID = GetSessionOrgID();
			string strURL = "~/MyTeams/ManageOrg.aspx?OrgID=" + orgID;
			Response.Redirect( strURL );
		}
	}
}