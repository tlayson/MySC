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

[assembly: TagPrefix("MyUSC.Classes", "MSC")]
namespace MyUSC.Classes.MyOrg
{
	[
		ToolboxData("<{0}:EventDisplayRow ID='edrID' runat=\"server\"> </{0}:EventDisplayRow>")
	]
	public class EventDisplayRow : TableRow
	{
		private string m_strSiteColor = "";
		private bool m_fAdminUser = false;
		private Venue m_venue;
		private Event m_Event;
		private Organization m_Organization;
		private UserAccount m_UserAcct;
		private USCPageBase m_parentPage;

		private TBSCPanel m_pnlExpandList;
		private EventResponseListPanel m_pnlResponseList;

		public EventDisplayRow()
		{
			m_venue = null;
			m_Event = null;
			m_Organization = null;
			m_UserAcct = null;
			m_pnlExpandList = null;
		}

		public string SiteColor
		{
			get
			{
				return this.m_strSiteColor;
			}
			set
			{
				this.m_strSiteColor = value;
			}
		}

		public bool ShowResponses
		{
			get
			{
				return this.m_pnlResponseList.Visible;
			}
			set
			{
				this.m_pnlResponseList.Visible = value;
			}
		}

		public bool ShowDetails
		{
			get
			{
				return this.m_pnlExpandList.Visible;
			}
			set
			{
				this.m_pnlExpandList.Visible = value;
			}
		}

		public bool IsAdminUser
		{
			get
			{
				return this.m_fAdminUser;
			}
			set
			{
				this.m_fAdminUser = value;
			}
		}

		public UserAccount UserAcct
		{
			get
			{
				return this.m_UserAcct;
			}
			set
			{
				this.m_UserAcct = value;
			}
		}

		public Venue EventVenue
		{
			get
			{
				return this.m_venue;
			}
			set
			{
				this.m_venue = value;
			}
		}

		public Event EventDetails
		{
			get
			{
				return this.m_Event;
			}
			set
			{
				this.m_Event = value;
			}
		}

		public Organization EventOrg
		{
			get
			{
				return this.m_Organization;
			}
			set
			{
				this.m_Organization = value;
			}
		}

		public void BuildRows( USCPageBase parentPage, Table tblContainer, Event evt, UserAccount acct, Organization org, Venue venue, bool fAdmin, bool fHideAll, string strSiteColor )
		{
			string strPnlResponse = "pnlER" + evt.EventID.ToString();
			string strPanelResponseList = "pnlERL" + evt.EventID.ToString();
			string strPanelExpand = "pnlExp" + evt.EventID.ToString();

			SiteColor = strSiteColor;
			IsAdminUser = fAdmin;
			EventVenue = venue;
			UserAcct = acct;
			EventOrg = org;
			EventDetails = evt;
			m_parentPage = parentPage;

			TableRow tr = new TableRow();
			tr.CssClass = "trEventRow";

			// Hide details
			TableCell td = new TableCell();
			td.ColumnSpan = 3;
			td.CssClass = "tdEventCenter";

			m_pnlExpandList = new TBSCPanel();
			m_pnlExpandList.ID = strPanelExpand;
			m_pnlExpandList.Visible = !fHideAll;

			Table tblExpand = new Table();
			tblExpand.CssClass = "tblEventExpand";
			m_pnlExpandList.Controls.Add( tblExpand );

			td.Controls.Add( m_pnlExpandList );
			tr.Controls.Add( td );
			tblContainer.Controls.Add( tr );

			DrawEventRow2(tblExpand, strPanelResponseList, evt, org);
			DrawEventRow3(tblExpand, strPanelResponseList, evt, org);
			DrawEventRow4(tblExpand, strPanelResponseList, evt, org);
		}
#region Row2
		protected void BuildVenueLink( TBSCHyperLink hl, Venue venue, Organization org )
		{
			if( null == venue )
			{
				hl.Text = "Unknown Venue";
			}
			else
			{
				StringBuilder sb = new StringBuilder();
				sb.Append( "~/MyTeams/VenueDetails.aspx?OrgID=" );
				sb.Append( org.OrgID ).Append( "&VID=" ).Append( venue.VenueID );
				hl.Text = venue.VenueName;
				hl.NavigateUrl = sb.ToString();

				string strSiteColor = SiteColor;
				if( strSiteColor.Length > 0 )
				{
					int nSiteColor = Convert.ToInt32(strSiteColor);
					Color clr = Color.FromArgb( nSiteColor );
					hl.ForeColor = clr;
				}
			}
		}

