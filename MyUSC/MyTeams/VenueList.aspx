<%@ Page Title="" Language="C#" MasterPageFile="~/MyTeams/OrgMaster.master" AutoEventWireup="true" CodeBehind="VenueList.aspx.cs" Inherits="MyUSC.MyTeams.VenueList" %>
<%@ MasterType VirtualPath="~/MyTeams/OrgMaster.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="OrgHeadContent" runat="server">
	<script type="text/javascript" src="~/js/GA.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="OrgContent" runat="server">
	<table style="width: 800px; vertical-align: top;">
		<tr valign="top">
			<td class="trAdmin">

			</td>
			<td class="tdInput">

			</td>
			<td class="tdRight">
				<asp:Button ID="btnFindVenue" runat="server" Text="Find Venue" OnClick="OnClickFindVenue" /> &nbsp;&nbsp;
				<asp:Button ID="btnNewVenue" runat="server" Text="New Venue" OnClick="OnClickNewVenue" Visible="False" CommandArgument="Test" />
			</td>
		</tr>
	</table>
	<asp:Table ID="tblVenueList" runat="server" CssClass="tblNormal">
	</asp:Table>
</asp:Content>
