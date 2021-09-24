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

[assembly: TagPrefix("MyUSC.Classes", "MSC")]
namespace MyUSC.Classes.MyOrg
{
	[
		ToolboxData("<{0}:EditEventDetailsTable ID='eedtID' runat=\"server\"> </{0}:EditEventDetailsTable>")
	]
	public class EditEventDetailsTable : Table
	{
#region Members
		private USCPageBase m_parentPage;
		private Label m_lblTitle;
		private Label m_lblOrgName;
		private TBSCDropDown m_ddlSeason;
		private TBSCTextBox m_txtEventName;
		private DDLEventType m_ddlEventType;
		private TBSCDropDown m_ddlVenue;
		private TBSCTextBox m_txtAltLocation;
		private TBSCCheckBox m_chkAltLocation;
		private DateSelectTable m_dsEventDateTable;
		private TimeSelectTable m_tsEventTimeTable;
		private TBSCTextBox m_txtOpponent;
		private TBSCDropDown m_ddlHomeAway;
		private TBSCTextBox m_txtUniform;
		private TBSCTextBox m_txtResult;
		private TBSCTextBox m_txtComments;
		private TBSCTextBox m_txtWebsite;
		private TBSCCheckBox m_chkRequestResponse;
		private TBSCCheckBox m_chkSendReminders;
		private TBSCButton m_btnOK;

		public string Title
		{
			get
			{
				return this.m_lblTitle.Text;
			}
			set
			{
				this.m_lblTitle.Text = value;
			}
		}

#endregion

#region Creation
		public EditEventDetailsTable( long lOrgID )
		{
			this.CssClass = "tblDlgControlOuter";
			this.Width = 650;

			AddTitleRow();
			AddOrgRow();
			AddSeasonRow( lOrgID );
			AddNameRow();
			AddTypeRow();
			AddVenueRow();
			AddLocationRow( lOrgID );
			AddDateRow();
			AddTimeRow();
			AddOpponentRow();
			AddHomeAwayRow();
			AddUniformRow();
			AddResultsRow();
			AddCommentsRow();
			AddWebsiteRow();
			AddSeperatorRow();
			AddOptionsRow();
			AddButtonsRow();
		}

		protected void AddTitleRow()
		{
			//Title
			TableRow tr = new TableRow();
			tr.CssClass = "trDlgControlTitle";
			TableCell td1 = new TableCell();
			td1.CssClass = "tdDlgControlNormal";
			td1.ColumnSpan = 2;
			m_lblTitle = new Label();
			m_lblTitle.Text = "Event";
			td1.Controls.Add( m_lblTitle );
			tr.Controls.Add( td1 );
			this.Controls.Add( tr );
		}
		
		protected void AddOrgRow()
		{
			//Org Name
			TableRow tr = new TableRow();
			tr.CssClass = "trDlgControlNormal";
			TableCell td1 = new TableCell();
			td1.CssClass = "tdDlgControlNormal";
			td1.ColumnSpan = 2;
			m_lblOrgName = new Label();
			m_lblOrgName.Text = "Event";
			td1.Controls.Add( m_lblOrgName );
			tr.Controls.Add( td1 );
			this.Controls.Add( tr );
		}
		
		protected void AddSeasonRow( long lOrgID )
		{
			//Season
			TableRow tr = new TableRow();
			tr.CssClass = "trDlgControlNormal";
			TableCell td1 = new TableCell();
			td1.CssClass = "tdDlgControlNormal";
			Label lbl = new Label();
			lbl.Text = "Season : ";
			td1.Controls.Add( lbl );
			tr.Controls.Add( td1 );
			TableCell td2 = new TableCell();
			td2.CssClass = "tdDlgControlNormal";
			m_ddlSeason = new TBSCDropDown();
			// List will be filled at display time
			td2.Controls.Add( m_ddlSeason );
			Literal lit = new Literal();
			lit.Text = "&nbsp;&nbsp;&nbsp;";
			td2.Controls.Add( lit );
			TBSCLinkButton lnkNewSeason = new TBSCLinkButton();
			lnkNewSeason.Text = "Create New Season";
			lnkNewSeason.UserValue1 = lOrgID;
			lnkNewSeason.Click += OnClickNewSeason;
			td2.Controls.Add( lnkNewSeason );
			tr.Controls.Add( td2 );
			this.Controls.Add( tr );
		}
		
