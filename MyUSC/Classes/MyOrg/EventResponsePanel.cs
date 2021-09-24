using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MyUSC.Classes;
using MyUSC.Classes.MyOrg;

[assembly: TagPrefix("MyUSC.Classes", "MSC")]
namespace MyUSC.Classes.MyOrg
{
	[
		ToolboxData("<{0}:EventResponsePanel ID='erpID' runat=\"server\"> </{0}:EventResponsePanel>")
	]
	public class EventResponsePanel : TBSCPanel
	{
		private long m_lOrgID;
		private long m_lEventID;
		private long m_lVenueID;
		public Table m_tblResponse;
		private RadioButtonList m_rblChoice;
		private TextBox m_txtNotes;

		public EventResponsePanel()
		{
			EventID = -1;
			VenueID = -1;
			OrgID = -1;

			this.ShowList = false;

			// Add the section title
			m_tblResponse = new Table();
			m_tblResponse.CssClass = "tblDlgControlOuter";

			// Title
			TableRow tr = new TableRow();
			tr.CssClass = "trDlgControlTitle";
			TableCell td = new TableCell();
			td.CssClass = "tdDlgControlNormal";
			td.ColumnSpan = 2;
			Label lbl = new Label();
			lbl.Text = "My Response";
			td.Controls.Add(lbl);
			tr.Controls.Add(td);
			m_tblResponse.Controls.Add(tr);

			// Create controls for later
			m_rblChoice = new RadioButtonList();
			m_txtNotes = new TextBox();

			this.Controls.Add( m_tblResponse );
		}

		public bool ShowList
		{
			get
			{
				return this.Visible;
			}
			set
			{
				this.Visible = value;
			}
		}

		public long EventID
		{
			get
			{
				return m_lEventID;
			}
			set
			{
				m_lEventID = value;
			}
		}

		public long VenueID
		{
			get
			{
				return m_lVenueID;
			}
			set
			{
				m_lVenueID = value;
			}
		}

		public long OrgID
		{
			get
			{
				return m_lOrgID;
			}
			set
			{
				m_lOrgID = value;
			}
		}

		public void SetResponseValues( USCPageBase parentPage, long orgID, long eventID, long acctID )
		{
			USCMaster mscMaster = (USCMaster)parentPage.Master;
			UserAccount acct = parentPage.GetActiveUser();
			Organization org = mscMaster.g_OrgList.GetOrganization(orgID);
			Event evt = org.orgEventList.GetEvent(eventID);
			EventResponse evtr = org.orgEventResponseList.GetEventResponse(eventID, acctID);

			EventID = evt.EventID;
			VenueID = evt.VenueID;
			OrgID = evt.OrgID;
			UserData = parentPage;

			BuildTopSection( mscMaster, evt );
			BuildBottomSection( mscMaster, evt, evtr );
			ShowList = true;
		}

		protected void BuildTopSection( USCMaster mscMaster, Event evt )
		{
			// Date
			TableRow tr = new TableRow();
			tr.CssClass = "trDlgControlNormal";
			TableCell td = new TableCell();
			td.CssClass = "tdDlgControlNormal";
			td.ColumnSpan = 2;
			Label lbl = new Label();
			string strDate = evt.EventDate.ToShortDateString();
			strDate += " - ";
			strDate = evt.EventDate.ToShortTimeString();
			lbl.Text = strDate;
			td.Controls.Add(lbl);
			tr.Controls.Add(td);
			m_tblResponse.Controls.Add(tr);

			// Name
			tr = new TableRow();
			tr.CssClass = "trDlgControlNormal";
			td = new TableCell();
			td.CssClass = "tdDlgControlNormal";
			td.ColumnSpan = 2;
			lbl = new Label();
			lbl.Text = evt.EventName;
			td.Controls.Add(lbl);
			tr.Controls.Add(td);
			m_tblResponse.Controls.Add(tr);

			// Venue
			tr = new TableRow();
			tr.CssClass = "trDlgControlNormal";
			td = new TableCell();
			td.CssClass = "tdDlgControlNormal";
			td.ColumnSpan = 2;
			lbl = new Label();
			Venue venue = mscMaster.g_VenueList.GetVenueByID(evt.VenueID);
			if (null == venue)
			{
				lbl.Text = "TBD";
			}
			else
			{
				lbl.Text = venue.VenueName;
			}
			td.Controls.Add(lbl);
			tr.Controls.Add(td);
			m_tblResponse.Controls.Add(tr);

			// Divider
			tr = new TableRow();
			tr.CssClass = "trDlgControlNormal";
			td = new TableCell();
			td.CssClass = "tdDlgControlNormal";
			td.ColumnSpan = 2;
			Literal literal = new Literal();
			literal.Text = "<hr />";
			td.Controls.Add(literal);
			tr.Controls.Add(td);
			m_tblResponse.Controls.Add(tr);
		}

