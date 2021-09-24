<%@ Page Title="" Language="C#" MasterPageFile="~/MyTeams/OrgMaster.master" AutoEventWireup="true" CodeBehind="FindAffiliate.aspx.cs" Inherits="MyUSC.MyTeams.FindAffiliate" %>
<%@ MasterType VirtualPath="~/MyTeams/OrgMaster.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="OrgHeadContent" runat="server">
	<script type="text/javascript" src="~/js/GA.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="OrgContent" runat="server">
	<table class="tblNormal" >
		<tr class="trNormal">
			<td class="tdInput">
				<asp:Panel ID="pnlInvite" runat="server">
					<table class="tblNormal">
						<tr>
							<td class="tdInput">
								<asp:Label ID="Label1" runat="server" Text="Invite" CssClass="xlgSiteColorTxt"></asp:Label>
							</td>
							<td style="text-align: center">
							</td>
						</tr>
						<tr>
							<td class="tdInput" colspan="2">
								Fill out one or more fields below and click FIND to search for other organizations you are affiliated with. You can use partial entries if you are not sure of the spelling. For example, &#39;smi&#39; will find &#39;Smith&#39;.
							</td>
						</tr>
						<tr>
							<td class="tdInput">
								<asp:Label ID="lblOrgName" runat="server" Text="Organization Name"></asp:Label><br />
								<asp:TextBox ID="txtOrgName" runat="server" Width="214px"></asp:TextBox>
							</td>

							<td class="tdInput">
								<asp:Label ID="lblState" runat="server" Text="State"></asp:Label><br />
								<MSC:StateDropDown ID="ddlState" runat="server"></MSC:StateDropDown>
							</td>
						</tr>
						<tr>
							<td colspan ="2" class="tdInput">
								<asp:Label ID="lblOrgType" runat="server" Text="Organization Type"></asp:Label><br />
								<MSC:DDLOrgType ID="ddlOrgType" runat="server"></MSC:DDLOrgType>
							</td>
						</tr>
						<tr>
							<td class="tdInput">

							</td>
							<td class="tdInput">
								<table>
									<tr align="center">
										<td class="tdButton">
											<asp:ImageButton ID="btnFindAffiliates" runat="server" ImageUrl="/Images/Button/btnFind.png" OnClick="OnClickFind" />
										</td>
										<td class="tdButton">
											<asp:ImageButton ID="btnCancelFind" runat="server" ImageUrl="/Images/Button/btnCancel.png" OnClick="OnClickCancel" />
										</td>
									</tr>
								</table>
							</td>
						</tr>
					</table>
				</asp:Panel>
			</td>
		</tr>
		<tr>
			<td>
				<asp:Panel ID="pnlResults" runat="server">
					<hr />
					<table width="100%">
						<tr valign="top">
							<td class="tdInput">
								<asp:Label ID="lblResultMessage" runat="server" CssClass="medSiteColorTxt"></asp:Label>&nbsp;
							</td>
							<td class="tdInput" style="text-align: right">
								<asp:LinkButton ID="btnCreateNew" runat="server" PostBackUrl="~/MyTeams/CreateOrg.aspx" OnClick="OnClickCreateNew">Didn't find who you were looking for? Create a new one!</asp:LinkButton>
							</td>
						</tr>
					</table>
					<br />
					<asp:Table ID="tblResults" Width="100%" runat="server">

					</asp:Table>
				</asp:Panel>
			</td>
		</tr>
	</table>
</asp:Content>
