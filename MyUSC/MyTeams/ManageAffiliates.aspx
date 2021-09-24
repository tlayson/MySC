<%@ Page Title="" Language="C#" MasterPageFile="~/MyTeams/OrgMaster.master" AutoEventWireup="true" CodeBehind="ManageAffiliates.aspx.cs" Inherits="MyUSC.MyTeams.ManageAffiliates" %>
<%@ MasterType VirtualPath="~/MyTeams/OrgMaster.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="OrgHeadContent" runat="server">
	<script type="text/javascript" src="~/js/GA.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="OrgContent" runat="server">
	<table style="width: 800px; vertical-align: top;">
		<tr valign="top" style="height: 45px">
			<td class="tdInput">
				<asp:Label ID="Label1" runat="server" Text="Current Affiliates" CssClass="medSiteColorTxt"></asp:Label>
			</td>
			<td class="tdInput" style="width: 50px">
				<asp:Button ID="Button2" runat="server" Text="Find Affiliates" OnClick="OnClickSearchAffiliates" />
			</td>
			<td class="tdInput" style="width: 50px">
				<asp:Button ID="Button1" runat="server" Text="New Organization" OnClick="OnClickNewOrg"/>
			</td>
		</tr>
		<tr valign="top" style="height: 45px">
			<td class="tdInput" colspan="3">
				<asp:Table ID="tblAffilates" runat="server"></asp:Table>
			</td>
		</tr>
	</table>
</asp:Content>
