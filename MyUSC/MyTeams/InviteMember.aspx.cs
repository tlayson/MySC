using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MyUSC.Classes;
using MyUSC.Classes.MyOrg;
using CuteEditor;
using CuteEditor.ImageEditor;

namespace MyUSC.MyTeams
{
	public partial class InviteMember : USCPageBase
	{
		const string k_strReplace = "QWERTYUIOP";
		protected void Page_Load(object sender, EventArgs e)
		{
			string strOrgID = Request.QueryString["OrgID"];
			string strIID = Request.QueryString["IID"];
			if( !IsPostBack )
			{
				long lOrgID = 0;
				long lIID = 0;
				hfUserID.Value = "0";
				txtFirst.Text = "";
				txtLast.Text = "";
				txtEmailAddress.Text = "";
				txtFirst.ReadOnly = false;
				txtLast.ReadOnly = false;
				txtEmailAddress.ReadOnly = false;

				ddlAccessLevel.SetSelectedType( OrgAccessTypes.Member );

				if( null != strOrgID && long.TryParse( strOrgID, out lOrgID ) )
				{
					Master.SetMenuState( lOrgID, false );

					// Are we sending to an existing user?
					if( null != strIID && long.TryParse( strIID, out lIID ) )
					{
						UserAccount invitee = Master.GetUserFromID( lIID );
						if( null != invitee )
						{
							hfUserID.Value = lIID.ToString();
							txtFirst.Text = invitee.First;
							txtLast.Text = invitee.Last;
							txtEmailAddress.Text = invitee.Email;
							txtFirst.ReadOnly = true;
							txtLast.ReadOnly = true;
							txtEmailAddress.ReadOnly = true;
						}
						else
						{
							lIID = 0;
						}
					}

					UserAccount acct = Master.GetActiveUser();
					if( UserHasViewAccess( acct.AccountID, lOrgID, OrgPageID.Manage ) )
					{
						SetSessionOrgID(lOrgID);
						Organization org = (Organization)Master.g_OrgList.htOrgList[lOrgID];
						if (!IsPostBack && null != org)
						{
							Master.PageHeading = "Invite member - " + org.OrgName;
							EnablePageOptions( acct.AccountID, lOrgID );
						}
						EnablePageOptions( acct.AccountID, lOrgID );
					}
					else
					{
						Master.AlertUser("You do not have permission to view this page.");
						Response.Redirect("~/MyTeams/Dashboard.aspx");
					}

					lblInstructions.Text = BuildInstructions( lIID );
					htmlEmailMsg.Text = BuildSampleEmail( acct, lOrgID, lIID );
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

		private string BuildInstructions( long lIID )
		{
			StringBuilder sb = new StringBuilder();

			if( lIID > 0 )
			{
				sb.Append("Make sure the email address below is correct for the person you would like to invite to join your team on MySportsConnect.net.  ");
				sb.Append("Then edit the message below to personalize it for them.  When you are done click send.");
			}
			else
			{
				sb.Append("Enter the email address below of the person you would like to invite to join your team on MySportsConnect.net.  ");
				sb.Append("Then edit the message below to personalize it for them.  When you are done click send.");
			}


			return sb.ToString();
		}

		private string BuildSampleEmail( UserAccount acct, long lOrgID, long lIID )
		{
			Organization org = (Organization)Master.g_OrgList.htOrgList[lOrgID];
			string strURL = "http://www.mysportsconnect.net/MyTeams/AcceptInvite.aspx?orgID=" + lOrgID.ToString();
			StringBuilder sb = new StringBuilder();

			if( lIID > 0 )
			{
				UserAccount invitee = Master.GetUserFromID( lIID );
				if( null != invitee )
				{
					string strInvitee = "";
					strURL += "&IID=" + lIID.ToString();
					strInvitee = " " + invitee.First;
					sb.Append("Hello").Append( strInvitee ).Append( "," ).AppendLine("<br><br>");
					sb.Append( acct.DisplayName() );
					sb.Append(" would like to invite you to join their team ");
					sb.Append( org.OrgName );
					sb.Append(".").AppendLine("<br><br>");
					sb.Append("If you would like to join this team, please go to <a href=\"").Append( strURL ).Append( "\">MySportsConnect.Net</a> and accept the invitation.").AppendLine("<br><br>");
					sb.Append("Thank you").AppendLine("<br><br>");
					sb.Append("The MySportsConnectStaff").AppendLine("<br><br>");
				}
				else
				{
					sb.Append( "There was a problem loading the invitee's account information.  Please go back and search again." );
					btnSend.Enabled = false;
				}
			}
			else
			{
				strURL += "&IID={0}";
				sb.Append("Hello,").AppendLine("<br><br>");
				sb.Append( acct.DisplayName() );
				sb.Append(" would like to invite you to join their team ");
				sb.Append( org.OrgName );
				sb.Append(".  They have created a temporary account for you that you can claim by clicking the link below.").AppendLine("<br><br>");
				sb.Append("If you would like to join this team, please go to <a href=\"").Append( strURL ).Append( "\">MySportsConnect.Net</a> and activate your account.").AppendLine("<br><br>");
				sb.Append("Thank you").AppendLine("<br><br>");
				sb.Append("The MySportsConnectStaff").AppendLine("<br><br>");
			}

			return sb.ToString();
		}

		protected void OnClickSend(object sender, ImageClickEventArgs e)
		{
			// TODO :: Verify values and make sure email address isn't already used
			string strTo = txtEmailAddress.Text;
			string strEmail = htmlEmailMsg.Text;
			UserAccount acct = Master.GetActiveUser();
			long orgID = GetSessionOrgID();
			string strURL = "~/MyTeams/AddMembers.aspx?OrgID=" + orgID;

			if (null == acct)
			{
				Master.AlertUser("There was a problem sending the email.  Please re-login and try again.");
				RedirectToLoginPage();
			}

			try
			{
				UserAccount acctInvitee = null;
				if( hfUserID.Value.Length == 0 || hfUserID.Value == "0")
				{
					acctInvitee = new UserAccount();
					acctInvitee.First = txtFirst.Text;
					acctInvitee.Last = txtLast.Text;
					acctInvitee.Email = txtEmailAddress.Text;
					acctInvitee.UserType = UserAccount.UserTypes.Roster;
					acctInvitee.Creator = acct.UserName;
					
					if( Master.g_AccountsList.AddUser( acctInvitee ) )
					{
						strEmail = string.Format( strEmail, acctInvitee.AccountID.ToString() );
					}
					else
					{
						Master.AlertUser( "Failed to create the account.  Please try again." );
						Response.Redirect( strURL );
					}
				}
				else
				{
					// We have an existing user
					long lInviteeID = 0;
					if( long.TryParse( hfUserID.Value, out lInviteeID ) )
					{
						acctInvitee = Master.GetUserFromID( lInviteeID );
					}
				}

				if( txtDetails.Text.Length > 0 )
				{
					StringBuilder sb = new StringBuilder();
					sb.AppendLine( "=== Personal Message ===</br></br>" );
					sb.AppendLine( txtDetails.Text );

					strEmail += sb.ToString();
				}

				if( null != acctInvitee )
				{
					// Send message
					EmailUtil email = new EmailUtil( Master.g_SiteAdmin );
					if( email.SendInfoMail(acct.Email, strTo, "MyTeams invitation", strEmail) )
					{
						Organization org = (Organization)Master.g_OrgList.htOrgList[orgID];
						OrgMember om = new OrgMember();
						om.OrgID = orgID;
						om.UserID = acctInvitee.AccountID;
						//om.MemberType = OrgAccessTypes.Member;
						om.MemberType = ddlAccessLevel.GetSelectedType();
						om.AcceptedInvite = false;

						org.orgMemberList.Add( om );

						// TODO :: Add indicator to roster showing non-accepted invites.
						// TODO :: Allow creator to edit account until claimed.
					}
					else
					{
						//Error condition more likely
					}
				}
				else
				{
					//Error condition
				}
			}
			catch( Exception ex )
			{
				string strErr = ex.Message;
			}
			Response.Redirect( strURL );
		}

		protected void OnClickCancel(object sender, ImageClickEventArgs e)
		{
			long orgID = GetSessionOrgID();
			string strURL = "~/MyTeams/AddMembers.aspx?OrgID=" + orgID;
			Response.Redirect( strURL );
		}
	}
}