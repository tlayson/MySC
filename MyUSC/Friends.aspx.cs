using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Globalization;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Text.RegularExpressions;
using MyUSC.Classes;
using CuteChat;
using Facebook;
using System.Web.UI.HtmlControls;
using System.Web.SessionState;

namespace MyUSC
{
	public partial class Friends : USCPageBase
	{
		enum PageSections { MsgBoard, FindFriends, FriendRequests, AllFriends }

#region SetVariables
#endregion

#region Localization constants
		//English localization string indexes
		const int langENUS = 1;
		// Master page
		const long enusIdxMasterAccountFinish = 1001;

		//Labels
		const long enusIdxFriendsLbl0 = 252;
		const long enusIdxFriendsLbl1 = 253;
		const long enusIdxFriendsLbl2 = 254;
		const long enusIdxFriendsLbl3 = 255;
		const long enusIdxFriendsLbl4 = 256;
		const long enusIdxFriendsLbl5 = 257;
		const long enusIdxFriendsLbl6 = 258;
		const long enusIdxFriendsLbl7 = 277;
		const long enusIdxFriendsLblDate = 300;
		const long enusIdxFriendsLblFilterFirst = 336;
		const long enusIdxFriendsLblFilterLast = 337;
		const long enusIdxFriendsLblCity = 338;
		const long enusIdxFriendsLblState = 339;
		const long enusIdxFriendsLblZip = 340;
		const long enusIdxFriendsLblFavSports = 341;
		const long enusIdxFriendsLblVidLink = 1002;
		const long enusIdxFriendsLblNewMsgs = 1168;
		const long enusIdxFriendsLblFavorites = 1204;
		const long enusIdxFriendsLblFBFriends = 1264;

		// Buttons
		const long enusIdxFriendsBtnFindFriends = 267;
		const long enusIdxFriendsBtnCancel = 280;
		const long enusIdxFriendsBtnSendMsg = 301;
		const long enusIdxFriendsBtnClearMsg = 302;
		const long enusIdxFriendsBtnComment = 304;
		const long enusIdxFriendsBtnSendBlog = 322;
		const long enusIdxFriendsBtnClearBlog = 323;
		const long enusIdxFriendsBtnFilterFriends = 332;
		const long enusIdxFriendsBtnMsgRefresh = 772;
		const long enusIdxFriendsBtnShowAll = 794;
		const long enusIdxFriendsBtnBlogRefresh = 876;
		const long enusIdxFriendsBtnAttachImage = 973;
		const long enusIdxFriendsBtnCreateImage = 997;
		const long enusIdxFriendsBtnPlayVideo = 1078;
		const long enusIdxFriendsBtnDeleteAllMgs = 1169;
		const long enusIdxFriendsBtnMarkAllRead = 1208;

		// Tooltips
		const long enusIdxFriendsTTTxt0 = 259;
		const long enusIdxFriendsTTTxt1 = 260;
		const long enusIdxFriendsTTTxt2 = 261;
		const long enusIdxFriendsTTTxt3 = 262;
		const long enusIdxFriendsTTTxt4 = 264;
		const long enusIdxFriendsTTDdl0 = 265;
		const long enusIdxFriendsTTSearch = 999;
		const long enusIdxFriendsTTDelAllMsg = 314;
		const long enusIdxFriendsTTGetFilterFriends = 792;
		const long enusIdxFriendsTTShowAll = 793;
		const long enusIdxFriendsTTMsgBoard = 799;
		const long enusIdxFriendsTTAllBlogs = 874;
		const long enusIdxFriendsTTAttachImage = 974;
		const long enusIdxFriendsTTCreateMsg = 998;
		const long enusIdxFriendsTTVidLink = 1003;
		const long enusIdxFriendsTTPlayVid = 1077;
		const long enusIdxFriendsTTChooseAll = 1148;
		const long enusIdxFriendsTTDeleteAll = 1171;
		const long enusIdxFriendsTTMarkAllRead = 1172;
		const long enusIdxFriendsTTDdlFavs = 1205;
		const long enusIdxFriendsTTNewMsg = 1211;
		const long enusIdxFriendsTTMsgRefresh = 1214;
		const long enusIdxFriendsTTSendMsg = 1217;
		const long enusIdxFriendsTTClearMsg = 1218;
		const long enusIdxFriendsTTBlogRefresh = 1227;
		const long enusIdxFriendsTTSendBlog = 1228;
		const long enusIdxFriendsTTClearBlog = 1229;
		const long enusIdxFriendsTTRblBlogs = 1232;
		const long enusIdxFriendsTTFilterFirst = 1236;
		const long enusIdxFriendsTTFilterLast = 1237;
		const long enusIdxFriendsTTFilterFavSport = 1238;
		const long enusIdxFriendsTTCity = 1239;
		const long enusIdxFriendsTTStates = 1240;
		const long enusIdxFriendsTTZipcode = 1241;
		const long enusIdxFriendsTTSearchCancel = 1242;
		const long enusIdxFriendsTTFBFriends = 1265;
		const long enusIdxFriendsTTDtlMsgs = 1256;

