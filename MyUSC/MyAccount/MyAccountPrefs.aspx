<%@ Page Title="" Language="C#" MasterPageFile="~/USC.Master" AutoEventWireup="true" CodeBehind="MyAccountPrefs.aspx.cs" Inherits="MyUSC.MyAccount.MyAccountPrefs" %>
<%@ MasterType VirtualPath="~/USC.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
	<script type="text/javascript" src="~/js/GA.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
	<table style="width: 1060px; height: 650px; background-image: url('Images/Background.png')">
		<tr valign="top">
			<td class="tdInput">
					<table width="100%">
						<tr>
							<td class="tdInput">
								<asp:Label ID="lblInstructions" runat="server" Text="Instructions go here ..." CssClass="lgSiteColorTxt"></asp:Label>
							</td>
							<td class="tdInput" align="right">
								<asp:ImageButton ID="btnOK2" ImageUrl="~/Images/Button/btnOK.png" runat="server" OnClick="OnClickOK"/>
							</td>
						</tr>
						<tr valign="top">
							<td class="tdInput" colspan="2">
								<asp:Label ID="lblSelectDefaultPage" runat="server" Text="Select your default page " CssClass="medSiteColorTxt"></asp:Label>
								<asp:DropDownList ID="ddlHomePage" runat="server">
									<asp:ListItem Text="Home" Value="home"></asp:ListItem>
									<asp:ListItem Text="Friends" Value="friends"></asp:ListItem>
									<asp:ListItem Text="Sports News" Value="sportsnews"></asp:ListItem>
									<asp:ListItem Text="Forums" Value="forums"></asp:ListItem>
								</asp:DropDownList>
							</td>
						</tr>
						<tr>
							<td class="tdInput" colspan="2">
								<asp:Label ID="lblOtherPrefs" runat="server" Text="Other Preferences" CssClass="medSiteColorTxt"></asp:Label>
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
								<asp:Label ID="lblPromotions" runat="server" Text="Promotions" CssClass="medSiteColorTxt"></asp:Label>
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
										<td class="tdInput" style="width: 600px">
										</td>
										<td class="tdInput">
											<asp:ImageButton ID="btnOK" ImageUrl="~/Images/Button/btnOK.png" runat="server" OnClick="OnClickOK"/>
											&nbsp;&nbsp;
											<asp:ImageButton ID="btnCancel" ImageUrl="~/Images/Button/btnCancel.png" runat="server" OnClick="OnClickCancel"/>
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
