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

namespace MyUSC.Classes
{
	public partial class VenueResultsTable : System.Web.UI.UserControl
	{
		protected void Page_Load(object sender, EventArgs e)
		{

		}

		public Table GetResultsTable()
		{
			return tblSearchResults;
		}

		public void ClearTable()
		{
			tblSearchResults.Controls.Clear();
		}

		public void OnClickResults(object sender, EventArgs e)
		{
			TBSCButton btn = (TBSCButton)sender;
			// Add it to the orgs list
		
			Organization org = (Organization)btn.UserData;
			OrgVenuePairingList ovpl = org.orgVenueList;

			OrgVenuePairing ovp = new OrgVenuePairing();
			ovp.VenueID = btn.UserValue1;
			ovp.OrgID = org.OrgID;

			ovpl.AddVenueUsage( ovp, (UserAccount)btn.UserAcct );

			btn.Text = "Venue Added";
			btn.Enabled = false;
		}
	}
}