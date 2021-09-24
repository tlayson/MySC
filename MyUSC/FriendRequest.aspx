<%@ Page Title="" Language="C#" MasterPageFile="~/USC.Master" AutoEventWireup="true" CodeBehind="FriendRequest.aspx.cs" Inherits="MyUSC.FriendRequestPage" %>
<%@ MasterType VirtualPath="~/USC.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
	<script type="text/javascript" src="~/js/GA.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
	<asp:HiddenField ID="hfTarget" runat="server" Value="0" />
	<table style="width: 1060px; height: 450px; background-image: url('Images/Background.png')">
		<tr valign="top">
			<td class="tdInput" style="height: 35px" colspan="2">
				<asp:Label ID="lblSendTo" runat="server" Text="Send Request To : "></asp:Label>
				<asp:Label ID="lblFriend" runat="server" Text="Label"></asp:Label>
			</td>
		</tr>
		<tr valign="top">
			<td class="tdInput" colspan="2" style="height: 80px">
				<asp:Label ID="lblComments" runat="server" Text="Comments"></asp:Label><br />
				<asp:TextBox ID="txtComments" runat="server" MaxLength="200" Width="800px"></asp:TextBox>
			</td>
		</tr>
		<tr valign="top">
			<td class="tdInput" align="right">
				<asp:ImageButton ID="btnSend" ImageUrl="~/Images/Button/btnSend.png" runat="server" OnClick="OnClickSend" />
			</td>
			<td class="tdInput">
				<asp:ImageButton ID="btnCancel" ImageUrl="~/Images/Button/btnCancel.png" runat="server" OnClick="OnClickCancel" />
			</td>
		</tr>
	</table>
</asp:Content>
