<%@ Page Title="" Language="C#" MasterPageFile="~/USC.Master" AutoEventWireup="true" CodeBehind="TestStuff.aspx.cs" Inherits="MyUSC.TestStuff" %>
<%@ MasterType VirtualPath="~/USC.Master" %>
<%@ Register Assembly="MyUSC" Namespace="MyUSC.Classes" TagPrefix="MSC" %>
<%@ Register Src="~/Classes/DateSelect.ascx" TagPrefix="uc1" TagName="DateSelect" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
	<table width="100%">
		<tr>
			<td>
				<MSC:VenueType ID="ddlVenueType" runat="server"></MSC:VenueType>
			</td>
		</tr>
		<tr>
			<td>
				<asp:Button ID="btnTest" runat="server" Text="Test" OnClick="OnClickTest" /><br/><br/>

				<asp:Button ID="btnRedirect" runat="server" Text="Redirect" OnClick="OnClickRedirect"/><br/><br/>

				<asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Images/Button/btnEdit.png" />
				<asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/Images/Button/btnEditSelection.png" />
				<asp:ImageButton ID="ImageButton3" runat="server" ImageUrl="~/Images/Button/btnEditSection.png" />
				<asp:ImageButton ID="ImageButton4" runat="server" ImageUrl="~/Images/Button/Button Template Example 1.png" />
				<asp:ImageButton ID="ImageButton5" runat="server" ImageUrl="~/Images/Button/Button Template Example 1.png" />
				<asp:ImageButton ID="ImageButton6" runat="server" ImageUrl="~/Images/Button/Button Template Example 1.png" />
				<asp:ImageButton ID="ImageButton7" runat="server" ImageUrl="~/Images/Button/Button Template Example 1.png" />
			</td>
		</tr>
	</table>

	<table width="1050px">
		<tr>
			<td>

			</td>
			<td>

			</td>
		</tr>
		<tr>
			<td>
				<uc1:DateSelect runat="server" ID="dsDateSelect" />
			</td>
			<td>
				<table width="100%">
					<tr>
						<td>

						</td>
					</tr>
				</table>
			</td>
		</tr>
	</table>
</asp:Content>
