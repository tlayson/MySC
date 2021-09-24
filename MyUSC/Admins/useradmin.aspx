<%@ Register TagPrefix="uc1" TagName="TopBanner" Src="AdminBanner.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Footer" Src="~/Footer.ascx" %>

<%@ Page Language="c#" %>

<%@ Import Namespace="System.Data" %>
<%@ Import Namespace="System.Data.SqlClient" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Users Administration</title>
    <style type="text/css">
		TABLE.Grid { BORDER-BOTTOM: #cccccc 5px; BORDER-LEFT: #cccccc 5px; BACKGROUND-COLOR: white; BORDER-COLLAPSE: collapse; BORDER-TOP: #cccccc 5px; BORDER-RIGHT: #cccccc 5px }
		TABLE.Grid TD { BORDER-BOTTOM: #cccccc 1px solid; BORDER-LEFT: #cccccc 1px solid; PADDING-BOTTOM: 4px; PADDING-LEFT: 6px; PADDING-RIGHT: 6px; FONT-FAMILY: segoe ui, arial,verdana,helvetica,sans-serif; FONT-SIZE: 11px; VERTICAL-ALIGN: top; BORDER-TOP: #cccccc 1px solid; BORDER-RIGHT: #cccccc 1px solid; PADDING-TOP: 4px }
		</style>
    <link href="../Sample.css" type="text/css" rel="stylesheet">
</head>
<body>
    <form id="Form1" method="post" runat="server">
        <uc1:TopBanner ID="TopBanner1" runat="server"></uc1:TopBanner>
        <div style="width: 900px; margin: 30px auto; min-height: 400px;">
            <h1>
                Members Administration</h1>
            <asp:DataGrid runat="server" ID="gridUsersList" OnPageIndexChanged="gridUsersList_PageIndexChanged"
                OnSortCommand="OnColSortSelected" OnDeleteCommand="gridUsersList_DeleteCommand"
                DataKeyField="Username" AllowSorting="True" OnItemCommand="gridUsersList_ItemCommand"
                Width="99%" AllowPaging="True" CssClass="Grid" CellPadding="3" AutoGenerateColumns="False">
                <FooterStyle ForeColor="#000000" BackColor="#efefef"></FooterStyle>
                <SelectedItemStyle ForeColor="White" BackColor="#669999"></SelectedItemStyle>
                <AlternatingItemStyle ForeColor="#000000" BackColor="#f5f5f5"></AlternatingItemStyle>
                <ItemStyle ForeColor="#000000" BackColor="White"></ItemStyle>
                <HeaderStyle ForeColor="#000000" BackColor="#efefef"></HeaderStyle>
                <Columns>
                    <asp:BoundColumn SortExpression="Username" DataField="Username" HeaderText="Username">
                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center" Wrap="False"></ItemStyle>
                    </asp:BoundColumn>
                    <asp:BoundColumn SortExpression="SexString" DataField="SexString" HeaderText="Gender">
                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center" Wrap="False"></ItemStyle>
                    </asp:BoundColumn>
                    <asp:BoundColumn SortExpression="Age" DataField="Age" HeaderText="Age">
                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center" Wrap="False"></ItemStyle>
                    </asp:BoundColumn>
                    <asp:BoundColumn SortExpression="Location" DataField="Location" HeaderText="Location">
                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center" Width="100" Wrap="False"></ItemStyle>
                    </asp:BoundColumn>
                    <asp:BoundColumn SortExpression="Occupation" DataField="Occupation" HeaderText="Occupation">
                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center" Width="80" Wrap="False"></ItemStyle>
                    </asp:BoundColumn>
                    <asp:BoundColumn SortExpression="Interests" DataField="Interests" HeaderText="Interests">
                        <HeaderStyle HorizontalAlign="Center" Wrap="False"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center" Width="120" Wrap="False"></ItemStyle>
                    </asp:BoundColumn>
                    <asp:BoundColumn SortExpression="LastLoginTime" DataField="LastLoginTime" HeaderText="Last Active"
                        DataFormatString="{0:MM-dd-yyyy}">
                        <HeaderStyle HorizontalAlign="Center" Wrap="False"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center" Width="90" Wrap="False"></ItemStyle>
                    </asp:BoundColumn>
                    <asp:BoundColumn SortExpression="DateCreated" DataField="DateCreated" HeaderText="Account Created"
                        DataFormatString="{0:MM-dd-yyyy}">
                        <HeaderStyle HorizontalAlign="Center" Wrap="False"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center" Width="90" Wrap="False"></ItemStyle>
                    </asp:BoundColumn>
                    <asp:BoundColumn SortExpression="Roles" DataField="Roles" HeaderText="Roles">
                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center" Wrap="False"></ItemStyle>
                    </asp:BoundColumn>
                    <asp:ButtonColumn Text="<img border=0 src=..\images\role.gif alt='Manage Roles'>"
                        HeaderText="Manage Roles" CommandName="ManageRoles">
                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center" Wrap="False"></ItemStyle>
                    </asp:ButtonColumn>
                    <asp:ButtonColumn Text="<img border=0 src=..\images\edit.gif alt=edit>" HeaderText="Edit"
                        CommandName="Modify">
                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center" Wrap="False"></ItemStyle>
                    </asp:ButtonColumn>
                    <asp:ButtonColumn Text="&lt;div onclick=&quot;return confirm('Are you sure you want to delete this user?')&quot;&gt;<img border=0 src=..\images\delete.gif alt=delete>&lt;/div&gt;"
                        HeaderText="Delete" CommandName="Delete">
                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center" Wrap="False"></ItemStyle>
                    </asp:ButtonColumn>
                </Columns>
                <PagerStyle HorizontalAlign="Left" ForeColor="#000066" BackColor="White" Mode="NumericPages">
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
                ShowSortData("");
            }
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

        public void DeleteUser(String username)
        {
            SqlConnection conn = new SqlConnection(SamplePortal.Global.ConnectionString);
            SqlCommand cmd = new SqlCommand("delete from sampleUsers where Username=@Username", conn);

            SqlParameter paraUsername = cmd.Parameters.Add("@Username", SqlDbType.NVarChar);
            paraUsername.Value = username;

            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
            }
            finally
            {
                conn.Close();
            }

            SamplePortal.Global.RemoveUserFromAllRoles(username);
        }

        private void gridUsersList_DeleteCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
        {
            String sUsername = e.Item.Cells[0].Text;

            if (sUsername == this.User.Identity.Name)
            {
                SamplePortal.JSHelper.MsgBox("You cannot delete yourself!");
                return;
            }

            this.DeleteUser(sUsername);

            ShowSortData("");

        }

        private void gridUsersList_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
        {
            String sUsername = e.Item.Cells[0].Text;
            if (e.CommandName == "ManageRoles")
            {
                this.Response.Redirect("UserRoleAdmin.aspx?Username=" + sUsername);
            }
            else if (e.CommandName == "Modify")
            {
                this.Response.Redirect("ModifyProfile.aspx?Username=" + sUsername);
            }
        }


        private void gridUsersList_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
        {
            this.gridUsersList.CurrentPageIndex = e.NewPageIndex;
            this.gridUsersList.DataSource = SamplePortal.Components.UserData.GetAllUsers(); ;
            this.gridUsersList.DataBind();
            this.gridUsersList.PagerStyle.Visible = (this.gridUsersList.PageCount > 1);
        }
	
    </script>

</body>
</html>
