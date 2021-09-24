<%@ Page Title="" Language="C#" MasterPageFile="~/MyTeams/OrgMaster.master" AutoEventWireup="true" CodeBehind="InviteMember.aspx.cs" Inherits="MyUSC.MyTeams.InviteMember" %>
<%@ MasterType VirtualPath="~/MyTeams/OrgMaster.Master" %>
<%@ Register TagPrefix="CE" Namespace="CuteEditor" Assembly="CuteEditor" %>
<%@ Register TagPrefix="CE" namespace="CuteEditor.ImageEditor" assembly="CuteEditor.ImageEditor" %>
<asp:Content ID="Content1" ContentPlaceHolderID="OrgHeadContent" runat="server">
	<script type="text/javascript" src="~/js/GA.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="OrgContent" runat="server">
	<asp:HiddenField ID="hfUserID" runat="server" />
	<table class="tblMyTeamsPage">
		<tr class="trShort">
			<td class="tdInput">
				<asp:Label ID="lblInstructions" runat="server" Text="Label" CssClass="medSiteColorTxt"></asp:Label>
			</td>
		</tr>
		<tr class="trShort">
			<td class="tdInput">
				<table class="tblNormal">
					<tr class="trShort">
						<td class="tdInput">
							<asp:Label ID="Label1" runat="server" Text="First Name : " CssClass="medSiteColorTxt"></asp:Label>&nbsp;
							<asp:TextBox ID="txtFirst" runat="server" Width="200px"></asp:TextBox>
						</td>
						<td class="tdInput">
							<asp:Label ID="Label2" runat="server" Text="Last Name : " CssClass="medSiteColorTxt"></asp:Label>&nbsp;
							<asp:TextBox ID="txtLast" runat="server" Width="200px"></asp:TextBox>
						</td>
					</tr>
				</table>
			</td>
		</tr>
		<tr class="trShort">
			<td class="tdInput">
				<asp:Label ID="lblEmailAddress" runat="server" Text="Email Address : " CssClass="medSiteColorTxt"></asp:Label>
				&nbsp;
				<asp:TextBox ID="txtEmailAddress" runat="server" MaxLength="250" Width="415px"></asp:TextBox>
			</td>
		</tr>
		<tr class="trShort">
			<td class="tdInput">
				<asp:Label ID="Label5" runat="server" Text="Invite as : " CssClass="medSiteColorTxt"></asp:Label>
				&nbsp;
				<MSC:TBSCAccessDropDown ID="ddlAccessLevel" runat="server"></MSC:TBSCAccessDropDown>
			</td>
		</tr>
		<tr class="trShort">
			<td class="tdInput">
				<asp:Label ID="Label3" runat="server" Text="Message that will be sent to invitee." CssClass="medSiteColorTxt"></asp:Label>
			</td>
		</tr>
		<tr class="trShort">
			<td class="tdInput">
				<asp:Literal ID="htmlEmailMsg" runat="server"></asp:Literal>
			</td>
		</tr>
		<tr class="trShort">
			<td class="tdInput">
				<asp:Label ID="Label4" runat="server" Text="Personal Message" CssClass="medSiteColorTxt"></asp:Label>
			</td>
		</tr>
		<tr class="trShort">
			<td class="tdInput">
				<CE:editor ID="txtDetails" runat="server" Width="650px" 
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
		<tr class="trShort">
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
