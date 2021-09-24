<%@ Page Title="" Language="C#" MasterPageFile="~/USC.Master" AutoEventWireup="true" CodeBehind="Advertise.aspx.cs" Inherits="MyUSC.Advertise" %>
<%@ MasterType VirtualPath="~/USC.Master" %>
<%@ Register assembly="CuteEditor, Version=6.6.0.0, Culture=neutral, PublicKeyToken=3858aa6802b1223a" namespace="CuteEditor" tagprefix="CE" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
	<script type="text/javascript" src="~/js/GA.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
			<asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:Panel ID="pnlAdvertise" runat="server" Width="1060px" Height="650px" BackImageUrl="~/Images/Background.png" CssClass="pnlNormal">
			<table width="100%">
				<tr>
					<td style="width: 50px"></td>
					<td>
						<table width="100%">
							<tr>
								<td colspan="2">
									<span class="xlgSiteColorTxt">Advertising Request</span>
								</td>
							</tr>
							<tr>
								<td valign="top">
									<table width="100%">
										<tr>
											<td class="tdInput" colspan="2">
												If you would like to advertise with us, please give us your contact infomation and we will get back to you as soon as possible.<br />
                                                At MySportsConnect, we pride ourselves on helping you get the most out of your advertising budget. Your ads will be targeted
                                                based on the demographics you specify.
											</td>
										</tr>
										<tr>
											<td class="tdInput" colspan="2">
												<asp:Label ID="lblCompanyName" runat="server" Text="Company Name *"></asp:Label><br />
												<asp:TextBox ID="txtCompanyName" runat="server" Width="400px"></asp:TextBox><br />
											</td>
										</tr>
										<tr>
											<td class="tdInput" colspan="2">
												<asp:Label ID="lblCompanyAddress" runat="server" Text="Address"></asp:Label><br />
												<asp:TextBox ID="txtCompanyAddress" runat="server" Width="400px"></asp:TextBox><br />
											</td>
										</tr>
										<tr>
											<td class="tdInput" colspan="2">
												<asp:Label ID="lblCity" runat="server" Text="City"></asp:Label><br />
												<asp:TextBox ID="txtCompanyCity" runat="server" Width="400px"></asp:TextBox><br />
											</td>
										</tr>
										<tr>
											<td class="tdInput">
												<asp:Label ID="lblState" runat="server" Text="State"></asp:Label><br />
												<asp:DropDownList ID="ddlState" runat="server"></asp:DropDownList>
											</td>
											<td class="tdInput">
												<asp:Label ID="lblZipcode" runat="server" Text="Postal Code"></asp:Label><br />
												<asp:TextBox ID="txtCompanyZip" runat="server" Width="100px"></asp:TextBox><br />
											</td>
										</tr>
										<tr>
											<td class="tdInput" colspan="2">
												<asp:Label ID="lblCompanyWebsite" runat="server" Text="Company Website"></asp:Label><br />
												<asp:TextBox ID="txtCompanyWebsite" runat="server" Width="400px"></asp:TextBox><br />
											</td>
										</tr>
									</table>
								</td>
								<td valign="top">
									<table width="100%">
										<tr>
											<td class="tdInput" colspan="2">
												<asp:Label ID="lblContactFirstName" runat="server" Text="Contact First Name *"></asp:Label><br />
												<asp:TextBox ID="txtContactFirstName" runat="server" Width="400px"></asp:TextBox><br />
											</td>
										</tr>
										<tr>
											<td class="tdInput" colspan="2">
												<asp:Label ID="lblContactLastName" runat="server" Text="Contact Last Name *"></asp:Label><br />
												<asp:TextBox ID="txtContactLastName" runat="server" Width="400px"></asp:TextBox><br />
											</td>
										</tr>
										<tr>
											<td class="tdInput" colspan="2">
												<asp:Label ID="lblEmail" runat="server" Text="Email Address *"></asp:Label><br />
												<asp:TextBox ID="txtEmail" runat="server" Width="400px"></asp:TextBox><br />
											</td>
										</tr>
										<tr>
											<td class="tdInput">
												<asp:Label ID="lblWorkPhone" runat="server" Text="Work Phone"></asp:Label><br />
												<asp:TextBox ID="txtWorkPhone" runat="server" Width="180px"></asp:TextBox><br />
											</td>
											<td class="tdInput">
												<asp:Label ID="lblCellPhone" runat="server" Text="Cell Phone"></asp:Label><br />
												<asp:TextBox ID="txtCellPhone" runat="server" Width="180px"></asp:TextBox><br />
											</td>
										</tr>
										<tr>
											<td class="tdInput" colspan="2">
												<asp:Label ID="lblComments" runat="server" Text="Comments"></asp:Label><br />
												<asp:TextBox ID="txtComments" runat="server" Width="400px" Height="47px" TextMode="MultiLine"></asp:TextBox><br />
											</td>
										</tr>
										<tr>
											<td class="tdInput" colspan="2">
												<table width="100%">
													<tr>
														<td class="tdInput" align="center">
                                                            <asp:ImageButton ID="btnSubmit" ImageUrl="~/Images/Button/btnSubmit.png" runat="server" OnClick="OnClickBtnSubmit" />
														</td>
														<td class="tdInput" align="center">
                                                            <asp:ImageButton ID="btnCancel" ImageUrl="~/Images/Button/btnCancel.png" runat="server" OnClick="OnClickBtnCancel" />
														</td>
														<td class="tdInput" align="center">
                                                            <asp:ImageButton ID="btnLogin" ImageUrl="~/Images/Button/btnSignIn.png" runat="server" OnClick="OnClickBtnLogin" />
														</td>
													</tr>
												</table>
											</td>
										</tr>
									</table>
								</td>
							</tr>
							<tr>
								<td class="tdInput">
									<asp:Label ID="lblLocalAdvertising" runat="server" Text="Local Advertising"  CssClass="xlgSiteColorTxt"></asp:Label><span class="smSiteColorTxt"></span>
									<asp:CheckBox ID="chkLocalAdvertising" runat="server" />
								</td>
								<td class="tdInput">
									<asp:Label ID="lblNationalAdvertising" runat="server" Text="National Advertising" CssClass="xlgSiteColorTxt"></asp:Label><span class="smSiteColorTxt"></span>
									<asp:CheckBox ID="chkNationalAdvertising" runat="server" />
								</td>
							</tr>
                            <tr>
                                <td class="tdInput" colspan="2">
                                    * Indicates a required field.
                                </td>
                            </tr>
						</table>
					</td>
				</tr>
			</table>
            </asp:Panel>
        </ContentTemplate>
                    </asp:UpdatePanel>
</asp:Content>