		protected void BuildEventLink( TBSCHyperLink hl, Event evt, Organization org )
		{
			if( null == evt )
			{
				hl.Text = "Unknown Event";
			}
			else
			{
				StringBuilder sb = new StringBuilder();
				sb.Append( "~/MyTeams/EventDetails.aspx?OrgID=" );
				sb.Append( org.OrgID ).Append( "&EventID=" ).Append( evt.EventID );
				hl.Text = "Edit Event";
				hl.NavigateUrl = sb.ToString();

				string strSiteColor = SiteColor;
				if( strSiteColor.Length > 0 )
				{
					int nSiteColor = Convert.ToInt32(strSiteColor);
					Color clr = Color.FromArgb( nSiteColor );
					hl.ForeColor = clr;
				}
			}
		}

		protected void DrawEventRow2(Table tblContainer, string strPanelResponseList, Event evt, Organization org)
		{
			UserAccount acct = UserAcct;
			TableRow tr = new TableRow();
			tr.CssClass = "trEventRow";

			// Time
			TableCell td = new TableCell();
			td.CssClass = "tdEventLeft";
			Label lbl = new Label();
			lbl.Text = evt.EventDate.ToShortTimeString();
			td.Controls.Add( lbl );
			tr.Controls.Add( td );

			// Venue Link
			td = new TableCell();
			td.CssClass = "tdEventRight";
			Venue venue = EventVenue;
			if( null == venue )
			{
				lbl = new Label();
				if( evt.AltLocation.Trim().Length > 0 )
				{
					lbl.Text = evt.AltLocation;
				}
				else
				{
					lbl.Text = "TBD";
				}
				td.Controls.Add( lbl );
			}
			else
			{
				TBSCHyperLink hlVenueDetails = new TBSCHyperLink();
				BuildVenueLink( hlVenueDetails, venue, org );
				td.Controls.Add( hlVenueDetails );
			}
			tr.Controls.Add( td );

			// Admin edit
			td = new TableCell();
			td.CssClass = "tdEventRight";
			if( IsAdminUser )
			{
				TBSCHyperLink hlEditEvent = new TBSCHyperLink();
				BuildEventLink( hlEditEvent, evt, org );
				td.Controls.Add( hlEditEvent );
			}
			else
			{
				lbl = new Label();
				lbl.Text = " ";
			}

			tr.Controls.Add( td );

			tblContainer.Controls.Add( tr );
		}
#endregion

#region Row3
		protected void DrawEventRow3(Table tblContainer, string strPanelResponseList, Event evt, Organization org)
		{
			UserAccount acct = UserAcct;
			TableRow tr = new TableRow();
			tr.CssClass = "trEventRow";

			// Blank
			TableCell td = new TableCell();
			td.CssClass = "tdEventLeft";
			Label lbl = new Label();
			lbl.Text = " ";
			td.Controls.Add( lbl );
			tr.Controls.Add( td );

			// Comments
			td = new TableCell();
			td.CssClass = "tdEventCenter";
			lbl = new Label();
			lbl.Text = evt.Comments;
			td.Controls.Add( lbl );
			tr.Controls.Add( td );

			// Manage Responses Link
			td = new TableCell();
			td.CssClass = "tdEventRight";
			if( IsAdminUser )
			{
				TBSCHyperLink hlEditResponses = new TBSCHyperLink();
				BuildManageResponsesLink( hlEditResponses, evt, org );
				td.Controls.Add( hlEditResponses );
			}
			else
			{
				lbl = new Label();
				lbl.Text = " ";
			}
			tr.Controls.Add( td );

			tblContainer.Controls.Add( tr );
		}

