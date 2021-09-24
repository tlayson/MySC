using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MyUSC.Classes;

namespace MyUSC.Admin
{
	public partial class UserStats : USCPageBase
	{
		protected UserStats()
		{
			_PAGENAME = "UserStats";
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
			USCBase obj = new USCBase();
			obj.ConnectionString = Master.g_strConnectionString;

			string strSP = "sp_GetTotalSiteUsers";
			int count = obj.ExecuteSPCount(strSP, null);
			lblTotalUsers.Text = count.ToString();

			strSP = "sp_GetUniqueSiteUsers";
			count = obj.ExecuteSPCount(strSP, null);
			lblUniqueUsers.Text = count.ToString();

			SqlParameter[] paramArray = new SqlParameter[1];
			SqlParameter sqlParam = new SqlParameter("@days", 30 );
			paramArray[0] = sqlParam;

			strSP = "sp_GetUsersLastXDays";
			count = obj.ExecuteSPCount(strSP, paramArray);
			lblActiveUsers30.Text = count.ToString();

			paramArray[0].Value = 90;
			strSP = "sp_GetUsersLastXDays";
			count = obj.ExecuteSPCount(strSP, paramArray);
			lblActiveUsers90.Text = count.ToString();

			paramArray[0].Value = 30;
			strSP = "sp_GetRegisteredLastXDays";
			count = obj.ExecuteSPCount(strSP, paramArray);
			lblRegistered30.Text = count.ToString();

			paramArray[0].Value = 90;
			strSP = "sp_GetRegisteredLastXDays";
			count = obj.ExecuteSPCount(strSP, paramArray);
			lblRegistered90.Text = count.ToString();

		}
#endregion

	}
}