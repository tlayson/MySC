using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MyUSC.Classes;

namespace MyUSC
{

	public partial class AccountPage : USCPageBase
	{
		enum Sections { Name, Address, Sports, Prefs, Photo }

#region ENUM FavSports
		enum FavSports
		{
			CFL = 1,
			Extreme = 2,
			LPGA = 3,
			MLB = 4,
			MLS = 5,
			NASCAR = 6,
			NBA = 7,
			NCAAFootball = 8,
			NCAASoftball = 9,
			NFL = 10,
			NHL = 11,
			Olympics = 12,
			Other = 13,
			PBA = 14,
			PGA = 15,
			UFC = 16,
			WNBA = 17,
			YouthSports = 18,
			MAX = 18
		}
#endregion

		protected AccountPage()
		{
			_PAGENAME = "Account";
			_pgUSCMaster = (USCMaster)Master;
		}

		protected void Page_Load(object sender, EventArgs e)
		{

			// One time settings
			if (!IsPostBack)
			{
				PopulateDropDownLists();

				SetInitialValues();

				// Get the users account
				UserAccount acct = null;
				long lCheckVal = GetSessionValue1();
				if (99 == lCheckVal)
				{
					long lInviteeID = GetSessionUserID();
					acct = GetUserByID( lInviteeID );
					tblNewInvitee.Visible = true;
				}
				else
				{
					IsUserLoggedIn(true);
					acct = GetUserAccount();
					tblNewInvitee.Visible = false;
				}
				SetDataValues( acct );
				DisplaySections(Sections.Name);
				Master.SelectMenuItem(SelectedPage.Profile);
				lblMessage.Visible = false;
				string strMessage = GetSessionMessage();
				if( strMessage.Length > 0 )
				{
					lblMessage.Text = strMessage;
					lblMessage.Visible = true;
					SetSessionMessage("");
				}

			}
		}

#region SetInitialValues
		private void SetInitialValues()
		{
			// Name panel
			txtUserName.Text = "";
			txtSecurityQuestion.Text = "";
			txtSecurityAnswer.Text = "";
			txtTitle.Text = "";
			txtFirstName.Text = "";
			txtMI.Text = "";
			txtLastName.Text = "";
			txtSuffix.Text = "";
			txtEmail.Text = "";
			txtBirthDate.Text = "";

			// Address panel
			txtAddress1.Text = "";
			txtAddress2.Text = "";
			txtCity.Text = "";
			txtPostalCode.Text = "";
			ddlState.SelectedIndex = -1;
			ddlCountry.SelectedIndex = -1;

			// Favorite Sports panel
			txtSportsInterests.Text = "";
			chkAcctCFL.Checked = false;
			chkAcctExtremeSports.Checked = false;
			chkAcctLPGA.Checked = false;
			chkAcctMLB.Checked = false;
			chkAcctMLS.Checked = false;
			chkAcctNASCAR.Checked = false;
			chkAcctNBA.Checked = false;
			chkAcctNCAAFootball.Checked = false;
			chkAcctNCAASoftball.Checked = false;
			chkAcctNFL.Checked = false;
			chkAcctNHL.Checked = false;
			chkAcctOlmpics.Checked = false;
			chkAcctOther.Checked = false;
			chkAcctPBA.Checked = false;
			chkAcctPGA.Checked = false;
			chkAcctUFC.Checked = false;
			chkAcctWNBA.Checked = false;
			chkAcctYouthSports.Checked = false;

			// Preferences panel
			chkAcctFriends.Checked = true;
			chkAcctSportsNews.Checked = false;
			chkDisableDeleteFriends.Checked = false;
			chkDisableDeleteFriendsMsgs.Checked = false;
			chkReceiveCommentEmails.Checked = false;
			chkOffersFromUs.Checked = false;
			chkOffersFromPartners.Checked = false;

			// Photo panel

		}
#endregion

#region SetDataValues
		private void SetDataValues( UserAccount acct )
		{
			// Set the values
			if( null == acct )
			{
				RedirectToLoginPage();
			}
			else
			{
				// Name panel
				txtUserName.Text = acct.UserName;
				// Leave password and confirmation blank
				//txtPassword.Text = "";
				//txtPswdConfirm.Text = "";
				txtSecurityQuestion.Text = acct.SecurityQuestion;
				txtSecurityAnswer.Text = acct.SecurityAnswer;
				txtTitle.Text = acct.Title;
				txtFirstName.Text = acct.First;
				txtMI.Text = acct.MI;
				txtLastName.Text = acct.Last;
				txtSuffix.Text = acct.Suffix;
				txtEmail.Text = acct.Email;
				txtBirthDate.Text = acct.BirthDate;
				dsBirthDay.SetDate( acct.BirthDate );

				// Address panel
				txtAddress1.Text = acct.Address1;
				txtAddress2.Text = acct.Address2;
				txtCity.Text = acct.City;
				txtPostalCode.Text = acct.Zip;
				// TODO: Set correct indexes
				ddlState.SelectedIndex = -1;
				ddlCountry.SelectedIndex = -1;

				// Favorite Sports panel
				txtSportsInterests.Text = acct.Preferences.Interests;

				string strFavSports = acct.Preferences.NewsSubjects;
				if( strFavSports.Length > 0 )
				{
					List<string> lstSports = new List<string>();
					lstSports = strFavSports.Trim().Split(',').ToList();

					for (int intCounter = 0; intCounter < lstSports.Count; intCounter++)
					{
						string strT = lstSports[intCounter].Trim();
						if( strT.Length > 0 )
						{
							int selection = Convert.ToInt32(strT);
							FavSports fs = (FavSports)selection;
							if (FavSports.MAX >= fs)
							{
								CheckSportBox(fs);
							}
						}
					}
				}

				// Preferences panel
				if( "friends" == acct.DefaultPage.ToLower() )
				{
					chkAcctFriends.Checked = true;
					chkAcctSportsNews.Checked = false;
				}
				else
				{
					chkAcctFriends.Checked = false;
					chkAcctSportsNews.Checked = true;
				}
				chkDisableDeleteFriends.Checked = acct.Preferences.DeleteMsgWarning;
				chkDisableDeleteFriendsMsgs.Checked = acct.Preferences.DeleteFriendsWarning;
				chkReceiveCommentEmails.Checked = acct.Preferences.SendCommentsEmail;
				chkOffersFromUs.Checked = acct.Preferences.OffersFromUs;
				chkOffersFromPartners.Checked = acct.Preferences.OffersFromPartners;

				// Photo panel
			}

		}

