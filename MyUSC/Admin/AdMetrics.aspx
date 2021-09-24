<%@ Page Title="" Language="C#" MasterPageFile="~/USC.Master" AutoEventWireup="true" CodeBehind="AdMetrics.aspx.cs" Inherits="MyUSC.Admin.AdMetrics" %>
<%@ MasterType VirtualPath="~/USC.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
	<table style="width: 1060px; height: 650px; background-image: url('Images/Background.png')">
		<tr valign="top">
			<td class="tdInput">
				<table width="100%">
					<tr valign="top">
						<td class="tdInput" colspan="2">
							<asp:Label ID="Label1" runat="server" Text="Advertising Metrics" CssClass="lgSiteColorTxt"></asp:Label>
						</td>
					</tr>
					<tr valign="top">
						<td class="tdInput">
							<asp:Label ID="Label2" runat="server" Text="Ad Impressions" CssClass="medSiteColorTxt"></asp:Label>
						</td>
						<td class="tdInput">
							<asp:Label ID="Label3" runat="server" Text="Ad Clicks" CssClass="medSiteColorTxt"></asp:Label>
						</td>
					</tr>
					<tr valign="top">
						<td class="tdInput">
			    			<asp:Xml ID="xmlImpressions" runat="server" TransformSource="~/Admin/AdImpressions.xslt" DocumentSource="~/App_Data/AdImpressions.xml"></asp:Xml>
						</td>
						<td class="tdInput">
			    			<asp:Xml ID="xmlClicks" runat="server" TransformSource="~/Admin/AdImpressions.xslt" DocumentSource="~/App_Data/AdResponses.xml"></asp:Xml>
						</td>
					</tr>
					<tr valign="top">
						<td class="tdInput">
							
						</td>
						<td class="tdInput">
							
						</td>
					</tr>
					<tr valign="top">
						<td class="tdInput">
							
						</td>
						<td class="tdInput">
							
						</td>
					</tr>
					<tr valign="top">
						<td class="tdInput">
							
						</td>
						<td class="tdInput">
							
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
