<%@ Page Title="" Language="C#" MasterPageFile="~/USC.Master" AutoEventWireup="true" CodeBehind="CreateMsg.aspx.cs" Inherits="MyUSC.CreateMsg" %>
<%@ MasterType VirtualPath="~/USC.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
	<script type="text/javascript" src="~/js/GA.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
	<asp:HiddenField ID="hfParentThread" runat="server" Value="0" />
	<asp:HiddenField ID="hfMsgTarget" runat="server" Value="" />
	<table style="width: 1060px; height: 650px; background-image: url('Images/Background.png')">
		<tr valign="top">
			<td class="tdError" colspan="2" style="height: 25px">
				<asp:Label ID="lblMsgError" runat="server" Text=""></asp:Label>
			</td>
		</tr>
		<tr valign="top">
			<td class="tdInput" style="height: 40px">
				To : 
				<asp:DropDownList ID="ddlSelectFriend" runat="server" Width="250px"></asp:DropDownList>
				<asp:Label ID="lblMsgTo" runat="server" Text="Label" Visible="False"></asp:Label>
			</td>
			<td class="tdInput">
				Related Story Title : <asp:TextBox ID="txtStoryTitle" runat="server" Width="400px" MaxLength="500"></asp:TextBox>
			</td>
		</tr>
		<tr valign="top">
			<td class="tdInput" style="height: 40px">
				From : <asp:TextBox ID="txtFrom" runat="server" Width="400px" MaxLength="100"></asp:TextBox>
			</td>
			<td class="tdInput">
				Related Story Link : <asp:TextBox ID="txtStoryLink" runat="server" Width="400px" MaxLength="500"></asp:TextBox>
			</td>
		</tr>
		<tr valign="top">
			<td class="tdInput" style="height: 40px">
			</td>
			<td class="tdInput">
				Related Photo Link : <asp:TextBox ID="txtStoryPhotoLink" runat="server" Width="400px" MaxLength="500"></asp:TextBox>
			</td>
		</tr>
		<tr valign="top">
			<td class="tdInput" style="height: 40px" colspan="2">
				Message Title : <asp:TextBox ID="txtMessageTitle" runat="server" Width="801px" MaxLength="50"></asp:TextBox>
			</td>
		</tr>
		<tr valign="top">
			<td class="tdInput" colspan="2">
				Message Text : <br/>
				<asp:TextBox ID="txtMessageText" runat="server" Height="400px" TextMode="MultiLine" Width="900px"></asp:TextBox>
			</td>
		</tr>
		<tr valign="top">
			<td class="tdInput" style="height: 40px" colspan="2">
				<asp:CheckBox ID="chkPrivateMsg" runat="server" Text="Private Message" />
			</td>
		</tr>
		<tr valign="top">
			<td class="tdInput" style="height: 40px" align="right">
				<asp:ImageButton ID="btnSendMsg" ImageUrl="~/Images/Button/btnSendMessage.png" runat="server" OnClick="OnClickSendMessage" />
			</td>
			<td class="tdInput" style="height: 40px">
				<asp:ImageButton ID="btnCancelMsg" ImageUrl="~/Images/Button/btnCancel.png" runat="server" OnClick="OnClickCancelMessage" />
			</td>
		</tr>
	</table>
</asp:Content>
