<%@ Page Title="" Language="C#" MasterPageFile="~/USC.Master" AutoEventWireup="true" CodeBehind="ReadMessage.aspx.cs" Inherits="MyUSC.ReadMessage" %>
<%@ MasterType VirtualPath="~/USC.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
	<script type="text/javascript" src="~/js/GA.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
	<table style="width: 1060px; height: 650px; background-image: url('Images/Background.png')">
		<tr valign="top">
			<td class="tdError" colspan="2" style="height: 25px">
				<asp:Label ID="lblMsgError" runat="server" Text=""></asp:Label>
			</td>
		</tr>
		<tr valign="top">
			<td class="tdInput">
				From : <asp:Label ID="lblFrom" runat="server" Text="Sender"></asp:Label>
			</td>
			<td class="tdInput">
				Story : <asp:Label ID="lblDiscussion" runat="server" Text="Discussion"></asp:Label>
			</td>
		</tr>
		<tr valign="top">
			<td class="tdInput">
				Date : <asp:Label ID="lblDate" runat="server" Text="Date"></asp:Label>
			</td>
			<td class="tdInput">
				Link : <asp:Label ID="lblLink" runat="server" Text="Link"></asp:Label>
			</td>
		</tr>
		<tr valign="top">
			<td class="tdInput">
				Title : <asp:Label ID="lblTitle" runat="server" Text="Title"></asp:Label>
			</td>
			<td class="tdInput">
				Video : <asp:Label ID="lblVideo" runat="server" Text="Video"></asp:Label>
			</td>
		</tr>
		<tr valign="top" style="height: 360px">
			<td class="tdInput" colspan="2">
				<table>
					<tr valign="top">
						<td class="tdInput">
							Message : <br />
							<asp:TextBox ID="txtMessage" runat="server" Height="300px" ReadOnly="True" TextMode="MultiLine" Width="650px"></asp:TextBox>
						</td>
						<td class="tdInput">
							<asp:Image ID="imgMsgPhoto" runat="server" />
						</td>
					</tr>
				</table>
			</td>
		</tr>
		<tr valign="top">
			<td class="tdInput" colspan="2">
				<table width="100%">
					<tr>
						<td class="tdInput" style="width: 20%" >

						</td>
						<td class="tdInput" style="width: 20%" >
							<asp:ImageButton ID="btnReturn" ImageUrl="~/Images/Button/btnReturn.png" runat="server" OnClick="OnClickReturn" />
						</td>
						<td class="tdInput" style="width: 20%" >
							<asp:ImageButton ID="btnReply" ImageUrl="~/Images/Button/btnReply.png" runat="server" OnClick="OnClickReply" />
						</td>
						<td class="tdInput" style="width: 20%" >
							<asp:ImageButton ID="btnDelete" ImageUrl="~/Images/Button/btnDelete.png" runat="server" OnClick="OnClickDelete" />
						</td>
						<td class="tdInput" style="width: 20%" >
							<asp:ImageButton ID="btnMarkUnread" ImageUrl="~/Images/Button/btnMarkUnread.png" runat="server" OnClick="OnClickMarkUnread" />
						</td>
					</tr>
				</table>
			</td>
		</tr>
	</table>
</asp:Content>
