using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MyUSC.Classes;

namespace MyUSC.MyAccount
{
	public partial class MyAccountSports : USCPageBase
	{
#region ENUM FavSports
/*
NewsMenuKeyID	Name
1	MLB
2	NBA
3	NFL
4	MLS
5	NHL
6	WNBA
7	NASCAR
8	PGA
9	LPGA
10	PBA
11	NCAA Football
12	UFC
13	CFL
14	Youth Sports
15	Olympics
16	NCAA Baseball
17	Extreme Sports
18	Outdoor Sports
19	Amateur Sports
20	Pro Lacrosse
*/

		enum FavSports
		{
			MLB = 1,
			NBA = 2,
			NFL = 3,
			MLS = 4,
			NHL = 5,
			WNBA = 6,
			NASCAR = 7,
			PGA = 8,
			LPGA = 9,
			PBA = 10,
			NCAA = 11,
			UFC = 12,
			CFL = 13,
			YouthSports = 14,
			Olympics = 15,
			NCAABaseball = 16,
			Extreme = 17,
			OutdoorSports = 18,
			AmateurSports = 19,
			ProLacrosse = 20,
			MAX = 20
		}
#endregion

		protected MyAccountSports()
		{
			_PAGENAME = "AccountSports";
			_pgUSCMaster = (USCMaster)Master;
		}

#region SetDataValues
		private void SetDataValues()
		{

			if( !IsPostBack )
			{
				// Get the users account
				UserAccount acct = GetActiveUser();

				// Set the values
				if (null == acct)
				{
					RedirectToLoginPage();
				}
				else
				{
					txtInterests.Text = acct.Preferences.Interests;

					foreach (DictionaryEntry de in acct.Preferences.htNewsMenuItems)
					{
						FavSports fs = (FavSports)de.Value;
						if (FavSports.MAX >= fs)
						{
							CheckSportBox(fs);
						}
					}
				}
			}
		}

		private void CheckSportBox(FavSports sport)
		{
			switch (sport)
			{
				case FavSports.MLB:
				{
					chkMLB.Checked = true;
					break;
				}
				case FavSports.NBA:
				{
					chkNBA.Checked = true;
					break;
				}
				case FavSports.NFL:
				{
					chkNFL.Checked = true;
					break;
				}
				case FavSports.MLS:
				{
					chkMLS.Checked = true;
					break;
				}
				case FavSports.NHL:
				{
					chkNHL.Checked = true;
					break;
				}
				case FavSports.WNBA:
				{
					chkWNBA.Checked = true;
					break;
				}
				case FavSports.NASCAR:
				{
					chkNASCAR.Checked = true;
					break;
				}
				case FavSports.LPGA:
				{
					chkLPGA.Checked = true;
					break;
				}
				case FavSports.PGA:
				{
					chkPGA.Checked = true;
					break;
				}
				case FavSports.PBA:
				{
					chkPBA.Checked = true;
					break;
				}
				case FavSports.NCAA:
				{
					chkNCAA.Checked = true;
					break;
				}
				case FavSports.UFC:
				{
					chkUFC.Checked = true;
					break;
				}
				case FavSports.CFL:
				{
					chkCFL.Checked = true;
					break;
				}
				case FavSports.Extreme:
				{
					chkExtreme.Checked = true;
					break;
				}
				case FavSports.Olympics:
				{
					chkOlympics.Checked = true;
					break;
				}
				case FavSports.YouthSports:
				{
					chkYouthSports.Checked = true;
					break;
				}
				case FavSports.AmateurSports:
				{
					chkAmateurSports.Checked = true;
					break;
				}
				case FavSports.ProLacrosse:
				{
					chkLacrosse.Checked = true;
					break;
				}
			}
		}

#endregion

#region Page_Load
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				SetDataValues();
				Master.SelectMenuItem(SelectedPage.Profile);
			}
		}
#endregion

