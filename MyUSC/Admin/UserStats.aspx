<%@ Page Title="" Language="C#" MasterPageFile="~/USC.Master" AutoEventWireup="true" CodeBehind="UserStats.aspx.cs" Inherits="MyUSC.Admin.UserStats" %>
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
							<asp:Label ID="Label1" runat="server" Text="Basic user statistics" CssClass="lgSiteColorTxt"></asp:Label>
						</td>
					</tr>
					<tr valign="top">
						<td class="tdInput">
							<asp:Label ID="Label2" runat="server" Text="Total registered users : " CssClass="smSiteColorTxt"></asp:Label>
							<asp:Label ID="lblTotalUsers" runat="server" Text="Label"></asp:Label>
						</td>
						<td class="tdInput">
							
						</td>
						<td class="tdInput">
							
						</td>
					</tr>
					<tr valign="top">
						<td class="tdInput">
							<asp:Label ID="Label3" runat="server" Text="Unique users : " CssClass="smSiteColorTxt"></asp:Label>
							<asp:Label ID="lblUniqueUsers" runat="server" Text="Label"></asp:Label>
						</td>
						<td class="tdInput">
							
						</td>
						<td class="tdInput">
							
						</td>
					</tr>
					<tr valign="top">
						<td class="tdInput">
							<asp:Label ID="Label4" runat="server" Text="Active last 30 days : " CssClass="smSiteColorTxt"></asp:Label>
							<asp:Label ID="lblActiveUsers30" runat="server" Text="Label"></asp:Label>
						</td>
						<td class="tdInput">
							
						</td>
						<td class="tdInput">
							
						</td>
					</tr>
					<tr valign="top">
						<td class="tdInput">
							<asp:Label ID="Label6" runat="server" Text="Registered last 30 days : " CssClass="smSiteColorTxt"></asp:Label>
							<asp:Label ID="lblRegistered30" runat="server" Text="Label"></asp:Label>
						</td>
						<td class="tdInput">
							
						</td>
						<td class="tdInput">
							
						</td>
					</tr>
					<tr valign="top">
						<td class="tdInput">
							<asp:Label ID="Label5" runat="server" Text="Active last 90 days : " CssClass="smSiteColorTxt"></asp:Label>
							<asp:Label ID="lblActiveUsers90" runat="server" Text="Label"></asp:Label>
						</td>
						<td class="tdInput">
							
						</td>
						<td class="tdInput">
							
						</td>
					</tr>
					<tr valign="top">
						<td class="tdInput">
							<asp:Label ID="Label8" runat="server" Text="Registered last 90 days : " CssClass="smSiteColorTxt"></asp:Label>
							<asp:Label ID="lblRegistered90" runat="server" Text="Label"></asp:Label>
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
