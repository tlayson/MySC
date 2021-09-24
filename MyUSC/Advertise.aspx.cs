using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Configuration;
using System.Diagnostics;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Web.SessionState;
using MyUSC.Classes;

namespace MyUSC
{
	public partial class Advertise : USCPageBase
    {
#region SetVariables
		public AdRequestList g_adList;
		private string m_strCnx = "";
#endregion

#region Localization constants
		//English localization string indexes
		const int langENUS = 1;
		const long enusIdxAdvertiseLblAdvertise = 1007;    
		const long enusIdxAdvertiseLblCompanyName = 1008;
		const long enusIdxAdvertiselblCompanyAddress = 1009;
		const long enusIdxAdvertiseLblCity = 1010;
		const long enusIdxAdvertiseLblState = 1011;
		const long enusIdxAdvertiseLblZipcode = 1012;
		const long enusIdxAdvertiseLblContactFirst = 1013;
		const long enusIdxAdvertiseLblContactLast = 1014;
		const long enusIdxAdvertiseLblWorkPhone = 1015;
		const long enusIdxAdvertiseLblCellPhone = 1016;
		const long enusIdxAdvertiseLblEmail = 1017;
		const long enusIdxAdvertiseLblCompanyWebsite = 1018;
		const long enusIdxAdvertiseLblLocAdv = 1019;
		const long enusIdxAdvertiseLblNatAd = 1020;
		const long enusIdxAdvertiseTTTxtCompanyName = 1021;
		const long enusIdxAdvertiseTTtxtCompanyAddress = 1022;
		const long enusIdxAdvertiseTTtxtCompanyCity = 1023;
		const long enusIdxAdvertiseTTDdlState = 1024;
		const long enusIdxAdvertiseTTtxtCompanyZip = 1025;
		const long enusIdxAdvertiseTTTxtContactFirst = 1026;
		const long enusIdxAdvertiseTTTxtContactLast = 1027;
		const long enusIdxAdvertiseTTTxtWorkPhone = 1028;
		const long enusIdxAdvertiseTTTxtCellPhone = 1029;
		const long enusIdxAdvertiseTTTxtEmail = 1030;
		const long enusIdxAdvertiseTTTxtCompanyWebsite = 1031;
		const long enusIdxAdvertiseTTChkLocAd = 1032;
		const long enusIdxAdvertiseTTChkNatAd = 1033;
		const long enusIdxAdvertiseTTBtnSubmit = 1034;
		const long enusIdxAdvertiseTTBtnCancel = 1035;
		const long enusIdxAdvertiseBtnSubmit = 1036;
		const long enusIdxAdvertiseBtnCancel = 1037;
		const long enusIdxAdvertiseChkLocAd = 1038;
		const long enusIdxAdvertiseChkNatAd = 1039;
		const long enusIdxAdvertiseMFTxtCompanyName = 1040;
		const long enusIdxAdvertiseMFMTxtCompanyName = 1041;
		const long enusIdxAdvertiseMFtxtCompanyAddress = 1042;
		const long enusIdxAdvertiseMFMtxtCompanyAddress = 1043;
		const long enusIdxAdvertiseMFtxtCompanyCity = 1044;
		const long enusIdxAdvertiseMFMtxtCompanyCity = 1045;
		const long enusIdxAdvertiseMFDdlState = 1046;
		const long enusIdxAdvertiseMFMDdlState = 1047;
		const long enusIdxAdvertiseMFtxtCompanyZip = 1048;
		const long enusIdxAdvertiseMFMtxtCompanyZip = 1049;
		const long enusIdxAdvertiseMFTxtContactFirst = 1050;
		const long enusIdxAdvertiseMFMTxtContactFirst = 1051;
		const long enusIdxAdvertiseMFTxtContactLast = 1052;
		const long enusIdxAdvertiseMFMTxtContactLast = 1053;
		const long enusIdxAdvertiseMFTxtWorkPhone = 1054;
		const long enusIdxAdvertiseMFMTxtWorkPhone = 1055;
		const long enusIdxAdvertiseMFTxtCellPhone = 1056;
		const long enusIdxAdvertiseMFMTxtCellPhone = 1057;
		const long enusIdxAdvertiseMFTxtEmail = 1058;
		const long enusIdxAdvertiseMFMTxtEmail = 1059;
		const long enusIdxAdvertiseMFTxtCompanyWebiste = 1060;
		const long enusIdxAdvertiseMFMTxtCompanyWebsite = 1061;
		const long enusIdxAdvertiseMFChkAdvertising = 1062;
		const long enusIdxAdvertiseMFMChkAdvertising = 1063;
		const long enusIdxAdvertiselbl0 = 1064;
		const long enusIdxAdvertiseMFMtxtCompanyZipValidation = 1065;
		const long enusIdxAdvertiseMFMTxtEmailA = 1066;
		const long enusIdxAdvertiseMFMTxtCompanyWebsiteValidation = 1067;
		const long enusIdxAdvertiseMFMTxtWorkPhoneA = 1068;
		const long enusIdxAdvertiseMFMTxtCellPhoneA = 1069;
		const long enusIdxAdvertiseMSG0 = 1070;
		const long enusIdxAdvertiseMSG1 = 1071;
		const long enusIdxAdvertiseBtnLogin = 1072;
		const long enusIdxAdvertiseTTBtnLogin = 1073;
		const long enusIdxAdvertiseMSG2 = 1074;
		const long enusIdxAdvertiseLblComments = 1206;
		const long enusIdxAdvertiseTTLblComments = 1207;
		const long enusIdxAdvertiseMSG3 = 1261;
#endregion

