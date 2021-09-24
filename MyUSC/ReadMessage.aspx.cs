using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MyUSC.Classes;

namespace MyUSC
{
	public partial class ReadMessage : USCPageBase
	{
		protected ReadMessage()
		{
			_PAGENAME = "ReadMessage";
			_pgUSCMaster = (USCMaster)Master;
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			string strMsgID = Request.QueryString["MsgID"];
			long lMsgID = 0;
			Messages msg = null;
			if (long.TryParse(strMsgID, out lMsgID))
			{
				msg = Master.g_MsgList.GetMessageByID(lMsgID);
			}

			lblMsgError.Text = "";
			lblMsgError.Visible = true;
			if (null != msg)
			{
				UserAccount acct = GetActiveUser();
				if (acct.Key == msg.AcctTo)
				{
					lblFrom.Text = msg.UserName;
					lblDate.Text = msg.MsgDate.ToShortDateString();
					lblTitle.Text = msg.DataText4;
					lblDiscussion.Text = msg.DiscussionURLText;
					lblLink.Text = msg.DiscussionURL;
					lblVideo.Text = msg.VideoLink;
					txtMessage.Text = msg.DataText1;

					imgMsgPhoto.Visible = false;
					if (msg.PhotoFile.Length > 0)
					{
						imgMsgPhoto.ImageUrl = msg.PhotoFile;
						imgMsgPhoto.Visible = true;
					}
					else if (msg.PhotoUploadURL.Length > 0)
					{
						imgMsgPhoto.ImageUrl = msg.PhotoUploadURL;
						imgMsgPhoto.Visible = true;
					}
					msg.MarkAsNew(false, GetCookieUserName());
				}
				else
				{
					lblMsgError.Text = "You do not have permission to view this message.";
					lblMsgError.Visible = true;
				}
			}
			else
			{
				lblMsgError.Text = "Message not found or does not exist.";
				lblMsgError.Visible = true;
			}

		}

		protected void OnClickReturn(object sender, ImageClickEventArgs e)
		{
			Response.Redirect( "Friends.aspx", true );
		}

		protected void OnClickReply(object sender, ImageClickEventArgs e)
		{
			string strMsgID = Request.QueryString["MsgID"];
			string strPage = "CreateMsg.aspx?ThreadID=" + strMsgID;
			Response.Redirect(strPage, true);
		}

		protected void OnClickDelete(object sender, ImageClickEventArgs e)
		{
			string strMsgID = Request.QueryString["MsgID"];
			long lMsgID = 0;
			Messages msg = null;
			if (long.TryParse(strMsgID, out lMsgID))
			{
				msg = Master.g_MsgList.GetMessageByID(lMsgID);
				if (null != msg)
				{
					msg.Delete();
				}
			}
			Response.Redirect("Friends.aspx", true);
		}

		protected void OnClickMarkUnread(object sender, ImageClickEventArgs e)
		{
			string strMsgID = Request.QueryString["MsgID"];
			long lMsgID = 0;
			Messages msg = null;
			if (long.TryParse(strMsgID, out lMsgID))
			{
				msg = Master.g_MsgList.GetMessageByID(lMsgID);
				if( null != msg )
				{
					msg.MarkAsNew(true, GetCookieUserName());
				}
			}
		}
	}
}