		// Grid view columns
		const long enusIdxFriendsGdvFindFriend1 = 271;
		const long enusIdxFriendsGdvFindFriend2 = 273;
		const long enusIdxFriendsGdvFindFriend3 = 274;
		const long enusIdxFriendsGdvMyFriends1 = 281;
		const long enusIdxFriendsGdvMyFriends2 = 282;
		const long enusIdxFriendsGdvFriendReq1 = 283;
		const long enusIdxFriendsGdvFriendReq2 = 284;
		const long enusIdxFriendsGdvFriendReq4 = 285;
		const long enusIdxFriendsGdvFriendReq5 = 286;
		const long enusIdxFriendsGdvChkText = 287;
		const long enusIdxFriendsGdvAcceptItem = 288;
		const long enusIdxFriendsGdvDecline = 289;
		const long enusIdxFriendsGdvBtnRequest = 290;
		const long enusIdxFriendsGdvBtnMutualFriends = 291;
		const long enusIdxFriendsGdvFriendReq3 = 1090;
		const long enusIdxFriendsGdvBlock = 1096;

		//Checkboxes
		const long enusIdxFriendsChkChoseAll = 1146;

		//Radio buttons
		const long enusIdxFriendsRblBlogs = 1143;

		// Messages
		const long enusIdxFriendsMSG0 = 269;
		const long enusIdxFriendsMSG1 = 270;
		const long enusIdxFriendsMSG2 = 275;
		const long enusIdxFriendsMSG3 = 276;
		const long enusIdxFriendsMSG4 = 278;
		const long enusIdxFriendsMSG5 = 279;
		const long enusIdxFriendsMSG6 = 292;
		const long enusIdxFriendsMSG7 = 293;
		const long enusIdxFriendsMSG8 = 294;
		const long enusIdxFriendsMSG9 = 295;
		const long enusIdxFriendsMSG10 = 296;
		const long enusIdxFriendsMSG11 = 297;
		const long enusIdxFriendsMSG12 = 303;
		const long enusIdxFriendsMSG13 = 305;
		const long enusIdxFriendsMSG14 = 306;
		const long enusIdxFriendsMSG15 = 312;
		const long enusIdxFriendsMSG16 = 316;
		const long enusIdxFriendsMSG17 = 317;
		const long enusIdxFriendsMSG18 = 318;
		const long enusIdxFriendsMSG19 = 319;
		const long enusIdxFriendsMSG20 = 320;
		const long enusIdxFriendsMSG21 = 324;
		const long enusIdxFriendsMSG22 = 325;
		const long enusIdxFriendsMSG23 = 326;
		const long enusIdxFriendsMSG24 = 327;
		const long enusIdxFriendsMSG25 = 335;
		const long enusIdxFriendsMSG26 = 774;
		const long enusIdxFriendsMSG27 = 787;
		const long enusIdxFriendsMSG28 = 790;
		const long enusIdxFriendsMSG29 = 791;
		const long enusIdxFriendsMSG30 = 795;
		const long enusIdxFriendsMSG31 = 979;
		const long enusIdxFriendsMSG32 = 981;
		const long enusIdxFriendsMSG33 = 994;
		const long enusIdxFriendsMSG34 = 1000;
		const long enusIdxFriendsMSG35 = 1135;
		const long enusIdxFriendsMSG36 = 1136;
		const long enusIdxFriendsMSG37 = 1137;
		const long enusIdxFriendsMSG38 = 1139;
		const long enusIdxFriendsMSG39 = 1140;
		const long enusIdxFriendsMSG40 = 1141;
		const long enusIdxFriendsMSG41 = 1142;
		const long enusIdxFriendsMSG42 = 1144;
		const long enusIdxFriendsMSG43 = 1145;
		const long enusIdxFriendsMSG44 = 1147;
		const long enusIdxFriendsMSG45 = 1157;
		const long enusIdxFriendsMSG46 = 1158;
		const long enusIdxFriendsMSG47 = 1159;
		const long enusIdxFriendsMSG48 = 1160;
		const long enusIdxFriendsMSG49 = 1173;
		const long enusIdxFriendsMSG50 = 1174;
		const long enusIdxFriendsMSG51 = 1175;
		const long enusIdxFriendsMSG52 = 1184;
		const long enusIdxFriendsMSG53 = 1185;
		const long enusIdxFriendsMSG54 = 1212;
		const long enusIdxFriendsMSG55 = 1213;
		const long enusIdxFriendsMSG56 = 1215;
		const long enusIdxFriendsMSG57 = 1216;
		const long enusIdxFriendsMSG58 = 1220;
		const long enusIdxFriendsMSG59 = 1221;
		const long enusIdxFriendsMSG60 = 1222;
		const long enusIdxFriendsMSG61 = 1223;
		const long enusIdxFriendsMSG62 = 1224;
		const long enusIdxFriendsMSG63 = 1225;
		const long enusIdxFriendsMSG64 = 1226;
		const long enusIdxFriendsMSG65 = 1230;
		const long enusIdxFriendsMSG66 = 1231;
		const long enusIdxFriendsMSG67 = 1233;
		const long enusIdxFriendsMSG68 = 1234;
		const long enusIdxFriendsMSG69 = 1235;
		const long enusIdxFriendsMSG70 = 1255;
		const long enusIdxFriendsMSG71 = 1258;
		const long enusIdxFriendsMSG72 = 1262;
		const long enusIdxFriendsMSG73 = 1263;
		const long enusIdxFriendsMSG74 = 1269;
		const long enusIdxFriendsMSG75 = 1270;
		const long enusIdxFriendsMSGTTMarkRead = 1209;
		const long enusIdxFriendsMSGBtnMarkRead = 1208;
		const long enusIdxFriendsMSGChkOffensive = 1138;
		const long enusIdxFriendsMSGBtnComment = 304;
		const long enusIdxFriendsMSGTTDislike = 1187;
		const long enusIdxFriendsMSGTTLike = 1186;
		const long enusIdxFriendsMSGLikeBlog = 1188;
		const long enusIdxFriendsMSGDislikeBlog = 1189;
		const long enusIdxFriendsBtnDeleteFriends = 1256;

#endregion

#region Init
		protected Friends()
		{
			_PAGENAME = "Friends";
			_pgUSCMaster = (USCMaster)Master;
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			if( !IsPostBack )
			{
				//here we check to see if the user is logged in
				UserAccount acct = GetActiveUser();
				if( null == acct )
				{
					RedirectToLoginPage();
				}

				lblUserName.Text = acct.UserName;
				if (acct.PhotoFile.Length > 0)
				{
					string strImageURL = "~/UsersFolder/" + acct.UserName + "/DisplayPhoto/" + acct.PhotoFile;
					imgUserPhoto.ImageUrl = strImageURL;
				}

				PageSections ps = PageSections.MsgBoard;
				string strSection = GetSessionPageSection();
				if ("FindFriends" == strSection)
				{
					ps = PageSections.FindFriends;
				}
				else if ("AllFriends" == strSection)
				{
					ps = PageSections.AllFriends;
				}
				else if ("FriendReq" == strSection)
				{
					ps = PageSections.FriendRequests;
				}
				DisplaySections(ps);
			}

			LoadUserMessages();
			LoadFriendRequests();
			LoadFriends();

			Master.SelectMenuItem(SelectedPage.Friends);
		}
#endregion

#region Messages
		string BuildLink( long key, string txt, bool fNew)
		{
			string strAnchorTag = "<a class=\"medSiteColorTxt\" style=\"font-weight: {0}\" href=\"ReadMessage.aspx?MsgID=";
			StringBuilder sbLink = new StringBuilder();
			string strWeight = "normal";
			if( fNew )
			{
				strWeight = "bolder";
			}
			sbLink.AppendFormat(strAnchorTag, strWeight).Append(key).Append("\">");
			sbLink.Append(txt).Append("</a>");
			return sbLink.ToString();
		}
		
