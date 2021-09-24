using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MyUSC.DataAccess;
using MyUSC.Classes;

namespace MyUSC.Account
{
    public partial class Login : System.Web.UI.Page
    {
        #region SetVariables
        private const string _PAGENAME = "Login";
        //private MyUSCDataContext MyUSCDC;
        #endregion
        #region PageLoad
        protected void Page_Load(object sender, EventArgs e)
        {
            //RegisterHyperLink.NavigateUrl = "Register.aspx?ReturnUrl=" + HttpUtility.UrlEncode(Request.QueryString["ReturnUrl"]); 
            Master.SetMaster_lbl2("");

            if (!IsPostBack)
            {
                if (!User.Identity.IsAuthenticated)
                {
                    //here we set focus to the User Name textbox...
                    TextBox tb = (TextBox)LoginUser.FindControl("UserName");
                    SetFocus(tb);
                }
            }
        }
        #endregion
        #region AlertUser
        public void AlertUser(string strMessage)
        {
            strMessage = USCBase.ReplaceString(strMessage, "'", "`");
            string strAlertUser = null;
            strAlertUser = "<script type=" + "\"text/javascript\">";
            strAlertUser = strAlertUser + "alert('" + strMessage + "');";
            strAlertUser = strAlertUser + "</script>";
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "AlertUser", strAlertUser, false);
        }
        #endregion
    }
}
