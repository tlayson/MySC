<%@ Page Title="" Language="C#" MasterPageFile="~/MyTeams/OrgMaster.master" AutoEventWireup="true" CodeBehind="CreateOrg.aspx.cs" Inherits="MyUSC.MyTeams.CreateOrg" %>
<%@ MasterType VirtualPath="~/MyTeams/OrgMaster.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="OrgHeadContent" runat="server">
	<script type="text/javascript" src="~/js/GA.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="OrgContent" runat="server">
	<table style="width: 800px; height: 650px; vertical-align: top;">
		<tr style="height: 45px">
			<td class="tdInput">
				<asp:Label ID="Label1" runat="server" Text="Name : "></asp:Label>
			</td>
			<td class="tdInput">
				<asp:TextBox ID="txtOrgName" runat="server" Width="310px"></asp:TextBox>
				&nbsp;
				<asp:Label ID="Label3" runat="server" Text=" * " CssClass="medErrorTxt"></asp:Label>
			</td>
			<td class="tdInput">
				<asp:Button ID="Button1" runat="server" Text="Next" OnClick="OnClickNext" />
			</td>
		</tr>
		<tr style="height: 45px">
			<td class="tdInput">
				<asp:Label ID="Label17" runat="server" Text="Description :" Width="150px"></asp:Label>
			</td>
			<td class="tdInput">
				<asp:TextBox ID="txtOrgDesc" runat="server" Width="310px"></asp:TextBox>
			</td>
			<td class="tdInput">
				<asp:Button ID="Button3" runat="server" Text="Cancel" OnClick="OnClickCancel" />
			</td>
		</tr>
		<tr style="height: 45px">
			<td class="tdInput">
				<asp:Label ID="Label2" runat="server" Text="Type : "></asp:Label>
			</td>
			<td class="tdInput">
				<asp:DropDownList ID="ddOrgType" runat="server">
					<asp:ListItem Text="Organization" Value="1"></asp:ListItem>
					<asp:ListItem Text="Region" Value="2"></asp:ListItem>
					<asp:ListItem Text="State" Value="3"></asp:ListItem>
					<asp:ListItem Text="District" Value="4"></asp:ListItem>
					<asp:ListItem Text="League" Value="5"></asp:ListItem>
					<asp:ListItem Text="Division" Value="6"></asp:ListItem>
					<asp:ListItem Selected="True" Text="Team" Value="7"></asp:ListItem>
					<asp:ListItem Text="Other" Value="10"></asp:ListItem>
				</asp:DropDownList>
				&nbsp;
				<asp:Label ID="Label4" runat="server" Text=" * " CssClass="medErrorTxt"></asp:Label>
			</td>
			<td class="tdInput">

			</td>
		</tr>
		<tr style="height: 45px">
			<td class="tdInput">
				<asp:Label ID="Label5" runat="server" Text="Owner :"></asp:Label>
			</td>
			<td class="tdInput">
				<asp:Label ID="lblOwnerName" runat="server" Text="Your name here"></asp:Label>
			</td>
			<td class="tdInput">

			</td>
		</tr>
		<tr style="height: 45px">
			<td class="tdInput">
				<asp:Label ID="Label15" runat="server" Text="Website :"></asp:Label>
			</td>
			<td class="tdInput">
				<asp:TextBox ID="txtWebsite" runat="server" Width="658px"></asp:TextBox>
			</td>
			<td class="tdInput">

			</td>
		</tr>
		<tr style="height: 45px">
			<td class="tdInput">
				<asp:Label ID="Label7" runat="server" Text="Address 1 :"></asp:Label>
			</td>
			<td class="tdInput">
				<asp:TextBox ID="txtAddress1" runat="server" Width="555px"></asp:TextBox>
			</td>
			<td class="tdInput">

			</td>
		</tr>
		<tr style="height: 45px">
			<td class="tdInput">
				<asp:Label ID="Label8" runat="server" Text="Address 2 :"></asp:Label>
			</td>
			<td class="tdInput">
				<asp:TextBox ID="txtAddress2" runat="server" Width="555px"></asp:TextBox>
			</td>
			<td class="tdInput">

			</td>
		</tr>
		<tr style="height: 45px">
			<td class="tdInput">
				<asp:Label ID="Label10" runat="server" Text="City :"></asp:Label>
			</td>
			<td class="tdInput">
				<asp:TextBox ID="txtCity" runat="server" Width="286px"></asp:TextBox>
			</td>
			<td class="tdInput">

			</td>
		</tr>
		<tr style="height: 45px">
			<td class="tdInput">
				<asp:Label ID="Label11" runat="server" Text="State :"></asp:Label>
			</td>
			<td class="tdInput">
				<asp:TextBox ID="txtState" runat="server" Width="255px"></asp:TextBox>
			</td>
			<td class="tdInput">

			</td>
		</tr>
		<tr style="height: 45px">
			<td class="tdInput">
				<asp:Label ID="Label12" runat="server" Text="Postal Code :"></asp:Label>
			</td>
			<td class="tdInput">
				<asp:TextBox ID="txtPostalCode" runat="server"></asp:TextBox>
			</td>
			<td class="tdInput">

			</td>
		</tr>
		<tr style="height: 45px">
			<td class="tdInput">
				<asp:Label ID="Label13" runat="server" Text="Country :"></asp:Label>
			</td>
			<td class="tdInput">
				<asp:TextBox ID="txtCountry" runat="server" Width="178px"></asp:TextBox>
			</td>
			<td class="tdInput">

			</td>
		</tr>
		<tr style="height: 45px">
			<td class="tdInput">
				<asp:Label ID="Label14" runat="server" Text="Email :"></asp:Label>
			</td>
			<td class="tdInput">
				<asp:TextBox ID="txtEmail" runat="server" Width="258px"></asp:TextBox>
			</td>
			<td class="tdInput">

			</td>
		</tr>
		<tr style="height: 45px">
			<td class="tdInput">
				<asp:Label ID="Label6" runat="server" Text="Phone :"></asp:Label>
			</td>
			<td class="tdInput">
				<asp:TextBox ID="txtPhone" runat="server" Width="181px"></asp:TextBox>
			</td>
			<td class="tdInput">

			</td>
		</tr>
		<tr style="height: 45px">
			<td class="tdInput">
				<asp:Label ID="Label9" runat="server" Text="Cell :"></asp:Label>
			</td>
			<td class="tdInput">
				<asp:TextBox ID="txtCell" runat="server" Width="182px"></asp:TextBox>
			</td>
			<td class="tdInput">

			</td>
		</tr>
		<tr style="height: 45px">
			<td class="tdInput">
				<asp:Label ID="Label16" runat="server" Text="Fax :"></asp:Label>
			</td>
			<td class="tdInput">
				<asp:TextBox ID="txtFax" runat="server" Width="178px"></asp:TextBox>
			</td>
			<td class="tdInput">

			</td>
		</tr>
		<tr style="vertical-align: top">
			<td class="tdInput">
			</td>
			<td class="tdInput">
				<asp:Button ID="Button2" runat="server" Text="Next" OnClick="OnClickNext" />&nbsp;&nbsp;&nbsp;
				<asp:Button ID="Button4" runat="server" Text="Cancel" OnClick="OnClickCancel" />
			
			</td>
			<td class="tdInput">

			</td>
		</tr>
	</table>
</asp:Content>
