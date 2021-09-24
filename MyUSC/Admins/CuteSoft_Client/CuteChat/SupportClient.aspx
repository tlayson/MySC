<%@ Import Namespace="CuteChat" %>

<%@ Page Language="c#" Inherits="CuteChat.ChatPageBase" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server" ID="Head1" NAME="Head1">
    <title>[[UI_Support_Title]]</title>
    <link rel="icon" href="Icons/Support.ico" type="image/x-icon" />
    <link rel="shortcut icon" href="Icons/Support.ico" type="image/x-icon" />

    <script runat="server">
		
        public string GetQueryString(string name)
        {
            string str = Request.QueryString[name];
            if (str != null) return str;
            Uri refer = Request.UrlReferrer;
            string val;
            if (refer != null)
            {
                val = FindQueryValue(name, refer.Query);
                if (val != null)
                    return val;
            }
            HttpCookie cookie = Request.Cookies["CuteSupportQuery"];
            if (cookie != null)
            {
                val = FindQueryValue(name, cookie.Value);
                if (val != null)
                    return val;
            }
            return null;
        }
        public string FindQueryValue(string name, string query)
        {
            if (query == null) return null;
            query = query.TrimStart('?');
            string[] arr = query.Split('&');
            for (int i = 0; i < arr.Length; i++)
            {
                string[] parts = arr[i].Split('=');
                if (parts.Length != 2)
                    continue;
                if (string.Compare(parts[0], name, true) == 0)
                {
                    return HttpUtility.UrlDecode(parts[1]);
                }
            }
            return null;
        }

        bool promptwait = false;
        string placename;
        string displayname;
        override protected void OnInit(EventArgs args)
        {
            AppChatIdentity identity = ChatWebUtility.GetLogonIdentity();
            if (identity != null)
                displayname = identity.DisplayName;

            promptwait = GetQueryString("PromptWait") != "0";

            placename = GetQueryString("PlaceName");
            if (placename == null || placename == "")
            {
                string userid;
                if (identity != null)
                    userid = identity.UniqueId;
                else
                    userid = ChatWebUtility.InitUniqueId();
                placename = "SupportSession:" + userid;
            }

            if (promptwait)
            {
                if (ChatApi.LSIsSessionActive(placename))
                    promptwait = false;
            }

            if (!promptwait)
            {
                if (ChatWebUtility.IsDownLevelBrowser)
                {
                    //Response.Redirect("FL_MainForm.aspx"+Request.Url.Query,true);
                }
            }

            base.OnInit(args);
        }
    </script>

    <script>
		window.resizeTo(570, 460);
		function GetStackTrace()
		{
			return "";
		}
		window.onerror=function window_error(a,b,c)
		{
			alert(b+","+c+"\r\n"+a+"\r\n"+GetStackTrace());
		}
    </script>

    <base target="_blank" />
    <link rel="stylesheet" href="SupportClient.css" />
    <style type="text/css"> 
			body { padding: 5px 0 0 0;}
			body, td { font: 12px Arial, Helvetica, sans-serif; }
            table { border: none; }
            img{border:0;}
            td{vertical-align:top;}
			a, a:visited, a:link { color: #0000ff; text-decoration: underline; }
            .inputwidth{width:190px;}
			a:hover { color: #FF3300; text-decoration: underline; }
			.bottomHeight { height:75px; }
			.informationBox	{ background-color: #ffffff; padding:10px;border:1px solid #efefef;}
			#messagePanel { height:240px;width:100%;padding:4px;background-color: #ffffff;border:1px solid #A5B6DE; }
			#rightPanel { height:240px;width:108px;padding:4px;background-color: #ffffff;border:1px solid #A5B6DE; }
			#span_waiting{font-family:segoe ui, arial,verdana,helvetica,sans-serif;font-size:90%;color:#444;}
			#span_nextline{font-family:segoe ui, arial,verdana,helvetica,sans-serif;font-size:90%;color:#444;}
			#dropdown_rate{font-family:segoe ui, arial,verdana,helvetica,sans-serif;font-size:90%;color:#444;margin-bottom:10px;}
		</style>
</head>
<body scroll="no" style="overflow: hidden">
    <div id="promptwaitcontainer" style='position: absolute; margin-top: -5px; width: 100%;
        height: 100%'>
        <img src="<%=ChatWebUtility.SupportWaitImage%>" alt="live chat" />
        <table cellspacing="0" cellpadding="2" style="width: 99%; height: 100%">
            <tr>
                <td class="informationBox">
                    <p>
                        [[UI_Support_Welcome]]</p>
                    <table border="0" cellspacing="0" cellpadding="4" width="100%">
                        <tr>
                            <td style="width:100px">
                                <b>[[Name]]</b>
                            </td>
                            <td>
                                <input id="input_name" value='<%=displayname%>' class="inputwidth" /><span style="color:red">*</span>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <b>[[Email]]</b>
                            </td>
                            <td>
                                <input id="input_email" class="inputwidth" />
                            </td>
                        </tr>
                        <%
                            string custdataname = ChatWebUtility.GetConfig("LS_CustomDataName");
                        %>
                        <%if (custdataname != null && custdataname.Length != 0)%>
                        <%{%>
                        <tr>
                            <td>
                                <b>
                                    <%=custdataname%>
                                    :</b>
                            </td>
                            <td>
                                <input id="input_customdata" class="inputwidth" />
                            </td>
                        </tr>
                        <%}%>
                        <tr>
                            <td>
                                <b>[[Question]]</b>
                            </td>
                            <td>
                                <textarea id="input_question" rows="5" cols="40" class="inputwidth"></textarea>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <b>[[Department]]</b>
                            </td>
                            <td>
                                <!-- TODO: add your departments here -->
                                <select id="input_department" class="inputwidth">
                                    <%foreach (string department in ChatWebUtility.GetDepartments())%>
                                    <%{%>
                                    <option value="<%=Server.HtmlEncode(department)%>">
                                        <%=Server.HtmlEncode(department)%>
                                    </option>
                                    <%}%>
                                </select>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" align="right">
                                <br />
                                <input type='hidden' id="input_referrer" value='<%=Server.HtmlEncode(GetQueryString("Referrer"))%>' />
                                <input type='hidden' id="input_url" value='<%=Server.HtmlEncode(GetQueryString("Url"))%>' />
                                <button id="button_submit" onclick="call_submit_form();return false;">
                                    [[StartChat]]</button>
                                <!-- &nbsp;	<button onclick="window.close()">Close</button> -->
                            </td>
                        </tr>
                    </table>
                </td>
                <td>
                    <img alt="Live Support" src="images/live-support-woman.jpg" />
                    <div style="display: none">
                        <a href="SupportFeedback.aspx">[[Feedback]]</a>
                    </div>
                </td>
            </tr>
        </table>
    </div>
    <table id="maintable" style="display: none; width: 100%" border="0" cellspacing="3"
        cellpadding="0">
        <tr>
            <td id="messagePanel">
                <div id="messageList" style="overflow-y: auto; height: 100%; overflow: auto">
                </div>
            </td>
            <td id="rightPanel">
                <table border="0" cellspacing="2" cellpadding="0" style="height:100%">
                    <tr>
                        <td style="width:160px">
                            <div id="userList">
                                <img src='images/live-support-woman.jpg' alt="live chat" />
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td valign="bottom">
                            <select id="dropdown_rate" onchange="SUI_RateTheAgent(this.value)">
                                <option value="0" selected="selected">-- Rate the agent --</option>
                                <option value="5">Very Good</option>
                                <option value="4">Good</option>
                                <option value="3">Normal</option>
                                <option value="2">Bad</option>
                                <option value="1">Very Bad</option>
                            </select>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr class="bottomHeight">
            <td colspan="2">
                <div style="border-bottom: #a5b6de 1px solid; padding-bottom: 2px; padding-left: 5px;
                    width: 100%; padding-right: 0px; padding-top: 3px">
					<%if(ChatWebUtility.GlobalShowBoldButton){%>
						<img title="[[UI_Bold]]" src='<%=ResolveUrl("images/bold.gif")%>' class="button" onclick="SetFontBold((this.className=(this.className=='button'?'buttondown':'button'))=='buttondown')" />
					<%}%>
					<%if(ChatWebUtility.GlobalShowItalicButton){%>
					<img title="[[UI_Italic]]" src='<%=ResolveUrl("images/italic.gif")%>' class="button" onclick="SetFontItalic((this.className=(this.className=='button'?'buttondown':'button'))=='buttondown')" />
					<%}%>
					<%if(ChatWebUtility.GlobalShowUnderlineButton){%>
					<img title="[[UI_Underline]]" src='<%=ResolveUrl("images/underline.gif")%>' class="button" onclick="SetFontUnderline((this.className=(this.className=='button'?'buttondown':'button'))=='buttondown')" />
					<%}%>
                   
                    <%if (ChatWebUtility.GlobalShowFontName)
                      {%>
                    <select style="vertical-align: middle" onchange="SetFontName(this.value)">
                        <option value="" selected="selected">[[UI_Font]]</option>
                        <option value="Arial">Arial</option>
                        <option value="Verdana">Verdana</option>
                        <option value="Courier">Courier</option>
                        <option value="Impact">Impact</option>
                    </select>
                    <%}%>
                    <%if (ChatWebUtility.GlobalShowFontSize)
                      {%>
                    <select style="width: 60px; vertical-align: middle" onchange="SetFontSize(this.value)">
                        <option value="" selected="selected">[[UI_Size]]</option>
                        <option value="7">7</option>
                        <option value="8">8</option>
                        <option value="9">9</option>
                        <option value="10">10</option>
                        <option value="11">11</option>
                        <option value="12">12</option>
                        <option value="14">14</option>
                        <option value="15">15</option>
                        <option value="16">16</option>
                        <option value="18">18</option>
                        <option value="20">20</option>
                        <option value="22">22</option>
                        <option value="28">28</option>
                        <option value="32">32</option>
                    </select>
                    <%}%>
                    <%if (ChatWebUtility.GlobalShowEmotion)
                      {%>
                    <img alt="[[UI_Emotion]]" src='images/emotion.png' class="button" onmouseover="this.className='buttonover'"
                        onmouseout="this.className='button'" onclick="ChatUI_ShowEmotionPanel(this)">
                    <%}%>
                    
                    <img title="[[UI_EnableSound]]" id=toolbarsoundbutton src='<%=ResolveUrl("images/sound_on.png")%>' class="button" onmouseover="this.className='buttonover'" onmouseout="this.className='button'"/>
					<script>				
					var toolbarsoundbutton=document.getElementById("toolbarsoundbutton"); 
					
					toolbarsoundbutton.onclick=function()
					{
						ChatUI_SetEnableSound( !ChatUI_GetEnableSound() );
					}
					setInterval(function(){
						if(typeof(ChatUI_GetEnableSound)=="undefined")return;
						var condition=ChatUI_GetEnableSound();
						if(toolbarsoundbutton.getAttribute("condition")==(condition?"1":"0"))return;
						toolbarsoundbutton.setAttribute("condition",condition?"1":"0");
						toolbarsoundbutton.title=TEXT( condition ?"UI_DisableSound":"UI_EnableSound" );
						toolbarsoundbutton.src= condition?'<%=ResolveUrl("images/sound_on.png")%>':'<%=ResolveUrl("images/sound_off.png")%>'
					},100);
					</script>

                    <%if (ChatWebUtility.SupportAllowSendFile)
                      {%>
                    <img alt="[[UI_SendFile]]" src="images/upload.png" class="button" onmouseover="this.className='buttonover'"
                        onmouseout="this.className='button'" onclick="IsConnected()&amp;&amp;ChatUI_ShowSendFile()" />
                    <%}%>
                    <img alt="[[UI_MENU_SaveMessages]]" src="images/disk.png" class="button" onmouseover="this.className='buttonover'"
                        onmouseout="this.className='button'" onclick="SaveMessages($('messageList'))" />
                    <%if (ChatWebUtility.SupportAllowSendMail)
                      {%>
                    <img alt="[[UI_MENU_EmailMessages]]" src="images/mail.png" class="button" onmouseover="this.className='buttonover'"
                        onmouseout="this.className='button'" onclick="SUI_SendMail()" />
                    <%}%>
                    <img alt="[[UI_Print]]" src="images/printer.png" class="button" onmouseover="this.className='buttonover'"
                        onmouseout="this.className='button'" onclick="window.print()" />
                    <img alt="[[UI_Close]]" src="images/exit.png" class="button" onmouseover="this.className='buttonover'"
                        onmouseout="this.className='button'" onclick="ChatUI_QuitWithConfirm()" />
                </div>
                <div>
                    <table style="background-color: #ffffff; width: 100%; border-top: #a5b6de 0px solid;
                        border-left: #a5b6de 1px solid" border="0" cellspacing="0" cellpadding="0">
                        <tr>
                            <td>
                                <textarea id="inputBox" cols="40" style="border-bottom: #ffffff 0px solid; border-left: #ffffff 0px solid;
                                    padding-bottom: 1px; background-color: #ffffff; padding-left: 1px; width: 99%;
                                    padding-right: 1px; height: 56px; overflow: auto; border-top: #ffffff 0px solid;
                                    border-right: #ffffff 0px solid; padding-top: 1px"></textarea>
                            </td>
                            <td align="right" style="width:80px; padding:2px;">
                                <button id="buttonSend" class="SendButton">
                                    Send</button>
                            </td>
                        </tr>
                    </table>
                </div>
                <table style="width: 100%; height: 22px; border-top: #a5b6de 1px solid" border="0"
                    cellspacing="2" cellpadding="0">
                    <tr>
                        <td>
                            <span id="TypingStatus"></span><span id="span_waiting" style="display: none">You are
                                #<span id="span_pos">-</span> in the queue. </span>
                        </td>
                        <td align="right">
                            <span id="span_nextline">[[GoNextLine]]</span></td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>

    <script type="text/javascript">
		var cohome='<%=ResolveUrl("~/")%>';
		var promptwait=<%=promptwait?"true":"false"%>
		var placename='<%=placename%>'
		var supportRequireMail=<%=ChatWebUtility.SupportRequireMail?"true":"false"%>;
		var querystring='<%=Request.Url.Query%>';
		var submit_clicked=false;
		function call_submit_form()
		{
			submit_clicked=true;
		}
		
    </script>

    <script type="text/javascript" src="LoadSettings.aspx"></script>

    <script type="text/javascript" src="LoadScripts.aspx"></script>

    <script type="text/javascript" src="SupportClient.js"></script>

    <script type="text/javascript">
		
		function call_submit_form()
		{
			submit_form();
		}
		if(submit_clicked)
		{
			submit_form();
		}
		
    </script>

</body>
</html>