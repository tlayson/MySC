<%@ Page Title="" Language="C#" MasterPageFile="~/MyTeams/OrgMaster.master" AutoEventWireup="true" CodeBehind="VenueDetails.aspx.cs" Inherits="MyUSC.MyTeams.VenueDetails" %>
<%@ Register Assembly="MyUSC" Namespace="MyUSC.Classes" TagPrefix="MSC" %>
<%@ MasterType VirtualPath="~/MyTeams/OrgMaster.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="OrgHeadContent" runat="server">
	<script type="text/javascript" src="~/js/GA.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="OrgContent" runat="server">
	<asp:Panel ID="pnlDisplay" runat="server">
		<table style="width: 800px; vertical-align: top;">
			<tr class="trShort">
				<td>
					<asp:Label ID="lblViewName" runat="server" Text="" CssClass="medSiteColorTxt"></asp:Label>
				</td>
			</tr>
			<tr class="trShort">
				<td>
					<asp:Label ID="lblViewType" runat="server" Text="" CssClass="medSiteColorTxt"></asp:Label>
				</td>
			</tr>
			<tr class="trShort">
				<td>
					<asp:Label ID="lblViewAddress" runat="server" Text="" CssClass="medSiteColorTxt"></asp:Label>
				</td>
			</tr>
			<tr class="trShort">
				<td>
					<asp:Label ID="lblViewCity" runat="server" Text="" CssClass="medSiteColorTxt"></asp:Label>
				</td>
			</tr>
			<tr class="trShort">
				<td>
					<asp:Label ID="lblViewCountry" runat="server" Text="" CssClass="medSiteColorTxt"></asp:Label>
				</td>
			</tr>
			<tr class="trShort">
				<td>
					<asp:Label ID="lblViewPhone" runat="server" Text="" CssClass="medSiteColorTxt"></asp:Label>
				</td>
			</tr>
			<tr class="trShort">
				<td>
					<asp:Image ID="imgViewPhoto" runat="server" />
				</td>
			</tr>
			<tr class="trShort">
				<td>
					<asp:HyperLink ID="hlViewMap" runat="server"></asp:HyperLink>
				</td>
			</tr>
			<tr class="trShort">
				<td>
					<asp:Label ID="Label11" runat="server" Text="Notes" CssClass="medSiteColorTxt"></asp:Label>
				</td>
			</tr>
			<tr class="trShort">
				<td>
					<asp:TextBox ID="txtViewNotes" runat="server" Width="750px" Height="300px" ReadOnly="True" TextMode="MultiLine"></asp:TextBox>
				</td>
			</tr>
			<tr class="trShort">
				<td class="tdRight">
					<asp:Button ID="btnBack" runat="server" Text="Back" OnClick="OnClickBack" />
				</td>
			</tr>
		</table>
	</asp:Panel>
	<asp:Panel ID="pnlAdmin" runat="server">
		<table style="width: 800px; vertical-align: top;">
		<tr valign="top" style="height: 45px">
			<td class="tdInput">
				<table width="100%">
					<tr valign="top">
						<td class="tdInput">
							<asp:Label ID="Label1" runat="server" Text="Name : " CssClass="medSiteColorTxt"></asp:Label>
						</td>
						<td class="tdInput">
							<asp:TextBox ID="txtEditVenueName" runat="server" Width="400px" ReadOnly="True" MaxLength="250"></asp:TextBox>
						</td>
					</tr>
					<tr valign="top">
						<td class="tdInput">
							<asp:Label ID="Label14" runat="server" Text="Type : " CssClass="medSiteColorTxt"></asp:Label>
						</td>
						<td class="tdInput">
							<MSC:VenueType ID='ddlVenueType' runat="server"></MSC:VenueType>
						</td>
					</tr>
					<tr valign="top">
						<td class="tdInput">
							<asp:Label ID="Label2" runat="server" Text="Address : " CssClass="medSiteColorTxt"></asp:Label>
						</td>
						<td class="tdInput">
							<asp:TextBox ID="txtEditVenueAddress" runat="server" Width="400px" ReadOnly="True" MaxLength="50"></asp:TextBox>
						</td>
					</tr>
					<tr valign="top">
						<td class="tdInput">
							<asp:Label ID="Label10" runat="server" Text="Display Location : " CssClass="medSiteColorTxt"></asp:Label>
						</td>
						<td class="tdInput">
							<asp:TextBox ID="txtEditDisplayLoc" runat="server" Width="400px" ReadOnly="True" MaxLength="50"></asp:TextBox>
						</td>
					</tr>
					<tr valign="top">
						<td class="tdInput">
							<asp:Label ID="Label3" runat="server" Text="City : " CssClass="medSiteColorTxt"></asp:Label>
						</td>
						<td class="tdInput">
							<asp:TextBox ID="txtEditVenueCity" runat="server" Width="400px" ReadOnly="True" MaxLength="50"></asp:TextBox>
						</td>
					</tr>
					<tr valign="top">
						<td class="tdInput">
							<asp:Label ID="Label4" runat="server" Text="State" CssClass="medSiteColorTxt"></asp:Label>
						</td>
						<td class="tdInput">
							<MSC:StateDropDown ID="ddlState" runat="server"></MSC:StateDropDown>
						</td>
					</tr>
					<tr valign="top">
						<td class="tdInput">
							<asp:Label ID="Label5" runat="server" Text="Zip/Postal Code" CssClass="medSiteColorTxt"></asp:Label>
						</td>
						<td class="tdInput">
							<asp:TextBox ID="txtEditZip" runat="server" Width="200px" ReadOnly="True" MaxLength="50"></asp:TextBox>
						</td>
					</tr>
					<tr valign="top">
						<td class="tdInput">
							<asp:Label ID="Label12" runat="server" Text="Country" CssClass="medSiteColorTxt"></asp:Label>
						</td>
						<td class="tdInput">
							<MSC:CountryDropDown ID="ddlCountry" runat="server"></MSC:CountryDropDown>
						</td>
					</tr>
					<tr valign="top">
						<td class="tdInput">
							<asp:Label ID="Label6" runat="server" Text="MAP URL" CssClass="medSiteColorTxt"></asp:Label>
						</td>
						<td class="tdInput">
							<asp:TextBox ID="txtEditMapURL" runat="server" Width="400px" ReadOnly="True" MaxLength="250"></asp:TextBox>
						</td>
					</tr>
					<tr valign="top">
						<td class="tdInput">
							<asp:Label ID="Label7" runat="server" Text="Website" CssClass="medSiteColorTxt"></asp:Label>
						</td>
						<td class="tdInput">
							<asp:TextBox ID="txtEditWebsite" runat="server" Width="400px" ReadOnly="True" MaxLength="250"></asp:TextBox>
						</td>
					</tr>
					<tr valign="top">
						<td class="tdInput">
							<asp:Label ID="Label13" runat="server" Text="Picture URL" CssClass="medSiteColorTxt"></asp:Label>
						</td>
						<td class="tdInput">
							<asp:TextBox ID="txtEditImageURL" runat="server" Width="400px" ReadOnly="True" MaxLength="250"></asp:TextBox>
						</td>
					</tr>
					<tr valign="top">
						<td class="tdInput">
							<asp:Label ID="Label8" runat="server" Text="Phone" CssClass="medSiteColorTxt"></asp:Label>
						</td>
						<td class="tdInput">
							<asp:TextBox ID="txtEditPhone" runat="server" Width="200px" ReadOnly="True" MaxLength="50"></asp:TextBox>
						</td>
					</tr>
					<tr valign="top">
						<td class="tdInput" colspan="2">
							<asp:CheckBox ID="chkMakePublic" runat="server" Checked="True" CssClass="medSiteColorTxt" Text="Public Venue" ToolTip="Selecting this will make this venue available to all organizations to view.  If other organizations are using this venue, it can not be made private again." />
						</td>
					</tr>
					<tr valign="top">
						<td class="tdInput" colspan="2">
							<asp:Label ID="Label9" runat="server" Text="Notes" CssClass="medSiteColorTxt"></asp:Label>
						</td>
					</tr>
					<tr valign="top">
						<td class="tdInput" colspan="2">
							<asp:TextBox ID="txtEditNotes" runat="server" Width="750px" Height="300px" ReadOnly="True" TextMode="MultiLine"></asp:TextBox>
						</td>
					</tr>
					<tr valign="top">
						<td class="tdRight" colspan="2">
							<asp:Button ID="btnEditBack" runat="server" Text="Back" OnClick="OnClickBack" />
						</td>
					</tr>
				</table>
			</td>
			<td class="tdInput" style="width: 40px">
				<asp:Button ID="btnEdit" runat="server" Text="Edit" OnClick="OnClickEditVenue" />
				<asp:Button ID="btnSave" runat="server" Text="Save" OnClick="OnClickSave" Visible="False" />
			</td>
			<td class="tdInput" style="width: 40px">
				<asp:Button ID="btnNew" runat="server" Text="New" OnClick="OnClickNewVenue" />
				<asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="OnClickCancel" Visible="False" />
			</td>
		</tr>
	</table>
	</asp:Panel>
</asp:Content>
