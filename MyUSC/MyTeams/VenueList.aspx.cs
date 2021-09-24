using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Globalization;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Text.RegularExpressions;
using CuteChat;
using Facebook;
using System.Web.UI.HtmlControls;
using System.Web.SessionState;
using MyUSC.Classes;
using MyUSC.Classes.MyOrg;

namespace MyUSC.MyTeams
{
	public partial class VenueList : USCPageBase
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			UserAccount acct = Master.GetActiveUser();
			if (null == acct)
			{
				SetSessionReturnURL("~/MyTeams/Dashboard.aspx");
				RedirectToLoginPage();
			}

			string strOrgID = Request.QueryString["OrgID"];
			if (!IsPostBack)
			{
				long lOrgID = 0;
				if (null != strOrgID && long.TryParse(strOrgID, out lOrgID))
				{
					Master.SetMenuState(lOrgID, false);

					if (UserHasViewAccess(acct.AccountID, lOrgID, OrgPageID.Manage))
					{
						SetSessionOrgID(lOrgID);
						Organization org = (Organization)Master.g_OrgList.htOrgList[lOrgID];
						if( !org.ListsLoaded )
						{
							org.LoadLists();
						}

						if (!IsPostBack && null != org)
						{
							Uri uriRet = Request.Url;
							SetSessionReturnURL( uriRet.PathAndQuery );

							Master.PageHeading = "Venue List - " + org.OrgName;
							EnablePageOptions(acct.AccountID, lOrgID);
							DisplayVenueList( org );
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
				Master.SelectMTMenuItemOnPageLoad(MyTeamsPage.Venues);
			}
		}

		protected void DrawEmptyList()
		{
			Table tbl = tblVenueList;

			TableRow tr = new TableRow();
			tr.CssClass = "trNormal";

			TableCell td = new TableCell();
			td.CssClass = "tdInput";
			td.ColumnSpan = 3;
			Label lbl = new Label();
			lbl.Text = "There are currently no fields/venues associated with your organization.  Use the buttons above to find or create some.";
			lbl.CssClass = "medSiteColorTxt";
			td.Controls.Add( lbl );
			tr.Controls.Add( td );
			tbl.Controls.Add( tr );
		}

		protected void AddVenueToTable( Venue venue )
		{
			long orgID = GetSessionOrgID();

			Table tbl = tblVenueList;

			TableRow tr = new TableRow();
			tr.CssClass = "trNormal";

			TableCell td = new TableCell();
			td.CssClass = "tdInput";
			HyperLink hl = new HyperLink();
			hl.Text = venue.VenueName;
			string strSiteColor = GetSiteSetting(SiteAdmin.saKeySiteColor, "SiteColor");
			if( strSiteColor.Length > 0 )
			{
				int nSiteColor = Convert.ToInt32(strSiteColor);
				Color clr = Color.FromArgb( nSiteColor );
				hl.ForeColor = clr;
			}

			// Build URL
			string strURL = "~/MyTeams/VenueDetails.aspx?VID=";
			strURL += venue.VenueID;
			strURL += "&OrgID=" + orgID;
			hl.NavigateUrl = strURL;
			td.Controls.Add( hl );
			tr.Controls.Add( td );

			td = new TableCell();
			td.CssClass = "tdInput";
			Label lbl = new Label();
			if( venue.DisplayLocation.Length > 0 )
			{
				lbl.Text = venue.DisplayLocation;
			}
			else
			{
				lbl.Text = venue.Address1;
			}
			lbl.CssClass = "medNormalTxt";
			td.Controls.Add( lbl );
			tr.Controls.Add( td );

			td = new TableCell();
			td.CssClass = "tdInput";
			lbl = new Label();
			lbl.Text = venue.City;
			lbl.CssClass = "medNormalTxt";
			td.Controls.Add( lbl );
			tr.Controls.Add( td );

			tbl.Controls.Add( tr );
		}

		protected void DrawHeader()
		{
			Table tbl = tblVenueList;

			TableRow tr = new TableRow();
			tr.CssClass = "trAdmin";

			TableCell td = new TableCell();
			td.CssClass = "tdInput";
			Label lbl = new Label();
			lbl.Text = "Field/Venue";
			lbl.CssClass = "medSiteColorTxt";
			td.Controls.Add( lbl );
			tr.Controls.Add( td );

			td = new TableCell();
			td.CssClass = "tdInput";
			lbl = new Label();
			lbl.Text = "Location";
			lbl.CssClass = "medSiteColorTxt";
			td.Controls.Add( lbl );
			tr.Controls.Add( td );

			td = new TableCell();
			td.CssClass = "City";
			lbl = new Label();
			lbl.Text = "City";
			lbl.CssClass = "medSiteColorTxt";
			td.Controls.Add( lbl );
			tr.Controls.Add( td );

			tbl.Controls.Add( tr );
		}

		protected void DisplayVenueList( Organization org )
		{
			OrgVenuePairingList ovpl = org.orgVenueList;
			DrawHeader();

			if( ovpl.m_sdOrgVenuePairs.Count > 0 )
			{
				foreach (KeyValuePair<long, object> kvp in ovpl.m_sdOrgVenuePairs)
				{
					OrgVenuePairing ovp = (OrgVenuePairing)kvp.Value;
					if (null != ovp)
					{
						Venue venue = Master.g_VenueList.GetVenueByID( ovp.VenueID );
						if( null != venue )
						{
							AddVenueToTable( venue );
						}
					}
				}
			}
			else
			{
				DrawEmptyList();
			}
		}

		protected void EnablePageOptions( long acctID, long orgID )
		{
			if( !UserHasMemberAccess( acctID, orgID, OrgPageID.Manage ) )
			{
			}

			if( !UserHasEditAccess( acctID, orgID, OrgPageID.Manage ) )
			{
			}

			if( UserHasAdminAccess( acctID, orgID, OrgPageID.Manage ) )
			{
				btnNewVenue.Visible = true;
				btnFindVenue.Visible = true;
			}
		}

		protected void OnClickNewVenue(object sender, EventArgs e)
		{
			Uri uriRet = Request.Url;
			SetSessionReturnURL(uriRet.PathAndQuery);

			long orgID = GetSessionOrgID();
			string strURL = "~/MyTeams/CreateVenue.aspx?OrgID=";
			strURL += orgID;
			Response.Redirect( strURL );
		}

		protected void OnClickFindVenue(object sender, EventArgs e)
		{
			Uri uriRet = Request.Url;
			SetSessionReturnURL(uriRet.PathAndQuery);

			long orgID = GetSessionOrgID();
			string strURL = "~/MyTeams/FindVenue.aspx?OrgID=";
			strURL += orgID;
			Response.Redirect( strURL );
		}
	}
}