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

namespace MyUSC.Classes
{
	public partial class TBSCResponseDlg : System.Web.UI.UserControl
	{
		private long m_lOrgID;
		private long m_lEventID;
		private long m_lVenueID;
		//private string m_cnx = "";

		public TBSCResponseDlg()
		{
			
		}

		protected void SetResponse( EventResponse evtr )
		{
			rblResponse.ClearSelection();
			rblResponse.SelectedValue = evtr.ResponseTypeToString();
		}

		public void SetValues( long eventID, long acctID )
		{
			USCMaster mscMaster = (USCMaster)Page.Master;
			UserAccount acct = ((USCPageBase)Page).GetActiveUser();
			Organization org = mscMaster.g_OrgList.GetOrganization(m_lOrgID);
			Event evt = org.orgEventList.GetEvent( eventID );
			EventResponse evtr = org.orgEventResponseList.GetEventResponse( eventID, acctID );
			if( lblTitle.Text.Length == 0 )
			{
				lblTitle.Text = org.OrgName;
			}

			EventID = evt.EventID;
			VenueID = evt.VenueID;
			OrgID = evt.OrgID;

			if( null != evtr )
			{
				//Response selection
				SetResponse( evtr );

				txtNotes.Text = evtr.Notes;
			}

			//Date
			string strDate = evt.EventDate.ToShortDateString();
			strDate += " - ";
			strDate = evt.EventDate.ToShortTimeString();
			lblDateTime.Text = strDate;

			//Event
			lblEvent.Text = evt.EventName;

			// Venue
			Venue venue = mscMaster.g_VenueList.GetVenueByID( evt.VenueID );
			if( null == venue )
			{
				lblVenue.Text = "TBD";
			}
			else
			{
				lblVenue.Text = venue.VenueName;
			}

			ShowList = true;
		}

		public bool ShowList
		{
			get
			{
				return this.pnlRDlg.Visible;
			}
			set
			{
				this.pnlRDlg.Visible = value;
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

		public string DlgTitle
		{
			get
			{
				return lblTitle.Text;
			}
			set
			{
				lblTitle.Text = value;
			}
		}

		protected void Page_Load(object sender, EventArgs e)
		{
		}

		protected void GetEventInfo( EventResponse evtr )
		{
			evtr.SetResponseFromString( rblResponse.SelectedValue );
			evtr.Notes = txtNotes.Text;
		}

		protected void OnClickOK(object sender, EventArgs e)
		{
			bool fNew = false;
			USCMaster mscMaster = (USCMaster)Page.Master;
			UserAccount acct = ((USCPageBase)Page).GetActiveUser();
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