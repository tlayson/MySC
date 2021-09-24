using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MyUSC.Classes;

namespace MyUSC.Admin.Super
{
	public partial class ManageLocalization : USCPageBase
	{
		protected ManageLocalization()
		{
			_PAGENAME = "ManageLocalization";
			_pgUSCMaster = (USCMaster)Master;
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			// Make sure the user should be here
			if (!IsSuperUser())
			{
				Master.Logout(true);
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

	}
}