		protected void AddNameRow()
		{
			//Event Name
			//Label
			TableRow tr = new TableRow();
			tr.CssClass = "trDlgControlNormal";
			TableCell td1 = new TableCell();
			td1.CssClass = "tdDlgControlNormal";
			Label lbl = new Label();
			lbl.Text = "Event Name : ";
			td1.Controls.Add( lbl );
			tr.Controls.Add( td1 );
			//Textbox
			TableCell td2 = new TableCell();
			td2.CssClass = "tdDlgControlNormal";
			m_txtEventName = new TBSCTextBox();
			m_txtEventName.MaxLength = 250;
			m_txtEventName.Width = 400;
			td2.Controls.Add( m_txtEventName );
			tr.Controls.Add( td2 );
			this.Controls.Add( tr );
		}
		
		protected void AddTypeRow()
		{
			//Type
			//Label
			TableRow tr = new TableRow();
			tr.CssClass = "trDlgControlNormal";
			TableCell td1 = new TableCell();
			td1.CssClass = "tdDlgControlNormal";
			Label lbl = new Label();
			lbl.Text = "Event Type : ";
			td1.Controls.Add( lbl );
			tr.Controls.Add( td1 );
			//Dropdown
			TableCell td2 = new TableCell();
			td2.CssClass = "tdDlgControlNormal";
			m_ddlEventType = new DDLEventType();
			// List will be filled at display time
			td2.Controls.Add( m_ddlEventType );
			tr.Controls.Add( td2 );
			this.Controls.Add( tr );
		}
		
		protected void AddVenueRow()
		{
			//Venue
			//Label
			TableRow tr = new TableRow();
			tr.CssClass = "trDlgControlNormal";
			TableCell td1 = new TableCell();
			td1.CssClass = "tdDlgControlNormal";
			Label lbl = new Label();
			lbl.Text = "Location : ";
			td1.Controls.Add( lbl );
			tr.Controls.Add( td1 );
			//Dropdown
			TableCell td2 = new TableCell();
			td2.CssClass = "tdDlgControlNormal";
			m_ddlVenue = new TBSCDropDown();
			// List will be filled at display time
			td2.Controls.Add( m_ddlVenue );
			Literal lit = new Literal();
			lit.Text = "&nbsp;&nbsp;";
			td2.Controls.Add( lit );
			//Textbox
			m_txtAltLocation = new TBSCTextBox();
			m_txtAltLocation.MaxLength = 50;
			m_txtAltLocation.Width = 200;
			m_txtAltLocation.Visible = false;
			m_txtAltLocation.ToolTip = "Text to display in the schedule instead of the full address.";
			//Checkbox
			m_chkAltLocation = new TBSCCheckBox();
			m_chkAltLocation.Text = "Display Alternate";
			m_chkAltLocation.ToolTip = "Text to display in the schedule instead of the full address.";
			m_chkAltLocation.AutoPostBack = true;
			td2.Controls.Add( m_chkAltLocation );
			tr.Controls.Add( td2 );
			this.Controls.Add( tr );
		}
		
		protected void AddLocationRow( long lOrgID )
		{
			//New Location
			//Label
			TableRow tr = new TableRow();
			tr.CssClass = "trDlgControlNormal";
			TableCell td1 = new TableCell();
			td1.CssClass = "tdDlgControlNormal";
			tr.Controls.Add( td1 );
			//Link button
			TableCell td2 = new TableCell();
			td2.CssClass = "tdDlgControlNormal";
			TBSCLinkButton lnkNewLocation = new TBSCLinkButton();
			lnkNewLocation.UserValue1 = lOrgID;
			lnkNewLocation.Click += OnClickNewLocation;
			td2.Controls.Add( lnkNewLocation );
			tr.Controls.Add( td2 );
			this.Controls.Add( tr );
		}

