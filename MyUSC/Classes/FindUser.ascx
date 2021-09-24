<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="FindUser.ascx.cs" Inherits="MyUSC.Classes.FindUser" %>
<link href="/Styles/Site.css" rel="stylesheet" type="text/css" />
<table style="width: 650px" class="tblDlgControlOuter">
	<tr class="trDlgControlTitle">
		<td class="tdDlgControlNormal" colspan="2">
			<asp:Label ID="lblTitle" runat="server" Text="Find User"></asp:Label>
		</td>
	</tr>
	<tr>
		<td class="tdDlgControlNormal" colspan="2">
			Fill in the fields below and click FIND FRIENDS to search for people you know.
		</td>
	</tr>
	<tr>
		<td class="tdDlgControlNormal">
			<asp:Label ID="lblFirstName" runat="server" Text="First Name"></asp:Label><br />
			<asp:TextBox ID="txtFirstName" runat="server" Width="214px"></asp:TextBox>
		</td>

		<td class="tdDlgControlNormal">
			<asp:Label ID="lblLastName" runat="server" Text="Last Name"></asp:Label><br />
			<asp:TextBox ID="txtLastName" runat="server" Width="214px"></asp:TextBox>
		</td>
	</tr>
	<tr>
		<td colspan ="2" class="tdDlgControlNormal">
			<asp:Label ID="lblCity" runat="server" Text="City"></asp:Label><br />
			<asp:TextBox ID="txtCity" runat="server" Width="610px"></asp:TextBox>
		</td>
	</tr>
	<tr>
		<td class="tdDlgControlNormal">
			<asp:Label ID="lblState" runat="server" Text="State"></asp:Label><br />
			<asp:DropDownList ID="ddlState" Height="19px" Width="214px" runat="server" OnSelectedIndexChanged="OnSelChange" OnTextChanged="OnTextChanged"></asp:DropDownList>
		</td>
		<td class="tdDlgControlNormal">
			<asp:Label ID="LblPostalCode" runat="server" Text="Postal Code"></asp:Label><br />
			<asp:TextBox ID="txtPostalCode" runat="server"></asp:TextBox>
		</td>
	</tr>
	<tr>
		<td class="tdRight">
			<asp:ImageButton ID="btnFindFriends" runat="server" ImageUrl="/Images/Button/btnFindFriends.png" OnClick="OnClickOK" />
		</td>
		<td class="tdLeft">
			<asp:ImageButton ID="btnCancelFind" runat="server" ImageUrl="/Images/Button/btnCancel.png" OnClick="OnClickCancel" />
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