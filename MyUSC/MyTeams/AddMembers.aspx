<%@ Page Title="" Language="C#" MasterPageFile="~/MyTeams/OrgMaster.master" AutoEventWireup="true" CodeBehind="AddMembers.aspx.cs" Inherits="MyUSC.MyTeams.AddMembers" %>
<%@ MasterType VirtualPath="~/MyTeams/OrgMaster.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="OrgHeadContent" runat="server">
	<script type="text/javascript" src="~/js/GA.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="OrgContent" runat="server">
	<table class="tblNormal" >
		<tr class="trNormal">
			<td class="tdInput">
				<asp:Panel ID="pnlInvite" runat="server">
					<table class="tblNormal">
						<tr>
							<td class="tdInput">
								<asp:Label ID="Label1" runat="server" Text="Invite" CssClass="xlgSiteColorTxt"></asp:Label>
							</td>
							<td style="text-align: center">
							</td>
						</tr>
						<tr>
							<td class="tdInput" colspan="2">
								Fill out one or more fields below and click FIND to search for people to invite. You can use partial entries if you are not sure of the spelling.  For example, 'smi' will 
								find 'Smith'.  If they are not already members of the site, you will be able to invite them via their email address as well.
							</td>
						</tr>
						<tr>
							<td style="width: 400px; padding-top: 5px; padding-bottom: 5px;" class="tdInput">
								<asp:Label ID="lblFirstName" runat="server" Text="First Name"></asp:Label><br />
								<asp:TextBox ID="txtFirstName" runat="server" Width="214px"></asp:TextBox>
							</td>

							<td style="width: 400px; padding-top: 5px; padding-bottom: 5px;" class="tdInput">
								<asp:Label ID="lblLastName" runat="server" Text="Last Name"></asp:Label><br />
								<asp:TextBox ID="txtLastName" runat="server" Width="214px"></asp:TextBox>
							</td>
						</tr>
						<tr>
							<td colspan ="2" style="padding-top: 5px; padding-bottom: 5px" class="tdInput">
								<asp:Label ID="lblCity" runat="server" Text="City"></asp:Label><br />
								<asp:TextBox ID="txtCity" runat="server" Width="610px"></asp:TextBox>
							</td>
						</tr>
						<tr>
							<td style="padding-top: 5px; padding-bottom: 5px" class="tdInput">
								<asp:Label ID="lblState" runat="server" Text="State"></asp:Label><br />
								<asp:DropDownList ID="ddlState" Height="19px" Width="214px" runat="server"></asp:DropDownList>
							</td>

							<td style="padding-top: 5px; padding-bottom: 5px" class="tdInput">
								<asp:Label ID="LblPostalCode" runat="server" Text="Postal Code"></asp:Label><br />
								<asp:TextBox ID="txtPostalCode" runat="server"></asp:TextBox>
								<br />
							</td>
						</tr>
						<tr>
							<td class="tdInput">
								<asp:Label ID="lblEmail" runat="server" Text="eMail Address"></asp:Label><br />
								<asp:TextBox ID="txtEmail" runat="server" Width="350px"></asp:TextBox>
							</td>
						</tr>
						<tr>
							<td class="tdInput">

							</td>
							<td class="tdInput">
								<table>
									<tr align="center">
										<td style="padding: 5px 15px 5px 15px">
											<asp:ImageButton ID="btnFindFriends" runat="server" ImageUrl="/Images/Button/btnFind.png" OnClick="OnClickFind" />
										</td>

										<td style="padding: 5px 15px 5px 15px">
											<asp:ImageButton ID="btnCancelFind" runat="server" ImageUrl="/Images/Button/btnCancel.png" OnClick="OnClickCancel" />
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
			<td>
				<asp:Panel ID="pnlResults" runat="server">
					<hr />
					<table width="100%">
						<tr valign="top">
							<td class="tdInput">
								<asp:Label ID="lblResultMessage" runat="server" CssClass="medSiteColorTxt"></asp:Label>&nbsp;
							</td>
							<td class="tdInput" style="text-align: right">
								<asp:LinkButton ID="btnInviteNew" runat="server" PostBackUrl="~/MyTeams/InviteMember.aspx" OnClick="OnClickInviteNew">Didn't find who you were looking for? Invite someone new!</asp:LinkButton>
							</td>
						</tr>
					</table>
					<br />
					<asp:Table ID="tblResults" Width="100%" runat="server">

					</asp:Table>
				</asp:Panel>
			</td>
		</tr>
	</table>
</asp:Content>