        #region PageLoad
        protected void Page_Load(object sender, EventArgs e)
        {
			//Global localization strings
			g_adList = AdRequestList.Instance;

			m_strCnx = Master.g_strConnectionString;
			if(null == m_strCnx || "" == m_strCnx )
			{
				// Get the Web application configuration object.
				Configuration config = WebConfigurationManager.OpenWebConfiguration("/MyUSC");
				// Get the conectionStrings section.
				ConnectionStringsSection csSection = config.ConnectionStrings;
				// Get the actual string
				m_strCnx = csSection.ConnectionStrings[0].ConnectionString;
			}

			g_adList.Init(m_strCnx, false);

            if (!IsPostBack)
            {
                //here we check to see if the user is logged in

				if (GetCookieUserID() == 0)
                {
                    //Session.Abandon();
                    //SessionIDManager managera = new SessionIDManager();
                    //string newIDa = managera.CreateSessionID(Context);
                    //bool redirecteda = false;
                    //bool isAddeda = false;
                    //managera.SaveSessionID(Context, newIDa, out redirecteda, out isAddeda);
					//Response.Redirect("LoginMSC.aspx");
                }

                GetLabelText();
                GetButtonText();
                GetCheckBoxText();
                SetAttributes();
                GetToolTips();
                PopulateDropDownLists();
                SetStandard();
            }//end if !postback
        }
        #endregion
        #region GetLabelText
        void GetLabelText()
        {
/*
			lbl0.Text = GetLocalizedString(enusIdxAdvertiselbl0, _PAGENAME, "lbl_0");
			lblAdvertise.Text = GetLocalizedString(enusIdxAdvertiseLblAdvertise, _PAGENAME, "lbl_Advertise");
			lblCompanyName.Text = GetLocalizedString(enusIdxAdvertiseLblCompanyName, _PAGENAME, "lbl_CompanyName");
			lblCompanyAddress.Text = GetLocalizedString(enusIdxAdvertiselblCompanyAddress, _PAGENAME, "lbl_Address");
			lblCity.Text = GetLocalizedString(enusIdxAdvertiseLblCity, _PAGENAME, "lbl_City");
			lblState.Text = GetLocalizedString(enusIdxAdvertiseLblState, _PAGENAME, "lbl_State");
			lblZipcode.Text = GetLocalizedString(enusIdxAdvertiseLblZipcode, _PAGENAME, "lbl_Zipcode");
			lblContactFirstName.Text = GetLocalizedString(enusIdxAdvertiseLblContactFirst, _PAGENAME, "lbl_ContactFirstName");
			lblContactLastName.Text = GetLocalizedString(enusIdxAdvertiseLblContactLast, _PAGENAME, "lbl_ContactLastName");
			lblWorkPhone.Text = GetLocalizedString(enusIdxAdvertiseLblWorkPhone, _PAGENAME, "lbl_WorkPhone");
			lblCellPhone.Text = GetLocalizedString(enusIdxAdvertiseLblCellPhone, _PAGENAME, "lbl_CellPhone");
			lblEmail.Text = GetLocalizedString(enusIdxAdvertiseLblEmail, _PAGENAME, "lbl_Email");
			lblCompanyWebsite.Text = GetLocalizedString(enusIdxAdvertiseLblCompanyWebsite, _PAGENAME, "lbl_CompanyWebsite");
			lblLocalAdvertising.Text = GetLocalizedString(enusIdxAdvertiseLblLocAdv, _PAGENAME, "lbl_LocalAdvertising");
			lblNationalAdvertising.Text = GetLocalizedString(enusIdxAdvertiseLblNatAd, _PAGENAME, "lbl_NationalAdvertising");
			lblComments.Text = GetLocalizedString(enusIdxAdvertiseLblComments, _PAGENAME, "lbl_Comments");
*/
        }
        #endregion
        #region GetButtonText
        void GetButtonText()
        {
/*
			btnSubmit.Text = GetLocalizedString(enusIdxAdvertiseBtnSubmit, _PAGENAME, "btn_Submit");
			btnCancel.Text = GetLocalizedString(enusIdxAdvertiseBtnCancel, _PAGENAME, "btn_Cancel");
			btnLogin.Text = GetLocalizedString(enusIdxAdvertiseBtnLogin, _PAGENAME, "btn_Login");
*/
		}
        #endregion
        #region GetCheckBoxText
        void GetCheckBoxText()
        {
			//chkNationalAdvertising.Text = GetLocalizedString(enusIdxAdvertiseChkNatAd, _PAGENAME, "chk_NationalAdvertising");
			//chkLocalAdvertising.Text = GetLocalizedString(enusIdxAdvertiseChkLocAd, _PAGENAME, "chk_LocalAdvertising");
		}
        #endregion
        #region SetAttributes
        public void SetAttributes()
        {
/*
            txtCompanyName.Attributes.Add("onfocus", "this.style.backgroundColor='#666666';");
            txtCompanyName.Attributes.Add("onBlur", "this.style.backgroundColor='Transparent';");
            txtCompanyName.Attributes.Add("onkeydown", "return (event.keyCode!=13);");
            txtCompanyAddress.Attributes.Add("onfocus", "this.style.backgroundColor='#666666';");
            txtCompanyAddress.Attributes.Add("onBlur", "this.style.backgroundColor='Transparent';");
            txtCompanyAddress.Attributes.Add("onkeydown", "return (event.keyCode!=13);");
            txtCompanyCity.Attributes.Add("onfocus", "this.style.backgroundColor='#666666';");
            txtCompanyCity.Attributes.Add("onBlur", "this.style.backgroundColor='Transparent';");
            txtCompanyCity.Attributes.Add("onkeydown", "return (event.keyCode!=13);");
            ddlState.Attributes.Add("onfocus", "this.style.backgroundColor='#666666';");
            ddlState.Attributes.Add("onBlur", "this.style.backgroundColor='Transparent';");
            txtCompanyZip.Attributes.Add("onfocus", "this.style.backgroundColor='#666666';");
            txtCompanyZip.Attributes.Add("onBlur", "this.style.backgroundColor='Transparent';");
            txtCompanyZip.Attributes.Add("onkeydown", "return (event.keyCode!=13);");
            txtContactFirstName.Attributes.Add("onfocus", "this.style.backgroundColor='#666666';");
            txtContactFirstName.Attributes.Add("onBlur", "this.style.backgroundColor='Transparent';");
            txtContactFirstName.Attributes.Add("onkeydown", "return (event.keyCode!=13);");
            txtContactLastName.Attributes.Add("onfocus", "this.style.backgroundColor='#666666';");
            txtContactLastName.Attributes.Add("onBlur", "this.style.backgroundColor='Transparent';");
            txtContactLastName.Attributes.Add("onkeydown", "return (event.keyCode!=13);");
            txtWorkPhone.Attributes.Add("onfocus", "this.style.backgroundColor='#666666';");
            txtWorkPhone.Attributes.Add("onBlur", "this.style.backgroundColor='Transparent';");
            txtWorkPhone.Attributes.Add("onkeydown", "return (event.keyCode!=13);");
            txtCellPhone.Attributes.Add("onfocus", "this.style.backgroundColor='#666666';");
            txtCellPhone.Attributes.Add("onBlur", "this.style.backgroundColor='Transparent';");
            txtCellPhone.Attributes.Add("onkeydown", "return (event.keyCode!=13);");
            txtEmail.Attributes.Add("onfocus", "this.style.backgroundColor='#666666';");
            txtEmail.Attributes.Add("onBlur", "this.style.backgroundColor='Transparent';");
            txtEmail.Attributes.Add("onkeydown", "return (event.keyCode!=13);");
            txtCompanyWebsite.Attributes.Add("onfocus", "this.style.backgroundColor='#666666';");
            txtCompanyWebsite.Attributes.Add("onBlur", "this.style.backgroundColor='Transparent';");
            txtCompanyWebsite.Attributes.Add("onkeydown", "return (event.keyCode!=13);");
            txtComments.Attributes.Add("onfocus", "this.style.backgroundColor='#666666';");
            txtComments.Attributes.Add("onBlur", "this.style.backgroundColor='Transparent';");
            txtComments.Attributes.Add("onkeydown", "return (event.keyCode!=13);");
*/
        }
        #endregion
        #region GetToolTips
        void GetToolTips()
        {
			txtCompanyName.ToolTip = GetLocalizedString(enusIdxAdvertiseTTTxtCompanyName, _PAGENAME, "TT_txtCompanyName");
			txtCompanyAddress.ToolTip = GetLocalizedString(enusIdxAdvertiseTTtxtCompanyAddress, _PAGENAME, "TT_txtCompanyAddress");
			txtCompanyCity.ToolTip = GetLocalizedString(enusIdxAdvertiseTTtxtCompanyCity, _PAGENAME, "TT_txtCompanyCity");
			ddlState.ToolTip = GetLocalizedString(enusIdxAdvertiseTTDdlState, _PAGENAME, "TT_ddlState");
			txtCompanyZip.ToolTip = GetLocalizedString(enusIdxAdvertiseTTtxtCompanyZip, _PAGENAME, "TT_txtCompanyZip");
			txtContactFirstName.ToolTip = GetLocalizedString(enusIdxAdvertiseTTTxtContactFirst, _PAGENAME, "TT_txtContactFirstName");
			txtContactLastName.ToolTip = GetLocalizedString(enusIdxAdvertiseTTTxtContactLast, _PAGENAME, "TT_txtContactLastName");
			txtWorkPhone.ToolTip = GetLocalizedString(enusIdxAdvertiseTTTxtWorkPhone, _PAGENAME, "TT_txtWorkPhone");
			txtCellPhone.ToolTip = GetLocalizedString(enusIdxAdvertiseTTTxtCellPhone, _PAGENAME, "TT_txtCellPhone");
			txtEmail.ToolTip = GetLocalizedString(enusIdxAdvertiseTTTxtEmail, _PAGENAME, "TT_txtEmail");
			txtCompanyWebsite.ToolTip = GetLocalizedString(enusIdxAdvertiseTTTxtCompanyWebsite, _PAGENAME, "TT_txtCompanyWebsite");
			lblLocalAdvertising.ToolTip = GetLocalizedString(enusIdxAdvertiseTTChkLocAd, _PAGENAME, "TT_chkLocalAdvertising");
			lblNationalAdvertising.ToolTip = GetLocalizedString(enusIdxAdvertiseTTChkNatAd, _PAGENAME, "TT_chkNationalAdvertising");
			btnSubmit.ToolTip = GetLocalizedString(enusIdxAdvertiseTTBtnSubmit, _PAGENAME, "TT_btnSubmit");
			btnCancel.ToolTip = GetLocalizedString(enusIdxAdvertiseTTBtnCancel, _PAGENAME, "TT_btnCancel");
			btnLogin.ToolTip = GetLocalizedString(enusIdxAdvertiseTTBtnLogin, _PAGENAME, "TT_btnLogin");
			txtComments.ToolTip = GetLocalizedString(enusIdxAdvertiseTTLblComments, _PAGENAME, "TT_lblComments");
		}
        #endregion
        #region PopulateDropDownLists
        void PopulateDropDownLists()
        {
            //populate ddls *********************************************************
            //populate ddlState *****************************************************
			var lstState = Master.GetSiteSetting(SiteAdmin.saKeyStates, "StateNames");
            List<string> lstStates = new List<string>();
            lstStates = lstState.Trim().Split(',').ToList();
            ddlState.Items.Clear();

            for (int intCounter = 0; intCounter < lstStates.Count; intCounter++)
            {
                ddlState.Items.Add(lstStates[intCounter].Trim());
            }
        }
        #endregion
        #region SetStandard
		void SetCompanyNameDefaults()
		{
			lblCompanyName.Text = "Company Name *";
			lblCompanyName.BackColor = System.Drawing.Color.Transparent;
			lblCompanyName.ForeColor = System.Drawing.Color.Black;
			txtCompanyName.BackColor = System.Drawing.Color.White;
		}

