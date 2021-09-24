<%@ Page Title="" Language="C#" MasterPageFile="~/MyTeams/OrgMaster.master" AutoEventWireup="true" CodeBehind="ManageNews.aspx.cs" Inherits="MyUSC.MyTeams.ManageNews" %>
<%@ MasterType VirtualPath="~/MyTeams/OrgMaster.Master" %>
<%@ Register assembly="CuteEditor, Version=6.6.0.0, Culture=neutral, PublicKeyToken=3858aa6802b1223a" namespace="CuteEditor" tagprefix="CE" %>
<asp:Content ID="Content1" ContentPlaceHolderID="OrgHeadContent" runat="server">
	<script type="text/javascript" src="~/js/GA.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="OrgContent" runat="server">
	<table class="tblNormal" >
		<tr class="trNormal">
			<td class="tdInput">
				<asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
			</td>
		</tr>
		<tr class="trNormal">
			<td class="tdInput">
				<CE:editor ID="txtNews" runat="server" Width="800px" 
					AutoConfigure="Minimal" BorderWidth="3px" CodeViewTemplateItemList="" 
					DownLevelRows="50" Focus="True" ResizeMode="AutoAdjust" ShowBottomBar="true" 
					ShowCodeViewToolBar="true" ShowDecreaseButton="true" 
					ShowEnlargeButton="true" ShowHtmlMode="true" ShowPreviewMode="true" 
					ShowTagSelector="true" UseSimpleAmpersand="True" BackColor="#FFFFFF" 
					Font-Underline="True" ForeColor="Black" Font-Names="Verdana" 
					Font-Size="Small" Height="200px" MaxTextLength="1023">
				</CE:editor>
			</td>
		</tr>
		<tr class="trNormal">
			<td class="tdInput">
				<table class="tblNormal">
					<tr class="trNormal">
						<td class="tdRight">
							<asp:ImageButton ID="btnOK" runat="server" ImageUrl="~/Images/Button/btnOK.png" OnClick="OnClickOK" />
						</td>
						<td class="tdLeft">
							<asp:ImageButton ID="btnCancel" runat="server" ImageUrl="~/Images/Button/btnCancel.png" OnClick="OnClickCancel" />
						</td>
					</tr>
				</table>
			</td>
		</tr>
	</table>
</asp:Content>