		void LoadUserMessages()
		{
			UserAccount acct = GetActiveUser();
			Hashtable htUserMessages = null;
			int nTotalMsgs = 0;
			int nNewMsgs = 0;
			Master.g_MsgList.FindUserMessageCounts(acct.Key, out nTotalMsgs, out nNewMsgs);
			Master.g_MsgList.FindUserMessages(acct.Key, out htUserMessages);

			if (0 < nTotalMsgs)
			{
				lnkMenuMsgBoard.Text = "Message Board (" + nTotalMsgs + ")";
			}
			else
			{
				lnkMenuMsgBoard.Text = "Message Board";
			}
			lblNewMsgs.Text = "Unread Messages : " + nNewMsgs;

			if( null == htUserMessages || 0 == htUserMessages.Count )
			{
				pnlNoMessages.Visible = true;
			}
			else
			{
				pnlNoMessages.Visible = false;

				try
				{
					foreach (DictionaryEntry de in htUserMessages)
					{
						Messages msg = (Messages)de.Value;

						TableCell tdFrom = new TableCell();
						tdFrom.Text = BuildLink(msg.Key, msg.UserName, msg.NewMessage);

						TableCell tdTitle = new TableCell();
						tdTitle.Text = BuildLink(msg.Key, msg.DataText4, msg.NewMessage);

						TableCell tdDate = new TableCell();
						tdDate.Text = BuildLink(msg.Key, msg.MsgDate.ToString(), msg.NewMessage); ;

						TableRow tr = new TableRow();
						tr.Cells.Add(tdFrom);
						tr.Cells.Add(tdTitle);
						tr.Cells.Add(tdDate);
						tblMessages.Rows.Add(tr);
					}
				}
				catch( Exception ex )
				{
					EvtLog.WriteException("Friends.LoadUserMessages failure", ex, 0);
				}
			}
		}
#endregion

#region FriendRequests
		string BuildFriendRequestLink(long friendID, string txt)
		{
			string strAnchorTag = "<a class=\"medSiteColorTxt\" style=\"font-weight: bolder\" href=\"FriendDetails.aspx?FriendID=";
			StringBuilder sbLink = new StringBuilder();
			sbLink.Append(strAnchorTag).Append(friendID).Append("\">");
			sbLink.Append(txt).Append("</a>");
			return sbLink.ToString();
		}

