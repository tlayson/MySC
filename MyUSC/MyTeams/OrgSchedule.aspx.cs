using System;
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
	public partial class OrgSchedule : USCPageBase
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
				if( !IsPostBack )
				{
					Master.SetMenuState(lOrgID, false);
				}

				UserAccount acct = Master.GetActiveUser();
				if (UserHasViewAccess(acct.AccountID, lOrgID, OrgPageID.Manage))
				{
					SetSessionOrgID(lOrgID);
					Organization org = (Organization)Master.g_OrgList.htOrgList[lOrgID];
					if (!IsPostBack && null != org)
					{
						Master.PageHeading = "Schedule - " + org.OrgName;
						EnablePageOptions(acct.AccountID, lOrgID);
					}
					LoadSchedule( org );
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

		protected void EnablePageOptions( long acctID, long orgID )
		{
			if (!UserHasMemberAccess(acctID, orgID, OrgPageID.Schedule))
			{
				Master.AlertUser("You do not have permission to view this page.");
				Response.Redirect("~/MyTeams/Dashboard.aspx");
			}

			if (!UserHasEditAccess(acctID, orgID, OrgPageID.Schedule))
			{
				pnlAdmin.Visible = false;
			}

			if (!UserHasAdminAccess(acctID, orgID, OrgPageID.Schedule))
			{
				pnlAdmin.Visible = false;
			}
		}

		protected void DrawEmptyRow()
		{
			Table tbl = tblEvents;

			TableRow tr = new TableRow();
			tr.CssClass = "trNormal";

			TableCell td = new TableCell();
			td.CssClass = "tdInput";
			td.ColumnSpan = 6;
			Label lblCol = new Label();
			lblCol.CssClass = "medSiteColorTxt";
			lblCol.Text = "No events scheduled";
			td.Controls.Add( lblCol );
			tr.Controls.Add( td );

			tbl.Rows.Add( tr );
		}

		protected void NewDrawEvent( TableCell tdContainer, Event evt, Organization org )
		{
			bool fHideAll = chkHideAll.Checked;

			Table tbl = new Table();
			tbl.CssClass = "tblEventSection";

			EventDisplayRow edr = new EventDisplayRow();

			DrawEventRow0( tbl, edr, evt, org );
			DrawEventRow1( tbl, evt, org );

			UserAccount acct = GetActiveUser();
			Venue venue = Master.g_VenueList.GetVenue( evt.VenueID );
			bool fIsAdmin = UserHasAdminAccess( acct.AccountID, org.OrgID, OrgPageID.Schedule );
			string strSiteColor = GetSiteSetting(SiteAdmin.saKeySiteColor, "SiteColor");

			edr.BuildRows( this, tbl, evt, acct, org, venue, fIsAdmin, fHideAll, strSiteColor );

			tdContainer.Controls.Add( tbl );
		}

#region Row0
		protected void DrawEventRow0(Table tblContainer, EventDisplayRow edr, Event evt, Organization org)
		{
			TableRow tr = new TableRow();
			tr.CssClass = "trEventRow";

			// Hide details
			TableCell td = new TableCell();
			td.ColumnSpan = 3;
			td.CssClass = "tdRight";

			TBSCLinkButton btn = new TBSCLinkButton();
			btn.Click += OnClickHideDetails;
			btn.UserData = edr;
			if( chkHideAll.Checked )
			{
				btn.Text = "Show Details";
			}
			else
			{
				btn.Text = "Hide Details";
			}

			td.Controls.Add(btn);
			tr.Controls.Add(td);
			tblContainer.Controls.Add( tr );
		}
#endregion

#region Row1
		protected void DrawEventRow1(Table tblContainer, Event evt, Organization org)
		{
			TableRow tr = new TableRow();
			tr.CssClass = "trEventRow";

			// Date
			TableCell td = new TableCell();
			td.CssClass = "tdEventLeft";
			Label lbl = new Label();
			lbl.Text = evt.EventDate.ToShortDateString();
			td.Controls.Add( lbl );
			tr.Controls.Add( td );

			// Name
			string strCell = evt.EventTypeToString();
			strCell += " - ";
			strCell += evt.EventName;
			td = new TableCell();
			td.CssClass = "tdEventCenter";

			TBSCLinkButton btnEventDetails = new TBSCLinkButton();
			btnEventDetails.Text = strCell;
			btnEventDetails.Click += OnClickViewEvent;
			btnEventDetails.UserValue1 = evt.EventID;
			btnEventDetails.UserValue2 = evt.OrgID;
			btnEventDetails.UserData = org;

			td.Controls.Add(btnEventDetails);
			tr.Controls.Add( td );

			//Result or Response
			td = new TableCell();
			td.CssClass = "tdEventRight";
			if( evt.EventResult.Trim().Length > 0 )
			{
				lbl = new Label();
				lbl.Text = evt.EventResult;
				td.Controls.Add( lbl );
			}
			else
			{
				UserAccount acct = GetActiveUser();
				EventResponse evtr = org.orgEventResponseList.GetEventResponse( evt.EventID, acct.AccountID );
				TBSCLinkButton btnResponse = new TBSCLinkButton();
				btnResponse.ToolTip = "Click to edit your response";
				BuildResponseLink( btnResponse, evt, evtr, acct.AccountID );
				td.Controls.Add( btnResponse );

				Literal literal = new Literal();
				literal.Text = "<br />";
				td.Controls.Add(literal);

				EventResponsePanel erPanel = new EventResponsePanel();
				erPanel.SetResponseValues( this, org.OrgID, evt.EventID, acct.AccountID );
				erPanel.ShowList = false;
				btnResponse.UserData = erPanel;
				td.Controls.Add( erPanel );
			}
			tr.Controls.Add( td );

			tblContainer.Controls.Add( tr );
		}

		protected void BuildResponseLink( TBSCLinkButton lb, Event evt, EventResponse evtr, long lMemberID )
		{
			string strResponseID = "mrEvt" + evt.EventID.ToString();
			lb.CommandArgument = strResponseID;
			lb.Click += OnClickResponse;
			if( null == evtr )
			{
				lb.Text = "Respond";
			}
			else
			{
				lb.Text = evtr.ResponseTypeToString();
			}
			lb.UserValue1 = evt.EventID;
			lb.UserValue2 = lMemberID;
			lb.UserValue3 = evt.OrgID;
		}
#endregion

		protected void LoadSchedule( Organization org )
		{
			tblEvents.Controls.Clear();

			if( org.orgEventList.Count == 0 )
			{
				DrawEmptyRow();
			}
			else
			{
				// Determine what to show
				foreach( KeyValuePair<long, object> kvp in org.orgEventList.m_sdOrgEvents )
				{
					Event evt = (Event)kvp.Value;
					if( null != evt )
					{
						TableRow tr = new TableRow();
						TableCell td = new TableCell();

						NewDrawEvent( td, evt, org );

						tr.Controls.Add( td );
						tblEvents.Controls.Add( tr );
					}
				}
			}
		}

		protected void OnClick(object sender, EventArgs e)
		{
			
		}

		protected void OnClickResponse(object sender, EventArgs e)
		{
			TBSCLinkButton lb = (TBSCLinkButton)sender;
			EventResponsePanel erPnl = (EventResponsePanel)lb.UserData;
			if( null != erPnl )
			{
				bool fShowDetails = !erPnl.ShowList;
				if( chkHideAll.Checked )
				{
					fShowDetails = false;
				}
				erPnl.ShowList = !erPnl.ShowList;
			}
		}

		protected void OnClickAddEvent(object sender, EventArgs e)
		{
			Uri uriRet = Request.Url;
			SetSessionReturnURL(uriRet.PathAndQuery);

			long lOrgID = GetSessionOrgID();
			string strURL = "~/MyTeams/CreateEvent.aspx?OrgID=";
			strURL += lOrgID;
			Response.Redirect(strURL);
		}

		protected void OnClickManageResponses(object sender, EventArgs e)
		{

		}

		protected void ShowDetailsPanel( bool fShow )
		{
			pnlEventDetails.Visible = !fShow;
			pnlSchedule.Visible = fShow;
		}
		
		public override void ProcessChildClick( int nCmd )
		{
			if( 1 == nCmd )
			{
				ShowDetailsPanel( false );
				pnlEventDetails.Controls.Clear();
			}
		}

		protected void OnClickViewEvent(object sender, EventArgs e)
		{
			TBSCLinkButton btnEventDetails = (TBSCLinkButton)sender;
			long evtID = btnEventDetails.UserValue1;
			Organization org = (Organization)btnEventDetails.UserData;
			Event evt = org.orgEventList.GetEvent( evtID );

			ViewEventDetailsTable vedt = new ViewEventDetailsTable( evt.OrgID );
			pnlEventDetails.Controls.Add( vedt );
			vedt.SetEventDetails( this, org, evt);
			ShowDetailsPanel( true );
		}

		protected void OnClickEditEvent(object sender, EventArgs e)
		{
			TBSCLinkButton btnEventDetails = (TBSCLinkButton)sender;
			long evtID = btnEventDetails.UserValue1;
			Organization org = (Organization)btnEventDetails.UserData;
			Event evt = org.orgEventList.GetEvent(evtID);
			UserAccount acct = GetActiveUser();

			EditEventDetailsTable eedt = new EditEventDetailsTable( org.OrgID );
			pnlEventDetails.Controls.Add( eedt );
			eedt.SetEventDetails(this, org, evt, acct, false );
			ShowDetailsPanel(true);
		}

		protected void OnClickHideDetails(object sender, EventArgs e)
		{
			TBSCLinkButton btn = (TBSCLinkButton)sender;
			EventDisplayRow edr = (EventDisplayRow)btn.UserData;
			bool fHideDetails = !edr.ShowDetails;
			if( fHideDetails )
			{
				edr.ShowDetails = fHideDetails;
				btn.Text = "Hide Details";
			}
			else
			{
				edr.ShowDetails = fHideDetails;
				btn.Text = "Show Details";
			}
		}
	}
}