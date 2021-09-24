<%@ Page Title="" Language="C#" MasterPageFile="~/MyTeams/OrgMaster.master" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="MyUSC.MyTeams.Home" %>
<%@ MasterType VirtualPath="~/MyTeams/OrgMaster.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="OrgHeadContent" runat="server">
	<script type="text/javascript" src="~/js/GA.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="OrgContent" runat="server">
	<table>
		<tr>
			<td>
				<table style="width: 600px; vertical-align: top;">
					<tr valign="top">
						<td class="tdImage">
							<asp:Image ID="imgOrgLogo" runat="server" CssClass="tdImage" ImageUrl="~/Images/NoPhoto.JPG" />
						</td>
						<td class="tdInput">
							<asp:Table ID="tblOrgDescription" runat="server"></asp:Table>
						</td>
					</tr>
					<tr valign="top" style="height: 45px">
						<td class="tdInput" colspan="2">

							<asp:Label ID="lblNewsHeading" runat="server" Text="News and announcements" CssClass="medSiteColorTxt"></asp:Label>

						</td>
					</tr>
					<tr valign="top" style="height: 45px">
						<td class="tdInput" colspan="2">
							<asp:Label ID="lblNoNews" runat="server" Text="No recent news items."></asp:Label>
							<asp:Literal ID="litOrgNews" runat="server"></asp:Literal>

						</td>
					</tr>
					<tr valign="top" style="height: 45px">
						<td class="tdInput"  colspan="2">

							<asp:Label ID="lblScheduleHeading" runat="server" Text="Schedule" CssClass="medSiteColorTxt"></asp:Label>

						</td>
					</tr>
					<tr valign="top" style="height: 45px">
						<td class="tdInput"  colspan="2">
							<asp:Label ID="lblNoSchedule" runat="server" Text="No upcoming scheduled events"></asp:Label>
							<asp:Table ID="tblOrgSchedule" runat="server"></asp:Table>
						</td>
					</tr>
					<tr valign="top" style="height: 45px">
						<td class="tdInput"  colspan="2">

							<asp:Label ID="lblTeamMessagesHeading" runat="server" Text="Team Messages" CssClass="medSiteColorTxt"></asp:Label>

						</td>
					</tr>
					<tr valign="top" style="height: 45px">
						<td class="tdInput"  colspan="2">
							<asp:Label ID="lblNoMessages" runat="server" Text="No recent posts"></asp:Label>
							<asp:Table ID="tblOrgMessages" runat="server"></asp:Table>
						</td>
					</tr>
				</table>
			</td>
			<td valign="top">
				<table style="width: 200px; vertical-align: top;">
					<tr>
						<td>
							<asp:Label ID="lblAffiliatesHeading" runat="server" Text="Affiliates" CssClass="medSiteColorTxt"></asp:Label>
						</td>
					</tr>
				</table>
				<asp:Table ID="tblAffilates" runat="server">
					
				</asp:Table>
			</td>
		</tr>
	</table>
</asp:Content>
