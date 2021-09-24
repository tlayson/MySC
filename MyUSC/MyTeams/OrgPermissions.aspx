<%@ Page Title="" Language="C#" MasterPageFile="~/MyTeams/OrgMaster.master" AutoEventWireup="true" CodeBehind="OrgPermissions.aspx.cs" Inherits="MyUSC.MyTeams.OrgPermissions" %>
<%@ MasterType VirtualPath="~/MyTeams/OrgMaster.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="OrgHeadContent" runat="server">
	<script type="text/javascript" src="~/js/GA.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="OrgContent" runat="server">
	<table style="width: 800px; vertical-align: top;">
		<tr valign="top" style="height: 45px">
			<td class="tdInput">
				<table width="100%">
					<tr valign="top">
						<td class="tdCenter">
							<MSC:TBSCButton ID='btnInviteMembers' runat="server" Text="Invite Someone" OnClick="OnClickInvite" />
						</td>
					</tr>
				</table>
			</td>
		</tr>
		<tr valign="top" style="height: 45px">
			<td class="tdInput">
				<table width="100%">
					<tr valign="top">
						<td class="tdInput">
							<asp:Table ID="tblMembers" runat="server" Width="100%">
							</asp:Table>
						</td>
					</tr>
				</table>
			</td>
		</tr>
	</table>
</asp:Content>
