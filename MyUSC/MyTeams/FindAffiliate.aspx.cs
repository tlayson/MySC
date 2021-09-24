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
	public partial class FindAffiliate : USCPageBase
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
		const int colOrgName = 1;
		const int colOrgDescription = 2;
		const int colOrgType = 3;
		const int colOwnerID = 4;
		const int colLanguage = 5;
		const int colAddress1 = 6;
		const int colAddress2 = 7;
		const int colCity = 8;
		const int colState = 9;
		const int colZip = 10;
		const int colCountry = 11;
		const int colEmailAddress = 12;
		const int colPhone = 13;
		const int colCell = 14;
		const int colFax = 15;
		const int colURL = 16;
		const int colLogoURL = 17;
		const int colShowContact = 18;
		const int colAllowMemberRequests = 19;
		const int colAllowFollowerRequests = 20;
		const int colAllowGuestViews = 21;
		const int colDeleted = 22;
		const int colCreator = 23;
		const int colCreateDate = 24;
		const int colLastUpdate = 25;
#endregion

		public SortedDictionary<long, object> m_sdResults = new SortedDictionary<long, object>();

		protected void Page_Load(object sender, EventArgs e)
		{
			Master.PageHeading = "Add affiliates to your organization";

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
						Master.PageHeading = "Add affiliates - " + org.OrgName;
						ddlState.LoadStates();
					}
					EnablePageOptions( acct.AccountID, lOrgID );
				}
				else
				{
					Master.AlertUser("You do not have permission to view this page.");
					Response.Redirect("~/MyTeams/Dashboard.aspx");
				}

				string strURL = "~/MyTeams/FindAffiliate.aspx?OrgID=" + lOrgID;
				btnCreateNew.PostBackUrl = strURL;
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
			string strOrgName = txtOrgName.Text;
			string strState = ddlState.SelectedValue;
			string strOrgType = ddlOrgType.SelectedValue;

			

			//Make sure we have at least one field
			if( strOrgName.Length > 0 ||
				strOrgType != "-1" ||
				strState.Length > 0
			)
			{
				bool fMulti = false;
				string strField = "";
				sbSQLQuery.Append("SELECT * FROM MyOrg WHERE ");
				if( strOrgName.Length > 0 )
				{
					strField = "OrgName LIKE '%";
					strField += strOrgName;
					strField += "%'";
					sbSQLQuery.Append(strField);
					fMulti = true;
				}


				if( strState.Length > 0 && strState != "---" )
				{
					if( fMulti )
					{
					sbSQLQuery.Append( " AND " );
					}
					strField = "State LIKE '%";
					strField += strState;
					strField += "%'";
					sbSQLQuery.Append(strField);
					fMulti = true;
				}

				if( strOrgType != "-1" )
				{
					if( fMulti )
					{
					sbSQLQuery.Append( " AND " );
					}
					strField = "OrgType = ";
					strField += strOrgType;
					sbSQLQuery.Append(strField);
					fMulti = true;
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
				EvtLog.WriteException("FindAffiliate.RunFind failure", ex, 0);
				return false;
			}
			finally
			{
				sqlConn.Close();
			}

			try
			{
				long orgID = GetSessionOrgID();
				DataRowCollection dra = locStrDS.Tables["Accounts"].Rows;
				Organization org = Master.GetOrgByID( orgID );
				foreach (DataRow dr in dra)
				{
					Organization affOrg = new Organization();
					fRet = ReadOrg(dr, affOrg);

					if( fRet && orgID != affOrg.OrgID )
					{
						// Make sure its not already an affiliate
						Affiliate aff = org.orgAffiliateList.GetAffiliate( affOrg.OrgID );
						if( null == aff )
						{
							// Set now so objects can update themselves
							affOrg.ConnectionString = Master.g_strConnectionString;

							m_sdResults.Add( affOrg.Key, affOrg );
						}
					}
				}
			}
			catch( Exception ex )
			{
				EvtLog.WriteException("FindAffiliate.RunFind (enum) failure", ex, 0);
				return false;
			}
			fRet = true;

			return fRet;
		}

		private bool ReadOrg(DataRow dr, Organization org)
		{
			bool fRet = true;
			try
			{
				org.Key = ObjectToLong(dr.ItemArray[colKey]);
				org.OrgName = ObjectToString(dr.ItemArray[colOrgName]);
				org.OrgDescription = ObjectToString(dr.ItemArray[colOrgDescription]);
				org.OrgType = (OrgTypes)ObjectToInt(dr.ItemArray[colOrgType]);
				org.OwnerAccountID = ObjectToLong(dr.ItemArray[colOwnerID]);
				org.Language = ObjectToInt(dr.ItemArray[colLanguage]);
				org.Address1 = ObjectToString(dr.ItemArray[colAddress1]);
				org.Address2 = ObjectToString(dr.ItemArray[colAddress2]);
				org.City = ObjectToString(dr.ItemArray[colCity]);
				org.State = ObjectToString(dr.ItemArray[colState]);
				org.Zip = ObjectToString(dr.ItemArray[colZip]);
				org.Country = ObjectToString(dr.ItemArray[colCountry]);
				org.Email = ObjectToString(dr.ItemArray[colEmailAddress]);
				org.Phone = ObjectToString(dr.ItemArray[colPhone]);
				org.Cell = ObjectToString(dr.ItemArray[colCell]);
				org.Fax = ObjectToString(dr.ItemArray[colFax]);
				org.URL = ObjectToString(dr.ItemArray[colURL]);
				org.LogoURL = ObjectToString(dr.ItemArray[colLogoURL]);
				org.ShowContactInfo = ObjectToBool(dr.ItemArray[colShowContact]);
				org.AllowMemberRequests = ObjectToBool(dr.ItemArray[colAllowMemberRequests]);
				org.AllowFollowerRequests = ObjectToBool(dr.ItemArray[colAllowFollowerRequests]);
				org.AllowGuestViews = ObjectToBool(dr.ItemArray[colAllowGuestViews]);
				org.Deleted = ObjectToBool(dr.ItemArray[colDeleted]);
				org.Creator = ObjectToString(dr.ItemArray[colCreator]);
				org.CreateDate = ObjectToDateTime(dr.ItemArray[colCreateDate]);
				org.LastUpdate = ObjectToString(dr.ItemArray[colLastUpdate]);
			}
			catch (Exception ex)
			{
				EvtLog.WriteException("FindAffiliate.ReadOrg failure", ex, 0);
				fRet = false;
			}
			return fRet;
		}

		protected void DrawResult( Organization organization, int nCount )
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
			if( organization.LogoURL.Length > 0 )
			{
				string strImageURL = "~/OrgFolders/" + organization.OrgID + "/Logo/" + organization.LogoURL;
				imgBtn.ImageUrl = strImageURL;
			}
			tdImg.Controls.Add( imgBtn );

			// Col 1
			Label lblDisplayName = new Label();
			lblDisplayName.Text = organization.OrgName;
			tdName.Controls.Add( lblDisplayName );

			// Col 2
//			TBSCAccessDropDown addl = new TBSCAccessDropDown();
//			addl.ID = strADDL + nCount;
//			tdAccess.Controls.Add( addl );

			// Col 3
			TBSCImageButton btnInvite = new TBSCImageButton();
			long orgID = GetSessionOrgID();
			string strURL = "~/MyTeams/AddAffiliate.aspx?OrgID=" + orgID + "&AFID=" + organization.OrgID;
			btnInvite.PostBackUrl = strURL;
			btnInvite.ImageUrl = "~/Images/Button/btnAdd.png";
			btnInvite.UserValue = organization.OrgID;
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
						Organization org = (Organization)kvp.Value;
						if (null != org)
						{
							DrawResult( org, nCount++ );
						}
					}
				}
			}
		}

		protected void OnClickCancel(object sender, ImageClickEventArgs e)
		{
			long orgID = GetSessionOrgID();
			string strURL = "~/MyTeams/ManageAffiliates.aspx?OrgID=" + orgID;
			Response.Redirect( strURL );
		}

		protected void OnClickCreateNew(object sender, EventArgs e)
		{

		}

	}
}