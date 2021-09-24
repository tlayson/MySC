<%@ Page Title="" Language="C#" MasterPageFile="~/MyTeams/OrgMaster.master" AutoEventWireup="true" CodeBehind="FindVenue.aspx.cs" Inherits="MyUSC.MyTeams.FindVenue" %>
<%@ MasterType VirtualPath="~/MyTeams/OrgMaster.Master" %>
<%@ Register Assembly="MyUSC" Namespace="MyUSC.Classes" TagPrefix="MSC" %>
<%@ Register src="../Classes/VenueResultsTable.ascx" tagname="VenueResultsTable" tagprefix="MSC" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %> 
<asp:Content ID="Content1" ContentPlaceHolderID="OrgHeadContent" runat="server">
	<script type="text/javascript" src="~/js/GA.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="OrgContent" runat="server">
	<table style="width: 800px; vertical-align: top;">
		<tr valign="top" class="trAdmin">
			<td class="tdInput">
				<asp:Label ID="Label6" runat="server" Text="Search for a venue to add to your team/organization.  " CssClass="smSiteColorTxt"></asp:Label>
			</td>
			<td class="tdRight">
				<table width="100%">
					<tr>
						<td>
							<asp:Button ID="btnNewVenue" runat="server" Text="New Venue" OnClick="OnClickNewVenue" Visible="False" CommandArgument="Test" />
						</td>
						<td>
							<asp:Button ID="btnDone" runat="server" Text="Done" OnClick="OnClickDone" Width="100px" />
						</td>
					</tr>
				</table>
			</td>
		</tr>
		<tr valign="top" style="height: 45px">
			<td class="tdInput">
				<asp:Label ID="Label1" runat="server" Text="Name : "></asp:Label>
				<asp:TextBox ID="txtVenueName" runat="server" MaxLength="50" Width="300px"></asp:TextBox>
			</td>
			<td class="tdInput">
				<asp:Label ID="Label2" runat="server" Text="Type : "></asp:Label>
				<MSC:VenueType ID='ddlVenueType' runat="server"></MSC:VenueType>
			</td>
		</tr>
		<tr valign="top" style="height: 45px">
			<td class="tdInput">
				<asp:Label ID="Label3" runat="server" Text="City : "></asp:Label>
				<asp:TextBox ID="txtVenueCity" runat="server" MaxLength="50" Width="300px"></asp:TextBox>
			</td>
			<td class="tdInput">
				<asp:Label ID="Label4" runat="server" Text="State : "></asp:Label>
				<MSC:StateDropDown ID="ddlVenueState" runat="server" AutoPostBack="False"></MSC:StateDropDown>
			</td>
		</tr>
		<tr valign="top" style="height: 45px">
			<td class="tdInput">
				<asp:Label ID="Label5" runat="server" Text="Country : "></asp:Label>
				<MSC:CountryDropDown ID="ddlVenueCountry" runat="server" AutoPostBack="False"></MSC:CountryDropDown>
			</td>
			<td class="tdInput">

			</td>
		</tr>
		<tr valign="top" style="height: 45px">
			<td class="tdInput">

			</td>
			<td class="tdRight">
				<asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="OnClickSearch" />
			</td>
		</tr>

	</table>
	<div>
		<asp:Panel ID="pnlResults" runat="server" Visible="False">
			<MSC:VenueResultsTable ID="vrt1" runat="server"></MSC:VenueResultsTable>
		</asp:Panel>
	</div>
</asp:Content>