		void LoadFriendRequests()
		{
			UserAccount acct = GetActiveUser();
			Hashtable htResult = null;
			Master.g_FriendRequests.FindUserFriendRequests(acct.Key, out htResult);
			if (null == htResult || 0 == htResult.Count)
			{
				pnlNoFriendRequests.Visible = true;
			}
			else
			{
				pnlNoFriendRequests.Visible = false;
				if (0 < htResult.Count)
				{
					lnkMenuFriendRequests.Text = "Friend Requests (" + htResult.Count + ")";
				}
				else
				{
					lnkMenuFriendRequests.Text = "Friend Requests";
				}

				try
				{
					foreach (DictionaryEntry de in htResult)
					{
						FriendRequest friendReq = (FriendRequest)de.Value;
						UserAccount friendAcct = (UserAccount)Master.g_AccountsList.htAccountsList[friendReq.AccountID];

						Image imgFriend = new Image();
						string strImageURL = "~/Images/NoPhoto.JPG";
						if (friendAcct.PhotoFile.Length > 0)
						{
							strImageURL = "~/UsersFolder/" + friendAcct.UserName + "/DisplayPhoto/" + friendAcct.PhotoFile;
						}
						imgFriend.ImageUrl = strImageURL;
						Unit unit = new Unit(60);
						imgFriend.Height = unit;
						imgFriend.Width = unit;
						imgFriend.ToolTip = friendReq.Comments;

						ImageButton btnAccept = new ImageButton();
						btnAccept.ImageUrl = "~/Images/Button/btnAccept.png";
						btnAccept.Command += new CommandEventHandler(this.CommandBtn_Click);
						btnAccept.CommandName = "AcceptReq";
						btnAccept.CommandArgument = friendReq.Key.ToString();

						ImageButton btnDecline = new ImageButton();
						btnDecline.ImageUrl = "~/Images/Button/btnDecline.png";
						btnDecline.Command += new CommandEventHandler(this.CommandBtn_Click);
						btnDecline.CommandName = "DeclineReq";
						btnDecline.CommandArgument = friendReq.Key.ToString();

						TableCell tdPic = new TableCell();
						tdPic.Controls.Add(imgFriend);

						TableCell tdFriend = new TableCell();
						tdFriend.Text = BuildFriendDetailsLink(friendReq.FriendID, friendAcct.DisplayName());

						TableCell tdAccept = new TableCell();
						tdAccept.Controls.Add(btnAccept);

						TableCell tdDecline = new TableCell();
						tdDecline.Controls.Add(btnDecline);

						TableRow tr = new TableRow();
						tr.Cells.Add(tdPic);
						tr.Cells.Add(tdFriend);
						tr.Cells.Add(tdAccept);
						tr.Cells.Add(tdDecline);
						int nIndex = tblFriendRequests.Rows.Add(tr);
						nIndex = 0;
					}
				}
				catch (Exception ex)
				{
					EvtLog.WriteException("Friends page load friend requests failure", ex, 0);
				}
			}
		}

#endregion

#region FriendsList
		void LoadFriends()
		{
			UserAccount acct = GetActiveUser();
			Hashtable htUserFriends = null;
			Master.g_FriendsList.FindUserFriends(acct.Key, out htUserFriends);

			if (null == htUserFriends || 0 == htUserFriends.Count)
			{
				pnlNoFriends.Visible = true;
			}
			else
			{
				pnlNoFriends.Visible = false;

				try
				{
					foreach (DictionaryEntry de in htUserFriends)
					{
						MyFriend friend = (MyFriend)de.Value;
						UserAccount friendAcct = (UserAccount)Master.g_AccountsList.htAccountsList[friend.FriendID];

						Image imgFriend = new Image();
						string strImageURL = "~/Images/NoPhoto.JPG";
						if( friendAcct.PhotoFile.Length > 0 )
						{
							strImageURL = "~/UsersFolder/" + friendAcct.UserName + "/DisplayPhoto/" + friendAcct.PhotoFile;
						}
						imgFriend.ImageUrl = strImageURL;
						Unit unit = new Unit(60);
						imgFriend.Height = unit;
						imgFriend.Width = unit;

						ImageButton btnMessage = new ImageButton();
						btnMessage.ImageUrl = "~/Images/Button/btnNewMsg.png";
						btnMessage.Command += new CommandEventHandler(this.CommandBtn_Click);
						btnMessage.CommandName = "NewMessage";
						btnMessage.CommandArgument = friend.FriendID.ToString();

						TableCell tdPic = new TableCell();
						tdPic.Controls.Add( imgFriend );

						TableCell tdFriend = new TableCell();
						tdFriend.Text = BuildFriendDetailsLink(friend.FriendID, friendAcct.DisplayName());

						TableCell tdLastLogin = new TableCell();
						tdLastLogin.Text = friendAcct.LastLogin.ToShortDateString();

						TableCell tdNewMessage = new TableCell();
						tdNewMessage.Controls.Add(btnMessage);

						TableRow tr = new TableRow();
						tr.Cells.Add(tdPic);
						tr.Cells.Add(tdFriend);
						tr.Cells.Add(tdLastLogin);
						tr.Cells.Add(tdNewMessage);
						tblFriendsList.Rows.Add(tr);
					}
				}
				catch (Exception ex)
				{
					EvtLog.WriteException("Friends page load friends failure", ex, 0);
				}
			}
		}
#endregion

#region Display Sections
		void DisplaySections(PageSections section)
		{
			DisplayAllFriendsSection(PageSections.AllFriends == section);
			DisplayFindFriendsSection(PageSections.FindFriends == section);
			DisplayFriendRequestsSection(PageSections.FriendRequests == section);
			DisplayMsgBoardSection(PageSections.MsgBoard == section);
		}

