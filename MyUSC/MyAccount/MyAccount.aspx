<%@ Page Title="" Language="C#" MasterPageFile="~/USC.Master" AutoEventWireup="true" CodeBehind="MyAccount.aspx.cs" Inherits="MyUSC.MyAccount.MyAccount" %>
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
							<span class="medSiteColorTxt">
								Update your profile...
							</span>
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
											<tr>
												<td align="left">
													<asp:Label ID="lblNameSectionHead" runat="server" Text="Name" CssClass="lgSiteColorTxt"></asp:Label>
												</td>
												<td align="right">
													<asp:ImageButton ID="btnNameSection" runat="server" ImageUrl="~/Images/Button/btnEditSection.png" OnClick="OnClickName" />
												</td>
												<td class="tdInput" style="width: 175px">

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
													<asp:Label ID="lblEMailLabel" runat="server" Text="Email : " CssClass="smSiteColorTxt"></asp:Label>
													<asp:Label ID="lblEMailValue" runat="server" CssClass="smNormalTxt"></asp:Label>
												</td>
											</tr>
											<tr valign="top">
												<td class="tdInput">
													<asp:Label ID="lblSecurityQuestionLabel" runat="server" Text="Security Question : " CssClass="smSiteColorTxt"></asp:Label>
													<asp:Label ID="lblSecurityQuestionValue" runat="server" CssClass="smNormalTxt"></asp:Label>
												</td>
											</tr>
											<tr valign="top">
												<td class="tdInput">
													<asp:Label ID="lblSecurityAnswerLabel" runat="server" Text="Security Answer : " CssClass="smSiteColorTxt"></asp:Label>
													<asp:Label ID="lblSecurityAnswerValue" runat="server" CssClass="smNormalTxt"></asp:Label>
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
							<table width="100%" class="tblSectionThinBorder">
								<tr valign="top">
									<td class="tdInput">
										<table width="100%">
											<tr>
												<td align="left">
													<asp:Label ID="lblAddressSectionHead" runat="server" Text="Address" CssClass="lgSiteColorTxt"></asp:Label>
												</td>
												<td align="right">
													<asp:ImageButton ID="btnAddressSection" runat="server" ImageUrl="~/Images/Button/btnEditSection.png" OnClick="OnClickAddress" />
												</td>
												<td class="tdInput" style="width: 175px">

												</td>
											</tr>
										</table>
									</td>
								</tr>
								<tr valign="top">
									<td class="tdInput">
										<table width="100%">
											<tr valign="top">
												<td class="tdInput" colspan="3">
													<asp:Label ID="lblAddress1Label" runat="server" Text="Address1 : " CssClass="smSiteColorTxt"></asp:Label>
													<asp:Label ID="lblAddress1Value" runat="server" CssClass="smNormalTxt"></asp:Label>
												</td>
											</tr>
											<tr valign="top">
												<td class="tdInput" colspan="3">
													<asp:Label ID="lblAddress2Label" runat="server" Text="Address2 : " CssClass="smSiteColorTxt"></asp:Label>
													<asp:Label ID="lblAddress2Value" runat="server" CssClass="smNormalTxt"></asp:Label>
												</td>
											</tr>
											<tr valign="top">
												<td class="tdInput">
													<asp:Label ID="lblCityLabel" runat="server" Text="City : " CssClass="smSiteColorTxt"></asp:Label>
													<asp:Label ID="lblCityValue" runat="server" CssClass="smNormalTxt"></asp:Label>
												</td>
												<td class="tdInput">
													<asp:Label ID="lblStateLabel" runat="server" Text="State : " CssClass="smSiteColorTxt"></asp:Label>
													<asp:Label ID="lblStateValue" runat="server" CssClass="smNormalTxt"></asp:Label>
												</td>
												<td class="tdInput">
													<asp:Label ID="lblZipLabel" runat="server" Text="Zip : " CssClass="smSiteColorTxt"></asp:Label>
													<asp:Label ID="lblZipValue" runat="server" CssClass="smNormalTxt"></asp:Label>
												</td>
											</tr>
											<tr valign="top">
												<td class="tdInput" colspan="3">
													<asp:Label ID="lblCountryLabel" runat="server" Text="Country : " CssClass="smSiteColorTxt"></asp:Label>
													<asp:Label ID="lblCountryValue" runat="server" CssClass="smNormalTxt"></asp:Label>
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
							<table width="100%" class="tblSectionThinBorder">
								<tr valign="top">
									<td class="tdInput">
										<table width="100%">
											<tr>
												<td align="left">
													<asp:Label ID="lblSportsSectionHead" runat="server" Text="Sports" CssClass="lgSiteColorTxt"></asp:Label>
												</td>
												<td align="right">
													<asp:ImageButton ID="btnSportsSection" runat="server" ImageUrl="~/Images/Button/btnEditSection.png" OnClick="OnClickSports" />
												</td>
												<td class="tdInput" style="width: 175px">

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
													<asp:Label ID="lblSportsInterestLabel" runat="server" Text="Sports Interests : " CssClass="smSiteColorTxt"></asp:Label>
													<asp:Label ID="lblSportsInterestValue" runat="server" CssClass="smNormalTxt"></asp:Label>
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
							<table width="100%" class="tblSectionThinBorder">
								<tr valign="top">
									<td class="tdInput">
										<table width="100%">
											<tr>
												<td align="left">
													<asp:Label ID="lblPrefsSectionHead" runat="server" Text="Preferences" CssClass="lgSiteColorTxt"></asp:Label>
												</td>
												<td align="right">
													<asp:ImageButton ID="btnPrefsSection" runat="server" ImageUrl="~/Images/Button/btnEditSection.png" OnClick="OnClickPreferences" />
												</td>
												<td class="tdInput" style="width: 175px">

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
													<asp:Label ID="lblStartPageLabel" runat="server" Text="Start Page : " CssClass="smSiteColorTxt"></asp:Label>
													<asp:Label ID="lblStartPageValue" runat="server" CssClass="smNormalTxt"></asp:Label>
												</td>
											</tr>
											<tr valign="top">
												<td class="tdInput">
													<asp:Label ID="lblPromoLabel" runat="server" Text="Receive Promotions : " CssClass="smSiteColorTxt"></asp:Label>
												</td>
											</tr>
											<tr valign="top">
												<td class="tdInput">
													&nbsp;&nbsp;
													<asp:Label ID="lblUsLabel" runat="server" Text="From Us : " CssClass="smSiteColorTxt"></asp:Label>
													<asp:Label ID="lblUsValue" runat="server" CssClass="smNormalTxt"></asp:Label>
												</td>
											</tr>
											<tr valign="top">
												<td class="tdInput">
													&nbsp;&nbsp;
													<asp:Label ID="lblPartnerLabel" runat="server" Text="From Partners : " CssClass="smSiteColorTxt"></asp:Label>
													<asp:Label ID="lblPartnerValue" runat="server" CssClass="smNormalTxt"></asp:Label>
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
							<table width="100%" class="tblSectionThinBorder">
								<tr valign="top">
									<td class="tdInput">
										<table width="100%">
											<tr>
												<td align="left">
													<asp:Label ID="lblPhotoSectionHead" runat="server" Text="User Photo" CssClass="lgSiteColorTxt"></asp:Label>
												</td>
												<td align="right">
													<asp:ImageButton ID="btnPhotoSection" runat="server" ImageUrl="~/Images/Button/btnEditSection.png" OnClick="OnClickPhoto" />
												</td>
												<td class="tdInput" style="width: 175px">

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
													<asp:Label ID="lblImageLabel" runat="server" Text="Image : " CssClass="smSiteColorTxt"></asp:Label>
													<asp:Image ID="imgUserPhoto" runat="server" ImageUrl="~/Images/NoPhoto.JPG" />
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
