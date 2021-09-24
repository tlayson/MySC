using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MyUSC.Classes;
using MyUSC.Classes.MyOrg;

namespace MyUSC.MyTeams
{
	public partial class UploadMedia : USCPageBase
	{

		protected void Page_Load(object sender, EventArgs e)
		{
			string  strOrgID = Request.QueryString["OrgID"];
			if (!IsPostBack)
			{
				long lOrgID = 0;
				if (null != strOrgID && long.TryParse(strOrgID, out lOrgID))
				{
					Master.SetMenuState(lOrgID, false);

					UserAccount acct = Master.GetActiveUser();
					if (UserHasEditAccess(acct.AccountID, lOrgID, OrgPageID.Media))
					{
						SetSessionOrgID(lOrgID);
						Organization org = (Organization)Master.g_OrgList.htOrgList[lOrgID];
						if (!IsPostBack && null != org)
						{
							Master.PageHeading = "Upload Media - " + org.OrgName;
							EnablePageOptions(acct.AccountID, lOrgID);
						}
						EnablePageOptions(acct.AccountID, lOrgID);

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
				Master.SelectMTMenuItemOnPageLoad(MyTeamsPage.Media);
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

		protected void OnClickUpload(object sender, EventArgs e)
		{
			Response.Cache.SetCacheability(HttpCacheability.NoCache);

			// Before attempting to save the file, verify
			// that the FileUpload control contains a file.
			if (fulUserPhoto.HasFile)
			{
				long lOrgID = GetSessionOrgID();

				// Get the org and user account
				UserAccount acct = GetActiveUser();
				Organization org = (Organization)Master.g_OrgList.htOrgList[lOrgID];

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

				if (!fValidExtension)
				{
					//TODO: Error Handling
				}
				else
				{
					string strMaxPhotoSize = GetSiteSetting(SiteAdmin.saKeyMaxUserPhotoSize, "Maximum User Photo Size To Upload");
					int nMaxPhotoSize = Convert.ToInt32(strMaxPhotoSize);
					if (fulUserPhoto.PostedFile.ContentLength > nMaxPhotoSize)
					{
						//TODO: Error Handling
					}
					else
					{
						String strRootPath = Server.MapPath("~");
						if( org.CreateOrgFolders( strRootPath ) )
						{
							string strFileName = System.IO.Path.GetFileName( strFile );
							string strFullSavePath = Server.HtmlEncode( org.GetOrgMediaPath(strRootPath) + "\\" + strFileName);
							string strImageURL = "~/OrgFolders/" + lOrgID + "/Media/" + strFileName;

							fulUserPhoto.SaveAs(strFullSavePath);

							//Load the image
							imgUpload.ImageUrl = strImageURL;
						}
					}
				}
			}
		}

		protected void OnClickFinished(object sender, EventArgs e)
		{
			string strReturnURL = GetSessionReturnURL();
			Response.Redirect( strReturnURL );
		}

	}
}