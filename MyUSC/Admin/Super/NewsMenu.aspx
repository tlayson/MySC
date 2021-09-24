<%@ Page Title="" Language="C#" MasterPageFile="~/USC.Master" AutoEventWireup="true" CodeBehind="NewsMenu.aspx.cs" Inherits="MyUSC.Admin.Super.NewsMenu" %>
<%@ MasterType VirtualPath="~/USC.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
	<asp:HiddenField ID="hf1" runat="server" Value="0" />
	<table style="width: 1060px; height: 650px; background-image: url('Images/Background.png')">
		<tr valign="top">
			<td class="tdInput">
				<table width="100%">
					<tr valign="top">
						<td class="tdInput" colspan="2">
							<asp:Label ID="Label1" runat="server" Text="Manage News Menu Items" CssClass="lgSiteColorTxt"></asp:Label>
						</td>
						<td class="tdInput">
							<asp:Button ID="btnSave" runat="server" Text="Save Item" OnClick="OnClickSave" />
						</td>
					</tr>
					<tr valign="top">
						<td class="tdInput">
							<asp:Label ID="Label5" runat="server" Text="Select : "></asp:Label>
						</td>
						<td class="tdInput">
							<asp:DropDownList ID="ddlLevelItems" runat="server" Width="350px" AutoPostBack="True" OnSelectedIndexChanged="OnSelChangeItems"></asp:DropDownList>
							&nbsp;&nbsp;
							</td>
						<td class="tdInput">

						</td>
					</tr>
					<tr valign="top">
						<td class="tdInput">
							<asp:Label ID="Label2" runat="server" Text="Key"></asp:Label>
						</td>
						<td class="tdInput">
							<asp:Label ID="lblKey" runat="server" Text="-"></asp:Label>
						</td>
						<td class="tdInput">

						</td>
					</tr>
					<tr valign="top">
						<td class="tdInput">
							<asp:Label ID="Label4" runat="server" Text="Parent ID"></asp:Label>
						</td>
						<td class="tdInput">
							<asp:DropDownList ID="ddlParent" runat="server" Height="16px" Width="350px"></asp:DropDownList>
						</td>
						<td class="tdInput">

						</td>
					</tr>
					<tr valign="top">
						<td class="tdInput">
							<asp:Label ID="Label6" runat="server" Text="Display Name"></asp:Label>
						</td>
						<td class="tdInput">
							<asp:TextBox ID="txtDisplayName" runat="server" Width="350px"></asp:TextBox>
						</td>
						<td class="tdInput">
							<asp:Label ID="lblDisplayNameError" runat="server" Text="" CssClass="medErrorTxt"></asp:Label>
						</td>
					</tr>
					<tr valign="top">
						<td class="tdInput">
							<asp:Label ID="Label7" runat="server" Text="Description"></asp:Label>
						</td>
						<td class="tdInput">
							<asp:TextBox ID="txtDescription" runat="server" Width="350px"></asp:TextBox>
						</td>
						<td class="tdInput">
							
						</td>
					</tr>
					<tr valign="top">
						<td class="tdInput">
							<asp:Label ID="Label8" runat="server" Text="RSSID"></asp:Label>
						</td>
						<td class="tdInput">
							<asp:DropDownList ID="ddlRSSFeeds" runat="server" Width="350px">
							</asp:DropDownList>
							&nbsp;&nbsp;
							</td>
						<td class="tdInput">

							<asp:LinkButton ID="LinkButton2" runat="server" PostBackUrl="~/Admin/Super/RSSFeeds.aspx">Edit RSS Feeds</asp:LinkButton>

						</td>
					</tr>
					<tr valign="top">
						<td class="tdInput">
							<asp:Label ID="Label15" runat="server" Text="News Target"></asp:Label>
						</td>
						<td class="tdInput">
							<asp:DropDownList ID="ddlTarget" runat="server" Width="350px">
								<asp:ListItem Text="None" Value="" Selected="True"></asp:ListItem>
								<asp:ListItem Text="_blank" Value="_blank"></asp:ListItem>
								<asp:ListItem Text="_parent" Value="_parent"></asp:ListItem>
								<asp:ListItem Text="_search" Value="_search"></asp:ListItem>
								<asp:ListItem Text="_self" Value="_self"></asp:ListItem>
								<asp:ListItem Text="_top" Value="_top"></asp:ListItem>
							</asp:DropDownList>
							&nbsp;&nbsp;
							</td>
						<td class="tdInput">

							<asp:LinkButton ID="LinkButton1" runat="server" PostBackUrl="~/Admin/Super/RSSFeeds.aspx">Edit RSS Feeds</asp:LinkButton>

						</td>
					</tr>
					<tr valign="top">
						<td class="tdInput">
							<asp:Label ID="Label11" runat="server" Text="Website"></asp:Label>
						</td>
						<td class="tdInput">
							<asp:TextBox ID="txtWebsite" runat="server" Width="350px"></asp:TextBox>
						</td>
						<td class="tdInput">
							
						</td>
					</tr>
					<tr valign="top">
						<td class="tdInput">
							<asp:Label ID="Label12" runat="server" Text="Notes"></asp:Label>
						</td>
						<td class="tdInput">
							<asp:TextBox ID="txtNotes" runat="server" Width="350px"></asp:TextBox>
						</td>
						<td class="tdInput">
							
						</td>
					</tr>
					<tr valign="top">
						<td class="tdInput">
							<asp:Label ID="Label9" runat="server" Text="Logo URL"></asp:Label>
						</td>
						<td class="tdInput">
							<asp:TextBox ID="txtLogoURL" runat="server" Width="350px"></asp:TextBox>
						</td>
						<td class="tdInput">
							
						</td>
					</tr>
					<tr valign="top">
						<td class="tdInput">
							<asp:Label ID="Label10" runat="server" Text="Display Sequence"></asp:Label>
						</td>
						<td class="tdInput">
							<asp:TextBox ID="txtSequence" runat="server" Width="100px"></asp:TextBox>
						</td>
						<td class="tdInput">
							<asp:Label ID="lblSequenceError" runat="server" Text="" CssClass="medErrorTxt"></asp:Label>
						</td>
					</tr>
					<tr valign="top">
						<td class="tdInput">
							<asp:Label ID="Label3" runat="server" Text="Active"></asp:Label>
						</td>
						<td class="tdInput">
							<asp:CheckBox ID="chkIsActive" runat="server" Text="Is item active?" />
						</td>
						<td class="tdInput">
							
						</td>
					</tr>
					<tr valign="top">
						<td class="tdInput">
							<asp:Label ID="Label13" runat="server" Text="Last update"></asp:Label>
						</td>
						<td class="tdInput">
							<asp:Label ID="lblLastUpdate" runat="server" Text="-"></asp:Label>
						</td>
						<td class="tdInput">
							
						</td>
					</tr>
					<tr valign="top">
						<td class="tdInput">
							<asp:Label ID="Label14" runat="server" Text="Menu Depth"></asp:Label>
						</td>
						<td class="tdInput">
							<asp:Label ID="lblMenuDepth" runat="server" Text="-"></asp:Label>
						</td>
						<td class="tdInput">
							
						</td>
					</tr>
					<tr valign="top">
						<td class="tdInput">
							<asp:Button ID="btnSave0" runat="server" Text="Save Item" OnClick="OnClickSave" />
						</td>
						<td class="tdInput">

						</td>
						<td class="tdInput">
							
						</td>
					</tr>
				</table>
			</td>
		</tr>
	</table>
</asp:Content>
