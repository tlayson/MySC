<%@ Page Title="" Language="C#" MasterPageFile="~/USC.Master" AutoEventWireup="true" CodeBehind="PickUsername.aspx.cs" Inherits="MyUSC.MyTeams.PickUsername" %>
<%@ MasterType VirtualPath="~/USC.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
	<script type="text/javascript" src="~/js/GA.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
	<table class="trNormal" width="800">
		<tr class="trNormal">
			<td class="tdInput">
				<table class="trNormal" width="100%">
					<tr class="trNormal">
						<td class="tdInput">
							<span class="xlgSiteColorTxt">Welcome to MySportsConnect.net!</span>
							<br />  
							<span class="lgNormalTxt">Before we jump in, we need to pick a username for you and set your password.  
							Use the text field below to make sure the name you would like to use is available.  
							You can use the suggest button to have us create a possible username for you.  
							If it is, click on the Done button and we'll take you to the site.</span>
						</td>
					</tr>
				</table>
			</td>
		</tr>
		<tr class="trNormal">
			<td class="tdInput">
				<table class="trNormal" width="100%">
					<tr class="trNormal">
						<td class="tdInput">
							<MSC:TBSCLabel ID="TBSCLabel1" runat="server" UserValue="-1">Desired username :</MSC:TBSCLabel>
						</td>
						<td class="tdInput">
							<MSC:TBSCTextBox ID="txtUsername" runat="server" Width="250px"></MSC:TBSCTextBox>
						</td>
						<td class="tdInput">
							<MSC:TBSCButton ID='btnCheckName' runat="server" OnClick="OnClickCheckUsername" Text="Check Username" ToolTip="Check to see if the name is available" />
						</td>
					</tr>
					<tr class="trNormal">
						<td class="tdInput" colspan="2">
							<MSC:TBSCLabel ID="lblNameAvailable" runat="server" CssClass="medSuccessTxt" UserValue="-1" Visible="False">The username is available.</MSC:TBSCLabel>
							<MSC:TBSCLabel ID="lblNameTaken" runat="server" CssClass="medErrorTxt" UserValue="-1" Visible="False">The username is taken.  Please try another.</MSC:TBSCLabel>
						</td>
						<td class="tdInput">
							<MSC:TBSCButton ID='btnSuggest' runat="server" OnClick="OnClickSuggest" Text="Suggest" ToolTip="Suggest a username" />
						</td>
					</tr>
					<tr class="trNormal">
						<td class="tdInput">
							<MSC:TBSCLabel ID="TBSCLabel2" runat="server" UserValue="-1">Password :</MSC:TBSCLabel>
						</td>
						<td class="tdInput">
							<MSC:TBSCTextBox ID="txtPassword" runat="server" TextMode="Password" Width="250px"></MSC:TBSCTextBox>
						</td>
						<td class="tdInput">
							
						</td>
					</tr>
					<tr class="trNormal">
						<td class="tdInput">
							<MSC:TBSCLabel ID="TBSCLabel3" runat="server" UserValue="-1">Confirm Password :</MSC:TBSCLabel>
						</td>
						<td class="tdInput">
							<MSC:TBSCTextBox ID="txtConfirmPswd" runat="server" TextMode="Password" Width="250px"></MSC:TBSCTextBox>
						</td>
						<td class="tdInput">
							
						</td>
					</tr>
				</table>
			</td>
		</tr>
		<tr class="trNormal">
			<td class="tdInput">
				<table class="trNormal" width="100%">
					<tr class="trNormal">
						<td class="tdCenter">
							<MSC:TBSCButton ID='btnOK' runat="server" Text="Done" OnClick="OnClickDone" />
						</td>
					</tr>
				</table>
			</td>
		</tr>
	</table>
</asp:Content>
