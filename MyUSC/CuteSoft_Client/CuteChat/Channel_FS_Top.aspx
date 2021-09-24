<%@ Page Language="c#" Inherits="CuteChat.ChatFramePage" AutoEventWireup="false" %>
<%@ Register TagPrefix="tAds" TagName="TopAds" Src="Advertising/TopAds.ascx" %>
<%@ Import Namespace="System.Data" %>
<%@ Import Namespace="CuteChat" %>
<html>
	<HEAD runat="server">
		<title>Top</title>
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/nav4-0">
		<meta HTTP-EQUIV="Content-Type" CONTENT="text/html;">
	</head>
	<link rel="stylesheet" href="style.css" />
	<link rel="stylesheet" href='Skins/<%=ChatWebUtility.SkinName%>/style.css' />
	<body style="margin:0px">
		<table width="100%">
			<tr>
				<td width="300">
					<img title="CuteChat" src="Images/logo.gif">
				</td>
				<td align="center">
					<tAds:TopAds id="ChatRoomAd1" runat="server"></tAds:TopAds>
				</td>
				<td width="240" align="center">
				</td>
			</tr>
		</table>
	</body>
</html>
