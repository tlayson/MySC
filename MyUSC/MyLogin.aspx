<%@ Page Title="" Language="C#" MasterPageFile="~/USC.Master" AutoEventWireup="true" CodeBehind="MyLogin.aspx.cs" Inherits="MyUSC.MyLogin" %>
<%@ MasterType VirtualPath="~/USC.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
		<asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
        <asp:Panel ID="pnlMain" runat="server" CssClass="pnlNormal" ForeColor="White">
            <table class="tblNormal" width="500">
                <tr valign="top">
                    <td>
                        <table class="tblNormal" width="500">
                            <tr valign="top">
                                <td>
									<MSC:TBSCPanel runat="server" ID="pnlMSCLogin" DefaultButton="btnLogin">
										<table>
											<tr>
												<td colspan="4">
`				                                    <span class="xlgRedTxt">"Where serious fans go to meet."</span><br />
												</td>
											</tr>
											<tr>
												<td colspan="4">
													<asp:Label ID="lblLoginError" runat="server" Text="The username you specified was not found.  Please try again or create a new account." CssClass="tdError"></asp:Label>
												</td>
											</tr>
											<tr class="tdInput">
												<td class="tdInput">
													Username
												</td>
												<td class="tdInput">
													<asp:TextBox ID="txtUserName" runat="server" BackColor="#CCCCCC"></asp:TextBox>
												</td>
												<td style="width: 5px">
												</td>
												<td style="padding: 5px">
													<asp:ImageButton ID="btnLogin" runat="server" ImageUrl="~/Images/Button/btnSignIn.png"/>
												</td>
											</tr>
											<tr class="tdInput">
												<td style="padding: 5px">
													Password
												</td>
												<td class="tdInput">
													<asp:TextBox ID="txtPassword" runat="server" TextMode="Password" BackColor="#CCCCCC"></asp:TextBox><br />
												</td>
												<td style="width: 5px">
												</td>
												<td class="tdInput">
													<asp:ImageButton ID="btnForgotPswd0" runat="server" ImageUrl="/Images/Button/btnForgotPswd.png" />
												</td>
											</tr>
											<tr class="tdInput">
												<td colspan="2" style="padding: 5px">
													<asp:CheckBox ID="chkKeepLoggedIn" runat="server" Text="Keep me signed in" ToolTip="If this is not working for you, you may need to enable your browser to use cookies." />
												</td>
												<td style="width: 5px">
												</td>
												<td style="padding: 5px" valign="middle">
													&nbsp;</td>
											</tr>
										</table>
									</MSC:TBSCPanel>
                                </td>
                            </tr>
                            <tr valign="top">
                                <td class="tdInput" style="height: 40px">
									Not a member yet?  No problem.  Sign up for your FREE account today!<br /><br />
                                </td>
                            </tr>
                            <tr valign="top" align="center">
                                <td class="tdInput" style="height: 30px">
									<asp:ImageButton ID="btnNewAccount" ImageUrl="~/Images/Button/btnCreateAccount.png" runat="server" />
                                </td>
                            </tr>
                            <tr class="trNormal">
                                <td class="tdInput" style="width: 500px">
									<asp:Image ID="Image1" runat="server" ImageUrl="~/Images/Notice.jpg" />
									<asp:Label ID="Label1" runat="server" Text="!!Important notice!!  " CssClass="lgErrorTxt"></asp:Label>
									<asp:Label ID="Label2" runat="server" Text="This site requires cookies.  If you are having difficulty logging in, please open your browser's internet options and allow the use of cookies."></asp:Label>
									&nbsp;
									<asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/Help/CookieHelp.aspx">Click here for help enabling cookies</asp:HyperLink>
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
