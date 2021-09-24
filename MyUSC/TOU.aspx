<%@ Page Title="" Language="C#" MasterPageFile="~/USC.Master" AutoEventWireup="true" CodeBehind="TOU.aspx.cs" Inherits="MyUSC.TermsOfUse" %>
<%@ MasterType VirtualPath="~/USC.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
	<script type="text/javascript" src="~/js/GA.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <table style="width: 1060px; height: 650px; background-image: url('Images/Background.png')">
		<tr>
			<td class="auto-style1" valign="top" colspan="2">
				<asp:TextBox ID="txtTermsOfUse" runat="server" TextMode="MultiLine" ReadOnly="True" Width="900px" Height="550px" BackColor="White" ForeColor="Black"></asp:TextBox>
			</td>
		</tr>
		<tr>
			<td class="tdInput" align="right">
				<asp:ImageButton ID="btnAccept" ImageUrl="~/Images/Button/btnIAccept.png" runat="server" OnClick="OnClickButtonAccept" />
			</td>
			<td class="tdInput">
				<asp:ImageButton ID="btnDecline" ImageUrl="~/Images/Button/btnIDecline.png" runat="server" OnClick="OnClickButtonDecline" />
			</td>
		</tr>
	</table>
</asp:Content>
