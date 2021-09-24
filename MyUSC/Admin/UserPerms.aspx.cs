using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MyUSC.Classes;

namespace MyUSC.Admin
{
	public partial class UserPerms : USCPageBase
	{
		protected UserPerms()
		{
			_PAGENAME = "UserPerms";
			_pgUSCMaster = (USCMaster)Master;
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			// Make sure the user should be here
			if (!IsUserAdmin())
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

#region Button Clicks
		protected void OnClickFindUser(object sender, EventArgs e)
		{
			string strUsername = txtUsername.Text;
			if( strUsername.Length > 3 )
			{
				UserAccount acct = Master.g_AccountsList.GetAccountByUserName(strUsername);
				if( null != acct )
				{
					lblUserNotFound.Visible = false;
					ddlUserType.SelectedValue = ((int)acct.UserType).ToString();
				}
				else
				{
					lblUserNotFound.Visible = true;
				}
			}
		}

		protected void OnClickUpdateType(object sender, EventArgs e)
		{
			string strUsername = txtUsername.Text;
			if (strUsername.Length > 3)
			{
				UserAccount acct = Master.g_AccountsList.GetAccountByUserName(strUsername);
				if (null != acct)
				{
					lblUserNotFound.Visible = false;
					acct.UserType = (UserAccount.UserTypes)Convert.ToInt32( ddlUserType.SelectedValue );
					acct.UpdateUserType();
				}
				else
				{
					lblUserNotFound.Visible = true;
				}
			}
		}

		protected void OnClickBack(object sender, EventArgs e)
		{
			Response.Redirect("/Admin/AdminMain.aspx");
		}
#endregion
	}
}