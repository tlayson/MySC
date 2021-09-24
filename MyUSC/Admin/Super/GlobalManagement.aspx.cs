using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MyUSC.Classes;

namespace MyUSC.Admin.Super
{
	public partial class GlobalManagement : USCPageBase
	{
		protected GlobalManagement()
		{
			_PAGENAME = "GlobalManagement";
			_pgUSCMaster = (USCMaster)Master;
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			// Make sure the user should be here
			if (!IsSuperUser())
			{
				Master.Logout( true );
			}

			// One time settings
			if (!IsPostBack)
			{
				PopulateDropDownLists();

				SetInitialValues();
			}
			Master.EnableAdminMenu();
			Master.SelectMenuItem(SelectedPage.Admin);
		}

#region PopulateDropDownLists
		void PopulateDropDownLists()
		{
		}
#endregion

#region SetInitialValues
		private void SetInitialValues()
		{
		}
#endregion

#region Button Clicks
		protected void OnClickResetAll(object sender, EventArgs e)
		{
			if (IsSuperUser())
			{
				Master.ResetAll();
			}
		}

		protected void OnClickResetUsers(object sender, EventArgs e)
		{
			if (IsSuperUser())
			{
				Master.ResetUsers();
			}
		}

		protected void OnClickResetSports(object sender, EventArgs e)
		{
			if (IsSuperUser())
			{
				Master.ResetSports();
			}
		}

		protected void OnClickResetSiteSettings(object sender, EventArgs e)
		{
			if (IsSuperUser())
			{
				Master.ResetSiteSettings();
			}
		}

		protected void OnClickResetLocalization(object sender, EventArgs e)
		{
			if (IsSuperUser())
			{
				Master.ResetLocalization();
			}
		}

		protected void OnClickResetRSS(object sender, EventArgs e)
		{
			if (IsSuperUser())
			{
				Master.ResetRSS();
			}
		}
#endregion

	}
}