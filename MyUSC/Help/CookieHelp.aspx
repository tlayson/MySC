<%@ Page Title="" Language="C#" MasterPageFile="~/USC.Master" AutoEventWireup="true" CodeBehind="CookieHelp.aspx.cs" Inherits="MyUSC.Help.CookieHelp" %>
<%@ MasterType VirtualPath="~/USC.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
	<table class="tblPage">
		<tr class="trShort">
			<td class="tdInput">
				<asp:Label ID="Label1" runat="server" Text="Enabling Cookies" CssClass="xlgSiteColorTxt"></asp:Label>
			</td>
		</tr>
		<tr class="trShort">
			<td class="tdInput">
				<table class="tblNormal">
					<tr class="trShort">
						<td class="tdInput">
							<asp:Label ID="Label2" runat="server" Text="In order for us to display MySportsConnect.net using your preferences, we need to use cookies to keep track of them.  "></asp:Label>
							<asp:Label ID="Label4" runat="server" Text="Your information will only be used to enhance your experience on MySportsConnect.net and not be shared with anyone.   "></asp:Label>
							<asp:Label ID="Label3" runat="server" Text="Please find your browser in the list below and follow the intructions to enable cookies.  "></asp:Label><br />
							<asp:Label ID="Label6" runat="server" Text="NOTE: " CssClass="medSiteColorTxt"></asp:Label>
							<asp:Label ID="Label5" runat="server" Text="The instructions are based on the most recent browser versions.  If you are using an older browser version, the steps may vary.   "></asp:Label>
						</td>
					</tr>
				</table>
			</td>
		</tr>
		<tr>
			<td>
				<hr />
			</td>
		</tr>
		<tr class="trShort">
			<td class="tdInput">
				<table class="tblNormal">
					<tr class="trShort">
						<td class="tdInput">
							<asp:Label ID="Label7" runat="server" Text="Internet Explorer" CssClass="medSiteColorTxt"></asp:Label>
						</td>
					</tr>
					<tr class="trShort">
						<td class="tdInput">
							Begin by clicking the left mouse button on the tools menu.
						</td>
					</tr>
					<tr class="trShort">
						<td class="tdImage">
							<asp:Image ID="Image1" runat="server" ImageUrl="~/Help/i/IEMenu.jpg" />
						</td>
					</tr>
					<tr class="trShort">
						<td class="tdInput">
							Then select <u>Internet Options</u>.
						</td>
					</tr>
					<tr class="trShort">
						<td class="tdInput">
							Next select the <u>Privacy</u> tab and click the <u>Advanced</u> button.
						</td>
					</tr>
					<tr class="trShort">
						<td class="tdImage">
							<asp:Image ID="Image2" runat="server" ImageUrl="~/Help/i/IEOptions.jpg" />
						</td>
					</tr>
					<tr class="trShort">
						<td class="tdInput">
							Finally, ensure that Override automatic cookie handling, Accept First-party Cookies and Always allow session cookies are all checked.  Third-party cookies are not required and you
							can set this to what ever options you choose.
						</td>
					</tr>
					<tr class="trShort">
						<td class="tdImage">
							<asp:Image ID="Image3" runat="server" ImageUrl="~/Help/i/IECookies.jpg" />
						</td>
					</tr>
				</table>
			</td>
		</tr>
	</table>

</asp:Content>
