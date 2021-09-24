<%@ Page Title="" Language="C#" MasterPageFile="~/USC.Master" AutoEventWireup="true" CodeBehind="SendInvite.aspx.cs" Inherits="MyUSC.SendInvite" %>
<%@ MasterType VirtualPath="~/USC.Master" %>
<%@ Register TagPrefix="CE" Namespace="CuteEditor" Assembly="CuteEditor" %>
<%@ Register TagPrefix="CE" namespace="CuteEditor.ImageEditor" assembly="CuteEditor.ImageEditor" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
	<script type="text/javascript" src="~/js/GA.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
	<table style="width: 1060px; height: 650px; background-image: url('Images/Background.png')">
		<tr valign="top">
			<td class="tdInput">
				<asp:Literal ID="litInstructions" runat="server"></asp:Literal>
			</td>
		</tr>
		<tr valign="top">
			<td class="tdInput">
				<asp:Label ID="lblEmailAddress" runat="server" Text="Email Address(es): "></asp:Label>
				<asp:TextBox ID="txtEmailAddress" runat="server" MaxLength="250" Width="365px"></asp:TextBox>
			</td>
		</tr>
		<tr valign="top">
			<td class="tdInput">
				<CE:editor ID="txtDetails" runat="server" Width="1000px" 
					AutoConfigure="Minimal" BorderWidth="3px" CodeViewTemplateItemList="" 
                    DownLevelRows="50" Focus="True" ResizeMode="AutoAdjust" ShowBottomBar="False" 
                    ShowCodeViewToolBar="False" ShowDecreaseButton="False" 
                    ShowEnlargeButton="False" ShowHtmlMode="False" ShowPreviewMode="False" 
                    ShowTagSelector="False" UseSimpleAmpersand="True" BackColor="#FFFFFF" 
                    Font-Underline="True" ForeColor="Black" Font-Names="Verdana" 
                    Font-Size="Small" Height="200px">
                </CE:editor>
			</td>
		</tr>
		<tr valign="top">
			<td class="tdInput">
				<table width="100%">
					<tr>
						<td class="tdInput" align="right">
							<asp:ImageButton ID="btnSend" runat="server" ImageUrl="~/Images/Button/btnSend.png" OnClick="OnClickSend" />
						</td>
						<td class="tdInput" align="left">
							<asp:ImageButton ID="btnCancel" runat="server" ImageUrl="~/Images/Button/btnCancel.png" OnClick="OnClickCancel" />
						</td>
					</tr>
				</table>
			</td>
		</tr>
	</table>

</asp:Content>
