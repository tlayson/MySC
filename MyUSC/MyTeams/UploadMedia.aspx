<%@ Page Title="" Language="C#" MasterPageFile="~/MyTeams/OrgMaster.master" AutoEventWireup="true" CodeBehind="UploadMedia.aspx.cs" Inherits="MyUSC.MyTeams.UploadMedia" %>
<%@ MasterType VirtualPath="~/MyTeams/OrgMaster.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="OrgHeadContent" runat="server">
	<script type="text/javascript" src="~/js/GA.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="OrgContent" runat="server">
	<table class="tblNormal">
		<tr class="trNormal">
			<td class="tdCenter">
				<table class="tblNormal">
					<tr class="trNormal">
						<td class="tdInput">
							<asp:FileUpload ID="fulUserPhoto" runat="server" BackColor="#666666" Font-Bold="True" ForeColor="White" Width="600px" />
						</td>
						<td class="tdInput">
							<asp:Button ID="btnUpload" runat="server" Text="Upload" OnClick="OnClickUpload" />
						</td>
						<td class="tdInput">
							<asp:Button ID="btnDone" runat="server" Text="Finished" OnClick="OnClickFinished" />
						</td>
					</tr>
				</table>
			</td>
		</tr>
		<tr class="trNormal">
			<td class="tdCenter">
				<asp:Image ID="imgUpload" runat="server" ImageUrl="~/Images/NoPhoto.JPG" />
			</td>
		</tr>
	</table>
</asp:Content>
