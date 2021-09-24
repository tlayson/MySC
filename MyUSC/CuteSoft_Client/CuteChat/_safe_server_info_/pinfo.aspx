<%@ Page language="c#" %>
<%@ Import Namespace="CuteChat" %>
<%@ Import Namespace="System.Xml" %>
<%@ Import Namespace="System.IO" %>
<%



System.Diagnostics.Process p=System.Diagnostics.Process.GetCurrentProcess();

string processtime=p.StartTime.ToString("HH:mm:ss");

int count=0;
object obj=Application["mycount"];
if(obj!=null)count=(int)obj;
count++;
Application["mycount"]=count;

TimeSpan span=DateTime.Now-p.StartTime;


double cpu1=100*p.TotalProcessorTime.TotalSeconds/span.TotalSeconds;
double cpu2=100*p.UserProcessorTime.TotalSeconds/span.TotalSeconds;

string title=count+"|"+span.TotalSeconds.ToString("###,###")+"|"+processtime

+"|"+cpu1.ToString("00.00")+"%"

+"|"+cpu2.ToString("00.00")+"%"
;



%>



<title><%=title%></title>

<div style="padding:100px;font-size:50px;">
<%=title%>
</div>

<pre>
<%
StringWriter stringw=new StringWriter();
XmlTextWriter writer=new XmlTextWriter(stringw);

writer.WriteStartElement("system");

ChatSystem.Instance.Dump(writer);

writer.WriteEndElement();
writer.Flush();
%>
<%=Server.HtmlEncode(stringw.ToString())%>

</pre>



















<hr/>
<script>

function toLongTimeString(d)
{
	return d.getHours()+":"+d.getMinutes()+":"+d.getSeconds()+":"+d.getMilliseconds()+":"+Math.random();
}

var url=location.href;

if(url.indexOf('?')!=-1)
	url=url.substring(0,url.indexOf('?'));
url=url+"?_time="+toLongTimeString(new Date());

var xh=null;
var xhtime=null;
function Reload()
{
	var time=new Date().getTime();
	if(xh!=null)
	{
		if(time-xhtime<5000)return;
		try
		{
			xh.abort();
		}
		catch(x)
		{
		}
		document.title=document.title+"*";
	}
	xhtime=time;
	xh=new XMLHttpRequest();
	xh.open("GET",url,true);
	xh.onreadystatechange=OnChange;
	xh.send("");
}
function OnChange()
{
	if(xh.readyState<4)return;
	if(xh.status==200)
	{
		var text=xh.responseText;
		document.open(url,"replace")
		document.write(text);
		document.close();
	}
	else
	{
		document.title=document.title+"%";
	}
	xh=null;
}

setInterval(Reload,1000);

document.write("<a href='"+url+"'>"+url+"</a>");

</script>

<head runat=server></head>