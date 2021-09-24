<%@ Page Title="" Language="C#" MasterPageFile="~/MyTeams/OrgMaster.master" AutoEventWireup="true" CodeBehind="OrgOptions.aspx.cs" Inherits="MyUSC.MyTeams.OrgOptions" %>
<%@ MasterType VirtualPath="~/MyTeams/OrgMaster.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="OrgHeadContent" runat="server">
	<script type="text/javascript" src="~/js/GA.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="OrgContent" runat="server">
	<table style="width: 800px; vertical-align: top;">
		<tr valign="top" style="height: 35px">
			<td class="tdInput">
			</td>
			<td class="tdInput">

			</td>
			<td class="tdInput">
				<asp:Button ID="btnNextTop" runat="server" Text="Next" OnClick="OnClickNext" />
				<asp:Button ID="btnBackTop" runat="server" Text="Back" OnClick="OnClickBack" />
				<asp:ImageButton ID="btnOKTop" runat="server" ImageUrl="~/Images/Button/btnOK.png" OnClick="OnClickOK" Visible="False" />
			</td>
		</tr>
		<tr valign="top">
			<td class="tdInput" colspan="3">
				<asp:Label ID="Label16" runat="server" Text="Logo" CssClass="medSiteColorTxt"></asp:Label>
			</td>
		</tr>
		<tr valign="top">
			<td class="tdInput" colspan="3">
					<table width="100%">
						<tr>
							<td class="tdInput">
								<span class="smSiteColorTxt">Upload or Update Your Logo</span>
							</td>
						</tr>
						<tr>
							<td>
								<table width="100%">
									<tr>
										<td class="tdInput">
											<asp:Image ID="imgOrgLogo" ImageUrl="~/Images/NoPhoto.JPG" runat="server" Height="200px" Width="200px" />
										</td>
										<td class="tdInput">
											<asp:Label ID="lblRefresh" runat="server" Text="If your logo does not appear correctly click the refresh button on your browser."></asp:Label>
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
											<asp:FileUpload ID="fulUserLogo" runat="server" BackColor="#666666" Font-Bold="True" ForeColor="White" Width="600px" />
										</td>
										<td class="tdInput">
											<asp:Button ID="btnUpload" runat="server" Text="Upload" OnClick="OnClickUpload" />
										</td>
									</tr>
								</table>
							</td>
						</tr>
						<tr class="medErrorTxt">
							<td class="tdInput">
								<asp:Label ID="lblUploadError" runat="server" Text="" CssClass="medErrorTxt"></asp:Label>
								<asp:Label ID="lblUploadSuccess" runat="server" Text="" CssClass="medSuccessTxt"></asp:Label>
							</td>
						</tr>
					</table>
			</td>
		</tr>
		<tr valign="top">
			<td class="tdInput" colspan="3">
				<asp:Label ID="Label1" runat="server" Text="Viewing" CssClass="medSiteColorTxt"></asp:Label>
			</td>
		</tr>
		<tr valign="top" style="height: 35px">
			<td class="tdInput">

			</td>
			<td class="tdInput">
				<asp:CheckBox ID="chkGuestView" runat="server" Text="Allow guests to view site" Checked="True" />
			</td>
			<td class="tdInput">

			</td>
		</tr>
		<tr valign="top" style="height: 35px">
			<td class="tdInput">

			</td>
			<td class="tdInput">
				<asp:CheckBox ID="chkMemberRequests" runat="server" Text="Allow member requests" Checked="True" />
			</td>
			<td class="tdInput">

			</td>
		</tr>
		<tr valign="top" style="height: 35px">
			<td class="tdInput">

			</td>
			<td class="tdInput">
				<asp:CheckBox ID="chkFollowerRequests" runat="server" Text="Allow follower requests" Checked="True" />
			</td>
			<td class="tdInput">

			</td>
		</tr>
		<tr valign="top" style="height: 35px">
			<td class="tdInput" colspan="3">
				<asp:Label ID="Label2" runat="server" Text="Page Access" CssClass="lgSiteColorTxt"></asp:Label>
			</td>
		</tr>
		<tr valign="top">
			<td class="tdInput" colspan="3">
				<table width="100%">
					<tr valign="top">
						<td class="tdInput">
							<asp:Label ID="Label8" runat="server" Text="Page" CssClass="medSiteColorTxt" Font-Bold="True"></asp:Label>
						</td>
						<td class="tdInput">
							<asp:Label ID="Label3" runat="server" Text="Visible" CssClass="medSiteColorTxt" Font-Bold="True"></asp:Label>
						</td>
						<td class="tdInput">
							<asp:Label ID="Label4" runat="server" Text="Admin" CssClass="medSiteColorTxt" Font-Bold="True"></asp:Label>
						</td>
						<td class="tdInput">
							<asp:Label ID="Label5" runat="server" Text="Edit" CssClass="medSiteColorTxt" Font-Bold="True"></asp:Label>
						</td>
						<td class="tdInput">
							<asp:Label ID="Label6" runat="server" Text="Full Access" CssClass="medSiteColorTxt" Font-Bold="True"></asp:Label>
						</td>
						<td class="tdInput">
							<asp:Label ID="Label7" runat="server" Text="View" CssClass="medSiteColorTxt" Font-Bold="True"></asp:Label>
						</td>
					</tr>
					<tr valign="top">
						<td class="tdInput">
							<asp:Label ID="Label9" runat="server" Text="Home"></asp:Label>
						</td>
						<td class="tdInput">
						</td>
						<td class="tdInput">
							<asp:DropDownList ID="ddHomeAdmin" runat="server">
								<asp:ListItem Text="Owner" Value="1"></asp:ListItem>
								<asp:ListItem Text="Admin" Value="2"></asp:ListItem>
								<asp:ListItem Text="Contributor" Value="3"></asp:ListItem>
								<asp:ListItem Text="Member" Value="4"></asp:ListItem>
								<asp:ListItem Text="Follower" Value="5"></asp:ListItem>
								<asp:ListItem Text="Guest" Value="6"></asp:ListItem>
							</asp:DropDownList>
						</td>
						<td class="tdInput">
							<asp:DropDownList ID="ddHomeEdit" runat="server">
								<asp:ListItem Text="Owner" Value="1"></asp:ListItem>
								<asp:ListItem Text="Admin" Value="2"></asp:ListItem>
								<asp:ListItem Text="Contributor" Value="3"></asp:ListItem>
								<asp:ListItem Text="Member" Value="4"></asp:ListItem>
								<asp:ListItem Text="Follower" Value="5"></asp:ListItem>
								<asp:ListItem Text="Guest" Value="6"></asp:ListItem>
							</asp:DropDownList>
						</td>
						<td class="tdInput">
							<asp:DropDownList ID="ddHomeAccess" runat="server">
								<asp:ListItem Text="Owner" Value="1"></asp:ListItem>
								<asp:ListItem Text="Admin" Value="2"></asp:ListItem>
								<asp:ListItem Text="Contributor" Value="3"></asp:ListItem>
								<asp:ListItem Text="Member" Value="4"></asp:ListItem>
								<asp:ListItem Text="Follower" Value="5"></asp:ListItem>
								<asp:ListItem Text="Guest" Value="6"></asp:ListItem>
							</asp:DropDownList>
						</td>
						<td class="tdInput">
							<asp:DropDownList ID="ddHomeView" runat="server">
								<asp:ListItem Text="Owner" Value="1"></asp:ListItem>
								<asp:ListItem Text="Admin" Value="2"></asp:ListItem>
								<asp:ListItem Text="Contributor" Value="3"></asp:ListItem>
								<asp:ListItem Text="Member" Value="4"></asp:ListItem>
								<asp:ListItem Text="Follower" Value="5"></asp:ListItem>
								<asp:ListItem Text="Guest" Value="6"></asp:ListItem>
							</asp:DropDownList>
						</td>
					</tr>
					<tr valign="top">
						<td class="tdInput">
							<asp:Label ID="lblRoster" runat="server" Text="Roster"></asp:Label>
						</td>
						<td class="tdInput">
							<asp:CheckBox ID="chkRosterVisible" runat="server" Text=" " />
						</td>
						<td class="tdInput">
							<asp:DropDownList ID="ddRosterAdmin" runat="server">
								<asp:ListItem Text="Owner" Value="1"></asp:ListItem>
								<asp:ListItem Text="Admin" Value="2"></asp:ListItem>
								<asp:ListItem Text="Contributor" Value="3"></asp:ListItem>
								<asp:ListItem Text="Member" Value="4"></asp:ListItem>
								<asp:ListItem Text="Follower" Value="5"></asp:ListItem>
								<asp:ListItem Text="Guest" Value="6"></asp:ListItem>
							</asp:DropDownList>
						</td>
						<td class="tdInput">
							<asp:DropDownList ID="ddRosterEdit" runat="server">
								<asp:ListItem Text="Owner" Value="1"></asp:ListItem>
								<asp:ListItem Text="Admin" Value="2"></asp:ListItem>
								<asp:ListItem Text="Contributor" Value="3"></asp:ListItem>
								<asp:ListItem Text="Member" Value="4"></asp:ListItem>
								<asp:ListItem Text="Follower" Value="5"></asp:ListItem>
								<asp:ListItem Text="Guest" Value="6"></asp:ListItem>
							</asp:DropDownList>
						</td>
						<td class="tdInput">
							<asp:DropDownList ID="ddRosterAccess" runat="server">
								<asp:ListItem Text="Owner" Value="1"></asp:ListItem>
								<asp:ListItem Text="Admin" Value="2"></asp:ListItem>
								<asp:ListItem Text="Contributor" Value="3"></asp:ListItem>
								<asp:ListItem Text="Member" Value="4"></asp:ListItem>
								<asp:ListItem Text="Follower" Value="5"></asp:ListItem>
								<asp:ListItem Text="Guest" Value="6"></asp:ListItem>
							</asp:DropDownList>
						</td>
						<td class="tdInput">
							<asp:DropDownList ID="ddRosterView" runat="server">
								<asp:ListItem Text="Owner" Value="1"></asp:ListItem>
								<asp:ListItem Text="Admin" Value="2"></asp:ListItem>
								<asp:ListItem Text="Contributor" Value="3"></asp:ListItem>
								<asp:ListItem Text="Member" Value="4"></asp:ListItem>
								<asp:ListItem Text="Follower" Value="5"></asp:ListItem>
								<asp:ListItem Text="Guest" Value="6"></asp:ListItem>
							</asp:DropDownList>
						</td>
					</tr>
					<tr valign="top">
						<td class="tdInput">
							<asp:Label ID="Label10" runat="server" Text="Schedule"></asp:Label>
						</td>
						<td class="tdInput">
							<asp:CheckBox ID="chkScheduleVisible" runat="server" Text=" " />
						</td>
						<td class="tdInput">
							<asp:DropDownList ID="ddScheduleAdmin" runat="server">
								<asp:ListItem Text="Owner" Value="1"></asp:ListItem>
								<asp:ListItem Text="Admin" Value="2"></asp:ListItem>
								<asp:ListItem Text="Contributor" Value="3"></asp:ListItem>
								<asp:ListItem Text="Member" Value="4"></asp:ListItem>
								<asp:ListItem Text="Follower" Value="5"></asp:ListItem>
								<asp:ListItem Text="Guest" Value="6"></asp:ListItem>
							</asp:DropDownList>
						</td>
						<td class="tdInput">
							<asp:DropDownList ID="ddScheduleEdit" runat="server">
								<asp:ListItem Text="Owner" Value="1"></asp:ListItem>
								<asp:ListItem Text="Admin" Value="2"></asp:ListItem>
								<asp:ListItem Text="Contributor" Value="3"></asp:ListItem>
								<asp:ListItem Text="Member" Value="4"></asp:ListItem>
								<asp:ListItem Text="Follower" Value="5"></asp:ListItem>
								<asp:ListItem Text="Guest" Value="6"></asp:ListItem>
							</asp:DropDownList>
						</td>
						<td class="tdInput">
							<asp:DropDownList ID="ddScheduleAccess" runat="server">
								<asp:ListItem Text="Owner" Value="1"></asp:ListItem>
								<asp:ListItem Text="Admin" Value="2"></asp:ListItem>
								<asp:ListItem Text="Contributor" Value="3"></asp:ListItem>
								<asp:ListItem Text="Member" Value="4"></asp:ListItem>
								<asp:ListItem Text="Follower" Value="5"></asp:ListItem>
								<asp:ListItem Text="Guest" Value="6"></asp:ListItem>
							</asp:DropDownList>
						</td>
						<td class="tdInput">
							<asp:DropDownList ID="ddScheduleView" runat="server">
								<asp:ListItem Text="Owner" Value="1"></asp:ListItem>
								<asp:ListItem Text="Admin" Value="2"></asp:ListItem>
								<asp:ListItem Text="Contributor" Value="3"></asp:ListItem>
								<asp:ListItem Text="Member" Value="4"></asp:ListItem>
								<asp:ListItem Text="Follower" Value="5"></asp:ListItem>
								<asp:ListItem Text="Guest" Value="6"></asp:ListItem>
							</asp:DropDownList>
						</td>
					</tr>
					<tr valign="top">
						<td class="tdInput">
							<asp:Label ID="Label11" runat="server" Text="Message Board"></asp:Label>
						</td>
						<td class="tdInput">
							<asp:CheckBox ID="chkMsgBoardVisible" runat="server" Text=" " />
						</td>
						<td class="tdInput">
							<asp:DropDownList ID="ddMsgBoardAdmin" runat="server">
								<asp:ListItem Text="Owner" Value="1"></asp:ListItem>
								<asp:ListItem Text="Admin" Value="2"></asp:ListItem>
								<asp:ListItem Text="Contributor" Value="3"></asp:ListItem>
								<asp:ListItem Text="Member" Value="4"></asp:ListItem>
								<asp:ListItem Text="Follower" Value="5"></asp:ListItem>
								<asp:ListItem Text="Guest" Value="6"></asp:ListItem>
							</asp:DropDownList>
						</td>
						<td class="tdInput">
							<asp:DropDownList ID="ddMsgBoardEdit" runat="server">
								<asp:ListItem Text="Owner" Value="1"></asp:ListItem>
								<asp:ListItem Text="Admin" Value="2"></asp:ListItem>
								<asp:ListItem Text="Contributor" Value="3"></asp:ListItem>
								<asp:ListItem Text="Member" Value="4"></asp:ListItem>
								<asp:ListItem Text="Follower" Value="5"></asp:ListItem>
								<asp:ListItem Text="Guest" Value="6"></asp:ListItem>
							</asp:DropDownList>
						</td>
						<td class="tdInput">
							<asp:DropDownList ID="ddMsgBoardAccess" runat="server">
								<asp:ListItem Text="Owner" Value="1"></asp:ListItem>
								<asp:ListItem Text="Admin" Value="2"></asp:ListItem>
								<asp:ListItem Text="Contributor" Value="3"></asp:ListItem>
								<asp:ListItem Text="Member" Value="4"></asp:ListItem>
								<asp:ListItem Text="Follower" Value="5"></asp:ListItem>
								<asp:ListItem Text="Guest" Value="6"></asp:ListItem>
							</asp:DropDownList>
						</td>
						<td class="tdInput">
							<asp:DropDownList ID="ddMsgBoardView" runat="server">
								<asp:ListItem Text="Owner" Value="1"></asp:ListItem>
								<asp:ListItem Text="Admin" Value="2"></asp:ListItem>
								<asp:ListItem Text="Contributor" Value="3"></asp:ListItem>
								<asp:ListItem Text="Member" Value="4"></asp:ListItem>
								<asp:ListItem Text="Follower" Value="5"></asp:ListItem>
								<asp:ListItem Text="Guest" Value="6"></asp:ListItem>
							</asp:DropDownList>
						</td>
					</tr>
					<tr valign="top">
						<td class="tdInput">
							<asp:Label ID="Label12" runat="server" Text="Media"></asp:Label>
						</td>
						<td class="tdInput">
							<asp:CheckBox ID="chkMediaVisible" runat="server" Text=" " />
						</td>
						<td class="tdInput">
							<asp:DropDownList ID="ddMediaAdmin" runat="server">
								<asp:ListItem Text="Owner" Value="1"></asp:ListItem>
								<asp:ListItem Text="Admin" Value="2"></asp:ListItem>
								<asp:ListItem Text="Contributor" Value="3"></asp:ListItem>
								<asp:ListItem Text="Member" Value="4"></asp:ListItem>
								<asp:ListItem Text="Follower" Value="5"></asp:ListItem>
								<asp:ListItem Text="Guest" Value="6"></asp:ListItem>
							</asp:DropDownList>
						</td>
						<td class="tdInput">
							<asp:DropDownList ID="ddMediaEdit" runat="server">
								<asp:ListItem Text="Owner" Value="1"></asp:ListItem>
								<asp:ListItem Text="Admin" Value="2"></asp:ListItem>
								<asp:ListItem Text="Contributor" Value="3"></asp:ListItem>
								<asp:ListItem Text="Member" Value="4"></asp:ListItem>
								<asp:ListItem Text="Follower" Value="5"></asp:ListItem>
								<asp:ListItem Text="Guest" Value="6"></asp:ListItem>
							</asp:DropDownList>
						</td>
						<td class="tdInput">
							<asp:DropDownList ID="ddMediaAccess" runat="server">
								<asp:ListItem Text="Owner" Value="1"></asp:ListItem>
								<asp:ListItem Text="Admin" Value="2"></asp:ListItem>
								<asp:ListItem Text="Contributor" Value="3"></asp:ListItem>
								<asp:ListItem Text="Member" Value="4"></asp:ListItem>
								<asp:ListItem Text="Follower" Value="5"></asp:ListItem>
								<asp:ListItem Text="Guest" Value="6"></asp:ListItem>
							</asp:DropDownList>
						</td>
						<td class="tdInput">
							<asp:DropDownList ID="ddMediaView" runat="server">
								<asp:ListItem Text="Owner" Value="1"></asp:ListItem>
								<asp:ListItem Text="Admin" Value="2"></asp:ListItem>
								<asp:ListItem Text="Contributor" Value="3"></asp:ListItem>
								<asp:ListItem Text="Member" Value="4"></asp:ListItem>
								<asp:ListItem Text="Follower" Value="5"></asp:ListItem>
								<asp:ListItem Text="Guest" Value="6"></asp:ListItem>
							</asp:DropDownList>
						</td>
					</tr>
					<tr valign="top">
						<td class="tdInput">
							<asp:Label ID="Label13" runat="server" Text="Email"></asp:Label>
						</td>
						<td class="tdInput">
							<asp:CheckBox ID="chkEmailVisible" runat="server" Text=" " />
						</td>
						<td class="tdInput">
							<asp:DropDownList ID="ddEmailAdmin" runat="server">
								<asp:ListItem Text="Owner" Value="1"></asp:ListItem>
								<asp:ListItem Text="Admin" Value="2"></asp:ListItem>
								<asp:ListItem Text="Contributor" Value="3"></asp:ListItem>
								<asp:ListItem Text="Member" Value="4"></asp:ListItem>
								<asp:ListItem Text="Follower" Value="5"></asp:ListItem>
								<asp:ListItem Text="Guest" Value="6"></asp:ListItem>
							</asp:DropDownList>
						</td>
						<td class="tdInput">
							<asp:DropDownList ID="ddEmailEdit" runat="server">
								<asp:ListItem Text="Owner" Value="1"></asp:ListItem>
								<asp:ListItem Text="Admin" Value="2"></asp:ListItem>
								<asp:ListItem Text="Contributor" Value="3"></asp:ListItem>
								<asp:ListItem Text="Member" Value="4"></asp:ListItem>
								<asp:ListItem Text="Follower" Value="5"></asp:ListItem>
								<asp:ListItem Text="Guest" Value="6"></asp:ListItem>
							</asp:DropDownList>
						</td>
						<td class="tdInput">
							<asp:DropDownList ID="ddEmailAccess" runat="server">
								<asp:ListItem Text="Owner" Value="1"></asp:ListItem>
								<asp:ListItem Text="Admin" Value="2"></asp:ListItem>
								<asp:ListItem Text="Contributor" Value="3"></asp:ListItem>
								<asp:ListItem Text="Member" Value="4"></asp:ListItem>
								<asp:ListItem Text="Follower" Value="5"></asp:ListItem>
								<asp:ListItem Text="Guest" Value="6"></asp:ListItem>
							</asp:DropDownList>
						</td>
						<td class="tdInput">
							<asp:DropDownList ID="ddEmailView" runat="server">
								<asp:ListItem Text="Owner" Value="1"></asp:ListItem>
								<asp:ListItem Text="Admin" Value="2"></asp:ListItem>
								<asp:ListItem Text="Contributor" Value="3"></asp:ListItem>
								<asp:ListItem Text="Member" Value="4"></asp:ListItem>
								<asp:ListItem Text="Follower" Value="5"></asp:ListItem>
								<asp:ListItem Text="Guest" Value="6"></asp:ListItem>
							</asp:DropDownList>
						</td>
					</tr>
					<tr valign="top">
						<td class="tdInput">
							<asp:Label ID="Label14" runat="server" Text="Venues (Fields)"></asp:Label>
						</td>
						<td class="tdInput">
							<asp:CheckBox ID="chkVenueVisible" runat="server" Text=" " />
						</td>
						<td class="tdInput">
							<asp:DropDownList ID="ddVenueAdmin" runat="server">
								<asp:ListItem Text="Owner" Value="1"></asp:ListItem>
								<asp:ListItem Text="Admin" Value="2"></asp:ListItem>
								<asp:ListItem Text="Contributor" Value="3"></asp:ListItem>
								<asp:ListItem Text="Member" Value="4"></asp:ListItem>
								<asp:ListItem Text="Follower" Value="5"></asp:ListItem>
								<asp:ListItem Text="Guest" Value="6"></asp:ListItem>
							</asp:DropDownList>
						</td>
						<td class="tdInput">
							<asp:DropDownList ID="ddVenueEdit" runat="server">
								<asp:ListItem Text="Owner" Value="1"></asp:ListItem>
								<asp:ListItem Text="Admin" Value="2"></asp:ListItem>
								<asp:ListItem Text="Contributor" Value="3"></asp:ListItem>
								<asp:ListItem Text="Member" Value="4"></asp:ListItem>
								<asp:ListItem Text="Follower" Value="5"></asp:ListItem>
								<asp:ListItem Text="Guest" Value="6"></asp:ListItem>
							</asp:DropDownList>
						</td>
						<td class="tdInput">
							<asp:DropDownList ID="ddVenueAccess" runat="server">
								<asp:ListItem Text="Owner" Value="1"></asp:ListItem>
								<asp:ListItem Text="Admin" Value="2"></asp:ListItem>
								<asp:ListItem Text="Contributor" Value="3"></asp:ListItem>
								<asp:ListItem Text="Member" Value="4"></asp:ListItem>
								<asp:ListItem Text="Follower" Value="5"></asp:ListItem>
								<asp:ListItem Text="Guest" Value="6"></asp:ListItem>
							</asp:DropDownList>
						</td>
						<td class="tdInput">
							<asp:DropDownList ID="ddVenueView" runat="server">
								<asp:ListItem Text="Owner" Value="1"></asp:ListItem>
								<asp:ListItem Text="Admin" Value="2"></asp:ListItem>
								<asp:ListItem Text="Contributor" Value="3"></asp:ListItem>
								<asp:ListItem Text="Member" Value="4"></asp:ListItem>
								<asp:ListItem Text="Follower" Value="5"></asp:ListItem>
								<asp:ListItem Text="Guest" Value="6"></asp:ListItem>
							</asp:DropDownList>
						</td>
					</tr>
					<tr valign="top">
						<td class="tdInput">
							<asp:Label ID="Label15" runat="server" Text="Management"></asp:Label>
						</td>
						<td class="tdInput">
						</td>
						<td class="tdInput">
							<asp:DropDownList ID="ddManageAdmin" runat="server">
								<asp:ListItem Text="Owner" Value="1"></asp:ListItem>
								<asp:ListItem Text="Admin" Value="2"></asp:ListItem>
								<asp:ListItem Text="Contributor" Value="3"></asp:ListItem>
								<asp:ListItem Text="Member" Value="4"></asp:ListItem>
							</asp:DropDownList>
						</td>
						<td class="tdInput">
							<asp:DropDownList ID="ddManageEdit" runat="server">
								<asp:ListItem Text="Owner" Value="1"></asp:ListItem>
								<asp:ListItem Text="Admin" Value="2"></asp:ListItem>
								<asp:ListItem Text="Contributor" Value="3"></asp:ListItem>
								<asp:ListItem Text="Member" Value="4"></asp:ListItem>
							</asp:DropDownList>
						</td>
						<td class="tdInput">
							<asp:DropDownList ID="ddManageAccess" runat="server">
								<asp:ListItem Text="Owner" Value="1"></asp:ListItem>
								<asp:ListItem Text="Admin" Value="2"></asp:ListItem>
								<asp:ListItem Text="Contributor" Value="3"></asp:ListItem>
								<asp:ListItem Text="Member" Value="4"></asp:ListItem>
							</asp:DropDownList>
						</td>
						<td class="tdInput">
							<asp:DropDownList ID="ddManageView" runat="server">
								<asp:ListItem Text="Owner" Value="1"></asp:ListItem>
								<asp:ListItem Text="Admin" Value="2"></asp:ListItem>
								<asp:ListItem Text="Contributor" Value="3"></asp:ListItem>
								<asp:ListItem Text="Member" Value="4"></asp:ListItem>
							</asp:DropDownList>
						</td>
					</tr>
				</table>
			</td>
		</tr>
		<tr valign="top" style="height: 35px">
			<td class="tdInput">

			</td>
			<td class="tdInput">
			</td>
			<td class="tdInput">

			</td>
		</tr>
		<tr valign="top" style="height: 35px">
			<td class="tdInput">
				<asp:Button ID="btnNextBottom" runat="server" Text="Next" OnClick="OnClickNext" />
				<asp:Button ID="btnBackBottom" runat="server" Text="Back" OnClick="OnClickBack" />
				<asp:ImageButton ID="btnOKBottom" runat="server" ImageUrl="~/Images/Button/btnOK.png" OnClick="OnClickOK" Visible="False" />
			</td>
			<td class="tdInput">

			</td>
			<td class="tdInput">

			</td>
		</tr>
	</table>
</asp:Content>
