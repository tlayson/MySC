<%@ Page Language="c#" Inherits="CuteChat.ChatPageBase" %>
<%@ Register TagPrefix="uc1" TagName="MessengerAds" Src="Advertising/MessengerAds.ascx" %>
<script runat=server>
override protected void OnLoad(EventArgs args)
{
	base.OnLoad(args);
	CuteChat.ChatWebUtility.InstallGzipForResponse();
}
</script>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
	<head runat="server" id="aspnetHead">
		<title>Messenger</title>
		<base target="_blank" />
        <link rel="icon" href="Icons/Messenger.ico" type="image/x-icon" />
		<link rel="shortcut icon" href="Icons/Messenger.ico" type="image/x-icon" />
		<link rel="stylesheet" href="IM_style.css" />
	</head>
	<body style="border:none 0px;">
		<div id="loadingtext">
			[[UI_LOADING]]....
		</div>
		<div style="display:none">
			<div id="MessengerAds">
				<uc1:MessengerAds id="MessengerAds" runat="server"></uc1:MessengerAds>
			</div>
		</div>
	</body>
	<script>
	var MessengerAds=document.getElementById("MessengerAds");

	function GetStackTrace()
	{
		return "";
	}
	window.onerror=function window_error(a,b,c)
	{
		alert(b+","+c+"\r\n"+a+"\r\n"+GetStackTrace());
	}
	</script>
	<script src="LoadSettings.aspx"></script>
	<script src="LoadScripts.aspx"></script>
	<script>
	
	function ReloadUI()
	{
		Desktop.SuspendLayout();
				
		LoadSkinClasses(SkinName,"ChatUI.Xml.aspx?Type=InstantMain");

		Desktop.AppendWindow( CreateInstance("InstantLoginForm") );

		Desktop.ResumeLayout();
	}

	window.onload=function()
	{
		document.getElementById("loadingtext").style.display='none';
		
		document.body.style.overflow="hidden";
	
		HtmlInitialize();
		
		_SL_ParseXml(LoadChatClasses("ChatUI.Xml.aspx?Type=InstantMain"),Desktop);
	
		ReloadUI();
		
		Connect("Messenger");
	}

	window.onbeforeunload=function()
	{
		if(IsConnected())
		{
			return "[[UI_AreYouSureQuitMessenger]]";
		}
	}


	function window_onunload()
	{
		Disconnect(true);
	}
	window.onunload=window_onunload;
	if(window.attachEvent)
	{
		window.attachEvent("onunload",window_onunload);
	}
	else if(window.addEventListener)
	{
		window.addEventListener("unload",window_onunload,true);
	}
	
	AttachChatEvent("IMCOOKIE",MainFormHandleIMCOOKIE);
	function MainFormHandleIMCOOKIE(name,type)
	{
		if(type=="DOUPDATE")
		{
			ChatUI_IMDeclareMainForm();
		}
	}


	</script>
</html>
