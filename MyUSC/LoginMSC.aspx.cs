using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;
using System.Text;
using MyUSC.Classes;
using System.IO;
using System.Web.SessionState;

namespace MyUSC
{
	public partial class LoginMSC : USCPageBase
    {
		protected LoginMSC()
		{
			_PAGENAME = "LoginMSC";
			_USERTYPE = "Pre-Login";
			_pgUSCMaster = (USCMaster)Master;
		}

#region SetVariables

#endregion

#region SetStandard
        private void SetStandard()
        {
			SetFocus(txtUserName);
        }
#endregion

#region PageLoad
		protected bool IsValidURL( string strURL )
		{
			bool fRet = true;
			return fRet;
		}

        protected void Page_Load(object sender, EventArgs e)
        {
			lblLoginError.Visible = false;
			// Session takes precendence
			string strSessionReturn = GetSessionReturnURL();
			if (null == strSessionReturn || !IsValidURL(strSessionReturn))
			{
				string strURLReturn = Request.QueryString["URL"];
				if (null != strURLReturn && IsValidURL(strURLReturn))
				{
					SetSessionReturnURL(strURLReturn);
				}
				else
				{
					SetSessionReturnURL("");
				}
			}

			SetFocus(txtUserName);
			if (!IsPostBack)
            {
                System.Random RandNum = new System.Random();
                Int32 intRandomNumber = RandNum.Next(5);

                if (intRandomNumber == 1)
                {
                    pnlMain.BackImageUrl = "/Images/soccer.png";
                }
                else if (intRandomNumber == 2)
                {
                    pnlMain.BackImageUrl = "/Images/basketball.png";
                }
                else if (intRandomNumber == 3)
                {
                    pnlMain.BackImageUrl = "/Images/baseball.png";
                }
                else if (intRandomNumber == 4)
                {
                    pnlMain.BackImageUrl = "/Images/football.png";
                }
                else
                {
					pnlMain.BackImageUrl = "/Images/baseball.png";
                }
                SetStandard();
                GetToolTips();

				_pgUSCMaster = (USCMaster)Master;
				if (!AutoLogin())
				{
					txtUserName.Text = GetCookieUserName();

					if (txtUserName.Text.Trim() == "")
					{
						SetFocus(txtUserName);
					}
					else
					{
						SetFocus(txtPassword);
					}
					
				}
			}
        }
#endregion

#region GetToolTips
        void GetToolTips()
        {
			// This Is The Page Where You Can Register, Login, Change Your Password And Change Your Security Questions And Answers
			txtUserName.ToolTip = "";
			txtPassword.ToolTip = "";
			btnLogin.ToolTip = "";
        }
#endregion

#region StronglyTypedPasswordCheck
		private bool StronglyTypedPasswordCheck(string strPassword)
        {
            //clsStronglyTypedPasswordCheck PC = new clsStronglyTypedPasswordCheck();
            //bool blnReturnPC;
            //try
            //{
            //    PC.strCheckPassword = txt4.Text.Trim();
            //    blnReturnPC = PC.ExecuteCheckPassword();

            //    if (blnReturnPC == false)
            //    {
            //        return false;
            //    }
            //}
            //catch
            //{
            //    return false;
            //}
            return true;
        }
#endregion

#region Forgotten Pswd
		protected void btnForgotPswdOnClick(object sender, ImageClickEventArgs e)
		{
			Response.Redirect( "~/MyAccount/ForgotPswd.aspx" );
		}
#endregion

#region Login
		protected void btnLoginOnClick(object sender, ImageClickEventArgs e)
		{
			string strUserName = txtUserName.Text;
			UserAccount userAcct = Master.g_AccountsList.GetAccountByUserName( strUserName );
			ClearSessionVariables();
			if (null == userAcct)
			{
				lblLoginError.Visible = true;
				SetFocus(txtUserName);
			}
			else
			{
				string strPassword = txtPassword.Text;
				string strUserPswd = userAcct.Password;

				USCEncrypt usce = new USCEncrypt();
				string strEncryptedPswd = usce.EncryptString(strPassword);
				if( strUserPswd == strEncryptedPswd )
				{
					userAcct.Preferences.KeepLoggedIn = chkKeepLoggedIn.Checked;
					ProcessLogin( userAcct );
				}
				else
				{
					// For debugging only
					string strDecryptedPswd = usce.DecryptString(strUserPswd);
					lblLoginError.Visible = true;
					lblLoginError.Text = "The username and password provided to not match our records.  Please try again.";
					SetFocus(txtPassword);
				}
			}
		}

#endregion

#region NewAccount
		protected void OnClickNewAccount(object sender, ImageClickEventArgs e)
		{
			DeleteSessionVariables();
			ExpireCookies();
			Response.Redirect("CreateAccount.aspx", true);
		}
#endregion
    }
}