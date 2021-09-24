<%@ Import Namespace="System.Data.SqlClient" %>
<%@ Import Namespace="System.Data" %>
<%@ Register TagPrefix="uc1" TagName="Footer" Src="Footer.ascx" %>
<%@ Register TagPrefix="uc1" TagName="TopBanner" Src="Banner.ascx" %>
<%@ Page language="c#" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
	<head>
		<title>User's Info</title>
		<link rel="stylesheet" href="sample.css" type="text/css" />
	</head>
	<body>
        <form id="Form1" method="post" runat="server">
            <uc1:TopBanner ID="TopBanner1" runat="server"></uc1:TopBanner>
            <div class="infobox" style="width: 700px; margin: 30px auto">
                <h2>
                    User Profile
                </h2>
                <div class="padding10">
                    <table cellspacing="0" border="0" cellpadding="8" style="width: 90%">
                        <tr>
                            <td align="right" style="width:160px">
                                Username:</td>
                            <td>
                                <asp:Label ID="lblUsername" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                Gender:</td>
                            <td>
                                <asp:Label ID="lblSex" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td align="right">
                                Age:</td>
                            <td>
                                <asp:Label ID="lblAge" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td align="right">
                                Location:</td>
                            <td>
                                <asp:Label ID="lblLocation" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                Occupation:</td>
                            <td>
                                <asp:Label ID="lblOccupation" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td align="right">
                                Interests:</td>
                            <td>
                                <asp:Label ID="lblInterests" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                Joined:</td>
                            <td>
                                <asp:Label ID="lblJoined" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                Last login:</td>
                            <td>
                                <asp:Label ID="lblLastLogin" runat="server"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
            <uc1:Footer ID="Footer1" runat="server"></uc1:Footer>
        </form>
		
		
		<script runat="server">
		
		private void Page_Load(object sender, System.EventArgs e)
		{
			string UserName = "";
			if (this.Request.QueryString["Username"] == null)
			{
				return;
			}
			
			UserName = Request.QueryString["Username"];

			DataSet ds = SamplePortal.Components.UserData.GetUserInfo(UserName);

			if (ds.Tables[0].Rows.Count == 0)
			{
				return;
			}

			DataRow row = ds.Tables[0].Rows[0];
			
			this.lblUsername.Text = row["Username"].ToString();
			this.lblLocation.Text = row["Location"].ToString();
			this.lblOccupation.Text = row["Occupation"].ToString();
			this.lblLastLogin.Text = row["LastLoginTime"].ToString();
			this.lblJoined.Text = row["DateCreated"].ToString();
			this.lblAge.Text = row["Age"].ToString();
			this.lblSex.Text = ((Boolean) row["Gender"]) ? "Male" : "Female";
		}

		</script>
	</body>
</html>
