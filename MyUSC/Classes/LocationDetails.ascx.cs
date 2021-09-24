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
	public partial class LocationDetails : System.Web.UI.UserControl
	{
		private long m_lOrgID;
		private long m_lVenueID;

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
			USCMaster mscMaster = (USCMaster)Page.Master;

			if( !IsPostBack )
			{
				OrganizationList ol = mscMaster.g_OrgList;
				Organization org = ol.GetOrganization(m_lOrgID);
				Venue venue = null;

				if( m_lVenueID > 0 )
				{
					VenueList vl = mscMaster.g_VenueList;
					venue = vl.GetVenueByID( m_lVenueID );
					if( null != venue )
					{
						SetDataValues( venue );
					}

				}
				else
				{
					lblTitle.Text = "New Venue";
				}

				EnableControls( venue );
			}
		}

		protected void SetDataValues( Venue venue )
		{
			if( null != venue )
			{
				if( lblTitle.Text.Length == 0 )
				{
					lblTitle.Text = venue.VenueName;
				}
				txtLocName.Text = venue.VenueName;
				txtLocAddress.Text = venue.Address1;
				txtLocAddress2.Text = venue.Address2;
				txtLocCity.Text = venue.City;
				txtLocZip.Text = venue.Zip;
				txtLocPhone.Text = venue.Phone;
				txtLocMapURL.Text = venue.MapURL;
				txtLocWebsite.Text = venue.URL;
				txtLocNotes.Text = venue.Note;
				chkPublicVenue.Checked = venue.PublicVenue;
				
				// Set drop downs

			}

		}

		protected void SetControlsRO( bool fRO )
		{
			txtLocName.ReadOnly = fRO;
			txtLocAddress.ReadOnly = fRO;
			txtLocAddress2.ReadOnly = fRO;
			txtLocCity.ReadOnly = fRO;
			txtLocZip.ReadOnly = fRO;
			txtLocPhone.ReadOnly = fRO;
			txtLocMapURL.ReadOnly = fRO;
			txtLocWebsite.ReadOnly = fRO;
			txtLocNotes.ReadOnly = fRO;

			chkPublicVenue.Enabled = !fRO;
			ddlLocCountry.Enabled = !fRO;
			ddlLocState.Enabled = !fRO;
		}
		
		protected void EnableControls( Venue venue )
		{
			if( null != venue )
			{
				bool fOwner = false;
				//Determine if this is the owner


				btnDelete.Enabled = false;
				btnDelete.Visible = false;
				if( fOwner )
				{
					if( !venue.PublicVenue )
					{
						// Can't delete it once it's public
						btnDelete.Enabled = true;
						btnDelete.Visible = true;
					}
					SetControlsRO( false );
				}
				else
				{
					SetControlsRO( true );
					
				}
			}
			else //new venue
			{
				SetControlsRO( false );
			}
		}

		protected void OnClickOK(object sender, EventArgs e)
		{

		}

		protected void OnClickCancel(object sender, EventArgs e)
		{

		}

		protected void OnClickDelete(object sender, EventArgs e)
		{

		}
	}
}