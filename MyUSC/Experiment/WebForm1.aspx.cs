using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MyUSC.Experiment
{
	public partial class WebForm1 : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{

		}

		public string CnxStr
		{
			get
			{
				return "Server=localhost;Database=MyUSCLocal;User ID=myscadmin;Password=Mysc2013!";
			}
		}

		/// <summary>
		/// Handler for the "add reminder" button
		/// </summary>
		/// <param name="sender">source</param>
		/// <param name="e">arguments</param>
		protected void ReminderButton_Click(object sender, EventArgs e)
		{
			string text;
			try
			{
				text = string.Format("A reminder would have been created for {0} with the message \"{1}\"",
					DateTime.Parse(DateTextBox.Text).ToLongDateString(), MessageTextBox.Text);
			}
			catch (FormatException ex)
			{
				text = string.Format("[Unable to parse \"{0}\": {1}]", DateTextBox.Text, ex.Message);
			}
			Label1.Text = HttpUtility.HtmlEncode(text);
		}

		/// <summary>
		/// Handler for calendar changes
		/// </summary>
		/// <param name="sender">source</param>
		/// <param name="e">arguments</param>
		protected void Calendar1_SelectionChanged(object sender, EventArgs e)
		{
			// Popup result is the selected date
			PopupControlExtender1.Commit(Calendar1.SelectedDate.ToShortDateString());
		}

		/// <summary>
		/// Handler for radio button changes
		/// </summary>
		/// <param name="sender">source</param>
		/// <param name="e">arguments</param>
		protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (!string.IsNullOrEmpty(RadioButtonList1.SelectedValue))
			{
				// Popup result is the selected task
				PopupControlExtender2.Commit(RadioButtonList1.SelectedValue);
			}
			else
			{
				// Cancel the popup
				PopupControlExtender2.Cancel();
			}
			// Reset the selected item
			RadioButtonList1.ClearSelection();
		}

		protected void OnClickTest(object sender, EventArgs e)
		{
			TBSCRSVPDlg1.EventID = 1;
			TBSCRSVPDlg1.OrgID = 1;
			TBSCRSVPDlg1.VenueID = 1;
		}
	}

}