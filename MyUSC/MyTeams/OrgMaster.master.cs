using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Configuration;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Web.SessionState;
using MyUSC.Classes;
using MyUSC.Classes.MyOrg;

namespace MyUSC.MyTeams
{
	public enum MyTeamsPage
	{
		Dashboard = -1,
		Home = 0,
		Roster = 1,
		Schedule = 2,
		Messages = 3,
		Media = 4,
		EMail = 5,
		Venues = 7,
		Manage = 100
	};

	public partial class OrgMaster : USCMaster
	{
		protected MyTeamsPage m_selectedPage;

		protected OrgMaster()
		{
			InitOrgVariables();
		}

		protected void InitOrgVariables()
		{
			m_selectedPage = MyTeamsPage.Home;
		}

		protected override void OnPreRender(EventArgs e) 
		{

			// your code...
			base.OnPreRender(e);

			SetMenuSelection();
			SelectMenuItem(SelectedPage.Teams);
		}

		public void SelectMTMenuItemOnPageLoad(MyTeamsPage mtp)
		{
			m_selectedPage = mtp;
		}

		public void SelectMTMenuItem(MyTeamsPage mtp)
		{
			m_selectedPage = mtp;
			SetMenuSelection();
		}

		protected MenuItem FindMenuItem(string strMenuItem)
		{
			MenuItem mi = null;
			MenuItemCollection mic = mnuMyTeams.Items;
			foreach (MenuItem tmp in mic)
			{
				if( strMenuItem == tmp.Text )
				{
					mi = tmp;
					break;
				}
			}

			return mi;
		}

		protected void SetMenuSelection()
		{
			string strMenuItem = "Dashboard";
			switch( m_selectedPage )
			{
				case MyTeamsPage.Home:
					{
						strMenuItem = "Home";
						break;
					}
				case MyTeamsPage.Roster:
					{
						strMenuItem = "Roster";
						break;
					}
				case MyTeamsPage.Schedule:
					{
						strMenuItem = "Schedule";
						break;
					}
				case MyTeamsPage.Messages:
					{
						strMenuItem = "Messages";
						break;
					}
				case MyTeamsPage.Media:
					{
						strMenuItem = "Media";
						break;
					}
				case MyTeamsPage.EMail:
					{
						strMenuItem = "EMail";
						break;
					}
				case MyTeamsPage.Venues:
					{
						strMenuItem = "Fields/Venues";
						break;
					}
				case MyTeamsPage.Manage:
					{
						strMenuItem = "Manage";
						break;
					}
			}

			MenuItem mi = FindMenuItem( strMenuItem );
			if (null != mi)
			{
				mi.Selected = true;
			}
		}

		public string PageHeading
		{
			get
			{
				return this.lblPageHead.Text;
			}
			set
			{
				this.lblPageHead.Text = value;
			}
		}

		protected void HideMenu()
		{
			// Start at the bottom to preserve the index.
			//int nNumItems = mnuMyTeams.Items.Count;
			//while( nNumItems > 1 )
			//{
			//	mnuMyTeams.Items.RemoveAt( nNumItems-- );
			//}

			if (mnuMyTeams.Items.Count > 0)
			{

			  // Iterate through the root menu items in the Items collection.
			  foreach (MenuItem item in mnuMyTeams.Items)
			  {

				// Hide the menu items.
				if( item.Text != "Dashboard" )
				{
					item.Enabled = false;
				}

			  }

			}

		}

		protected void AppendOrgIDToMenuItems( long orgID )
		{
			string strAppend = "?OrgID=" + orgID.ToString();
			int nNumItems = mnuMyTeams.Items.Count;
			int nIndex = 1;
			while( nIndex < nNumItems )
			{
				mnuMyTeams.Items[nIndex++].NavigateUrl += strAppend;
			}
		}