		protected void BuildManageResponsesLink( TBSCHyperLink hl, Event evt, Organization org )
		{
			if( null == evt )
			{
				hl.Text = "Unknown Event";
			}
			else
			{
				StringBuilder sb = new StringBuilder();
				sb.Append( "~/MyTeams/ManageEventResponses.aspx?OrgID=" );
				sb.Append( org.OrgID ).Append( "&EventID=" ).Append( evt.EventID );
				hl.Text = "Manage Responses";
				hl.NavigateUrl = sb.ToString();

				string strSiteColor = SiteColor;
				if( strSiteColor.Length > 0 )
				{
					int nSiteColor = Convert.ToInt32(strSiteColor);
					Color clr = Color.FromArgb( nSiteColor );
					hl.ForeColor = clr;
				}
			}
		}
#endregion

#region Row4
		protected void DrawEventRow4(Table tblContainer, string strPanelResponseList, Event evt, Organization org)
		{
			// Create the IDs now so the buttons can know what it is.
			string strResponseCtrlID = "mrOrg" + org.OrgID + "Evt" + evt.EventID;

			UserAccount acct = UserAcct;
			TableRow tr = new TableRow();
			tr.CssClass = "trEventRow";

			// Blank
			TableCell td = new TableCell();
			td.CssClass = "tdEventLeft";
			Label lbl = new Label();
			lbl.Text = " ";
			td.Controls.Add( lbl );
			tr.Controls.Add( td );

			// User Response Table
			td = new TableCell();
			td.CssClass = "tdEventCenter";

			Table tblUserResponses = new Table();
			tblUserResponses.CssClass = "tblNormal";

			TableRow trUR1 = new TableRow();
			trUR1.CssClass = "trEventResponseRow";
			TableCell tdUR1C1 = new TableCell();
			tdUR1C1.CssClass = "tdEventResponseList";
			TableCell tdUR1C2 = new TableCell();
			tdUR1C2.CssClass = "tdEventResponseListExpand";

			// Label
			Label lblResponses = new Label();
			lblResponses.Text = "Build the numbers";

			// Add the counts label to the cell
			tdUR1C1.Controls.Add( lblResponses );

			// Expand Button
			TBSCImageButton ibExpand = new TBSCImageButton();
			ibExpand.Click += OnClickExpandResponseList;
			ibExpand.ImageUrl = "~/Images/Button/btnExpandSection.jpg";
			ibExpand.CommandArgument = strPanelResponseList;
			ibExpand.ToolTip = "Click to view all responses";
			
			// Add the button to the cell
			tdUR1C2.Controls.Add( ibExpand );

			// Add both cells to the 1st row
			trUR1.Controls.Add( tdUR1C1 );
			trUR1.Controls.Add( tdUR1C2 );

			// Response Table
			TableRow trUR2 = new TableRow();
			trUR2.CssClass = "trEventResponseListRow";
			TableCell tdUR2C1 = new TableCell();
			tdUR2C1.ColumnSpan = 2;

			m_pnlResponseList = new EventResponseListPanel();
			//MemberResponseList responseList = new MemberResponseList();
			m_pnlResponseList.ID = strResponseCtrlID;
			m_pnlResponseList.ClientIDMode = System.Web.UI.ClientIDMode.Static;
			m_pnlResponseList.ID = strPanelResponseList;
			m_pnlResponseList.LoadResponseList( m_parentPage, org.OrgID, evt.EventID );
			ibExpand.UserData = m_pnlResponseList;
			tdUR2C1.Controls.Add( m_pnlResponseList );
			trUR2.Controls.Add( tdUR2C1 );

			tblUserResponses.Controls.Add( trUR1 );
			tblUserResponses.Controls.Add( trUR2 );
			td.Controls.Add( tblUserResponses );
			tr.Controls.Add( td );

			// ResponseDlg
			td = new TableCell();
			td.CssClass = "tdEventRight";

			tr.Controls.Add( td );

			tblContainer.Controls.Add( tr );
		}

#endregion

		protected void OnClickExpandResponseList(object sender, ImageClickEventArgs e)
		{
			m_pnlResponseList.Visible = !m_pnlResponseList.Visible;
		}

	}
}