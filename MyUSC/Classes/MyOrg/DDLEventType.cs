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
	public class DDLEventType : TBSCDropDown
	{

		public DDLEventType()
		{
			LoadTypes();
		}

/*
				public enum EventTypes
				{
					Undefined = 0,
					Game = 1,
					Practice = 2,
					PracticeGame = 3,
					Tournament = 4,
					Playoff = 5,
					Race = 6,
					Match = 7,
					Meet = 8,
					Jamboree = 9,
					Ride = 10,
					Workout = 11,
					Meeting = 12,
					Party = 13,
					Other = 100
				}
*/
		private void LoadTypes()
		{
			ListItem li = new ListItem( "---", "0" );
			this.Items.Add( li );
			li = new ListItem("Game", "1");
			this.Items.Add( li );
			li = new ListItem("Practice", "2");
			this.Items.Add( li );
			li = new ListItem("PracticeGame", "3");
			this.Items.Add( li );
			li = new ListItem("Tournament", "4");
			this.Items.Add( li );
			li = new ListItem("Playoff", "5");
			this.Items.Add( li );
			li = new ListItem("Race", "6");
			this.Items.Add( li );
			li = new ListItem("Match", "7");
			this.Items.Add( li );
			li = new ListItem("Meet", "8");
			this.Items.Add( li );
			li = new ListItem("Jamboree", "9");
			this.Items.Add(li);
			li = new ListItem("Ride", "10");
			this.Items.Add(li);
			li = new ListItem("Workout", "11");
			this.Items.Add(li);
			li = new ListItem("Meeting", "12");
			this.Items.Add(li);
			li = new ListItem("Party", "13");
			this.Items.Add(li);
			li = new ListItem("Other", "100");
			this.Items.Add(li);
		}

		public Event.EventTypes EvtType()
		{
			Event.EventTypes ot = (Event.EventTypes)IntValue();
			return ot;
		}

		public void EvtType(Event.EventTypes ot)
		{
			IntValue((int)ot);
		}
	}
}