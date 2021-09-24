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
		ToolboxData("<{0}:EditVenueTable ID='evdtID' runat=\"server\"> </{0}:EditVenueTable>")
	]
	public class EditVenueTable : Table
	{
#region Members
		private USCPageBase m_parentPage;
		private Label m_lblTitle;
		private TBSCTextBox m_txtVenueName;
		private Label m_lblVenueNameError;
		private TBSCTextBox m_txtVenueType;
		private TBSCTextBox m_txtVenueAddress;
		private TBSCTextBox m_txtVenueCity;
		private TBSCTextBox m_txtVenueState;
		private TBSCTextBox m_txtVenuePostalCode;
		private TBSCTextBox m_txtVenueCountry;
		private TBSCTextBox m_txtVenuePhone;
		private TBSCTextBox m_txtVenueWebsite;
		private TBSCTextBox m_txtVenueMapURL;
		private TBSCTextBox m_txtVenueNotes;
		private TBSCCheckBox m_chkMakePublic;

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
		public EditVenueTable(long lOrgID)
		{
			this.CssClass = "tblDlgControlOuter";
			this.Width = 650;

			AddTitleRow();
			AddNameRow();
			AddTypeRow();
			AddAddressRow();
			AddCityRow();
			AddStateRow();
			AddPostCodeRow();
			AddCountryRow();
			AddPhoneRow();
			AddWebsiteRow();
			AddMapURLRow();
			AddNotesRow();
			AddPublicRow();
			AddRequiredRow();

			AddButtonsRow( lOrgID );
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
			m_lblTitle.Text = "Venue";
			td1.Controls.Add( m_lblTitle );
			tr.Controls.Add( td1 );
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
			lbl.Text = "Venue Name : ";
			td1.Controls.Add( lbl );
			tr.Controls.Add( td1 );
			//Textbox
			TableCell td2 = new TableCell();
			td2.CssClass = "tdDlgControlNormal";
			m_txtVenueName = new TBSCTextBox();
			m_txtVenueName.MaxLength = 250;
			m_txtVenueName.Width = 250;
			td2.Controls.Add( m_txtVenueName );

			Label lblRequired = new Label();
			lblRequired.Text = "&nbsp;*";
			lblRequired.CssClass = "medErrorTxt";
			td2.Controls.Add( lblRequired );
			tr.Controls.Add( td2 );
			this.Controls.Add( tr );

			//Error code
			tr = new TableRow();
			tr.CssClass = "trDlgControlNormal";
			td1 = new TableCell();
			td1.ColumnSpan = 2;
			m_lblVenueNameError = new Label();
			m_lblVenueNameError.Text = "Please enter a venue name at least 5 characters long.";
			m_lblVenueNameError.CssClass = "smErrorTxt";
			m_lblVenueNameError.Visible = false;
			td1.Controls.Add( m_lblVenueNameError );
			tr.Controls.Add( td1 );
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
			lbl.Text = "Venue Type : ";
			td1.Controls.Add( lbl );
			tr.Controls.Add( td1 );
			//Textbox
			TableCell td2 = new TableCell();
			td2.CssClass = "tdDlgControlNormal";
			m_txtVenueType = new TBSCTextBox();
			m_txtVenueType.MaxLength = 250;
			m_txtVenueType.Width = 250;
			td2.Controls.Add(m_txtVenueType);
			tr.Controls.Add(td2);
			this.Controls.Add(tr);
		}

		protected void AddAddressRow()
		{
			//Type
			//Label
			TableRow tr = new TableRow();
			tr.CssClass = "trDlgControlNormal";
			TableCell td1 = new TableCell();
			td1.CssClass = "tdDlgControlNormal";
			Label lbl = new Label();
			lbl.Text = "Address : ";
			td1.Controls.Add(lbl);
			tr.Controls.Add(td1);
			//Textbox
			TableCell td2 = new TableCell();
			td2.CssClass = "tdDlgControlNormal";
			m_txtVenueAddress = new TBSCTextBox();
			m_txtVenueAddress.MaxLength = 250;
			m_txtVenueAddress.Width = 250;
			td2.Controls.Add(m_txtVenueAddress);
			tr.Controls.Add(td2);
			this.Controls.Add(tr);
		}

		protected void AddCityRow()
		{
			//Type
			//Label
			TableRow tr = new TableRow();
			tr.CssClass = "trDlgControlNormal";
			TableCell td1 = new TableCell();
			td1.CssClass = "tdDlgControlNormal";
			Label lbl = new Label();
			lbl.Text = "City : ";
			td1.Controls.Add(lbl);
			tr.Controls.Add(td1);
			//Textbox
			TableCell td2 = new TableCell();
			td2.CssClass = "tdDlgControlNormal";
			m_txtVenueCity = new TBSCTextBox();
			m_txtVenueCity.MaxLength = 250;
			m_txtVenueCity.Width = 250;
			td2.Controls.Add(m_txtVenueCity);
			tr.Controls.Add(td2);
			this.Controls.Add(tr);
		}

		protected void AddStateRow()
		{
			//Type
			//Label
			TableRow tr = new TableRow();
			tr.CssClass = "trDlgControlNormal";
			TableCell td1 = new TableCell();
			td1.CssClass = "tdDlgControlNormal";
			Label lbl = new Label();
			lbl.Text = "State : ";
			td1.Controls.Add(lbl);
			tr.Controls.Add(td1);
			//Textbox
			TableCell td2 = new TableCell();
			td2.CssClass = "tdDlgControlNormal";
			m_txtVenueState = new TBSCTextBox();
			m_txtVenueState.MaxLength = 250;
			m_txtVenueState.Width = 250;
			td2.Controls.Add(m_txtVenueState);
			tr.Controls.Add(td2);
			this.Controls.Add(tr);
		}

		protected void AddPostCodeRow()
		{
			//Type
			//Label
			TableRow tr = new TableRow();
			tr.CssClass = "trDlgControlNormal";
			TableCell td1 = new TableCell();
			td1.CssClass = "tdDlgControlNormal";
			Label lbl = new Label();
			lbl.Text = "Postal Code : ";
			td1.Controls.Add(lbl);
			tr.Controls.Add(td1);
			//Textbox
			TableCell td2 = new TableCell();
			td2.CssClass = "tdDlgControlNormal";
			m_txtVenuePostalCode = new TBSCTextBox();
			m_txtVenuePostalCode.MaxLength = 250;
			m_txtVenuePostalCode.Width = 250;
			td2.Controls.Add(m_txtVenuePostalCode);
			tr.Controls.Add(td2);
			this.Controls.Add(tr);
		}

		protected void AddCountryRow()
		{
			//Type
			//Label
			TableRow tr = new TableRow();
			tr.CssClass = "trDlgControlNormal";
			TableCell td1 = new TableCell();
			td1.CssClass = "tdDlgControlNormal";
			Label lbl = new Label();
			lbl.Text = "Country : ";
			td1.Controls.Add(lbl);
			tr.Controls.Add(td1);
			//Textbox
			TableCell td2 = new TableCell();
			td2.CssClass = "tdDlgControlNormal";
			m_txtVenueCountry = new TBSCTextBox();
			m_txtVenueCountry.MaxLength = 250;
			m_txtVenueCountry.Width = 250;
			td2.Controls.Add(m_txtVenueCountry);
			tr.Controls.Add(td2);
			this.Controls.Add(tr);
		}

		protected void AddPhoneRow()
		{
			//Type
			//Label
			TableRow tr = new TableRow();
			tr.CssClass = "trDlgControlNormal";
			TableCell td1 = new TableCell();
			td1.CssClass = "tdDlgControlNormal";
			Label lbl = new Label();
			lbl.Text = "Phone : ";
			td1.Controls.Add(lbl);
			tr.Controls.Add(td1);
			//Textbox
			TableCell td2 = new TableCell();
			td2.CssClass = "tdDlgControlNormal";
			m_txtVenuePhone = new TBSCTextBox();
			m_txtVenuePhone.MaxLength = 250;
			m_txtVenuePhone.Width = 250;
			td2.Controls.Add(m_txtVenuePhone);
			tr.Controls.Add(td2);
			this.Controls.Add(tr);
		}

		protected void AddWebsiteRow()
		{
			//Type
			//Label
			TableRow tr = new TableRow();
			tr.CssClass = "trDlgControlNormal";
			TableCell td1 = new TableCell();
			td1.CssClass = "tdDlgControlNormal";
			Label lbl = new Label();
			lbl.Text = "Website : ";
			td1.Controls.Add(lbl);
			tr.Controls.Add(td1);
			//Textbox
			TableCell td2 = new TableCell();
			td2.CssClass = "tdDlgControlNormal";
			m_txtVenueWebsite = new TBSCTextBox();
			m_txtVenueWebsite.MaxLength = 250;
			m_txtVenueWebsite.Width = 250;
			td2.Controls.Add(m_txtVenueWebsite);
			tr.Controls.Add(td2);
			this.Controls.Add(tr);
		}

		protected void AddMapURLRow()
		{
			//Type
			//Label
			TableRow tr = new TableRow();
			tr.CssClass = "trDlgControlNormal";
			TableCell td1 = new TableCell();
			td1.CssClass = "tdDlgControlNormal";
			Label lbl = new Label();
			lbl.Text = "Map URL : ";
			td1.Controls.Add(lbl);
			tr.Controls.Add(td1);
			//Textbox
			TableCell td2 = new TableCell();
			td2.CssClass = "tdDlgControlNormal";
			m_txtVenueMapURL = new TBSCTextBox();
			m_txtVenueMapURL.MaxLength = 250;
			m_txtVenueMapURL.Width = 250;
			td2.Controls.Add(m_txtVenueMapURL);
			tr.Controls.Add(td2);
			this.Controls.Add(tr);
		}

		protected void AddNotesRow()
		{
			//Type
			//Label
			TableRow tr = new TableRow();
			tr.CssClass = "trDlgControlNormal";
			TableCell td1 = new TableCell();
			td1.CssClass = "tdDlgControlNormal";
			Label lbl = new Label();
			lbl.Text = "Notes : ";
			td1.Controls.Add(lbl);
			tr.Controls.Add(td1);
			//Textbox
			TableCell td2 = new TableCell();
			td2.CssClass = "tdDlgControlNormal";
			m_txtVenueNotes = new TBSCTextBox();
			m_txtVenueNotes.MaxLength = 250;
			m_txtVenueNotes.Width = 250;
			td2.Controls.Add(m_txtVenueNotes);
			tr.Controls.Add(td2);
			this.Controls.Add(tr);
		}

		protected void AddPublicRow()
		{
			//Type
			//Label
			TableRow tr = new TableRow();
			tr.CssClass = "trDlgControlNormal";
			TableCell td1 = new TableCell();
			td1.CssClass = "tdDlgControlNormal";
			Label lbl = new Label();
			lbl.Text = "Public Venue : ";
			td1.Controls.Add(lbl);
			tr.Controls.Add(td1);
			//Textbox
			TableCell td2 = new TableCell();
			td2.CssClass = "tdDlgControlNormal";
			m_chkMakePublic = new TBSCCheckBox();
			td2.Controls.Add(m_chkMakePublic);
			tr.Controls.Add(td2);
			this.Controls.Add(tr);
		}

		protected void AddButtonsRow( long lOrgID )
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
			m_btnOK.UserValue1 = lOrgID;
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

		protected void AddRequiredRow()
		{
			TableRow tr = new TableRow();
			tr.CssClass = "trDlgControlNormal";

			TableCell td1 = new TableCell();
			td1.CssClass = "tdError";
			td1.ColumnSpan = 2;

			Label lbl = new Label();
			lbl.Text = "* Indicates a required field.";
			td1.Controls.Add(lbl);

			tr.Controls.Add( td1 );
			this.Controls.Add( tr );
		}

#endregion

#region SetValues
		public void SetVenueDetails(USCPageBase parentPage, Organization org, Venue venue, UserAccount acct, bool fNew)
		{
			m_parentPage = parentPage;
			if (fNew)
			{
				m_btnOK.CommandName = "new";
				m_btnOK.UserData = org;
				m_btnOK.UserAcct = acct;
			}
			else
			{
				m_btnOK.CommandName = "edit";
				m_btnOK.UserData = venue;
				m_btnOK.UserAcct = acct;
			}

			if (null != venue)
			{
				m_txtVenueName.Text = venue.VenueName;
				m_txtVenueType.Text = venue.VenueType;
				m_txtVenueAddress.Text = venue.Address1;
				m_txtVenueCity.Text = venue.City;
				m_txtVenueState.Text = venue.State;
				m_txtVenuePostalCode.Text = venue.Zip;
				m_txtVenueCountry.Text = venue.Country;

				m_txtVenuePhone.Text = venue.Phone;
				m_txtVenueWebsite.Text = venue.URL;
				m_txtVenueMapURL.Text = venue.MapURL;
				m_txtVenueNotes.Text = venue.Note;

				m_chkMakePublic.Checked = venue.PublicVenue;
			}
			else
			{
				m_chkMakePublic.Checked = true;
			}

		}
#endregion

#region Handlers
		protected bool GetValues( Venue venue )
		{
			bool fRet= true;
			venue.VenueName = m_txtVenueName.Text.Trim();
			if( venue.VenueName.Length < 5 )
			{
				fRet = false;
				m_lblVenueNameError.Visible = true;
			}
			else
			{
				venue.VenueType = m_txtVenueType.Text;
				venue.Address1 = m_txtVenueAddress.Text;
				venue.City = m_txtVenueCity.Text;
				venue.State = m_txtVenueState.Text;
				venue.Zip = m_txtVenuePostalCode.Text;
				venue.Country = m_txtVenueCountry.Text;
				venue.Phone = m_txtVenuePhone.Text;
				venue.URL = m_txtVenueWebsite.Text;
				venue.MapURL = m_txtVenueMapURL.Text;
				venue.Note = m_txtVenueNotes.Text;

				venue.PublicVenue = m_chkMakePublic.Checked;
			}

			return fRet;
		}

		protected void OnClickOK(object sender, EventArgs e)
		{
			TBSCButton btn = (TBSCButton)sender;
			UserAccount acct = (UserAccount)btn.UserAcct;
			long lOrgID = m_btnOK.UserValue1;
			Venue venue = null;
			bool fNew = (btn.CommandName == "new");
			USCMaster mstrPage = (USCMaster)m_parentPage.Master;
			if( fNew )
			{
				venue = new Venue();
				venue.OrgID = lOrgID;
				venue.Creator = acct.UserName;
				venue.OwnerID = acct.AccountID;
			}
			else
			{
				venue = (Venue)m_btnOK.UserData;
			}

			if( null != venue && GetValues( venue ) )
			{
				if( fNew )
				{
					if( mstrPage.g_VenueList.Add( venue ) )
					{
						Organization org = mstrPage.g_OrgList.GetOrganization( lOrgID );
						if( null != org )
						{
							OrgVenuePairing ovp = new OrgVenuePairing();
							ovp.VenueID = venue.VenueID;
							ovp.OrgID = venue.OrgID;
							ovp.HideVenue = false;

							org.orgVenueList.AddVenueUsage( ovp, acct );
						}
					}
				}
				else
				{
					venue.Update(acct);
				}
				
				m_parentPage.ProcessChildClick(1);
			}
			else
			{
				m_lblVenueNameError.Visible = true;
				m_txtVenueName.Focus();
				mstrPage.AlertUser( "Please enter a valid venue name." );
			}
		}

		protected void OnClickCancel(object sender, EventArgs e)
		{
			m_parentPage.ProcessChildClick(1);
		}
#endregion
	}
}