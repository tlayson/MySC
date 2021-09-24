<%@ Page language="c#" %>
<%@ Import Namespace="System.Web.Mail" %>
<%@ Import Namespace="CuteChat" %>
<script runat="server">

string customerid;
AppChatIdentity identity;
string name;
bool requested=false;
string referrer;
string newurl;
string status;
string detail;

override protected void OnInit(EventArgs e)
{
	base.OnInit(e);

	name=Context.Request.QueryString["input_name"];
	if(name!=null)
	{
		requested=true;
	}

	referrer=Request.QueryString["input_referrer"];
	if(referrer==null)
	{
		referrer=Server.HtmlEncode(Request.QueryString["Referrer"]);
	}
	string url=Context.Request.QueryString["Url"];
	if(url==null||url=="")
	{
		Uri uri=Context.Request.UrlReferrer;
		if(uri!=null)
		{
			url=uri.AbsoluteUri;
		}
		else
		{
			url="";
		}
	}
	
	
	customerid=ChatWebUtility.InitUniqueId();
	identity=ChatWebUtility.GetLogonIdentity();

	if(name==null&&identity!=null)
	{
		name=identity.DisplayName;
	}
	

	if(!requested)return;
	
	
	string email=Context.Request.QueryString["input_email"];
	string department=Context.Request.QueryString["input_department"];
	string question=Context.Request.QueryString["input_question"];
	string customdata=Context.Request.QueryString["input_customdata"];

	string ipaddr=Context.Request.UserHostAddress;
	string browser=ChatWebUtility.GetBrowser();
	string platform=ChatWebUtility.GetPlatform();
			
	string culture="";
	try
	{
		culture=Request.UserLanguages[0];
	}
	catch
	{
	}
	if(culture==null)
	{
		culture="";
	}

	if(identity==null)
	{
		identity=new AppChatIdentity(name,true,customerid,Context.Request.UserHostAddress);
	}


	ChatPortal portal=ChatSystem.Instance.GetCurrentPortal();
	lock(portal)
	{
		SupportAgentChannel sac=(SupportAgentChannel)portal.GetPlace(ChatTempConst.SupportAgentPlaceName);

		status=sac.HandleCustomerWait(identity,name,ipaddr,culture,platform,browser,url,referrer,email,department,question,customdata,out detail);
	}
	
	if(status=="READY")
	{
		//Response.Redirect("SupportClient.aspx?PlaceName="+detail,true);
		Response.Redirect("SupportClient.aspx",true);
	}
	else if(status=="WAITING")
	{
		
	}

	newurl=ChatWebUtility.ReplaceParam(Request.RawUrl,"_time",DateTime.Now.Ticks.ToString());
	
	if(requested)
	{
		Response.AppendHeader("Refresh", "3; URL="+newurl);
	}
}

</script>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
	<HEAD runat="server">
		<title>Live Support</title>
        <link rel="icon" href="Icons/Support.ico" type="image/x-icon" />
		<link rel="shortcut icon" href="Icons/Support.ico" type="image/x-icon" />
		<style type="text/css">
			body
			{
				background-color: #edf1fa;	
			}
			body, td
			{
				font: 12px Arial, Helvetica, sans-serif;
			}
			
		</style>
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
	</head>
	<body bottomMargin="0" topMargin="0" marginwidth="0" marginheight="0">
		<form action='<%=Request.Url%>' method=get>
			<img src="<%=ChatWebUtility.SupportWaitImage%>" border="0" />
			<table cellspacing="0" cellpadding="2" width="98%" align="center" border="0">
				<tr>
					<td>
						<div style="width:100%;border:0px solid #ffffff;background-color:#ffffff;padding:15px;height:320px;">
							<p>Thank you for contacting us. In order to serve you better please provide the 
								following information:</p>
							<table border="0" cellspacing="0" cellpadding="5" width="100%">
								<tr>
									<td width="100">
										<B>Your Name:</B>
									</td>
									<td>
										<input id="input_name" name="input_name" <%=requested?"disabled":""%> value='<%=name%>' style="width:180px;">
									</td>
								</tr>
								<tr>
									<td>
										<B>E-Mail:</B>
									</td>
									<td>
										<input id="input_email" name="input_email" <%=requested?"disabled":""%>  value='<%=Request.QueryString["input_email"]%>' style="width:180px;">
									</td>
								</tr>
								<tr>
									<td>
										<B>Question:</B>
									</td>
									<td>
										<textarea id="input_question" name="input_question" <%=requested?"disabled":""%>  rows="3" style="width:180px;"><%=Request.QueryString["input_question"]%></textarea>
									</td>
								</tr>
								<tr>
									<td>
										<B>Department:</B>
									</td>
									<td>
										<!-- TODO: add your departments here -->
										<select id="input_department" name="input_department" <%=requested?"disabled":""%>  style="width:180px;">
											<option value="Support">Support</option>
											<option value="Sales">Sales</option>
										</select>
									</td>
								</tr>
								<tr>
									<td colspan="2" align="center">
										<br>
									
										<%if(requested){%>

										<div id="span_waiting">
										<%if(status=="NOAGENT"){%>
										There's no operator online now.
										<%}else{%>
										Please wait for a live consultant to respond. You are number <span id="span_pos"><%=detail%></span> in the queue. Thank you for waiting.
										<%}%>
										</div>
										
							
										<%}else{%>
										
										<input type='hidden' id="input_referrer" name="input_referrer" value='<%=referrer%>'>
										<input type='submit' id="button_submit" value='Request Chat..'> &nbsp;
										<%}%>
									</td>
								</tr>
								<tr>
									<td colspan="2" align="center">
									
									
									</td>
								</tr>
							</table>
							<br />
							<br />
						</div>
					</td>
					<td>&nbsp;</td>
					<td valign="top">
						<img alt="Live Support" src="images/live-support-woman.jpg">
						<div style="display:none">
							<a href="SupportFeedback.aspx">Post Feedback </a>
						</div>
					</td>
				</tr>
			</table>
		</form>
	</body>
</html>
