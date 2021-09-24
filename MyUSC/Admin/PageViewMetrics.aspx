<%@ Page Title="" Language="C#" MasterPageFile="~/USC.Master" AutoEventWireup="true" CodeBehind="PageViewMetrics.aspx.cs" Inherits="MyUSC.Admin.PageViewMetrics" %>
<%@ MasterType VirtualPath="~/USC.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
	<table style="width: 800px; background-image: url('Images/Background.png')">
		<tr valign="top">
			<td class="tdInput">
				<table width="100%">
					<tr valign="top">
						<td class="tdInput" colspan="2">
							<asp:Label ID="Label1" runat="server" Text="Page View Metrics" CssClass="lgSiteColorTxt"></asp:Label>
						</td>
					</tr>
					<tr valign="top">
						<td class="tdInput" colspan="2">
							<asp:Label ID="Label4" runat="server" Text="Total page views last" CssClass="medSiteColorTxt"></asp:Label>
						</td>
					</tr>
					<tr valign="top">
						<td class="tdPVLabel">
							<asp:Label ID="Label2" runat="server" Text="24 Hrs" CssClass="medSiteColorTxt"></asp:Label>
						</td>
						<td class="tdPVValue">
							<asp:Label ID="lblPV1Day" runat="server" Text="" CssClass="medNormalTxt"></asp:Label>
						</td>
					</tr>
					<tr valign="top">
						<td class="tdPVLabel">
							<asp:Label ID="Label5" runat="server" Text="3 Days" CssClass="medSiteColorTxt"></asp:Label>
						</td>
						<td class="tdPVValue">
							<asp:Label ID="lblPV3Day" runat="server" Text="" CssClass="medNormalTxt"></asp:Label>
						</td>
					</tr>
					<tr valign="top">
						<td class="tdPVLabel">
							<asp:Label ID="Label7" runat="server" Text="5 Days" CssClass="medSiteColorTxt"></asp:Label>
						</td>
						<td class="tdPVValue">
							<asp:Label ID="lblPV5Day" runat="server" Text="" CssClass="medNormalTxt"></asp:Label>
						</td>
					</tr>
					<tr valign="top">
						<td class="tdPVLabel">
							<asp:Label ID="Label9" runat="server" Text="7 Days" CssClass="medSiteColorTxt"></asp:Label>
						</td>
						<td class="tdPVValue">
							<asp:Label ID="lblPV7Day" runat="server" Text="" CssClass="medNormalTxt"></asp:Label>
						</td>
					</tr>
					<tr valign="top">
						<td class="tdPVLabel">
							<asp:Label ID="Label11" runat="server" Text="30 Days" CssClass="medSiteColorTxt"></asp:Label>
						</td>
						<td class="tdPVValue">
							<asp:Label ID="lblPV30Day" runat="server" Text="" CssClass="medNormalTxt"></asp:Label>
						</td>
					</tr>
					<tr valign="top">
						<td class="tdInput">
							
						</td>
						<td class="tdInput">
							
						</td>
					</tr>
					<tr valign="top">
						<td class="tdInput" colspan="2">
							<asp:Label ID="Label3" runat="server" Text="Total MyTeams page views last" CssClass="medSiteColorTxt"></asp:Label>
						</td>
					</tr>
					<tr valign="top">
						<td class="tdPVLabel">
							<asp:Label ID="Label10" runat="server" Text="24 Hrs" CssClass="medSiteColorTxt"></asp:Label>
						</td>
						<td class="tdPVValue">
							<asp:Label ID="lblMTPV1Day" runat="server" Text="" CssClass="medNormalTxt"></asp:Label>
						</td>
					</tr>
					<tr valign="top">
						<td class="tdPVLabel">
							<asp:Label ID="Label15" runat="server" Text="3 Days" CssClass="medSiteColorTxt"></asp:Label>
						</td>
						<td class="tdPVValue">
							<asp:Label ID="lblMTPV3Day" runat="server" Text="" CssClass="medNormalTxt"></asp:Label>
						</td>
					</tr>
					<tr valign="top">
						<td class="tdPVLabel">
							<asp:Label ID="Label19" runat="server" Text="5 Days" CssClass="medSiteColorTxt"></asp:Label>
						</td>
						<td class="tdPVValue">
							<asp:Label ID="lblMTPV5Day" runat="server" Text="" CssClass="medNormalTxt"></asp:Label>
						</td>
					</tr>
					<tr valign="top">
						<td class="tdPVLabel">
							<asp:Label ID="Label21" runat="server" Text="7 Days" CssClass="medSiteColorTxt"></asp:Label>
						</td>
						<td class="tdPVValue">
							<asp:Label ID="lblMTPV7Day" runat="server" Text="" CssClass="medNormalTxt"></asp:Label>
						</td>
					</tr>
					<tr valign="top">
						<td class="tdPVLabel">
							<asp:Label ID="Label23" runat="server" Text="30 Days" CssClass="medSiteColorTxt"></asp:Label>
						</td>
						<td class="tdPVValue">
							<asp:Label ID="lblMTPV30Day" runat="server" Text="" CssClass="medNormalTxt"></asp:Label>
						</td>
					</tr>
					<tr valign="top">
						<td class="tdInput">
							
						</td>
						<td class="tdInput">
							
						</td>
					</tr>
				</table>
			</td>
			<td class="tdInput">
				<table width="100%">
					<tr valign="top">
						<td class="tdInput" colspan="2">
						</td>
					</tr>
					<tr valign="top">
						<td class="tdInput" colspan="2">
							<asp:Label ID="Label6" runat="server" Text="Unique users last" CssClass="medSiteColorTxt"></asp:Label>
						</td>
					</tr>
					<tr valign="top">
						<td class="tdPVLabel">
							<asp:Label ID="Label8" runat="server" Text="24 Hrs" CssClass="medSiteColorTxt"></asp:Label>
						</td>
						<td class="tdPVValue">
							<asp:Label ID="lblUU1Day" runat="server" Text="" CssClass="medNormalTxt"></asp:Label>
						</td>
					</tr>
					<tr valign="top">
						<td class="tdPVLabel">
							<asp:Label ID="Label12" runat="server" Text="3 Days" CssClass="medSiteColorTxt"></asp:Label>
						</td>
						<td class="tdPVValue">
							<asp:Label ID="lblUU3Day" runat="server" Text="" CssClass="medNormalTxt"></asp:Label>
						</td>
					</tr>
					<tr valign="top">
						<td class="tdPVLabel">
							<asp:Label ID="Label14" runat="server" Text="5 Days" CssClass="medSiteColorTxt"></asp:Label>
						</td>
						<td class="tdPVValue">
							<asp:Label ID="lblUU5Day" runat="server" Text="" CssClass="medNormalTxt"></asp:Label>
						</td>
					</tr>
					<tr valign="top">
						<td class="tdPVLabel">
							<asp:Label ID="Label16" runat="server" Text="7 Days" CssClass="medSiteColorTxt"></asp:Label>
						</td>
						<td class="tdPVValue">
							<asp:Label ID="lblUU7Day" runat="server" Text="" CssClass="medNormalTxt"></asp:Label>
						</td>
					</tr>
					<tr valign="top">
						<td class="tdPVLabel">
							<asp:Label ID="Label18" runat="server" Text="30 Days" CssClass="medSiteColorTxt"></asp:Label>
						</td>
						<td class="tdPVValue">
							<asp:Label ID="lblUU30Day" runat="server" Text="" CssClass="medNormalTxt"></asp:Label>
						</td>
					</tr>
					<tr valign="top">
						<td class="tdInput">
							
						</td>
						<td class="tdInput">
							
						</td>
					</tr>
				</table>
			</td>
		</tr>
	</table>
</asp:Content>
