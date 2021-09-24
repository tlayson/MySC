using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Globalization;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Text.RegularExpressions;
using CuteChat;
using Facebook;
using System.Web.UI.HtmlControls;
using System.Web.SessionState;
using MyUSC.Classes;
using MyUSC.Classes.MyOrg;

//
// TODO : Need a way to leave an org and delete/transfer if you are the owner
//
namespace MyUSC.MyTeams
{
	public partial class Dashboard : USCPageBase
	{
#region
		const int colKey = 0;
		const int colOrgID = 1;
		const int colVenueID = 2;
		const int colSeasonID = 3;
		const int colEventType = 4;
		const int colEventName = 5;
		const int colAltLocation = 6;
		const int colEventDate = 7;
		const int colOpponentID = 8;
		const int colOpponent = 9;
		const int colHomeAway = 10;
		const int colUniforms = 11;
		const int colEventResult = 12;
		const int colComments = 13;
		const int colURL = 14;
		const int colRequestResponse = 15;
		const int colResponseLevel = 16;
		const int colSendReminder = 17;
		const int colReminderLevel = 18;
		const int colReminderDays = 19;
		const int colEditLevel = 20;
		const int colViewLevel = 21;
		const int colReminderSent = 22;
		const int colReservedLong = 23;
		const int colReservedString = 24;
		const int colDeleted = 25;
		const int colCreator = 26;
		const int colCreateDate = 27;
		const int colLastUpdate = 28;
#endregion
		protected void Page_Load(object sender, EventArgs e)
		{
			UserAccount acct = Master.GetActiveUser();
			if (null == acct)
			{
				SetSessionReturnURL("~/MyTeams/Dashboard.aspx");
				RedirectToLoginPage();
			}

			LoadMyOrgs( acct );
			LoadMyEvents( acct );
			Master.SelectMenuItem(SelectedPage.Teams);
		}

		private void DrawOrgTable( Table tbl, OrgMember member )
		{
			int td1Width = 110;
			int td2Width = 250;
			Organization org = GetOrgByID(member.OrgID);
			if (!org.ListsLoaded)
			{
				org.LoadLists();
			}

			TableRow trOrg = new TableRow();
			TableCell tdOrg = new TableCell();
			trOrg.Controls.Add(tdOrg);
			tbl.Controls.Add(trOrg);
			
			Table tblDrawOrg = new Table();
			tblDrawOrg.BorderWidth = 1;
			tblDrawOrg.BorderColor = Color.White;
			tblDrawOrg.BorderStyle = BorderStyle.Solid;
			tdOrg.Controls.Add(tblDrawOrg);

			TableRow tr1 = new TableRow();
			TableRow tr2 = new TableRow();
			TableRow tr3 = new TableRow();
			tblDrawOrg.Controls.Add(tr1);
			tblDrawOrg.Controls.Add(tr2);
			tblDrawOrg.Controls.Add(tr3);

			// Build URL
			String strURL = "~/MyTeams/Home.aspx?OrgID=";
			strURL += org.OrgID;

			// Logo
			TableCell tdImg = new TableCell();
			tdImg.CssClass = "tdImage";
			tdImg.RowSpan = 3;
			tdImg.Width = td1Width;
			ImageButton imgBtn = new ImageButton();
			imgBtn.ImageUrl = "~/Images/NoPhoto.JPG";
			imgBtn.Width = 100;
			imgBtn.Height = 100;
			imgBtn.PostBackUrl = strURL;
			if (org.LogoURL.Length > 0)
			{
				string strImageURL = "~/OrgFolders/" + org.Key + "/Logo/" + org.LogoURL;
				imgBtn.ImageUrl = strImageURL;
			}
			tdImg.Controls.Add(imgBtn);
			tr1.Controls.Add(tdImg);

			// Org Name
			TableCell td = new TableCell();
			td.CssClass = "tdInput";
			td.Width = td2Width;
			LinkButton lnkBtn = new LinkButton();
			lnkBtn.Text = org.OrgName;
			lnkBtn.CssClass = "medNormalTxt";
			lnkBtn.PostBackUrl = strURL;
			td.Controls.Add(lnkBtn);
			tr1.Controls.Add(td);

			// Org Type
			td = new TableCell();
			td.CssClass = "tdInput";
			td.Width = td2Width;
			Label lbl = new Label();
			lbl.Text = org.OrgType.ToString();
			lbl.CssClass = "medNormalTxt";
			td.Controls.Add(lbl);
			tr2.Controls.Add(td);

			// Access level
			td = new TableCell();
			td.CssClass = "tdInput";
			td.Width = td2Width;
			lbl = new Label();
			lbl.Text = member.MemberType.ToString();
			lbl.CssClass = "medNormalTxt";
			td.Controls.Add(lbl);
			tr3.Controls.Add(td);
		}

