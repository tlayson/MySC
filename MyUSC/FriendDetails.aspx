<%@ Page Title="" Language="C#" MasterPageFile="~/USC.Master" AutoEventWireup="true" CodeBehind="FriendDetails.aspx.cs" Inherits="MyUSC.FriendDetails" %>
<%@ MasterType VirtualPath="~/USC.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
	<script type="text/javascript" src="~/js/GA.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
	<table style="width: 1060px; height: 650px; background-image: url('Images/Background.png')">
		<tr valign="top" style="height: 50px" >
			<td class="tdInput">
				<table width="100%">
					<tr valign="top">
						<td class="tdInput">
							<asp:Label ID="Label1" runat="server" Text="Friend Profile" CssClass="lgSiteColorTxt"></asp:Label>
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
							<table width="100%" class="tblSectionThinBorder">
								<tr valign="top">
									<td class="tdInput">
										<table width="100%">
											<tr valign="top">
												<td class="tdInput" colspan="3">
													<asp:Label ID="lblUserNameLabel" runat="server" Text="UserName : " CssClass="smSiteColorTxt"></asp:Label>
													<asp:Label ID="lblUserNameValue" runat="server" CssClass="smNormalTxt"></asp:Label>
												</td>
											</tr>
											<tr valign="top">
												<td class="tdInput" colspan="3">
													<asp:Label ID="lblNameLabel" runat="server" Text="Name : " CssClass="smSiteColorTxt"></asp:Label>
													<asp:Label ID="lblNameValue" runat="server" CssClass="smNormalTxt"></asp:Label>
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
												<td class="tdInput" colspan="3">
													<asp:Label ID="Label10" runat="server" Text="Sports Interests : " CssClass="smSiteColorTxt"></asp:Label>
													<asp:Label ID="lblSportsInterestValue" runat="server" CssClass="smNormalTxt"></asp:Label>
												</td>
											</tr>
											<tr valign="top">
												<td class="tdInput" colspan="3">
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
