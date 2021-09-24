<%@ Page Title="" Language="C#" MasterPageFile="~/USC.Master" AutoEventWireup="true" CodeBehind="MyAccountName.aspx.cs" Inherits="MyUSC.MyAccount.MyAccountName" %>
<%@ MasterType VirtualPath="~/USC.Master" %>
<%@ Register Src="~/Classes/DateSelect.ascx" TagPrefix="uc1" TagName="DateSelect" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
	<script type="text/javascript" src="~/js/GA.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
	<table style="width: 1060px; height: 650px; background-image: url('Images/Background.png')">
		<tr valign="top">
			<td class="tdInput">
				<table width="100%">
					<tr valign="top">
						<td class="tdInput" colspan="3">
							<asp:Label ID="lblInstructions" runat="server" Text="Instructions go here ..." CssClass="lgSiteColorTxt"></asp:Label>
						</td>
					</tr>
					<tr valign="top">
						<td class="tdInput">
							<asp:Label ID="lblUsername" runat="server" Text="UserName : "></asp:Label>
							<asp:Label ID="lblUsernameValue" runat="server" Text="username"></asp:Label>
						</td>
						<td class="tdInput">

						</td>
						<td class="tdInput" align="right">
							<asp:ImageButton ID="btnChangePswd" ImageUrl="~/Images/Button/btnChangePswd.png" runat="server" OnClick="OnClickChangePswd"/>
						</td>
					</tr>
					<tr valign="top">
						<td class="tdInput">
							<asp:Label ID="lblTitle" runat="server" Text="Title : "></asp:Label>
							<asp:TextBox ID="txtTitle" runat="server" MaxLength="50"></asp:TextBox>
						</td>
						<td class="tdInput">
							<asp:Label ID="lblSuffix" runat="server" Text="Suffix : "></asp:Label>
							<asp:TextBox ID="txtSuffix" runat="server" MaxLength="50"></asp:TextBox>
						</td>
						<td class="tdInput" align="right">
							<asp:ImageButton ID="btnOK2" runat="server" ImageUrl="~/Images/Button/btnOK.png" OnClick="OnClickSave" />
						</td>
					</tr>
					<tr valign="top">
						<td class="tdInput">
							<asp:Label ID="lblFirst" runat="server" Text="First : "></asp:Label>
							<asp:TextBox ID="txtFirst" runat="server" MaxLength="50" Width="250px"></asp:TextBox>
						</td>
						<td class="tdInput">
							<asp:Label ID="lblMI" runat="server" Text="MI : "></asp:Label>
							<asp:TextBox ID="txtMI" runat="server" Width="26px" MaxLength="1"></asp:TextBox>
						</td>
						<td class="tdInput">
							<asp:Label ID="lblLast" runat="server" Text="Last : "></asp:Label>
							<asp:TextBox ID="txtLast" runat="server" Width="250px" MaxLength="50"></asp:TextBox>
						</td>
					</tr>
					<tr valign="top">
						<td class="tdInput">
							<asp:Label ID="lblNickname" runat="server" Text="Nickname : "></asp:Label>
							<asp:TextBox ID="txtNickname" runat="server" MaxLength="50" Width="250px"></asp:TextBox>
						</td>
						<td class="tdInput" colspan="2">
							<asp:CheckBox ID="chkIncludeNickname" runat="server" Text="Include nickname in display name" />
						</td>
					</tr>
					<tr valign="top">
						<td class="tdInput" colspan="3">
							<asp:Label ID="lblEmail" runat="server" Text="Email Address : "></asp:Label>
							<asp:TextBox ID="txtEmail" runat="server" MaxLength="50" Width="250px"></asp:TextBox>
						</td>
					</tr>
					<tr valign="top">
						<td class="tdInput" colspan="3">
							<asp:Label ID="lblBirthDate" runat="server" Text="Birth Date : "></asp:Label>
							<asp:TextBox ID="txtBirthDate" runat="server" MaxLength="50" Width="250px"></asp:TextBox>
							<uc1:DateSelect runat="server" id="dsBirthDay" />
						</td>
					</tr>
					<tr valign="top">
						<td class="tdInput" colspan="3">
							<asp:Label ID="lblSecurityQuestion" runat="server" Text="Security Question : "></asp:Label>
						</td>
					</tr>
					<tr valign="top">
						<td class="tdInput">
							<asp:Label ID="lblChoose" runat="server" Text="Choose one : "></asp:Label>
							<asp:DropDownList ID="ddlChooseSQ" runat="server"></asp:DropDownList>
						</td>
						<td class="tdInput" colspan="2">
							<asp:CheckBox ID="chkProvide" runat="server" Text="Provide your own : " />
							<asp:TextBox ID="txtSecQuest" runat="server" Width="372px" MaxLength="50"></asp:TextBox>
						</td>
					</tr>
					<tr valign="top">
						<td class="tdInput" colspan="2">
							<asp:Label ID="lblSecAnswer" runat="server" Text="Security Answer : "></asp:Label>
							<asp:TextBox ID="txtSecAnswer" runat="server" MaxLength="50" Width="250px"></asp:TextBox>
						</td>
						<td class="tdInput">
							<asp:Label ID="lblCaseSensitive" runat="server" Text="* Answer is case sensitive."></asp:Label>
						</td>
					</tr>
					<tr>
						<td>

						</td>
						<td>

						</td>
						<td class="tdInput">
							<asp:ImageButton ID="btnOK" runat="server" ImageUrl="~/Images/Button/btnOK.png" OnClick="OnClickSave" />
							&nbsp;&nbsp;
							<asp:ImageButton ID="btnCancel" runat="server" ImageUrl="~/Images/Button/btnCancel.png" OnClick="OnClickCancel" />
						</td>
					</tr>
				</table>
			</td>
		</tr>
	</table>
</asp:Content>
