<%@ Page Title="" Language="C#" MasterPageFile="~/MyTeams/OrgMaster.master" AutoEventWireup="true" CodeBehind="AddAffiliate.aspx.cs" Inherits="MyUSC.MyTeams.AddAffiliate" %>
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
								<asp:Label ID="Label1" runat="server" Text="Add Affiliate" CssClass="xlgSiteColorTxt"></asp:Label>
							</td>
						</tr>
						<tr>
							<td class="tdInput">
								<asp:Label ID="lbl1" runat="server" Text="Affiliate Name" CssClass="medSiteColorTxt"></asp:Label><br />
								<asp:Label ID="lblAffName" runat="server" Text="Org Name"></asp:Label>
							</td>
						</tr>
						<tr>
							<td class="tdInput">
								<asp:Label ID="lbl2" runat="server" Text="Affiliate Type" CssClass="medSiteColorTxt"></asp:Label><br />
								<asp:DropDownList ID="ddlAffType" runat="server" Width="150px" AutoPostBack="True">
									<asp:ListItem Text="Undefined" Value="-1" Selected="True"></asp:ListItem>
									<asp:ListItem Text="Sponsor" Value="0" Selected="False"></asp:ListItem>
									<asp:ListItem Text="Charter" Value="1" Selected="False"></asp:ListItem>
									<asp:ListItem Text="Parent" Value="2" Selected="False"></asp:ListItem>
									<asp:ListItem Text="Peer" Value="3" Selected="False"></asp:ListItem>
									<asp:ListItem Text="Child" Value="4" Selected="False"></asp:ListItem>
									<asp:ListItem Text="None" Value="10" Selected="False"></asp:ListItem>
								</asp:DropDownList>
							</td>
						</tr>
						<tr>
							<td class="tdInput">
								<asp:Label ID="lbl3" runat="server" Text="Note" CssClass="medSiteColorTxt"></asp:Label><br />
								<asp:TextBox ID="txtAffNotes" runat="server" TextMode="MultiLine" Width="500px" Height="50px" MaxLength="200"></asp:TextBox>
							</td>
						</tr>
						<tr>
							<td class="tdInput">
								<table>
									<tr align="center">
										<td class="tdButton">
											<asp:ImageButton ID="btnAddAffiliates" runat="server" ImageUrl="~/Images/Button/btnAdd.png" OnClick="OnClickAdd" />
										</td>
										<td class="tdButton">
											<asp:ImageButton ID="btnCancel" runat="server" ImageUrl="/Images/Button/btnCancel.png" OnClick="OnClickCancel" />
										</td>
									</tr>
								</table>
							</td>
						</tr>
					</table>
				</asp:Panel>
			</td>
		</tr>
	</table>
</asp:Content>
