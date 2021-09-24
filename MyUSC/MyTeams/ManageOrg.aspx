<%@ Page Title="" Language="C#" MasterPageFile="~/MyTeams/OrgMaster.master" AutoEventWireup="true" CodeBehind="ManageOrg.aspx.cs" Inherits="MyUSC.MyTeams.ManageOrg" %>
<%@ MasterType VirtualPath="~/MyTeams/OrgMaster.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="OrgHeadContent" runat="server">
	<script type="text/javascript" src="~/js/GA.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="OrgContent" runat="server">
	<table style="width: 800px; vertical-align: top;">
		<tr valign="top" style="height: 45px">
			<td class="tdInput">
				<asp:Label ID="Label1" runat="server" Text="Details" CssClass="xlgNormalTxt"></asp:Label>
			</td>
			<td class="tdInput">
				<asp:Button ID="Button1" runat="server" Text="Edit" OnClick="OnClickEditDetails" />
			</td>
			<td class="tdInput">

			</td>
		</tr>
		<tr valign="top" style="height: 45px">
			<td class="tdInput">
				Manage basic details about your organization, such as name and contact information.
			</td>
			<td class="tdInput">

			</td>
			<td class="tdInput">

			</td>
		</tr>
		<tr valign="top" style="height: 45px">
			<td class="tdInput">
				<asp:Label ID="Label2" runat="server" Text="News and Announcements" CssClass="xlgNormalTxt"></asp:Label>
			</td>
			<td class="tdInput">
				<asp:Button ID="Button5" runat="server" Text="Edit" OnClick="OnClickEditNews" />
			</td>
			<td class="tdInput">

			</td>
		</tr>
		<tr valign="top" style="height: 45px">
			<td class="tdInput">
				Update the latest news and announcements.
			</td>
			<td class="tdInput">

			</td>
			<td class="tdInput">

			</td>
		</tr>
		<tr valign="top" style="height: 45px">
			<td class="tdInput">
				<asp:Label ID="Label3" runat="server" Text="Options" CssClass="xlgNormalTxt"></asp:Label>
			</td>
			<td class="tdInput">
				<asp:Button ID="Button2" runat="server" Text="Edit" OnClick="OnClickEditOptions" />
			</td>
			<td class="tdInput">

			</td>
		</tr>
		<tr valign="top" style="height: 45px">
			<td class="tdInput">
				Change your teams logo, manage which pages are displayed and who has access to them.
			</td>
			<td class="tdInput">

			</td>
			<td class="tdInput">

			</td>
		</tr>
		<tr valign="top" style="height: 45px">
			<td class="tdInput">
				<asp:Label ID="Label8" runat="server" Text="Permissions" CssClass="xlgNormalTxt"></asp:Label>
			</td>
			<td class="tdInput">
				<asp:Button ID="Button3" runat="server" Text="Edit" OnClick="OnClickEditPermissions" />
			</td>
			<td class="tdInput">

			</td>
		</tr>
		<tr valign="top" style="height: 45px">
			<td class="tdInput">
				Manage who has access to your site.
			</td>
			<td class="tdInput">

			</td>
			<td class="tdInput">

			</td>
		</tr>
		<tr valign="top" style="height: 45px">
			<td class="tdInput">
				<asp:Label ID="Label7" runat="server" Text="Affiliates" CssClass="xlgNormalTxt"></asp:Label>
			</td>
			<td class="tdInput">
				<asp:Button ID="Button4" runat="server" Text="Edit" OnClick="OnClickEditAffiliates"/>
			</td>
			<td class="tdInput">

			</td>
		</tr>
		<tr valign="top">
			<td class="tdInput">
				Associate your organization with others.
			</td>
			<td class="tdInput">

			</td>
			<td class="tdInput">

			</td>
		</tr>
	</table>
</asp:Content>
