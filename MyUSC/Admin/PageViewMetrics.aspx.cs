using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MyUSC.Classes;

namespace MyUSC.Admin
{
	public partial class PageViewMetrics : USCPageBase
	{
		protected PageViewMetrics()
		{
			_PAGENAME = "PageViewMetrics";
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

			SqlParameter[] paramArray = new SqlParameter[1];
			SqlParameter sqlParam = new SqlParameter("@days", 1);
			paramArray[0] = sqlParam;

			string strSP = "sp_GetPageViewsLastXDays";
			int count = obj.ExecuteSPCount(strSP, paramArray);
			lblPV1Day.Text = count.ToString();

			paramArray[0].Value = 3;
			count = obj.ExecuteSPCount(strSP, paramArray);
			lblPV3Day.Text = count.ToString();

			paramArray[0].Value = 5;
			count = obj.ExecuteSPCount(strSP, paramArray);
			lblPV5Day.Text = count.ToString();

			paramArray[0].Value = 7;
			count = obj.ExecuteSPCount(strSP, paramArray);
			lblPV7Day.Text = count.ToString();

			paramArray[0].Value = 30;
			count = obj.ExecuteSPCount(strSP, paramArray);
			lblPV30Day.Text = count.ToString();

			strSP = "sp_GetUniqueUsersLastXDays";
			paramArray[0].Value = 1;
			count = obj.ExecuteSPCount(strSP, paramArray);
			lblUU1Day.Text = count.ToString();

			paramArray[0].Value = 3;
			count = obj.ExecuteSPCount(strSP, paramArray);
			lblUU3Day.Text = count.ToString();

			paramArray[0].Value = 5;
			count = obj.ExecuteSPCount(strSP, paramArray);
			lblUU5Day.Text = count.ToString();

			paramArray[0].Value = 7;
			count = obj.ExecuteSPCount(strSP, paramArray);
			lblUU7Day.Text = count.ToString();

			paramArray[0].Value = 30;
			count = obj.ExecuteSPCount(strSP, paramArray);
			lblUU30Day.Text = count.ToString();

			strSP = "sp_GetMTPageViewsLastXDays";
			paramArray[0].Value = 1;
			count = obj.ExecuteSPCount(strSP, paramArray);
			lblMTPV1Day.Text = count.ToString();

			paramArray[0].Value = 3;
			count = obj.ExecuteSPCount(strSP, paramArray);
			lblMTPV3Day.Text = count.ToString();

			paramArray[0].Value = 5;
			count = obj.ExecuteSPCount(strSP, paramArray);
			lblMTPV5Day.Text = count.ToString();

			paramArray[0].Value = 7;
			count = obj.ExecuteSPCount(strSP, paramArray);
			lblMTPV7Day.Text = count.ToString();

			paramArray[0].Value = 30;
			count = obj.ExecuteSPCount(strSP, paramArray);
			lblMTPV30Day.Text = count.ToString();
		}
#endregion

	}
}