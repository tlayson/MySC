<%@ Page Title="" Language="C#" MasterPageFile="~/USC.Master" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="MyUSC.MyTeams.Dashboard" %>
<%@ MasterType VirtualPath="~/USC.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
	<script type="text/javascript" src="~/js/GA.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
	<table class="tblDashboard">
		<tr class="trPageHeading">
			<td class="tdInput">
				<asp:Label ID="lblPageHead" runat="server" Text="My Teams Dashboard" CssClass="tdPageHeading"></asp:Label>
			</td>
		</tr>
		<tr valign="top">
			<td class="tdInput">
				<table width="100%">
					<tr valign="top">
						<td class="tdInput">
							<table width="100%">
								<tr valign="top">
									<td class="tdInput">
										
									</td>
									<td class="tdInput">
										<asp:ImageButton ID="btnFindOrg" runat="server" ImageUrl="~/Images/Button/btnFindOrg.png" OnClick="OnClickFindOrg" />
									</td>
									<td class="tdInput">
										 
									</td>
									<td class="tdInput">
										<asp:ImageButton ID="btnNewOrg" runat="server" ImageUrl="~/Images/Button/btnNewOrg.png" OnClick="OnClickNewOrg" />
									</td>
									<td class="tdInput">
										
									</td>
								</tr>
							</table>
						</td>
					</tr>
					<tr>
						<td>
							<table class="tblNormal">
								<tr class="trNormal">
									<td class="tdLeft">
										<table class="tblDashboardTeams">
											<tr valign="top">
												<td class="tdInput">
													<asp:Panel ID="pnlOwnedOrgs" runat="server">
														<table width="100%">
															<tr valign="top">
																<td class="tdInput">
																	<asp:Label ID="Label1" runat="server" Text="Owned Organizations" CssClass="tdSectionHeading"></asp:Label>
																</td>
															</tr>
															<tr valign="top">
																<td class="tdInput">
																	<asp:Table ID="tblOwnedOrgs" runat="server">

																	</asp:Table>
																</td>
															</tr>
														</table>
													</asp:Panel>
												</td>
											</tr>
											<tr valign="top">
												<td class="tdInput">
													<asp:Panel ID="pnlMemberOrgs" runat="server">
														<table width="100%">
															<tr valign="top">
																<td class="tdInput">
																	<asp:Label ID="Label2" runat="server" Text="Member Organizations" CssClass="tdSectionHeading"></asp:Label>
																</td>
															</tr>
															<tr valign="top">
																<td class="tdInput">
																	<asp:Table ID="tblMemberOrgs" runat="server">

																	</asp:Table>
																</td>
															</tr>
														</table>
													</asp:Panel>
												</td>
											</tr>
											<tr valign="top">
												<td class="tdInput">
													<asp:Panel ID="pnlFollowedOrgs" runat="server">
														<table width="100%">
															<tr valign="top">
																<td class="tdInput">
																	<asp:Label ID="Label3" runat="server" Text="Followed Organizations" CssClass="tdSectionHeading"></asp:Label>
																</td>
															</tr>
															<tr valign="top">
																<td class="tdInput">
																	<asp:Table ID="tblFollowedOrgs" runat="server">

																	</asp:Table>
																</td>
															</tr>
														</table>
													</asp:Panel>
												</td>
											</tr>
										</table>
									</td>
									<td class="tdInput">
<!--
										<table class="tblDashboardEventsList">
											<tr class="trNormal">
												<td class="tdInput">
													<table width="100%">
														<tr valign="top">
															<td class="tdInput">
																<asp:Label ID="Label4" runat="server" Text="Upcoming Events" CssClass="tdSectionHeading"></asp:Label>
															</td>
														</tr>
														<tr valign="top">
															<td class="tdInput">
																<table  class="tblDashboardEvent">
																	<tr class="trShort">
																		<td class="tdInput">
																			<asp:Label ID="Label6" runat="server" Text="Org Name"></asp:Label>
																		</td>
																		<td class="tdInput">
																			<asp:Label ID="Label7" runat="server" Text="Event Name"></asp:Label>
																		</td>
																		<td class="tdInput">
																			RSVP
																		</td>
																	</tr>
																	<tr class="trShort">
																		<td class="tdInput">
																			<asp:Label ID="Label8" runat="server" Text="Org Name"></asp:Label>
																		</td>
																		<td class="tdInput">
																			<asp:Label ID="Label9" runat="server" Text="Event Name"></asp:Label>
																		</td>
																		<td class="tdInput">
																			RSVP
																		</td>
																	</tr>
																	<tr class="trShort">
																		<td class="tdInput">
																			Date</td>
																		<td class="tdInput">
																			Time</td>
																		<td class="tdInput">
																			<asp:Label ID="Label11" runat="server" Text="Label"></asp:Label>
																		</td>
																	</tr>
																</table>
															</td>
														</tr>
														<tr valign="top">
															<td class="tdInput">
																<asp:Panel ID="pnlFutureEvents" runat="server">
																	<asp:Table ID="tblFutureEvents" runat="server"></asp:Table>
																</asp:Panel>
															</td>
														</tr>
													</table>
												</td>
											</tr>
											<tr class="trNormal">
												<td class="tdInput">
													<table width="100%">
														<tr valign="top">
															<td class="tdInput">
																<asp:Label ID="Label5" runat="server" Text="Past Events" CssClass="tdSectionHeading"></asp:Label>
															</td>
														</tr>
														<tr valign="top">
															<td class="tdInput">
																<asp:Panel ID="pnlPastEvents" runat="server">
																	<asp:Table ID="tblPastEvents" runat="server"></asp:Table>
																</asp:Panel>
															</td>
														</tr>
													</table>
												</td>
											</tr>
										</table>
-->
									</td>
								</tr>
							</table>
						</td>
					</tr>
				</table>
			</td>
		</tr>
	</table>
</asp:Content>
