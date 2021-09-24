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
	public partial class MemberInfo : USCPageBase
	{
/*
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
							Master.PageHeading = "Home - " + org.OrgName;
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

			}
		}

 * */
		protected void Page_Load(object sender, EventArgs e)
		{
			Master.PageHeading = "Profile";
			string strMemberID = Request.QueryString["MemID"];
			long lMemberID = 0;
			if (long.TryParse(strMemberID, out lMemberID))
			{
				if (1 == lMemberID)
				{
					Master.PageHeading = "Profile: Tom Layson";
					lblUserNameValue.Text = "tlayson";
					lblNameValue.Text = "Tom Layson";
					lblNickname.Text = "Double 0 Dad";
				}
				else if (2 == lMemberID)
				{
					Master.PageHeading = "Profile: Dan Ozment";
					lblUserNameValue.Text = "doz";
					lblNameValue.Text = "Dan Ozment";
					lblNickname.Text = "Injury magnet";
				}
				else if (3 == lMemberID)
				{
					Master.PageHeading = "Profile: Eddie Rivera";
					lblUserNameValue.Text = "erivera";
					lblNameValue.Text = "Eddie Rivera";
					lblNickname.Text = "";
				}
			}
			Master.SelectMTMenuItemOnPageLoad(MyTeamsPage.Roster);
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
	}
}