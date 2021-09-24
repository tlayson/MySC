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
	public partial class OrgPermissions : USCPageBase
	{
		int td1Width = 110;
		int td2Width = 250;
		int td3Width = 250;
		int td4Width = 150;
		int td5Width = 10;

		protected void Page_Load(object sender, EventArgs e)
		{
			string strOrgID = Request.QueryString["OrgID"];
			string strWiz = Request.QueryString["Wiz"];
			if (!IsPostBack)
			{
				long lOrgID = 0;
				if (null != strOrgID && long.TryParse(strOrgID, out lOrgID))
				{
					Master.SetMenuState(lOrgID, (string.Empty == strWiz));

					UserAccount acct = Master.GetActiveUser();
					if (UserHasViewAccess(acct.AccountID, lOrgID, OrgPageID.Manage))
					{
						SetSessionOrgID(lOrgID);
						Organization org = (Organization)Master.g_OrgList.htOrgList[lOrgID];
						if (!IsPostBack && null != org)
						{
							Master.PageHeading = "Manage Permissions - " + org.OrgName;
							EnablePageOptions(acct.AccountID, lOrgID);
							// Fill member list here
							FillMemberList(lOrgID);
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

		private void DrawHeaderRow()
		{
			TableRow trHeader = new TableRow();
			TableCell tdHeader = new TableCell();
			trHeader.Controls.Add(tdHeader);
			tblMembers.Controls.Add(trHeader);


			// Add a new table so we can draw a border
			Table tblHeader = new Table();
			tblHeader.BorderWidth = 0;
			//tblHeader.BorderColor = Color.White;
			//tblHeader.BorderStyle = BorderStyle.Solid;
			tdHeader.Controls.Add(tblHeader);

			TableRow tr = new TableRow();
			TableCell td = new TableCell();
			td.CssClass = "tdInput";
			td.Width = td1Width;
			Label lbl = new Label();
			lbl.Text = "  ";
			lbl.CssClass = "medSiteColorTxt";
			td.Controls.Add(lbl);
			tr.Controls.Add(td);

			td = new TableCell();
			td.CssClass = "tdInput";
			td.Width = td2Width;
			lbl = new Label();
			lbl.Text = "Player Name";
			lbl.CssClass = "medSiteColorTxt";
			td.Controls.Add(lbl);
			tr.Controls.Add(td);

			td = new TableCell();
			td.CssClass = "tdInput";
			td.Width = td3Width;
			lbl = new Label();
			lbl.Text = "Contact Info";
			lbl.CssClass = "medSiteColorTxt";
			td.Controls.Add(lbl);
			tr.Controls.Add(td);

			td = new TableCell();
			td.CssClass = "tdInput";
			td.Width = td4Width;
			lbl = new Label();
			lbl.Text = "Status";
			lbl.CssClass = "medSiteColorTxt";
			td.Controls.Add(lbl);
			tr.Controls.Add(td);

			td = new TableCell();
			td.CssClass = "tdInput";
			td.Width = td5Width;
			lbl = new Label();
			lbl.Text = "  ";
			lbl.CssClass = "medSiteColorTxt";
			td.Controls.Add(lbl);
			tr.Controls.Add(td);

			tblHeader.Controls.Add(tr);
		}

		private void FillMemberList( long lOrgID )
		{
			Organization org = GetOrgByID(lOrgID);
			if( null != org )
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
		}

/*
			<asp:DropDownList ID="DropDownList1" runat="server">
				<asp:ListItem Text="Owner" Value="1"></asp:ListItem>
				<asp:ListItem Text="Admin" Value="2"></asp:ListItem>
				<asp:ListItem Text="Contributor" Value="3"></asp:ListItem>
				<asp:ListItem Text="Member" Value="4"></asp:ListItem>
				<asp:ListItem Text="Follower" Value="5"></asp:ListItem>
				<asp:ListItem Text="Guest" Value="6"></asp:ListItem>
				<asp:ListItem Text="Banned" Value="10"></asp:ListItem>
			</asp:DropDownList>
 */
		private void DrawStatusList(TableCell td, UserAccount acct, OrgMember member)
		{
			DropDownList ddl = new DropDownList();
			ddl.ID = acct.AccountID.ToString();
			ddl.AutoPostBack = true;

			ListItem li = new ListItem();
			li.Text = "Owner";
			li.Value = "1";
			li.Selected = false;
			if( member.MemberType == OrgAccessTypes.Owner )
			{
				li.Selected = true;
			}
			ddl.Items.Add( li );

			li = new ListItem();
			li.Text = "Admin";
			li.Value = "2";
			li.Selected = false;
			if (member.MemberType == OrgAccessTypes.Admin)
			{
				li.Selected = true;
			}
			ddl.Items.Add(li);

			li = new ListItem();
			li.Text = "Contributor";
			li.Value = "3";
			li.Selected = false;
			if (member.MemberType == OrgAccessTypes.Contributor)
			{
				li.Selected = true;
			}
			ddl.Items.Add(li);

			li = new ListItem();
			li.Text = "Member";
			li.Value = "4";
			li.Selected = false;
			if (member.MemberType == OrgAccessTypes.Member)
			{
				li.Selected = true;
			}
			ddl.Items.Add(li);

			li = new ListItem();
			li.Text = "Follower";
			li.Value = "5";
			li.Selected = false;
			if (member.MemberType == OrgAccessTypes.Follower)
			{
				li.Selected = true;
			}
			ddl.Items.Add(li);

			li = new ListItem();
			li.Text = "Guest";
			li.Value = "6";
			li.Selected = false;
			if (member.MemberType == OrgAccessTypes.Guest)
			{
				li.Selected = true;
			}
			ddl.Items.Add(li);

			li = new ListItem();
			li.Text = "Banned";
			li.Value = "10";
			li.Selected = false;
			if (member.MemberType == OrgAccessTypes.Banned)
			{
				li.Selected = true;
			}
			ddl.Items.Add(li);

			td.Controls.Add( ddl );
		}

		private void DrawMember(UserAccount acct, OrgMember member, long lOrgID)
		{
			UserAccount viewerAcct = GetActiveUser();

			tblMembers.BorderColor = Color.Black;

			TableRow trMember = new TableRow();
			TableCell tdMember = new TableCell();
			trMember.Controls.Add( tdMember );
			tblMembers.Controls.Add( trMember );


			// Add a new table so we can draw a border
			Table tblDrawMember = new Table();
			tblDrawMember.BorderWidth = 1;
			tblDrawMember.BorderColor = Color.White;
			tblDrawMember.BorderStyle = BorderStyle.Solid;
			tdMember.Controls.Add( tblDrawMember );

			// Add 1st row
			TableRow tr1 = new TableRow();
			TableCell tdImg = new TableCell();
			tdImg.CssClass = "tdImage";
			tdImg.RowSpan = 3;
			tdImg.Width = td1Width;
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

			// Cell 2
			TableCell tdRow1Cell2 = new TableCell();
			tdRow1Cell2.CssClass = "tdInput";
			tdRow1Cell2.Width = td2Width;
			Label lblName = new Label();
			lblName.Text = acct.DisplayName();
			lblName.CssClass = "medNormalTxt";
			tdRow1Cell2.Controls.Add( lblName );

			// Cell 3
			TableCell tdRow1Cell3 = new TableCell();
			tdRow1Cell3.CssClass = "tdInput";
			tdRow1Cell3.Width = td3Width;
			Label lblContact1 = new Label();
			lblContact1.Text = "---";
			if (IsMember(viewerAcct.AccountID, lOrgID) && acct.Email.Length > 0)
			{
				lblContact1.Text = acct.Email;
			}
			lblContact1.CssClass = "medNormalTxt";
			tdRow1Cell3.Controls.Add(lblContact1);

			// Cell 4
			TableCell tdRow1Cell4 = new TableCell();
			tdRow1Cell4.Width = td4Width;
			DrawStatusList(tdRow1Cell4, acct, member);

			// Cell 5
			TableCell tdRow1Cell5 = new TableCell();
			tdRow1Cell5.CssClass = "tdInput";
			tdRow1Cell5.Width = td5Width;
			Label lblRow1Cell5 = new Label();
			lblRow1Cell5.Text = "   ";
			tdRow1Cell5.Controls.Add(lblRow1Cell5);

			tr1.Controls.Add( tdImg );
			tr1.Controls.Add(tdRow1Cell2);
			tr1.Controls.Add(tdRow1Cell3);
			tr1.Controls.Add(tdRow1Cell4);
			tr1.Controls.Add(tdRow1Cell5);

			// Add 2nd row
			TableRow tr2 = new TableRow();

			// Cell 2
			TableCell tdRow2Cell2 = new TableCell();
			tdRow2Cell2.CssClass = "tdInput";
			tdRow2Cell2.Width = td2Width;
			Label lblRow2Cell2 = new Label();
			lblRow2Cell2.Text = "   ";
			tdRow2Cell2.Controls.Add( lblRow2Cell2 );

			// Cell 3
			TableCell tdRow2Cell3 = new TableCell();
			tdRow2Cell3.CssClass = "tdInput";
			tdRow2Cell3.Width = td3Width;
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
			Label lblRow2Cell4 = new Label();
			lblRow2Cell4.Text = "   ";
			tdRow2Cell4.Controls.Add(lblRow2Cell4);

			// Cell 5
			TableCell tdRow2Cell5 = new TableCell();
			tdRow2Cell5.CssClass = "tdInput";
			tdRow2Cell5.Width = td5Width;
			Label lblRow2Cell5 = new Label();
			lblRow2Cell5.Text = "   ";
			tdRow2Cell5.Controls.Add(lblRow2Cell5);

			tr2.Controls.Add(tdRow2Cell2);
			tr2.Controls.Add(tdRow2Cell3);
			tr2.Controls.Add(tdRow2Cell4);
			tr2.Controls.Add(tdRow2Cell5);

			//Add 3rd row
			TableRow tr3 = new TableRow();

			// Cell 2
			TableCell tdRow3Cell2 = new TableCell();
			tdRow3Cell2.CssClass = "tdInput";
			tdRow3Cell2.Width = td2Width;
			Label lblRow3Cell2 = new Label();
			lblRow3Cell2.Text = "   ";
			tdRow3Cell2.Controls.Add(lblRow3Cell2);

			// Cell 3
			TableCell tdRow3Cell3 = new TableCell();
			tdRow3Cell3.CssClass = "tdInput";
			tdRow3Cell3.Width = td3Width;
			Label lblRow3Cell3 = new Label();
			lblRow3Cell3.Text = "   ";
			tdRow3Cell3.Controls.Add(lblRow3Cell2);

			// Cell 4
			TableCell tdRow3Cell4 = new TableCell();
			tdRow3Cell4.CssClass = "tdInput";
			tdRow3Cell4.Width = td4Width;
			Label lblRow3Cell4 = new Label();
			lblRow3Cell4.Text = "   ";
			tdRow3Cell4.Controls.Add(lblRow3Cell2);

			// Cell 5
			TableCell tdRow3Cell5 = new TableCell();
			tdRow3Cell5.CssClass = "tdInput";
			tdRow3Cell5.Width = td5Width;
			Label lblRow3Cell5 = new Label();
			lblRow3Cell5.Text = "   ";
			tdRow3Cell5.Controls.Add(lblRow3Cell2);

			tr3.Controls.Add(tdRow3Cell2);
			tr3.Controls.Add(tdRow3Cell3);
			tr3.Controls.Add(tdRow3Cell4);
			tr3.Controls.Add(tdRow3Cell5);

			tblDrawMember.Controls.Add(tr1);
			tblDrawMember.Controls.Add(tr2);
			tblDrawMember.Controls.Add(tr3);

		}

		protected void OnMemberStatusChange(object sender, EventArgs e)
		{
			DropDownList ddl = (DropDownList)sender;
			string strVal = ddl.SelectedValue;
			int nAT = 0;
			if( int.TryParse( strVal, out nAT ) )
			{
				string strID = ddl.ID;
				long orgID = GetSessionOrgID();
				OrgMember member = GetOrgMember(strID, orgID);
				member.MemberType = (OrgAccessTypes)nAT;
				UserAccount acct = GetUserByID( strID );
				if( null != member && null != acct )
				{
					member.Update( acct );
				}
				strID = "";
			}
		}

		protected void OnClickInvite(object sender, EventArgs e)
		{
			long lOrgID = GetSessionOrgID();
			string strURL = "~/MyTeams/AddMembers.aspx?OrgID=" + lOrgID;
			Response.Redirect( strURL );
		}
	}
}