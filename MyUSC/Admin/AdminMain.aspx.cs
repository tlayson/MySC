using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MyUSC.Classes;

namespace MyUSC.Admin
{
	public partial class AdminMain : USCPageBase
	{
		protected AdminMain()
		{
			_PAGENAME = "AdminMain";
			_pgUSCMaster = (USCMaster)Master;
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			// Make sure the user should be here
			if( !IsUserAdmin() )
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
			UserAccount acct = Master.GetActiveUser();
			if( UserAccount.UserTypes.SuperUser == acct.UserType )
			{
				hlUserType.Enabled = true;
				hlUserType.Visible = true;
			}
			else
			{
				hlUserType.Enabled = false;
				hlUserType.Visible = false;
			}
		}
#endregion

	}
}