		private void DrawOwnedOrg(OrgMember member)
		{
			Table tbl = tblOwnedOrgs;

			DrawOrgTable( tbl, member );
		}

		private void DrawMemberOrg(OrgMember member)
		{
			Table tbl = tblMemberOrgs;

			DrawOrgTable(tbl, member);
		}

		private void DrawFollowedOrg(OrgMember member)
		{
			Table tbl = tblFollowedOrgs;

			DrawOrgTable(tbl, member);
		}

		private void LoadMyOrgs(UserAccount acct)
		{
			bool fOwnedOrg = false;
			bool fMemberOrg = false;
			bool fFollowedOrg = false;

			if (acct.m_sdMyOrgs.Count == 0)
			{
				acct.LoadMyOrgs();
			}

			if( acct.m_sdMyOrgs.Count == 0 )
			{
				// Message to tell them about adding orgs
			}
			else
			{
				foreach (KeyValuePair<long, object> kvp in acct.m_sdMyOrgs)
				{
					OrgMember member = (OrgMember)kvp.Value;
					if (null != member)
					{
						if( member.MemberType == OrgAccessTypes.Owner )
						{
							DrawOwnedOrg( member );
							fOwnedOrg = true;
						}
						else if (member.MemberType > OrgAccessTypes.Owner && member.MemberType <= OrgAccessTypes.Member)
						{
							DrawMemberOrg( member );
							fMemberOrg = true;
						}
						else if (member.MemberType != OrgAccessTypes.Banned)
						{
							DrawFollowedOrg( member );
							fFollowedOrg = true;
						}
					}
				}
			}

			pnlOwnedOrgs.Visible = fOwnedOrg;
			pnlMemberOrgs.Visible = fMemberOrg;
			pnlFollowedOrgs.Visible = fFollowedOrg;
		}

		private void DrawEvent( Table tbl, Event evt )
		{
			TableRow trEvent = new TableRow();
			TableCell tdEvent = new TableCell();
			trEvent.Controls.Add(tdEvent);
			tbl.Controls.Add(trEvent);

			Label lbl = new Label();
			lbl.Text = evt.EventName;
			lbl.CssClass = "medNormalTxt";
			tdEvent.Controls.Add(lbl);

		}

		private bool ReadEvent(IDataRecord dr, Event evt, string strCnx)
		{
			bool fRet = true;
			try
			{
				evt.ConnectionString = strCnx;
				evt.EventID = ObjectToLong(dr[colKey]);
				evt.OrgID = ObjectToLong(dr[colOrgID]);
				evt.VenueID = ObjectToLong(dr[colVenueID]);
				evt.SeasonID = ObjectToLong(dr[colSeasonID]);
				evt.EventType = (Event.EventTypes)ObjectToInt(dr[colEventType]);
				evt.EventName = ObjectToString(dr[colEventName]);
				evt.AltLocation = ObjectToString(dr[colAltLocation]);
				evt.EventDate = ObjectToDateTime(dr[colEventDate]);
				evt.OpponentID = ObjectToLong(dr[colOpponentID]);
				evt.Opponent = ObjectToString(dr[colOpponent]);
				evt.HomeAway = ObjectToString(dr[colHomeAway]);
				evt.Uniform = ObjectToString(dr[colUniforms]);
				evt.EventResult = ObjectToString(dr[colEventResult]);
				evt.Comments = ObjectToString(dr[colComments]);
				evt.URL = ObjectToString(dr[colURL]);
				evt.RequestResponse = ObjectToBool(dr[colRequestResponse]);
				evt.ResponseLevel = ObjectToInt(dr[colResponseLevel]);
				evt.SendReminder = ObjectToBool(dr[colSendReminder]);
				evt.ReminderLevel = ObjectToInt(dr[colReminderLevel]);
				evt.ReminderDays = ObjectToInt(dr[colReminderDays]);
				evt.EditLevel = ObjectToInt(dr[colEditLevel]);
				evt.ViewLevel = ObjectToInt(dr[colViewLevel]);
				evt.ReminderSent = ObjectToInt(dr[colReminderSent]);
				evt.ReservedLong = ObjectToLong(dr[colReservedLong]);
				evt.ReservedString = ObjectToString(dr[colResponseLevel]);

				evt.Deleted = ObjectToBool(dr[colDeleted]);
				evt.Creator = ObjectToString(dr[colCreator]);
				evt.CreateDate = ObjectToDateTime(dr[colCreateDate]);
				evt.LastUpdate = ObjectToString(dr[colLastUpdate]);
			}
			catch (Exception ex)
			{
				EvtLog.WriteException("Dashboard.ReadEvent failure", ex, 0);
				fRet = false;
			}
			return fRet;
		}

