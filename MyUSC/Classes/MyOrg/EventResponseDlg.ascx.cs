using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using MyUSC.Classes;
using MyUSC.Classes.MyOrg;

namespace MyUSC.Classes.MyOrg
{
	public partial class EventResponseDlg : System.Web.UI.UserControl
	{
		private long m_lOrgID;
		private long m_lEventID;
		private long m_lVenueID;
		public TBSCPanel m_pnlResponse;
		public Table m_tblResponse;
		private RadioButtonList m_rblChoice;
		private TextBox m_txtNotes;

		public EventResponseDlg()
		{
			m_pnlResponse = new TBSCPanel();
			m_pnlResponse.Visible = true;

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

			m_pnlResponse.Controls.Add( m_tblResponse );
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

/*
<MSC:TBSCPanel ID="pnlRDlg" runat="server" Visible="False">
	<table style="width: 350px" class="tblDlgControlOuter">
	<tr class="trDlgControlTitle">
		<td class="tdDlgControlNormal" colspan="2">
			<asp:Label ID="lblTitle" runat="server" Text="RESPONSE"></asp:Label>
		</td>
	</tr>
	<tr class="trDlgControlNormal">
		<td class="tdDlgControlNormal" colspan="2">
			<asp:Label ID="lblDateTime" runat="server" Text="EventDate"></asp:Label>
		</td>
	</tr>
	<tr class="trDlgControlNormal">
		<td class="tdDlgControlNormal" colspan="2">
			<asp:Label ID="lblEvent" runat="server" Text="Event"></asp:Label>
		</td>
	</tr>
	<tr class="trDlgControlNormal">
		<td class="tdDlgControlNormal" colspan="2">
			<asp:Label ID="lblVenue" runat="server" Text="Venue"></asp:Label>
		</td>
	</tr>
	<tr>
		<td colspan="2">
			<hr />
		</td>
	</tr>
*/

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
/* 
		   <tr class="trDlgControlNormal">
				<td class="tdDlgControlNormal" colspan="2">
					<asp:Label ID="lblQuestion" runat="server" Text="Will you be there?"></asp:Label>
				</td>
			</tr>
			<tr class="trDlgControlNormal">
				<td class="tdDlgControlNormal" colspan="2">
					<asp:RadioButtonList ID="rblResponse" runat="server" AutoPostBack="True">
						<asp:ListItem Text="Yes" Value="Yes"></asp:ListItem>
						<asp:ListItem Text="No" Value="No"></asp:ListItem>
						<asp:ListItem Text="Maybe" Value="Maybe"></asp:ListItem>
					</asp:RadioButtonList>
				</td>
			</tr>
			<tr class="trDlgControlNormal">
				<td class="tdDlgControlNormal" colspan="2">
					<asp:Label ID="Label3" runat="server" Text="Notes:"></asp:Label>
				</td>
			</tr>
			<tr class="trDlgControlNormal">
				<td class="tdDlgControlNormal" colspan="2">
					<asp:TextBox ID="txtNotes" runat="server" Width="215px" Height="75px" TextMode="MultiLine" MaxLength="200"></asp:TextBox>
				</td>
			</tr>
			<tr class="trDlgControlNormal">
				<td class="tdRight">
					<asp:Button ID="btnOK" runat="server" Text="OK" OnClick="OnClickOK" />
				</td>
				<td class="tdLeft">
					<asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="OnClickCancel" />
				</td>
			</tr>
		</table>
		</MSC:TBSCPanel>
		*/
		
		public bool ShowList
		{
			get
			{
				return this.m_pnlResponse.Visible;
			}
			set
			{
				this.m_pnlResponse.Visible = value;
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

		protected void Page_Load(object sender, EventArgs e)
		{
		}

		protected void GetEventInfo( EventResponse evtr )
		{
			evtr.SetResponseFromString( m_rblChoice.SelectedValue );
			evtr.Notes = m_txtNotes.Text;
		}

		protected void OnClickOK(object sender, EventArgs e)
		{
			bool fNew = false;
			TBSCButton btn = (TBSCButton)sender;
			USCPageBase parentPage = (USCPageBase)btn.UserData;
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