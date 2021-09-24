<%@ Page Title="" Language="C#" MasterPageFile="~/MyTeams/OrgMaster.master" AutoEventWireup="true" CodeBehind="MemberInfo.aspx.cs" Inherits="MyUSC.MyTeams.MemberInfo" %>
<%@ MasterType VirtualPath="~/MyTeams/OrgMaster.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="OrgHeadContent" runat="server">
	<script type="text/javascript" src="~/js/GA.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="OrgContent" runat="server">
	<table style="width: 1060px; height: 650px; background-image: url('Images/Background.png')">
		<tr valign="top">
			<td class="tdInput">
				<table width="100%">
					<tr valign="top">
						<td class="tdInput">
							<table width="100%" class="tblSectionThinBorder">
								<tr valign="top">
									<td class="tdInput">
										<table width="100%">
											<tr valign="top">
												<td class="tdInput">
													<asp:Label ID="lblUserNameLabel" runat="server" Text="UserName : " CssClass="smSiteColorTxt"></asp:Label>
													<asp:Label ID="lblUserNameValue" runat="server" CssClass="smNormalTxt"></asp:Label>
												</td>
											</tr>
											<tr valign="top">
												<td class="tdInput">
													<asp:Label ID="lblNameLabel" runat="server" Text="Name : " CssClass="smSiteColorTxt"></asp:Label>
													<asp:Label ID="lblNameValue" runat="server" CssClass="smNormalTxt"></asp:Label>
												</td>
											</tr>
											<tr valign="top">
												<td class="tdInput">
													<asp:Label ID="Label9" runat="server" Text="Nickname : " CssClass="smSiteColorTxt"></asp:Label>
													<asp:Label ID="lblNickname" runat="server" CssClass="smNormalTxt"></asp:Label>
												</td>
											</tr>
											<tr>
												<td>
													<table id="tblContactInfo" width="100%" style="visibility: visible">
														<tr valign="top">
															<td class="tdInput" colspan="3">
																<asp:Label ID="Label5" runat="server" Text="Contact Info" CssClass="smSiteColorTxt"></asp:Label>
															</td>
														</tr>
														<tr valign="top">
															<td class="tdInput" colspan="3">
																<asp:Label ID="Label3" runat="server" Text="Address : " CssClass="smSiteColorTxt"></asp:Label>
																<asp:Label ID="lblAddress" runat="server" CssClass="smNormalTxt">This section only visible to team members</asp:Label>
															</td>
														</tr>
														<tr valign="top">
															<td class="tdInput">
																<asp:Label ID="Label2" runat="server" Text="City : " CssClass="smSiteColorTxt"></asp:Label>
																<asp:Label ID="lblCityValue" runat="server" CssClass="smNormalTxt"></asp:Label>
															</td>
															<td class="tdInput">
																<asp:Label ID="Label4" runat="server" Text="State : " CssClass="smSiteColorTxt"></asp:Label>
																<asp:Label ID="lblStateValue" runat="server" CssClass="smNormalTxt"></asp:Label>
															</td>
															<td class="tdInput">
																<asp:Label ID="Label6" runat="server" Text="Zip : " CssClass="smSiteColorTxt"></asp:Label>
																<asp:Label ID="lblZipValue" runat="server" CssClass="smNormalTxt"></asp:Label>
															</td>
														</tr>
														<tr valign="top">
															<td class="tdInput" colspan="3">
																<asp:Label ID="Label8" runat="server" Text="Country : " CssClass="smSiteColorTxt"></asp:Label>
																<asp:Label ID="lblCountryValue" runat="server" CssClass="smNormalTxt"></asp:Label>
															</td>
														</tr>
														<tr valign="top">
															<td class="tdInput">
																<asp:Label ID="Label10" runat="server" Text="Phone : " CssClass="smSiteColorTxt"></asp:Label>
																<asp:Label ID="lblPhone" runat="server" CssClass="smNormalTxt"></asp:Label>
															</td>
															<td class="tdInput" colspan="2">
																<asp:Label ID="Label7" runat="server" Text="eMail : " CssClass="smSiteColorTxt"></asp:Label>
																<asp:Label ID="lbleMail" runat="server" CssClass="smNormalTxt"></asp:Label>
															</td>
														</tr>
													</table>
												</td>
											</tr>
											<tr valign="top">
												<td class="tdInput">
													<asp:Label ID="Label12" runat="server" Text="Image : " CssClass="smSiteColorTxt"></asp:Label>
													<asp:Image ID="imgUserPhoto" runat="server" ImageUrl="~/Images/NoPhoto.JPG" Height="150px" Width="150px" />
												</td>
											</tr>
										</table>
									</td>
								</tr>
							</table>
						</td>
					</tr>
					<tr valign="top">
						<td class="tdInput">

						</td>
					</tr>
				</table>
			</td>
		</tr>
		<tr valign="top">
			<td class="tdInput">
				<table width="100%">
					<tr valign="top">
						<td class="tdInput">

						</td>
					</tr>
				</table>
			</td>
		</tr>
	</table>
</asp:Content>