		void DisplayAllFriendsSection(bool fVisible)
		{
			imgMenuAllFriends.Visible = fVisible;
			pnlAllFriends.Visible = fVisible;
		}

		void DisplayFindFriendsSection(bool fVisible)
		{
			imgMenuFindFriends.Visible = false;
		}

		void DisplayFriendRequestsSection(bool fVisible)
		{
			imgMenuFriendRequests.Visible = fVisible;
			pnlFriendRequests.Visible = fVisible;
		}

		void DisplayMsgBoardSection(bool fVisible)
		{
			imgMenuMsgBoard.Visible = fVisible;
			pnlMsgBoard.Visible = fVisible;
		}

		protected void OnClickMenuMessageBoard(object sender, EventArgs e)
		{
			DisplaySections(PageSections.MsgBoard);
		}

		protected void OnClickMenuFindFriends(object sender, EventArgs e)
		{
			SetSessionPageSection("");
			Response.Redirect("FindFriends.aspx");
		}

		protected void OnClickMenuFriendRequests(object sender, EventArgs e)
		{
			DisplaySections(PageSections.FriendRequests);
		}

		protected void OnClickMenuAllFriends(object sender, EventArgs e)
		{
			DisplaySections(PageSections.AllFriends);
		}

#endregion

#region ButtonClicks
		protected void OnClickNewMsg(object sender, ImageClickEventArgs e)
		{
			Response.Redirect( "CreateMsg.aspx", true );
		}

