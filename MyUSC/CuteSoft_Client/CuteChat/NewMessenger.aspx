<%@ Page Language="C#" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.1//EN" "http://www.w3.org/TR/xhtml11/DTD/xhtml11.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<html>
	<head>
		<title>Messenger</title>
		<link rel="stylesheet" href="NewMessenger.css" />		
	</head>
	<body>
		
	</body>
	<script type="text/javascript" src="Script/CuteWebUI.js"></script>
	<script src="IntegrationUtility.js.aspx"></script>
	<script type="text/javascript" src="LoadSettings.aspx"></script>
	<script type="text/javascript" src="LoadScripts.aspx"></script>
	<script type="text/javascript" src="Script/NewMessenger.js"></script>
	<script>
		window.onload=CuteChatMessenger.Start;
	</script>
</html>