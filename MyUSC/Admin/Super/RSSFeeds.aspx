<%@ Page Title="" Language="C#" MasterPageFile="~/USC.Master" AutoEventWireup="true" CodeBehind="RSSFeeds.aspx.cs" Inherits="MyUSC.Admin.Super.RSSFeeds" %>
<%@ MasterType VirtualPath="~/USC.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .auto-style1 {
            vertical-align: top;
            color: #FF0000;
            font-weight: bolder;
            padding: 5px;
            width: 139px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:HiddenField ID="hf1" runat="server" Value="0" />
	<table style="width: 1060px">
		<tr valign="top">
			<td class="tdInput">
				<table width="100%">
					<tr valign="top">
						<td class="tdInput" colspan="2">
							<asp:Label ID="Label1" runat="server" Text="Manage RSS Feeds" CssClass="lgSiteColorTxt"></asp:Label>
						</td>
						<td class="tdInput">
							<asp:Button ID="btnSave" runat="server" Text="Save Item" OnClick="OnClickSave" />
						</td>
					</tr>
					<tr valign="top">
						<td class="auto-style1">
							<asp:Label ID="Label3" runat="server" Text="Select : "></asp:Label>
						</td>
						<td class="tdInput">
							<asp:DropDownList ID="ddlRSSFeeds" runat="server" Width="350px" AutoPostBack="True" OnSelectedIndexChanged="OnSelChangeSelect"></asp:DropDownList>
							&nbsp;&nbsp;
							</td>
						<td class="tdInput">

						</td>
					</tr>
					<tr valign="top">
						<td class="auto-style1">
							<asp:Label ID="Label2" runat="server" Text="Key"></asp:Label>
						</td>
						<td class="tdInput">
							<asp:Label ID="lblKey" runat="server" Text="-"></asp:Label>
						</td>
						<td class="tdInput">

						</td>
					</tr>
					<tr valign="top">
						<td class="auto-style1">
							<asp:Label ID="Label6" runat="server" Text="Display Name"></asp:Label>
						</td>
						<td class="tdInput">
							<asp:TextBox ID="txtDisplayName" runat="server" Width="250px"></asp:TextBox>
						</td>
						<td class="tdInput">
							<asp:Label ID="lblDisplayNameError" runat="server" Text="" CssClass="medErrorTxt"></asp:Label>
						</td>
					</tr>
					<tr valign="top">
						<td class="auto-style1">
							<asp:Label ID="Label7" runat="server" Text="Description"></asp:Label>
						</td>
						<td class="tdInput">
							<asp:TextBox ID="txtDescription" runat="server" Width="250px"></asp:TextBox>
						</td>
						<td class="tdInput">
							
						</td>
					</tr>
					<tr valign="top">
						<td class="auto-style1">
							<asp:Label ID="Label9" runat="server" Text="Feed URL"></asp:Label>
						</td>
						<td class="tdInput">
							<asp:TextBox ID="txtURL" runat="server" Width="350px"></asp:TextBox>
						</td>
						<td class="tdInput">
						    &nbsp;</td>
					</tr>
					<tr valign="top">
						<td class="auto-style1">
							<asp:Label ID="Label11" runat="server" Text="Website"></asp:Label>
						</td>
						<td class="tdInput">
							<asp:TextBox ID="txtWebsite" runat="server" Width="350px"></asp:TextBox>
							&nbsp;&nbsp;
							<asp:CheckBox ID="chkUseWebsite" runat="server" Text="Use website for news" />
						</td>
						<td class="tdInput">
							
						</td>
					</tr>
					<tr valign="top">
						<td class="auto-style1">
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
							<asp:Button ID="btnSave0" runat="server" Text="Save Item" OnClick="OnClickSave" />
						</td>
						<td class="tdInput">

						    <asp:Button ID="btnTestNews" runat="server" OnClick="OnClickTestNews" Text="Test News" />

						</td>
						<td class="tdInput">
							
						</td>
					</tr>
					<tr class="trAdmin">
						<td class="tdInput" colspan="3">
							<hr />
						</td>
					</tr>
					<tr class ="trNormal">
						<td class="tdInput" colspan="5">
							<asp:Table ID="tblRSSErrors" runat="server"></asp:Table>
						</td>
					</tr>
					<tr class ="trNormal">
						<td class="auto-tdInput" colspan="4">
                            <!--
						   <MSC:TBSCButton ID="btnVerifyFeeds" runat="server" Text="Verify RSS Feeds" OnClick="OnClickVerifyRSSFeeds" />
                            -->

						    <asp:Image ID="imgDisplay" runat="server" Height="400px" Width="800px" />

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
