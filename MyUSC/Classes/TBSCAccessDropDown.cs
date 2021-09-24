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
	public class TBSCAccessDropDown : DropDownList
	{
		private object m_userData;
		private long m_userValue;

		public TBSCAccessDropDown()
		{
			m_userData = null;
			m_userValue = -1;
			AutoPostBack = false;
			CreateItems();
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

		protected void CreateItems()
		{
			ListItem li = new ListItem();
			li.Text = "<<Select>>";
			li.Value = "-1";
			li.Selected = true;
			Items.Add( li );

			li = new ListItem();
			li.Text = "Owner";
			li.Value = "1";
			li.Selected = false;
			Items.Add( li );
			
			li = new ListItem();
			li.Text = "Admin";
			li.Value = "2";
			li.Selected = false;
			Items.Add( li );
			
			li = new ListItem();
			li.Text = "Contributor";
			li.Value = "3";
			li.Selected = false;
			Items.Add( li );
			
			li = new ListItem();
			li.Text = "Member";
			li.Value = "4";
			li.Selected = false;
			Items.Add( li );
			
			li = new ListItem();
			li.Text = "Follower";
			li.Value = "5";
			li.Selected = false;
			Items.Add( li );
			
			li = new ListItem();
			li.Text = "Guest";
			li.Value = "6";
			li.Selected = false;
			Items.Add( li );
			
			li = new ListItem();
			li.Text = "Banned";
			li.Value = "10";
			li.Selected = false;
			Items.Add( li );
		}

		public OrgAccessTypes GetSelectedType()
		{
			OrgAccessTypes oat = OrgAccessTypes.Undefined;
			string strVal = SelectedValue;
			int nAT = 0;
			if( int.TryParse( strVal, out nAT ) )
			{
				oat = (OrgAccessTypes)nAT;
			}

			return oat;
		}

		public void SetSelectedType( OrgAccessTypes oat )
		{
			int nSel = (int)oat;
			string strSelect = nSel.ToString();
			SelectedValue = strSelect;
		}
	}
}