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
	public partial class AddAffiliate : USCPageBase
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			string strOrgID = Request.QueryString["OrgID"];
			string strAFID = Request.QueryString["AFID"];
			string strAFType = Request.QueryString["AFType"];
			long lOrgID = 0;
			long lAFID = 0;
			string strURL = "~/MyTeams/Dashboard.aspx";

			if( strOrgID.Length > 0 && 
				strAFID.Length > 0 && 
				long.TryParse(strOrgID, out lOrgID) &&
				long.TryParse(strAFID, out lAFID)
				)
			{
				UserAccount acct = Master.GetActiveUser();
				if( UserHasEditAccess( acct.AccountID, lOrgID, OrgPageID.Manage ) )
				{
					SetSessionOrgID(lOrgID);
					SetSessionValue1( lAFID );
					if( !IsPostBack )
					{
						Organization org = Master.GetOrgByID(lOrgID);
						Organization orgAff = Master.GetOrgByID(lAFID);
						if (null == org || null == orgAff)
						{
							if (null == org)
							{
								string strError = "Error retreiving org ID = " + lOrgID + "while trying to add affiliate";
								EvtLog.WriteEvent(strError, EventLogEntryType.Error, (int)EventErrors.ErrorType.NullOrg, 0);
							}
							else
							{
								string strError = "Error retreiving affiliate org ID = " + lOrgID + "while trying to add affiliate - load";
								EvtLog.WriteEvent(strError, EventLogEntryType.Error, (int)EventErrors.ErrorType.NullAffiliate, 0);
							}
							Master.AlertUser("An error occured while loading organization information.");
							Response.Redirect(strURL);
						}
						else
						{
							//TODO : See if this is a new affiliate or not
							Affiliate aff = org.orgAffiliateList.GetAffiliate(lAFID);
							lblAffName.Text = orgAff.OrgName;
							txtAffNotes.Text = "";
							SetSessionValue2(1);
							ddlAffType.SelectedValue = "-1";

							if (null != aff)
							{
								txtAffNotes.Text = aff.Note;
								SetSessionValue2(0);
								ddlAffType.SelectedValue = aff.AffiliateType.ToString();
							}
						}
					}
				}
				else
				{
					Master.AlertUser("You do not have permission to view this page.");
					Response.Redirect( strURL );
				}
			}
			else
			{
				Master.AlertUser("No organization specified.");
				Response.Redirect( strURL );
			}
		}

		protected void OnClickAdd(object sender, ImageClickEventArgs e)
		{
			long lOrgID = GetSessionOrgID();
			string strURL = "~/MyTeams/ManageAffiliates.aspx?OrgID=" + lOrgID;
			string strAFID = Request.QueryString["AFID"];
			long lAFID = GetSessionValue1();
			if( 0 >= lAFID )
			{
				Master.AlertUser( "An error occured while trying to add the affiliate.  Please try again." );
				Response.Redirect( strURL );
			}

			bool fIsNew = (bool)(GetSessionValue2() == 1);
			Organization org = (Organization)Master.g_OrgList.htOrgList[lOrgID];
			Organization orgAff = (Organization)Master.g_OrgList.htOrgList[lAFID];

			string strAffType = ddlAffType.SelectedValue;
			int nAffType = -1;
			int.TryParse( strAffType, out nAffType );

			Affiliate aff = null;
			if( fIsNew )
			{
				aff = new Affiliate();
				aff.OrgID = lOrgID;
				aff.AffiliateID = lAFID;
				aff.AffiliateType = (AffiliateTypes)nAffType;
				aff.Note = txtAffNotes.Text;
				org.orgAffiliateList.Add( aff );
			}
			else
			{
				aff = org.orgAffiliateList.GetAffiliate( lAFID );
				if( null == aff )
				{
					string strError = "Error retreiving affiliate org ID = " + lOrgID + "while trying to add affiliate - add";
					EvtLog.WriteEvent( strError, EventLogEntryType.Error, (int)EventErrors.ErrorType.NullAffiliate, 1 );
					Master.AlertUser( "An error occured while trying to add the affiliate.  Please try again." );
				}
				else
				{
					aff.Note = txtAffNotes.Text;
					aff.AffiliateType = (AffiliateTypes)nAffType;
					UserAccount acct = Master.GetActiveUser();
					aff.Update(acct);
				}
			}
			Response.Redirect(strURL);
		}

		protected void OnClickCancel(object sender, ImageClickEventArgs e)
		{
			long lOrgID = GetSessionOrgID();
			string strURL = "~/MyTeams/ManageAffiliates.aspx?OrgID=" + lOrgID;
			Response.Redirect( strURL );
		}
	}
}