		private void CheckSportBox( FavSports sport )
		{
			switch( sport )
			{
				case FavSports.CFL:
				{
					chkAcctCFL.Checked = true;
					break;
				}
				case FavSports.Extreme:
				{
					chkAcctExtremeSports.Checked = true;
					break;
				}
				case FavSports.LPGA:
				{
					chkAcctLPGA.Checked = true;
					break;
				}
				case FavSports.MLB:
				{
					chkAcctMLB.Checked = true;
					break;
				}
				case FavSports.MLS:
				{
					chkAcctMLS.Checked = true;
					break;
				}
				case FavSports.NASCAR:
				{
					chkAcctNASCAR.Checked = true;
					break;
				}
				case FavSports.NBA:
				{
					chkAcctNBA.Checked = true;
					break;
				}
				case FavSports.NCAAFootball:
				{
					chkAcctNCAAFootball.Checked = true;
					break;
				}
				case FavSports.NCAASoftball:
				{
					chkAcctNCAASoftball.Checked = true;
					break;
				}
				case FavSports.NFL:
				{
					chkAcctNFL.Checked = true;
					break;
				}
				case FavSports.NHL:
				{
					chkAcctNHL.Checked = true;
					break;
				}
				case FavSports.Olympics:
				{
					chkAcctOlmpics.Checked = true;
					break;
				}
				case FavSports.Other:
				{
					chkAcctOther.Checked = true;
					break;
				}
				case FavSports.PBA:
				{
					chkAcctPBA.Checked = true;
					break;
				}
				case FavSports.PGA:
				{
					chkAcctPGA.Checked = true;
					break;
				}
				case FavSports.UFC:
				{
					chkAcctUFC.Checked = true;
					break;
				}
				case FavSports.WNBA:
				{
					chkAcctWNBA.Checked = true;
					break;
				}
				case FavSports.YouthSports:
				{
					chkAcctYouthSports.Checked = true;
					break;
				}
			}
		}
#endregion

#region GetDataValues
		/// <summary>
		/// Get data from controls.
		/// 
		/// Assumes that the controls have been previously validated.
		/// </summary>
		private void GetDataValues( UserAccount acct )
		{


			// Set the values
			if (null != acct)
			{
				// Name panel
				if( acct.UserName.Length == 0 )
				{
					acct.UserName = txtUserName.Text;
				}
				
				acct.SecurityQuestion = txtSecurityQuestion.Text;
				acct.SecurityAnswer = txtSecurityAnswer.Text;
				acct.Title = txtTitle.Text;
				acct.First = txtFirstName.Text;
				acct.MI = txtMI.Text;
				acct.Last = txtLastName.Text;
				acct.Suffix = txtSuffix.Text;
				acct.Email = txtEmail.Text;
				acct.BirthDate = txtBirthDate.Text;

				// Address panel
				acct.Address1 = txtAddress1.Text;
				acct.Address2 = txtAddress2.Text;
				acct.City = txtCity.Text;
				acct.Zip = txtPostalCode.Text;
				acct.State = ddlState.SelectedValue;
				acct.Country = ddlCountry.SelectedValue;

				// Favorite Sports panel
				acct.Preferences.Interests = txtSportsInterests.Text;
				acct.Preferences.NewsSubjects = GetCheckedSports();

				// Preferences panel
				if( chkAcctFriends.Checked )
				{
					acct.DefaultPage = "friends";
				}
				else
				{
					acct.DefaultPage = "sportsnews";
				}

				acct.Preferences.DeleteMsgWarning = chkDisableDeleteFriends.Checked;
				acct.Preferences.DeleteFriendsWarning = chkDisableDeleteFriendsMsgs.Checked;
				acct.Preferences.SendCommentsEmail = chkReceiveCommentEmails.Checked;
				acct.Preferences.OffersFromUs = chkOffersFromUs.Checked;
				acct.Preferences.OffersFromPartners = chkOffersFromPartners.Checked;

				// Photo panel
			}

		}

