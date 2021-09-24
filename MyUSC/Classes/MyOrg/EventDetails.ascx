<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EventDetails.ascx.cs" Inherits="MyUSC.Classes.MyOrg.EventDetails" %>
<%@ Register Src="~/Classes/DateSelect.ascx" TagPrefix="uc1" TagName="DateSelect" %>
<link href="/Styles/Site.css" rel="stylesheet" type="text/css" />
<table style="width: 650px" class="tblDlgControlOuter">
	<tr class="trDlgControlTitle">
		<td class="tdDlgControlNormal" colspan="2">
			<asp:Label ID="lblTitle" runat="server" Text="Event"></asp:Label>
		</td>
	</tr>
	<tr class="trDlgControlNormal">
		<td class="tdDlgControlNormal" colspan="2">
			<asp:Label ID="lblOrgName" runat="server" Text="Org Name"></asp:Label>
		</td>
	</tr>
	<tr class="trDlgControlNormal">
		<td class="tdDlgControlNormal">
			<asp:Label ID="lblSeason" runat="server" Text="Season : "></asp:Label>&nbsp;
		</td>
		<td class="tdDlgControlNormal">
			<asp:DropDownList ID="ddlSeason" runat="server"></asp:DropDownList>&nbsp;&nbsp;&nbsp;
			<asp:LinkButton ID="btnNewSeason" runat="server">Create New Season</asp:LinkButton>
		</td>
	</tr>
	<tr class="trDlgControlNormal">
		<td class="tdDlgControlNormal">
			<asp:Label ID="lblEventName" runat="server" Text="Event Name : "></asp:Label>&nbsp;
		</td>
		<td class="tdDlgControlNormal">
			<asp:TextBox ID="txtEventName" runat="server" MaxLength="250" Width="400px"></asp:TextBox>
		</td>
	</tr>
	<tr class="trDlgControlNormal">
		<td class="tdDlgControlNormal">
			<asp:Label ID="Label1" runat="server" Text="Event Type : "></asp:Label>&nbsp;
		</td>
		<td class="tdDlgControlNormal">
			<asp:DropDownList ID="ddlEventType" runat="server"></asp:DropDownList>
		</td>
	</tr>
	<tr class="trDlgControlNormal">
		<td class="tdDlgControlNormal">
			<asp:Label ID="Label8" runat="server" Text="Location : "></asp:Label>&nbsp;
		</td>
		<td class="tdDlgControlNormal">
			<asp:DropDownList ID="ddlVenue" runat="server"></asp:DropDownList>&nbsp;&nbsp;
			<asp:TextBox ID="txtAltLocation" runat="server" Width="200" MaxLength="50" Visible="False" ToolTip="Text to display in the schedule instead of the full address."></asp:TextBox>
			<asp:CheckBox ID="chkAltLocation" runat="server" Text="Display Alternate" AutoPostBack="True" ToolTip="Text to display in the schedule instead of the full address." />
		</td>
	</tr>
	<tr class="trDlgControlNormal">
		<td class="tdDlgControlNormal">
		</td>
		<td class="tdDlgControlNormal">
			<asp:LinkButton ID="btnNewLocation" runat="server">Create New Location</asp:LinkButton>
		</td>
	</tr>
	<tr class="trDlgControlNormal">
		<td class="tdDlgControlNormal">
			<asp:Label ID="Label2" runat="server" Text="Opponent : "></asp:Label>&nbsp;
		</td>
		<td class="tdDlgControlNormal">
			<uc1:DateSelect runat="server" ID="dsEventDate" />
		</td>
	</tr>
	<tr class="trDlgControlNormal">
		<td class="tdDlgControlNormal">
			<asp:Label ID="Label3" runat="server" Text="Event Time : "></asp:Label>&nbsp;
		</td>
		<td class="tdDlgControlNormal">
			<asp:TextBox ID="txtTime" runat="server" MaxLength="20" Width="200px"></asp:TextBox>
		</td>
	</tr>
	<tr class="trDlgControlNormal">
		<td class="tdDlgControlNormal">
			<asp:Label ID="Label4" runat="server" Text="Opponent : "></asp:Label>&nbsp;
		</td>
		<td class="tdDlgControlNormal">
			<asp:TextBox ID="txtOpponent" runat="server" MaxLength="50" Width="200px"></asp:TextBox>
		</td>
	</tr>
	<tr class="trDlgControlNormal">
		<td class="tdDlgControlNormal">
			<asp:Label ID="Label5" runat="server" Text="Home/Away : "></asp:Label>&nbsp;
		</td>
		<td class="tdDlgControlNormal">
			<asp:DropDownList ID="ddlHomeAway" runat="server">
				<asp:ListItem Value="0">Select</asp:ListItem>
				<asp:ListItem>Home</asp:ListItem>
				<asp:ListItem>Away</asp:ListItem>
			</asp:DropDownList>
		</td>
	</tr>
	<tr class="trDlgControlNormal">
		<td class="tdDlgControlNormal">
			<asp:Label ID="Label6" runat="server" Text="Uniform : "></asp:Label>&nbsp;
		</td>
		<td class="tdDlgControlNormal">
			<asp:TextBox ID="txtUniform" runat="server" MaxLength="20" Width="200px"></asp:TextBox>
		</td>
	</tr>
	<tr class="trDlgControlNormal">
		<td class="tdDlgControlNormal">
			<asp:Label ID="Label7" runat="server" Text="Event Result : "></asp:Label>&nbsp;
		</td>
		<td class="tdDlgControlNormal">
			<asp:TextBox ID="txtResult" runat="server" MaxLength="50" Width="200px"></asp:TextBox>
		</td>
	</tr>
	<tr class="trDlgControlNormal">
		<td class="tdDlgControlNormal">
			<asp:Label ID="lblComments" runat="server" Text="Comments : "></asp:Label>&nbsp;
		</td>
		<td class="tdDlgControlNormal">
			<asp:TextBox ID="txtComments" runat="server" Height="50px" TextMode="MultiLine" Width="400px"></asp:TextBox>
		</td>
	</tr>
	<tr class="trDlgControlNormal">
		<td class="tdDlgControlNormal">
			<asp:Label ID="Label9" runat="server" Text="Event Website : "></asp:Label>&nbsp;
		</td>
		<td class="tdDlgControlNormal">
			<asp:TextBox ID="txtWebsite" runat="server" MaxLength="250" Width="400px"></asp:TextBox>
		</td>
	</tr>
	<tr class="trDlgControlNormal">
		<td class="tdDlgControlNormal" colspan="2">
			<hr />
		</td>
	</tr>
	<tr class="trDlgControlNormal">
		<td class="tdDlgControlNormal" colspan="2">
			<asp:Label ID="Label10" runat="server" Text="Options : "></asp:Label>&nbsp;
		</td>
	</tr>
	<tr class="trDlgControlNormal">
		<td class="tdDlgControlNormal" colspan="2">
			<table class="tblDlgControlInner">
				<tr class="trDlgControlNormal">
					<td class="tdDlgControlNormal">
						<asp:CheckBox ID="chkRequestResponse" runat="server" Text="Request responses" />
					</td>
					<td class="tdDlgControlNormal">
						<asp:CheckBox ID="chkSendReminder" runat="server" Text="Send Reminders" />
					</td>
					<td class="tdDlgControlNormal">
						<asp:Button ID="btnDeleteEvent" runat="server" Text="Delete Event" />
					</td>
				</tr>
			</table>
		</td>
	</tr>
	<tr class="trDlgControlNormal">
		<td class="tdRight">
			<asp:Button ID="btnOK" runat="server" Text="OK" OnClick="OnClickOK" CssClass="btnOK" />
		</td>
		<td class="tdLeft">
			<asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="OnClickCancel" CssClass="btnCancel" />
		</td>
	</tr>
</table>