<%@ Page Title="" Language="C#" MasterPageFile="~/USC.Master" AutoEventWireup="true" CodeBehind="AccountSuccess.aspx.cs" Inherits="MyUSC.Verify.AccountSuccess" %>
<%@ MasterType VirtualPath="~/USC.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
	<script type="text/javascript" src="~/js/GA.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
	<table style="width: 1060px; height: 650px; background-image: url('/Images/Background.png')">
		<tr valign="top" style="height: 200px">
			<td class="tdInput">
				<div class="medSiteColorTxt">
					<asp:Literal ID="litAccountSuccess" runat="server"></asp:Literal>
				</div>
			</td>
		</tr>
		<tr valign="top">
			<td class="tdInput" align="center">
				<asp:ImageButton ID="btnOK"  runat="server" ImageUrl="~/Images/Button/btnOK.png" OnClick="OnClickOK" />
			</td>
		</tr>
    </table>
</asp:Content>