		private string GetCheckedSports()
		{
			StringBuilder sbSports = new StringBuilder();
			if(chkAcctCFL.Checked)
			{
				sbSports.Append((int)FavSports.CFL).Append(",");
			}
			if (chkAcctExtremeSports.Checked)
			{
				sbSports.Append((int)FavSports.Extreme).Append(",");
			}
			if (chkAcctLPGA.Checked)
			{
				sbSports.Append((int)FavSports.LPGA).Append(",");
			}
			if (chkAcctMLB.Checked)
			{
				sbSports.Append((int)FavSports.MLB).Append(",");
			}
			if (chkAcctMLS.Checked)
			{
				sbSports.Append((int)FavSports.MLS).Append(",");
			}
			if (chkAcctNASCAR.Checked)
			{
				sbSports.Append((int)FavSports.NASCAR).Append(",");
			}
			if (chkAcctNBA.Checked)
			{
				sbSports.Append((int)FavSports.NBA).Append(",");
			}
			if (chkAcctNCAAFootball.Checked)
			{
				sbSports.Append((int)FavSports.NCAASoftball).Append(",");
			}
			if (chkAcctNCAASoftball.Checked)
			{
				sbSports.Append((int)FavSports.NCAAFootball).Append(",");
			}
			if (chkAcctNFL.Checked)
			{
				sbSports.Append((int)FavSports.NFL).Append(",");
			}
			if (chkAcctNHL.Checked)
			{
				sbSports.Append((int)FavSports.NHL).Append(",");
			}
			if (chkAcctOlmpics.Checked)
			{
				sbSports.Append((int)FavSports.Olympics).Append(",");
			}
			if (chkAcctOther.Checked)
			{
				sbSports.Append((int)FavSports.Other).Append(",");
			}

			if (chkAcctPBA.Checked)
			{
				sbSports.Append((int)FavSports.PBA).Append(",");
			}

			if (chkAcctPGA.Checked)
			{
				sbSports.Append((int)FavSports.PGA).Append(",");
			}

			if (chkAcctUFC.Checked)
			{
				sbSports.Append((int)FavSports.UFC).Append(",");
			}

			if (chkAcctWNBA.Checked)
			{
				sbSports.Append((int)FavSports.WNBA).Append(",");
			}
			if (chkAcctYouthSports.Checked)
			{
				sbSports.Append((int)FavSports.YouthSports).Append(",");
			}

			string strRet = sbSports.ToString();
			// Remove the last comma
			if( strRet.Length > 0 )
			{
				strRet.TrimEnd(',');
			}
			return strRet;
		}
		#endregion

#region PopulateDropDownLists
        void PopulateDropDownLists()
        {
            //populate ddls *********************************************************
            //populate ddlState *****************************************************
            var lstState = Master.GetSiteSetting(SiteAdmin.saKeyStates, "StateNames");
            List<string> lstStates = new List<string>();
            lstStates = lstState.Trim().Split(',').ToList();
            ddlState.Items.Clear();

            for (int intCounter = 0; intCounter < lstStates.Count; intCounter++)
            {
                ddlState.Items.Add(lstStates[intCounter].Trim());
            }

			ddlCountry.Items.Add(" --- ");
			ddlCountry.Items.Add("Cananda");
			ddlCountry.Items.Add("United States");
			ddlCountry.Items.Add("Other");
		}
#endregion

#region Display Sections
		void DisplaySections( Sections section )
		{
			DisplayNameSection( Sections.Name == section );
			DisplayAddressSection(Sections.Address == section);
			DisplaySportsSection(Sections.Sports == section);
			DisplayPrefsSection(Sections.Prefs == section);
			DisplayPhotoSection(Sections.Photo == section);
		}

