<%@ Page Title="Forum" Language="C#" MasterPageFile="~/USC.master" AutoEventWireup="true" CodeBehind="Forum.aspx.cs" Inherits="MyUSC.forum.YForum" %>
<%@ MasterType VirtualPath="~/USC.Master" %>
<%@ Register TagPrefix="YAF" Assembly="YAF" Namespace="YAF" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
	<script type="text/javascript" src="~/js/GA.js"></script>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
	<table width="1060px">
		<tr valign="top">
			<td class="tdInput">
			    <YAF:Forum runat="server" ID="ctrlForum" BoardID="1" />
			</td>
		</tr>
	</table>
</asp:Content>
