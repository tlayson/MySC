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
	public partial class EventDetails : USCPageBase
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			_PAGENAME = "OrgSchedule";
			_pgUSCMaster = (OrgMaster)Master;


			// Draw everytime so the controls exist on postback
			string strOrgID = Request.QueryString["OrgID"];
			string strEventID = Request.QueryString["EventID"];
			long lOrgID = -1;
			long lEventID = -1;
			if (null != strOrgID && long.TryParse(strOrgID, out lOrgID) &&
				null != strEventID && long.TryParse(strEventID, out lEventID))
			{
				string strURL = "~/MyTeams/Home.aspx?OrgID=" + lOrgID;
				SetSessionReturnURL( strURL );
				if (!IsPostBack)
				{
					Master.SetMenuState(lOrgID, false);
				}

				UserAccount acct = Master.GetActiveUser();
				if (UserHasViewAccess(acct.AccountID, lOrgID, OrgPageID.Manage))
				{
					SetSessionOrgID(lOrgID);
					Organization org = (Organization)Master.g_OrgList.htOrgList[lOrgID];
					if (null != org)
					{
						Event evt = org.orgEventList.GetEvent(lEventID);
						if( null != evt )
						{
							if (!IsPostBack)
							{
								EnablePageOptions(acct.AccountID, lOrgID);
							}

							if( UserHasEditAccess( acct.AccountID, lOrgID, OrgPageID.Schedule ) )
							{
								Master.PageHeading = "Edit Event Details";
								BuildEditEventTable(org, evt, acct);
							}
							else
							{
								Master.PageHeading = "Event Details";
								BuildViewEventTable(org, evt, acct);
							}
						}
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
				Master.AlertUser("You do not have permission to view this page.");
				Response.Redirect("~/MyTeams/Dashboard.aspx");
			}

			if (!UserHasEditAccess(acctID, orgID, OrgPageID.Schedule))
			{
			}

			if (!UserHasAdminAccess(acctID, orgID, OrgPageID.Schedule))
			{
			}
		}

		protected void BuildEditEventTable(Organization org, Event evt, UserAccount acct)
		{
			EditEventDetailsTable eedt = new EditEventDetailsTable(org.OrgID);
			eedt.SetEventDetails( this, org, evt, acct, false );
			pnlEditEvent.Controls.Add(eedt);
		}

		protected void BuildViewEventTable(Organization org, Event evt, UserAccount acct)
		{
			ViewEventDetailsTable vedt = new ViewEventDetailsTable(org.OrgID);
			vedt.SetEventDetails(this, org, evt);
			pnlEditEvent.Controls.Add(vedt);
		}

		public override void ProcessChildClick(int nCmd)
		{
			if (1 == nCmd)
			{
				string strRetURL = GetSessionReturnURL();
				Response.Redirect(strRetURL);
			}
		}
	}
}