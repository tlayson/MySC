using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MyUSC.Classes;
using MyUSC.Classes.MyOrg;

namespace MyUSC.MyTeams
{
	public partial class CreateEvent : USCPageBase
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			_PAGENAME = "OrgSchedule";
			_pgUSCMaster = (OrgMaster)Master;

			// Draw everytime so the controls exist on postback
			string strOrgID = Request.QueryString["OrgID"];
			long lOrgID = 0;
			if (null != strOrgID && long.TryParse(strOrgID, out lOrgID))
			{
				if (!IsPostBack)
				{
					Master.SetMenuState(lOrgID, false);
				}

				UserAccount acct = Master.GetActiveUser();
				if (UserHasViewAccess(acct.AccountID, lOrgID, OrgPageID.Manage))
				{
					SetSessionOrgID(lOrgID);
					Organization org = (Organization)Master.g_OrgList.htOrgList[lOrgID];
					if( null != org )
					{
						if (!IsPostBack)
						{
							Master.PageHeading = "Create Event - " + org.OrgName;
							EnablePageOptions(acct.AccountID, lOrgID);
						}

						BuildEventTable( org, acct );
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

			if (!IsPostBack)
			{
				Master.SelectMTMenuItemOnPageLoad(MyTeamsPage.Schedule);
			}
		}

		protected void EnablePageOptions(long acctID, long orgID)
		{
			if (!UserHasMemberAccess(acctID, orgID, OrgPageID.Schedule))
			{
			}

			if (!UserHasEditAccess(acctID, orgID, OrgPageID.Schedule))
			{
				Master.AlertUser("You do not have permission to view this page.");
				Response.Redirect("~/MyTeams/Dashboard.aspx");
			}

			if (!UserHasAdminAccess(acctID, orgID, OrgPageID.Schedule))
			{
			}
		}

		protected void BuildEventTable( Organization org, UserAccount acct )
		{
			EditEventDetailsTable eedt = new EditEventDetailsTable( org.OrgID );
			eedt.SetEventDetails( this, org, null, acct, true );
			pnlCreateEvent.Controls.Add( eedt );
		}

		public override void ProcessChildClick(int nCmd)
		{
			if (1 == nCmd)
			{
				string strRetURL = GetSessionReturnURL();
				Response.Redirect( strRetURL );
			}
		}

	}
}