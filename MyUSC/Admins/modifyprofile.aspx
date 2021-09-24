<%@ Page language="c#" %>
<%@ Register TagPrefix="uc1" TagName="Footer" Src="~/Footer.ascx" %>
<%@ Register TagPrefix="uc1" TagName="TopBanner" Src="AdminBanner.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ModifyUserProfile" Src="../ModifyUserProfile.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
	<head>
		<title>Modify User's Profile</title>
		<link href="../Sample.css" type="text/css" rel="stylesheet" />
	</head>
	<body>
        <form id="Form1" method="post" runat="server">
            <uc1:TopBanner ID="TopBanner1" runat="server"></uc1:TopBanner>
            <div class="infobox" style="width: 700px; margin: 30px auto">
                <h2>
                    Edit User Profile: <asp:Label ForeColor="red" Font-Bold="true" Font-Size="13px" ID="lblUsername" runat="server"></asp:Label>
                </h2>
                <div class="padding10">
                    <uc1:ModifyUserProfile ID="_modifyUserProfile" runat="server"></uc1:ModifyUserProfile>
                </div>
            </div>
            <uc1:Footer ID="Footer1" runat="server"></uc1:Footer>
        </form>
	</body>
</html>

<script runat="server">
	private void Page_Load(object sender, System.EventArgs e)
	{
		_modifyUserProfile.ParentPageRelativePath = "../";
		_modifyUserProfile.AfterUpdate += new EventHandler(_modifyUserProfile_AfterUpdate);
		_modifyUserProfile.AfterCancel += new EventHandler(_modifyUserProfile_AfterCancel);

		if (this.Request.QueryString["Username"] == null)
		{
			return;
		}

		String sUsername = this.Request.QueryString["Username"];

		_modifyUserProfile.Username = sUsername;
		this.lblUsername.Text = sUsername;
	}
		
	private void _modifyUserProfile_AfterUpdate(object sender, EventArgs e)
	{
		this.Response.Redirect("UserAdmin.aspx");
	}

	private void _modifyUserProfile_AfterCancel(object sender, EventArgs e)
	{
		this.Response.Redirect("UserAdmin.aspx");
	}
</script>