		void SetCompanyAddressDefaults()
		{
			lblCompanyAddress.Text = "Address";
			lblCompanyAddress.BackColor = System.Drawing.Color.Transparent;
			lblCompanyAddress.ForeColor = System.Drawing.Color.Black;
			txtCompanyAddress.BackColor = System.Drawing.Color.White;
		}

		void SetCompanyCityDefaults()
		{
			lblCity.Text = "City";
			lblCity.BackColor = System.Drawing.Color.Transparent;
			lblCity.ForeColor = System.Drawing.Color.Black;
			txtCompanyCity.BackColor = System.Drawing.Color.White;
		}

		void SetCompanyStateDefaults()
		{
			lblState.Text = "State";
			lblState.BackColor = System.Drawing.Color.Transparent;
			lblState.ForeColor = System.Drawing.Color.Black;
			ddlState.BackColor = System.Drawing.Color.White;
		}

		void SetCompanyZipDefaults()
		{
			lblZipcode.Text = "Postal Code";
			lblZipcode.BackColor = System.Drawing.Color.Transparent;
			lblZipcode.ForeColor = System.Drawing.Color.Black;
			txtCompanyZip.BackColor = System.Drawing.Color.White;
		}

		void SetCompanyWebsiteDefaults()
		{
			lblCompanyWebsite.Text = "Company Website";
			lblCompanyWebsite.BackColor = System.Drawing.Color.Transparent;
			lblCompanyWebsite.ForeColor = System.Drawing.Color.Black;
			txtCompanyWebsite.BackColor = System.Drawing.Color.White;
		}

