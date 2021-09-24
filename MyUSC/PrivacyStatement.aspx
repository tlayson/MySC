<%@ Page Title="" Language="C#" MasterPageFile="~/USC.Master" AutoEventWireup="true" CodeBehind="PrivacyStatement.aspx.cs" Inherits="MyUSC.PrivacyStatement" %>
<%@ MasterType VirtualPath="~/USC.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
	<script type="text/javascript" src="~/js/GA.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <table style="width: 1060px; height: 650px; background-image: url('Images/Background.png')">
		<tr>
			<td class="tdInput" style="height: 40px">
				<span class="xlgSiteColorTxt">
					MySportsConnect Privacy Policy
				</span>
			</td>
		</tr>
		<tr>
			<td valign="top">
				<span class="lgSiteColorTxt">
					<asp:Label ID="lblPrivacyText" runat="server" Text="Label"></asp:Label>
				</span>
			</td>
		</tr>
		<tr>
			<td valign="top" align="center">
				<span class="lgSiteColorTxt">
					<asp:ImageButton ID="btnReturn" ImageUrl="~/Images/Button/btnReturn.png" runat="server" OnClick="OnClickReturn" />
				</span>
			</td>
		</tr>
    </table>
</asp:Content>
