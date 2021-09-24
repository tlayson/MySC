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
	public partial class ManageAffiliates : USCPageBase
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			string strOrgID = Request.QueryString["OrgID"];
			if (!IsPostBack)
			{
				long lOrgID = 0;
				if (null != strOrgID && long.TryParse(strOrgID, out lOrgID))
				{
					Master.SetMenuState(lOrgID, false);

					UserAccount acct = Master.GetActiveUser();
					if (UserHasViewAccess(acct.AccountID, lOrgID, OrgPageID.Manage))
					{
						SetSessionOrgID(lOrgID);
						Organization org = (Organization)Master.g_OrgList.htOrgList[lOrgID];
						if (!IsPostBack && null != org)
						{
							Master.PageHeading = "Manage Affiliates - " + org.OrgName;
							EnablePageOptions(acct.AccountID, lOrgID);
							DisplayAffiliates( org );
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

		protected void DrawAffiliate( Table tbl, Affiliate affiliate )
		{
			Organization org = Master.GetOrgByID( affiliate.AffiliateID );
			TableRow tr = new TableRow();
			TableCell td = new TableCell();
			td.CssClass = "tdInput";
			HyperLink hl = new HyperLink();
			hl.Text = org.OrgName;
			//hl.Target = "_blank";
			hl.ForeColor = System.Drawing.Color.Black;

			//string strURL = "~/MyTeams/Home.aspx?OrgID=" + affiliate.AffiliateID;
			string strURL = "~/MyTeams/AddAffiliate.aspx?OrgID=" + affiliate.OrgID;
			strURL += "&AFID=" + affiliate.AffiliateID;
			strURL += "&AFType=" + (int)affiliate.AffiliateType;
			hl.NavigateUrl = strURL;
			
			td.Controls.Add( hl );
			tr.Controls.Add( td );
			tbl.Controls.Add( tr );

			if( affiliate.Note.Length > 0 )
			{
				tr = new TableRow();
				td = new TableCell();
				Label lbl = new Label();
				lbl.Text = affiliate.Note;
				td.Controls.Add( lbl );
				tr.Controls.Add( td );
				tbl.Controls.Add( tr );
			}
		}

		protected void DrawAffiliateSection( Table tbl, Affiliate affiliate, bool fFirst )
		{
			if (!fFirst)
			{
				TableRow tr1 = new TableRow();
				TableCell td1 = new TableCell();
				td1.CssClass = "tdInput";
				tr1.Controls.Add(td1);
				tbl.Controls.Add(tr1);
			}

			TableRow tr = new TableRow();
			TableCell td = new TableCell();
			td.CssClass = "tdInput";
			Label lbl = new Label();
			lbl.Text = affiliate.GetAffiliateTypeString();
			lbl.CssClass = "medSiteColorTxt";

			td.Controls.Add(lbl);
			tr.Controls.Add(td);
			tbl.Controls.Add(tr);
		}

		protected void DisplayAffiliates(Organization org)
		{
			Table tbl = tblAffilates;
			tbl.Controls.Clear();
			Affiliate prev = null;
			bool fFirst = true;

			if( org.orgAffiliateList.Count > 0 )
			{
				foreach (KeyValuePair<long, object> kvp in org.orgAffiliateList.m_sdAffiliates)
				{
					Affiliate affiliate = (Affiliate)kvp.Value;
					if( null != affiliate )
					{
						if( null == prev || affiliate.AffiliateType != prev.AffiliateType )
						{
							DrawAffiliateSection( tbl, affiliate, fFirst );
							fFirst = false;
						}
						prev = affiliate;
						DrawAffiliate( tbl, affiliate);
					}
				}
			}
			else
			{
				TableRow tr = new TableRow();
				TableCell td = new TableCell();
				td.CssClass = "tdInput";
				td.Text = "No affiliates";

				tr.Controls.Add( td );
				tbl.Controls.Add( tr );
			}
		}

		void SetCallingPage()
		{
			// Set session variables so we can return and add
			long orgID = GetSessionOrgID();
			string strURL = "~/MyTeams/ManageAffiliates.aspx?OrgID=" + orgID;
		}

		protected void OnClickSearchAffiliates(object sender, EventArgs e)
		{
			SetCallingPage();
			long orgID = GetSessionOrgID();
			string strURL = "~/MyTeams/FindAffiliate.aspx?OrgID=" + orgID;
			Response.Redirect( strURL );
		}

		protected void OnClickNewOrg(object sender, EventArgs e)
		{
			SetCallingPage();
			Response.Redirect("/MyTeams/CreateOrg.aspx");
		}
	}
}