		void SetContactFirstNameDefaults()
		{
			lblContactFirstName.Text = "Contact First Name *";
			lblContactFirstName.BackColor = System.Drawing.Color.Transparent;
			lblContactFirstName.ForeColor = System.Drawing.Color.Black;
			txtContactFirstName.BackColor = System.Drawing.Color.White;
		}

		void SetContactLastNameDefaults()
		{
			lblContactLastName.Text = "Contact Last Name *";
			lblContactLastName.BackColor = System.Drawing.Color.Transparent;
			lblContactLastName.ForeColor = System.Drawing.Color.Black;
			txtContactLastName.BackColor = System.Drawing.Color.White;
		}

		void SetEmailDefaults()
		{
			lblEmail.Text = "Email Address *";
			lblEmail.BackColor = System.Drawing.Color.Transparent;
			lblEmail.ForeColor = System.Drawing.Color.Black;
			txtEmail.BackColor = System.Drawing.Color.White;
		}

		void SetWorkPhoneDefaults()
		{
			lblWorkPhone.Text = "Work Phone";
			lblWorkPhone.BackColor = System.Drawing.Color.Transparent;
			lblWorkPhone.ForeColor = System.Drawing.Color.Black;
			txtWorkPhone.BackColor = System.Drawing.Color.White;
		}

