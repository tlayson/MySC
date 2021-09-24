using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MyUSC.Classes;
using MyUSC.Classes.MyOrg;

namespace MyUSC.Classes.MyOrg
{
	public class DDLOrgType : TBSCDropDown
	{
		public DDLOrgType()
		{
			LoadTypes();
		}

//	public enum OrgTypes { Undefined = -1, Organization = 1, Region = 2, State = 3, District = 4, League = 5, Division = 6, Team = 7, Other = 10 }
		private void LoadTypes()
		{
			ListItem li = new ListItem( "---", "-1" );
			this.Items.Add( li );
			li = new ListItem( "Organization", "1" );
			this.Items.Add( li );
			li = new ListItem( "Region", "2" );
			this.Items.Add( li );
			li = new ListItem( "State", "3" );
			this.Items.Add( li );
			li = new ListItem( "District", "4" );
			this.Items.Add( li );
			li = new ListItem( "League", "5" );
			this.Items.Add( li );
			li = new ListItem( "Division", "6" );
			this.Items.Add( li );
			li = new ListItem( "Team", "7" );
			this.Items.Add( li );
			li = new ListItem( "Other", "10" );
			this.Items.Add( li );
		}

		public OrgTypes OrgType()
		{
			OrgTypes ot = (OrgTypes)IntValue();
			return ot;
		}

		public void OrgType( OrgTypes ot )
		{
			IntValue( (int)ot );
		}
	}
}