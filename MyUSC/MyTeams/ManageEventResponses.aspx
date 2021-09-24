<%@ Page Title="" Language="C#" MasterPageFile="~/MyTeams/OrgMaster.master" AutoEventWireup="true" CodeBehind="ManageEventResponses.aspx.cs" Inherits="MyUSC.MyTeams.ManageEventResponses" %>
<%@ MasterType VirtualPath="~/MyTeams/OrgMaster.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="OrgHeadContent" runat="server">
	<script type="text/javascript" src="~/js/GA.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="OrgContent" runat="server">
<table class="tblMyTeamsPage">
	<tr class="trNormal">
		<td class="tdInput">
			<asp:Table ID="tblResponses" runat="server" Width="675px">
				<asp:TableRow>
					<asp:TableCell CssClass="tdInput" Width="150">
						Member
					</asp:TableCell>
					<asp:TableCell CssClass="tdInput" Width="75">
						Response
					</asp:TableCell>
					<asp:TableCell CssClass="tdInput" Width="100">
						Last Login
					</asp:TableCell>
					<asp:TableCell CssClass="tdInput" Width="350">
						Notes
					</asp:TableCell>
				</asp:TableRow>
				<asp:TableRow>
					<asp:TableCell ColumnSpan="4">
						<hr />
					</asp:TableCell>
				</asp:TableRow>
				<asp:TableRow CssClass="trNormal">
					<asp:TableCell CssClass="tdInput">
						<label class="medNormalTxt">Player 1</label>
					</asp:TableCell>
					<asp:TableCell CssClass="tdInput">
						<asp:DropDownList ID="DropDownList2" runat="server" AutoPostBack="True">
							<asp:ListItem Selected="True">None</asp:ListItem>
							<asp:ListItem>Yes</asp:ListItem>
							<asp:ListItem>No</asp:ListItem>
							<asp:ListItem>Maybe</asp:ListItem>
						</asp:DropDownList>
					</asp:TableCell>
					<asp:TableCell CssClass="tdInput">
						<label class="medNormalTxt">1-2-16</label>
					</asp:TableCell>
					<asp:TableCell CssClass="tdInput">
						<label class="smNormalTxt">Blah</label>
					</asp:TableCell>
				</asp:TableRow>
			</asp:Table>
</asp:Content>