		void DisplayNameSection( bool fVisible )
		{
			imgWizStepName.Visible = fVisible;
			pnlAccountName.Visible = fVisible;
		}

		void DisplayAddressSection(bool fVisible)
		{
			imgWizStepAddress.Visible = fVisible;
			pnlAccountAddress.Visible = fVisible;
		}

		void DisplaySportsSection(bool fVisible)
		{
			imgWizStepSports.Visible = fVisible;
			pnlAccountFavSports.Visible = fVisible;
		}

		void DisplayPrefsSection(bool fVisible)
		{
			imgWizStepPrefs.Visible = fVisible;
			pnlAccountPrefs.Visible = fVisible;
		}

		void DisplayPhotoSection(bool fVisible)
		{
			imgWizStepPhoto.Visible = fVisible;
			pnlAccountPhoto.Visible = fVisible;
		}

#endregion

#region Validate Sections
		bool ValidateNameSection( long lCheckVal )
		{
			bool fSectionValid = true;
			// Only check if this is a new invitee
			if (99 == lCheckVal)
			{
				if (fSectionValid && !(txtPassword.Text.Length > 0 && txtPswdConfirm.Text.Length > 0))
				{
					Master.AlertUser( "You must provide a password and confirmation for new accounts." );
					SetFocus( txtPassword );
					fSectionValid = false;
				}

				// Confirm the passwords match and are valid
				if (fSectionValid && (txtPassword.Text.Length > 0 || txtPswdConfirm.Text.Length > 0))
				{
					if (txtPassword.Text != txtPswdConfirm.Text)
					{
						Master.AlertUser("Your password and confirmation do not match.  Please reenter them.");
						SetFocus(txtPassword);
						fSectionValid = false;
					}
					else
					{
						if (!USCBase.IsPasswordString(txtPassword.Text))
						{
							Master.AlertUser("Your password can contain only letter, numbers and certain special characters (!@#$%^&*).  Please try again.");
							SetFocus(txtPassword);
							fSectionValid = false;
						}
					}
				}
			}

			// SecurityQuestion and Answer
			if( fSectionValid && (txtSecurityQuestion.Text.Length > 0 || txtSecurityAnswer.Text.Length > 0) )
			{
				if( txtSecurityQuestion.Text.Length == 0 || txtSecurityAnswer.Text.Length == 0 )
				{
					Master.AlertUser("Please provide a security question and answer, so we can help if you forget your password.");
					SetFocus(txtSecurityQuestion);
					fSectionValid = false;
				}
			}

			// Verify we have an email address
			if( fSectionValid )
			{
				int indexAt = txtEmail.Text.IndexOf('@');
				int indexPeriod = txtEmail.Text.IndexOf('.');
				if( 0 > indexAt || 0 > indexPeriod )
				{
					Master.AlertUser("Please enter a valid email address.");
					SetFocus(txtEmail);
					fSectionValid = false;
				}

			}


			return fSectionValid;
		}