		protected void AddDateRow()
		{
			//Event Date
			//Label
			TableRow tr = new TableRow();
			tr.CssClass = "trDlgControlNormal";
			TableCell td1 = new TableCell();
			td1.CssClass = "tdDlgControlNormal";
			Label lbl = new Label();
			lbl.Text = "Date : ";
			td1.Controls.Add( lbl );
			tr.Controls.Add( td1 );
			// Date Select
			TableCell td2 = new TableCell();
			td2.CssClass = "tdDlgControlNormal";
			m_dsEventDateTable = new DateSelectTable( DateSelectTable.DateSelectTypes.Future);
			td2.Controls.Add( m_dsEventDateTable );
			tr.Controls.Add( td2 );
			this.Controls.Add( tr );
		}
		
		protected void AddTimeRow()
		{
			//Time
			//Label
			TableRow tr = new TableRow();
			tr.CssClass = "trDlgControlNormal";
			TableCell td1 = new TableCell();
			td1.CssClass = "tdDlgControlNormal";
			Label lbl = new Label();
			lbl.Text = "Time : ";
			td1.Controls.Add( lbl );
			tr.Controls.Add( td1 );
			// Textbox
			TableCell td2 = new TableCell();
			td2.CssClass = "tdDlgControlNormal";
			m_tsEventTimeTable = new TimeSelectTable();
			td2.Controls.Add( m_tsEventTimeTable );
			tr.Controls.Add( td2 );
			this.Controls.Add( tr );
		}

		protected void AddOpponentRow()
		{
			//Opponent
			//Label
			TableRow tr = new TableRow();
			tr.CssClass = "trDlgControlNormal";
			TableCell td1 = new TableCell();
			td1.CssClass = "tdDlgControlNormal";
			Label lbl = new Label();
			lbl.Text = "Opponent : ";
			td1.Controls.Add( lbl );
			tr.Controls.Add( td1 );
			// Textbox
			TableCell td2 = new TableCell();
			td2.CssClass = "tdDlgControlNormal";
			m_txtOpponent = new TBSCTextBox();
			m_txtOpponent.MaxLength = 50;
			m_txtOpponent.Width = 200;
			td2.Controls.Add( m_txtOpponent );
			tr.Controls.Add( td2 );
			this.Controls.Add( tr );
		}

		protected void AddHomeAwayRow()
		{
			//HomeAway
			//Label
			TableRow tr = new TableRow();
			tr.CssClass = "trDlgControlNormal";
			TableCell td1 = new TableCell();
			td1.CssClass = "tdDlgControlNormal";
			Label lbl = new Label();
			lbl.Text = "Home/Away : ";
			td1.Controls.Add( lbl );
			tr.Controls.Add( td1 );
			//Dropdown
			TableCell td2 = new TableCell();
			td2.CssClass = "tdDlgControlNormal";
			m_ddlHomeAway = new TBSCDropDown();
			ListItem li = new ListItem();
			li.Value = "0";
			m_ddlHomeAway.Items.Add( li );
			li = new ListItem();
			li.Text = "Home";
			m_ddlHomeAway.Items.Add( li );
			li = new ListItem();
			li.Text = "Away";
			m_ddlHomeAway.Items.Add( li );
			td2.Controls.Add( m_ddlHomeAway );
			tr.Controls.Add( td2 );
			this.Controls.Add( tr );
		}

		protected void AddUniformRow()
		{
			//Uniform
			//Label
			TableRow tr = new TableRow();
			tr.CssClass = "trDlgControlNormal";
			TableCell td1 = new TableCell();
			td1.CssClass = "tdDlgControlNormal";
			Label lbl = new Label();
			lbl.Text = "Uniform : ";
			td1.Controls.Add( lbl );
			tr.Controls.Add( td1 );
			//Textbox
			TableCell td2 = new TableCell();
			td2.CssClass = "tdDlgControlNormal";
			m_txtUniform = new TBSCTextBox();
			m_txtUniform.MaxLength = 20;
			m_txtUniform.Width = 200;
			td2.Controls.Add( m_txtUniform );
			tr.Controls.Add( td2 );
			this.Controls.Add( tr );
		}

