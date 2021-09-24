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
	public partial class FindFriends : USCPageBase
	{
		int nMaxDisplay = 20;

		protected FindFriends()
		{
			_PAGENAME = "FindFriends";
			_pgUSCMaster = (USCMaster)Master;
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			Master.SelectMenuItem(SelectedPage.Friends);
			if (!IsPostBack)
			{
				ddlState.SelectedIndex = -1;

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

				PopulateDropDownLists();
			}
			else
			{
				LookForFriends();
			}
		}

		void PopulateDropDownLists()
		{
			//populate ddlState *****************************************************
			var lstState = Master.GetSiteSetting(SiteAdmin.saKeyStates, "StateNames");
			List<string> lstStates = new List<string>();
			lstStates = lstState.Trim().Split(',').ToList();
			ddlState.Items.Clear();

			for (int intCounter = 0; intCounter < lstStates.Count; intCounter++)
			{
				ddlState.Items.Add(lstStates[intCounter].Trim());
			}
		}

#region Menu Clicks
		protected void OnClickMenuMessageBoard(object sender, EventArgs e)
		{
			// Default is Message board
			SetSessionPageSection("");
			Response.Redirect( "Friends.aspx" );
		}

		protected void OnClickMenuFindFriends(object sender, EventArgs e)
		{
			// TODO: Clear results ?
		}

		protected void OnClickMenuFriendRequests(object sender, EventArgs e)
		{
			SetSessionPageSection("FriendReq");
			Response.Redirect("Friends.aspx");
		}

		protected void OnClickMenuAllFriends(object sender, EventArgs e)
		{
			SetSessionPageSection("AllFriends");
			Response.Redirect("Friends.aspx");
		}
#endregion

#region FindFriends
		void AddFriendRow( int nRow, UserAccount acct )
		{
			TableRow tr = new TableRow();
			TableCell td1 = new TableCell();
			td1.CssClass = "tdImage";
			TableCell td2 = new TableCell();
			td2.CssClass = "tdImage";
			td2.Width = 150;
			TableCell td3 = new TableCell();
			td3.CssClass = "tdButton";
			TableCell td4 = new TableCell();
			td4.CssClass = "tdButton";

			Image img = new Image();
			string strImageURL = "~/Images/NoPhoto.JPG";
			if (acct.PhotoFile.Length > 0)
			{
				strImageURL = "~/UsersFolder/" + acct.UserName + "/DisplayPhoto/" + acct.PhotoFile;
			}
			img.ImageUrl = strImageURL;
			img.Height = 100;
			img.Width = 100;
			td1.Controls.Add( img );

			HyperLink hlDetails = new HyperLink();
			hlDetails.Text = acct.DisplayName();
			hlDetails.NavigateUrl = "FriendDetails.aspx?FriendID=" + acct.Key;
			td2.Controls.Add( hlDetails );

			ImageButton btnSendRequest = new ImageButton();
			btnSendRequest.ImageUrl = "~/Images/Button/btnSendRequest.png";
			btnSendRequest.CommandName = "request";
			btnSendRequest.CommandArgument = acct.Key.ToString();
			btnSendRequest.Command += new CommandEventHandler(this.OnClickResultButton);
			td3.Controls.Add( btnSendRequest );

			ImageButton btnSendMessage = new ImageButton();
			btnSendMessage.ImageUrl = "~/Images/Button/btnSendMessage.png";
			btnSendMessage.CommandName = "message";
			btnSendMessage.CommandArgument = acct.Key.ToString();
			btnSendMessage.Command += new CommandEventHandler(this.OnClickResultButton);
			td4.Controls.Add( btnSendMessage );

			tr.Controls.Add( td1 );
			tr.Controls.Add( td2 );
			tr.Controls.Add( td3 );
			tr.Controls.Add( td4 );
			tblNewResults.Controls.Add( tr );
		}

		void LookForFriends()
		{
			bool fNew = true;
			if( hdnSearchExecuted.Value == "1" )
			{
				tblNewResults.Controls.Clear();

				// Set up search criteria
				UserAccount acct = new UserAccount();
				acct.First = txtFirstName.Text;
				acct.Last = txtLastName.Text;
				acct.City = txtCity.Text;
				acct.Preferences.Interests = txtInterest.Text;
				string strState = ddlState.SelectedValue;
				if (strState != "---")
				{
					acct.State = strState;
				}
				else
				{
					acct.State = "";
				}
				acct.Zip = txtPostalCode.Text;
				if( ddlResultCount.SelectedValue == "10" )
				{
					nMaxDisplay = 10;
				}
				else if( ddlResultCount.SelectedValue == "25" )
				{
					nMaxDisplay = 25;
				}
				else if( ddlResultCount.SelectedValue == "50" )
				{
					nMaxDisplay = 50;
				}
				else if( ddlResultCount.SelectedValue == "75" )
				{
					nMaxDisplay = 75;
				}
				else
				{
					nMaxDisplay = 25;
				}

				pnlFindResults.Visible = true;

				if( fNew )
				{
					SortedDictionary<long, object> sdResults = new SortedDictionary<long, object>();
					if (0 < Master.g_AccountsList.QueryPossibleFriends(acct, nMaxDisplay, sdResults))
					{
						int nRowID = 0;
						foreach (KeyValuePair<long, object> kvp in sdResults)
						{
							UserAccount friendAcct = (UserAccount)kvp.Value;
							AddFriendRow( nRowID, friendAcct );
						}
					}
				}
				else
				{
				}
			}
		}

		protected void OnSelChange(object sender, EventArgs e)
		{
		}

		protected void OnTextChanged(object sender, EventArgs e)
		{
		}
#endregion

#region Button Clicks
		protected void OnClickResultButton(object sender, CommandEventArgs e)
		{
			//LookForFriends();
			string strCmd = e.CommandName;
			string strArg = e.CommandArgument.ToString();
			if ("request" == strCmd)
			{
				long lReqID = Convert.ToInt64(strArg);
				FriendRequest fr = new FriendRequest();
				fr.AccountID = GetActiveUser().Key;
				fr.FriendID = lReqID;
				fr.Comments = "Please accept my friend request.";
				if (Master.g_FriendRequests.Add(fr, GetCookieUserName()))
				{
					ImageButton btnSendRequest = (ImageButton)sender;
					btnSendRequest.ImageUrl = "~/Images/Button/btnRequestSent.png";
					btnSendRequest.CommandName = "";
					btnSendRequest.CommandArgument = "";
				}
			}
			else if ("message" == strCmd)
			{
				string strURL = "~/CreateMsg.aspx?MsgTarget=" + strArg;
				Response.Redirect( strURL );
			}
		}

		protected void OnClickFindFriends(object sender, ImageClickEventArgs e)
		{
			hdnSearchExecuted.Value = "1";
			LookForFriends();
		}

		protected void OnClickCancelFindFriends(object sender, ImageClickEventArgs e)
		{
			SetSessionPageSection("AllFriends");
			Response.Redirect("Friends.aspx");
		}
#endregion
	}
}