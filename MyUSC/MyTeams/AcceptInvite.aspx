<%@ Page Title="" Language="C#" MasterPageFile="~/USC.Master" AutoEventWireup="true" CodeBehind="AcceptInvite.aspx.cs" Inherits="MyUSC.MyTeams.AcceptInvite" %>
<%@ MasterType VirtualPath="~/USC.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
	<script type="text/javascript" src="~/js/GA.js"></script>
<script type="text/javascript">
	function ClientValidate(source, arguments)
	{
		var rbA = document.getElementById("rbAccept");
		var rbD = document.getElementById("rbDecline");

		// Only process one of the validation calls.
		if (source.id == "cvAccept")
		{
			var rbA = document.getElementById("rbAccept");
			var rbD = document.getElementById("rbDecline");
			if( rbA.checked && rbD.checked )
			{
				rbA.checked = false;
				rbD.checked = true;
			}
			else
			{
				rbA.checked = true;
				rbD.checked = false;
			}
		}

		arguments.IsValid = true;
	}
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
	<table style="width: 800px; vertical-align: top;">
		<tr style="height: 45px">
			<td class="tdInput" colspan="2">
				<asp:Label ID="lblTitle" runat="server" Text="Accept your invitation" CssClass="lgSiteColorTxt"></asp:Label>
			</td>
		</tr>
		<tr style="height: 45px">
			<td class="tdInput" colspan="2">
				<asp:Literal ID="litGreeting" runat="server" Text="Greeting"></asp:Literal>
			</td>
		</tr>
		<tr style="height: 45px">
			<td class="tdInput" colspan="2">
				<asp:RadioButton ID="rbAccept" runat="server" Text="Accept the invitation." Checked="True" CssClass="medNormalTxt" OnCheckedChanged="OnChangedAccept" AutoPostBack="True" ClientIDMode="Static" />
			</td>
		</tr>
		<tr style="height: 45px">
			<td class="tdInput" colspan="2">
				<asp:RadioButton ID="rbDecline" runat="server" Text="Decline the invitation." CssClass="medNormalTxt" OnCheckedChanged="OnChangedDecline" ClientIDMode="Static" AutoPostBack="True" />
			</td>
		</tr>
		<tr style="height: 45px">
			<td class="tdRight">
				<asp:ImageButton ID="btnOK" runat="server" ImageUrl="~/Images/Button/btnOK.png" OnClick="OnClickOK" />
			</td>
			<td class =" tdLeft">
				<asp:ImageButton ID="btnCancel" runat="server" ImageUrl="~/Images/Button/btnCancel.png" OnClick="OnClickCancel" />
			</td>
		</tr>
	</table>
</asp:Content>
