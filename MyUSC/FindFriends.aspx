<%@ Page Title="" Language="C#" MasterPageFile="~/USC.Master" AutoEventWireup="true" CodeBehind="FindFriends.aspx.cs" Inherits="MyUSC.FindFriends" %>
<%@ MasterType VirtualPath="~/USC.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
	<script type="text/javascript" src="~/js/GA.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
	<asp:HiddenField ID="hdnSearchExecuted" runat="server" Value="0" />
	<table class="tblMainPage" style="background-image: url('Images/Background.png'); background-repeat: repeat;">
		<tr class="trVATop">
			<!-- Friends page menu  -->
			<td class="tdInput" style="width: 200px" >
				<table class="tblNormal">
					<tr>
						<td class="tdInput" colspan="2">
							<asp:Image ID="imgUserPhoto" ImageUrl="~/Images/NoPhoto.JPG" runat="server" Height="120px" Width="120" />
						</td>
					</tr>
					<tr>
						<td class="tdInput" colspan="2">
						<span class="xlgSiteColorTxt">
							<asp:Label ID="lblUserName" runat="server" Text="John Doe"></asp:Label>
						</span>
						</td>
					</tr>
					<tr>
						<td class="tdInput" colspan="2">
							<asp:Label ID="lblNewMsgs" runat="server" Text="New Messages"></asp:Label>
						</td>
					</tr>
					<tr>
						<td class="tdInput">
							<asp:Image ID="imgMenuMsgBoard" ImageUrl="~/Images/Button/MenuSelectRed.png" runat="server" Visible="False" />
						</td>
						<td class="tdFriendsMenu">
							<asp:LinkButton ID="lnkMenuMsgBoard" runat="server" CssClass="miFriendsNormal" OnClick="OnClickMenuMessageBoard" Width="125px">Message Board</asp:LinkButton><br />
						</td>
					</tr>
					<tr>
						<td class="tdInput">
							<asp:Image ID="imgMenuFindFriends" ImageUrl="~/Images/Button/MenuSelectRed.png" runat="server" />
						</td>
						<td class="tdFriendsMenu">
							<asp:LinkButton ID="lnkMenuFindFriends" runat="server" CssClass="miFriendsNormal" OnClick="OnClickMenuFindFriends">Find Friends</asp:LinkButton><br />
						</td>
					</tr>
					<tr>
						<td class="tdInput">
							<asp:Image ID="imgMenuFriendRequests" ImageUrl="~/Images/Button/MenuSelectRed.png" runat="server" Visible="False" />
						</td>
						<td class="tdFriendsMenu">
							<asp:LinkButton ID="lnkMenuFriendRequests" runat="server" CssClass="miFriendsNormal" OnClick="OnClickMenuFriendRequests">Friend Requests</asp:LinkButton><br />
						</td>
					</tr>
					<tr>
						<td class="tdInput">
							<asp:Image ID="imgMenuAllFriends" ImageUrl="~/Images/Button/MenuSelectRed.png" runat="server" Visible="False" />
						</td>
						<td class="tdFriendsMenu">
							<asp:LinkButton ID="lnkMenuAllFriends" runat="server" CssClass="miFriendsNormal" OnClick="OnClickMenuAllFriends">My Friends</asp:LinkButton><br />
						</td>
					</tr>
				</table>
			</td>
			<td class="tdInput">
				<asp:Panel ID="pnlFindFriends" runat="server">
					<table class="tblNormal">
						<tr>
							<td class="tdInput" colspan="2">
								<span class="xlgSiteColorTxt">
									Find Friends
								</span>
							</td>
						</tr>
						<tr>
							<td class="tdInput" colspan="2">
								Fill in the fields below and click FIND FRIENDS to search for people you know.
							</td>
						</tr>
						<tr>
							<td class="tdInput" style="width: 400px;">
								<asp:Label ID="lblFirstName" runat="server" Text="First Name"></asp:Label><br />
								<asp:TextBox ID="txtFirstName" runat="server" Width="214px"></asp:TextBox>
							</td>

							<td class="tdInput" style="width: 400px;">
								<asp:Label ID="lblLastName" runat="server" Text="Last Name"></asp:Label><br />
								<asp:TextBox ID="txtLastName" runat="server" Width="214px"></asp:TextBox>
							</td>
						</tr>
						<tr>
							<td colspan ="2" class="tdInput">
								<asp:Label ID="lblCity" runat="server" Text="City"></asp:Label><br />
								<asp:TextBox ID="txtCity" runat="server" Width="610px"></asp:TextBox>
							</td>
						</tr>
						<tr>
							<td class="tdInput">
								<asp:Label ID="lblState" runat="server" Text="State"></asp:Label><br />
								<asp:DropDownList ID="ddlState" Height="19px" Width="214px" runat="server" OnSelectedIndexChanged="OnSelChange" OnTextChanged="OnTextChanged"></asp:DropDownList>
							</td>

							<td class="tdInput">
								<asp:Label ID="LblPostalCode" runat="server" Text="Postal Code"></asp:Label><br />
								<asp:TextBox ID="txtPostalCode" runat="server"></asp:TextBox>
								<br />
							</td>
						</tr>
						<tr>
							<td colspan="2" class="tdInput">
								<asp:Label ID="Label1" runat="server" Text="Interest - Enter an interest you would like to match someone on."></asp:Label>
							</td>
						</tr>
						<tr>
							<td colspan="2" class="tdInput">
								<asp:TextBox ID="txtInterest" runat="server" MaxLength="50" Width="264px"></asp:TextBox>
							</td>
						</tr>
						<tr>
							<td colspan="2">
							</td>
						</tr>
						<tr>
							<td colspan="2" class="tdInput">
								<table class="tblNormal">
									<tr class="trVATop">
										<td class="tdRight">
											<asp:Label ID="Label2" runat="server" Text="Return Results : "></asp:Label>
										</td>
										<td>
											<asp:DropDownList ID="ddlResultCount" runat="server" AutoPostBack="True">
												<asp:ListItem Text="10" Value="10"></asp:ListItem>
												<asp:ListItem Text="25" Value="25" Selected="True"></asp:ListItem>
												<asp:ListItem Text="50" Value="50"></asp:ListItem>
												<asp:ListItem Text="75" Value="75"></asp:ListItem>
											</asp:DropDownList>
										</td>
										<td class="tdButton">
											<asp:ImageButton ID="btnFindFriends" runat="server" ImageUrl="/Images/Button/btnFindFriends.png" OnClick="OnClickFindFriends" />
										</td>

										<td class="tdButton">
											<asp:ImageButton ID="btnCancelFind" runat="server" ImageUrl="/Images/Button/btnCancel.png" OnClick="OnClickCancelFindFriends" />
										</td>
									</tr>
								</table>
							</td>
						</tr>
					</table>
				</asp:Panel>
				<asp:Panel ID="pnlFindResults" runat="server" Visible="False">
					<table class="tblNormal">
						<tr>
							<td>
								<span class="xlgSiteColorTxt">
									Find Results
								</span>
							</td>
						</tr>
						<tr>
							<td>
								<asp:Table ID="tblNewResults" runat="server" CssClass="tblResults"></asp:Table>
							</td>
						</tr>
					</table>
				</asp:Panel>
			</td>
		</tr>
	</table>
</asp:Content>