		public void SetMenuState( long orgID, bool fHideMenu )
		{
			// TODO: Make this a loop
			//MenuItem miDash = mnuMyTeams.FindItem("Dashboard");
			//MenuItem miT = mnuMyTeams.

			//MenuItem miTemp = mnuMyTeams.FindItem("Home");
			//miTemp = mnuMyTeams.FindItem("Roster");
			//miTemp = mnuMyTeams.FindItem("Stats");

			UserAccount acct = GetActiveUser();
			AppendOrgIDToMenuItems( orgID );

			// Dashboard doesn't have Org Menu, so ignore
			if( fHideMenu )
			{
				HideMenu();
			}
			else
			{
				Organization org = GetOrgByID(orgID);
				OrgMember om = org.orgMemberList.GetOrgMember(acct.AccountID);
				if (null == om)
				{
					if (!org.AllowGuestViews)
					{
						// If no, send them to the dashboard
						Response.Redirect("~/MyTeams/Dashboard.aspx");
					}
					else
					{
						// Create a temporary member object
						om = new OrgMember();
						om.OrgID = orgID;
						om.MemberType = OrgAccessTypes.Guest;
						om.Note = "Temporary guest";
					}
				}

				// If someone has been banned from the org site, log it and send them away.
				if( om.MemberType == OrgAccessTypes.Banned )
				{
					// Alert
					// Log
					Response.Redirect("~/MyTeams/Dashboard.aspx");
				}

				//
				// Enable menu items
				//

				// For the remainder of the pages, if the page option is not found assume it is not available and remove it.
				// Start at the bottom to preserve the index.

				// Manage
				OrgPageOptions opo = org.orgPageOptionList.GetOrgPageOption( OrgPageID.Manage );
				if( null == opo || om.MemberType > opo.ViewLevel )
				{
					MenuItem mi = mnuMyTeams.FindItem("Manage");
					if( null != mi )
					{
						mnuMyTeams.Items.Remove( mi );
					}
					//mnuMyTeams.Items.RemoveAt(9);
				}

				// Venues
				opo = org.orgPageOptionList.GetOrgPageOption(OrgPageID.Venue);
				if (null == opo || om.MemberType > opo.ViewLevel || !opo.Visible )
				{
					MenuItem mi = mnuMyTeams.FindItem("Fields/Venues");
					if (null != mi)
					{
						mnuMyTeams.Items.Remove(mi);
					}
					//mnuMyTeams.Items.RemoveAt(8);
				}

				// Remove the stats page for now.
				if( true )
				{
					MenuItem mi = mnuMyTeams.FindItem("Stats");
					if (null != mi)
					{
						mnuMyTeams.Items.Remove(mi);
					}
					//mnuMyTeams.Items.RemoveAt(7);
				}

				// Email
				opo = org.orgPageOptionList.GetOrgPageOption(OrgPageID.Email);
				if (null == opo || om.MemberType > opo.ViewLevel || !opo.Visible)
				{
					MenuItem mi = mnuMyTeams.FindItem("eMail");
					if (null != mi)
					{
						mnuMyTeams.Items.Remove(mi);
					}
					//mnuMyTeams.Items.RemoveAt(6);
				}

				// Media
				opo = org.orgPageOptionList.GetOrgPageOption(OrgPageID.Media);
				if (null == opo || om.MemberType > opo.ViewLevel || !opo.Visible)
				{
					MenuItem mi = mnuMyTeams.FindItem("Media");
					if (null != mi)
					{
						mnuMyTeams.Items.Remove(mi);
					}
					//mnuMyTeams.Items.RemoveAt(5);
				}

				// Msg Board
				opo = org.orgPageOptionList.GetOrgPageOption(OrgPageID.MsgBoard);
				if (null == opo || om.MemberType > opo.ViewLevel || !opo.Visible)
				{
					MenuItem mi = mnuMyTeams.FindItem("Message Board");
					if (null != mi)
					{
						mnuMyTeams.Items.Remove(mi);
					}
					//mnuMyTeams.Items.RemoveAt(4);
				}

				// Schedule
				opo = org.orgPageOptionList.GetOrgPageOption(OrgPageID.Schedule);
				if (null == opo || om.MemberType > opo.ViewLevel || !opo.Visible)
				{
					MenuItem mi = mnuMyTeams.FindItem("Schedule");
					if (null != mi)
					{
						mnuMyTeams.Items.Remove(mi);
					}
					//mnuMyTeams.Items.RemoveAt(3);
				}

				// Roster
				opo = org.orgPageOptionList.GetOrgPageOption(OrgPageID.Roster);
				if (null == opo || om.MemberType > opo.ViewLevel || !opo.Visible)
				{
					MenuItem mi = mnuMyTeams.FindItem("Roster");
					if (null != mi)
					{
						mnuMyTeams.Items.Remove(mi);
					}
					//mnuMyTeams.Items.RemoveAt(2);
				}

				// Home
				opo = org.orgPageOptionList.GetOrgPageOption(OrgPageID.Home);
				if (null == opo || om.MemberType > opo.ViewLevel || !opo.Visible)
				{
					MenuItem mi = mnuMyTeams.FindItem("Home");
					if (null != mi)
					{
						mnuMyTeams.Items.Remove(mi);
					}
					//mnuMyTeams.Items.RemoveAt(1);
				}

			}
		}

		public bool SendOrgInvite()
		{
			bool fRet = true;
			return fRet;
		}

	}
}