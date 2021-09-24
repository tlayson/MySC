<%@ Page Language="C#" Inherits="CuteChat.ChatPageBase" %>
<%@ Import Namespace="CuteChat" %>
<script runat="server">

override protected void OnLoad(EventArgs args)
{
	base.OnLoad(args);
	
	string userId=Request.QueryString["UserId"];
}	
	
</script>
<html>
	<HEAD runat=server>
		<title>[[UI_MENU_ViewProfile]]</title>
		<link rel="stylesheet" href="IM_Style.css">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
	</head>
	<BODY style="background-color: #ffffff;">
		<div class="dialogPageHeader">
				<table width="100%" background='images/up.gif' cellpadding="4" cellspacing="0" border="0">
					<tr>
						<td width=60 height=65 align=center valign=middle><img src='images/default.png'><td>
						<td valign=middle>
							<strong>[[ViewUserProfile_Title]]</strong>
							<br>
							[[Developer should modify this file and show the profile of the user.]]
						</td>
					</tr>
				</table>
			</div>
			<div Style="margin-bottom: 55px; height: 300px; padding: 20px 0 5px 10px;"> 
					<div align="left" style="padding:5px;">
						<TABLE id="Table1" cellSpacing="0" cellPadding="3" width="100%" align="center" border="0">
							<tr>
								<td><b>Internal User ID:</b>
								</td>
								<td>&nbsp;</td>
								<td>
									
								</td>
							</tr>
							<tr>
								<td><b>Integrated User Name:</b>
								</td>
								<td>&nbsp;</td>
								<td>
									
								</td>
							</tr>
							<tr>
								<td>
									<b>User Name:</b>
								</td>
								<td>&nbsp;</td>
								<td>
								</td>
							</tr>
						</TABLE>
					</div>
			</div>
			
			<div class="dialogPageButtons">
				<table width="100%" border=0 cellspacing=0 cellpadding=0 background='images/up.gif'>
					<tr height="55px">
						<td align=center valign=top class="dialogButtonRow">
							<button id="btncancel" style="width:72px">[[Close]]</button>
						</td>            
					</tr>
				</table>
			</div>
	</body>
	<script>
	btncancel.onclick=function()
	{
		top.returnValue=null;
		(top.cc_close||top.close)();
	}
	</script>
</html>
