<%@ Page language="c#" %>
<%@ Register TagPrefix="uc1" TagName="TopBanner" Src="AdminBanner.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Footer" Src="~/Footer.ascx" %>
<%@ Import Namespace="System.Data" %>
<%@ Import Namespace="System.Data.SqlClient" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
	<head>
		<title>User Role Administration</title>
		<link href="../Sample.css" type="text/css" rel="stylesheet" />
	</head>
<body>
    <form id="Form1" method="post" runat="server">
        <uc1:TopBanner ID="TopBanner1" runat="server"></uc1:TopBanner>
        <div class="infobox" style="width: 700px; margin: 30px auto; min-height:350px;">
            <h2>
                User Role Management
            </h2>
            <div class="padding10">
                <table cellpadding="15" style="text-align:center;width:90%">
                    <tr>
                        <td align="right">
                            <span style="font-size:16px; font-weight:bold">
                                All Roles</span>
                        </td>
                        <td align="center">
                            <span style="font-size:16px; font-weight:bold">
                                User:
                                <asp:Label ID="lblUserInfo" Font-Size="16px" ForeColor="#ff0000" Font-Bold="True"
                                    runat="server"></asp:Label></span>
                        </td>
                        <td align="left">
                            <span style="font-size:16px; font-weight:bold">
                                Current Rolesw</span>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <asp:ListBox ID="lstAllRoles" runat="server" Width="200px" Font-Size="14px" CssClass="padding10"></asp:ListBox></td>
                        <td align="center">
                            <asp:Button ID="btnAddRole" OnClick="btnAddRole_Click" runat="server" Text="Add ->"
                                Width="120px"></asp:Button>
                            <br />
                            <br />
                            <br />
                            <asp:Button ID="btnDeleteRole" OnClick="btnDeleteRole_Click" runat="server" Text="<- Remove"
                                Width="120px"></asp:Button>
                        </td>
                        <td align="left">
                            <asp:ListBox ID="lstUserRoles" runat="server" Width="200px" Font-Size="14px" CssClass="padding10"></asp:ListBox></td>
                    </tr>
                </table>
            </div>
        </div>
        <uc1:Footer ID="Footer1" runat="server"></uc1:Footer>
    </form>
</body>
</html>
<script runat="server">

	private void Page_Load(object sender, System.EventArgs e)
		{
			if (this.Request.QueryString["Username"] == null)
			{
				return;
			}

			String sUsername = this.Request.QueryString["Username"];

			DataSet ds = SamplePortal.Components.UserData.GetUserInfo(sUsername);
			if (ds.Tables[0].Rows.Count == 0)
			{
				return;
			}
			
			DataRow row = ds.Tables[0].Rows[0];

			if (! this.IsPostBack)
			{
				this.lblUserInfo.Text = sUsername;
				this.BindRoles(sUsername);
			}
		}

		private void BindRoles(String username)
		{
			this.lstAllRoles.DataSource = SamplePortal.Global.GetAllRoles();
			this.lstAllRoles.DataBind();

			this.lstUserRoles.DataSource = SamplePortal.Global.GetRolesOfUser(username);
			this.lstUserRoles.DataBind();
		}

		private void btnFinish_Click(object sender, System.EventArgs e)
		{
			this.Response.Redirect("UserAdmin.aspx");
		}

		private void btnAddRole_Click(object sender, System.EventArgs e)
		{
			if (this.lstAllRoles.SelectedIndex == -1)
			{
				return;
			}

			String sUsername = this.Request.QueryString["Username"];
			String sRoleName = this.lstAllRoles.SelectedValue;

			SamplePortal.Global.AddUserToRole(sRoleName, sUsername);

			this.BindRoles(sUsername);
		}

		private void btnDeleteRole_Click(object sender, System.EventArgs e)
		{
			if (this.lstUserRoles.SelectedIndex == -1)
			{
				return;
			}

			String sUsername = this.Request.QueryString["Username"];
			String sRoleName = this.lstUserRoles.SelectedValue;

			SamplePortal.Global.RemoveUserFromRole(sRoleName, sUsername);

			this.BindRoles(sUsername);
		}
</script>