		protected void AddResultsRow()
		{
			//Result
			//Label
			TableRow tr = new TableRow();
			tr.CssClass = "trDlgControlNormal";
			TableCell td1 = new TableCell();
			td1.CssClass = "tdDlgControlNormal";
			Label lbl = new Label();
			lbl.Text = "Result : ";
			td1.Controls.Add( lbl );
			tr.Controls.Add( td1 );
			//Textbox
			TableCell td2 = new TableCell();
			td2.CssClass = "tdDlgControlNormal";
			m_txtResult = new TBSCTextBox();
			m_txtResult.MaxLength = 20;
			m_txtResult.Width = 200;
			td2.Controls.Add( m_txtResult );
			tr.Controls.Add( td2 );
			this.Controls.Add( tr );
		}

		protected void AddCommentsRow()
		{
			//Comments
			//Label
			TableRow tr = new TableRow();
			tr.CssClass = "trDlgControlNormal";
			TableCell td1 = new TableCell();
			td1.CssClass = "tdDlgControlNormal";
			Label lbl = new Label();
			lbl.Text = "Comments :";
			td1.Controls.Add( lbl );
			tr.Controls.Add( td1 );
			//Textbox
			TableCell td2 = new TableCell();
			td2.CssClass = "tdDlgControlNormal";
			m_txtComments = new TBSCTextBox();
			m_txtComments.Height = 50;
			m_txtComments.Width = 400;
			m_txtComments.TextMode = TextBoxMode.MultiLine;
			td2.Controls.Add( m_txtComments );
			tr.Controls.Add( td2 );
			this.Controls.Add( tr );
		}

		protected void AddWebsiteRow()
		{
			//Website
			//Label
			TableRow tr = new TableRow();
			tr.CssClass = "trDlgControlNormal";
			TableCell td1 = new TableCell();
			td1.CssClass = "tdDlgControlNormal";
			Label lbl = new Label();
			lbl.Text = "Event Website : ";
			td1.Controls.Add( lbl );
			tr.Controls.Add( td1 );
			//Textbox
			TableCell td2 = new TableCell();
			td2.CssClass = "tdDlgControlNormal";
			m_txtWebsite = new TBSCTextBox();
			m_txtWebsite.MaxLength = 250;
			m_txtWebsite.Width = 400;
			td2.Controls.Add( m_txtWebsite );
			tr.Controls.Add( td2 );
			this.Controls.Add( tr );
		}

		protected void AddSeperatorRow()
		{
			//Seperator
			//Literal
			TableRow tr = new TableRow();
			tr.CssClass = "trDlgControlNormal";
			TableCell td1 = new TableCell();
			td1.ColumnSpan = 2;
			td1.CssClass = "tdDlgControlNormal";
			Literal lit = new Literal();
			lit.Text = "<hr />";
			td1.Controls.Add( lit );
			tr.Controls.Add( td1 );
			this.Controls.Add( tr );

			//Options
			//Label
			tr = new TableRow();
			tr.CssClass = "trDlgControlNormal";
			td1 = new TableCell();
			td1.ColumnSpan = 2;
			td1.CssClass = "tdDlgControlNormal";
			Label lbl = new Label();
			lbl.Text = "<hr />";
			td1.Controls.Add( lbl );
			tr.Controls.Add( td1 );
			this.Controls.Add( tr );
		}

