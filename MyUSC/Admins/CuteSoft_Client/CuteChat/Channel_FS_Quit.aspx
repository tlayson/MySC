<%@ Page Language="c#" Inherits="CuteChat.ChatFramePage" AutoEventWireup="false" %>
<%@ Import Namespace="System.Data" %>
<%@ Import Namespace="CuteChat" %>
<script runat=server>
override protected void OnLoad(EventArgs args)
{
	Disconnect();
	
	Response.Redirect("~/");
}
</script>