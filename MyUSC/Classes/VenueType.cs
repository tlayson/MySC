using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MyUSC.Classes;
using MyUSC.Classes.MyOrg;

[assembly: TagPrefix("MyUSC.Classes", "MSC")]
namespace MyUSC.Classes
{
	[
		ToolboxData("<{0}:VenueType ID='VenueTypeID' runat=\"server\"> </{0}:VenueType>")
	]
	public class VenueType : DropDownList
	{
		public VenueType()
		{
		}

		public void LoadVenueTypes()
		{
			this.Items.Clear();

			ListItem li = new ListItem( " --- ", "" );
			this.Items.Add( li );
			li = new ListItem("Baseball", "Baseball");
			this.Items.Add(li);
			li = new ListItem("Football", "Football");
			this.Items.Add(li);
			li = new ListItem("Soccer", "Soccer");
			this.Items.Add(li);
			li = new ListItem("Hockey", "Hockey");
			this.Items.Add(li);
			li = new ListItem("Basketball", "Basketball");
			this.Items.Add(li);
			li = new ListItem("Lacrosse", "Lacrosse");
			this.Items.Add(li);
			li = new ListItem("Track", "Track");
			this.Items.Add(li);
			li = new ListItem("Multi Purpose", "Multi Purpose");
			this.Items.Add(li);
			li = new ListItem("Other", "Other");
			this.Items.Add(li);
		}
	}
}