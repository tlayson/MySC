<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="LocationDetails.ascx.cs" Inherits="MyUSC.Classes.LocationDetails" %>
<%@ Register Assembly="MyUSC" Namespace="MyUSC.Classes" TagPrefix="MSC" %>
<link href="/Styles/Site.css" rel="stylesheet" type="text/css" />
<table style="width: 650px" class="tblDlgControlOuter">
	<tr class="trDlgControlTitle">
		<td class="tdDlgControlNormal" colspan="2">
			<asp:Label ID="lblTitle" runat="server" Text="Location"></asp:Label>
		</td>
	</tr>
	<tr class="trDlgControlNormal">
		<td class="tdDlgControlNormal">
			<asp:Label ID="lblLocName" runat="server" Text="Name"></asp:Label>
		</td>
		<td class="tdDlgControlNormal">
			<asp:TextBox ID="txtLocName" runat="server" MaxLength="250" Width="400px"></asp:TextBox>
		</td>
	</tr>
	<tr class="trDlgControlNormal">
		<td class="tdDlgControlNormal">
			<asp:Label ID="Label1" runat="server" Text="Address"></asp:Label>
		</td>
		<td class="tdDlgControlNormal">
			<asp:TextBox ID="txtLocAddress" runat="server" MaxLength="50" Width="300px"></asp:TextBox>
		</td>
	</tr>
	<tr class="trDlgControlNormal">
		<td class="tdDlgControlNormal">
			<asp:Label ID="Label10" runat="server" Text="Address"></asp:Label>
		</td>
		<td class="tdDlgControlNormal">
			<asp:TextBox ID="txtLocAddress2" runat="server" MaxLength="50" Width="300px"></asp:TextBox>
		</td>
	</tr>
	<tr class="trDlgControlNormal">
		<td class="tdDlgControlNormal">
			<asp:Label ID="Label2" runat="server" Text="City"></asp:Label>
		</td>
		<td class="tdDlgControlNormal">
			<asp:TextBox ID="txtLocCity" runat="server" MaxLength="50" Width="200px"></asp:TextBox>
		</td>
	</tr>
	<tr class="trDlgControlNormal">
		<td class="tdDlgControlNormal">
			<asp:Label ID="Label3" runat="server" Text="State"></asp:Label>
		</td>
		<td class="tdDlgControlNormal">
			<MSC:StateDropDown ID="ddlLocState" runat="server"></MSC:StateDropDown>
		</td>
	</tr>
	<tr class="trDlgControlNormal">
		<td class="tdDlgControlNormal">
			<asp:Label ID="Label4" runat="server" Text="Postal Code"></asp:Label>
		</td>
		<td class="tdDlgControlNormal">
			<asp:TextBox ID="txtLocZip" runat="server" MaxLength="30" Width="150px"></asp:TextBox>
		</td>
	</tr>
	<tr class="trDlgControlNormal">
		<td class="tdDlgControlNormal">
			<asp:Label ID="Label5" runat="server" Text="Country"></asp:Label>
		</td>
		<td class="tdDlgControlNormal">
			<MSC:CountryDropDown ID="ddlLocCountry" runat="server"></MSC:CountryDropDown>
		</td>
	</tr>
	<tr class="trDlgControlNormal">
		<td class="tdDlgControlNormal">
			<asp:Label ID="Label6" runat="server" Text="Phone"></asp:Label>
		</td>
		<td class="tdDlgControlNormal">
			<asp:TextBox ID="txtLocPhone" runat="server" MaxLength="50" Width="150px"></asp:TextBox>
		</td>
	</tr>
	<tr class="trDlgControlNormal">
		<td class="tdDlgControlNormal">
			<asp:Label ID="Label7" runat="server" Text="Website"></asp:Label>
		</td>
		<td class="tdDlgControlNormal">
			<asp:TextBox ID="txtLocWebsite" runat="server" MaxLength="250" Width="400px"></asp:TextBox>
		</td>
	</tr>
	<tr class="trDlgControlNormal">
		<td class="tdDlgControlNormal">
			<asp:Label ID="Label8" runat="server" Text="Map URL"></asp:Label>
		</td>
		<td class="tdDlgControlNormal">
			<asp:TextBox ID="txtLocMapURL" runat="server" MaxLength="250" Width="400px"></asp:TextBox>
		</td>
	</tr>
	<tr class="trDlgControlNormal">
		<td class="tdDlgControlNormal">
			<asp:Label ID="Label9" runat="server" Text="Notes"></asp:Label>
		</td>
		<td class="tdDlgControlNormal">
			<asp:TextBox ID="txtLocNotes" runat="server" Height="100px" MaxLength="1000" TextMode="MultiLine" Width="400px"></asp:TextBox>
		</td>
	</tr>
	<tr class="trDlgControlNormal">
		<td class="tdDlgControlNormal" colspan="2">
			<table class="tblDlgControlInner">
				<tr class="trDlgControlNormal">
					<td class="tdDlgControlNormal">
						<asp:CheckBox ID="chkPublicVenue" runat="server" Text="Public Venue" ToolTip="Make this venue available to everyone." />
					</td>
					<td class="tdDlgControlNormal">
						<asp:Button ID="btnDelete" runat="server" Text="Delete" CssClass="btnNormal" OnClick="OnClickDelete" />
					</td>
				</tr>
			</table>
		</td>
	</tr>
	<tr class="trDlgControlNormal">
		<td class="tdDlgControlNormal" colspan="2">
			<table class="tblDlgControlInner">
				<tr class="trDlgControlNormal">
					<td class="tdRight">
						<asp:Button ID="btnOK" runat="server" Text="OK" OnClick="OnClickOK" CssClass="btnOK" />
					</td>
					<td class="tdLeft">
						<asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="OnClickCancel" CssClass="btnCancel" />
					</td>
				</tr>
			</table>
		</td>
	</tr>
</table>