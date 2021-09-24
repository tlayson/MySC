<%@ Page Title="" Language="C#" MasterPageFile="~/MyTeams/OrgMaster.master" AutoEventWireup="true" CodeBehind="OrgMail.aspx.cs" Inherits="MyUSC.MyTeams.OrgMail" %>
<%@ MasterType VirtualPath="~/MyTeams/OrgMaster.Master" %>
<%@ Register TagPrefix="CE" Namespace="CuteEditor" Assembly="CuteEditor" %>
<%@ Register TagPrefix="CE" namespace="CuteEditor.ImageEditor" assembly="CuteEditor.ImageEditor" %>
<asp:Content ID="Content1" ContentPlaceHolderID="OrgHeadContent" runat="server">
	<script type="text/javascript" src="~/js/GA.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="OrgContent" runat="server">
	<table class="tblNormal">
		<tr class="trAdmin">
			<td class="tdInput">
				<asp:Label ID="Label1" runat="server" Text="Send options" CssClass="medSiteColorTxt"></asp:Label>
			</td>
			<td class="tdInput">

			</td>
		</tr>
		<tr class="trNormal">
			<td class="tdInput">
				<asp:CheckBox ID="chkAllMembers" runat="server" Text="All Members" />
			</td>
			<td class="tdInput">
				<asp:CheckBox ID="chkAllFollowers" runat="server" Text="All Followers" />
			</td>
		</tr>
		<tr class="trNormal">
			<td class="tdInput" colspan="2">
				<table class="tblNormal">
					<tr class="trNormal">
						<td class="tdCenter">
							<MSC:TBSCListBox ID="lbUnselected" runat="server" Width="250px" Rows="12"></MSC:TBSCListBox>
						</td>
						<td class="tdInput" style="vertical-align: middle">
							<table class="tblNormal">
								<tr class="trAdmin">
									<td class="tdCenter">
										<MSC:TBSCLinkButton ID='lbnAddAll' runat="server">Add All >></MSC:TBSCLinkButton>
									</td>
								</tr>
								<tr class="trAdmin">
									<td class="tdCenter">
										<MSC:TBSCLinkButton ID='lbnAddSelected' runat="server">Add >></MSC:TBSCLinkButton>
									</td>
								</tr>
								<tr class="trAdmin">
									<td class="tdCenter">
										<MSC:TBSCLinkButton ID='lbnRemoveSelected' runat="server"><< Remove</MSC:TBSCLinkButton>
									</td>
								</tr>
								<tr class="trAdmin">
									<td class="tdCenter">
										<MSC:TBSCLinkButton ID='lbnRemoveAll' runat="server"><< Remove All</MSC:TBSCLinkButton>
									</td>
								</tr>
							</table>
						</td>
						<td class="tdCenter">
							<MSC:TBSCListBox ID="lbSelected" runat="server" Width="250px" Rows="12"></MSC:TBSCListBox>
						</td>
					</tr>
				</table>
			</td>
		</tr>
		<tr class="trNormal">
			<td class="tdInput">
				<asp:CheckBox ID="chkAdditional" runat="server" Text="Additional" />
			</td>
			<td class="tdInput">
				<asp:TextBox ID="txtAdditional" runat="server" Width="550px"></asp:TextBox>
			</td>
		</tr>
		<tr class="trNormal">
			<td class="tdInput">
				<asp:Label ID="Label3" runat="server" Text="Subject" CssClass="medSiteColorTxt"></asp:Label>
			</td>
			<td class="tdInput">
				<asp:TextBox ID="txtSubject" runat="server" Width="550px"></asp:TextBox>
			</td>
		</tr>
		<tr class="trAdmin">
			<td class="tdInput">
				<asp:Label ID="Label2" runat="server" Text="Message" CssClass="medSiteColorTxt"></asp:Label>
			</td>
			<td class="tdInput">

			</td>
		</tr>
		<tr class="trNormal">
			<td class="tdInput" colspan="2">
				<CE:editor ID="txtEmail" runat="server" Width="800px" 
					AutoConfigure="Minimal" BorderWidth="3px" CodeViewTemplateItemList="" 
                    DownLevelRows="50" Focus="True" ResizeMode="AutoAdjust" ShowBottomBar="False" 
                    ShowCodeViewToolBar="False" ShowDecreaseButton="False" 
                    ShowEnlargeButton="False" ShowHtmlMode="False" ShowPreviewMode="False" 
                    ShowTagSelector="False" UseSimpleAmpersand="True" BackColor="#FFFFFF" 
                    Font-Underline="True" ForeColor="Black" Font-Names="Verdana" 
                    Font-Size="Small" Height="350px">
                </CE:editor>

			</td>
		</tr>
		<tr class="trNormal">
			<td class="tdInput">

			</td>
			<td class="tdInput">
				<asp:Button ID="btnSend" runat="server" Text="Send" CssClass="btnNormal" OnClick="OnClickSend" />
			</td>
		</tr>
	</table>
</asp:Content>
