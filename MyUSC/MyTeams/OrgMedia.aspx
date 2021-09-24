<%@ Page Title="" Language="C#" MasterPageFile="~/MyTeams/OrgMaster.master" AutoEventWireup="true" CodeBehind="OrgMedia.aspx.cs" Inherits="MyUSC.MyTeams.OrgMedia" %>
<%@ MasterType VirtualPath="~/MyTeams/OrgMaster.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="OrgHeadContent" runat="server">
	<script type="text/javascript" src="~/js/GA.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="OrgContent" runat="server">
	<table class="tblMyTeamsPage">
		<tr class="trAdmin">
			<td class="tdInput">
				<MSC:TBSCPanel ID='pnlUpload' runat="server" Visible="False">
					<MSC:TBSCLinkButton ID='lbnUpdate' runat="server" OnClick="OnClickUpload">Upload Photos</MSC:TBSCLinkButton>
				</MSC:TBSCPanel>
			</td>
		</tr>
		<tr class="trAdmin">
			<td class="tdCenter">
				<asp:LinkButton ID="lbnFirstPage" runat="server" CommandName="first" OnClick="OnClickFirst" Width="125px"><< First</asp:LinkButton>
				<asp:LinkButton ID="lbnPrevPage" runat="server" CommandName="prev" OnClick="OnClickPrev" Width="125px">< Previous</asp:LinkButton>
				<asp:LinkButton ID="lbnNextPage" runat="server" CommandName="next" OnClick="OnClickNext" Width="125px">Next ></asp:LinkButton>
				<asp:LinkButton ID="lbnLastPage" runat="server" CommandName="last" OnClick="OnClickLast" Width="125px">Last >></asp:LinkButton>
			</td>
		</tr>
		<tr class="trNormal">
			<td class="tdInput">
				<asp:DataList runat="server" RepeatColumns="5" RepeatDirection="Horizontal" Width="800px" ID="dlOrgMedia">
				<ItemTemplate>
					<asp:ImageButton ID="IB_tn" runat="server" ImageUrl='<%# "/OrgFolders/" + this.OrgID  + "/Media/" + Eval("Url") %>' Width="100px" Height="100px" OnClick="IB_tn_Click" CommandArgument='<%# Container.ItemIndex %>' />
				</ItemTemplate>
				<SelectedItemStyle BorderColor="Red" BorderWidth="1px" />
				</asp:DataList>
			</td>
		</tr>
		<tr class="trNormal">
			<td class="tdCenter">
				<asp:Image ID="Image1" runat="server" ImageUrl="~/Images/NoPhoto.JPG" />
			</td>
		</tr>
	</table>
</asp:Content>
