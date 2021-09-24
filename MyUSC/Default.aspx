<%@ Page Title="Managing Sports Teams for Free | MySportsConnect - Home Page" Language="C#" MasterPageFile="~/USC.master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="MyUSC._Default" %>
<%@ MasterType VirtualPath="~/USC.Master" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
	<script type="text/javascript" src="~/js/GA.js"></script>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
	<table style="width: 1060px; height: 650px; background-image: url('Images/Background/Background.png')">
		<!-- Header -->
		<tr class="trNormal">
			<td>
				<table class="tblNormal">
					<tr class="trVATop">
						<td class="tdInput" colspan="3">
							<h1>The Social Platform for Sports Fans</h1>
						</td>
					</tr>
					<tr class="trVATop">
						<td class="tdInput">
							<span class="xlgSiteColorTxt">Welcome back!</span>
						</td>
						<td class="tdInput" align="right" style="width: 150px;">
							<asp:LinkButton ID="btnNewMsgs" runat="server" PostBackUrl="~/Friends.aspx?Section=MsgBoard">New Messages</asp:LinkButton>
						</td>
						<td class="tdInput" align="right" style="width: 150px">
							<asp:LinkButton ID="btnFriendRequests" runat="server" PostBackUrl="~/Friends.aspx?Section=FriendRequests">Friend Requests</asp:LinkButton>
						</td>
					</tr>
				</table>
			</td>
		</tr>
		<!-- Body -->
		<tr valign="top">
			<td>
				<table class="tblNormal">
					<tr class="trVATop">
						<td class="tdInput">
							<table width="100%">
								<tr>
									<td>
										<table class="tblNormal">
											<tr>
												<td class="tdInput">
													<table class="trNormal">
														<tr class="trNormal">
															<td class="tdHomeButton">
																<asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Images/Button/MyNews_wc.jpg" OnClick="OnClickHomeNews" CssClass="btnHomePage" />
															</td>
															<td class="tdHomeButton">
																<asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/Images/Button/MyTeams_wc.jpg" OnClick="OnClickHomeTeams" CssClass="btnHomePage" />
															</td>
															<td class="tdHomeButton">
																<asp:ImageButton ID="ImageButton3" runat="server" ImageUrl="~/Images/Button/MyConnections_wc.jpg" OnClick="OnClickHomeFriends" CssClass="btnHomePage" />
															</td>
														</tr>
													</table>
												</td>
											</tr>
											<tr>
												<td class="tdInput">
													<asp:Panel ID="pnlAnnounce" runat="server">
														<table class="tblNormal">
															<tr class="trNormal">
																<td>
																	<asp:Label ID="lblAnnouncements" CssClass="xlgSiteColorTxt" runat="server" Text="Announcements"></asp:Label><br />
																	<asp:Label ID="lblAnnounceText" CssClass="medNormalTxt" runat="server" Text="No new announcements at this time."></asp:Label>
																</td>
															</tr>
															<tr class="trVATop">
																<td class="tdInput">
																	<hr class="lgNormalTxt" />
																</td>
															</tr>
														</table>
													</asp:Panel>
												</td>
											</tr>
											<tr class="trVATop">
												<td class="tdInput">
													<asp:Label ID="lblTopStories" runat="server" Text="Today's Top Stories" CssClass="xlgSiteColorTxt"></asp:Label>
												</td>
											</tr>
											<tr>
												<td class="tdInput">
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
								<tr>
									<td>
										<asp:Panel ID="pnlError" runat="server" Visible="False">
											<asp:Label ID="lblDisplayError" CssClass="lgSiteColorTxt" runat="server" Text=""></asp:Label>
										</asp:Panel>
									</td>
								</tr>
							</table>
						</td>
					</tr>
				</table>
			</td>
		</tr>
		<!-- Footer -->
		<tr class="trNormal">
			<td>
				<table class="tblNormal">
					<tr class="trVATop">
						<td class="tdInput">
							<asp:CheckBox ID="chkMakeDefaultPage" runat="server" Text="Make This My Home Page" AutoPostBack="True" OnCheckedChanged="OnClickMakeHome" />
						</td>
					</tr>
				</table>
			</td>
		</tr>
	</table>
</asp:Content>
