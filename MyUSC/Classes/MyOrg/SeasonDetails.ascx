<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SeasonDetails.ascx.cs" Inherits="MyUSC.Classes.MyOrg.SeasonDetails" %>
<%@ Register Assembly="MyUSC" Namespace="MyUSC.Classes" TagPrefix="MSC" %>
<%@ Register Assembly="MyUSC" Namespace="MyUSC.Classes.MyOrg" TagPrefix="MSC" %>
<%@ Register Src="~/Classes/DateSelect.ascx" TagPrefix="uc1" TagName="DateSelect" %>

<link href="/Styles/Site.css" rel="stylesheet" type="text/css" />
<table style="width: 550px" class="tblDlgControlOuter">
	<tr class="trDlgControlTitle">
		<td class="tdDlgControlNormal" colspan="2">
			<asp:Label ID="lblTitle" runat="server" Text="Season Details"></asp:Label>
		</td>
	</tr>
	<tr class="trDlgControlNormal">
		<td class="tdDlgControlNormal" colspan="2">
			<table class="tblDlgControlInner">
				<tr class="trDlgControlNormal">
					<td class="tdDlgControlNormal">
						Name</td>
					<td class="tdDlgControlNormal">
						<asp:TextBox ID="txtSeasonName" runat="server" MaxLength="250" Width="400px"></asp:TextBox>
					</td>
				</tr>
				<tr class="trDlgControlNormal">
					<td class="tdDlgControlNormal">
						Start Date</td>
					<td class="tdDlgControlNormal">
						<uc1:DateSelect runat="server" ID="dsStartDate" />
					</td>
				</tr>
				<tr class="trDlgControlNormal">
					<td class="tdDlgControlNormal">
						Comments</td>
					<td class="tdDlgControlNormal">
						<asp:TextBox ID="txtComments" runat="server" MaxLength="200" Width="400px"></asp:TextBox>
					</td>
				</tr>
				<tr class="trDlgControlNormal">
					<td class="tdDlgControlNormal">
						Options</td>
					<td class="tdDlgControlNormal">
						<asp:CheckBox ID="chkDefault" runat="server" Text="Make Default" />
					</td>
				</tr>
				<tr class="trDlgControlNormal">
					<td class="tdDlgControlNormal">

					</td>
					<td class="tdDlgControlNormal">
						<asp:CheckBox ID="chkShare" runat="server" Text="Share with Affiliates" />
					</td>
				</tr>
				<tr class="trDlgControlNormal">
					<td class="tdDlgControlNormal">
						<asp:HiddenField ID="hfSeasonID" runat="server" />
					</td>
					<td class="tdDlgControlNormal">
					</td>
				</tr>
			</table>
		</td>
	</tr>
	<tr class="trDlgControlNormal">
		<td class="tdRight">
			<asp:Button ID="btnOK" runat="server" Text="OK" OnClick="OnClickOK" CssClass="btnOK" />
		</td>
		<td class="tdLeft">
			<asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="OnClickCancel" CssClass="btnCancel" />
		</td>
	</tr>
</table>