<%@ Page Language="c#" %>
<%@ Register TagPrefix="uc1" TagName="TopAds" Src="Advertising/TopAds.ascx" %>
<%@ Import Namespace="CuteChat" %>
<script runat="server">
		string Place;
		string UrlBase;
		override protected void OnInit(EventArgs args)
		{
			Place=Request.QueryString["Place"];
			UrlBase=ResolveUrl("./");
			base.OnInit(args);
		}
</script>
<html>
	<HEAD runat=server>
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/nav4-0">
		<meta HTTP-EQUIV="Content-Type" CONTENT="text/html;">
		<title>Cute Chat</title>
	</head>
	<body>
		<div>
			<table id="toptableid" width="100%" border="0" cellspacing="0" cellpadding="0">
				<tr>
					<td width="200">
						<img title="CuteChat" src="<%=ChatWebUtility.LogoUrl %>">
					</td>
					<td align="center">
						<uc1:TopAds id="TopAds1" runat="server"></uc1:TopAds>
					</td>
					<td width="200">
					</td>
				</tr>
			</table>
		</div>
		<div>
			<object classid="clsid:d27cdb6e-ae6d-11cf-96b8-444553540000" codebase="http://fpdownload.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=9,0,0,0"
				width="720" height="400" id="FlashClient" align="middle" VIEWASTEXT>
				<param name="movie" value="FlashClient.swf<%=Request.Url.Query%>" />
				<param name="quality" value="high" />
				<embed src="FlashClient.swf<%=Request.Url.Query%>" quality="high" width="720" height="400"
				name="FlashClient" align="middle" type="application/x-shockwave-flash"
				pluginspage="http://www.macromedia.com/go/getflashplayer" />
			</object>
		</div>
		<div>
		Can’t view flash? <a href="http://www.macromedia.com/go/getflashplayer" target="_blank">
		
		Download it</a> or <a href="Channel_FS.aspx<%=Request.Url.Query%>">Use HTML mode</a>
		</div>
	</body>
</html>
