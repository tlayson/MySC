<%@ Page Title="" Language="C#" MasterPageFile="~/USC.Master" AutoEventWireup="true" CodeBehind="DeactivateAccount.aspx.cs" Inherits="MyUSC.MyAccount.DeactivateAccount" %>
<%@ MasterType VirtualPath="~/USC.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
	<script type="text/javascript" src="~/js/GA.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
	<table style="width: 1060px; height: 650px; background-image: url('Images/Background.png')">
		<tr valign="top">
			<td class="tdInput" colspan="2" style="height: 30px">
				<asp:Label ID="lblConfirmHead" runat="server" Text="Are you sure you want to deactivate your account?" CssClass="lgSiteColorTxt"></asp:Label>
			</td>
		</tr>
		<tr valign="top">
			<td class="tdInput" colspan="2" style="height: 100px">
				<asp:Label ID="lblConfirmText" runat="server" Text="Please confirm that you wish to deactivate your account.  Once deactivated, you will need to contact support to re-activate it."></asp:Label>
			</td>
		</tr>
		<tr valign="top">
			<td class="tdInput" align="right">
				<asp:ImageButton ID="btnOK" runat="server" ImageUrl="~/Images/Button/btnOK.png" OnClick="OnClickOK" />
			</td>
			<td class="tdInput">
				<asp:ImageButton ID="btnCancel" runat="server" ImageUrl="~/Images/Button/btnCancel.png" OnClick="OnClickCancel" />
			</td>
		</tr>
	</table>
</asp:Content>
