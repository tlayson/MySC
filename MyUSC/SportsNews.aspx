<%@ Page Title="" Language="C#" MasterPageFile="~/USC.Master" AutoEventWireup="true" CodeBehind="SportsNews.aspx.cs" Inherits="MyUSC.SportsNews" %>
<%@ MasterType VirtualPath="~/USC.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
	<script type="text/javascript" src="~/js/GA.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
	<table style="width: 1050px; height: 650px; background-image: url('Images/Background/Background.png')">
		<!-- Header -->
		<tr valign="top" style="height: 40px">
			<td>
				<table width="100%">
					<tr valign="top">
						<td class="tdInput" valign="top">
							<span class="xlgSiteColorTxt">SPORTS NEWS</span>
						</td>
						<td class="tdInput" valign="top" align="right" style="width: 150px;">
							<asp:LinkButton ID="btnNewMsgs" runat="server" PostBackUrl="~/Friends.aspx?Section=MsgBoard">New Messages</asp:LinkButton>
						</td>
						<td class="tdInput" valign="top" align="right" style="width: 150px">
							<asp:LinkButton ID="btnFriendRequests" runat="server" PostBackUrl="~/Friends.aspx?Section=FriendRequests">Friend Requests</asp:LinkButton>
						</td>
					</tr>
				</table>
			</td>
		</tr>
		<!-- Body -->
		<tr valign="top">
			<td>
				<table width="100%" style="height: 570px">
					<tr valign="top">
						<td class="tdInput" valign="top" style="width: 75px">
							<asp:Menu ID="NewSportsMenu"
							Orientation="Vertical" 
							staticsubmenuindent="10" 
							MaximumDynamicDisplayLevels="4" 
							StaticDisplayLevels="1" 
							runat="server"
							>
								<StaticMenuItemStyle CssClass="miNewsNormal" />
								<StaticSelectedStyle BackColor="Red" ForeColor="White" BorderColor="White" VerticalPadding="2" BorderWidth="1" />
								<StaticHoverStyle backcolor="LightSkyBlue"/>
								<DynamicMenuItemStyle BackColor="#CCFFFF" VerticalPadding="2px" HorizontalPadding="15" BorderStyle="None"></DynamicMenuItemStyle>
								<DynamicSelectedStyle BackColor="Red" ForeColor="White" BorderColor="LightSkyBlue" VerticalPadding="2" BorderWidth="1" />
								<DynamicHoverStyle BackColor="#CCFFFF" BorderStyle="None"></DynamicHoverStyle>
							</asp:Menu>
						</td>
						<td class="tdInput" valign="top">
								<table width="100%">
									<tr>
										<td>

										</td>
									</tr>
									<tr>
										<td>
											<table class="tblNormal">
												<tr>
													<td class="tdInput">
														<asp:Label ID="lblName" CssClass="xlgSiteColorTxt" runat="server" Text="Name"></asp:Label><br />
														<asp:Label ID="lblParent" CssClass="medSiteColorTxt" runat="server" Text="Parent"></asp:Label>
													</td>
													<td class="tdInput">
														<asp:Image ID="imgLogo" runat="server" /><br />
													</td>
												</tr>
												<tr>
													<td colspan="2" class="tdInput">
														<asp:Panel ID="pnlDisplayXML" runat="server">
			    											<asp:Xml ID="xmlRSSDisplay" runat="server" TransformSource="~/RSSDisplay.xslt"></asp:Xml>
														</asp:Panel>
														<asp:Panel ID="pnlDisplayHTML" runat="server" Visible="False">
														</asp:Panel>
													</td>
												</tr>
											</table>
										</td>
									</tr>
								</table>
							<asp:Panel ID="pnlError" runat="server" Visible="False">
								<asp:Label ID="lblDisplayError" CssClass="lgSiteColorTxt" runat="server" Text=""></asp:Label>
							</asp:Panel>
						</td>
					</tr>
				</table>
			</td>
		</tr>
		<!-- Footer -->
		<tr valign="top" style="height: 40px">
			<td>
				<table width="100%">
					<tr valign="top">
						<td class="tdInput" valign="top">
							<asp:CheckBox ID="chkMakeDefaultPage" runat="server" OnCheckedChanged="OnCheckMakeDefaultPage" Text="Make This My Home Page" AutoPostBack="True" />
						</td>
					</tr>
				</table>
			</td>
		</tr>
	</table>
</asp:Content>
