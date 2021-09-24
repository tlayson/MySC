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
	public partial class FindVenue : USCPageBase
	{
		const string strQuery = "SELECT TOP 50 * FROM [dbo].[Venue] WHERE (MakePublic=1 OR OrgID={0})";

		protected void Page_Load(object sender, EventArgs e)
		{
			UserAccount acct = Master.GetActiveUser();
			if (null == acct)
			{
				SetSessionReturnURL("~/MyTeams/Dashboard.aspx");
				RedirectToLoginPage();
			}

			string strOrgID = Request.QueryString["OrgID"];
			if (!IsPostBack)
			{
				ddlVenueType.LoadVenueTypes();
				ddlVenueState.LoadStates();
				SetSessionQuery( "" );

				long lOrgID = 0;
				if (null != strOrgID && long.TryParse(strOrgID, out lOrgID))
				{
					Master.SetMenuState(lOrgID, false);

					if (UserHasViewAccess(acct.AccountID, lOrgID, OrgPageID.Manage))
					{
						SetSessionOrgID(lOrgID);
						Organization org = (Organization)Master.g_OrgList.htOrgList[lOrgID];
						if (!IsPostBack && null != org)
						{
							Master.PageHeading = "Venue List - " + org.OrgName;
							EnablePageOptions(acct.AccountID, lOrgID);
						}
					}
					else
					{
						Master.AlertUser("You do not have permission to view this page.");
						Response.Redirect("~/MyTeams/Dashboard.aspx");
					}
				}
				else
				{
					Master.AlertUser("No organization specified.");
					Response.Redirect("~/MyTeams/Dashboard.aspx");
				}
				Master.SelectMTMenuItemOnPageLoad(MyTeamsPage.Venues);
			}
			else
			{
				string strQuery = GetSessionQuery();
				if( strQuery.Length > 0 )
				{
					// TODO: Break this up and rebuild querry for safety
					RunQuery( strQuery );
				}
			}
		}

		protected void EnablePageOptions( long acctID, long orgID )
		{
			if( !UserHasMemberAccess( acctID, orgID, OrgPageID.Manage ) )
			{
			}

			if( !UserHasEditAccess( acctID, orgID, OrgPageID.Manage ) )
			{
				btnNewVenue.Visible = true;
			}

			if( UserHasAdminAccess( acctID, orgID, OrgPageID.Manage ) )
			{

			}
		}

		protected void OnClickNewVenue(object sender, EventArgs e)
		{
			SetSessionQuery( "" );
			long orgID = GetSessionOrgID();
			string strURL = "~/MyTeams/VenueDetails.aspx?VID=0&OrgID=";
			strURL += orgID;
			Response.Redirect( strURL );
		}

		protected void RunQuery( string strQuery )
		{
			vrt1.ClearTable();
			long lOrgID = GetSessionOrgID();
			Organization org = (Organization)Master.g_OrgList.htOrgList[lOrgID];
			OrgVenuePairingList ovpl = org.orgVenueList;
			int nDisplayCount = 0;

			SqlConnection sqlConn = null;
			DataSet locStrDS = new DataSet();

			try
			{
				sqlConn = new SqlConnection( Master.g_strConnectionString );
				sqlConn.Open();
				SqlDataAdapter daLocStrings = new SqlDataAdapter( strQuery, sqlConn );
				daLocStrings.Fill(locStrDS, "Venue");
			}
			catch (Exception ex)
			{
				EvtLog.WriteException("FindVenue.OnClickSearch failure", ex, 0);
			}
			finally
			{
				sqlConn.Close();
			}

			try
			{
				DataRowCollection dra = locStrDS.Tables["Venue"].Rows;
				if( dra.Count > 0 )
				{
					foreach (DataRow dr in dra)
					{
						Venue venue = new Venue();
						venue.ConnectionString = Master.g_strConnectionString;
						if (ReadVenue(dr, venue))
						{
							// Check to see if the venue is already in the teams list
							if (!ovpl.DoesPairingExist(lOrgID, venue.VenueID))
							{
								AddRow(venue);
								nDisplayCount++;
								if (nDisplayCount >= 20)
								{
									break;
								}
							}
						}
					}
				}
				else
				{
					NoResults();
				}
			}
			catch( Exception ex )
			{
				EvtLog.WriteException("FindVenue.OnClickSearch (enum) failure", ex, 0);
			}

			pnlResults.Visible = true;
		}

		protected void OnClickSearch(object sender, EventArgs e)
		{
			string strName = txtVenueName.Text;
			string strCity = txtVenueCity.Text;
			string strState = ddlVenueState.SelectedValue;
			string strCountry = ddlVenueCountry.SelectedValue;
			string strType = ddlVenueType.SelectedValue;
			long orgID = GetSessionOrgID();

			string strBaseQuery = String.Format( strQuery, orgID );

			StringBuilder sb = new StringBuilder();
			// Make sure it's not currently in the orgs list
			sb.Append( strBaseQuery );

			if( strName.Length > 0 )
			{
				sb.Append( " AND VenueName LIKE '%" ).Append( strName ).Append("%' ");
			}

			if( strCity.Length > 0 )
			{
				sb.Append( " AND City LIKE '%" ).Append( strCity ).Append("%' ");
			}

			if( strState.Length > 0 && strState != "---" )
			{
				sb.Append( " AND State LIKE '%" ).Append( strState ).Append("%' ");
			}

			if( strCountry.Length > 0 )
			{
				sb.Append( " AND Country LIKE '%" ).Append( strCountry ).Append("%' ");
			}

			if (strType.Length > 0)
			{
				sb.Append(" AND VenueType LIKE '%").Append(strType).Append("%' ");
			}

			SetSessionQuery( sb.ToString() );

			RunQuery( sb.ToString() );
		}

        private bool ReadVenue( DataRow dr, Venue venue )
		{
#region Column Constants
			const int colKey = 0;
			const int colVenueName = 1;
			const int colOwnerID = 2;
			const int colOrgID = 3;
			const int colVenueType = 4;
			const int colDisplayLocation = 5;
			const int colAddress1 = 6;
			const int colAddress2 = 7;
			const int colCity = 8;
			const int colState = 9;
			const int colPostalCode = 10;
			const int colCountry = 11;
			const int colPhone = 12;
			const int colWebsite = 13;
			const int colMapURL = 14;
			const int colImageURL = 15;
			const int colNote = 16;
			const int colPublicVenue = 17;
			const int colDeleted = 18;
			const int colCreator = 19;
			const int colCreateDate = 20;
			const int colLastUpdate = 21;
#endregion
            bool fRet = true;
            try
            {
                venue.Key = ObjectToLong( dr.ItemArray[colKey] );
				venue.VenueID = venue.Key;
				venue.VenueName = ObjectToString( dr.ItemArray[colVenueName]);
                venue.OwnerID = ObjectToLong( dr.ItemArray[colOwnerID] );
                venue.OrgID = ObjectToLong( dr.ItemArray[colOrgID] );
				venue.VenueType = ObjectToString(dr.ItemArray[colVenueType]);
				venue.DisplayLocation = ObjectToString( dr.ItemArray[colDisplayLocation]);
				venue.Address1 = ObjectToString( dr.ItemArray[colAddress1] );
				venue.Address2 = ObjectToString( dr.ItemArray[colAddress2] );
				venue.City = ObjectToString( dr.ItemArray[colCity] );
				venue.State = ObjectToString( dr.ItemArray[colState] );
				venue.Zip = ObjectToString( dr.ItemArray[colPostalCode] );
				venue.Country = ObjectToString( dr.ItemArray[colCountry] );
				venue.Phone = ObjectToString( dr.ItemArray[colPhone] );
				venue.URL = ObjectToString( dr.ItemArray[colWebsite] );
				venue.MapURL = ObjectToString( dr.ItemArray[colMapURL] );
				venue.ImageURL = ObjectToString( dr.ItemArray[colImageURL] );
				venue.Note = ObjectToString( dr.ItemArray[colNote] );
				venue.PublicVenue = ObjectToBool( dr.ItemArray[colPublicVenue] );
				venue.Deleted = ObjectToBool( dr.ItemArray[colDeleted] );
				venue.Creator = ObjectToString( dr.ItemArray[colCreator] );
				venue.CreateDate = ObjectToDateTime( dr.ItemArray[colCreateDate] );
				venue.LastUpdate = ObjectToString( dr.ItemArray[colLastUpdate] );
            }
            catch (Exception ex)
            {
				EvtLog.WriteException("FindVeue.ReadVenue failure", ex, 0);
				fRet = false;
            }
            return fRet;
		}

		protected void NoResults()
		{
			Table tbl = vrt1.GetResultsTable();
			tbl.CssClass = "tblNormal";
			TableRow tr = new TableRow();
			tr.CssClass = "trNormal";

			TableCell td = new TableCell();
			td.CssClass = "tdInput";
			Label lbl = new Label();
			lbl.CssClass = "medSiteColorTxt";
			lbl.Text = "No results found.";
			td.Controls.Add(lbl);
			tr.Controls.Add(td);

			tbl.Controls.Add(tr);
		}

		protected void AddRow( Venue venue )
		{
			long lOrgID = GetSessionOrgID();
			Organization org = (Organization)Master.g_OrgList.htOrgList[lOrgID];
			UserAccount acct = GetActiveUser();

			Table tbl = vrt1.GetResultsTable();
			tbl.CssClass = "tblNormal";
			TableRow tr = new TableRow();
			tr.CssClass = "trNormal";

			TableCell td = new TableCell();
			td.CssClass = "tdInput";
			Label lbl = new Label();
			lbl.CssClass = "medSiteColorTxt";
			lbl.Text = venue.VenueName;
			td.Controls.Add(lbl);
			tr.Controls.Add(td);

			td = new TableCell();
			td.CssClass = "tdInput";
			lbl = new Label();
			lbl.CssClass = "smSiteColorTxt";
			lbl.Text = venue.City;
			td.Controls.Add(lbl);
			tr.Controls.Add(td);

			td = new TableCell();
			td.CssClass = "tdInput";
			lbl = new Label();
			lbl.CssClass = "smSiteColorTxt";
			lbl.Text = venue.State;
			td.Controls.Add(lbl);
			tr.Controls.Add(td);

			td = new TableCell();
			td.CssClass = "tdInput";
			TBSCButton btn = new TBSCButton();
			btn.Text = "Add Venue";
			btn.ID = "btnVenue" + venue.VenueID.ToString();
			btn.Click += new EventHandler(vrt1.OnClickResults);
			btn.UserData = org;
			btn.UserValue1 = venue.VenueID;
			btn.UserAcct = acct;
			td.Controls.Add(btn);
			tr.Controls.Add(td);

			tbl.Controls.Add(tr);
		}

		protected void OnClickDone(object sender, EventArgs e)
		{
			long lOrgID = GetSessionOrgID();
			string strURL = "~/MyTeams/VenueList.aspx?OrgID=" + lOrgID.ToString();
			Response.Redirect(strURL);
		}

	}
}