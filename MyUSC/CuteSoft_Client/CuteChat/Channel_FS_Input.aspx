<%@ Page Language="c#" Inherits="CuteChat.ChatFramePage" AutoEventWireup="false" %>
<%@ Register TagPrefix="tAds" TagName="TopAds" Src="Advertising/TopAds.ascx" %>
<%@ Import Namespace="System.Data" %>
<%@ Import Namespace="CuteChat" %>
<script runat="server">
override protected void OnLoad(EventArgs e)
{
	base.OnLoad(e);
}
override protected void OnPreRender(EventArgs e)
{
	base.OnPreRender(e);
	
		string text=TextBox1.Text.Trim();
	TextBox1.Text="";
	if(text.Length==0)return;
	SendMessage(text);	
}
void Button1_Click(object sender,EventArgs args)
{

}
</script>
<html>
	<HEAD runat="server">
		<title>Top</title>
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/nav4-0">
		<meta HTTP-EQUIV="Content-Type" CONTENT="text/html;">
	</head>
	<link rel="stylesheet" href="style.css" />
	<link rel="stylesheet" href='Skins/<%=ChatWebUtility.SkinName%>/style.css' />
	<BODY style="margin: 0px;padding: 0px;">
		<div style="padding: 1px 10px 10px 10px">
			<form id="Form1" runat="server" method="post">
				<table width="100%" cellpadding="1" cellspacing="0" border="0">
					<tr>
						<td>
							<a target="_top" href="Channel_FS_Quit.aspx<%=UrlQuery%>"><img title='[[Signout]]' src="images/logoff.png" border="0"></a>
							<img title='[[Help]]' onclick="window.open('user-guide/');" src="images/help.png" border="0">
						</td>
						<td>
						</td>
					</tr>
					<tr>
						<td width="70%">
							<asp:TextBox ID="TextBox1" Runat="server" Columns="100" Width="100%"></asp:TextBox>
						</td>
						<td>
							<asp:Button Runat="server" OnClick="Button1_Click" Text="[[UI_SEND]]" Height="22" Width="80"></asp:Button>
						</td>
					</tr>
				</table>
			</form>
		</div>
	</BODY>
	<script language="javascript">
		document.forms['Form1'].TextBox1.focus();
		try
		{
			window.parent.messageframe.location.reload()
		}
		catch(x)
		{
		}
	</script>
</html>
