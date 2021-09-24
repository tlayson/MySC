using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MyUSC.Classes;
using MyUSC.Classes.MyOrg;
using CuteEditor;
using CuteEditor.ImageEditor;

namespace MyUSC.MyTeams
{
	public partial class OrgMail : USCPageBase
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
					if (UserHasViewAccess(acct.AccountID, lOrgID, OrgPageID.Email))
					{
						SetSessionOrgID(lOrgID);
						Organization org = (Organization)Master.g_OrgList.htOrgList[lOrgID];
						if (!IsPostBack && null != org)
						{
							Master.PageHeading = "Send eMail - " + org.OrgName;
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
				Master.SelectMTMenuItemOnPageLoad(MyTeamsPage.EMail);
			}
		}

		protected void EnablePageOptions( long acctID, long orgID )
		{
			if( !UserHasMemberAccess( acctID, orgID, OrgPageID.Email ) )
			{
			}

			if( !UserHasEditAccess( acctID, orgID, OrgPageID.Email ) )
			{
			}

			if( !UserHasAdminAccess( acctID, orgID, OrgPageID.Email ) )
			{
			}
		}

		protected void OnClickSend(object sender, EventArgs e)
		{

		}

	}
}