<%@ Page Title="" Language="C#" MasterPageFile="~/USC.Master" AutoEventWireup="true" CodeBehind="GlobalManagement.aspx.cs" Inherits="MyUSC.Admin.Super.GlobalManagement" %>
<%@ MasterType VirtualPath="~/USC.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
	<style type="text/css">
		.auto-style1 {
			color: #FFFFFF;
			font-weight: bolder;
			padding: 5px;
			width: 380px;
		}
	</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
	<table style="width: 1060px; height: 650px; background-image: url('Images/Background.png')">
		<tr valign="top">
			<td class="tdInput">
				<table width="100%">
					<tr valign="top">
						<td class="tdInput" colspan="3">
							<asp:Label ID="Label1" runat="server" Text="Manage Global Site Settings" CssClass="lgSiteColorTxt"></asp:Label>
						</td>
					</tr>
					<tr valign="top">
						<td class="auto-style1">
							<asp:Label ID="Label2" runat="server" Text="Reset Cache  (Causes global caches to reset)" CssClass="medSiteColorTxt"></asp:Label>
						</td>
						<td class="tdInput">
							<asp:Label ID="Label3" runat="server" Text="Manage Global Settings" CssClass="medSiteColorTxt"></asp:Label>
						</td>
						<td class="tdInput">
							
						</td>
					</tr>
					<tr valign="top">
						<td class="auto-style1">
							<asp:Button ID="Button1" runat="server" Text="Reset All" OnClick="OnClickResetAll" />
						</td>
						<td class="tdInput">
							
						</td>
						<td class="tdInput">
							
						</td>
					</tr>
					<tr valign="top">
						<td class="auto-style1">
							<asp:Button ID="Button2" runat="server" Text="Reset Users" OnClick="OnClickResetUsers" />
						</td>
						<td class="tdInput">
							
							<asp:HyperLink ID="HyperLink5" runat="server" NavigateUrl="~/Admin/UserPerms.aspx">Users</asp:HyperLink>
							
						</td>
						<td class="tdInput">
							
						</td>
					</tr>
					<tr valign="top">
						<td class="auto-style1">
							<asp:Button ID="Button3" runat="server" Text="Reset Sports" OnClick="OnClickResetSports" />
						</td>
						<td class="tdInput">
							<asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/Admin/Super/NewsMenu.aspx">News Menu Items</asp:HyperLink>
						</td>
						<td class="tdInput">
							
						</td>
					</tr>
					<tr valign="top">
						<td class="auto-style1">
							<asp:Button ID="Button4" runat="server" Text="Reset Site Settings" OnClick="OnClickResetSiteSettings" />
						</td>
						<td class="tdInput">
							<asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl="~/Admin/Super/ManageSiteSettings.aspx">Site Settings</asp:HyperLink>
						</td>
						<td class="tdInput">
							
						</td>
					</tr>
					<tr valign="top">
						<td class="auto-style1">
							<asp:Button ID="Button5" runat="server" Text="Reset Localization" OnClick="OnClickResetLocalization" />
						</td>
						<td class="tdInput">
							<asp:HyperLink ID="HyperLink3" runat="server" NavigateUrl="~/Admin/Super/ManageLocalization.aspx">Localization</asp:HyperLink>
						</td>
						<td class="tdInput">
							
						</td>
					</tr>
					<tr valign="top">
						<td class="auto-style1">
							<asp:Button ID="Button6" runat="server" Text="Reset RSS" OnClick="OnClickResetRSS" />
						</td>
						<td class="tdInput">
							<asp:HyperLink ID="HyperLink4" runat="server" NavigateUrl="~/Admin/Super/RSSFeeds.aspx">RSS Feeds</asp:HyperLink>
						</td>
						<td class="tdInput">
							
						</td>
					</tr>
				</table>
			</td>
		</tr>
	</table>
</asp:Content>
