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
	public partial class AcceptInvite : USCPageBase
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			bool fParamsValid = false;
			string strOrgID = Request.QueryString["OrgID"];
			string strIID = Request.QueryString["IID"];

			if (!IsPostBack)
			{
				long lOrgID = 0;
				long lIID = 0;

				if (null != strOrgID && long.TryParse(strOrgID, out lOrgID))
				{
					if (null != strIID && long.TryParse(strIID, out lIID))
					{
						SetSessionOrgID( lOrgID );
						SetSessionUserID( lIID );

						Organization org = (Organization)Master.g_OrgList.htOrgList[lOrgID];
						UserAccount invitee = Master.GetUserFromID(lIID);
						// TODO: Verify a vaild OM entry exists.
						if( null == invitee || null == org )
						{
							SetSessionOrgID( 0 );
							SetSessionUserID( 0 );
						}
						else
						{
							// If this is any user except a new roster invitee log them in first.
							if( invitee.UserType != UserAccount.UserTypes.Roster )
							{
								Uri uri = Request.Url;
								string strURL = uri.PathAndQuery;
								LoginAndReturn( true, strURL );
							}

							fParamsValid = true;
							StringBuilder sb = new StringBuilder();
							sb.AppendLine( "Greetings,<br><br>" );
							sb.Append( "You have been invited to join the " ).Append( org.OrgName ).Append(".&nbsp;&nbsp;");
							sb.AppendLine( "Please select the appropriate response below and click OK.<br><br>" );
							sb.Append( "If are already a member of MySportsConnect and you accept the invitation, you will become a permanent member of that organization and taken to their home page.&nbsp;&nbsp;<b><u>You may chose to leave it at any time.</u></b>&nbsp;&nbsp;If you decline the invitation, you will be removed from the " );
							sb.AppendLine( "organizations roster and taken to your dashboard page.&nbsp;&nbsp;If you decide later that you wish to join the organization, you will need to be invited again.<br><br>" );
							sb.AppendLine( "If you are brand new to the site, you will be transfered to the account creation page whether you chose to accept or decline the invitation.<br><br>" );

							litGreeting.Text = sb.ToString();
						}
					}
				}
				
				if( !fParamsValid )
				{
					string strEventText = "Invalid call to AcceptInvite with org = " + strOrgID + " and IID = " + strIID;
					EvtLog.WriteEvent( strEventText, System.Diagnostics.EventLogEntryType.Warning, 666, 0 );
					Master.AlertUser("Unable to process the request.");
					RedirectToLoginPage();
				}

			}
		}

		protected void OnClickOK(object sender, ImageClickEventArgs e)
		{
			bool fAccepted = rbAccept.Checked;

			long lIID = GetSessionUserID();
			long lOrgID = GetSessionOrgID();
			Organization org = (Organization)Master.g_OrgList.htOrgList[lOrgID];
			UserAccount invitee = Master.GetUserFromID(lIID);
			if( null == org || null == invitee )
			{
				// TODO : Error handling
			}
			else
			{
				bool fNewInvitee = false;
				// If this is a new roster invitee send them to account setup.
				if( invitee.UserType == UserAccount.UserTypes.Roster )
				{
					fNewInvitee = true;
				}

				OrgMember om = org.orgMemberList.GetOrgMember( invitee.AccountID );
				if( null == om )
				{
					// TODO : Error handling
				}
				else
				{
					if( fAccepted )
					{
						om.AcceptedInvite = fAccepted;
					}
					else
					{
						// TODO: Make sure deleted OMs don't display
						om.Deleted = true;
					}

					om.Update( invitee );
				}

				string strURL = "";
				string strOrgHome = "~/MyTeams/Home.aspx?OrgID=" + org.OrgID;
				if (fNewInvitee)
				{
					SetSessionUserID( invitee.AccountID );
					SetSessionValue1( 99 );
					SetSessionReturnURL( strOrgHome );
					//strURL = "~/MyAccount/MyAccount.aspx";

					strURL = "~/MyTeams/PickUsername.aspx?IID=" + lIID;
				}
				else
				{
					strURL = strOrgHome;
				}

				Response.Redirect(strURL);
			}
		}

		protected void OnClickCancel(object sender, ImageClickEventArgs e)
		{
			RedirectToLoginPage();
		}

		protected void OnChangedAccept(object sender, EventArgs e)
		{
			rbAccept.Checked = true;
			rbDecline.Checked = false;
		}

		protected void OnChangedDecline(object sender, EventArgs e)
		{
			rbAccept.Checked = false;
			rbDecline.Checked = true;
		}

	}
}