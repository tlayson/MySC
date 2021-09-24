<%@ Page Title="" Language="C#" MasterPageFile="~/USC.Master" AutoEventWireup="true" CodeBehind="Friends.aspx.cs" Inherits="MyUSC.Friends" %>
<%@ MasterType VirtualPath="~/USC.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
	<script type="text/javascript" src="~/js/GA.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
	<table style="width: 1060px; height: 650px; background-image: url('Images/Background.png')">
		<tr valign="top">
			<!-- Friends page menu  -->
			<td class="tdInput" style="width: 200px" >
				<table width="100%">
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
							<asp:Image ID="imgMenuMsgBoard" ImageUrl="~/Images/Button/MenuSelect.jpg" runat="server" />
						</td>
						<td class="tdInput">
							<asp:LinkButton ID="lnkMenuMsgBoard" runat="server" CssClass="miFriendsNormal" OnClick="OnClickMenuMessageBoard" Width="125px">Message Board</asp:LinkButton><br />
						</td>
					</tr>
					<tr>
						<td class="tdInput">
							<asp:Image ID="imgMenuFindFriends" ImageUrl="~/Images/Button/MenuSelect.jpg" runat="server" />
						</td>
						<td class="tdInput">
							<asp:LinkButton ID="lnkMenuFindFriends" runat="server" CssClass="miFriendsNormal" OnClick="OnClickMenuFindFriends">Find Friends</asp:LinkButton><br />
						</td>
					</tr>
					<tr>
						<td class="tdInput">
							<asp:Image ID="imgMenuFriendRequests" ImageUrl="~/Images/Button/MenuSelect.jpg" runat="server" />
						</td>
						<td class="tdInput">
							<asp:LinkButton ID="lnkMenuFriendRequests" runat="server" CssClass="miFriendsNormal" OnClick="OnClickMenuFriendRequests">Friend Requests</asp:LinkButton><br />
						</td>
					</tr>
					<tr>
						<td class="tdInput">
							<asp:Image ID="imgMenuAllFriends" ImageUrl="~/Images/Button/MenuSelect.jpg" runat="server" />
						</td>
						<td class="tdInput">
							<asp:LinkButton ID="lnkMenuAllFriends" runat="server" CssClass="miFriendsNormal" OnClick="OnClickMenuAllFriends">My Friends</asp:LinkButton><br />
						</td>
					</tr>
				</table>
			</td>
			<td class="tdInput">
				<asp:Panel ID="pnlMsgBoard" runat="server">
					<table width="100%">
						<tr>
							<td class="tdInput">
								<span class="xlgSiteColorTxt">
									Messages
								</span>
							</td>
						</tr>
						<tr>
							<td class="tdInput" align="right" >
								<asp:ImageButton ID="btnNewMessage" ImageUrl="~/Images/Button/btnNewMsg.png" runat="server" OnClick="OnClickNewMsg" />
							</td>
						</tr>
						<tr>
							<td>
								<asp:Table ID="tblMessages" runat="server" Width="100%" HorizontalAlign="Left">
									<asp:TableHeaderRow>
										<asp:TableHeaderCell CssClass="tdInput" HorizontalAlign="Left">
											From
										</asp:TableHeaderCell>
										<asp:TableHeaderCell CssClass="tdInput" HorizontalAlign="Left">
											Title
										</asp:TableHeaderCell>
										<asp:TableHeaderCell CssClass="tdInput" HorizontalAlign="Left">
											Date
										</asp:TableHeaderCell>
									</asp:TableHeaderRow>
								</asp:Table>
							</td>
						</tr>
						<tr>
							<td>
								<asp:Panel ID="pnlNoMessages" runat="server">
									<span class="medSiteColorTxt">
										You do not currently have any messages to view.
									</span>
								</asp:Panel>
							</td>
						</tr>
					</table>
				</asp:Panel>
				<asp:Panel ID="pnlFriendRequests" runat="server">
					<table width="100%">
						<tr>
							<td>
								<span class="xlgSiteColorTxt">
									Friend Requests
								</span>
							</td>
						</tr>
						<tr>
							<td>
								<asp:Table ID="tblFriendRequests" runat="server" Width="100%" HorizontalAlign="Left">
									<asp:TableHeaderRow>
										<asp:TableHeaderCell CssClass="tdInput" HorizontalAlign="Left">
											Pic
										</asp:TableHeaderCell>
										<asp:TableHeaderCell CssClass="tdInput" HorizontalAlign="Left">
											Name
										</asp:TableHeaderCell>
										<asp:TableHeaderCell CssClass="tdInput" HorizontalAlign="Left">
											&nbsp;
										</asp:TableHeaderCell>
										<asp:TableHeaderCell CssClass="tdInput" HorizontalAlign="Left">
											&nbsp;
										</asp:TableHeaderCell>
									</asp:TableHeaderRow>
								</asp:Table>
							</td>
						</tr>
						<tr>
							<td>
								<asp:Panel ID="pnlNoFriendRequests" runat="server">
									<span class="medSiteColorTxt">
										You do not currently have any friend requests.
									</span>
								</asp:Panel>
							</td>
						</tr>
					</table>
				</asp:Panel>
				<asp:Panel ID="pnlAllFriends" runat="server">
					<table width="100%">
						<tr>
							<td>
								<span class="xlgSiteColorTxt">
									My Friends
								</span>
							</td>
						</tr>
						<tr>
							<td>
								<asp:Table ID="tblFriendsList" runat="server" Width="100%" HorizontalAlign="Left">
									<asp:TableHeaderRow>
										<asp:TableHeaderCell CssClass="tdInput" HorizontalAlign="Left">
											Pic
										</asp:TableHeaderCell>
										<asp:TableHeaderCell CssClass="tdInput" HorizontalAlign="Left">
											Name
										</asp:TableHeaderCell>
										<asp:TableHeaderCell CssClass="tdInput" HorizontalAlign="Left">
											Last login
										</asp:TableHeaderCell>
										<asp:TableHeaderCell CssClass="tdInput" HorizontalAlign="Left">
											&nbsp;
										</asp:TableHeaderCell>
									</asp:TableHeaderRow>
								</asp:Table>
							</td>
						</tr>
						<tr>
							<td>
								<asp:Panel ID="pnlNoFriends" runat="server">
									<span class="medSiteColorTxt">
										You do not currently have any friends.  Select Find Friends to search for people you know.
									</span>
								</asp:Panel>
							</td>
						</tr>
					</table>
				</asp:Panel>
			</td>
		</tr>
	</table>
</asp:Content>
