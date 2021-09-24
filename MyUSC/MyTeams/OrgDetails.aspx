<%@ Page Title="" Language="C#" MasterPageFile="~/MyTeams/OrgMaster.master" AutoEventWireup="true" CodeBehind="OrgDetails.aspx.cs" Inherits="MyUSC.MyTeams.OrgDetails" %>
<%@ MasterType VirtualPath="~/MyTeams/OrgMaster.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="OrgHeadContent" runat="server">
	<script type="text/javascript" src="~/js/GA.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="OrgContent" runat="server">
	<table style="width: 800px; vertical-align: top;">
		<tr style="height: 45px">
			<td class="tdInput">
				<asp:Label ID="Label1" runat="server" Text="Name"></asp:Label>
			</td>
			<td class="tdInput">
				<asp:TextBox ID="txtOrgName" runat="server" Width="310px" MaxLength="249"></asp:TextBox>
				&nbsp;
				<asp:Label ID="Label3" runat="server" Text=" * " CssClass="medErrorTxt"></asp:Label>
			</td>
			<td class="tdInput">
				<asp:Button ID="btnNextTop" runat="server" Text="OK" OnClientClick="OnClickNext" OnClick="OnClickOK" />
			</td>
		</tr>
		<tr style="height: 45px">
			<td class="tdInput">
				<asp:Label ID="Label2" runat="server" Text="Type"></asp:Label>
			</td>
			<td class="tdInput">
				<asp:DropDownList ID="ddlOrgType" runat="server">
					<asp:ListItem Text="Organization" Value="1"></asp:ListItem>
					<asp:ListItem Text="Region" Value="2"></asp:ListItem>
					<asp:ListItem Text="State" Value="3"></asp:ListItem>
					<asp:ListItem Text="District" Value="4"></asp:ListItem>
					<asp:ListItem Text="League" Value="5"></asp:ListItem>
					<asp:ListItem Text="Division" Value="6"></asp:ListItem>
					<asp:ListItem Text="Team" Value="7"></asp:ListItem>
					<asp:ListItem Text="Other" Value="10"></asp:ListItem>
				</asp:DropDownList>
				&nbsp;
				<asp:Label ID="Label4" runat="server" Text=" * " CssClass="medErrorTxt"></asp:Label>
			</td>
			<td class="tdInput">
				<asp:Button ID="btnCancelTop" runat="server" Text="Cancel" OnClick="OnClickCancel" />
			</td>
		</tr>
		<tr style="height: 45px">
			<td class="tdInput">
				<asp:Label ID="Label6" runat="server" Text="Description"></asp:Label>
			</td>
			<td class="tdInput">
				<asp:TextBox ID="txtOrgDescription" runat="server" Width="650px" Height="50px" MaxLength="499" TextMode="MultiLine"></asp:TextBox>
			</td>
			<td class="tdInput">

			</td>
		</tr>
		<tr style="height: 45px">
			<td class="tdInput">
				<asp:Label ID="Label5" runat="server" Text="Administrator"></asp:Label>
			</td>
			<td class="tdInput">
				<asp:Label ID="lblOrgAdmin" runat="server" Text="Your name here"></asp:Label>
			</td>
			<td class="tdInput">

			</td>
		</tr>
		<tr style="height: 45px">
			<td class="tdInput">
				<asp:Label ID="Label15" runat="server" Text="Website"></asp:Label>
			</td>
			<td class="tdInput">
				<asp:TextBox ID="txtOrgWebsite" runat="server" Width="650px" MaxLength="249"></asp:TextBox>
			</td>
			<td class="tdInput">

			</td>
		</tr>
		<tr style="height: 45px">
			<td class="tdInput">
				<asp:Label ID="Label7" runat="server" Text="Address 1"></asp:Label>
			</td>
			<td class="tdInput">
				<asp:TextBox ID="txtOrgAddress1" runat="server" Width="555px" MaxLength="49"></asp:TextBox>
			</td>
			<td class="tdInput">

			</td>
		</tr>
		<tr style="height: 45px">
			<td class="tdInput">
				<asp:Label ID="Label8" runat="server" Text="Address 2"></asp:Label>
			</td>
			<td class="tdInput">
				<asp:TextBox ID="txtOrgAddress2" runat="server" Width="555px" MaxLength="49"></asp:TextBox>
			</td>
			<td class="tdInput">

			</td>
		</tr>
		<tr style="height: 45px">
			<td class="tdInput">
				<asp:Label ID="Label10" runat="server" Text="City"></asp:Label>
			</td>
			<td class="tdInput">
				<asp:TextBox ID="txtOrgCity" runat="server" Width="286px" MaxLength="49"></asp:TextBox>
			</td>
			<td class="tdInput">

			</td>
		</tr>
		<tr style="height: 45px">
			<td class="tdInput">
				<asp:Label ID="Label11" runat="server" Text="State"></asp:Label>
			</td>
			<td class="tdInput">
				<MSC:StateDropDown ID="ddlOrgState" runat="server"></MSC:StateDropDown>
			</td>
			<td class="tdInput">

			</td>
		</tr>
		<tr style="height: 45px">
			<td class="tdInput">
				<asp:Label ID="Label12" runat="server" Text="Postal Code"></asp:Label>
			</td>
			<td class="tdInput">
				<asp:TextBox ID="txtOrgZip" runat="server" MaxLength="49"></asp:TextBox>
			</td>
			<td class="tdInput">

			</td>
		</tr>
		<tr style="height: 45px">
			<td class="tdInput">
				<asp:Label ID="Label13" runat="server" Text="Country"></asp:Label>
			</td>
			<td class="tdInput">
				<asp:TextBox ID="txtOrgCountry" runat="server" MaxLength="49"></asp:TextBox>
			</td>
			<td class="tdInput">

			</td>
		</tr>
		<tr style="vertical-align: top">
			<td class="tdInput">
			</td>
			<td class="tdInput">
				<asp:Button ID="tblOKBottom" runat="server" Text="OK" OnClick="OnClickOK" />&nbsp;&nbsp;&nbsp;
				<asp:Button ID="tblCancelBottom" runat="server" Text="Cancel" OnClick="OnClickCancel" />
			</td>
			<td class="tdInput">

			</td>
		</tr>
	</table>
</asp:Content>
