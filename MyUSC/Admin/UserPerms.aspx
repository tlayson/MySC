<%@ Page Title="" Language="C#" MasterPageFile="~/USC.Master" AutoEventWireup="true" CodeBehind="UserPerms.aspx.cs" Inherits="MyUSC.Admin.UserPerms" %>
<%@ MasterType VirtualPath="~/USC.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
	<style type="text/css">
		.auto-style1 {
			color: #FFFFFF;
			font-weight: bolder;
			padding: 5px;
			width: 550px;
		}
	</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
	<table style="width: 1060px; height: 650px; background-image: url('Images/Background.png')">
		<tr valign="top">
			<td class="tdInput">
				<table width="100%">
					<tr valign="top">
						<td class="tdInput" colspan="2">
							<asp:Label ID="Label1" runat="server" Text="User Permission and Settings" CssClass="lgSiteColorTxt"></asp:Label>
						</td>
					</tr>
					<tr valign="top">
						<td class="auto-style1">
							<asp:Label ID="Label2" runat="server" Text="Find user : "></asp:Label>
							<asp:TextBox ID="txtUsername" runat="server"></asp:TextBox>
							&nbsp;&nbsp;
							<asp:Label ID="lblUserNotFound" runat="server" Text="User not found!" ForeColor="Red" Visible="False"></asp:Label>
						</td>
						<td class="tdInput">
							<asp:Button ID="btnFindUser" runat="server" Text="Find User" OnClick="OnClickFindUser" />
						</td>
					</tr>
					<tr valign="top">
						<td class="auto-style1">
<!--
UserTypes { SuperUser = 1, Admin = 2, Contributor = 3, Trusted = 4, Normal = 5, Unverified = 6, Roster = 7, Anonymous = 8, Inactive = 9, Banned = 10 }
-->							
							<asp:Label ID="Label3" runat="server" Text="User type : "></asp:Label>
							<asp:DropDownList ID="ddlUserType" runat="server">
								<asp:ListItem Text="Admin" Value="2"></asp:ListItem>
								<asp:ListItem Text="Contributor" Value="3"></asp:ListItem>
								<asp:ListItem Text="Trusted" Value="4"></asp:ListItem>
								<asp:ListItem Text="Normal" Value="5" Selected="True"></asp:ListItem>
								<asp:ListItem Text="Unverified" Value="6"></asp:ListItem>
								<asp:ListItem Text="Roster" Value="7"></asp:ListItem>
								<asp:ListItem Text="Anonymous" Value="8"></asp:ListItem>
								<asp:ListItem Text="Inactive" Value="9"></asp:ListItem>
								<asp:ListItem Text="Banned" Value="10"></asp:ListItem>
							</asp:DropDownList>
						</td>
						<td class="tdInput">
							<asp:Button ID="btnUpdateUserType" runat="server" Text="Update Type" OnClick="OnClickUpdateType" />
						</td>
					</tr>
					<tr valign="top">
						<td class="auto-style1">
							
						</td>
						<td class="tdInput">
							
						</td>
					</tr>
					<tr valign="top">
						<td class="auto-style1">
							
						</td>
						<td class="tdInput">
							
						</td>
					</tr>
					<tr valign="top">
						<td class="auto-style1">
							
						</td>
						<td class="tdInput">
							
						</td>
					</tr>
					<tr valign="top">
						<td class="auto-style1">
							
						</td>
						<td class="tdInput">
							<asp:Button ID="btnBack" runat="server" Text="Back" OnClick="OnClickBack" />
						</td>
					</tr>
				</table>
			</td>
		</tr>
	</table>
</asp:Content>