		void SetCellPhoneDefaults()
		{
			lblCellPhone.Text = "Cell Phone";
			lblCellPhone.BackColor = System.Drawing.Color.Transparent;
			lblCellPhone.ForeColor = System.Drawing.Color.Black;
			txtCellPhone.BackColor = System.Drawing.Color.White;
		}

		void SetCommentsDefaults()
		{
			lblComments.Text = "Comments";
			lblComments.BackColor = System.Drawing.Color.Transparent;
			lblComments.ForeColor = System.Drawing.Color.Black;
			txtComments.BackColor = System.Drawing.Color.White;
		}

		void SetStandard()
        {
			SetCompanyNameDefaults();
			SetCompanyAddressDefaults();
			SetCompanyCityDefaults();
			SetCompanyStateDefaults();
			SetCompanyZipDefaults();
			SetCompanyWebsiteDefaults();
			SetContactFirstNameDefaults();
			SetContactLastNameDefaults();
			SetEmailDefaults();
			SetWorkPhoneDefaults();
			SetCellPhoneDefaults();
			SetCommentsDefaults();

			txtCompanyName.Text = "";
			txtCompanyAddress.Text = "";
			txtCompanyCity.Text = "";
			ddlState.SelectedIndex = 0;
			txtCompanyZip.Text = "";
			txtContactFirstName.Text = "";
			txtContactLastName.Text = "";
			txtWorkPhone.Text = "";
			txtCellPhone.Text = "";
			txtEmail.Text = "";
			txtCompanyWebsite.Text = "";
			txtComments.Text = "";
			chkLocalAdvertising.Checked = false;
			chkNationalAdvertising.Checked = false;

			SetFocus(txtCompanyName);
        }
        #endregion
        #region ddl0SelectedIndexChanged
        protected void ddl0_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        #endregion
        #region btnCancelClick
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            SetStandard();
            GetLabelText();
        }
        #endregion
        #region txtCompanyZipTextChanged
        protected void txtCompanyZip_TextChanged(object sender, EventArgs e)
        {
/*
            // here we fill in the city and state from the entered zipcode
            if (txtCompanyZip.Text.Trim() == "")
            {
				string strText = GetLocalizedString(enusIdxAdvertiseMSG2, _PAGENAME, "MSG_2");
				AlertUser(strText);
                SetFocus(txtCompanyZip);
                return;
            }
            else
            {
                try
                {
                    Boolean blnSetCityState;
                    blnSetCityState = GetCityStateFromZip(txtCompanyZip.Text.Trim());

                    if (blnSetCityState == false)
                    {
						string strText = GetLocalizedString(enusIdxAdvertiseMFMtxtCompanyZipValidation, _PAGENAME, "MFM_txtCompanyZipValidation");
                        AlertUser(strText);
                        SetFocus(txtCompanyZip);
                        return;
                    }
                }
                catch
                {
					string strText = GetLocalizedString(enusIdxAdvertiseMFMtxtCompanyZipValidation, _PAGENAME, "MFM_txtCompanyZipValidation");
                    AlertUser(strText);
                    SetFocus(txtCompanyZip);
                    return;
                }
            }
            SetFocus(txtContactFirstName);
*/
        }
        #endregion        
        #region ValidateEntry
        Boolean ValidateEntry()
        {
            //******************************************************
            bool blnFoundErrors = false;
            #region ValidateCompanyName
            //validate txtCompanyName **********************************************************
            if (txtCompanyName.BackColor == System.Drawing.Color.Red)
            {
				SetCompanyNameDefaults();
            }

            if (txtCompanyName.Text.Trim() == "")
			{
				var strMF_txtCompanyName = GetLocalizedString(enusIdxAdvertiseMFTxtCompanyName, _PAGENAME, "MF_txtCompanyName");
				if (strMF_txtCompanyName == "True")
				{
					var strMFM_txtCompanyName = GetLocalizedString(enusIdxAdvertiseMFMTxtCompanyName, _PAGENAME, "MFM_txtCompanyName");
					lblCompanyName.Text = strMFM_txtCompanyName;
					lblCompanyName.ForeColor = System.Drawing.Color.Red;
					txtCompanyName.BackColor = System.Drawing.Color.Red;
					blnFoundErrors = true;
				}
			}


            //*********************************************************************************
            #endregion

            #region ValidateAddress
            //validate txtCompanyAddress ****************************************************************
            if (txtCompanyAddress.BackColor == System.Drawing.Color.Red)
            {
				SetCompanyAddressDefaults();
            }

			if (txtCompanyAddress.Text.Trim() == "")
			{
				var strMF_txtCompanyAddress = GetLocalizedString(enusIdxAdvertiseMFtxtCompanyAddress, _PAGENAME, "MF_txtCompanyAddress");
				if (strMF_txtCompanyAddress == "True")
				{
					var strMFM_txtCompanyAddress = GetLocalizedString(enusIdxAdvertiseMFMtxtCompanyAddress, _PAGENAME, "MFM_txtCompanyAddress");
					lblCompanyAddress.Text = strMFM_txtCompanyAddress;
					lblCompanyAddress.ForeColor = System.Drawing.Color.Red;
					txtCompanyAddress.BackColor = System.Drawing.Color.Red;
					blnFoundErrors = true;
				}
			}
				
            //********************************************************************************
            #endregion

            #region ValidateCity
            //validate txtCompanyCity *******************************************************************

            if (txtCompanyCity.BackColor == System.Drawing.Color.Red)
            {
				SetCompanyCityDefaults();
            }

            if (txtCompanyCity.Text.Trim() == "")
			{
				var strMF_txtCompanyCity = GetLocalizedString(enusIdxAdvertiseMFtxtCompanyCity, _PAGENAME, "MF_txtCompanyCity");

				if (strMF_txtCompanyCity == "True")
				{
					var strMFM_txtCompanyCity = GetLocalizedString(enusIdxAdvertiseMFMtxtCompanyCity, _PAGENAME, "MFM_txtCompanyCity");
					lblCity.Text = strMFM_txtCompanyCity;
					lblCity.ForeColor = System.Drawing.Color.Red;
					txtCompanyCity.BackColor = System.Drawing.Color.Red;
					blnFoundErrors = true;
				}
			}

            //************************************************************************
            #endregion

            #region ValidateState
            //validate ddlState *********************************************************
            if (ddlState.BackColor == System.Drawing.Color.Red)
            {
				SetCompanyStateDefaults();
            }

            if (ddlState.SelectedIndex == 0)
			{
				var strMF_ddlState = GetLocalizedString(enusIdxAdvertiseMFDdlState, _PAGENAME, "MF_ddlState");

				if (strMF_ddlState == "True")
				{
					var strMFM_ddlState = GetLocalizedString(enusIdxAdvertiseMFMDdlState, _PAGENAME, "MFM_ddlState");
					lblState.Text = strMFM_ddlState;
					lblState.ForeColor = System.Drawing.Color.Red;
					ddlState.BackColor = System.Drawing.Color.Red;
					blnFoundErrors = true;
				}
			}

            //*********************************************************************************
            #endregion

            #region ValidateZipcode
            //validate txtCompanyZip *****************************************************************
            if (txtCompanyZip.BackColor == System.Drawing.Color.Red)
            {
				SetCompanyZipDefaults();
            }

            if (txtCompanyZip.Text.Trim() == "")
			{
				var strMF_txtCompanyZip = GetLocalizedString(enusIdxAdvertiseMFtxtCompanyZip, _PAGENAME, "MF_txtCompanyZip");

				if (strMF_txtCompanyZip == "True")
				{
					var strMFM_txtCompanyZip = GetLocalizedString(enusIdxAdvertiseMFMtxtCompanyZip, _PAGENAME, "MFM_txtCompanyZip");
					lblZipcode.Text = strMFM_txtCompanyZip;
					lblZipcode.ForeColor = System.Drawing.Color.Red;
					txtCompanyZip.BackColor = System.Drawing.Color.Red;
					blnFoundErrors = true;
				}
			}

            //*********************************************************************************
            #endregion

            #region ValidateContactFirstName
            //validate txtcontactfirstname *********************************************************
            if (txtContactFirstName.BackColor == System.Drawing.Color.Red)
            {
				SetContactFirstNameDefaults();
            }

            if (txtContactFirstName.Text.Trim() == "")
			{
				var strMF_txtContactFirstName = GetLocalizedString(enusIdxAdvertiseMFTxtContactFirst, _PAGENAME, "MF_txtContactFirstName");

				if (strMF_txtContactFirstName == "True")
				{
					var strMFM_txtContactFirstName = GetLocalizedString(enusIdxAdvertiseMFMTxtContactFirst, _PAGENAME, "MFM_txtContactFirstName");
					lblContactFirstName.Text = strMFM_txtContactFirstName;
					lblContactFirstName.ForeColor = System.Drawing.Color.Red;
					txtContactFirstName.BackColor = System.Drawing.Color.Red;
					blnFoundErrors = true;
				}
			}

            //*********************************************************************************
            #endregion

            #region ValidateContactLastName
            //validate txtcontactlastname *********************************************************
            if (txtContactLastName.BackColor == System.Drawing.Color.Red)
            {
				SetContactLastNameDefaults();
            }

            if (txtContactLastName.Text.Trim() == "")
			{
				var strMF_txtContactLastName = GetLocalizedString(enusIdxAdvertiseMFTxtContactLast, _PAGENAME, "MF_txtContactLastName");

				if (strMF_txtContactLastName == "True")
				{
					var strMFM_txtContactLastName = GetLocalizedString(enusIdxAdvertiseMFMTxtContactLast, _PAGENAME, "MFM_txtContactLastName");
					lblContactLastName.Text = strMFM_txtContactLastName;
					lblContactLastName.ForeColor = System.Drawing.Color.Red;
					txtContactLastName.BackColor = System.Drawing.Color.Red;
					blnFoundErrors = true;
				}
			}

            //*********************************************************************************
            #endregion

            #region ValidateWorkPhone
            //validate txtworkphone ********************************************************
            if (txtWorkPhone.BackColor == System.Drawing.Color.Red)
            {
				SetWorkPhoneDefaults();
            }

            if (txtWorkPhone.Text.Trim() == "")
			{
				var strMF_txtWorkPhone = GetLocalizedString(enusIdxAdvertiseMFTxtWorkPhone, _PAGENAME, "MF_txtWorkPhone");
	            if (strMF_txtWorkPhone == "True")
				{
					var strMFM_txtWorkPhone = GetLocalizedString(enusIdxAdvertiseMFMTxtWorkPhone, _PAGENAME, "MFM_txtWorkPhone");
					lblWorkPhone.Text = strMFM_txtWorkPhone;
					lblWorkPhone.ForeColor = System.Drawing.Color.Red;
					txtWorkPhone.BackColor = System.Drawing.Color.Red;
					blnFoundErrors = true;
				}
			}

            //*********************************************************************************
            #endregion

            #region ValidateCellPhone
            //validate txtcellphone ***************************************************************
            if (txtCellPhone.BackColor == System.Drawing.Color.Red)
            {
				SetCellPhoneDefaults();
            }

			if (txtCellPhone.Text.Trim() == "")
			{
				var strMF_txtCellPhone = GetLocalizedString(enusIdxAdvertiseMFMTxtCellPhone, _PAGENAME, "MFM_txtCellPhone");
				if (strMF_txtCellPhone == "True")
				{
					var strMFM_txtCellPhone = GetLocalizedString(enusIdxAdvertiseMFMTxtCellPhone, _PAGENAME, "MFM_txtCellPhone");
					lblCellPhone.Text = strMFM_txtCellPhone;
					lblCellPhone.ForeColor = System.Drawing.Color.Red;
					txtCellPhone.BackColor = System.Drawing.Color.Red;
					blnFoundErrors = true;
				}
			}
            //*********************************************************************************
            #endregion

            #region ValidateEmail
            //validate txtemail ******************************************************************
            if (txtEmail.BackColor == System.Drawing.Color.Red)
            {
				SetEmailDefaults();
            }

			if (txtEmail.Text.Trim() == "")
			{
				var strMF_txtEmail = GetLocalizedString(enusIdxAdvertiseMFTxtEmail, _PAGENAME, "MF_txtEmail");
				if (strMF_txtEmail == "True")
				{
					var strMFM_txtEmail = GetLocalizedString(enusIdxAdvertiseMFMTxtEmail, _PAGENAME, "MFM_txtEmail");
					lblEmail.Text = strMFM_txtEmail;
					lblEmail.ForeColor = System.Drawing.Color.Red;
					txtEmail.BackColor = System.Drawing.Color.Red;
					blnFoundErrors = true;
				}
			}
            //*********************************************************************************
            #endregion

            #region ValidateCompanyWebsite
            //validate txtcompanywebsite *********************************************************
            if (txtCompanyWebsite.BackColor == System.Drawing.Color.Red)
            {
				SetCompanyWebsiteDefaults();
            }

			if (txtCompanyWebsite.Text.Trim() == "")
			{
				var strMF_txtCompanyWebsite = GetLocalizedString(enusIdxAdvertiseMFTxtCompanyWebiste, _PAGENAME, "MF_txtCompanyWebsite");

				if (strMF_txtCompanyWebsite == "True")
				{
					var strMFM_txtCompanyWebsite = GetLocalizedString(enusIdxAdvertiseMFMTxtCompanyWebsite, _PAGENAME, "MFM_txtCompanyWebsite");
					lblCompanyWebsite.Text = strMFM_txtCompanyWebsite;
					lblCompanyWebsite.ForeColor = System.Drawing.Color.Red;
					txtCompanyWebsite.BackColor = System.Drawing.Color.Red;
					blnFoundErrors = true;
				}
			}
            //*********************************************************************************
            #endregion

            #region ValidateAdvertising
            //validate chkadvertising **************************************************************
/*
            if (chkLocalAdvertising.BackColor == System.Drawing.Color.Red || chkNationalAdvertising.BackColor == System.Drawing.Color.Red)
            {
				var strlblLocalAdvertising = GetLocalizedString(enusIdxAdvertiseLblLocAdv, _PAGENAME, "lbl_LocalAdvertising");
                lblLocalAdvertising.Text = strlblLocalAdvertising;
                lblLocalAdvertising.ForeColor = System.Drawing.Color.White;
                chkLocalAdvertising.BackColor = System.Drawing.Color.Transparent;
                lblNationalAdvertising.Text = strlblLocalAdvertising;
                lblNationalAdvertising.ForeColor = System.Drawing.Color.White;
                chkNationalAdvertising.BackColor = System.Drawing.Color.Transparent;
            }

            if (lblNationalAdvertising.BackColor == System.Drawing.Color.Red)
            {
				var strlblNationalAdvertising = GetLocalizedString(enusIdxAdvertiseLblNatAd, _PAGENAME, "lbl_NationalAdvertising");
                lblNationalAdvertising.Text = strlblNationalAdvertising;
                lblNationalAdvertising.ForeColor = System.Drawing.Color.White;
                chkNationalAdvertising.BackColor = System.Drawing.Color.Transparent;
            }

			var strMF_chkAdvertising = GetLocalizedString(enusIdxAdvertiseMFChkAdvertising, _PAGENAME, "MF_chkAdvertising");
			var strMFM_chkAdvertising = GetLocalizedString(enusIdxAdvertiseMFMChkAdvertising, _PAGENAME, "MFM_chkAdvertising");

            if (strMF_chkAdvertising == "True")
            {
                if (chkNationalAdvertising.Checked == false && chkLocalAdvertising.Checked == false)
                {
                    lblNationalAdvertising.Text = strMFM_chkAdvertising;
                    lblNationalAdvertising.ForeColor = System.Drawing.Color.Red;
                    chkNationalAdvertising.BackColor = System.Drawing.Color.Red;
                    lblLocalAdvertising.Text = strMFM_chkAdvertising;
                    lblLocalAdvertising.ForeColor = System.Drawing.Color.Red;
                    chkLocalAdvertising.BackColor = System.Drawing.Color.Red;
                    blnFoundErrors = true;
                }
            }
*/
            //*********************************************************************************
            #endregion
            return !blnFoundErrors;
        }
        #endregion