		protected void AddOptionsRow()
		{
			TableRow trOuter = new TableRow();
			trOuter.CssClass = "trDlgControlNormal";
			TableCell tdOuter = new TableCell();
			tdOuter.CssClass = "tdDlgControlNormal";
			tdOuter.ColumnSpan = 2;

			Table tblInner = new Table();
			tblInner.CssClass = "tblDlgControlInner";
			TableRow trInner = new TableRow();
			trInner.CssClass = "trDlgControlNormal";
			TableCell tdInner = new TableCell();
			tdInner.CssClass = "tdDlgControlNormal";
			m_chkRequestResponse = new TBSCCheckBox();
			m_chkRequestResponse.Text = "Request responses";
			tdInner.Controls.Add( m_chkRequestResponse );
			trInner.Controls.Add( tdInner );

			tdInner = new TableCell();
			tdInner.CssClass = "tdDlgControlNormal";
			m_chkSendReminders = new TBSCCheckBox();
			m_chkSendReminders.Text = "Send reminders";
			tdInner.Controls.Add( m_chkSendReminders );
			trInner.Controls.Add( tdInner );

			tdInner = new TableCell();
			tdInner.CssClass = "tdDlgControlNormal";
			TBSCButton btnDelete = new TBSCButton();
			btnDelete.Text = "Delete Event";
			btnDelete.Click += OnClickDeleteEvent;

			tdInner.Controls.Add( btnDelete );
			trInner.Controls.Add( tdInner );

			tblInner.Controls.Add( trInner );
			tdOuter.Controls.Add( tblInner );
			trOuter.Controls.Add( tdOuter );
		}

		protected void AddButtonsRow()
		{
			//OKCancel
			//OK
			TableRow tr = new TableRow();
			tr.CssClass = "trDlgControlNormal";
			TableCell td1 = new TableCell();
			td1.CssClass = "tdRight";
			m_btnOK = new TBSCButton();
			m_btnOK.Click += OnClickOK;
			m_btnOK.Text = "OK";
			m_btnOK.CssClass = "btnOK";
			td1.Controls.Add( m_btnOK );
			tr.Controls.Add( td1 );

			//Cancel
			TableCell td2 = new TableCell();
			td2.CssClass = "tdLeft";
			TBSCButton btnCancel = new TBSCButton();
			btnCancel.Click += OnClickCancel;
			btnCancel.Text = "Cancel";
			btnCancel.CssClass = "btnCancel";
			td2.Controls.Add( btnCancel );
			tr.Controls.Add( td2 );
			this.Controls.Add( tr );
		}
#endregion

#region SetValues
		protected void LoadSeasons( USCPageBase parentPage, Organization org, Event evt )
		{
			SeasonList sl = org.orgSeasonList;
			if( sl.htSeasons.Count > 0 )
			{
				foreach (DictionaryEntry de in sl.htSeasons)
				{
					Season season = (Season)de.Value;
					if (null != season)
					{
						ListItem li = new ListItem( season.SeasonName, season.SeasonID.ToString() );
						m_ddlSeason.Items.Add( li );
					}
				}

				if( null != evt )
				{
					m_ddlSeason.SelectedValue = evt.SeasonID.ToString();
				}
			}
		}
		
		protected void LoadTypes( Event evt )
		{
			if( null != evt )
			{
				int nEventType = (int)evt.EventType;
				m_ddlEventType.SelectedValue = nEventType.ToString();
			}
		}
		
		protected void LoadVenues( USCPageBase parentPage, Organization org, Event evt )
		{
			OrgVenuePairingList ovpl = org.orgVenueList;

			if( ovpl.m_sdOrgVenuePairs.Count > 0 )
			{
				foreach (KeyValuePair<long, object> kvp in ovpl.m_sdOrgVenuePairs)
				{
					OrgVenuePairing ovp = (OrgVenuePairing)kvp.Value;
					if (null != ovp)
					{
						Venue venue = parentPage.MasterPg.g_VenueList.GetVenueByID( ovp.VenueID );
						if( null != venue )
						{
							ListItem li = new ListItem( venue.VenueName, venue.VenueID.ToString() );
							m_ddlVenue.Items.Add( li );
						}
					}
				}

				if( null != evt )
				{
					m_ddlVenue.SelectedValue = evt.VenueID.ToString();
				}
			}
		}
		
		protected void SetHomeAway( Event evt )
		{
			m_ddlHomeAway.SelectedValue = evt.HomeAway;
		}
		
