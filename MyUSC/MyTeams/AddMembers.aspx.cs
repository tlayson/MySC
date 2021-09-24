using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MyUSC.Classes;
using MyUSC.Classes.MyOrg;

namespace MyUSC.MyTeams
{
	public partial class AddMembers : USCPageBase
	{

#region Column Constants
		int nCellBorder = 1;
		Color clrCellBorder = Color.White;
		BorderStyle bsCellBorder = BorderStyle.Dotted;
		int nRowBorder = 1;
		Color clrRowBorder = Color.White;
		BorderStyle bsRowBorder = BorderStyle.Dashed;
		int nTblBorder = 2;
		Color clrTblBorder = Color.FromArgb(16737792);
		BorderStyle bsTblBorder = BorderStyle.Solid;
		int td0Width = 50;
		int td1Width = 150;
		int td2Width = 250;
		int td3Width = 250;

		const string strBtnInvite = "btnInvite";
		const string strCBResult = "cbResult";
		const string strADDL = "addl";
        const int colKey = 0;
        const int colUserName = 1;
		const int colUserType = 2;
		const int colPassword = 3;
		const int colLanguage = 4;
		const int colIsActive = 5;
		const int colAcceptedTOU = 6;
		const int colTitle = 7;
        const int colFirst = 8;
        const int colMI = 9;
        const int colLast = 10;
		const int colNickname = 11;
        const int colSuffix = 12;
        const int colBirthDate = 13; 
        const int colAddress1 = 14;
        const int colAddress2 = 15;
        const int colCity = 16;
        const int colState = 17;
        const int colZip = 18;
		const int colCountry = 19;
		const int colEmail = 20;
		const int colEmailValid = 21;
		const int colDefaultPage = 22;
		const int colPhotoFile = 23;
		const int colSecurityQuestion = 24;
		const int colSecurityAnswer = 25;
		const int colLoginAttempts = 26;
		const int colLastLogin = 27;
		const int colCreatorID = 28;
		const int colCreator = 29;
		const int colCreateDate = 30;
		const int colLastUpdate = 31;
#endregion

		public SortedDictionary<long, object> m_sdResults = new SortedDictionary<long, object>();

