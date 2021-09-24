<%@ Register TagPrefix="uc1" TagName="TopBanner" Src="Banner.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Footer" Src="Footer.ascx" %>

<%@ Page Language="c#" %>

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
        <div style="width: 900px; margin: 30px auto; min-height:400px;">
            <h1>
                Who's Online</h1>
            <asp:Repeater ID="rptOnlineUsers" runat="server">
                <ItemTemplate>
                    <a href="~/memberlist.aspx" onclick="window.open('viewprofile.aspx?Username=<%#DataBinder.Eval(Container.DataItem, "Username")%>');">
                        <%#DataBinder.Eval(Container.DataItem, "Location")%>
                        &nbsp;<%#DataBinder.Eval(Container.DataItem, "Occupation")%></a><br />
                </ItemTemplate>
            </asp:Repeater>
            <asp:DataGrid ID="gridUsersList" CssClass="Grid" OnPageIndexChanged="gridUsersList_PageIndexChanged"
                OnSortCommand="OnColSortSelected" runat="server" CellPadding="1" CellSpacing="0"
                AutoGenerateColumns="False" Width="99%" AllowPaging="True" AllowSorting="True">
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
                    <asp:BoundColumn SortExpression="Age" DataField="Age" HeaderText="Age">
                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:BoundColumn>
                    <asp:BoundColumn SortExpression="SexString" DataField="SexString" HeaderText="Gender">
                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:BoundColumn>
                    <asp:BoundColumn SortExpression="Location" DataField="Location" HeaderText="Location">
                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:BoundColumn>
                    <asp:BoundColumn SortExpression="Occupation" DataField="Occupation" HeaderText="Occupation">
                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:BoundColumn>
                    <asp:BoundColumn SortExpression="Interests" DataField="Interests" HeaderText="Interests">
                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:BoundColumn>
                    <asp:BoundColumn SortExpression="LastLoginTime" DataField="LastLoginTime" HeaderText="Last Active"
                        DataFormatString="{0:MM-dd-yyyy}">
                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:BoundColumn>
                    <asp:BoundColumn SortExpression="DateCreated" DataField="DateCreated" HeaderText="Account Created"
                        DataFormatString="{0:MM-dd-yyyy}">
                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:BoundColumn>
                </Columns>
                <PagerStyle HorizontalAlign="Left" ForeColor="#000066" BackColor="White" Mode="NumericPages">
                </PagerStyle>
            </asp:DataGrid>
        </div>
        <uc1:Footer ID="Footer1" runat="server"></uc1:Footer>
    </form>
</body>
</html>

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
        DataView oView = new DataView(GetOnlineUsers().Tables[0]);
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

    public DataSet GetOnlineUsers()
    {
        SqlConnection conn = new SqlConnection(SamplePortal.Global.ConnectionString);
        SqlCommand cmd = new SqlCommand("select * from sampleUsers where LastLoginTime>=@LastLoginTime order by LastLoginTime desc", conn);

        SqlParameter paraLastLoginTime = cmd.Parameters.Add("@LastLoginTime", SqlDbType.DateTime);
        paraLastLoginTime.Value = SamplePortal.Global.OnlineTimeoutTime;

        SqlDataAdapter ada = new SqlDataAdapter(cmd);
        DataSet result = new DataSet();

        try
        {
            conn.Open();
            ada.Fill(result);
        }
        finally
        {
            conn.Close();
        }

        result.Tables[0].Columns.Add("SexString", typeof(String));
        result.Tables[0].Columns.Add("Roles", typeof(String));

        foreach (DataRow row in result.Tables[0].Rows)
        {
            row["SexString"] = ((Boolean)row["Gender"]) ? "Male" : "Female";
            row["Roles"] = String.Join(",", SamplePortal.Global.GetRolesOfUser(row["Username"].ToString()));
        }


        return result;
    }

</script>