		void CommandBtn_Click(Object sender, CommandEventArgs e)
		{
			string strCmd = e.CommandName;
			string strArg = e.CommandArgument.ToString();
			string strURL = "";
			if ("NewMessage" == strCmd)
			{
				strURL = "CreateMsg.aspx?target=" + strArg;
				SetSessionReturnURL("Friends.aspx");
				SetSessionPageSection("AllFriends");
				if (strURL.Length > 0)
				{
					Response.Redirect(strURL, true);
				}
			}
			else if ("AcceptReq" == strCmd)
			{
				long lReqID = Convert.ToInt64( strArg );
				FriendRequest fr = (FriendRequest)Master.g_FriendRequests.htFriendRequest[lReqID];
				if( null != fr )
				{
					Master.g_FriendsList.AddFromRequest( fr );
					Master.g_FriendRequests.Delete(lReqID);
				}
				else
				{
				}
				SetSessionPageSection("FindFriends");
				DisplaySections(PageSections.FindFriends);
			}
			else if ("DeclineReq" == strCmd)
			{
				long lReqID = Convert.ToInt64(strArg);
				// TODO: Should we notify the requestor of the decline?
				Master.g_FriendRequests.Decline(lReqID);
				SetSessionPageSection("FindFriends");
				DisplaySections(PageSections.FindFriends);
			}
		}
#endregion

	}
}