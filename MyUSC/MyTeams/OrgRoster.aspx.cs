using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MyUSC.Classes;
using MyUSC.Classes.MyOrg;

namespace MyUSC.MyTeams
{
	public partial class OrgRoster : USCPageBase
	{
		int nCellBorder = 0;
		Color clrCellBorder = Color.White;
		BorderStyle bsCellBorder = BorderStyle.Dotted;
		int nRowBorder = 0;
		Color clrRowBorder = Color.White;
		BorderStyle bsRowBorder = BorderStyle.Dashed;
		int nTblBorder = 2;
		Color clrTblBorder = Color.FromArgb(16737792);
//		Color clrTblBorder = Color.FromArgb( Convert.ToInt32("FF6600"));
		BorderStyle bsTblBorder = BorderStyle.Solid;
		int td0Width = 50;
		int td1Width = 150;
		int td2Width = 250;
		int td3Width = 250;
		int td4Width = 150;
		int td5Width = 10;

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
							Master.PageHeading = "Roster - " + org.OrgName;
							EnablePageOptions(acct.AccountID, lOrgID);
						}
						FillMemberList(lOrgID);
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
				Master.SelectMTMenuItemOnPageLoad(MyTeamsPage.Roster);
			}
		}

		private void DrawHeaderRow()
		{
			TableRow tr = new TableRow();
			tr.BorderWidth = nRowBorder;
			tr.BorderColor = clrRowBorder;
			tr.BorderStyle = bsRowBorder;

			TableCell td = new TableCell();
			td.CssClass = "tdInput";
			td.Width = td0Width;
			td.BorderWidth = nCellBorder;
			td.BorderColor = clrCellBorder;
			td.BorderStyle = bsCellBorder;
			Label lbl = new Label();
			lbl.Text = " # ";
			lbl.CssClass = "medSiteColorTxt";
			td.Controls.Add(lbl);
			tr.Controls.Add(td);

			td = new TableCell();
			td.CssClass = "tdInput";
			td.Width = td1Width;
			td.BorderWidth = nCellBorder;
			td.BorderColor = clrCellBorder;
			td.BorderStyle = bsCellBorder;
			lbl = new Label();
			lbl.Text = "Picture";
			lbl.CssClass = "medSiteColorTxt";
			td.Controls.Add(lbl);
			tr.Controls.Add(td);

			td = new TableCell();
			td.CssClass = "tdInput";
			td.Width = td2Width;
			td.BorderWidth = nCellBorder;
			td.BorderColor = clrCellBorder;
			td.BorderStyle = bsCellBorder;
			lbl = new Label();
			lbl.Text = "Name";
			lbl.CssClass = "medSiteColorTxt";
			td.Controls.Add(lbl);
			tr.Controls.Add(td);

			td = new TableCell();
			td.CssClass = "tdInput";
			td.Width = td3Width;
			td.BorderWidth = nCellBorder;
			td.BorderColor = clrCellBorder;
			td.BorderStyle = bsCellBorder;
			lbl = new Label();
			lbl.Text = "Position(s)";
			lbl.CssClass = "medSiteColorTxt";
			td.Controls.Add(lbl);
			tr.Controls.Add(td);

			td = new TableCell();
			td.CssClass = "tdInput";
			td.Width = td4Width;
			td.BorderWidth = nCellBorder;
			td.BorderColor = clrCellBorder;
			td.BorderStyle = bsCellBorder;
			lbl = new Label();
			lbl.Text = "Status";
			lbl.CssClass = "medSiteColorTxt";
			td.Controls.Add(lbl);
			tr.Controls.Add(td);

			td = new TableCell();
			td.CssClass = "tdInput";
			td.Width = td5Width;
			td.BorderWidth = nCellBorder;
			td.BorderColor = clrCellBorder;
			td.BorderStyle = bsCellBorder;
			lbl = new Label();
			lbl.Text = "Last Activity";
			lbl.CssClass = "medSiteColorTxt";
			td.Controls.Add(lbl);
			tr.Controls.Add(td);

			//Add seperator
			TableRow tr4 = new TableRow();
			tr4.BorderWidth = nRowBorder;
			tr4.BorderColor = clrRowBorder;
			tr4.BorderStyle = bsRowBorder;
			
			TableCell tdRow4Cell0 = new TableCell();
			tdRow4Cell0.ColumnSpan = 6;

			Literal literal = new Literal();
			literal.Text = "<hr/>";
			tdRow4Cell0.Controls.Add( literal );
			tr4.Controls.Add( tdRow4Cell0 );

			tblRoster.Controls.Add(tr);
			tblRoster.Controls.Add(tr4);
		}

		private void FillMemberList( long lOrgID )
		{
			tblRoster.Controls.Clear();

			Organization org = GetOrgByID(lOrgID);
			if( null != org )
			{
				if( org.orgMemberList.m_sdOrgMembers.Count == 0 )
				{
					org.LoadLists();
				}

				if( org.orgMemberList.m_sdOrgMembers.Count > 0 )
				{
					DrawHeaderRow();
					foreach (KeyValuePair<long, object> kvp in org.orgMemberList.m_sdOrgMembers)
					{
						OrgMember member = (OrgMember)kvp.Value;
						if( null != member )
						{
							UserAccount acct = GetUserByID( member.UserID );
							if( null != acct )
							{
								DrawMember(acct, member, lOrgID);
							}
						}
					}
				}
				else
				{
					TableRow trError = new TableRow();
					TableCell tdError = new TableCell();
					Label lblError = new Label();
					lblError.Text = "No members were found for this team/organization.";
					lblError.CssClass = "medSiteColorTxt";
					tdError.Controls.Add(lblError);
					trError.Controls.Add(tdError);
					tblRoster.Controls.Add(trError);
				}
			}
		}

		private void DrawMember(UserAccount acct, OrgMember member, long lOrgID)
		{
			UserAccount viewerAcct = GetActiveUser();

			tblRoster.BorderWidth = nTblBorder;
			tblRoster.BorderColor = clrTblBorder;
			tblRoster.BorderStyle = bsTblBorder;

			// Cell 0 (Number)
			TableCell tdRow1Cell0 = new TableCell();
			tdRow1Cell0.CssClass = "tdInput";
			tdRow1Cell0.RowSpan = 3;
			tdRow1Cell0.Width = td0Width;
			tdRow1Cell0.BorderWidth = nCellBorder;
			tdRow1Cell0.BorderColor = clrCellBorder;
			tdRow1Cell0.BorderStyle = bsCellBorder;
			Label lblNumber = new Label();
			lblNumber.Text = member.Number;
			lblNumber.CssClass = "medNormalTxt";
			tdRow1Cell0.Controls.Add( lblNumber );

			// Add 1st row (Pic)
			TableRow tr1 = new TableRow();
			tr1.BorderWidth = nRowBorder;
			tr1.BorderColor = clrRowBorder;
			tr1.BorderStyle = bsRowBorder;

			TableCell tdImg = new TableCell();
			tdImg.CssClass = "tdImage";
			tdImg.RowSpan = 3;
			tdImg.Width = td1Width;
			tdImg.BorderWidth = nCellBorder;
			tdImg.BorderColor = clrCellBorder;
			tdImg.BorderStyle = bsCellBorder;
			ImageButton imgBtn = new ImageButton();
			imgBtn.ImageUrl = "~/Images/NoPhoto.JPG";
			imgBtn.Width = 100;
			imgBtn.Height = 100;
			if( acct.PhotoFile.Length > 0 )
			{
				string strImageURL = "~/UsersFolder/" + acct.UserName + "/DisplayPhoto/" + acct.PhotoFile;
				imgBtn.ImageUrl = strImageURL;
			}
			tdImg.Controls.Add( imgBtn );

			// Cell 2 (Name)
			TableCell tdRow1Cell2 = new TableCell();
			tdRow1Cell2.CssClass = "tdInput";
			tdRow1Cell2.Width = td2Width;
			tdRow1Cell2.BorderWidth = nCellBorder;
			tdRow1Cell2.BorderColor = clrCellBorder;
			tdRow1Cell2.BorderStyle = bsCellBorder;
			Label lblName = new Label();
			lblName.Text = acct.DisplayName();
			lblName.CssClass = "medNormalTxt";
			tdRow1Cell2.Controls.Add( lblName );

			// Cell 3 (Positions)
			TableCell tdRow1Cell3 = new TableCell();
			tdRow1Cell3.CssClass = "tdInput";
			tdRow1Cell3.Width = td3Width;
			tdRow1Cell3.BorderWidth = nCellBorder;
			tdRow1Cell3.BorderColor = clrCellBorder;
			tdRow1Cell3.BorderStyle = bsCellBorder;
			Label lblPositions = new Label();
			lblPositions.Text = member.Positions;
			lblPositions.CssClass = "medNormalTxt";
			tdRow1Cell3.Controls.Add(lblPositions);

			// Cell 4 (Status )
			TableCell tdRow1Cell4 = new TableCell();
			tdRow1Cell4.Width = td4Width;
			tdRow1Cell4.BorderWidth = nCellBorder;
			tdRow1Cell4.BorderColor = clrCellBorder;
			tdRow1Cell4.BorderStyle = bsCellBorder;
			Label lblStatus = new Label();
			lblStatus.Text = member.MemberType.ToString();
			tdRow1Cell4.Controls.Add( lblStatus );

			// Cell 3 (Last Activity)
			TableCell tdRow1Cell5 = new TableCell();
			tdRow1Cell5.CssClass = "tdInput";
			tdRow1Cell5.Width = td5Width;
			tdRow1Cell5.BorderWidth = nCellBorder;
			tdRow1Cell5.BorderColor = clrCellBorder;
			tdRow1Cell5.BorderStyle = bsCellBorder;
			Label lblLastLog = new Label();
			lblLastLog.Text = "---";
			if( IsMember( viewerAcct.AccountID, lOrgID ) )
			{
				lblLastLog.Text = acct.LastLogin.ToShortDateString();
			}
			lblLastLog.CssClass = "medNormalTxt";
			tdRow1Cell5.Controls.Add(lblLastLog);

			tr1.Controls.Add(tdRow1Cell0);
			tr1.Controls.Add( tdImg );
			tr1.Controls.Add(tdRow1Cell2);
			tr1.Controls.Add(tdRow1Cell3);
			tr1.Controls.Add(tdRow1Cell4);
			tr1.Controls.Add(tdRow1Cell5);

			// Add 2nd row
			TableRow tr2 = new TableRow();
			tr2.BorderWidth = nRowBorder;
			tr2.BorderColor = clrRowBorder;
			tr2.BorderStyle = bsRowBorder;

			// Cell 2
			TableCell tdRow2Cell2 = new TableCell();
			tdRow2Cell2.CssClass = "tdInput";
			tdRow2Cell2.Width = td2Width;
			tdRow2Cell2.BorderWidth = nCellBorder;
			tdRow2Cell2.BorderColor = clrCellBorder;
			tdRow2Cell2.BorderStyle = bsCellBorder;
			Label lblRow2Cell2 = new Label();
			lblRow2Cell2.Text = "   ";
			tdRow2Cell2.Controls.Add( lblRow2Cell2 );

			// Cell 3
			TableCell tdRow2Cell3 = new TableCell();
			tdRow2Cell3.CssClass = "tdInput";
			tdRow2Cell3.Width = td3Width;
			tdRow2Cell3.BorderWidth = nCellBorder;
			tdRow2Cell3.BorderColor = clrCellBorder;
			tdRow2Cell3.BorderStyle = bsCellBorder;
			Label lblContact2 = new Label();
			lblContact2.Text = "---";
			if (IsMember(viewerAcct.AccountID, lOrgID) && acct.Phone.Length > 0)
			{
				lblContact2.Text = acct.Phone;
			}
			lblContact2.CssClass = "medNormalTxt";
			tdRow2Cell3.Controls.Add(lblContact2);

			// Cell 4
			TableCell tdRow2Cell4 = new TableCell();
			tdRow2Cell4.CssClass = "tdInput";
			tdRow2Cell4.Width = td4Width;
			tdRow2Cell4.BorderWidth = nCellBorder;
			tdRow2Cell4.BorderColor = clrCellBorder;
			tdRow2Cell4.BorderStyle = bsCellBorder;
			Label lblRow2Cell4 = new Label();
			lblRow2Cell4.Text = "   ";
			tdRow2Cell4.Controls.Add(lblRow2Cell4);

			// Cell 5
			TableCell tdRow2Cell5 = new TableCell();
			tdRow2Cell5.CssClass = "tdInput";
			tdRow2Cell5.Width = td5Width;
			tdRow2Cell5.BorderWidth = nCellBorder;
			tdRow2Cell5.BorderColor = clrCellBorder;
			tdRow2Cell5.BorderStyle = bsCellBorder;
			Label lblRow2Cell5 = new Label();
			lblRow2Cell5.Text = "   ";
			tdRow2Cell5.Controls.Add(lblRow2Cell5);

			tr2.Controls.Add(tdRow2Cell2);
			tr2.Controls.Add(tdRow2Cell3);
			tr2.Controls.Add(tdRow2Cell4);
			tr2.Controls.Add(tdRow2Cell5);

			//Add 3rd row
			TableRow tr3 = new TableRow();
			tr3.BorderWidth = nRowBorder;
			tr3.BorderColor = clrRowBorder;
			tr3.BorderStyle = bsRowBorder;

			// Cell 2
			TableCell tdRow3Cell2 = new TableCell();
			tdRow3Cell2.CssClass = "tdInput";
			tdRow3Cell2.Width = td2Width;
			tdRow3Cell2.BorderWidth = nCellBorder;
			tdRow3Cell2.BorderColor = clrCellBorder;
			tdRow3Cell2.BorderStyle = bsCellBorder;
			Label lblRow3Cell2 = new Label();
			lblRow3Cell2.Text = "   ";
			tdRow3Cell2.Controls.Add(lblRow3Cell2);

			// Cell 3
			TableCell tdRow3Cell3 = new TableCell();
			tdRow3Cell3.CssClass = "tdInput";
			tdRow3Cell3.Width = td3Width;
			tdRow3Cell3.BorderWidth = nCellBorder;
			tdRow3Cell3.BorderColor = clrCellBorder;
			tdRow3Cell3.BorderStyle = bsCellBorder;
			Label lblRow3Cell3 = new Label();
			lblRow3Cell3.Text = "   ";
			tdRow3Cell3.Controls.Add(lblRow3Cell2);

			// Cell 4
			TableCell tdRow3Cell4 = new TableCell();
			tdRow3Cell4.CssClass = "tdInput";
			tdRow3Cell4.Width = td4Width;
			tdRow3Cell4.BorderWidth = nCellBorder;
			tdRow3Cell4.BorderColor = clrCellBorder;
			tdRow3Cell4.BorderStyle = bsCellBorder;
			Label lblRow3Cell4 = new Label();
			lblRow3Cell4.Text = "   ";
			tdRow3Cell4.Controls.Add(lblRow3Cell2);

			// Cell 5
			TableCell tdRow3Cell5 = new TableCell();
			tdRow3Cell5.CssClass = "tdInput";
			tdRow3Cell5.Width = td5Width;
			tdRow3Cell5.BorderWidth = nCellBorder;
			tdRow3Cell5.BorderColor = clrCellBorder;
			tdRow3Cell5.BorderStyle = bsCellBorder;
			Label lblRow3Cell5 = new Label();
			lblRow3Cell5.Text = "   ";
			tdRow3Cell5.Controls.Add(lblRow3Cell2);

			tr3.Controls.Add(tdRow3Cell2);
			tr3.Controls.Add(tdRow3Cell3);
			tr3.Controls.Add(tdRow3Cell4);
			tr3.Controls.Add(tdRow3Cell5);

			//Add 4th row
			TableRow tr4 = new TableRow();
			tr4.BorderWidth = nRowBorder;
			tr4.BorderColor = clrRowBorder;
			tr4.BorderStyle = bsRowBorder;
			
			TableCell tdRow4Cell0 = new TableCell();
			tdRow4Cell0.ColumnSpan = 6;

			Literal literal = new Literal();
			literal.Text = "<hr/>";
			tdRow4Cell0.Controls.Add( literal );
			tr4.Controls.Add( tdRow4Cell0 );

			tblRoster.Controls.Add(tr1);
			tblRoster.Controls.Add(tr2);
			tblRoster.Controls.Add(tr3);
			tblRoster.Controls.Add(tr4);
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
	}
}