#region Button Clicks
		protected bool GetData(UserAccount acct)
		{
			bool fRet = true;

			acct.Preferences.Interests = txtInterests.Text;
			GetCheckedSports( acct );

			return fRet;
		}

		private void GetCheckedSports(UserAccount acct)
		{
			acct.Preferences.htNewsMenuItems.Clear();

			int nVal = 0;
			if (chkCFL.Checked)
			{
				nVal = (int)FavSports.CFL;
				acct.Preferences.htNewsMenuItems.Add(nVal, nVal);
				nVal = 0;
			}
			if (chkExtreme.Checked)
			{
				nVal = (int)FavSports.Extreme;
				acct.Preferences.htNewsMenuItems.Add(nVal, nVal);
				nVal = 0;
			}
			if (chkLPGA.Checked)
			{
				nVal = (int)FavSports.LPGA;
				acct.Preferences.htNewsMenuItems.Add(nVal, nVal);
				nVal = 0;
			}
			if (chkMLB.Checked)
			{
				nVal = (int)FavSports.MLB;
				acct.Preferences.htNewsMenuItems.Add(nVal, nVal);
				nVal = 0;
			}
			if (chkMLS.Checked)
			{
				nVal = (int)FavSports.MLS;
				acct.Preferences.htNewsMenuItems.Add(nVal, nVal);
				nVal = 0;
			}
			if (chkNASCAR.Checked)
			{
				nVal = (int)FavSports.NASCAR;
				acct.Preferences.htNewsMenuItems.Add(nVal, nVal);
				nVal = 0;
			}
			if (chkNBA.Checked)
			{
				nVal = (int)FavSports.NBA;
				acct.Preferences.htNewsMenuItems.Add(nVal, nVal);
				nVal = 0;
			}
			if (chkNCAA.Checked)
			{
				nVal = (int)FavSports.NCAA;
				acct.Preferences.htNewsMenuItems.Add(nVal, nVal);
				nVal = 0;
			}
			if (chkNFL.Checked)
			{
				nVal = (int)FavSports.NFL;
				acct.Preferences.htNewsMenuItems.Add(nVal, nVal);
				nVal = 0;
			}
			if (chkNHL.Checked)
			{
				nVal = (int)FavSports.NHL;
				acct.Preferences.htNewsMenuItems.Add(nVal, nVal);
				nVal = 0;
			}
			if (chkOlympics.Checked)
			{
				nVal = (int)FavSports.Olympics;
				acct.Preferences.htNewsMenuItems.Add(nVal, nVal);
				nVal = 0;
			}
			if (chkPBA.Checked)
			{
				nVal = (int)FavSports.PBA;
				acct.Preferences.htNewsMenuItems.Add(nVal, nVal);
				nVal = 0;
			}

			if (chkPGA.Checked)
			{
				nVal = (int)FavSports.PGA;
				acct.Preferences.htNewsMenuItems.Add(nVal, nVal);
				nVal = 0;
			}

			if (chkUFC.Checked)
			{
				nVal = (int)FavSports.UFC;
				acct.Preferences.htNewsMenuItems.Add(nVal, nVal);
				nVal = 0;
			}

			if (chkWNBA.Checked)
			{
				nVal = (int)FavSports.WNBA;
				acct.Preferences.htNewsMenuItems.Add(nVal, nVal);
				nVal = 0;
			}
			if (chkYouthSports.Checked)
			{
				nVal = (int)FavSports.YouthSports;
				acct.Preferences.htNewsMenuItems.Add(nVal, nVal);
				nVal = 0;
			}
			if (chkAmateurSports.Checked)
			{
				nVal = (int)FavSports.AmateurSports;
				acct.Preferences.htNewsMenuItems.Add(nVal, nVal);
				nVal = 0;
			}
			if (chkLacrosse.Checked)
			{
				nVal = (int)FavSports.ProLacrosse;
				acct.Preferences.htNewsMenuItems.Add(nVal, nVal);
				nVal = 0;
			}
		}

		protected void OnClickOK(object sender, ImageClickEventArgs e)
		{
			// Get the users account
			UserAccount acct = GetActiveUser();

			if (GetData(acct))
			{
				acct.UpdatePreferences();
				Response.Redirect("/MyAccount/MyAccount.aspx");
			}
		}

		protected void OnClickCancel(object sender, ImageClickEventArgs e)
		{
			Response.Redirect("/MyAccount/MyAccount.aspx");
		}
#endregion
	}
}