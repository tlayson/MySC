using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MyUSC.Classes;

namespace MyUSC
{
	public partial class FriendRequestPage : USCPageBase
	{
		protected FriendRequestPage()
		{
			_PAGENAME = "FriendRequest";
			_pgUSCMaster = (USCMaster)Master;
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			if( !IsPostBack )
			{
				Master.SelectMenuItem(SelectedPage.Friends);

				UserAccount acct = GetActiveUser();
				string strTarget = Request.QueryString["target"];
				long lTargetID = 0;
				long.TryParse(strTarget, out lTargetID);
				UserAccount targetAcct = (UserAccount)Master.g_AccountsList.htAccountsList[lTargetID];
				if( null != targetAcct )
				{
					hfTarget.Value = strTarget;
					lblFriend.Text = targetAcct.DisplayName();
				}
				else
				{
					// TODO: Set an error message
				}
			}
		}

		private void LeavePage()
		{
			string strRet = GetSessionReturnURL();
			if (strRet.Length == 0)
			{
				strRet = "Friends.aspx";
				SetSessionPageSection("FindFriends");
			}
			Response.Redirect(strRet);
		}

		protected void OnClickSend(object sender, ImageClickEventArgs e)
		{
			UserAccount acct = GetActiveUser();
			string strTarget = hfTarget.Value;

			FriendRequest fr = new FriendRequest();
			fr.AccountID = acct.Key;
			fr.FriendID = Convert.ToInt64( strTarget );
			fr.Comments = txtComments.Text;

			Master.g_FriendRequests.Add( fr, acct.UserName );

			LeavePage();
		}

		protected void OnClickCancel(object sender, ImageClickEventArgs e)
		{
			LeavePage();
		}
	}
}