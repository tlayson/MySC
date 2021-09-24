<%@ Page Language="C#" %>
<%@ Import Namespace="CuteChat" %>
<html>
	<head runat=server>
	</head>
</html>
<script runat=server>
protected override void OnInit(EventArgs args)
{
	Response.Redirect(ChatWebUtility.LogoutUrl);
}
</script>