		bool ValidateAddressSection()
		{
			bool fSectionValid = true;
			return fSectionValid;
		}

		bool ValidateIMSection()
		{
			bool fSectionValid = true;
			return fSectionValid;
		}

		bool ValidateSportsSection()
		{
			bool fSectionValid = true;
			return fSectionValid;
		}

		bool ValidatePrefsSection()
		{
			bool fSectionValid = true;
			return fSectionValid;
		}

		bool ValidatePhotoSection()
		{
			bool fSectionValid = true;
			return fSectionValid;
		}

		bool ValidateAllSections( long lCheckVal )
		{
			bool fSectionsValid = true;
			// Only section we are currently validating
			if (!ValidateNameSection( lCheckVal ))
			{
				DisplaySections(Sections.Name);
				fSectionsValid = false;
			}
			return fSectionsValid;
		}
#endregion

#region CreateUpdateLogoff
		protected UserAccount GetUserAccount()
		{
			string strUser = GetCookieUserName();
			UserAccount acct = Master.g_AccountsList.GetAccountByUserName(strUser);

			return acct;
		}

		protected void OnClickUpdateAccount(object sender, ImageClickEventArgs e)
		{
			long lCheckVal = GetSessionValue1();

			if (ValidateAllSections( lCheckVal ))
			{
				// Get the users account
				UserAccount acct = null;
				if (99 == lCheckVal)
				{
					long lInviteeID = GetSessionUserID();
					acct = GetUserByID(lInviteeID);
				}
				else
				{
					acct = GetUserAccount();
				}

				GetDataValues(acct);

				if( acct.Update() )
				{
					// If new invitee update password as well.
					if (99 == lCheckVal)
					{
						// Mark as normal rather than roster.
						acct.UserType = UserAccount.UserTypes.Normal;
						acct.Password = txtPassword.Text;

						// Actually calls a full update
						acct.UpdatePassword();
					}

					string strReturnURL = GetSessionReturnURL();
					if( strReturnURL.Length > 0 )
					{
						Response.Redirect( strReturnURL );
					}
					else
					{
						RedirectToDefault(acct);
					}
				}
				else
				{
					Master.AlertUser("There was a problem updating the account.  Please try again or go to the support page.");
				}
			}
		}

#endregion

#region Menu Clicks
		protected void OnMenuNameClickX(object sender, EventArgs e)
		{
			DisplaySections(Sections.Name);
		}

		protected void OnMenuAddressClickX(object sender, EventArgs e)
		{
			DisplaySections(Sections.Address);
		}

		protected void OnMenuSportsClickX(object sender, EventArgs e)
		{
			DisplaySections(Sections.Sports);
		}

		protected void OnMenuPrefsClickX(object sender, EventArgs e)
		{
			DisplaySections(Sections.Prefs);
		}

		protected void OnMenuPhotoClickX(object sender, EventArgs e)
		{
			DisplaySections(Sections.Photo);
		}
#endregion

#region NextPrevButtons
		protected void OnBtnAcctNameNext(object sender, ImageClickEventArgs e)
		{
			DisplaySections(Sections.Address);
		}

		protected void OnBtnAcctAddressPrev(object sender, ImageClickEventArgs e)
		{
			DisplaySections(Sections.Name);
		}