		protected void SendAdRequestEmail()
		{
			StringBuilder strBody = new StringBuilder("");
			strBody.Append("Company Name : " + txtCompanyName.Text.Trim() + " ");
			strBody.Append("<br><br>");
			strBody.Append("Address : " + txtCompanyAddress.Text.Trim() + " ");
			strBody.Append("<br><br>");
			strBody.Append("City : " + txtCompanyCity.Text.Trim() + " ");
			strBody.Append("<br><br>");
			strBody.Append("State : " + ddlState.SelectedValue.Trim() + " ");
			strBody.Append("<br><br>");
			strBody.Append("Zip : " + txtCompanyZip.Text.Trim() + " ");
			strBody.Append("<br><br>");
			strBody.Append("Contact First Name : " + txtContactFirstName.Text.Trim() + " ");
			strBody.Append("<br><br>");
			strBody.Append("Contact Last Name : " + txtContactLastName.Text.Trim() + " ");
			strBody.Append("<br><br>");
			strBody.Append("WorkPhone : " + txtWorkPhone.Text.Trim() + " ");
			strBody.Append("<br><br>");
			strBody.Append("CellPhone : " + txtCellPhone.Text.Trim() + " ");
			strBody.Append("<br><br>");
			strBody.Append("Email : " + txtEmail.Text.Trim() + " ");
			strBody.Append("<br><br>");
			strBody.Append("lblCompanyWebsite : " + txtCompanyWebsite.Text.Trim() + " ");
			strBody.Append("<br><br>");
			strBody.Append("Comments : " + txtComments.Text.Trim() + " ");
			strBody.Append("<br><br>");

			try
			{
				EmailUtil email = new EmailUtil( Master.g_SiteAdmin );
				string strTo = GetSiteSetting(SiteAdmin.saKeyAdvertiseEmailList, "Advertise With Email List");
				email.SendInfoMail( strTo, "Advertising request", strBody.ToString() );

			}
			catch(Exception ex )
			{
				EvtLog.WriteException("Advertise.SendAdRequestEmail failure", ex, 0);
			}
		}

