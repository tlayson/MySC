<%@ Page Title="" Language="C#" MasterPageFile="~/USC.Master" AutoEventWireup="true" CodeBehind="AdminMain.aspx.cs" Inherits="MyUSC.Admin.AdminMain" %>
<%@ MasterType VirtualPath="~/USC.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
	<table style="width: 1060px; height: 650px; background-image: url('Images/Background.png')">
		<tr valign="top">
			<td class="tdInput">
				<table width="100%">
					<tr valign="top">
						<td class="tdInput" colspan="3">
							<asp:Label ID="Label1" runat="server" Text="Site Administration" CssClass="lgSiteColorTxt"></asp:Label>
						</td>
					</tr>
					<tr valign="top">
						<td class="tdInput">
							<asp:Label ID="Label3" runat="server" Text="Users" CssClass="medSiteColorTxt"></asp:Label>
						</td>
						<td class="tdInput">
							<asp:Label ID="Label4" runat="server" Text="Metrics" CssClass="medSiteColorTxt"></asp:Label>
						</td>
						<td class="tdInput">
							<asp:Label ID="Label5" runat="server" Text="TBD" CssClass="medSiteColorTxt"></asp:Label>
						</td>
					</tr>
					<tr valign="top">
						<td class="tdInput">
							<asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl="~/Admin/UserStats.aspx" ForeColor="Red">User Statistics</asp:HyperLink>
						</td>
						<td class="tdInput">
							<asp:HyperLink ID="HyperLink3" runat="server" NavigateUrl="~/Admin/PageViewMetrics.aspx" ForeColor="Red">Page View Metrics</asp:HyperLink>
 						</td>
						<td class="tdInput">

						</td>
					</tr>
					<tr valign="top">
						<td class="tdInput">
							
						</td>
						<td class="tdInput">
							
						</td>
						<td class="tdInput">
							
						</td>
					</tr>
					<tr valign="top">
						<td class="tdInput">
							
						</td>
						<td class="tdInput">
							
						</td>
						<td class="tdInput">
							
						</td>
					</tr>
					<tr valign="top">
						<td class="tdInput">
							
						</td>
						<td class="tdInput">
							
						</td>
						<td class="tdInput">
							
						</td>
					</tr>
					<tr valign="top">
						<td class="tdInput">
							
						</td>
						<td class="tdInput">
							
						</td>
						<td class="tdInput">
							
						</td>
					</tr>
					<tr valign="top">
						<td class="tdInput" colspan="3">
							<asp:Label ID="Label2" runat="server" Text="System Management" CssClass="lgSiteColorTxt"></asp:Label>
						</td>
					</tr>
					<tr valign="top">
						<td class="tdInput">
							<asp:HyperLink ID="hlUserType" runat="server" NavigateUrl="~/Admin/Super/GlobalManagement.aspx" Enabled="False" ForeColor="Red" Visible="False">Global Settings</asp:HyperLink>
						</td>
						<td class="tdInput">

						</td>
						<td class="tdInput">
							
						</td>
					</tr>
					<tr valign="top">
						<td class="tdInput">
							
						</td>
						<td class="tdInput">
							
						</td>
						<td class="tdInput">
							
						</td>
					</tr>
					<tr valign="top">
						<td class="tdInput">
							
						</td>
						<td class="tdInput">
							
						</td>
						<td class="tdInput">
							
						</td>
					</tr>
				</table>
			</td>
		</tr>
	</table>
</asp:Content>