		private void DrawMyEvents( UserAccount acct, bool fPastEvents )
		{
			Table tbl = tblFutureEvents;
			Panel pnl = pnlFutureEvents;
			string strSP = "sp_GetUserDashboardFutureEvents";
			if( fPastEvents )
			{
				tbl = tblPastEvents;
				pnl = pnlPastEvents;
				strSP = "sp_GetUserDashboardPastEvents";
			}

			SortedDictionary<long, object> sdEvents = new SortedDictionary<long, object>();

			SqlDataReader reader = null;
			SqlConnection sqlConn = null;
			SqlParameter[] paramArray = new SqlParameter[1];
			paramArray[0] = new SqlParameter("@UserID", acct.AccountID);

			if (SQLHelper.ExecuteSPRows( strSP, acct.ConnectionString, paramArray, out reader, out sqlConn) && null != reader )
			{
				try
				{
					while (reader.Read())
					{
						IDataRecord dr = (IDataRecord)reader;
						Event evt = new Event();
						if( ReadEvent(dr, evt, acct.ConnectionString) )
						{
							pnl.Visible = true;
							DrawEvent( tbl, evt );
						}
					}
					reader.Close();
					sqlConn.Close();
				}
				catch (Exception ex)
				{
					string strErr = "Error loading Org events for " + acct.AccountID;
					short sCat = 0;
					if (IsLocalInstance())
					{
						strErr += " [Local] ";
						sCat = 99;
					}
					EvtLog.WriteException(strErr, ex, EventErrors.ErrorType.OrgLoadEvents, sCat);
				}
			}
		}

		private void LoadMyEvents(UserAccount acct)
		{
			pnlPastEvents.Visible = false;
			pnlFutureEvents.Visible = false;
/*
			if (null != acct && acct.AccountID > 0)
			{
				DrawMyEvents( acct, false );
				DrawMyEvents( acct, true );
			}
*/
		}

		private string BuildLink(long key, string txt, bool fNew)
		{
			string strAnchorTag = "<a style=\"font-weight: {0}\" href=\"Home.aspx?OrgID=";
			StringBuilder sbLink = new StringBuilder();
			string strWeight = "normal";
			if (fNew)
			{
				strWeight = "bolder";
			}
			sbLink.AppendFormat(strAnchorTag, strWeight).Append(key).Append("\">");
			sbLink.Append(txt).Append("</a>");
			return sbLink.ToString();
		}

		private void AddOrg(OrgMember member)
		{
			Organization org = new Organization();  // Get the org
			Table tblOrg = null;

			if( member.MemberType == OrgAccessTypes.Owner || member.MemberType == OrgAccessTypes.Admin )
			{
				tblOrg = tblOwnedOrgs;
			}
			else if( member.MemberType == OrgAccessTypes.Contributor || member.MemberType == OrgAccessTypes.Member )
			{
				tblOrg = tblMemberOrgs;
			}
			else if( member.MemberType == OrgAccessTypes.Follower )
			{
				tblOrg = tblFollowedOrgs;
			}

			if( null != tblOrg )
			{
				TableCell tdLogo = new TableCell();
				ImageButton imgButton = new ImageButton();
				//tdFrom.Text = BuildLink(msg.Key, msg.UserName, msg.NewMessage);

				TableCell tdTitle = new TableCell();
				//tdTitle.Text = BuildLink(msg.Key, msg.DataText4, msg.NewMessage);

				TableCell tdStatus = new TableCell();
				//tdDate.Text = BuildLink(msg.Key, msg.MsgDate.ToString(), msg.NewMessage); ;

				TableRow trOrg = new TableRow();
				trOrg.Cells.Add(tdLogo);
				trOrg.Cells.Add(tdTitle);
				trOrg.Cells.Add(tdStatus);

				tblOrg.Rows.Add(trOrg);
			}
		}

		protected void OnClickFindOrg(object sender, ImageClickEventArgs e)
		{
			Response.Redirect( "FindOrg.aspx", true );
		}

		protected void OnClickNewOrg(object sender, ImageClickEventArgs e)
		{
			Response.Redirect( "CreateOrg.aspx", true );
		}
	}
}