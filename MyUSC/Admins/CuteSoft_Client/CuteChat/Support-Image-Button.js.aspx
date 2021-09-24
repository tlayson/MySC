<%@ Page Language="c#" ContentType="text/javascript" %>
//<html runat=server Visible=false ID="Html1"><head runat=server ID="Head1"></head></html>
<script runat=server>
string baseurl;
</script>
<%
baseurl=Request.Url.ToString();
baseurl=baseurl.Substring(0,baseurl.LastIndexOf("/")+1);
	// URL of the online image
	string URL_live_help_online_image = baseurl+"images/141x44-online.gif";
	
	// URL of the offline image
    string URL_live_help_offline_image = baseurl+ "images/141x44-offline.gif";
%>
//<script>

// Default window ornaments for the live help dialog
var _liveHelpDialogFeature = "status=1,width=500,height=400,resizable=1";



function WriteLiveSupportButton()
{


	var ImageURL='<%= CuteChat.ChatWebUtility.HasReadyAgents()/*HasOnlineAgents()*/?URL_live_help_online_image:URL_live_help_offline_image %>';

	ImageURL= "<img title=\"support chat\" src=\""+ImageURL+"\" border=0>";

	// write the live support button to the page
	document.write('<a href=\"###\" onclick=\"OpenLiveSupport()\">'+ImageURL+'</a>');

}

WriteLiveSupportButton();

function OpenLiveSupport()
{
	var encode=escape
	if(typeof(encodeURIComponent)!="undefined")
		encode=encodeURIComponent;
		
	var url="<%=baseurl%>"+"SupportRedirect.aspx?Referrer="+encode(document.referrer)+"&Url="+encode(location.href)+"&_time="+(new Date().getTime());
	var win;
	try
	{
		win=window.open(url,'',_liveHelpDialogFeature);
	}
	catch(x)
	{
	}
	
	if(win==null)
	{
		alert("Pop-up Blocker Detected.");
	}
}


