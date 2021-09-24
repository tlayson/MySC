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
	public partial class Home : USCPageBase
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
							Master.PageHeading = "Home - " + org.OrgName;
							EnablePageOptions(acct.AccountID, lOrgID);
							DisplayHomePage( org );
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
				Master.SelectMTMenuItemOnPageLoad( MyTeamsPage.Home );
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

		protected void DrawEvent( Table tbl, Event evt )
		{
			TableRow tr = new TableRow();
			TableCell td = new TableCell();
			td.CssClass = "tdInput";
			HyperLink hl = new HyperLink();
			hl.Text = evt.EventName;
			hl.ForeColor = System.Drawing.Color.Black;
			string strURL = "~/MyTeams/EventDetails.aspx?OrgID=" + evt.OrgID + "&EventID=" + evt.EventID;
			hl.NavigateUrl = strURL;
			td.Controls.Add(hl);
			tr.Controls.Add(td);

			td = new TableCell();
			td.CssClass = "tdInput";
			Label lbl = new Label();
			lbl.Text = evt.EventDate.ToShortDateString() + " - " + evt.EventDate.ToShortTimeString();
			td.Controls.Add(lbl);
			tr.Controls.Add(td);

			Venue venue = Master.g_VenueList.GetVenue( evt.VenueID );
			td = new TableCell();
			td.CssClass = "tdInput";
			lbl = new Label();
			if (null != venue)
			{
				lbl.Text = venue.VenueName;
			}
			else
			{
				lbl.Text = "TBD";
			}
			td.Controls.Add(lbl);
			tr.Controls.Add(td);

			tbl.Controls.Add(tr);
		}

		protected void DisplayEvents( Organization org )
		{
			Table tbl = tblOrgSchedule;
			tbl.Controls.Clear();
			// Determine what to show
			foreach (KeyValuePair<long, object> kvp in org.orgEventList.m_sdOrgEvents)
			{
				Event evt = (Event)kvp.Value;
				if (null != evt)
				{
					DrawEvent( tbl, evt );
				}
			}
		}

		protected void DrawNews(Table tbl, Organization org)
		{
			TableRow tr = new TableRow();
			tr.CssClass = "trNormal";
			TableCell td = new TableCell();
			td.CssClass = "tdInput";
			Literal lit = new Literal();
			lit.Text = org.orgInfo.News;
			td.Controls.Add( lit );
			tr.Controls.Add( td );
			tbl.Controls.Add( tr );
		}

		protected void DisplayNews(Organization org)
		{
			Table tbl = tblOrgSchedule;
			lblNoNews.Visible = false;
			if( org.orgInfo.News.Length > 0 )
			{
				litOrgNews.Text = org.orgInfo.News;
			}
			else
			{
				lblNoNews.Visible = true;
			}

		}

		protected void DrawMessages( Table tbl )
		{
			
		}

		protected void DisplayMessages(Organization org)
		{
			Table tbl = tblOrgSchedule;


		}

		protected void DrawAffiliate(Table tbl, Affiliate affiliate)
		{
			Organization org = Master.GetOrgByID(affiliate.AffiliateID);
			TableRow tr = new TableRow();
			TableCell td = new TableCell();
			td.CssClass = "tdInput";
			HyperLink hl = new HyperLink();
			hl.Text = org.OrgName;
			hl.Target = "_blank";
			hl.ForeColor = System.Drawing.Color.Black;

			string strURL = "~/MyTeams/Home.aspx?OrgID=" + affiliate.AffiliateID;
			hl.NavigateUrl = strURL;

			td.Controls.Add(hl);
			tr.Controls.Add(td);
			tbl.Controls.Add(tr);

			if (affiliate.Note.Length > 0)
			{
				tr = new TableRow();
				td = new TableCell();
				Label lbl = new Label();
				lbl.Text = affiliate.Note;
				td.Controls.Add(lbl);
				tr.Controls.Add(td);
				tbl.Controls.Add(tr);
			}
		}

		protected void DrawAffiliateSection(Table tbl, Affiliate affiliate, bool fFirst)
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

			if (org.orgAffiliateList.Count > 0)
			{
				foreach (KeyValuePair<long, object> kvp in org.orgAffiliateList.m_sdAffiliates)
				{
					Affiliate affiliate = (Affiliate)kvp.Value;
					if (null != affiliate)
					{
						if (null == prev || affiliate.AffiliateType != prev.AffiliateType)
						{
							DrawAffiliateSection(tbl, affiliate, fFirst);
							fFirst = false;
						}
						prev = affiliate;
						DrawAffiliate(tbl, affiliate);
					}
				}
			}
			else
			{
				TableRow tr = new TableRow();
				TableCell td = new TableCell();
				td.CssClass = "tdInput";
				td.Text = "No affiliates";

				tr.Controls.Add(td);
				tbl.Controls.Add(tr);
			}
		}

		protected void ShowAddress( Table tbl, Organization org )
		{
			TableRow tr = null;
			TableCell td = null;
			Label lbl = null;

			if( org.Address1.Length > 0 )
			{
				tr = new TableRow();
				td = new TableCell();
				lbl = new Label();
				lbl.CssClass = "smNormalTxt";
				lbl.Text = org.Address1;
				td.Controls.Add( lbl );
				tr.Controls.Add( td );
				tbl.Controls.Add( tr );
			}

			if( org.Address2.Length > 0 )
			{
				tr = new TableRow();
				td = new TableCell();
				lbl = new Label();
				lbl.CssClass = "smNormalTxt";
				lbl.Text = org.Address2;
				td.Controls.Add( lbl );
				tr.Controls.Add( td );
				tbl.Controls.Add( tr );
			}

			if( org.City.Length > 0 ||
				org.State.Length > 0 ||
				org.Zip.Length > 0 )
			{
				tr = new TableRow();
				td = new TableCell();
				lbl = new Label();
				lbl.CssClass = "smNormalTxt";
				lbl.Text = org.City + ", " + org.State + " " + org.Zip;
				td.Controls.Add( lbl );
				tr.Controls.Add( td );
				tbl.Controls.Add( tr );
			}

		}
		
		protected void ShowPhone( Table tbl, Organization org )
		{
			if( org.Phone.Length > 0 )
			{
				TableRow tr = new TableRow();
				TableCell td = new TableCell();
				Label lbl = new Label();
				lbl.CssClass = "smNormalTxt";
				lbl.Text = org.Phone;
				td.Controls.Add( lbl );
				tr.Controls.Add( td );
				tbl.Controls.Add( tr );
			}
		}
		
		protected void ShowWebsite( Table tbl, Organization org )
		{
			if( org.URL.Length > 0 )
			{
				TableRow tr = new TableRow();
				TableCell td = new TableCell();
				td.CssClass = "tdInput";
				HyperLink lnk = new HyperLink();
				lnk.CssClass = "smNormalTxt";
				lnk.Target = "_blank";
				lnk.Text = org.URL;
				lnk.NavigateUrl = org.URL;
				td.Controls.Add( lnk );
				tr.Controls.Add( td );
				tbl.Controls.Add( tr );
			}
		}
		
		protected void ShowDecription( Table tbl, Organization org )
		{
			if( org.OrgDescription.Length > 0 )
			{
				TableRow tr = new TableRow();
				TableCell td = new TableCell();
				Label lbl = new Label();
				lbl.CssClass = "smNormalTxt";
				lbl.Text = org.OrgDescription;
				td.Controls.Add( lbl );
				tr.Controls.Add( td );
				tbl.Controls.Add( tr );
			}
		}
		
		protected void DisplayOrgInfo( Organization org )
		{
			Table tbl = tblOrgDescription;

			ShowAddress( tbl, org );
			ShowPhone( tbl, org );
			ShowWebsite( tbl, org );
			ShowDecription( tbl, org );
		}

		protected void DisplayHomePage(Organization org)
		{
			if( org.LogoURL.Length > 0 )
			{
				string strImageURL = "~/OrgFolders/" + org.Key + "/Logo/" + org.LogoURL;
				imgOrgLogo.ImageUrl = strImageURL;
			}

			DisplayOrgInfo( org );

			DisplayNews(org);
			DisplayAffiliates(org);


			if( org.orgEventList.Count > 0 )
			{
				lblNoSchedule.Visible = false;
				tblOrgSchedule.Visible = true;
				DisplayEvents( org );
			}
			else
			{
				lblNoSchedule.Visible = true;
				tblOrgSchedule.Visible = false;
			}


			bool fTemp = false;



			if (fTemp)
			{
				lblNoMessages.Visible = false;
				tblOrgMessages.Visible = true;
				DisplayMessages(org);
			}
			else
			{
				lblNoMessages.Visible = true;
				tblOrgMessages.Visible = false;
			}
		}
	}
}