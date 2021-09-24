<%@ WebHandler Language="C#" Class="DrawAvatar" %>

using System;
using System.Web;
using System.Web.UI;

public class DrawAvatar : IHttpHandler
{
	public void ProcessRequest(HttpContext context)
	{
		string avatar=context.Request.QueryString["Avatar"];
		string type=context.Request.QueryString["AvatarType"];
		
		if(type==null||type=="")
				type="Anonymous";
				
		if(avatar!=null&&avatar!="")
		{
			if(avatar.ToLower().StartsWith("everyone"))
			{
				context.Response.Redirect("Avatars/"+avatar);
				return;
			}
			if(avatar.ToLower().StartsWith(type.ToLower()))
			{
				context.Response.Redirect("Avatars/"+avatar);
				return;
			}
		}
		
		context.Response.Redirect("Avatars/"+type+"/default.gif");
	}
	public bool IsReusable
	{
		get
		{
			return false;
		}
	}
}
