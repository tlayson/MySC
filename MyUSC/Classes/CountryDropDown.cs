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
	public class CountryDropDown : DropDownList
	{
		public CountryDropDown()
		{
			AddCountries();
		}

		protected void AddCountries()
		{
			ListItem li = new ListItem();
			li.Text = "---";
			li.Value = "";
			this.Items.Add( li );

			li = new ListItem();
			li.Text = "United States";
			li.Value = "United States";
			this.Items.Add(li);

			li = new ListItem();
			li.Text = "Canada";
			li.Value = "Canada";
			this.Items.Add( li );

			li = new ListItem();
			li.Text = "Puerto Rico";
			li.Value = "Puerto Rico";
			this.Items.Add( li );

			li = new ListItem();
			li.Text = "Other";
			li.Value = "Other";
			this.Items.Add( li );
		}

		protected void SetSelectedCountry( string strCountry )
		{
			SelectedValue = strCountry;
		}
	}
}