		protected void OnBtnAcctAddressNext(object sender, ImageClickEventArgs e)
		{
			DisplaySections(Sections.Sports);
		}

		protected void OnBtnAcctSportsPrev(object sender, ImageClickEventArgs e)
		{
			DisplaySections(Sections.Address);
		}

		protected void OnBtnAcctSportsNext(object sender, ImageClickEventArgs e)
		{
			DisplaySections(Sections.Prefs);
		}

		protected void OnBtnAcctPrefsPrev(object sender, ImageClickEventArgs e)
		{
			DisplaySections(Sections.Sports);
		}

		protected void OnBtnAcctPrefsNext(object sender, ImageClickEventArgs e)
		{
			DisplaySections(Sections.Photo);
		}

		protected void OnBtnAcctPhotoPrev(object sender, ImageClickEventArgs e)
		{
			DisplaySections(Sections.Prefs);
		}

#endregion

#region PrefsPage
		protected void OnCheckFriends(object sender, EventArgs e)
		{
			chkAcctFriends.Checked = true;
			chkAcctSportsNews.Checked = false;
		}

		protected void OnCheckSportsNews(object sender, EventArgs e)
		{
			chkAcctFriends.Checked = false;
			chkAcctSportsNews.Checked = true;
		}
#endregion

#region Change PSWD
		protected void OnClickChangePswd(object sender, ImageClickEventArgs e)
		{
			UserAccount acct = GetUserAccount();
			GetDataValues( acct );
			// Save any changes to this point
			acct.Update();
			Response.Redirect( "/MyAccount/ChangePassword.aspx" );
		}
#endregion

		protected void OnClickUpload(object sender, EventArgs e)
		{
			Response.Cache.SetCacheability(HttpCacheability.NoCache);

			// Before attempting to save the file, verify
			// that the FileUpload control contains a file.
			if (fulUserPhoto.HasFile)
			{
				// Get the account
				UserAccount acct = GetActiveUser();

				// Get the name of the file to upload.
				string strFile = Server.HtmlEncode(fulUserPhoto.FileName);

				// Get the extension of the uploaded file.
				string extension = System.IO.Path.GetExtension(strFile);

				// Allow only files with correct extensions to be uploaded.
				var strPhotoType = GetSiteSetting(SiteAdmin.saKeyValidImgTypes, "ValidImageTypes");
				List<string> lstPhotoTypes = new List<string>();
				lstPhotoTypes = strPhotoType.Trim().Split(',').ToList();
				Boolean fValidExtension = false;
				String strExtension = "";

				for (Int16 intCounter = 0; intCounter < lstPhotoTypes.Count; intCounter++)
				{
					if (strFile.ToLower().EndsWith(lstPhotoTypes[intCounter].Trim().ToLower()))
					{
						fValidExtension = true;
						strExtension = lstPhotoTypes[intCounter].Trim().ToLower();
						break;
					}
				}

				if( !fValidExtension )
				{
				}
				else
				{
					string strMaxPhotoSize = GetSiteSetting(SiteAdmin.saKeyMaxUserPhotoSize, "Maximum User Photo Size To Upload");
					int nMaxPhotoSize = Convert.ToInt32(strMaxPhotoSize);
					if (fulUserPhoto.PostedFile.ContentLength > nMaxPhotoSize)
					{
					}
					else
					{
						String strRootPath = Server.MapPath("~");
						if( acct.CreateUserFolders( strRootPath ) )
						{
							string strFileName = acct.UserName + "." + strExtension;
							string strFullSavePath = Server.HtmlEncode( acct.GetUserPhotoPath(strRootPath) + "\\" + strFileName );
							string strImageURL = "~/UsersFolder/" + acct.UserName + "/DisplayPhoto/" + strFileName;

							fulUserPhoto.SaveAs( strFullSavePath );

							//Update the user
							acct.PhotoFile = strFileName;
							acct.UpdateUserPhoto();
							imgUserPhoto.ImageUrl = strImageURL;
						}

					}
				}

			}

		}
	}
}