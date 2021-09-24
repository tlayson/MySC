<%@ Page Title="" Language="C#" MasterPageFile="~/USC.Master" AutoEventWireup="true" CodeBehind="CreateAccount.aspx.cs" Inherits="MyUSC.CreateAccountPage" %>
<%@ MasterType VirtualPath="~/USC.Master" %>
<%@ Register Src="~/Classes/DateSelect.ascx" TagPrefix="uc1" TagName="DateSelect" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
	<script type="text/javascript" src="~/js/GA.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <table style="width: 1060px; height: 650px; background-image: url('Images/Background.png')">
        <tr style="height: 50px">
			<td class="tdInput">
				<asp:Label ID="lblInstructions" runat="server" Text="* indicates a required field"></asp:Label>
			</td>
            <td align="right">
				<asp:ImageButton ID="btnCreateAccount" ImageUrl="~/Images/Button/btnCreateAccount.png" runat="server" OnClick="OnClickCreateAccount" />&nbsp;
            </td>
        </tr>
        <tr>
            <td valign="top" colspan="2">
				<!-- Name Panel -->
				<asp:Panel ID="pnlAccountName" runat="server">
					<table width="100%">
						<tr>
							<td>
								<table width="100%">
									<tr>
										<td class="tdInput">
											<asp:Label ID="lblUserName" runat="server" Text="* User Name" ToolTip="Minimum 5 characters. Letters and numbers only."></asp:Label><br />
											<asp:TextBox ID="txtUserName" runat="server" Width="200px" MaxLength="50"></asp:TextBox>
										</td>
										<td class="tdInput">
											<asp:Label ID="lblPswd" runat="server" Text="* Password"></asp:Label><br />
											<asp:TextBox ID="txtPassword" runat="server" Width="200px" MaxLength="50" TextMode="Password"></asp:TextBox>
										</td>
										<td class="tdInput">
											<asp:Label ID="lblPswdConfirm" runat="server" Text="* Confirm Password"></asp:Label><br />
											<asp:TextBox ID="txtPswdConfirm" runat="server" Width="200px" MaxLength="50" TextMode="Password"></asp:TextBox>
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

										</td>
									</tr>
									<tr>
										<td class="tdInput">
											<asp:Label ID="lblTitle" runat="server" Text="Title"></asp:Label><br />
											<asp:TextBox ID="txtTitle" runat="server" Width="75px" MaxLength="50"></asp:TextBox>
										</td>
										<td class="tdInput">
											<asp:Label ID="lblFirstName" runat="server" Text="* First Name"></asp:Label><br />
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
											<asp:Label ID="lblEmail" runat="server" Text="* Email Address"></asp:Label><br />
											<asp:TextBox ID="txtEmail" runat="server" Width="375px" MaxLength="50"></asp:TextBox>
										</td>
										<td class="tdInput">
											<asp:Label ID="lblBirthDate" runat="server" Text="* Birth Date"></asp:Label>
											&nbsp;&nbsp;&nbsp;&nbsp;
											<asp:CheckBox ID="chkCertifyAge" Text="* I certify that I am at least 14 years old." runat="server" />
											<br />
											<uc1:DateSelect runat="server" id="dsBirthDay" />
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
											<asp:Label ID="lblSecurityQuestion" runat="server" Text="* Security Question"></asp:Label><br />
											<asp:TextBox ID="txtSecurityQuestion" runat="server" Width="750px" MaxLength="50"></asp:TextBox>
										</td>
									</tr>
									<tr>
										<td class="tdInput" colspan="3">
											<asp:Label ID="lblSecurityAnswer" runat="server" Text="* Security Answer"></asp:Label><br />
											<asp:TextBox ID="txtSecurityAnswer" runat="server" Width="750px" MaxLength="50"></asp:TextBox>
										</td>
									</tr>
									<tr>
										<td class="tdInput" colspan="3" align="right">
											<asp:ImageButton ID="btnCreateAccount2" ImageUrl="~/Images/Button/btnCreateAccount.png" runat="server" OnClick="OnClickCreateAccount" />&nbsp;
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
            <td colspan="2">

            </td>
        </tr>
    </table>
</asp:Content>
