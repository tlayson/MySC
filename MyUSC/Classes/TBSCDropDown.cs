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
	public class TBSCDropDown : DropDownList
	{
		private object m_userData;
		private long m_userValue;

		public TBSCDropDown()
		{
			m_userData = null;
			m_userValue = -1;
		}

		public object UserData
		{
			get
			{
				return this.m_userData;
			}
			set
			{
				this.m_userData = value;
			}
		}
		
		public long UserValue
		{
			get
			{
				return this.m_userValue;
			}
			set
			{
				this.m_userValue = value;
			}
		}

		public int IntValue()
		{
			int nRet = -1;
			string strVal = this.SelectedValue;
			int nOut = -1;
			if (int.TryParse( strVal, out nOut ) )
			{
				nRet = nOut;
			}

			return nRet;
		}

		public void IntValue( int nIn )
		{
			string strValue = "";
			strValue += nIn;
			if(this.Items.FindByValue( strValue ) != null) 
			{
				this.SelectedValue = strValue;
			}
		}
	}
}

