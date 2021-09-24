<%@ Page Title="" Language="C#" MasterPageFile="~/USC.Master" AutoEventWireup="true" CodeBehind="SupportSuccess.aspx.cs" Inherits="MyUSC.SupportSuccess" %>
<%@ MasterType VirtualPath="~/USC.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
	<script type="text/javascript" src="~/js/GA.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
	<table style="width: 1060px; height: 650px; background-image: url('Images/Background.png')">
		<tr valign="top" style="height: 40px">
			<td class="tdInput" colspan="3">
				<asp:Label ID="lblSuccessHead" runat="server" Text="Support Request Sent" CssClass="xlgSiteColorTxt"></asp:Label>
			</td>
		</tr>
		<tr valign="top" style="height: 100px">
			<td class="tdInput" colspan="3">
				<asp:Label ID="lblSuccessMsg" runat="server" Text="Label"></asp:Label>
			</td>
		</tr>
		<tr valign="top">
			<td class="tdInput">
				
			</td>
			<td class="tdInput" align="center">
				<asp:ImageButton ID="btnOK" runat="server" ImageUrl="~/Images/Button/btnOK.png" OnClick="OnClickOK" />
			</td>
			<td class="tdInput">
				
			</td>
		</tr>
	</table>
</asp:Content>
