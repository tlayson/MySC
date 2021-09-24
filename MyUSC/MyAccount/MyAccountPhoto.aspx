<%@ Page Title="" Language="C#" MasterPageFile="~/USC.Master" AutoEventWireup="true" CodeBehind="MyAccountPhoto.aspx.cs" Inherits="MyUSC.MyAccount.MyAccountPhoto" %>
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
								<asp:Label ID="lblUpload" runat="server" Text="Upload or Update Your Photo" CssClass="lgSiteColorTxt"></asp:Label>
							</td>
						</tr>
						<tr>
							<td>
								<table width="100%">
									<tr>
										<td class="tdInput">
											<asp:Image ID="imgUserPhoto" ImageUrl="~/Images/NoPhoto.JPG" runat="server" Height="200px" Width="200px" />
										</td>
										<td class="tdInput">
											<asp:Label ID="lblRefresh" runat="server" Text="If your photo does not appear correctly here or on the friends page, click the refresh button on your browser."></asp:Label>
										</td>
									</tr>
								</table>
							</td>
						</tr>
						<tr>
							<td>
								<asp:FileUpload ID="fulUserPhoto" runat="server" BackColor="#666666" Font-Bold="True" ForeColor="White" Width="682px" />
								&nbsp;
								<asp:Button ID="btnUpload" runat="server" Text="Upload" OnClick="OnClickUpload" />
							</td>
						</tr>
						<tr class="medErrorTxt">
							<td class="tdInput">
								<asp:Label ID="lblUploadError" runat="server" Text="" CssClass="medErrorTxt"></asp:Label>
								<asp:Label ID="lblUploadSuccess" runat="server" Text="" CssClass="medSuccessTxt"></asp:Label>
							</td>
						</tr>
						<tr>
							<td>
								<table width="75%">
									<tr>
										<td class="tdInput" align="right" >
											<asp:Label ID="lblDeactivate" runat="server" Text="Deactiveate your account?"></asp:Label>
											&nbsp;&nbsp;
											<asp:ImageButton ID="btnDeactivateAcct" ImageUrl="~/Images/Button/btnDeactivate.png" runat="server" OnClick="OnClickDeactivate" />
										</td>
									</tr>
								</table>
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
