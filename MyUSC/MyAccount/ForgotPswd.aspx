<%@ Page Title="" Language="C#" MasterPageFile="~/USC.Master" AutoEventWireup="true" CodeBehind="ForgotPswd.aspx.cs" Inherits="MyUSC.ForgotPswd" %>
<%@ MasterType VirtualPath="~/USC.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
	<script type="text/javascript" src="~/js/GA.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
	<table style="background-image: url('Images/Background.png')" class="tblPage">
		<tr valign="top" style="height: 140px">
			<td class="tdInput" align="center">
				<table width="100%">
					<tr valign="top">
						<td class="tdInput">
							<asp:Label ID="lblSiteName" CssClass="xlgSiteColorTxt" runat="server" Text="MySportsConnect.net"></asp:Label>
						</td>
					</tr>
					<tr valign="top">
						<td class="tdInput">
							<asp:Label ID="lblPageText" CssClass="lgSiteColorTxt" runat="server" Text="Password Recovery"></asp:Label>
						</td>
					</tr>
					<tr valign="top">
						<td class="tdInput">
							<span class="lgErrorTxt">* - </span>
							<asp:Label ID="lblRequired" runat="server" Text="indicates required fields."></asp:Label>
						</td>
					</tr>
					<tr valign="top">
						<td class="tdInput">
							<asp:Panel ID="pnlEmailFailure" runat="server" Visible="False">
								<asp:Label ID="lblEmailFailure1" runat="server" Text="There was an error sending the email with your new password.  Please try once more." CssClass="medErrorTxt" Visible="False"></asp:Label>
								<br />
								<asp:Label ID="lblEmailFailure2" runat="server" Text="If the problem continues, please contact support." CssClass="medErrorTxt" Visible="False"></asp:Label>
							</asp:Panel>
						</td>
					</tr>
				</table>
			</td>
		</tr>
		<tr valign="top">
			<td class="tdInput" style="height: 30px">
				<table width="100%">
					<tr valign="top">
						<td class="tdInput" align="right">
							<asp:Label ID="lblUserName" runat="server" Text="User Name: "></asp:Label>
						</td>
						<td class="tdInput" style="width: 225px">
							<asp:TextBox ID="txtUserName" runat="server" MaxLength="50" Width="200px"></asp:TextBox>
						</td>
						<td class="tdInput">
							<asp:Button ID="btnCheckUserName" runat="server" Text="Check User Name" OnClick="OnClickCheckUserName" ToolTip="We will look for this user name to find your security question." />
						</td>
					</tr>
				</table>
			</td>
		</tr>
		<tr valign="top">
			<td class="tdInput" style="height: 30px">
				<table width="100%">
					<tr valign="top">
						<td class="tdInput" style="width: 100px" align="right">
							<asp:Label ID="lblQuestion" runat="server" Text="Question: "></asp:Label>
						</td>
						<td class="tdInput">
							<asp:Label ID="lblSecurityQuestion" runat="server" Text="Label"></asp:Label>
						</td>
					</tr>
				</table>
			</td>
		</tr>
		<tr valign="top">
			<td class="tdInput" style="height: 30px">
				<table width="100%">
					<tr valign="top">
						<td class="tdInput" style="width: 100px" align="right">
							<asp:Label ID="lblSecurityAnswer" runat="server" Text="Answer: "></asp:Label>
						</td>
						<td class="tdInput">
							<asp:TextBox ID="txtSecurityAnswer" runat="server" MaxLength="50" Width="200px"></asp:TextBox> &nbsp; &nbsp;
							<asp:Panel ID="pnlSecurityError" runat="server">
								<br />
								<asp:Label ID="lblSecurityError" runat="server" Text="The answer you provided does not match our records.  Please try again." CssClass="medErrorTxt" Visible="False"></asp:Label>
							</asp:Panel>
						</td>
					</tr>
				</table>
			</td>
		</tr>
		<tr valign="top">
			<td class="tdInput">
				<table width="100%">
					<tr valign="top">
						<td class="tdInput" align="right">
							<asp:ImageButton ID="btnSubmit" ImageUrl="~/Images/Button/btnSubmit.png" runat="server" OnClick="OnClickSubmit" />
						</td>
						<td class="tdInput" align="left">
							<asp:ImageButton ID="btnCancel" ImageUrl="~/Images/Button/btnCancel.png" runat="server" OnClick="OnClickCancel" />
						</td>
					</tr>
				</table>
			</td>
		</tr>
	</table>
</asp:Content>