		protected void Page_Load(object sender, EventArgs e)
		{
			Master.PageHeading = "Invite Members to your organization";

			Table tbl = tblResults;
			string strOrgID = Request.QueryString["OrgID"];
			long lOrgID = 0;
			if (null != strOrgID && long.TryParse(strOrgID, out lOrgID))
			{
				Master.SetMenuState(lOrgID, false);

				UserAccount acct = Master.GetActiveUser();
				if( UserHasViewAccess( acct.AccountID, lOrgID, OrgPageID.Manage ) )
				{
					SetSessionOrgID(lOrgID);
					Organization org = (Organization)Master.g_OrgList.htOrgList[lOrgID];
					if (!IsPostBack && null != org)
					{
						Master.PageHeading = "Invite members - " + org.OrgName;
						EnablePageOptions( acct.AccountID, lOrgID );
					}
					EnablePageOptions( acct.AccountID, lOrgID );
				}
				else
				{
					Master.AlertUser("You do not have permission to view this page.");
					Response.Redirect("~/MyTeams/Dashboard.aspx");
				}

				string strURL = "~/MyTeams/InviteMember.aspx?OrgID=" + lOrgID;
				btnInviteNew.PostBackUrl = strURL;
			}
			else
			{
				Master.AlertUser("No organization specified.");
				Response.Redirect("~/MyTeams/Dashboard.aspx");
			}

			if( !IsPostBack )
			{
				pnlResults.Visible = false;
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

		private string BuildQuery()
		{
			StringBuilder sbSQLQuery = new StringBuilder("");
			string strFirst = txtFirstName.Text;
			string strLast = txtLastName.Text;
			string strCity = txtCity.Text;
			string strZip = txtPostalCode.Text;
			string strEMail = txtEmail.Text;
			string strState = ddlState.SelectedValue;

			//Make sure we have at least one field
			if( strFirst.Length > 0 ||
				strLast.Length > 0 ||
				strCity.Length > 0 ||
				strZip.Length > 0 ||
				strEMail.Length > 0 ||
				strState.Length > 0
			)
			{
				bool fMulti = false;
				string strField = "";
				sbSQLQuery.Append("SELECT * FROM Accounts WHERE ");
				if( strFirst.Length > 0 )
				{
					strField = "FirstName LIKE '%";
					strField += strFirst;
					strField += "%'";
					sbSQLQuery.Append(strField);
					fMulti = true;
				}

				if( strLast.Length > 0 )
				{
					if( fMulti )
					{
					sbSQLQuery.Append( " AND " );
					}
					strField = "LastName LIKE '%";
					strField += strLast;
					strField += "%'";
					sbSQLQuery.Append(strField);
					fMulti = true;
				}

				if( strCity.Length > 0 )
				{
					if( fMulti )
					{
					sbSQLQuery.Append( " AND " );
					}
					strField = "City LIKE '%";
					strField += strCity;
					strField += "%'";
					sbSQLQuery.Append(strField);
					fMulti = true;
				}

				if( strZip.Length > 0 )
				{
					if( fMulti )
					{
					sbSQLQuery.Append( " AND " );
					}
					strField = "Zipcode LIKE '%";
					strField += strZip;
					strField += "%'";
					sbSQLQuery.Append(strField);
					fMulti = true;
				}

				if( strEMail.Length > 0 )
				{
					if( fMulti )
					{
					sbSQLQuery.Append( " AND " );
					}
					strField = "EmailAddress LIKE '%";
					strField += strEMail;
					strField += "%'";
					sbSQLQuery.Append(strField);
					fMulti = true;
				}

				if( strState.Length > 0 )
				{
					if( fMulti )
					{
					sbSQLQuery.Append( " AND " );
					}
					strField = "State LIKE '%";
					strField += strState;
					strField += "%'";
					sbSQLQuery.Append(strField);
				}
			}

			return sbSQLQuery.ToString();
		}

		private bool RunFind()
		{
			bool fRet = false;

			m_sdResults.Clear();

			string strSQL = BuildQuery();

			SqlConnection sqlConn = null;
			DataSet locStrDS = new DataSet();

			try
			{
				sqlConn = new SqlConnection( Master.g_strConnectionString );
				sqlConn.Open();
				SqlDataAdapter daLocStrings = new SqlDataAdapter(strSQL, sqlConn);
				daLocStrings.Fill(locStrDS, "Accounts");
			}
			catch (Exception ex)
			{
				EvtLog.WriteException("Invite.RunFind failure", ex, 0);
				return false;
			}
			finally
			{
				sqlConn.Close();
			}

			try
			{
				DataRowCollection dra = locStrDS.Tables["Accounts"].Rows;
				foreach (DataRow dr in dra)
				{
					UserAccount acct = new UserAccount();
					fRet = ReadAccount(dr, acct);
					if (fRet)
					{
						// Set now so objects can update themselves
						acct.ConnectionString = Master.g_strConnectionString;

						m_sdResults.Add( acct.Key, acct );
					}
				}
			}
			catch( Exception ex )
			{
				EvtLog.WriteException("Invite.RunFind (enum) failure", ex, 0);
				return false;
			}
			fRet = true;

			return fRet;
		}

        private bool ReadAccount(DataRow dr, UserAccount acct)
        {
            bool fRet = true;
            try
            {
                acct.Key = ObjectToLong( dr.ItemArray[colKey] );
				acct.UserName = ObjectToString( dr.ItemArray[colUserName]);
				acct.UserType = (UserAccount.UserTypes)ObjectToInt( dr.ItemArray[colUserType] );
				acct.Password = ObjectToString( dr.ItemArray[colPassword]);
				acct.Language = ObjectToInt( dr.ItemArray[colLanguage]);
				acct.IsActive = ObjectToBool(dr.ItemArray[colIsActive]);
				acct.AcceptedTOU = ObjectToBool(dr.ItemArray[colAcceptedTOU] );
				acct.Title = ObjectToString( dr.ItemArray[colTitle] );
				acct.First = ObjectToString( dr.ItemArray[colFirst] );
				acct.MI = ObjectToString( dr.ItemArray[colMI] );
				acct.Last = ObjectToString( dr.ItemArray[colLast] );
				acct.NickName = ObjectToString(dr.ItemArray[colNickname]);
				acct.Suffix = ObjectToString(dr.ItemArray[colSuffix]);
				acct.BirthDate = ObjectToString( dr.ItemArray[colBirthDate] );
				acct.Address1 = ObjectToString( dr.ItemArray[colAddress1] );
				acct.Address2 = ObjectToString( dr.ItemArray[colAddress2] );
				acct.City = ObjectToString( dr.ItemArray[colCity] );
				acct.State = ObjectToString( dr.ItemArray[colState] );
				acct.Zip = ObjectToString( dr.ItemArray[colZip] );
				acct.Country = ObjectToString( dr.ItemArray[colCountry] );
				acct.Email = ObjectToString( dr.ItemArray[colEmail] );
				acct.EmailValid = ObjectToBool ( dr.ItemArray[colEmailValid] );
				acct.DefaultPage = ObjectToString( dr.ItemArray[colDefaultPage] );
				acct.PhotoFile = ObjectToString( dr.ItemArray[colPhotoFile] );
				acct.SecurityQuestion = ObjectToString( dr.ItemArray[colSecurityQuestion] );
				acct.SecurityAnswer = ObjectToString( dr.ItemArray[colSecurityAnswer] );
				acct.LoginAttempts = ObjectToInt( dr.ItemArray[colLoginAttempts] );
				acct.LastLogin = ObjectToDateTime(dr.ItemArray[colLastLogin]);
				acct.CreatorID = ObjectToLong(dr.ItemArray[colCreatorID]);
				acct.Creator = ObjectToString(dr.ItemArray[colCreator]);
				acct.CreateDate = ObjectToDateTime( dr.ItemArray[colCreateDate] );
				acct.LastUpdate = ObjectToString( dr.ItemArray[colLastUpdate] );
            }
            catch (Exception ex)
            {
				EvtLog.WriteException("Accounts.ReadAccount failure", ex, 0);
				fRet = false;
            }
            return fRet;
        }

		protected void DrawResult( UserAccount acct, int nCount )
		{
			TableRow tr = new TableRow();
			tr.BorderWidth = nRowBorder;
			tr.BorderColor = clrRowBorder;
			tr.BorderStyle = bsRowBorder;

			TableCell tdImg = new TableCell();
			tdImg.Width = td0Width;
			tdImg.CssClass = "tdImage";
			tdImg.BorderWidth = nCellBorder;
			tdImg.BorderColor = clrCellBorder;
			tdImg.BorderStyle = bsCellBorder;

			TableCell tdName = new TableCell();
			tdName.Width = td1Width;
			tdName.CssClass = "tdInput";
			tdName.BorderWidth = nCellBorder;
			tdName.BorderColor = clrCellBorder;
			tdName.BorderStyle = bsCellBorder;

			TableCell tdAccess = new TableCell();
			tdAccess.Width = td2Width;
			tdAccess.CssClass = "tdInput";
			tdAccess.BorderWidth = nCellBorder;
			tdAccess.BorderColor = clrCellBorder;
			tdAccess.BorderStyle = bsCellBorder;

			TableCell tdInvite = new TableCell();
			tdInvite.Width = td3Width;
			tdInvite.CssClass = "tdInput";
			tdInvite.BorderWidth = nCellBorder;
			tdInvite.BorderColor = clrCellBorder;
			tdInvite.BorderStyle = bsCellBorder;


			tr.Height = 35;

			// Col 0
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

			// Col 1
			Label lblDisplayName = new Label();
			lblDisplayName.Text = acct.DisplayName();
			tdName.Controls.Add( lblDisplayName );

			// Col 2
//			TBSCAccessDropDown addl = new TBSCAccessDropDown();
//			addl.ID = strADDL + nCount;
//			tdAccess.Controls.Add( addl );

			// Col 3
			TBSCImageButton btnInvite = new TBSCImageButton();
			long orgID = GetSessionOrgID();
			string strURL = "~/MyTeams/InviteMember.aspx?OrgID=" + orgID + "&IID=" + acct.AccountID;
			btnInvite.PostBackUrl = strURL;
			btnInvite.ImageUrl = "~/Images/Button/btnInvite.png";
			btnInvite.UserValue = acct.AccountID;
			tdInvite.Controls.Add( btnInvite );

			tr.Cells.Add( tdImg );
			tr.Cells.Add( tdName );
			tr.Cells.Add( tdAccess );
			tr.Cells.Add( tdInvite );

			TableRow trSep = new TableRow();
			TableCell tdSep = new TableCell();
			tdSep.ColumnSpan = 4;

			Literal literal = new Literal();
			literal.Text = "<hr/>";
			tdSep.Controls.Add( literal );
			trSep.Controls.Add( tdSep );

			tblResults.Rows.Add( tr );
			tblResults.Rows.Add( trSep );

			tblResults.BorderWidth = nTblBorder;
			tblResults.BorderColor = clrTblBorder;
			tblResults.BorderStyle = bsTblBorder;
		}

		protected void OnClickFind(object sender, ImageClickEventArgs e)
		{
			// TODO :: Don't add existing members to list
			if( RunFind() )
			{
				pnlResults.Visible = true;
				tblResults.Controls.Clear();
				lblResultMessage.Text = "Select the people below that you want to add and click Invite.  If you didn't find who you were looking for, you can invite them to join the site via eMail.";
				lblResultMessage.Visible = true;
				SetSessionValue1( m_sdResults.Count );
				if( m_sdResults.Count == 0 )
				{
					lblResultMessage.Visible = true;
					lblResultMessage.Text  = "No results were found that match your criteria.  Please try again or send them an invitation by email.";
				}
				else if( m_sdResults.Count > 50 )
				{
					lblResultMessage.Visible = true;
					lblResultMessage.Text  = "Too many results found.  Please refine your search information.";
				}
				else
				{
					int nCount = 1;
					foreach( KeyValuePair<long, object> kvp in m_sdResults )
					{
						UserAccount acct = (UserAccount)kvp.Value;
						if (null != acct)
						{
							DrawResult( acct, nCount++ );
						}
					}
				}
			}
		}

		protected void OnClickCancel(object sender, ImageClickEventArgs e)
		{
			long orgID = GetSessionOrgID();
			string strURL = "~/MyTeams/OrgRoster.aspx?OrgID=" + orgID;
			Response.Redirect( strURL );
		}

/*
		protected void OnClickInviteSelected(object sender, ImageClickEventArgs e)
		{
			long orgID = GetSessionOrgID();
			Organization org = GetOrgByID( orgID );
			if( null != org )
			{
				long resultCount = GetSessionValue();
				int nIndex = 1;
				try
				{
					while( nIndex <= resultCount )
					{
						string cbName = strCBResult + nIndex;
						TBSCCheckBox cb = (TBSCCheckBox)FindControl( cbName );
						if( cb.Checked )
						{
							long lAcctID = cb.UserValue;
							string ddlName = strADDL + nIndex;
							TBSCAccessDropDown addl = (TBSCAccessDropDown)FindControl( ddlName );

							// TODO: This needs to be changed to ask if they want to join.
							OrgMember om = new OrgMember();
							om.OrgID = orgID;
							om.UserID = lAcctID;
							om.MemberType = addl.GetSelectedType();
							om.Creator = org.Creator;
							org.orgMemberList.Add( om );

							// Add to the owners account
							UserAccount acct = GetUserByID( lAcctID );
							acct.m_sdMyOrgs.Add( om.Key, om );
						}
						nIndex++;
					}
				}
				catch( Exception ex )
				{
					EvtLog.WriteException("Account:ReadOrgMember.Read failure", ex, 0);
				}
			}
		}

		protected void OnClickInvite(object sender, ImageClickEventArgs e)
		{
			TBSCImageButton btn = (TBSCImageButton)sender;
			if( null != btn )
			{
				long lAcctID = btn.UserValue;
			}
		}
*/

		protected void OnClickInviteNew(object sender, EventArgs e)
		{
			long orgID = GetSessionOrgID();
			string strURL = "~/MyTeams/InviteMember.aspx?OrgID=" + orgID;
			Response.Redirect( strURL );
		}
	}
}