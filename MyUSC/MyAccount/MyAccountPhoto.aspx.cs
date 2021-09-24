using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MyUSC.Classes;

namespace MyUSC.MyAccount
{
	public partial class MyAccountPhoto : USCPageBase
	{
		protected MyAccountPhoto()
		{
			_PAGENAME = "AccountPhoto";
			_pgUSCMaster = (USCMaster)Master;
		}

#region SetDataValues
		private void SetDataValues()
		{
			// Get the users account
			UserAccount acct = GetActiveUser();

			// Set the values
			if (null == acct)
			{
				RedirectToLoginPage();
			}
			else
			{
				if (acct.PhotoFile.Length > 0)
				{
					string strImageURL = "~/UsersFolder/" + acct.UserName + "/DisplayPhoto/" + acct.PhotoFile;
					imgUserPhoto.ImageUrl = strImageURL;
				}
			}
		}
#endregion

#region Page_Load
		protected void Page_Load(object sender, EventArgs e)
		{
			Response.Cache.SetCacheability(HttpCacheability.NoCache);
			Response.Cache.SetNoServerCaching();
			if (!IsPostBack)
			{
				SetDataValues();
				Master.SelectMenuItem(SelectedPage.Profile);
			}
		}
#endregion

#region Button Clicks
		protected void OnClickOK(object sender, ImageClickEventArgs e)
		{
			Response.Redirect("/MyAccount/MyAccount.aspx");
		}

		protected void OnClickCancel(object sender, ImageClickEventArgs e)
		{
			Response.Redirect("/MyAccount/MyAccount.aspx");
		}

		protected void OnClickUpload(object sender, EventArgs e)
		{
			Response.Cache.SetCacheability(HttpCacheability.NoCache);
			Response.Cache.SetNoServerCaching();
			lblUploadError.Text = "";
			lblUploadSuccess.Text = "";

			// Before attempting to save the file, verify
			// that the FileUpload control contains a file.
			if (fulUserPhoto.HasFile)
			{
				// Get the account
				UserAccount acct = GetActiveUser();
				acct.ConnectionString = Master.g_strConnectionString;

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

				lblUploadError.Text = "";
				if (!fValidExtension)
				{
					lblUploadError.Text = "That is not a supported image format.";
				}
				else
				{
					string strMaxPhotoSize = GetSiteSetting(SiteAdmin.saKeyMaxUserPhotoSize, "Maximum User Photo Size To Upload");
					int nMaxPhotoSize = Convert.ToInt32(strMaxPhotoSize);
					if (fulUserPhoto.PostedFile.ContentLength > nMaxPhotoSize)
					{
						lblUploadError.Text = "The image you are trying to load is too large.";
					}
					else
					{
						String strRootPath = Server.MapPath("~");
						if (acct.CreateUserFolders(strRootPath))
						{
							try
							{
								Random rnd = new Random();
								int nRandom = rnd.Next();

								// add random number to the filename to make unique
								string strFileName = acct.UserName + nRandom + "." + strExtension;
								string strFullUserPath = Server.HtmlEncode(acct.GetUserPhotoPath(strRootPath));
								string strFullSavePath = Server.HtmlEncode(strFullUserPath + "\\" + strFileName);
								string strImageURL = "~/UsersFolder/" + acct.UserName + "/DisplayPhoto/" + strFileName;

								fulUserPhoto.SaveAs(strFullSavePath);

								FileInfo fi = new FileInfo( strFullSavePath );
								if( null != fi && fi.Exists )
								{
									fi.CreationTime = DateTime.Now;
								}

								if( acct.PhotoFile.Length > 0 )
								{
									string strFullOldPath = Server.HtmlEncode(strFullUserPath + "\\" + acct.PhotoFile);
									FileInfo fiOld = new FileInfo(strFullOldPath);
									if( null != fiOld && fiOld.Exists )
									{
										fiOld.Delete();
										acct.PhotoFile = "";
									}
								}

								//Update the user
								acct.PhotoFile = strFileName;
								if( acct.UpdateUserPhoto() )
								{
									imgUserPhoto.ImageUrl = strImageURL;
									lblUploadSuccess.Text = "Photo uploaded successfully";
									string strMsg = "Successfully updated photo for " + acct.UserName;
									EvtLog.WriteEvent( strMsg, System.Diagnostics.EventLogEntryType.Information, 0, 0 );
								}
								else
								{
									lblUploadError.Text = "There was an error uploading the file.  Please try again.";
								}
							}
							catch( Exception ex )
							{
								string strEx = "Account.UploadUserPhoto";
								EvtLog.WriteException( strEx, ex, 1 );
							}
						}

					}
				}
			}
		}

		protected void OnClickDeactivate(object sender, ImageClickEventArgs e)
		{
			Response.Redirect("/MyAccount/DeactivateAccount.aspx");
		}
#endregion
	}
}