<%@ Page Title="" Language="C#" MasterPageFile="~/USC.Master" AutoEventWireup="true" CodeBehind="ChangePassword.aspx.cs" Inherits="MyUSC.ChangePassword" %>
<%@ MasterType VirtualPath="~/USC.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
	<script type="text/javascript" src="~/js/GA.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
	<table style="width: 1060px; height: 650px; background-image: url('Images/Background.png')">
		<tr>
			<td class="tdInput" style="height: 40px" colspan="2">
				<asp:Label ID="lblInstructions" runat="server" Text=""></asp:Label>
			</td>
		</tr>
		<tr>
			<td class="tdError" style="height: 40px" colspan="2">
				<asp:Label ID="lblError" runat="server" Text=""></asp:Label>
			</td>
		</tr>
		<tr>
			<td class="tdInput" style="height: 40px; width: 150px;">
				<asp:Label ID="lblPassword" runat="server" Text="Password : "></asp:Label>
			</td>
			<td class="tdInput" style="height: 40px">
				<asp:TextBox ID="txtPassword" runat="server" MaxLength="50" Width="279px" TextMode="Password"></asp:TextBox>&nbsp;&nbsp;
				<asp:CheckBox ID="chkShowPswd" runat="server" Text="Show Password" OnCheckedChanged="OnChangedShowPswd" AutoPostBack="True" />
			</td>
		</tr>
		<tr>
			<td class="tdInput" style="height: 40px">
				<asp:Label ID="lblConfirm" runat="server" Text="Confirm Password : "></asp:Label>
			</td>
			<td class="tdInput" style="height: 40px">
				<asp:TextBox ID="txtConfirm" runat="server" MaxLength="50" Width="279px" TextMode="Password"></asp:TextBox>
			</td>
		</tr>
		<tr>
			<td class="tdInput" valign="top" align="right">
				<asp:ImageButton ID="btnUpdate" ImageUrl="~/Images/Button/btnUpdate.png" runat="server" OnClick="OnClickUpdate" />
			</td>
			<td class="tdInput" valign="top">
				<asp:ImageButton ID="btnCancel" ImageUrl="~/Images/Button/btnCancel.png" runat="server" OnClick="OnClickCancel" />
			</td>
		</tr>
    </table>
</asp:Content>
