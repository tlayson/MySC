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
	public partial class EventDetails : System.Web.UI.UserControl
	{
		private void FillEventTypes()
		{
			ListItem li = new ListItem("---", "0");
			ddlEventType.Items.Add(li);

			li = new ListItem("Game", "1");
			ddlEventType.Items.Add(li);

			li = new ListItem("Practice", "2");
			ddlEventType.Items.Add(li);

			li = new ListItem("Practice Game", "3");
			ddlEventType.Items.Add(li);

			li = new ListItem("Tournament", "4");
			ddlEventType.Items.Add(li);

			li = new ListItem("Playoff", "5");
			ddlEventType.Items.Add(li);

			li = new ListItem("Race", "6");
			ddlEventType.Items.Add(li);

			li = new ListItem("Match", "7");
			ddlEventType.Items.Add(li);

			li = new ListItem("Meet", "8");
			ddlEventType.Items.Add(li);

			li = new ListItem("Jamboree", "9");
			ddlEventType.Items.Add(li);

			li = new ListItem("Ride", "10");
			ddlEventType.Items.Add(li);

			li = new ListItem("Workout", "11");
			ddlEventType.Items.Add(li);

			li = new ListItem("Meeting", "12");
			ddlEventType.Items.Add(li);

			li = new ListItem("Party", "13");
			ddlEventType.Items.Add(li);

			li = new ListItem("Other", "100");
			ddlEventType.Items.Add(li);
		}

		private void LoadSeasons( Organization org, Event evt )
		{
			//ddlSeason;
		}

		private void LoadVenues( Organization org, Event evt )
		{
			//ddlVenue;
		}
		
		private void EnableControls( bool fEdit )
		{
			ddlSeason.Enabled = fEdit;
			txtEventName.Enabled = fEdit;
			ddlEventType.Enabled = fEdit;
			ddlVenue.Enabled = fEdit;
			txtAltLocation.Enabled = fEdit;
			chkAltLocation.Enabled = fEdit;
			dsEventDate.Enable( fEdit );
			txtTime.Enabled = fEdit;
			txtOpponent.Enabled = fEdit;
			ddlHomeAway.Enabled = fEdit;
			txtUniform.Enabled = fEdit;
			txtResult.Enabled = fEdit;
			txtComments.Enabled = fEdit;
			txtWebsite.Enabled = fEdit;
			chkRequestResponse.Enabled = fEdit;
			chkSendReminder.Enabled = fEdit;
			btnDeleteEvent.Enabled = fEdit;
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			long lOrgID = ((USCPageBase)Page).GetSessionOrgID();
			USCPageBase pg = (USCPageBase)Page;
			USCMaster mstr = (USCMaster)pg.MasterPg;

			UserAccount acct = mstr.GetActiveUser();
			Organization org = mstr.g_OrgList.GetOrganization(lOrgID);

			FillEventTypes();
		}

		public void SetEventDetails( Organization org, Event evt, bool fEdit )
		{
			LoadSeasons( org, evt );
			LoadVenues( org, evt );

			txtEventName.Text = evt.EventName;
			txtAltLocation.Text = evt.AltLocation;
			txtTime.Text = evt.EventDate.ToShortTimeString();
			txtOpponent.Text = evt.Opponent;
			txtUniform.Text = evt.Uniform;
			txtResult.Text = evt.EventResult;
			txtComments.Text = evt.Comments;
			txtWebsite.Text = evt.URL;
			chkAltLocation.Checked = (evt.AltLocation.Length > 0);
			chkRequestResponse.Checked = evt.RequestResponse;
			chkSendReminder.Checked = evt.SendReminder;
			dsEventDate.SetDate( evt.EventDate );

			EnableControls( fEdit );
		}

		public void InitEvent( Event evt )
		{
			txtEventName.Text = evt.EventName;
		}

		protected void OnClickOK(object sender, EventArgs e)
		{

		}

		protected void OnClickCancel(object sender, EventArgs e)
		{

		}
	}
}