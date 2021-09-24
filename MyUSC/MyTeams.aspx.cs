using System;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.Security;
using System.Web.UI;
using System.Xml;
using System.IO;
using MyUSC.Classes;
using System.Web.SessionState;

namespace MyUSC
{
	public partial class MyTeamsTemp : USCPageBase
    {
		protected MyTeamsTemp()
		{
			_PAGENAME = "MyTeams";
			_pgUSCMaster = (USCMaster)Master;
		}

#region PageLoad
        protected void Page_Load(object sender, EventArgs e)
        {
#region IfNotIsPostBack
            if (!IsPostBack)
            {
                //here we check to see if the user is logged in
				//here we check to see if the user is logged in
				UserAccount acct = GetActiveUser();
				if (null == acct)
				{
					RedirectToLoginPage();
				}

            //    GetLabelText();
            //    GetButtonText();
            //    PopulateDropDownLists();
            //    SetAttributes();
            //    SetStandard();
            //    #region CheckForMyAccountEntry
            //    Int32 _LANGUAGE = Convert.ToInt32(Session["USC_UserLanguage"]);
            //    Boolean blnMyAccountSaved = Convert.ToBoolean(Session["USCAcct_Saved"]);
            //    if (Session["USCAcct_Saved"] != null && blnMyAccountSaved == false)
            //    {
            //        Session.Remove("USCAcct_Saved");
            //        var strMSG_MyAccountFinish = (from c in MyUSCDC.Localizations
            //                                      where c.Parent == "Site.Master" &&
            //                                      c.Language == _LANGUAGE &&
            //                                      c.ObjectName.Equals("MSG_MyAccountFinish")
            //                                      select c.Text).Single();
            //        AlertUser(strMSG_MyAccountFinish);
            //    }
            //    #endregion
            }
#endregion
			
			Master.SelectMenuItem(SelectedPage.Teams);
		}
#endregion
    }
}