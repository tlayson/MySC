using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MyUSC.Classes;
using MyUSC.Classes.MyOrg;

namespace MyUSC.MyTeams
{
	public partial class OrgOptions : USCPageBase
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			Master.PageHeading = "Options";

			string strOrgID = Request.QueryString["OrgID"];
			string strWiz = Request.QueryString["Wiz"];
			if( string.Empty == strWiz )
			{
				btnNextTop.Visible = false;
				btnNextBottom.Visible = false;
				btnBackTop.Visible = false;
				btnBackBottom.Visible = false;
				btnOKTop.Visible = true;
				btnOKBottom.Visible = true;
			}
			else
			{
				btnNextTop.Visible = true;
				btnNextBottom.Visible = true;
				btnBackTop.Visible = true;
				btnBackBottom.Visible = true;
				btnOKTop.Visible = false;
				btnOKBottom.Visible = false;
			}

			if( !IsPostBack )
			{
				long lOrgID = 0;
				if (null != strOrgID && long.TryParse(strOrgID, out lOrgID))
				{
					Master.SetMenuState(lOrgID, (string.Empty == strWiz));
					Organization org = (Organization)Master.g_OrgList.htOrgList[lOrgID];

					UserAccount acct = Master.GetActiveUser();
					if (UserHasViewAccess(acct.AccountID, lOrgID, OrgPageID.Manage))
					{
						SetSessionOrgID(lOrgID);
						if (!IsPostBack && null != org)
						{
							Master.PageHeading = "Manage Options - " + org.OrgName;
							EnablePageOptions(acct.AccountID, lOrgID);
						}
					}
					else
					{
						Master.AlertUser("You do not have permission to view this page.");
						Response.Redirect("~/MyTeams/Dashboard.aspx");
					}

					SetSessionOrgID(lOrgID);
					if (null == org)
					{
						// Some error handling here
					}
					else
					{
						if (org.LogoURL.Length > 0)
						{
							string strOrgBaseFolder = GetSiteSetting(SiteAdmin.saKeyOrgFolders, "OrgBaseFolder");
							string strLogoFolder = GetSiteSetting(SiteAdmin.saKeyOrgLogo, "OrgLogoFolder");
							string strImageURL = "~/" + strOrgBaseFolder + "/" + org.OrgID + "/" + strLogoFolder + "/" + org.LogoURL;

							imgOrgLogo.ImageUrl = strImageURL;
						}
						else
						{
							imgOrgLogo.ImageUrl = "~/Images/NoPhoto.JPG";
						}

						//	public enum OrgPageID { Undefined = -1, Home = 0, Roster = 1, Schedule = 2, MsgBoard = 3, Media = 4, Email = 5, Stats = 6, Venue = 7, Manage = 100 }

						// TODO: Make sure user belongs here or go to their dashboard
						chkFollowerRequests.Checked = org.AllowFollowerRequests;
						chkMemberRequests.Checked = org.AllowMemberRequests;
						chkGuestView.Checked = org.AllowGuestViews;

						OrgPageOptions opo = org.GetOrgPageOption(OrgPageID.Home);
						ddHomeAdmin.SelectedValue = ((int)opo.AdminLevel).ToString();
						ddHomeEdit.SelectedValue = ((int)opo.EditLevel).ToString();
						ddHomeAccess.SelectedValue = ((int)opo.AccessLevel).ToString();
						ddHomeView.SelectedValue = ((int)opo.ViewLevel).ToString();

						opo = org.GetOrgPageOption(OrgPageID.Roster);
						chkRosterVisible.Checked = opo.Visible;
						ddRosterAdmin.SelectedValue = ((int)opo.AdminLevel).ToString();
						ddRosterEdit.SelectedValue = ((int)opo.EditLevel).ToString();
						ddRosterAccess.SelectedValue = ((int)opo.AccessLevel).ToString();
						ddRosterView.SelectedValue = ((int)opo.ViewLevel).ToString();

						opo = org.GetOrgPageOption(OrgPageID.Schedule);
						chkScheduleVisible.Checked = opo.Visible;
						ddScheduleAdmin.SelectedValue = ((int)opo.AdminLevel).ToString();
						ddScheduleEdit.SelectedValue = ((int)opo.EditLevel).ToString();
						ddScheduleAccess.SelectedValue = ((int)opo.AccessLevel).ToString();
						ddScheduleView.SelectedValue = ((int)opo.ViewLevel).ToString();

						opo = org.GetOrgPageOption(OrgPageID.MsgBoard);
						chkMsgBoardVisible.Checked = opo.Visible;
						ddMsgBoardAdmin.SelectedValue = ((int)opo.AdminLevel).ToString();
						ddMsgBoardEdit.SelectedValue = ((int)opo.EditLevel).ToString();
						ddMsgBoardAccess.SelectedValue = ((int)opo.AccessLevel).ToString();
						ddMsgBoardView.SelectedValue = ((int)opo.ViewLevel).ToString();

						opo = org.GetOrgPageOption(OrgPageID.Media);
						chkMediaVisible.Checked = opo.Visible;
						ddMediaAdmin.SelectedValue = ((int)opo.AdminLevel).ToString();
						ddMediaEdit.SelectedValue = ((int)opo.EditLevel).ToString();
						ddMediaAccess.SelectedValue = ((int)opo.AccessLevel).ToString();
						ddMediaView.SelectedValue = ((int)opo.ViewLevel).ToString();

						opo = org.GetOrgPageOption(OrgPageID.Email);
						chkEmailVisible.Checked = opo.Visible;
						ddEmailAdmin.SelectedValue = ((int)opo.AdminLevel).ToString();
						ddEmailEdit.SelectedValue = ((int)opo.EditLevel).ToString();
						ddEmailAccess.SelectedValue = ((int)opo.AccessLevel).ToString();
						ddEmailView.SelectedValue = ((int)opo.ViewLevel).ToString();

						opo = org.GetOrgPageOption(OrgPageID.Venue);
						chkVenueVisible.Checked = opo.Visible;
						ddVenueAdmin.SelectedValue = ((int)opo.AdminLevel).ToString();
						ddVenueEdit.SelectedValue = ((int)opo.EditLevel).ToString();
						ddVenueAccess.SelectedValue = ((int)opo.AccessLevel).ToString();
						ddVenueView.SelectedValue = ((int)opo.ViewLevel).ToString();

						opo = org.GetOrgPageOption(OrgPageID.Manage);
						ddManageAdmin.SelectedValue = ((int)opo.AdminLevel).ToString();
						ddManageEdit.SelectedValue = ((int)opo.EditLevel).ToString();
						ddManageAccess.SelectedValue = ((int)opo.AccessLevel).ToString();
						ddManageView.SelectedValue = ((int)opo.ViewLevel).ToString();

					}
				}
				else
				{
					Master.AlertUser("No organization specified.");
					Response.Redirect("~/MyTeams/Dashboard.aspx");
				}
				Master.SelectMTMenuItemOnPageLoad(MyTeamsPage.Manage);
			}
		}

		protected void EnablePageOptions( long acctID, long orgID )
		{
			if( !UserHasMemberAccess( acctID, orgID, OrgPageID.Manage ) )
			{
			}

			if( !UserHasEditAccess( acctID, orgID, OrgPageID.Manage ) )
			{
			}

			if( !UserHasAdminAccess( acctID, orgID, OrgPageID.Manage ) )
			{
			}
		}

		protected bool GetData( long orgID )
		{
			bool fRet = false;
			Organization org = Master.g_OrgList.GetOrganization( orgID );
			UserAccount acct = Master.GetActiveUser();
			if (null != org)
			{
				org.AllowFollowerRequests = chkFollowerRequests.Checked;
				org.AllowMemberRequests = chkMemberRequests.Checked;
				org.AllowGuestViews = chkGuestView.Checked;

				OrgPageOptions opo = org.GetOrgPageOption(OrgPageID.Home);
				opo.ConnectionString = org.ConnectionString;
				opo.AdminLevel = (OrgAccessTypes)Convert.ToInt32(ddHomeAdmin.SelectedValue);
				opo.EditLevel = (OrgAccessTypes)Convert.ToInt32(ddHomeEdit.SelectedValue);
				opo.AccessLevel = (OrgAccessTypes)Convert.ToInt32(ddHomeAccess.SelectedValue);
				opo.ViewLevel = (OrgAccessTypes)Convert.ToInt32(ddHomeView.SelectedValue);
				opo.Update(acct);

				opo = org.GetOrgPageOption(OrgPageID.Roster);
				opo.ConnectionString = org.ConnectionString;
				opo.Visible = chkRosterVisible.Checked;
				opo.AdminLevel = (OrgAccessTypes)Convert.ToInt32(ddRosterAdmin.SelectedValue);
				opo.EditLevel = (OrgAccessTypes)Convert.ToInt32(ddRosterEdit.SelectedValue);
				opo.AccessLevel = (OrgAccessTypes)Convert.ToInt32(ddRosterAccess.SelectedValue);
				opo.ViewLevel = (OrgAccessTypes)Convert.ToInt32(ddRosterView.SelectedValue);
				opo.Update(acct);

				opo = org.GetOrgPageOption(OrgPageID.Schedule);
				opo.ConnectionString = org.ConnectionString;
				opo.Visible = chkScheduleVisible.Checked;
				opo.AdminLevel = (OrgAccessTypes)Convert.ToInt32(ddScheduleAdmin.SelectedValue);
				opo.EditLevel = (OrgAccessTypes)Convert.ToInt32(ddScheduleEdit.SelectedValue);
				opo.AccessLevel = (OrgAccessTypes)Convert.ToInt32(ddScheduleAccess.SelectedValue);
				opo.ViewLevel = (OrgAccessTypes)Convert.ToInt32(ddScheduleView.SelectedValue);
				opo.Update(acct);

				opo = org.GetOrgPageOption(OrgPageID.MsgBoard);
				opo.ConnectionString = org.ConnectionString;
				opo.Visible = chkMsgBoardVisible.Checked;
				opo.AdminLevel = (OrgAccessTypes)Convert.ToInt32(ddMsgBoardAdmin.SelectedValue);
				opo.EditLevel = (OrgAccessTypes)Convert.ToInt32(ddMsgBoardEdit.SelectedValue);
				opo.AccessLevel = (OrgAccessTypes)Convert.ToInt32(ddMsgBoardAccess.SelectedValue);
				opo.ViewLevel = (OrgAccessTypes)Convert.ToInt32(ddMsgBoardView.SelectedValue);
				opo.Update(acct);

				opo = org.GetOrgPageOption(OrgPageID.Media);
				opo.ConnectionString = org.ConnectionString;
				opo.Visible = chkMediaVisible.Checked;
				opo.AdminLevel = (OrgAccessTypes)Convert.ToInt32(ddMediaAdmin.SelectedValue);
				opo.EditLevel = (OrgAccessTypes)Convert.ToInt32(ddMediaEdit.SelectedValue);
				opo.AccessLevel = (OrgAccessTypes)Convert.ToInt32(ddMediaAccess.SelectedValue);
				opo.ViewLevel = (OrgAccessTypes)Convert.ToInt32(ddMediaView.SelectedValue);
				opo.Update(acct);

				opo = org.GetOrgPageOption(OrgPageID.Email);
				opo.ConnectionString = org.ConnectionString;
				opo.Visible = chkEmailVisible.Checked;
				opo.AdminLevel = (OrgAccessTypes)Convert.ToInt32(ddEmailAdmin.SelectedValue);
				opo.EditLevel = (OrgAccessTypes)Convert.ToInt32(ddEmailEdit.SelectedValue);
				opo.AccessLevel = (OrgAccessTypes)Convert.ToInt32(ddEmailAccess.SelectedValue);
				opo.ViewLevel = (OrgAccessTypes)Convert.ToInt32(ddEmailView.SelectedValue);
				opo.Update(acct);

				opo = org.GetOrgPageOption(OrgPageID.Venue);
				opo.ConnectionString = org.ConnectionString;
				opo.Visible = chkVenueVisible.Checked;
				opo.AdminLevel = (OrgAccessTypes)Convert.ToInt32(ddVenueAdmin.SelectedValue);
				opo.EditLevel = (OrgAccessTypes)Convert.ToInt32(ddVenueEdit.SelectedValue);
				opo.AccessLevel = (OrgAccessTypes)Convert.ToInt32(ddVenueAccess.SelectedValue);
				opo.ViewLevel = (OrgAccessTypes)Convert.ToInt32(ddVenueView.SelectedValue);
				opo.Update(acct);

				opo = org.GetOrgPageOption(OrgPageID.Manage);
				opo.ConnectionString = org.ConnectionString;
				opo.AdminLevel = (OrgAccessTypes)Convert.ToInt32(ddManageAdmin.SelectedValue);
				opo.EditLevel = (OrgAccessTypes)Convert.ToInt32(ddManageEdit.SelectedValue);
				opo.AccessLevel = (OrgAccessTypes)Convert.ToInt32(ddManageAccess.SelectedValue);
				opo.ViewLevel = (OrgAccessTypes)Convert.ToInt32(ddManageView.SelectedValue);
				opo.Update(acct);

				org.Update( acct );
			}
			return fRet;
		}

		protected void OnClickNext(object sender, EventArgs e)
		{
			long orgID = GetSessionOrgID();
			string strURL = "~/MyTeams/OrgPermissions.aspx?OrgID=" + orgID + "&Wiz=1";
			GetData( orgID );
			Response.Redirect( strURL );
		}

		protected void OnClickBack(object sender, EventArgs e)
		{
			long orgID = GetSessionOrgID();
			string strURL = "~/MyTeams/CreateOrg.aspx?OrgID=" + orgID + "&Wiz=1";
			Response.Redirect( strURL );
		}

		protected void OnClickOK(object sender, ImageClickEventArgs e)
		{
			long orgID = GetSessionOrgID();
			GetData( orgID );
			string strURL = "~/MyTeams/ManageOrg.aspx?OrgID=" + orgID;
			Response.Redirect( strURL );
		}

		protected void OnClickUpload(object sender, EventArgs e)
		{
			lblUploadError.Text = "";
			lblUploadSuccess.Text = "";

			if (fulUserLogo.HasFile)
			{
				// Get the org
				UserAccount user = GetActiveUser();
				user.ConnectionString = Master.g_strConnectionString;

				long orgID = GetSessionOrgID();
				Organization org = Master.g_OrgList.GetOrganization(orgID);

				// Get the name of the file to upload.
				string strFile = Server.HtmlEncode(fulUserLogo.FileName);

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

				if (!fValidExtension)
				{
					//lblUploadError.Text = "That is not a supported image format.";
				}
				else
				{
					string strMaxPhotoSize = GetSiteSetting(SiteAdmin.saKeyMaxUserPhotoSize, "Maximum User Photo Size To Upload");
					int nMaxPhotoSize = Convert.ToInt32(strMaxPhotoSize);
					if (fulUserLogo.PostedFile.ContentLength > nMaxPhotoSize)
					{
						//lblUploadError.Text = "The image you are trying to load is too large.";
					}
					else
					{
						String strRootPath = Server.MapPath("~");
						if (org.CreateOrgFolders(strRootPath))
						{
							try
							{
								Random rnd = new Random();
								int nRandom = rnd.Next();

								// add random number to the filename to make unique
								string strFileName = org.OrgID + nRandom + "." + strExtension;
								string strFullUserPath = Server.HtmlEncode(org.GetOrgLogoPath(strRootPath));
								string strFullSavePath = Server.HtmlEncode(strFullUserPath + "\\" + strFileName);
								string strOrgBaseFolder = GetSiteSetting(SiteAdmin.saKeyOrgFolders, "OrgBaseFolder");
								string strLogoFolder = GetSiteSetting(SiteAdmin.saKeyOrgLogo, "OrgLogoFolder");
								string strImageURL = "~/" + strOrgBaseFolder + "/" + orgID + "/" + strLogoFolder + "/" + strFileName;

								fulUserLogo.SaveAs(strFullSavePath);

								FileInfo fi = new FileInfo( strFullSavePath );
								if( null != fi && fi.Exists )
								{
									fi.CreationTime = DateTime.Now;
								}

								if( org.LogoURL.Length > 0 )
								{
									string strFullOldPath = Server.HtmlEncode(strFullUserPath + "\\" + org.LogoURL);
									FileInfo fiOld = new FileInfo(strFullOldPath);
									if( null != fiOld && fiOld.Exists )
									{
										fiOld.Delete();
										org.LogoURL = "";
									}
								}

								//Update the user
								org.LogoURL = strFileName;
								if( org.Update( user ) )
								{
									imgOrgLogo.ImageUrl = strImageURL;
									lblUploadSuccess.Text = "Logo uploaded successfully";
									string strMsg = "Successfully updated logo for " + org.OrgName;
									EvtLog.WriteEvent( strMsg, System.Diagnostics.EventLogEntryType.Information, 0, 0 );
								}
								else
								{
									//lblUploadError.Text = "There was an error uploading the file.  Please try again.";
								}

							}
							catch( Exception ex )
							{
								string strEx = "OrgOptions.UploadOrgLogo";
								EvtLog.WriteException( strEx, ex, 1 );
							}

						}

					}
				}

			}
		}
	}
}