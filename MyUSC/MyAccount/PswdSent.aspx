<%@ Page Title="" Language="C#" MasterPageFile="~/USC.Master" AutoEventWireup="true" CodeBehind="PswdSent.aspx.cs" Inherits="MyUSC.MyAccount.PswdSent" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
	<asp:Panel ID="Panel1" runat="server">
		<table style="background-image: url('Images/Background.png')" class="tblPage">
			<tr class="trNormal">
				<td class="tdInput">
					<span class="lgNormalTxt">Success! </span>
					<span class="medNormalTxt"> An email has been sent to the account we have on record for you and should arrive in a few minutes. If you do not see it, please
					check your junk mail folder and also make sure to accept emails from MySportsConnect.net.
					</span>
				</td>
			</tr>
			<tr class="trNormal">
				<td class="tdCenter">
					<asp:Button ID="btnOK" runat="server" Text="OK" CssClass="btnOK" />
				</td>
			</tr>
		</table>
	</asp:Panel>
</asp:Content>