		protected void BuildBottomSection(USCMaster mscMaster, Event evt, EventResponse evtr)
		{
			// Question
			TableRow tr = new TableRow();
			tr.CssClass = "trDlgControlNormal";
			TableCell td = new TableCell();
			td.CssClass = "tdDlgControlNormal";
			td.ColumnSpan = 2;
			Label lbl = new Label();
			lbl.Text = "Will you be there?";
			td.Controls.Add(lbl);
			tr.Controls.Add(td);
			m_tblResponse.Controls.Add(tr);

			// Choice
			tr = new TableRow();
			tr.CssClass = "trDlgControlNormal";
			td = new TableCell();
			td.CssClass = "tdDlgControlNormal";
			td.ColumnSpan = 2;
			m_rblChoice = new RadioButtonList();
			m_rblChoice.ID = "rblEvt" + evt.EventID.ToString();
			m_rblChoice.ClientIDMode = System.Web.UI.ClientIDMode.Static;
			m_rblChoice.AutoPostBack = true;

			ListItem li = new ListItem();
			li.Text = "Yes";
			li.Value = "Yes";
			m_rblChoice.Items.Add(li);
			li = new ListItem();
			li.Text = "No";
			li.Value = "No";
			m_rblChoice.Items.Add(li);
			li = new ListItem();
			li.Text = "Maybe";
			li.Value = "Maybe";
			m_rblChoice.Items.Add(li);

			m_rblChoice.ClearSelection();
			if (null != evtr)
			{
				m_rblChoice.SelectedValue = evtr.ResponseTypeToString();
			}

			td.Controls.Add(m_rblChoice);
			tr.Controls.Add(td);
			m_tblResponse.Controls.Add(tr);

			// Notes
			tr = new TableRow();
			tr.CssClass = "trDlgControlNormal";
			td = new TableCell();
			td.CssClass = "tdDlgControlNormal";
			td.ColumnSpan = 2;
			lbl = new Label();
			lbl.Text = "Notes:";
			td.Controls.Add(lbl);
			tr.Controls.Add(td);
			m_tblResponse.Controls.Add(tr);
			tr = new TableRow();
			tr.CssClass = "trDlgControlNormal";
			td = new TableCell();
			td.CssClass = "tdDlgControlNormal";
			td.ColumnSpan = 2;
			m_txtNotes = new TextBox();
			m_txtNotes.ID = "txtEvt" + evt.EventID.ToString();
			m_txtNotes.ClientIDMode = System.Web.UI.ClientIDMode.Static;
			if (null != evtr)
			{
				m_txtNotes.Text = evtr.Notes;
			}
			td.Controls.Add(m_txtNotes);
			tr.Controls.Add(td);
			m_tblResponse.Controls.Add(tr);

			// OK
			tr = new TableRow();
			tr.CssClass = "trDlgControlNormal";
			td = new TableCell();
			td.CssClass = "tdDlgControlNormal";
			TBSCButton btn = new TBSCButton();
			btn.Click += OnClickOK;
			btn.Text = "OK";
			btn.CssClass = "btnOK";
			btn.UserData = this;
			td.Controls.Add(btn);
			tr.Controls.Add(td);
			m_tblResponse.Controls.Add(tr);

			// Cancel
			tr = new TableRow();
			tr.CssClass = "trDlgControlNormal";
			td = new TableCell();
			td.CssClass = "tdDlgControlNormal";
			btn = new TBSCButton();
			btn.Click += OnClickCancel;
			btn.Text = "Cancel";
			btn.CssClass = "btnCancel";
			td.Controls.Add(btn);
			tr.Controls.Add(td);
			m_tblResponse.Controls.Add(tr);
		}

		protected void GetEventInfo( EventResponse evtr )
		{
			evtr.SetResponseFromString( m_rblChoice.SelectedValue );
			evtr.Notes = m_txtNotes.Text;
		}

		protected void OnClickOK(object sender, EventArgs e)
		{
			bool fNew = false;
			USCPageBase parentPage = (USCPageBase)UserData;
			USCMaster mscMaster = (USCMaster)parentPage.Master;
			UserAccount acct = parentPage.GetActiveUser();
			Organization org = mscMaster.g_OrgList.GetOrganization(m_lOrgID);

			EventResponse evtr = org.orgEventResponseList.GetEventResponse( EventID, acct.AccountID );
			if( null == evtr )
			{
				evtr = new EventResponse();
				fNew = true;
			}

			GetEventInfo( evtr );

			if( fNew )
			{
				org.orgEventResponseList.Add( evtr );
			}
			else
			{
				evtr.Update( acct );
			}
			ShowList = false;
		}

		protected void OnClickCancel(object sender, EventArgs e)
		{
			ShowList = false;
		}
	}
}