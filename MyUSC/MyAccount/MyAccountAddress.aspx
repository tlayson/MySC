<%@ Page Title="" Language="C#" MasterPageFile="~/USC.Master" AutoEventWireup="true" CodeBehind="MyAccountAddress.aspx.cs" Inherits="MyUSC.MyAccount.MyAccountAddress" %>
<%@ MasterType VirtualPath="~/USC.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
	<script type="text/javascript" src="~/js/GA.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
	<table style="width: 1060px; height: 650px; background-image: url('Images/Background.png')">
		<tr valign="top">
			<td class="tdInput">
				<table width="100%">
					<tr valign="top">
						<td class="tdInput" colspan="2">
							<asp:Label ID="lblInstructions" runat="server" Text="Instructions go here..."></asp:Label>
						</td>
					</tr>
					<tr valign="top">
						<td class="tdInput">
							<asp:Label ID="lblAddress1" runat="server" Text="Address 1 : "></asp:Label>
							<asp:TextBox ID="txtAddress1" runat="server" Width="350px" MaxLength="50"></asp:TextBox>
						</td>
						<td class="tdInput" align="right">
							<asp:ImageButton ID="btnOK" runat="server" ImageUrl="~/Images/Button/btnOK.png" OnClick="OnClickOK" />
						</td>
					</tr>
					<tr valign="top">
						<td class="tdInput" colspan="2">
							<asp:Label ID="lblAddress2" runat="server" Text="Address 2 : "></asp:Label>
							<asp:TextBox ID="txtAddress2" runat="server" Width="350px" MaxLength="50"></asp:TextBox>
						</td>
					</tr>
					<tr valign="top">
						<td class="tdInput">
							<asp:Label ID="lblCity" runat="server" Text="City : "></asp:Label>
							<asp:TextBox ID="txtCity" runat="server" Width="250px" MaxLength="50"></asp:TextBox>
						</td>
						<td class="tdInput">
							<asp:Label ID="lblState" runat="server" Text="State : "></asp:Label>
							<asp:DropDownList ID="ddlState" runat="server" Width="200px"></asp:DropDownList>
						</td>
					</tr>
					<tr valign="top">
						<td class="tdInput">
							<asp:Label ID="lblZip" runat="server" Text="Zip : "></asp:Label>
							<asp:TextBox ID="txtZip" runat="server" Width="250px" MaxLength="50"></asp:TextBox>
						</td>
						<td class="tdInput">
							<asp:Label ID="lblCountry" runat="server" Text="Country : "></asp:Label>
							<asp:DropDownList ID="ddlCountry" runat="server" Width="200px"></asp:DropDownList>
						</td>
					</tr>
					<tr valign="top">
						<td class="tdInput">

						</td>
						<td class="tdInput">
							<asp:ImageButton ID="btnOK2" runat="server" OnClick="OnClickOK" ImageUrl="~/Images/Button/btnOK.png" />
							&nbsp;&nbsp;
							<asp:ImageButton ID="btnCancel" runat="server" ImageUrl="~/Images/Button/btnCancel.png" OnClick="OnClickCancel" />
						</td>
					</tr>
					<tr valign="top">
						<td class="tdInput">

						</td>
					</tr>
					<tr valign="top">
						<td class="tdInput">

						</td>
					</tr>
				</table>
			</td>
		</tr>
	</table>
</asp:Content>