		public void SetEventDetails( USCPageBase parentPage, Organization org, Event evt, UserAccount acct, bool fNew )
		{
			m_parentPage = parentPage;
			if( fNew )
			{
				m_btnOK.CommandName = "new";
				m_btnOK.UserData = org;
				m_btnOK.UserAcct = acct;
			}
			else
			{
				m_btnOK.CommandName = "edit";
				m_btnOK.UserData = evt;
				m_btnOK.UserAcct = acct;
			}

			LoadSeasons( parentPage, org, evt );
			LoadTypes( evt );
			LoadVenues( parentPage, org, evt );

			if( null != evt )
			{
				m_lblOrgName.Text = org.OrgName;
				m_txtEventName.Text = evt.EventName;
				m_txtAltLocation.Text = evt.AltLocation;
				m_txtOpponent.Text = evt.Opponent;
				m_txtUniform.Text = evt.Uniform;
				m_txtResult.Text = evt.EventResult;
				m_txtComments.Text = evt.Comments;
				m_txtWebsite.Text = evt.URL;

				m_chkRequestResponse.Checked = evt.RequestResponse;
				m_chkSendReminders.Checked = evt.SendReminder;
				m_chkAltLocation.Checked = (evt.AltLocation.Length > 0);

				m_dsEventDateTable.SetDate(evt.EventDate);
				m_tsEventTimeTable.SetDate(evt.EventDate);

				m_ddlHomeAway.SelectedValue = evt.HomeAway;
			}

		}
#endregion

#region Handlers
		protected void OnClickNewSeason(object sender, EventArgs e)
		{

		}

		protected void OnClickNewLocation(object sender, EventArgs e)
		{

		}

		protected void OnClickDeleteEvent(object sender, EventArgs e)
		{

		}

		protected void GetValues( Event evt )
		{
			long lVal = -1;
			string strSel = m_ddlSeason.SelectedValue;
			if( long.TryParse( strSel, out lVal ) )
			{
				evt.SeasonID = lVal;
			}

			strSel = m_ddlVenue.SelectedValue;
			if( long.TryParse( strSel, out lVal ) )
			{
				evt.VenueID = lVal;
			}

			evt.SetEventTypeByValue( m_ddlEventType.SelectedValue );


			evt.EventName = m_txtEventName.Text;
			if( m_chkAltLocation.Checked )
			{
				evt.AltLocation = m_txtAltLocation.Text;
			}
			else
			{
				evt.AltLocation = "";
			}

			evt.Opponent = m_txtOpponent.Text;
			evt.Uniform = m_txtUniform.Text;
			evt.EventResult = m_txtResult.Text.Trim();
			evt.Comments = m_txtComments.Text;
			evt.URL = m_txtWebsite.Text.Trim();

			evt.RequestResponse = m_chkRequestResponse.Checked;
			evt.SendReminder = m_chkSendReminders.Checked;

			string strDate = m_dsEventDateTable.ToString();
			string strTime = m_tsEventTimeTable.ToString();
			strDate += " " + strTime;

			DateTime dtTmp = new DateTime();
			if(DateTime.TryParse(strDate, out dtTmp))
			{
				evt.EventDate = dtTmp;
			}
			else
			{
				evt.EventDate = new DateTime( 1900, 1, 1 );
			}

			 evt.HomeAway = m_ddlHomeAway.SelectedValue;
		}

		protected void OnClickOK(object sender, EventArgs e)
		{
			TBSCButton btn = (TBSCButton)sender;
			UserAccount acct = (UserAccount)btn.UserAcct;
			Event evt = null;
			if( btn.CommandName == "new" )
			{
				Organization org = (Organization)m_btnOK.UserData;
				evt = new Event();
				evt.OrgID = org.OrgID;
				GetValues( evt );
				evt.Creator = acct.UserName;
				org.orgEventList.Add( evt );
			}
			else
			{
				evt = (Event)m_btnOK.UserData;
				GetValues( evt );
				evt.Update( acct );
			}
			m_parentPage.ProcessChildClick( 1 );

		}

		protected void OnClickCancel(object sender, EventArgs e)
		{
			m_parentPage.ProcessChildClick( 1 );
		}
#endregion
	}
}