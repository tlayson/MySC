<%@ Page Title="" Language="C#" MasterPageFile="~/USC.Master" AutoEventWireup="true" CodeBehind="Account.aspx.cs" Inherits="MyUSC.AccountPage" %>
<%@ MasterType VirtualPath="~/USC.Master" %>
<%@ Register assembly="CuteEditor, Version=6.6.0.0, Culture=neutral, PublicKeyToken=3858aa6802b1223a" namespace="CuteEditor" tagprefix="CE" %>
<%@ Register Src="~/Classes/DateSelect.ascx" TagPrefix="uc1" TagName="DateSelect" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
	<script type="text/javascript" src="~/js/GA.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
	<table style="width: 1060px; height: 650px; background-image: url('Images/Background.png')">
        <tr style="height: 50px">
            <td class="acctCol1">
                <span class="xlgSiteColorTxt">MY ACCOUNT</span>
            </td>
            <td align="right">
				<table width="100%">
					<tr>
						<td class="tdInput">
							<asp:Label ID="lblMessage" runat="server" CssClass="medSiteColorTxt" Visible="False"></asp:Label>
						</td>
						<td class="tdInput">
							<asp:ImageButton ID="btnUpdateAccount" ImageUrl="~/Images/Button/btnUpdate.png" runat="server" OnClick="OnClickUpdateAccount" />
						</td>
					</tr>
				</table>
			</td>
        </tr>
        <tr>
            <td valign="top" class="acctCol1">
			<!-- Account wizard menu  -->
                <table width="100%">
                    <tr>
						<td>
                            <asp:Image ID="imgWizStepName" ImageUrl="~/Images/Button/WizStepSel.jpg" runat="server" />
						</td>
                        <td class="lgNormalTxt">
							<asp:LinkButton ID="btnMenuName" runat="server" ForeColor="White" OnClick="OnMenuNameClickX">Name</asp:LinkButton><br />
                        </td>
                    </tr>
                    <tr>
						<td>
                            <asp:Image ID="imgWizStepAddress" ImageUrl="~/Images/Button/WizStepSel.jpg" runat="server" />
						</td>
                        <td class="lgNormalTxt">
							<asp:LinkButton ID="btnMenuAddress" runat="server" ForeColor="White" OnClick="OnMenuAddressClickX">Address</asp:LinkButton><br />
                        </td>
                    </tr>
                    <tr>
						<td>
                            <asp:Image ID="imgWizStepSports" ImageUrl="~/Images/Button/WizStepSel.jpg" runat="server" />
						</td>
                        <td class="lgNormalTxt">
							<asp:LinkButton ID="btnMenuSports" runat="server" ForeColor="White" OnClick="OnMenuSportsClickX">Favorite Sports</asp:LinkButton><br />
                        </td>
                    </tr>
                    <tr>
						<td>
                            <asp:Image ID="imgWizStepPrefs" ImageUrl="~/Images/Button/WizStepSel.jpg" runat="server" />
						</td>
                        <td class="lgNormalTxt">
							<asp:LinkButton ID="btnMenuPrefs" runat="server" ForeColor="White" OnClick="OnMenuPrefsClickX">Preferences</asp:LinkButton><br />
                        </td>
                    </tr>
                    <tr>
						<td>
                            <asp:Image ID="imgWizStepPhoto" ImageUrl="~/Images/Button/WizStepSel.jpg" runat="server" />
						</td>
                        <td class="lgNormalTxt">
							<asp:LinkButton ID="btnMenuPhoto" runat="server" ForeColor="White" OnClick="OnMenuPhotoClickX">Photo</asp:LinkButton><br />
                        </td>
                    </tr>
                </table>
            </td>
            <td valign="top">
				<!-- Name Panel -->
				<asp:Panel ID="pnlAccountName" runat="server">
					<table width="100%">
						<tr>
							<td>
								<table width="100%">
									<tr>
										<td class="tdInput">
											<asp:Label ID="lblUserName" runat="server" Text="User Name" ToolTip="Minimum 3 characters. Letters and numbers only."></asp:Label><br />
											<asp:TextBox ID="txtUserName" runat="server" Width="200px" MaxLength="50" ReadOnly="True"></asp:TextBox>
										</td>
										<td class="tdInput" align="center">
											<asp:ImageButton ID="btnChangePswd" ImageUrl="~/Images/Button/btnChangePswd.png" runat="server" OnClick="OnClickChangePswd" />
										</td>
									</tr>
								</table>
							</td>
						</tr>
						<tr>
							<td class="tdInput">
								<asp:Table ID="tblNewInvitee" runat="server" Width="100%">
									<asp:TableRow>
										<asp:TableCell>
											<asp:Label ID="Label2" runat="server" Text="* Password"></asp:Label><br />
											<asp:TextBox ID="txtPassword" runat="server" Width="200px" MaxLength="50" TextMode="Password"></asp:TextBox>
										</asp:TableCell>
										<asp:TableCell>
											<asp:Label ID="Label3" runat="server" Text="* Confirm Password"></asp:Label><br />
											<asp:TextBox ID="txtPswdConfirm" runat="server" Width="200px" MaxLength="50" TextMode="Password"></asp:TextBox>
										</asp:TableCell>
									</asp:TableRow>
								</asp:Table>
							</td>
						</tr>
						<tr>
							<td>
								<table width="100%">
									<tr>
										<td class="tdInput">
											<asp:Label ID="lblTitle" runat="server" Text="Title"></asp:Label><br />
											<asp:TextBox ID="txtTitle" runat="server" Width="75px" MaxLength="50"></asp:TextBox>
										</td>
										<td class="tdInput">
											<asp:Label ID="lblFirstName" runat="server" Text="First Name"></asp:Label><br />
											<asp:TextBox ID="txtFirstName" runat="server" Width="500px" MaxLength="50"></asp:TextBox>
										</td>
									</tr>
								</table>
							</td>
						</tr>
						<tr>
							<td>
								<table width="100%">
									<tr>
										<td class="tdInput">
											<asp:Label ID="lblMI" runat="server" Text="MI"></asp:Label><br />
											<asp:TextBox ID="txtMI" runat="server" Width="40px" MaxLength="1"></asp:TextBox>
										</td>
										<td class="tdInput">
											<asp:Label ID="lblLastName" runat="server" Text="Last Name"></asp:Label><br />
											<asp:TextBox ID="txtLastName" runat="server" Width="500px" MaxLength="50"></asp:TextBox>
										</td>
										<td class="tdInput">
											<asp:Label ID="lblSuffix" runat="server" Text="Suffix"></asp:Label><br />
											<asp:TextBox ID="txtSuffix" runat="server" Width="75px" MaxLength="50"></asp:TextBox>
										</td>
									</tr>
								</table>
							</td>
						</tr>
						<tr>
							<td>
								<table width="100%">
									<tr>
										<td class="tdInput">
											<asp:Label ID="lblEmail" runat="server" Text="Email Address"></asp:Label><br />
											<asp:TextBox ID="txtEmail" runat="server" Width="375px" MaxLength="50"></asp:TextBox>
										</td>
										<td class="tdInput">
											<asp:Label ID="lblBirthDate" runat="server" Text="Birth Date"></asp:Label><br />
											<uc1:DateSelect runat="server" ID="dsBirthDay" />
											<asp:TextBox ID="txtBirthDate" runat="server" Width="375px" MaxLength="50" ReadOnly="True"></asp:TextBox>
										</td>
									</tr>
								</table>
							</td>
						</tr>
						<tr>
							<td>
								<table width="100%">
									<tr>
										<td class="tdInput" colspan="3">
											<asp:Label ID="lblSecurityQuestion" runat="server" Text="Security Question"></asp:Label><br />
											<asp:TextBox ID="txtSecurityQuestion" runat="server" Width="750px" MaxLength="50"></asp:TextBox>
										</td>
									</tr>
									<tr>
										<td class="tdInput" colspan="3">
											<asp:Label ID="lblSecurityAnswer" runat="server" Text="Security Answer"></asp:Label><br />
											<asp:TextBox ID="txtSecurityAnswer" runat="server" Width="750px" MaxLength="50"></asp:TextBox>
										</td>
									</tr>
									<tr>
										<td class="tdInput" style="width: 500px">

										</td>
										<td class="tdInput" style="width: 150px" align="center">
										</td>
										<td class="tdInput" style="width: 150px" align="center">
											<asp:ImageButton ID="btnAcctNameNext" ImageUrl="~/Images/Button/btnNext.png" runat="server" OnClick="OnBtnAcctNameNext" />
										</td>
									</tr>
								</table>
							</td>
						</tr>
					</table>
				</asp:Panel>
				<!-- Address Panel -->
				<asp:Panel ID="pnlAccountAddress" runat="server">
					<table width="100%">
						<tr>
							<td>
								<table width="100%">
									<tr>
										<td class="tdInput" colspan="3">
											<asp:Label ID="lblAddress1" runat="server" Text="Address 1"></asp:Label><br />
											<asp:TextBox ID="txtAddress1" runat="server" Width="750px" MaxLength="50"></asp:TextBox>
										</td>
									</tr>
									<tr>
										<td class="tdInput" colspan="3">
											<asp:Label ID="lblAddress2" runat="server" Text="Address 2"></asp:Label><br />
											<asp:TextBox ID="txtAddress2" runat="server" Width="750px" MaxLength="50"></asp:TextBox>
										</td>
									</tr>
									<tr>
										<td class="tdInput">
											<asp:Label ID="lblCity" runat="server" Text="City"></asp:Label><br />
											<asp:TextBox ID="txtCity" runat="server" Width="200px" MaxLength="50"></asp:TextBox>
										</td>
										<td class="tdInput">
											<asp:Label ID="lblPostalCode" runat="server" Text="Postal Code"></asp:Label><br />
											<asp:TextBox ID="txtPostalCode" runat="server" Width="200px" MaxLength="50"></asp:TextBox>
										</td>
										<td class="tdInput">
											<asp:Label ID="lblState" runat="server" Text="State"></asp:Label><br />
											<asp:DropDownList ID="ddlState" Width="200px" runat="server"></asp:DropDownList>
										</td>
									</tr>
								</table>
							</td>
						</tr>
						<tr>
							<td>
								<table width="100%">
									<tr>
										<td class="tdInput" colspan="2">
											<asp:Label ID="lblCountry" runat="server" Text="Country"></asp:Label><br />
											<asp:DropDownList ID="ddlCountry" Width="200px" runat="server"></asp:DropDownList>
										</td>
									</tr>
									<tr>
										<td class="tdInput">
										</td>
										<td class="tdInput">
										</td>
									</tr>
								</table>
							</td>
						</tr>
						<tr>
							<td>
								<table width="100%">
									<tr>
										<td class="tdInput" style="width: 500px">

										</td>
										<td class="tdInput" style="width: 150px" align="center">
											<asp:ImageButton ID="btnAcctAddressPrev" ImageUrl="~/Images/Button/btnPrevious.png" runat="server" OnClick="OnBtnAcctAddressPrev"  />
										</td>
										<td class="tdInput" style="width: 150px" align="center">
											<asp:ImageButton ID="btnAcctAddressNext" ImageUrl="~/Images/Button/btnNext.png" runat="server" OnClick="OnBtnAcctAddressNext" />
										</td>
									</tr>
								</table>
							</td>
						</tr>
					</table>
				</asp:Panel>
				<!-- Favorite Sports Panel -->
				<asp:Panel ID="pnlAccountFavSports" runat="server">
					<table width="100%">
						<tr>
							<td class="tdInput" colspan="3">
								<span class="smSiteColorTxt">Enter Your Sports Interests</span><br />
								<asp:TextBox ID="txtSportsInterests" runat="server" Height="50px" TextMode="MultiLine" Width="800px" MaxLength="500"></asp:TextBox>
							</td>
						</tr>
						<tr>
							<td class="tdInput" colspan="3">
								<span class="smSiteColorTxt">Pick The Sports You Want To Show On Sports News Menu List</span>
							</td>
						</tr>
						<tr>
							<td class="tdInput">
								<asp:CheckBox ID="chkAcctMLB" Text="MLB" runat="server" />
							</td>
							<td class="tdInput">
								<asp:CheckBox ID="chkAcctNASCAR" Text="NASCAR" runat="server" />
							</td>
							<td class="tdInput">
								<asp:CheckBox ID="chkAcctCFL" Text="CFL" runat="server" />
							</td>
						</tr>
						<tr>
							<td class="tdInput">
								<asp:CheckBox ID="chkAcctNBA" Text="NBA" runat="server" />
							</td>
							<td class="tdInput">
								<asp:CheckBox ID="chkAcctPGA" Text="PGA" runat="server" />
							</td>
							<td class="tdInput">
								<asp:CheckBox ID="chkAcctYouthSports" Text="Youth Sports" runat="server" />
							</td>
						</tr>
						<tr>
							<td class="tdInput">
								<asp:CheckBox ID="chkAcctNFL" Text="NFL" runat="server" />
							</td>
							<td class="tdInput">
								<asp:CheckBox ID="chkAcctLPGA" Text="LPGA" runat="server" />
							</td>
							<td class="tdInput">
								<asp:CheckBox ID="chkAcctOlmpics" Text="Olympics" runat="server" />
							</td>
						</tr>
						<tr>
							<td class="tdInput">
								<asp:CheckBox ID="chkAcctMLS" Text="MLS" runat="server" />
							</td>
							<td class="tdInput">
								<asp:CheckBox ID="chkAcctPBA" Text="PBA" runat="server" />
							</td>
							<td class="tdInput">
								<asp:CheckBox ID="chkAcctNCAASoftball" Text="NCAA Softball" runat="server" />
							</td>
						</tr>
						<tr>
							<td class="tdInput">
								<asp:CheckBox ID="chkAcctNHL" Text="NHL" runat="server" />
							</td>
							<td class="tdInput">
								<asp:CheckBox ID="chkAcctNCAAFootball" Text="NCAA Football" runat="server" />
							</td>
							<td class="tdInput">
								<asp:CheckBox ID="chkAcctExtremeSports" Text="Extreme Sports" runat="server" />
							</td>
						</tr>
						<tr>
							<td class="tdInput">
								<asp:CheckBox ID="chkAcctWNBA" Text="WNBA" runat="server" />
							</td>
							<td class="tdInput">
								<asp:CheckBox ID="chkAcctUFC" Text="UFC" runat="server" />
							</td>
							<td class="tdInput">
								<asp:CheckBox ID="chkAcctOther" Text="Other" runat="server" />
							</td>
						</tr>
						<tr>
							<td colspan="3">
								<table width="100%">
									<tr>
										<td class="tdInput" style="width: 500px">
										</td>
										<td class="tdInput" style="width: 150px" align="center">
											<asp:ImageButton ID="btnAcctSportsPrev" ImageUrl="~/Images/Button/btnPrevious.png" runat="server" OnClick="OnBtnAcctSportsPrev"/>
										</td>
										<td class="tdInput" style="width: 150px" align="center">
											<asp:ImageButton ID="btnAcctSportsNext" ImageUrl="~/Images/Button/btnNext.png" runat="server" OnClick="OnBtnAcctSportsNext"/>
										</td>
									</tr>
								</table>
							</td>
						</tr>

					</table>
				</asp:Panel>
				<!-- Preferences Panel -->
				<asp:Panel ID="pnlAccountPrefs" runat="server">
					<table width="100%">
						<tr>
							<td class="tdInput" colspan="2">
								<span class="smSiteColorTxt">
									Select Your Home Page
								</span>
							</td>
						</tr>
						<tr>
							<td class="tdInput" style="width: 200px">
								<asp:CheckBox ID="chkAcctFriends" Text="Friends" runat="server" OnCheckedChanged="OnCheckFriends" />
							</td>
							<td class="tdInput" style="width: 700px">
								<asp:CheckBox ID="chkAcctSportsNews" Text="Sports News" runat="server" OnCheckedChanged="OnCheckSportsNews" />
							</td>
						</tr>
						<tr>
							<td class="tdInput" colspan="2">
								<span class="smSiteColorTxt">
									Other Preferences
								</span>
							</td>
						</tr>
						<tr>
							<td class="tdInput" colspan="2">
								<asp:CheckBox ID="chkDisableDeleteFriends" Text="Disable Delete Friends Dialog Box" runat="server" />
							</td>
						</tr>
						<tr>
							<td class="tdInput" colspan="2">
								<asp:CheckBox ID="chkDisableDeleteFriendsMsgs" Text="Disable Delete Friends Messages Dialog Box" runat="server" />
							</td>
						</tr>
						<tr>
							<td class="tdInput" colspan="2">
								<asp:CheckBox ID="chkReceiveCommentEmails" Text="Receive Comment Emails" runat="server" />
							</td>
						</tr>
						<tr>
							<td class="tdInput" colspan="2">
								<span class="smSiteColorTxt">
									Promotions
								</span>
							</td>
						</tr>
						<tr>
							<td class="tdInput" colspan="2">
								<asp:CheckBox ID="chkOffersFromUs" Text="Offers From Us" runat="server" />
							</td>
						</tr>
						<tr>
							<td class="tdInput" colspan="2">
								<asp:CheckBox ID="chkOffersFromPartners" Text="Offers From Our Partners" runat="server" />
							</td>
						</tr>
						<tr>
							<td colspan="2">
								<table width="100%">
									<tr>
										<td class="tdInput" style="width: 500px">
										</td>
										<td class="tdInput" style="width: 150px" align="center">
											<asp:ImageButton ID="btnAcctPrefsPrev" ImageUrl="~/Images/Button/btnPrevious.png" runat="server" OnClick="OnBtnAcctPrefsPrev"/>
										</td>
										<td class="tdInput" style="width: 150px" align="center">
											<asp:ImageButton ID="btnAcctPrefsNext" ImageUrl="~/Images/Button/btnNext.png" runat="server" OnClick="OnBtnAcctPrefsNext"/>
										</td>
									</tr>
								</table>
							</td>
						</tr>
					</table>
				</asp:Panel>
				<!-- Photo Panel -->
				<asp:Panel ID="pnlAccountPhoto" runat="server">
					<table width="100%">
						<tr>
							<td class="tdInput">
								<span class="smSiteColorTxt">Upload or Update Your Photo</span>
							</td>
						</tr>
						<tr>
							<td>
								<table width="75%">
									<tr>
										<td class="tdInput">
											<asp:Image ID="imgUserPhoto" ImageUrl="~/Images/NoPhoto.JPG" runat="server" Height="200px" Width="200px" />
										</td>
										<td class="tdInput">
											If your photo does not appear in<br />
											the friends page, click the refresh<br />
											button on your browser.
										</td>
									</tr>
								</table>
							</td>
						</tr>
						<tr>
							<td>
								<table width="75%">
									<tr>
										<td class="tdInput">
											<asp:FileUpload ID="fulUserPhoto" runat="server" BackColor="#666666" Font-Bold="True" ForeColor="White" Width="600px" />
										</td>
										<td class="tdInput">
											<asp:Button ID="btnUpload" runat="server" Text="Upload" OnClick="OnClickUpload" />
										</td>
									</tr>
								</table>
							</td>
						</tr>
						<tr>
							<td>
								<table width="75%">
									<tr>
										<td class="tdInput" align="right" >
											Deactiveate your account?
											<asp:ImageButton ID="btnDeactivateAcct" ImageUrl="~/Images/Button/btnDeactivate.png" runat="server" />
										</td>
									</tr>
								</table>
							</td>
						</tr>
						<tr>
							<td colspan="2">
								<table width="100%">
									<tr>
										<td class="tdInput" style="width: 500px">
										</td>
										<td class="tdInput" style="width: 150px" align="center">
											<asp:ImageButton ID="btnAcctPhotoPrev" ImageUrl="~/Images/Button/btnPrevious.png" runat="server" OnClick="OnBtnAcctPhotoPrev"/>
										</td>
										<td class="tdInput" style="width: 150px" align="center">
										</td>
									</tr>
								</table>
							</td>
						</tr>
					</table>
				</asp:Panel>
            </td>
        </tr>
        <tr>
            <td class="acctCol1">

            </td>
            <td>

            </td>
        </tr>
    </table>
</asp:Content>
