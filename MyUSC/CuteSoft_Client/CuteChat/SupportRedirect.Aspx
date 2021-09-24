<%@ Page Language="C#" %>
<html>
	<head>
		<TITLE>Live Support</TITLE>
	</head>
	<script runat="server">
		override protected void OnInit(EventArgs args)
		{
			base.OnInit(args);
			
			//if( CuteChat.ChatWebUtility.HasOnlineOperators() )
			if( CuteChat.ChatWebUtility.HasReadyAgents() )
			{
				Response.Redirect("SupportCustomer.aspx"+Request.Url.Query);
			}
			else
			{
				Response.Redirect("SupportFeedback.aspx"+Request.Url.Query);
			}
		}
	</script>
</html>
