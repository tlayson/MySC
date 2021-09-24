<%@ Page language="c#" %>
<html runat="server" Visible="false">
	<head runat="server">
	</head>
</html>
<script runat=server>


override protected void OnInit(EventArgs args)
{
	ArrayList list=new ArrayList();
	list.Add("ChatUI.xml");
	if(Request.QueryString["Type"]=="InstantMain")
	{
		list.Add("ChatUI_IM.xml");
	}
	
	if(Request.QueryString["Hash"]==null)
	{
		string url=Request.RawUrl;
		if(url.IndexOf("?")==-1)
			url+="?";
		else
			url+="&";
		int hash=list.Count;
		foreach(string file in list)
		{
			hash+=file.GetHashCode();
			hash+=System.IO.File.GetLastWriteTime(Server.MapPath(file)).GetHashCode();
		}
		url+="Hash="+hash;
		Response.Redirect(url,true);
		return;
	}

	Response.ContentType="text/xml";
	Response.Cache.SetCacheability(HttpCacheability.Public);
	Response.Cache.SetExpires(DateTime.Now.AddYears(10));
	
	CuteChat.ChatWebUtility.InstallGzipForResponse();
	
	Response.Write("<Library>");

	foreach(string file in list)
	{
		string filepath=Server.MapPath(file);
		System.Xml.XmlDocument doc=new System.Xml.XmlDocument();
		doc.Load(Server.MapPath(file));
		Response.Write(doc.DocumentElement.InnerXml);
	}
	
	Response.Write("</Library>");
	
	Response.End();
}

</script>