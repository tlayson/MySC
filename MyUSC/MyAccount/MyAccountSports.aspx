<%@ Page Title="" Language="C#" MasterPageFile="~/USC.Master" AutoEventWireup="true" CodeBehind="MyAccountSports.aspx.cs" Inherits="MyUSC.MyAccount.MyAccountSports" %>
<%@ MasterType VirtualPath="~/USC.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
	<script type="text/javascript" src="~/js/GA.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
	<table style="width: 1060px; height: 650px; background-image: url('Images/Background.png')">
		<tr valign="top">
			<td class="tdInput">
				<table width="100%">
					<tr valign="top">
						<td class="tdInput" style="width: 700px">
							<asp:Label ID="lblInstructions" runat="server" Text="Instructions go here ..."></asp:Label>
						</td>
						<td class="tdInput" align="right">
							<asp:ImageButton ID="btnOK" runat="server" ImageUrl="~/Images/Button/btnOK.png" OnClick="OnClickOK" />
						</td>
					</tr>
					<tr valign="top">
						<td class="tdInput" colspan="2">
							<asp:Label ID="lblInterests" runat="server" Text="Enter your sports interests : "></asp:Label><br />
							<asp:TextBox ID="txtInterests" runat="server" Height="75px" TextMode="MultiLine" Width="806px" MaxLength="500"></asp:TextBox>
						</td>
					</tr>
					<tr valign="top">
						<td class="tdInput" colspan="2">
							<asp:Label ID="lblPickSports" runat="server" Text="Pick the sports you want to show on the Sports News menu : "></asp:Label>
						</td>
					</tr>
					<tr valign="top">
						<td class="tdInput" colspan="2">
							<table width="100%">
								<tr valign="top">
									<td class="tdInput">
										<asp:CheckBox ID="chkMLB" runat="server" Text="MLB" />
									</td>
									<td class="tdInput">
										<asp:CheckBox ID="chkNFL" runat="server" Text="NFL" />
									</td>
									<td class="tdInput">
										<asp:CheckBox ID="chkNBA" runat="server" Text="NBA" />
									</td>
								</tr>
								<tr valign="top">
									<td class="tdInput">
										<asp:CheckBox ID="chkNCAA" runat="server" Text="NCAA" />
									</td>
									<td class="tdInput">
										<asp:CheckBox ID="chkNHL" runat="server" Text="NHL" />
									</td>
									<td class="tdInput">
										<asp:CheckBox ID="chkWNBA" runat="server" Text="WNBA" />
									</td>
								</tr>
								<tr valign="top">
									<td class="tdInput">
										<asp:CheckBox ID="chkMLS" runat="server" Text="MLS" />
									</td>
									<td class="tdInput">
										<asp:CheckBox ID="chkPGA" runat="server" Text="PGA" />
									</td>
									<td class="tdInput">
										<asp:CheckBox ID="chkLPGA" runat="server" Text="LPGA" />
									</td>
								</tr>
								<tr valign="top">
									<td class="tdInput">
										<asp:CheckBox ID="chkUFC" runat="server" Text="UFC" />
									</td>
									<td class="tdInput">
										<asp:CheckBox ID="chkExtreme" runat="server" Text="Extreme Sports" />
									</td>
									<td class="tdInput">
										<asp:CheckBox ID="chkLacrosse" runat="server" Text="Pro Lacrosse" />
									</td>
								</tr>
								<tr valign="top">
									<td class="tdInput">
										<asp:CheckBox ID="chkOutdoor" runat="server" Text="Outdoor Sports" />
									</td>
									<td class="tdInput">
										<asp:CheckBox ID="chkOlympics" runat="server" Text="Olympics" />
									</td>
									<td class="tdInput">
										<asp:CheckBox ID="chkNASCAR" runat="server" Text="NASCAR" />
									</td>
								</tr>
								<tr valign="top">
									<td class="tdInput">
										<asp:CheckBox ID="chkPBA" runat="server" Text="PBA" />
									</td>
									<td class="tdInput">
										<asp:CheckBox ID="chkCFL" runat="server" Text="CFL" />
									</td>
									<td class="tdInput">

									</td>
								</tr>
								<tr valign="top">
									<td class="tdInput">
										<asp:CheckBox ID="chkYouthSports" runat="server" Text="Youth Sports" />
									</td>
									<td class="tdInput">
										<asp:CheckBox ID="chkAmateurSports" runat="server" Text="Amateur Sports" />
									</td>
									<td class="tdInput">

									</td>
								</tr>
							</table>
						</td>
					</tr>
					<tr valign="top">
						<td class="tdInput" style="width: 700px">

						</td>
						<td class="tdInput">
							<asp:ImageButton ID="btnOK2" runat="server" ImageUrl="~/Images/Button/btnOK.png" OnClick="OnClickOK" />
							&nbsp;&nbsp;
							<asp:ImageButton ID="btnCancel" runat="server" ImageUrl="~/Images/Button/btnCancel.png" OnClick="OnClickCancel" />
						</td>
					</tr>
				</table>
			</td>
		</tr>
	</table>
</asp:Content>
