<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ViewEventDetails.ascx.cs" Inherits="MyUSC.Classes.MyOrg.ViewEventDetails" %>
<link href="/Styles/Site.css" rel="stylesheet" type="text/css" />
<table style="width: 650px" class="tblDlgControlOuter">
	<tr class="trDlgControlTitle">
		<td class="tdDlgControlNormal" colspan="2">
			<asp:Label ID="lblTitle" runat="server" Text="Event Details"></asp:Label>
		</td>
	</tr>
	<tr class="trDlgControlNormal">
		<td class="tdDlgControlNormal" colspan="2">
			<asp:Label ID="lblOrgName" runat="server" Text="Org Name"></asp:Label>
		</td>
	</tr>
	<tr class="trDlgControlNormal">
		<td class="tdDlgControlLabel">
			<asp:Label ID="lblSeasonX" runat="server" Text="Season : "></asp:Label>&nbsp;
		</td>
		<td class="tdDlgControlNormal">
			<asp:Label ID="lblSeason" runat="server" Text="Season Name"></asp:Label>&nbsp;
		</td>
	</tr>
	<tr class="trDlgControlNormal">
		<td class="tdDlgControlNormal">
			<asp:Label ID="lblEventNameX" runat="server" Text="Event Name : "></asp:Label>&nbsp;
		</td>
		<td class="tdDlgControlNormal">
			<asp:Label ID="lblEventName" runat="server" Text="Event Name"></asp:Label>&nbsp;
		</td>
	</tr>
	<tr class="trDlgControlNormal">
		<td class="tdDlgControlNormal">
			<asp:Label ID="Label1" runat="server" Text="Event Type : "></asp:Label>&nbsp;
		</td>
		<td class="tdDlgControlNormal">
			<asp:Label ID="lblEventType" runat="server" Text="Event Type"></asp:Label>&nbsp;
		</td>
	</tr>
	<tr class="trDlgControlNormal">
		<td class="tdDlgControlNormal">
			<asp:Label ID="Label2" runat="server" Text="Location : "></asp:Label>&nbsp;
		</td>
		<td class="tdDlgControlNormal">
			<asp:Label ID="Label3" runat="server" Text="Link to venue"></asp:Label>&nbsp;
		</td>
	</tr>
	<tr class="trDlgControlNormal">
		<td class="tdDlgControlNormal">
			<asp:Label ID="Label4" runat="server" Text="Date : "></asp:Label>&nbsp;
		</td>
		<td class="tdDlgControlNormal">
			<asp:Label ID="Label5" runat="server" Text="Event Date"></asp:Label>&nbsp;
		</td>
	</tr>
	<tr class="trDlgControlNormal">
		<td class="tdDlgControlNormal">
			<asp:Label ID="Label12" runat="server" Text="Time : "></asp:Label>&nbsp;
		</td>
		<td class="tdDlgControlNormal">
			<asp:Label ID="Label13" runat="server" Text="Event Time"></asp:Label>&nbsp;
		</td>
	</tr>
	<tr class="trDlgControlNormal">
		<td class="tdDlgControlNormal">
			<asp:Label ID="Label6" runat="server" Text="Opponent : "></asp:Label>&nbsp;
		</td>
		<td class="tdDlgControlNormal">
			<asp:Label ID="Label7" runat="server" Text="Opponent"></asp:Label>&nbsp;
		</td>
	</tr>
	<tr class="trDlgControlNormal">
		<td class="tdDlgControlNormal">
			<asp:Label ID="Label8" runat="server" Text="Home/Away : "></asp:Label>&nbsp;
		</td>
		<td class="tdDlgControlNormal">
			<asp:Label ID="Label9" runat="server" Text="Home/Away"></asp:Label>&nbsp;
		</td>
	</tr>
	<tr class="trDlgControlNormal">
		<td class="tdDlgControlNormal">
			<asp:Label ID="Label10" runat="server" Text="Uniform : "></asp:Label>&nbsp;
		</td>
		<td class="tdDlgControlNormal">
			<asp:Label ID="Label11" runat="server" Text="Uniform"></asp:Label>&nbsp;
		</td>
	</tr>
	<tr class="trDlgControlNormal">
		<td class="tdDlgControlNormal">
			<asp:Label ID="Label14" runat="server" Text="Event Result : "></asp:Label>&nbsp;
		</td>
		<td class="tdDlgControlNormal">
			<asp:Label ID="Label15" runat="server" Text="Result"></asp:Label>&nbsp;
		</td>
	</tr>
	<tr class="trDlgControlNormal">
		<td class="tdDlgControlNormal" colspan="2">
			<asp:Label ID="Label16" runat="server" Text="Comments : "></asp:Label>&nbsp;
		</td>
	</tr>
	<tr class="trDlgControlNormal">
		<td class="tdDlgControlNormal" colspan="2">
			<asp:Literal ID="Literal1" runat="server">
				Comments in text or HTML format
			</asp:Literal>
		</td>
	</tr>
	<tr class="trDlgControlNormal">
		<td class="tdDlgControlNormal">
			<asp:Label ID="Label20" runat="server" Text="Website : "></asp:Label>&nbsp;
		</td>
		<td class="tdDlgControlNormal">
			<asp:Label ID="Label21" runat="server" Text="Event URL"></asp:Label>&nbsp;
		</td>
	</tr>
	<tr class="trDlgControlNormal">
		<td class="tdDlgControlNormal">
			<asp:Label ID="Label17" runat="server" Text="Response requested : "></asp:Label>&nbsp;
		</td>
		<td class="tdDlgControlNormal">
			<asp:Label ID="Label18" runat="server" Text="Yes/No"></asp:Label>&nbsp;
		</td>
	</tr>
	<tr class="trDlgControlNormal">
		<td class="tdCenter" colspan="2">
			<asp:Button ID="btnOK" runat="server" Text="OK" CssClass="btnOK" OnClick="OnClickOK" />
		</td>
	</tr>
</table>