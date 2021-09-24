<%@ Page Language="c#" Inherits="CuteChat.ChatPageBase" %>
<%@ Import Namespace="CuteChat" %>
<%@ Import Namespace="System.Web.Mail" %>
<script runat="server">

string displayname;
override protected void OnInit(EventArgs e)
{
	base.OnInit(e);
	
	//TODO:configuration : goto support client and wait..
	if(true)
	{
		//now use another way..
		//Response.Redirect("SupportClient.aspx"+Request.Url.Query,true);
		
		Response.Cookies["CuteSupportQuery"].Value=Request.Url.Query;
		Response.ContentType="text/html";
		Response.Write("<script type='text/javascript'>location.href='SupportClient.aspx'</scr"+"ipt>");
		Response.End();
	}
	
	if(ChatWebUtility.IsDownLevelBrowser)
	{
		Response.Redirect("SupportCustomer2.aspx"+Request.Url.Query,true);
	}
	
	AppChatIdentity identity=ChatWebUtility.GetLogonIdentity();
	if(identity!=null)
		displayname=identity.DisplayName;
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
		<script>
		function GetStackTrace()
		{
			return "";
		}
		window.onerror=function window_error(a,b,c)
		{
		//	alert(b+","+c+"\r\n"+a+"\r\n"+GetStackTrace());
		}
		</script>
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
	</HEAD>
	<body bottomMargin="0" topMargin="0" marginwidth="0" marginheight="0">
		<img src="<%=ChatWebUtility.SupportWaitImage%>" border="0">
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
									<input id="input_name" value='<%=displayname%>' style="width:180px;">
								</td>
							</tr>
							<tr>
								<td>
									<B>E-Mail:</B>
								</td>
								<td>
									<input id="input_email" style="width:180px;">
								</td>
							</tr>
							<%
							string custdataname=ChatWebUtility.GetConfig("LS_CustomDataName");
							%>
							<%if(custdataname!=null&&custdataname.Length!=0)%>
							<%{%>
							<TR>
								<TD>
									<B><%=custdataname%>:</B>
								</TD>
								<TD>
									<input id="input_customdata" style="WIDTH:180px">
								</TD>
							</TR>
							<%}%>
							<tr>
								<td>
									<B>Question:</B>
								</td>
								<td>
									<textarea id="input_question" rows="3" style="width:180px;"></textarea>
								</td>
							</tr>
							<tr>
								<td>
									<B>Department:</B>
								</td>
								<td>
									<!-- TODO: add your departments here -->
									<select id="input_department" style="width:180px;">
										<%foreach(string department in ChatWebUtility.GetDepartments())%>
										<%{%>
										<option value="<%=Server.HtmlEncode(department)%>"><%=Server.HtmlEncode(department)%></option>
										<%}%>
									</select>
								</td>
							</tr>
							<tr>
								<td colspan="2" align="center">
									<br>
									<input type='hidden' id="input_referrer" value='<%=Server.HtmlEncode(Request.QueryString["Referrer"])%>'>
									<input type='hidden' id="input_url" value='<%=Server.HtmlEncode(Request.QueryString["Url"])%>'>
									<button id="button_submit" onclick="submit_form()">Request Chat..</button> &nbsp;
									<button onclick="window.close()">Close</button>
								</td>
							</tr>
							<tr>
								<td colspan="2" align="center">
								</td>
							</tr>
						</table>
						<div id="span_waiting" style="display:none">
							Please wait for a live consultant to respond. <span style="display:none;">You are 
								number <span id="span_pos">-</span> in the queue. </span>Thank you for 
							waiting.
						</div>
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
	</body>
	<script>
	function Chat_CreateRequest()
	{
		if(typeof(XMLHttpRequest)!="undefined")
			return new XMLHttpRequest();
		return new ActiveXObject("Microsoft.XMLHTTP");
	}
	
	var _chat_waittimerid=0;
	var _chat_waiting=false;
	var _chat_waitprops={};
	var _chat_customerid=null;
	
	function Chat_IntervalWait(props)
	{
		//changed by terry 2006-06-10
		var _chat_waittimeout=1000;
		
		clearTimeout(_chat_waittimerid);
		_chat_waittimerid=setTimeout(Chat_IntervalWait,_chat_waittimeout);
		
		if(props)
		{
			for(var p in props)
			{
				if(props.hasOwnProperty(p))
					_chat_waitprops[p]=props[p];
			}
		}
		
		if(_chat_waiting)return;
		_chat_waiting=true;

		var xh=Chat_CreateRequest();
		function HandleReadyState()
		{
			if(xh.readyState<4)return;

			xh.onreadystatechange=new Function();
			var xh2=xh;
			xh=null;
			
			_chat_waiting=false;
			
			Chat_IntervalWaitHandleResponse(xh2);
		}
		
		var encode=escape
		
		if(typeof(encodeURIComponent)!="undefined")
			encode=encodeURIComponent;
		
		xh.onreadystatechange=HandleReadyState;
		var url="SupportCustomerHandler.ashx?_temp="+(new Date().getTime())+"&Type=WAIT"
			+"&Url="+encode(_chat_waitprops["Url"]||"")
			+"&Referrer="+encode(_chat_waitprops["Referrer"]||"")
			+"&Name="+encode(_chat_waitprops["Name"]||"")
			+"&Email="+encode(_chat_waitprops["Email"]||"")
			+"&CustomData="+encode(_chat_waitprops["CustomData"]||"")
			+"&Department="+encode(_chat_waitprops["Department"]||"")
			+"&Question="+encode(_chat_waitprops["Question"]||"")
			;
		if(_chat_customerid!=null)
		{
			url+="&CCCustomerId="+escape(_chat_customerid);
		}
		xh.open("GET",url,true,null,null);
		xh.send("");
	}
	function Chat_IntervalWaitHandleResponse(xh)
	{
		if(xh.status!=200)return;//ignore error
		var text=xh.responseText;
		if(text==null||text=="")return;//unknown output
		
		//input_name.value=new Date().getSeconds()+" - "+text;
		
		var arr=text.split(":");
		switch(arr[0])
		{
			case "READY":
				clearTimeout(_chat_waittimerid);
				if(_chat_customerid)
					location.href="SupportClient.aspx?CCCustomerId"+escape(_chat_customerid);
				else
					location.href="SupportClient.aspx";
				break;
			case "NOAGENT":
				clearTimeout(_chat_waittimerid);
				WaitOperator_HandleError("NOAGENT");
				break;
			case "WAITING":
				WaitOperator_UpdatePosition(arr[1]);
				if(arr[2]=="Guest"&&arr[3])
				{
					if(document.cookie&&document.cookie.indexOf(arr[3])!=-1)
						break;
					_chat_customerid=arr[3];
				}
				break;
			case "ERROR":
				clearTimeout(_chat_waittimerid);
				WaitOperator_HandleError("ERROR",arr[1]);
			default:
				break;
		}
	}

	function WaitOperator_HandleError(type,msg)
	{
	}
	function WaitOperator_UpdatePosition(pos)
	{
	}

	</script>
	<script>
	
	var supportRequireMail=<%=ChatWebUtility.SupportRequireMail?"true":"false"%>;
	
	var redirectIfNoAcceptForSeconds=600;
	
	function submit_form()
	{
		var input_name=document.getElementById("input_name");
		var input_email=document.getElementById("input_email");
		var input_customdata=document.getElementById("input_customdata");
		var input_department=document.getElementById("input_department");
		var input_question=document.getElementById("input_question");
		var button_submit=document.getElementById("button_submit");
		
		if(input_name.value=="")
		{
			alert("Please input your name");
			input_name.focus();
			return;
		}
		
		if(supportRequireMail)
		{
			if(input_email.value=="")
			{
				alert("Please input your email");
				input_email.focus();
				return;
			}
			var exp=/[^@]+@[^@]+\.[^@]+/i;
			if(!exp.test(input_email.value))
			{
				alert("Please input a valid email");
				input_email.focus();
				return;
			}
		}
	
		var props={};
		props["Name"]=input_name.value;
		props["Email"]=input_email.value||"";
		props["Department"]=input_department.value||"";
		if(input_customdata)
		{
			props["CustomData"]=input_customdata.value||"";
		}
		props["Question"]=input_question.value||"";
		props["Url"]=input_url.value||"";
		props["Referrer"]=input_referrer.value||"";
		
		//more public properties ?
		Chat_IntervalWait(props);

		input_name.disabled=true;
		input_email.disabled=true;
		button_submit.disabled=true;
		span_waiting.style.display=''
		
		setTimeout(RedirectIfNoResponse,redirectIfNoAcceptForSeconds*1000);
	}
	function RedirectIfNoResponse()
	{
		location.href="SupportFeedback.aspx"+'<%=Request.Url.Query%>'
	}
	
	function WaitOperator_HandleError(type,errmsg)
	{
		var input_name=document.getElementById("input_name");
		var input_email=document.getElementById("input_email");
		var button_submit=document.getElementById("button_submit");
		
		input_name.disabled=false;
		input_email.disabled=false;
		button_submit.disabled=false;
		span_waiting.style.display='none'
		
		if(type=="NOAGENT")
		{
			alert("No agent online!");
		}
		else
		{
			alert(errmsg);
		}
		//location.href="SupportFeedback.aspx";
	}
	
	//override the function , as event handler to update the position information to UI
	function WaitOperator_UpdatePosition(pos)
	{
		span_pos.innerHTML=pos;
	}
	</script>
</html>
