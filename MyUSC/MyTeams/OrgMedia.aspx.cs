using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MyUSC.Classes;
using MyUSC.Classes.MyOrg;

namespace MyUSC.MyTeams
{
	public partial class OrgMedia : USCPageBase
	{
        //set page size
        int PageSize = 5;
		string m_strOrgID;

		protected void Page_Load(object sender, EventArgs e)
		{
			m_strOrgID = Request.QueryString["OrgID"];
			if (!IsPostBack)
			{
				long lOrgID = 0;
				if (null != m_strOrgID && long.TryParse(m_strOrgID, out lOrgID))
				{
					Master.SetMenuState(lOrgID, false);

					UserAccount acct = Master.GetActiveUser();
					if (UserHasViewAccess(acct.AccountID, lOrgID, OrgPageID.Media))
					{
						SetSessionOrgID(lOrgID);
						Organization org = (Organization)Master.g_OrgList.htOrgList[lOrgID];
						if (!IsPostBack && null != org)
						{
							Master.PageHeading = "Media - " + org.OrgName;
							EnablePageOptions(acct.AccountID, lOrgID);
						}
						EnablePageOptions(acct.AccountID, lOrgID);

						//initial page index
						Page_Index = 0;

						//set page count
						if ((ImageCount() % PageSize) == 0)
						{
							Page_Count = ImageCount() / PageSize;
							if (Page_Count == 0)
							{
								lbnFirstPage.Enabled = false;
								lbnPrevPage.Enabled = false;
								lbnNextPage.Enabled = false;
								lbnLastPage.Enabled = false;
							}
						}
						else
						{
							Page_Count = (ImageCount() / PageSize + 1);
						}

						//bind DataList
						dlOrgMedia.DataSource = BindGrid();
						dlOrgMedia.DataBind();

						//disable two button for the initial page
						lbnFirstPage.Enabled = false;
						lbnPrevPage.Enabled = false;
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
			if (!UserHasMemberAccess(acctID, orgID, OrgPageID.Media))
			{
			}

			if (UserHasEditAccess(acctID, orgID, OrgPageID.Media))
			{
				// If user has edit access, enable upload link
				pnlUpload.Visible = true;
			}

			if (!UserHasAdminAccess(acctID, orgID, OrgPageID.Media))
			{
			}
		}

		public string OrgID
		{
			get { return m_strOrgID; }
		}

		//property for current page index
		public int Page_Index
		{
			get { return (int)ViewState["_Page_Index"]; }
			set { ViewState["_Page_Index"] = value; }
		}
		//property for total page count
		public int Page_Count
		{
			get { return (int)ViewState["_Page_Count"]; }
			set { ViewState["_Page_Count"] = value; }
		}

		//return total number of images
		protected int ImageCount()
		{
			string strPath = "/OrgFolders/" + OrgID + "/Media/";
            DirectoryInfo di = new DirectoryInfo(Server.MapPath( strPath ));
			FileInfo[] fi = di.GetFiles();
			return fi.GetLength(0);
		}

//return the data source for DataList
        protected DataTable BindGrid()
        {
            //get all image paths             
			string strPath = "/OrgFolders/" + OrgID + "/Media/";
            DirectoryInfo di = new DirectoryInfo(Server.MapPath( strPath ));
            FileInfo[] fi = di.GetFiles();
 
            //save all paths to the DataTable as the data source
            DataTable dt = new DataTable();
            DataColumn dc = new DataColumn("Url", typeof(System.String));
            dt.Columns.Add(dc);
            int lastindex = 0;
            if (Page_Count == 0 || Page_Index == Page_Count - 1)
            {
                lastindex = ImageCount();
            }
            else
            {
                lastindex = Page_Index * PageSize + 5;
            }
            for (int i = Page_Index * PageSize; i < lastindex; i++)
            {
                DataRow dro = dt.NewRow();
                dro[0] = fi[i].Name;
                dt.Rows.Add(dro);
            }
            return dt;
        }

        //handle the thumbnail image selecting event
        protected void IB_tn_Click(object sender, EventArgs e)
        {
            ImageButton ib = (ImageButton)sender;
            Image1.ImageUrl = ib.ImageUrl;
            dlOrgMedia.SelectedIndex = Convert.ToInt32(ib.CommandArgument);
        }

		protected void OnClickFirst(object sender, EventArgs e)
		{
			ClickHandler( 1 );
		}

		protected void OnClickPrev(object sender, EventArgs e)
		{
			ClickHandler( 2 );
		}

		protected void OnClickNext(object sender, EventArgs e)
		{
			ClickHandler( 3 );
		}

		protected void OnClickLast(object sender, EventArgs e)
		{
			ClickHandler( 4 );
		}

		protected void ClickHandler( int cmnd )
		{
            if ( cmnd == 1 )
            {
                Page_Index = 0;
                lbnFirstPage.Enabled = false;
                lbnPrevPage.Enabled = false;
                lbnNextPage.Enabled = true;
                lbnLastPage.Enabled = true;
            }
            else if ( cmnd == 2 )
            {
                Page_Index -= 1;
                if (Page_Index == 0)
                {
                    lbnFirstPage.Enabled = false;
                    lbnPrevPage.Enabled = false;
                    lbnNextPage.Enabled = true;
                    lbnLastPage.Enabled = true;
                }
                else
                {
                    lbnFirstPage.Enabled = true;
                    lbnPrevPage.Enabled = true;
                    lbnNextPage.Enabled = true;
                    lbnLastPage.Enabled = true;
                }
            }
            else if ( cmnd == 3 )
            {
                Page_Index += 1;
                if (Page_Index == Page_Count - 1)
                {
                    lbnFirstPage.Enabled = true;
                    lbnPrevPage.Enabled = true;
                    lbnNextPage.Enabled = false;
                    lbnLastPage.Enabled = false;
                }
                else
                {
                    lbnFirstPage.Enabled = true;
                    lbnPrevPage.Enabled = true;
                    lbnNextPage.Enabled = true;
                    lbnLastPage.Enabled = true;
                }
            }
            else if ( cmnd == 4 )
            {
                Page_Index = Page_Count - 1;
                lbnFirstPage.Enabled = true;
                lbnPrevPage.Enabled = true;
                lbnNextPage.Enabled = false;
                lbnLastPage.Enabled = false;
            }
 
            dlOrgMedia.SelectedIndex = 0;
            dlOrgMedia.DataSource = BindGrid();
            dlOrgMedia.DataBind();
            Image1.ImageUrl 
                = ((Image)dlOrgMedia.Items[0].FindControl("IB_tn")).ImageUrl;
		}

		protected void OnClickUpload(object sender, EventArgs e)
		{
			Uri uriRet = Request.Url;
			SetSessionReturnURL(uriRet.PathAndQuery);

			string strURL = "~/MyTeams/UploadMedia.aspx?OrgID=";
			strURL += OrgID;
			Response.Redirect(strURL);

		}

	}
}