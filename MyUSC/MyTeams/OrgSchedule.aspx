<%@ Page Title="" Language="C#" MasterPageFile="~/MyTeams/OrgMaster.master" AutoEventWireup="true" CodeBehind="OrgSchedule.aspx.cs" Inherits="MyUSC.MyTeams.OrgSchedule" %>
<%@ MasterType VirtualPath="~/MyTeams/OrgMaster.Master" %>
<%@ Register Src="~/Classes/MyOrg/EventDetails.ascx" TagPrefix="MSC" TagName="EventDetails" %>
<%@ Register Src="~/Classes/MyOrg/EventResponseDlg.ascx" TagPrefix="MSC" TagName="TBSCResponseDlg" %>
<asp:Content ID="Content1" ContentPlaceHolderID="OrgHeadContent" runat="server">
	<script type="text/javascript" src="~/js/GA.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="OrgContent" runat="server">
	<asp:Panel ID="pnlAdmin" runat="server">
		<table class="tblMyTeamsPage">
			<tr class="trNormal">
				<td class="tdInput">
					<asp:Label ID="Label13" runat="server" Text="Admin Options" CssClass="medSiteColorTxt"></asp:Label>
				</td>
				<td class="tdRight">
					<MSC:TBSCButton ID='btnAddEvent' runat="server" OnClick="OnClickAddEvent" Text="Add Event" />
				</td>
			</tr>
		</table>
	</asp:Panel>
	<div class="xlgSiteColorTxt">
		<MSC:TBSCLabel ID="TBSCLabel3" runat="server" UserValue="-1">Schedule</MSC:TBSCLabel>
	</div>
	<MSC:TBSCPanel ID="pnlEventDetails" runat="server" Visible="False">
	</MSC:TBSCPanel>
	<MSC:TBSCPanel ID='pnlSchedule' runat="server">
		<table class="tblNormal">
			<tr>
				<td class = "tdRight">
					<asp:CheckBox ID="chkHideAll" runat="server" Text="Hide all details" AutoPostBack="True" CssClass="medNormalTxt" />
				</td>
			</tr>
		</table>
		<asp:Table ID="tblEvents" runat="server" CssClass="tblNormal">
		
		</asp:Table>
	</MSC:TBSCPanel>

</asp:Content>