		protected void OnClickBtnSubmit(object sender, ImageClickEventArgs e)
		{
			if( ValidateEntry() )
			{
				AdRequest adReq = new AdRequest();
				adReq.CompanyName = txtCompanyName.Text;
				adReq.Address = txtCompanyAddress.Text;
				adReq.City = txtCompanyCity.Text;
				adReq.State = ddlState.SelectedItem.Text;
				adReq.Zip = txtCompanyZip.Text;
				adReq.Website = txtCompanyWebsite.Text;
				adReq.FirstName = txtContactFirstName.Text;
				adReq.LastName = txtContactLastName.Text;
				adReq.Email = txtEmail.Text;
				adReq.WorkPhone = txtWorkPhone.Text;
				adReq.CellPhone = txtCellPhone.Text;
				adReq.Comments = txtComments.Text;
				adReq.LocalAds = chkLocalAdvertising.Checked;
				adReq.NationalAds = chkNationalAdvertising.Checked;

				adReq.Creator = GetActiveUserName();
				if (adReq.Creator.Length == 0)
				{
					adReq.Creator = "Unknown";
				}
				adReq.CreateDate = DateTime.Now;
				adReq.LastUpdate = DateTime.Now.ToLongDateString();

				if(g_adList.Add(adReq))
				{
					//If successful notify the user the add succeeded, send email and go to Login
					SendAdRequestEmail();
					Master.AlertUser("Thank you for your interest.  Your request was sent successfully.  We will get back to you as soon as possible.");
					RedirectToLoginPage();
				}
				else
				{
					//If failed notify the user the add failed and stay here or take them to support page
					Master.AlertUser("There was a problem submitting your request.  Please try again or contact us using the support page.");
				}
			}
		}

		protected void OnClickBtnCancel(object sender, ImageClickEventArgs e)
		{
            SetStandard();
            GetLabelText();
		}

		protected void OnClickBtnLogin(object sender, ImageClickEventArgs e)
		{
			Session.Abandon();
			SessionIDManager managera = new SessionIDManager();
			string newIDa = managera.CreateSessionID(Context);
			bool redirecteda = false;
			bool isAddeda = false;
			managera.SaveSessionID(Context, newIDa, out redirecteda, out isAddeda);
			RedirectToLoginPage();
		}
    }
}