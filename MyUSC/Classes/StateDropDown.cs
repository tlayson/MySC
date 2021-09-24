using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MyUSC.Classes;
using MyUSC.Classes.MyOrg;

namespace MyUSC.Classes
{
	public class StateDropDown : DropDownList
	{
		public StateDropDown()
		{
		}

		public void LoadStates()
		{
			USCMaster mp = (USCMaster)(Page.Master);
            var lstState = mp.GetSiteSetting(SiteAdmin.saKeyStates, "StateNames");
            List<string> lstStates = new List<string>();
            lstStates = lstState.Trim().Split(',').ToList();
            this.Items.Clear();

            for (int intCounter = 0; intCounter < lstStates.Count; intCounter++)
            {
                this.Items.Add(lstStates[intCounter].Trim());
            }
		}
	}
}