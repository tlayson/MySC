<%@ Page Language="C#" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
	<HEAD runat="server">
		<TITLE>Safe Server Infomation</TITLE>
	</head>
	<BODY>
		This file list some runtime information on the server .
		<hr>
		<table width="100%" border="1" cellpadding="4" cellspacing="0" style="border-collapse:collapse;">
			<tr>
				<td style="background-color:steelblue;color:white">Name</td>
				<td style="background-color:steelblue;color:white">Value</td>
			</tr>
			<tr>
				<td>GC.GetTotalMemory(true)</td>
				<td><%=GC.GetTotalMemory(true).ToString("###,###")%></td>
			</tr>
			<tr>
				<td>DateTime.Now</td>
				<%
				int timezone=TimeZone.CurrentTimeZone.GetUtcOffset(DateTime.Now).Hours;
				%>
				<td><%=DateTime.Now%>
					(
					<%=(timezone>=0?"+":"-")+timezone%>
					)
				</td>
			</tr>
			<tr>
				<td>DN Version</td>
				<td><%=Environment.Version%></td>
			</tr>
			<tr>
				<td>OS Version</td>
				<td><%=Environment.OSVersion%></td>
			</tr>
			<tr>
				<td>CurrentCulture</td>
				<td><%=System.Globalization.CultureInfo.CurrentCulture%></td>
			</tr>
			<tr>
				<td>CurrentUICulture</td>
				<td><%=System.Globalization.CultureInfo.CurrentUICulture%></td>
			</tr>
			<tr>
				<td>ASP.NET - HTTP_HOST</td>
				<td><%=Request.ServerVariables["HTTP_HOST"]%></td>
			</tr>
			<tr>
				<td>ASP.NET - SERVER_NAME</td>
				<td><%=Request.ServerVariables["SERVER_NAME"]%></td>
			</tr>
			<tr>
				<td>ASP.NET - SERVER_SOFTWARE</td>
				<td><%=Request.ServerVariables["SERVER_SOFTWARE"]%></td>
			</tr>
			<tr>
				<td>ASP.NET - App Virtual Path</td>
				<td><%=System.Web.HttpRuntime.AppDomainAppVirtualPath%></td>
			</tr>
		</table>
	</BODY>
</html>
