<%@ Page language="c#" %>
<%@ Import Namespace="CuteChat" %>
<%@ Import Namespace="System.Xml" %>
<%@ Import Namespace="System.IO" %>
<head runat=server><title>Errors</title></head>
<%

string vpath="~/Errors";
string dir=Server.MapPath(vpath);
if(Directory.Exists(dir))
{
	foreach(string file in Directory.GetFiles(dir,"*.txt"))
	{
		string name=Path.GetFileName(file);
		string href=Response.ApplyAppPathModifier("~/Errors/"+name);
%>
	<div><a href='<%=href%>'><%=name%></a></div>
<%
	}
}


%>

