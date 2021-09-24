<%@ Register TagPrefix="uc1" TagName="TopBanner" Src="Banner.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Footer" Src="Footer.ascx" %>
<%@ Page language="c#" %>
<%@ Import Namespace="System.Data" %>
<%@ Import Namespace="System.Data.SqlClient" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
	<head>
		<title>Who's Online</title>
		<link rel="stylesheet" href="sample.css" type="text/css" />
		<style type="text/css">
		table.Grid
		{
			border-width: 5px;
			border-style: none;
			background-color: White;
			border-color: #cccccc;
			border-collapse: collapse;
		}

		table.Grid TD
		{
			padding: 4px 6px 4px 6px;
			border: solid 1px #cccccc;
			vertical-align: top;
			font-family:segoe ui, arial,verdana,helvetica,sans-serif;
			font-size:11px;
		}
		</style>
		</head>
<body>
    <form id="Form1" method="post" runat="server">
        <uc1:TopBanner ID="TopBanner1" runat="server"></uc1:TopBanner>
        <div style="width: 900px; margin: 30px auto; min-height: 400px;">
            <h1>
                Members</h1>
            <asp:DataGrid ID="gridUsersList" CssClass="Grid" OnPageIndexChanged="gridUsersList_PageIndexChanged"
                OnSortCommand="OnColSortSelected" runat="server" BorderColor="#cccccc" BorderWidth="1px"
                BackColor="White" CellPadding="1" CellSpacing="0" PageSize="20" AutoGenerateColumns="False"
                Width="99%" AllowPaging="True" AllowSorting="True">
                <FooterStyle ForeColor="#000000" BackColor="#efefef"></FooterStyle>
                <SelectedItemStyle ForeColor="White" BackColor="#669999"></SelectedItemStyle>
                <AlternatingItemStyle ForeColor="#000000" BackColor="#f5f5f5"></AlternatingItemStyle>
                <ItemStyle ForeColor="#000000" BackColor="White"></ItemStyle>
                <HeaderStyle ForeColor="#000000" BackColor="#efefef"></HeaderStyle>
                <Columns>
                    <asp:HyperLinkColumn HeaderStyle-HorizontalAlign="Center" SortExpression="Username"
                        HeaderText="Username" DataNavigateUrlField="Username" DataNavigateUrlFormatString="viewprofile.aspx?Username={0}"
                        DataTextField="Username">
                        <ItemStyle HorizontalAlign="Center" Width="80"></ItemStyle>
                    </asp:HyperLinkColumn>
                    <asp:BoundColumn SortExpression="SexString" DataField="SexString" HeaderText="Gender">
                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center" Width="35"></ItemStyle>
                    </asp:BoundColumn>
                    <asp:BoundColumn SortExpression="Age" DataField="Age" HeaderText="Age">
                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center" Width="30"></ItemStyle>
                    </asp:BoundColumn>
                    <asp:BoundColumn SortExpression="Location" DataField="Location" HeaderText="Location">
                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center" Width="100"></ItemStyle>
                    </asp:BoundColumn>
                    <asp:BoundColumn SortExpression="Occupation" DataField="Occupation" HeaderText="Occupation">
                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center" Width="80"></ItemStyle>
                    </asp:BoundColumn>
                    <asp:BoundColumn SortExpression="Interests" DataField="Interests" HeaderText="Interests">
                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center" Width="120"></ItemStyle>
                    </asp:BoundColumn>
                    <asp:BoundColumn SortExpression="LastLoginTime" DataField="LastLoginTime" HeaderText="Last Active"
                        DataFormatString="{0:MM-dd-yyyy}">
                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center" Width="90"></ItemStyle>
                    </asp:BoundColumn>
                    <asp:BoundColumn SortExpression="DateCreated" DataField="DateCreated" HeaderText="Account Created"
                        DataFormatString="{0:MM-dd-yyyy}">
                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center" Width="90"></ItemStyle>
                    </asp:BoundColumn>
                </Columns>
                <PagerStyle HorizontalAlign="Left" ForeColor="#000000" BackColor="White" Mode="NumericPages">
                </PagerStyle>
            </asp:DataGrid>
        </div>
        <uc1:Footer ID="Footer1" runat="server"></uc1:Footer>
    </form>

    <script runat="server">
        private void Page_Load(object sender, System.EventArgs e)
        {
            if (!this.IsPostBack)
            {
                // Initialize first time
                ShowSortData("");
            }
        }

        private void gridUsersList_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
        {
            this.gridUsersList.CurrentPageIndex = e.NewPageIndex;

            this.gridUsersList.DataSource = SamplePortal.Components.UserData.GetAllUsers(); ;
            this.gridUsersList.DataBind();
            this.gridUsersList.PagerStyle.Visible = (this.gridUsersList.PageCount > 1);
        }

        private void ShowSortData(string sOrderBy)
        {
            DataView oView = new DataView(SamplePortal.Components.UserData.GetAllUsers().Tables[0]);
            if (sOrderBy.Length == 0)
                sOrderBy = "userName ASC";
            oView.Sort = sOrderBy;
            this.gridUsersList.DataSource = oView;
            this.gridUsersList.DataBind();
            this.gridUsersList.PagerStyle.Visible = (this.gridUsersList.PageCount > 1);
        }

        private void OnColSortSelected(object source, System.Web.UI.WebControls.DataGridSortCommandEventArgs e)
        {
            // Call the function with order by string
            ShowSortData(SamplePortal.Components.GridUtils.OnColSortSelection(gridUsersList, e));
        }

    </script>

</body>
</HTML>
