<%@ Page Title="" Language="C#" MasterPageFile="~/USC.Master" AutoEventWireup="true" CodeBehind="Forum.aspx.cs" Inherits="MyUSC.forum.Forum" %>
<%@ MasterType VirtualPath="~/USC.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
	<script type="text/javascript" src="~/js/GA.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
	<table style="width: 1060px; height: 650px; background-image: url('/Images/Background.png')">
		<tr valign="top">
			<td class="tdInput">
				<iframe src="/forums/index.php" name="ifHTMLDisplay" frameborder="0" width="1060px" height="900px"></iframe>
			</td>
		</tr>
    </table>
</asp:Content>
