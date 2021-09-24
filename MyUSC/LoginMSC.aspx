<%@ Page Title="" Language="C#" MasterPageFile="~/USC.Master" AutoEventWireup="true" CodeBehind="LoginMSC.aspx.cs" Inherits="MyUSC.LoginMSC" %>
<%@ MasterType VirtualPath="~/USC.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
	<script type="text/javascript" src="~/js/GA.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
		<asp:UpdatePanel runat="server">
        <ContentTemplate>
        <asp:Panel ID="pnlMain" runat="server" Width="1060px" Height="650px" CssClass="pnlNormal" ForeColor="White">
            <table style="width: 1060px; height: 650px">
                <tr valign="top">
                    <td style="width: 550px">

                    </td>
                    <td>
                        <table width="100%" style="height: 650px">
                            <tr valign="top">
                                <td class="tdInput" style="height: 40px">
									Not a member yet?  No problem.  Sign up for your FREE account today!<br /><br />
                                </td>
                            </tr>
                            <tr valign="top" align="center">
                                <td class="tdInput" style="height: 30px">
									<asp:ImageButton ID="btnNewAccount" ImageUrl="~/Images/Button/btnCreateAccount.png" runat="server" OnClick="OnClickNewAccount" />
                                </td>
                            </tr>
                            <tr valign="top">
                                <td style="height: 250px">
                                    <span style="font-size: 60px; font-weight: bolder; color: #FF0000">MySportsConnect.net</span><br />
                                    <span style="font-size: 60px; font-weight: bolder; color: #FFFFFF">Manage your teams for free.</span><br />
									<asp:Image ID="Image1" runat="server" ImageUrl="~/Images/Notice.jpg" />
									<asp:Label ID="Label1" runat="server" Text="!!Important notice!!  " CssClass="lgErrorTxt"></asp:Label>
									<asp:Label ID="Label2" runat="server" Text="This site requires cookies.  If you are having difficulty logging in, please open your browser's internet options and allow the use of cookies."></asp:Label>
									&nbsp;
									<asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/Help/CookieHelp.aspx">Click here for help enabling cookies</asp:HyperLink>
                                </td>
                            </tr>
                            <tr valign="top">
                                <td>
									<MSC:TBSCPanel runat="server" ID="pnlMSCLogin" DefaultButton="btnLogin">
										<table>
											<tr>
												<td colspan="4">
													<asp:Label ID="lblLoginError" runat="server" Text="The username you specified was not found.  Please try again or create a new account." CssClass="tdError"></asp:Label>
												</td>
											</tr>
											<tr valign="top" align="left">
												<td style="padding: 5px">
													Username
												</td>
												<td style="padding: 5px">
													<asp:TextBox ID="txtUserName" runat="server" BackColor="#CCCCCC"></asp:TextBox>
												</td>
												<td style="width: 5px">
												</td>
												<td style="padding: 5px">

												</td>
											</tr>
											<tr valign="top" align="left">
												<td style="padding: 5px">
													Password
												</td>
												<td style="padding: 5px">
													<asp:TextBox ID="txtPassword" runat="server" TextMode="Password" BackColor="#CCCCCC"></asp:TextBox><br />
												</td>
												<td style="width: 5px">
												</td>
												<td style="padding: 5px" valign="middle">
													<asp:ImageButton ID="btnLogin" runat="server" ImageUrl="~/Images/Button/btnSignIn.png" OnClick="btnLoginOnClick" />
												</td>
											</tr>
											<tr valign="top" align="left">
												<td colspan="2" style="padding: 5px">
													<asp:CheckBox ID="chkKeepLoggedIn" runat="server" Text="Keep me signed in" ToolTip="If this is not working for you, you may need to enable your browser to use cookies." />
												</td>
												<td style="width: 5px">
												</td>
												<td style="padding: 5px" valign="middle">
													<asp:ImageButton ID="btnForgotPswd" ImageUrl="/Images/Button/btnForgotPswd.png" runat="server" OnClick="btnForgotPswdOnClick" />
												</td>
											</tr>
											<tr valign="top">
												<td class="tdInput" colspan="4">
												</td>
											</tr>
										</table>
									</MSC:TBSCPanel>
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
