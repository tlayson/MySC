using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MyUSC.Classes;

namespace MyUSC
{
	public partial class CreateMsg : USCPageBase
	{
		private bool fSelectable;
		protected CreateMsg()
		{
			_PAGENAME = "CreateMsg";
			_pgUSCMaster = (USCMaster)Master;
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			// One time settings
			if (!IsPostBack)
			{
				bool fDDFilled = false;
				fSelectable = true;
				// If replying to a message, load current values
				string strMsgID = Request.QueryString["ThreadID"];
				string strTarget = Request.QueryString["target"];
				string strMsgTarget = Request.QueryString["MsgTarget"];
				long lMsgID = 0;
				Messages msg = null;
				if (long.TryParse(strMsgID, out lMsgID))
				{
					if( lMsgID > 0 )
					{
						msg = Master.g_MsgList.GetMessageByID(lMsgID);
						if( null != msg )
						{
							SetData(msg);
							fDDFilled = true;
						}
					}
				}

				string strUserName = GetCookieUserName();
				UserAccount msgAcct = (UserAccount)Master.g_AccountsList.GetAccountByKey( strMsgTarget );
				if( null != msgAcct )
				{
					fSelectable = false;
				}
				if( fSelectable )
				{
					if( !fDDFilled )
					{
						FillFriendsDD( strUserName, strTarget );
					}
				}
				else
				{
					hfMsgTarget.Value = strMsgTarget;
					lblMsgTo.Visible = true;
					lblMsgTo.Text = msgAcct.UserName;
					ddlSelectFriend.Visible = false;
					ddlSelectFriend.Enabled = false;
				}

				txtFrom.Text = strUserName;
			}

		}

		protected void SetData( Messages msg )
		{
			hfParentThread.Value = msg.Key.ToString();
			StringBuilder sb = new StringBuilder();
			sb.AppendLine();
			sb.AppendLine();
			sb.AppendLine("==========================================");
			sb.Append(msg.DataText1);
			// TODO: HTML encode the text
			txtMessageText.Text = sb.ToString();

			txtMessageTitle.Text = msg.DataText4;
			txtStoryTitle.Text = msg.DiscussionURLText;
			txtStoryLink.Text = msg.DiscussionURL;
			txtStoryPhotoLink.Text = msg.PhotoFile;
			chkPrivateMsg.Checked = msg.PrivateMsg;
			if( fSelectable )
			{
				ListItem li = new ListItem(msg.UserName, msg.AcctFrom.ToString());
				ddlSelectFriend.Items.Add(li);
				ddlSelectFriend.SelectedIndex = 0;
			}
		}

		protected void GetData( Messages msg )
		{
			UserAccount acct = GetActiveUser();

			msg.AcctFrom = acct.Key;
			string strMsgTarget = hfMsgTarget.Value;
			if( fSelectable )
			{
				ListItem li = ddlSelectFriend.SelectedItem;
				strMsgTarget = li.Value;
			}
			msg.AcctTo = Convert.ToInt64( strMsgTarget );
			msg.UserName = txtFrom.Text;
			msg.ThreadID = Convert.ToInt64(hfParentThread.Value);
			if( 0 == msg.ThreadID )
			{
				msg.ThreadParent = true;
			}
			msg.DataText1 = txtMessageText.Text;
			msg.DataText4 = txtMessageTitle.Text;
			msg.MsgDate = DateTime.Now;
			msg.DiscussionURLText = txtStoryTitle.Text;
			msg.DiscussionURL = txtStoryLink.Text;
			msg.PhotoFile = txtStoryPhotoLink.Text;
			msg.NewMessage = true;
			msg.PrivateMsg = chkPrivateMsg.Checked;
		}

		protected void FillFriendsDD(string strUserName, string strTarget)
		{
			ddlSelectFriend.Items.Clear();
			UserAccount acct = Master.g_AccountsList.GetAccountByUserName( strUserName );
			long lTargetID = 0;
			long.TryParse( strTarget, out lTargetID );

			if( null != acct )
			{
				FriendsList friendsList = Master.g_FriendsList;
				foreach (DictionaryEntry de in friendsList.htMyFriends)
				{
					MyFriend friend = (MyFriend)de.Value;
					if( friend.UserAccountID == acct.Key )
					{
						UserAccount friendAcct = (UserAccount)Master.g_AccountsList.htAccountsList[friend.FriendID];
						if (null != friendAcct)
						{
							string strFriendName = friendAcct.UserName;
							if( friendAcct.First.Length > 0 || friendAcct.Last.Length > 0 )
							{
								strFriendName = friendAcct.First + " " + friendAcct.Last;
							}
							ListItem li = new ListItem(strFriendName, friendAcct.Key.ToString());
							if( lTargetID == friendAcct.Key )
							{
								li.Selected = true;
							}
							ddlSelectFriend.Items.Add(li);
						}
					}
				}
			}
			else
			{
			}
		}

		protected void OnClickSendMessage(object sender, ImageClickEventArgs e)
		{
			Messages msg = new Messages();
			GetData( msg );
			MessagesList msgList = Master.g_MsgList;
			if( msgList.Add( msg ) )
			{
				Response.Redirect("Friends.aspx", true);
			}
			else
			{
				Master.AlertUser("There was a problem creating the message.  Please try again or go to the support page.");
			}
		}

		protected void OnClickCancelMessage(object sender, ImageClickEventArgs e)
		{
			Response.Redirect("Friends.aspx", true);
		}
	}
}