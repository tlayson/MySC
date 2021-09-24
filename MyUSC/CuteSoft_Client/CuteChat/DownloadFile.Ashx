<%@ WebHandler Language="C#" Class="DrawAvatar" %>

using System;
using System.Web;
using System.Web.UI;
using CuteChat;

public class DrawAvatar : IHttpHandler
{
	public void ProcessRequest(HttpContext Context)
	{
		string date=Context.Request.QueryString["date"];
		string guid=Context.Request.QueryString["guid"];
		string filename=Context.Request.QueryString["filename"];
		//verify the parameters
		DateTime.ParseExact(date,"yyyy-MM-dd",null);
		Guid theguid=new Guid(guid);

		string phypath;

		string folder = System.Configuration.ConfigurationSettings.AppSettings["CuteChat.SendFile.Directory"];
		if (folder == null || folder.Length == 0)
			folder = "~/CuteChatSendFiles";
		string virpath = folder + "/" + date + "/" + guid + ".licx";

		if (virpath.StartsWith("~/") || virpath.StartsWith("/"))
		{
			virpath = ChatWebUtility.ProcessWebPath(Context, null, virpath);
			phypath = Context.Server.MapPath(virpath);
		}
		else
		{
			phypath = virpath.Replace('/', '\\');
		}

		Context.Response.ContentType=Context.Request.QueryString["mime"];
		Context.Response.AddHeader("Content-Disposition", "inline;filename=" + filename);
		Context.Response.WriteFile(phypath);
	}
	public bool IsReusable
	{
		get
		{
			return false;
		}
	}
}
