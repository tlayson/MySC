using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MyUSC.Classes;
using MyUSC.Classes.MyOrg;

namespace MyUSC.Experiment
{
	public partial class Test : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if( !IsPostBack )
			{
				AddRow( "1" );
				AddRow("2");
				AddRow("3");
				AddRow("4");
				AddRow("5");
				AddRow("6");
				AddRow("7");
			}
		}

		protected void AddRow( string strLabel )
		{
			Table tbl = ResultsTable1.GetResultsTable();
			TableRow tr = new TableRow();
			TableCell td = new TableCell();
			Label lbl = new Label();
			lbl.Text = "Label " + strLabel;
			td.Controls.Add(lbl);
			tr.Controls.Add(td);

			td = new TableCell();
			Button btn = new Button();
			btn.Text = "Button " + strLabel;
			btn.Click += new EventHandler(this.OnClickResults);
			td.Controls.Add( btn );
			tr.Controls.Add(td);

			tbl.Controls.Add(tr);
		}

		protected void OnClickTest(object sender, EventArgs e)
		{

		}

		protected void OnClickResults(object sender, EventArgs e)
		{

		}
	}
}