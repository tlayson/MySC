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
	public partial class VenueDetails : USCPageBase
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			Master.PageHeading = "";
			string strOrgID = Request.QueryString["OrgID"];
			string strVenueID = Request.QueryString["VID"];
			long lOrgID = 0;
			long lVenueID = 0;

			UserAccount acct = Master.GetActiveUser();
			if (null == acct)
			{
				SetSessionReturnURL("~/MyTeams/Dashboard.aspx");
				RedirectToLoginPage();
			}

			if (!IsPostBack)
			{
				ddlVenueType.LoadVenueTypes();
				ddlState.LoadStates();

				if (null != strOrgID && long.TryParse(strOrgID, out lOrgID))
				{
					Master.SetMenuState(lOrgID, false);

					if (UserHasViewAccess(acct.AccountID, lOrgID, OrgPageID.Manage))
					{
						SetSessionOrgID(lOrgID);
						Organization org = (Organization)Master.g_OrgList.htOrgList[lOrgID];
						if (!org.ListsLoaded)
						{
							org.LoadLists();
						}

						if (!IsPostBack && null != org)
						{
							EnablePageOptions(acct.AccountID, lOrgID);
							if (strVenueID.Length > 0 && strVenueID != "0" && long.TryParse(strVenueID, out lVenueID))
							{
								SetSessionValue1( lVenueID );
								DisplayVenueDetails( lVenueID );
								DisableEdit();
							}
							else
							{
								// Create a new venue
								EnableEdit();
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
				Master.SelectMTMenuItemOnPageLoad(MyTeamsPage.Venues);
			}
		}

		protected void EnablePageOptions(long acctID, long orgID)
		{
			if (!UserHasMemberAccess(acctID, orgID, OrgPageID.Manage))
			{
			}

			if (!UserHasEditAccess(acctID, orgID, OrgPageID.Manage))
			{
				pnlDisplay.Visible = false;
				pnlAdmin.Visible = true;
			}
			else
			{
				pnlDisplay.Visible = false;
				pnlAdmin.Visible = true;
			}

			if (UserHasAdminAccess(acctID, orgID, OrgPageID.Manage))
			{
			}
		}

		protected void DisplayVenueDetails( long vid )
		{
			Venue venue = Master.g_VenueList.GetVenueByID( vid );
			if( null != venue )
			{
				Master.PageHeading = venue.VenueName;
				if (pnlDisplay.Visible)
				{
					lblViewName.Text = venue.VenueName;
					lblViewAddress.Text = venue.Address1;
					lblViewType.Text = venue.VenueType;

					string strCity = venue.City;
					if( strCity.Length > 0 && venue.State.Length > 0 )
					{
						strCity += ", " + venue.State;
					}
					if( venue.Zip.Length > 0 )
					{
						strCity += " " + venue.Zip;
					}
					if (venue.Country.Length > 0)
					{
						strCity += " " + venue.Country;
					}
					lblViewCity.Text = strCity;

					if (venue.MapURL.Length > 0)
					{
						hlViewMap.Text = "View Map";
						hlViewMap.NavigateUrl = venue.MapURL;
					}

					lblViewPhone.Text = venue.Phone;
					txtViewNotes.Text = "";
				}
				else
				{
					txtEditVenueName.Text = venue.VenueName;
					txtEditVenueAddress.Text = venue.Address1;
					txtEditDisplayLoc.Text = venue.DisplayLocation;
					txtEditVenueCity.Text = venue.City;
					txtEditZip.Text = venue.Zip;
					txtEditMapURL.Text = venue.MapURL;
					txtEditWebsite.Text = venue.URL;
					txtEditImageURL.Text = venue.ImageURL;
					txtEditPhone.Text = venue.Phone;
					txtEditNotes.Text = venue.Note;

					chkMakePublic.Checked = venue.PublicVenue;
					ddlVenueType.SelectedValue = venue.VenueType;
					ddlState.SelectedValue = venue.State;
					ddlCountry.SelectedValue = venue.Country;
				}
			}
			else
			{
				// New venue
				Master.PageHeading = "New Venue";
			}

		}

		protected void OnClickNewVenue(object sender, EventArgs e)
		{
			string strReturnURL = "~/MyTeams/VenueDetails.aspx?OrgID=" + GetSessionOrgID();
			Response.Redirect(strReturnURL);
		}

		private void EnableEdit()
		{
			txtEditVenueName.ReadOnly = false;
			txtEditVenueAddress.ReadOnly = false;
			txtEditVenueCity.ReadOnly = false;
			txtEditDisplayLoc.ReadOnly = false;
			txtEditVenueCity.ReadOnly = false;
			txtEditZip.ReadOnly = false;
			txtEditMapURL.ReadOnly = false;
			txtEditWebsite.ReadOnly = false;
			txtEditImageURL.ReadOnly = false;
			txtEditPhone.ReadOnly = false;
			txtEditNotes.ReadOnly = false;

			// TODO: if the venue is public and any other org has a reference to it then we can't delete it.
			// We can only hide it.
			if( true )
			{
				chkMakePublic.Enabled = true;
			}
			ddlVenueType.Enabled = true;
			ddlCountry.Enabled = true;
			ddlState.Enabled = true;

			btnCancel.Visible  = true;
			btnSave.Visible = true;
			btnEdit.Visible = false;
			btnNew.Visible = false;
		}

		private void  DisableEdit()
		{
			txtEditVenueName.ReadOnly = true;
			txtEditVenueAddress.ReadOnly = true;
			txtEditVenueCity.ReadOnly = true;
			txtEditDisplayLoc.ReadOnly = true;
			txtEditVenueCity.ReadOnly = true;
			txtEditZip.ReadOnly = true;
			txtEditMapURL.ReadOnly = true;
			txtEditWebsite.ReadOnly = true;
			txtEditImageURL.ReadOnly = true;
			txtEditPhone.ReadOnly = true;
			txtEditNotes.ReadOnly = true;

			chkMakePublic.Enabled = false;
			ddlVenueType.Enabled = false;
			ddlCountry.Enabled = false;
			ddlState.Enabled = false;

			btnCancel.Visible = false;
			btnSave.Visible = false;
			btnEdit.Visible = true;
			btnNew.Visible = true;
		}

		protected void OnClickEditVenue(object sender, EventArgs e)
		{
			EnableEdit();
		}

		protected void GetVenueDetails( Venue venue )
		{
			venue.VenueName = txtEditVenueName.Text;
			venue.Address1 = txtEditVenueAddress.Text;
			venue.DisplayLocation = txtEditDisplayLoc.Text;
			venue.City = txtEditVenueCity.Text;
			venue.Zip = txtEditZip.Text;
			venue.MapURL = txtEditMapURL.Text;
			venue.URL = txtEditWebsite.Text;
			venue.ImageURL = txtEditImageURL.Text;
			venue.Phone = txtEditPhone.Text;
			venue.Note = txtEditNotes.Text;

			venue.PublicVenue = chkMakePublic.Checked;
			venue.VenueType = ddlVenueType.SelectedValue;
			venue.State = ddlState.SelectedValue;
			venue.Country = ddlCountry.SelectedValue;
		}

		protected void OnClickSave(object sender, EventArgs e)
		{
			UserAccount acct = GetActiveUser();
			long lOrgID = GetSessionOrgID();
			string strVenueID = Request.QueryString["VID"];
			long lVenueID = 0;
			Venue venue = null;
			if (strVenueID.Length > 0 && strVenueID != "0" && long.TryParse(strVenueID, out lVenueID))
			{
				venue = Master.g_VenueList.GetVenueByID(lVenueID);
				if( null == venue )
				{
					// TODO: Add error handeling
					return;
				}
			}
			else
			{
				venue = new Venue();
			}
			GetVenueDetails( venue );

			if( venue.VenueID > 0 )
			{
				venue.Update( acct );
			}
			else
			{
				if( Master.g_VenueList.Add( venue ) )
				{
					OrgVenuePairing ovp = new OrgVenuePairing();
					ovp.OrgID = lOrgID;
					ovp.VenueID = venue.VenueID;
					Organization org = GetOrgByID(lOrgID);
					org.orgVenueList.AddVenueUsage(ovp, acct);
				}
				else
				{
					// TODO: Add error handeling
				}
			}

			DisableEdit();
		}

		protected void OnClickCancel(object sender, EventArgs e)
		{
			DisableEdit();
		}

		protected void OnClickBack(object sender, EventArgs e)
		{
			string strReturnURL = GetSessionReturnURL();
			if( 0 >= strReturnURL.Length )
			{
				strReturnURL = "~/MyTeams/Dashboard.aspx";
			}

			Response.Redirect( strReturnURL );

		}
	}
}