
var Desktop=CuteWebUI.HTML;

CuteWebUI.Resource=__cc_urlbase;

function CCM_LoadTemplate(filename)
{
	var text=CCM_LoadTemplate["Cache"+filename];
	if(!text)
	{
		var xh=CuteWebUI.Ajax.CreateXMLHttp();
		xh.open("GET",__cc_urlbase+filename+"?_time="+new Date().getTime(),false,null,null);
		xh.send("");
		text=xh.responseText;
		text=text.replace(/\[\[([^\]]+)\]\]/gi,function(a,b,c){return TEXT(b)});
		text=text.split("#__cc_urlbase#").join(__cc_urlbase);
		CCM_LoadTemplate["Cache"+filename]=text;
	}
	return text;
}

function CuteChatMessenger(fullmode)
{
	var templatemain=CCM_LoadTemplate("NewMessengerMain.htm");;
	var templatesign=CCM_LoadTemplate("NewMessengerSign.htm");;

	this._win=new CuteWebUI.HTML.Window({
		onclose:CuteWebUI.Delegate(this,this._OnClose)
		,
		onresize:CuteWebUI.Delegate(this,this._OnResize)
	});


	this._container=this._win.GetContentElement();
	this._container.messengerInstance=this;
	
	this._signpanel=document.createElement("DIV");
	this._mainpanel=document.createElement("DIV");
	
	this._container.appendChild(this._signpanel);
	this._container.appendChild(this._mainpanel);
	
	this._mainpanel.innerHTML=templatemain;
	this._signpanel.innerHTML=templatesign;

	
	Connect("Messenger");

	if(IsConnected())
		this._signpanel.style.display="none";
	else
		this._mainpanel.style.display="none";
	this.UpdateContactList();
		
	this._win.SetTitle("Cute Web Messenger");
	this._win.SetWidth(260);
	this._win.SetHeight(480);
	

	this._win.SetTop(Math.max(0,parseInt((document.documentElement.clientHeight-480)/4)));
	this._win.SetLeft(Math.max(0,parseInt((document.documentElement.clientWidth-240)/8)));
	
	this._intervalid=setInterval(CuteWebUI.Delegate(this,this._OnInterval),100);
	
	var sbtn=CuteWebUI.HTML.FindChild(this._container,"ccm_SoundButton");
	if(sbtn)
	{
		//call twice to update the UI..
		CuteChatMessenger.BubbleCommand("ToggleSound",sbtn,null);
		CuteChatMessenger.BubbleCommand("ToggleSound",sbtn,null);
	}
	
	if(fullmode)
	{
		this._fullmode=true;
		this._win.FullWindow();
	}
}

CuteChatMessenger.prototype._OnResize=function __CuteChatMessenger__OnResize(reason)
{
	var ce=this._win.GetContentElement();
	var cl=CuteWebUI.HTML.FindChild(this._container,"ccm_ContactList");
	try{cl.style.height=(parseInt(ce.style.height)-170)+"px";}catch(x){}
	var md=CuteWebUI.HTML.FindChild(this._container,"ccm_MyDescription");
	try{md.style.width=(parseInt(ce.style.width)-80)+"px";}catch(x){}
}

CuteChatMessenger.prototype._OnClose=function __CuteChatMessenger__OnClose(win,reason)
{
	if(reason=="close")return;
	
	var thisobj=this;
	Desktop.Confirm(function(res){
		if(res)
			thisobj.Dispose();
	},TEXT("UI_AreYouSureQuitMessenger"));
	
	return true;
}
CuteChatMessenger.prototype._OnInterval=function __CuteChatMessenger__OnInterval()
{
	if(this._convs)
	{
		for(var i=0;i<this._convs.length;i++)
		{
			this._convs[i]._OnInterval();
		}
	}
	
	this._ContinueBlink();
}
CuteChatMessenger.prototype.Dispose=function __CuteChatMessenger_Dispose()
{
	clearInterval(this._intervalid);
	if(this._convs)
	{
		for(var i=0;i<this._convs.length;i++)
		{
			this._convs[i].Dispose();
		}
	}
	this._container.messengerInstance=null;
	this._win.Close();
	window.__messenger=null;
	Disconnect(false);
	ChatUI_IMUnloadMainForm();
}
CuteChatMessenger.prototype.Show=function __CuteChatMessenger_Show()
{
	this._win.Show();
}
CuteChatMessenger.prototype.IsConversationActived=function __CuteChatMessenger_IsConversationActived(contact)
{
	if(!this._convs)this._convs=[];
	for(var i=0;i<this._convs.length;i++)
	{
		if(UserEquals(this._convs[i]._contact,contact))
		{
			return true;
		}
	}
}
CuteChatMessenger.prototype.ActiveConversation=function __CuteChatMessenger_ActiveConversation(contact)
{
	if(!this._convs)this._convs=[];
	for(var i=0;i<this._convs.length;i++)
	{
		if(UserEquals(this._convs[i]._contact,contact))
		{
			this._convs[i].Activate();
			return;
		}
	}
	
	if(!this._fullmode)
	{
		var conv=new CuteChatConversation(this,contact);
		this._convs.push(conv);
		ChatUI_FocusWindow();
		conv.Activate();
		return;
	}
	
	//FULL MODE :
	
	var win;
	try
	{
		var winname=0;
		for(var i=0;i<contact.UserId.length;i++)winname+=contact.UserId.charCodeAt(i);
		win=window.open(window.location.href,"user"+winname,"status=1,width=500,height=400,resizable=1");
	}
	catch(x)
	{
	}
	
	if(win==null)
	{
		if(!this.IsContactBlink(contact))
		{
			var m=this;
			Desktop.Confirm(function(res){
				if(res)m.ActiveConversation(contact);
			},contact.DisplayName+" sent you a message");
		}
		
		if(IsContact(contact))
		{
			this.SetContactBlink(contact,true);
		}
		
		ChatUI_FocusWindow();
		
		return;
	}
	
	this.SetContactBlink(contact,false);
	
	var proxy={};
	proxy._window=window;
	proxy._messenger=this;
	proxy._contact=contact;
	window._last_conversationproxy=proxy;
	
	win._conversationproxy=proxy;
	proxy.win=win;
	
	proxy._OnInterval=function()
	{
		try
		{
			if(proxy._conv)
				proxy._conv._OnInterval();
		}
		catch(x)
		{
		}
	}
	proxy.Dispose=function()
	{
		try
		{
			if(proxy._conv)
				proxy._conv.Dispose();
			proxy.win.close();
		}
		catch(x)
		{
		}
	}
	proxy.HandleChatEvent=function(a,b,c,d,e)
	{
		try
		{
			if(proxy._conv)
				proxy._conv.HandleChatEvent(a,b,c,d,e);
		}
		catch(x)
		{
		}
	}
	proxy.Activate=function()
	{
		try
		{
			if(proxy._conv)
			{
				proxy.win.ChatUI_FocusWindow();
				proxy._conv.Activate();
			}
		}
		catch(x)
		{
		}
	}
	proxy.OnLoad=function()
	{
		proxy._messenger._convs.push(proxy);
		proxy.win.CuteChatMessenger.StartConversitionForProxy(proxy);
	}
}
CuteChatMessenger.StartConversitionForProxy=function __CuteChatMessenger_StartConversitionForProxy(proxy)
{
	//replace the CuteChat client JavaScript API !
	var apilist=["SetFontName","GetFontName","SetFontSize","GetFontSize","SetFontColor","GetFontColor","SetFontBold","GetFontBold","SetFontItalic","SetFontItalic","SetFontUnderline","GetFontUnderline"
		,"IsConnected","IsConnecting","FireMessage","PushCTSMessage","DoSendMessage","GetMessages","__SendMessage","SendMessage","SendEmotion"
		,"GetUsers","GetUserByGuid","GetUserByName","GetUserById","GetTypingUsers","IsUserTyping"
		,"GetContacts","GetContactByName","GetContactById","SetInstantContact","GetInstantContact"
		,"GetIgnores","GetIgnoreByName","GetIgnoreById","GetSelectedContact","SetSelectedContact","GetSelectedIgnore","SetSelectedIgnore"
		,"AddContact","RemoveContact","AddIgnore","RemoveIgnore","IsContact","IsBlock","SetBlock"
		,"InvitePrivateChat","InvitePrivateChatBatch","AcceptPrivateChat","RejectPrivateChat","InviteIntoPrivateChat"
		,"GetPlace","IsMessenger","GetLocation","GetMyInfo","SetOnlineStatus","ChangeDisplayName","SetDescription"
		,"SetPrivateProperty","SetPublicProperty","SetAvatar","SetInstantAvatar","SetIsTyping","InstantSetIsTyping"
		//,"_InvokeChatEvent"
		]
	for(var i=0;i<apilist.length;i++)
	{
		var fn=apilist[i];
		window[fn]=proxy._window[fn]||window[fn];
	}
	
	window._InvokeChatEvent=function(name,args)
	{
	    if(name=="IMCOOKIE")
	        return;
	    proxy._window._InvokeChatEvent(name,args);
	}
	
	ChatUI_InsertEmotion=function(emotion)
	{
		ChatUI_Emotion_OnDocumentMouseDown();
		conv.HandleChatEvent("UICOMMAND","EMOTION",emotion);
		conv.HandleChatEvent("UICOMMAND","FOCUSINPUT");
	}

	var conv=new CuteChatConversation(proxy._messenger,proxy._contact,true);
	window.__conversation=conv;
	proxy._conv=conv;
	conv.Activate();
}

CuteChatMessenger.prototype.CloseConversation=function __CuteChatMessenger_CloseConversation(conv)
{
	if(!this._convs)return;
	for(var i=0;i<this._convs.length;i++)
	{
		if(this._fullmode)
		{
			if(this._convs[i]._conv==conv)
			{
				this._convs.splice(i,1);
				conv.Dispose();
				return;
			}
		}
		else
		{
			if(this._convs[i]==conv)
			{
				this._convs.splice(i,1);
				conv.Dispose();
				return;
			}
		}
	}
}
CuteChatMessenger.prototype.SetContactBlink=function __CuteChatMessenger_SetContactBlink(contact,shake)
{
	if(!this._blinklist)
	{
		this._blinklist=[];
	}
	else
	{
		for(var i=0;i<this._blinklist.length;i++)
		{
			if(UserEquals(this._blinklist[i],contact))
			{
				if(!shake)
					this._blinklist.splice(i,1);
				return;
			}
		}
	}
	if(shake)
	{
		this._blinklist.push(contact);
	}
}
CuteChatMessenger.prototype.IsContactBlink=function __CuteChatMessenger_IsContactBlink(contact)
{
	if(!this._blinklist)return false;
	for(var i=0;i<this._blinklist.length;i++)
	{
		if(UserEquals(this._blinklist[i],contact))
		{
			return true;
		}
	}
}
CuteChatMessenger.prototype._ContinueBlink=function __CuteChatMessenger__ContinueBlink()
{
	var nt=new Date().getTime();
	if(this.blinktime)
	{
		if(nt-this.blinktime<450)
			return;
	}
	this.blinktime=nt;
	
	if(!this._blinklist)return;
	
	var cl=CuteWebUI.HTML.FindChild(this._container,"ccm_ContactList");
	if(!cl)return;
	
	var newlist=[];
	
	var coll=cl.getElementsByTagName("div");
	for(var i=0;i<coll.length;i++)
	{
		var div=coll.item(i);
		
		if(div.blinked)continue;
		
		var cuid=div.getAttribute("contact");
		if(!cuid)continue;
		var contact=GetContactById(cuid);
		if(!contact)continue;
		if(!this.IsContactBlink(contact))continue;
		
		div._backupClass=div.className;
		div.blinked=true;
		div.className=div.className+" MessengerContactBlink"
		newlist.push(div);
	}
	
	if(this._blinkedlist)
	{
		for(var i=0;i<this._blinkedlist.length;i++)
		{
			var div=this._blinkedlist[i];
			div.blinked=false;
			div.className=div._backupClass;
		}
	}
	
	this._blinkedlist=newlist;
	
}

CuteChatMessenger.prototype.UpdateContactListDelay=function __CuteChatMessenger_UpdateContactListDelay()
{
		clearTimeout(this._updateTimeoutid);
		this._updateTimeoutid=setTimeout(CuteWebUI.Delegate(this,this.UpdateContactList),1);
}
CuteChatMessenger.prototype.UpdateContactList=function __CuteChatMessenger_UpdateContactList()
{
	var cl=CuteWebUI.HTML.FindChild(this._container,"ccm_ContactList");
	if(!cl)return;
	
	var myinfo=GetMyInfo();
	if(!myinfo)return;
		
	var filter=this._filterword||"";
	filter=filter.toLowerCase();
	
	var groupmap={};
	var groups=[];
	var group;
	
	var imicon=myinfo.PrivateProperties["IMICON"];
	var sortgroup=myinfo.PrivateProperties["IMSORT"]=="Group";

	var customgroupmap={};
	var contactgroupmap={};
	if(sortgroup)
	{
		group={};
		group.Name=TEXT("Default");
		group.Items=[];
		groupmap[group.Name]=group;
		groups.push(group);
		
		customgroupmap=PropStrToObj(myinfo.PrivateProperties["IMGROUPS"])||{};
		for(var gname in customgroupmap)
		{
			customgroupmap[gname]=PropStrToObj(customgroupmap[gname])||{};
			for(var cid in customgroupmap[gname])
			{
				contactgroupmap[cid]=gname;
			}
			
			var group={};
			group.Name=gname;
			group.Items=[];
			groupmap[gname]=group;
			groups.push(group);
		}
	}
	else
	{
		group={};
		group.Name=TEXT("Online")
		group.Items=[];
		groupmap[group.Name]=group;
		groups.push(group);
		group={};
		group.Name=TEXT("Offline")
		group.Items=[];
		groupmap[group.Name]=group;
		groups.push(group);
		group={};
		group.Name=TEXT("Blocked")
		group.Items=[];
		groupmap[group.Name]=group;
		groups.push(group);
	}
	
	var arr=GetContacts();
	var showcontactcount=0;
	
	for(var i=0;i<arr.length;i++)
	{
		var contact=arr[i];
		
		if(filter.length&&contact.DisplayName.toLowerCase().indexOf(filter)==-1)
			continue;
		
		showcontactcount++;
		
		var groupname;
		if(sortgroup)
		{
			groupname=contactgroupmap[contact.UserId];
			if(!groupname)
				groupname=groups[0].Name;
		}
		else
		{
			if(IsBlock(contact))
				groupname=TEXT("Blocked");
			else if(contact.IsOnline)
				groupname=TEXT("Online");
			else
				groupname=TEXT("Offline");
		}
		contact._groupname=groupname;
		
		var group=groupmap[groupname];
		if(!group)
		{
			group={};
			group.Name=groupname;
			group.Items=[];
			groupmap[groupname]=group;
			groups.push(group);
		}
		group.Items.push(contact);
	}

	
	function StrCompare(s1,s2)
	{
		s1=s1.toLowerCase();
		s2=s2.toLowerCase();
		var minl=Math.min(s1.length,s2.length);
		for(var i=0;i<minl;i++)
		{
			var c1=s1.charCodeAt(i);
			var c2=s2.charCodeAt(i);
			if(c1!=c2)
				return c1-c2;
		}
		return s1.length-s2.length;
	}
	function GetGroupIndex(g)
	{
		if(g.Name==TEXT("Online"))return -3;
		if(g.Name==TEXT("Offline"))return -2;
		if(g.Name==TEXT("Blocked"))return -1;
		return 0;
	}
	groups.sort(function(g1,g2){
		var i1=GetGroupIndex(g1);
		var i2=GetGroupIndex(g2);
		if(i1==i2)
			return StrCompare(g1.Name,g2.Name);
		return i1-i2;
	});
	
	var sb=[];
	
	if(showcontactcount==0) //no contact found
	{
		if(filter.length!=0)
		{
			cl.innerHTML="No contacts be found by the filter....";
			return;
		}
		
		if(arr.length==0)
		{
			//cl.innerHTML="Please click add contact button....";
			//return;
		}
	}
	
	for(var gi=0;gi<groups.length;gi++)
	{
		var group=groups[gi];
		
		sb.push("<div class='MessengerGroup");
		if(this._selectedline=="group:"+group.Name)
			sb.push(" MessengerGroupSelected");
		sb.push("'");
		if(sortgroup)
			sb.push(" oncontextmenu='CuteChatMessenger.BubbleCommand(\"ShowGroupMenu\",this,event);return false;'");
		sb.push(" group='"+group.Name+"' hoverclass='MessengerGroupHover' onclick='CuteChatMessenger.BubbleCommand(\"ClickGroup\",this,event)''>");
			
		if(this["_collapsed_"+group.Name])
		{
			sb.push("<img title='"+TEXT("ShowContactsinGroup")+"' class='MessengerArrow' src='"+__cc_urlbase+"Images/IM_Arrow1.png'/>");
			sb.push("<img title='"+TEXT("ShowContactsinGroup")+"' class='MessengerArrowOver' src='"+__cc_urlbase+"Images/IM_Arrow1Hover.png'/>")
		}
		else
		{
			sb.push("<img title='"+TEXT("HideContactsinGroup")+"' class='MessengerArrow' src='"+__cc_urlbase+"Images/IM_Arrow2.png'/>");
			sb.push("<img title='"+TEXT("HideContactsinGroup")+"' class='MessengerArrowOver' src='"+__cc_urlbase+"Images/IM_Arrow2Hover.png'/>")
		}
		
		if(sortgroup)
		{
			var onlinecount=0;
			for(var i=0;i<group.Items.length;i++)
			{
				if(group.Items[i].IsOnline)
					onlinecount++;
			}
			sb.push(group.Name+" ( "+onlinecount+"/"+group.Items.length+" ) "+"</div>");
		}
		else
		{
			sb.push(group.Name+" ( "+group.Items.length+" ) "+"</div>");
		}
		
		if(!this["_collapsed_"+group.Name])
		{
			if(group.Items.length==0)
			{
				sb.push("<div class='MessengerNoContact'>"+TEXT("NoContactsinGroup")+"</div>");
			}
			else
			{
				if(sortgroup)
				{
					//sort item by online offline
					group.Items.sort(function(c1,c2){
						if(c1.IsOnline==c2.IsOnline)
							return StrCompare(c1.DisplayName,c2.DisplayName);
						if(c1.IsOnline)
							return -1;
						return 1;
					});
				}
				else
				{
					group.Items.sort(function(c1,c2){
						return StrCompare(c1.DisplayName,c2.DisplayName);
					});
				}
			}
			for(var i=0;i<group.Items.length;i++)
			{
				var contact=group.Items[i];
				var imgurl="im_online.png";
				if(IsBlock(contact))
				{
					imgurl="im_blocked.png";
					if(!contact.IsOnline)
						imgurl="im_blocked_offline.png";
				}
				else if(!contact.IsOnline)
				{
					imgurl="im_offline.png";
				}
				else if(contact.OnlineStatus=="AWAY")
				{
					imgurl="im_away.png";
				}
				else if(contact.OnlineStatus=="PARTIAL")
				{
					imgurl="im_away.png";
				}
				else if(contact.OnlineStatus=="BUSY")
				{
					imgurl="im_busy.png";
				}
				
				sb.push("<div class='MessengerContact");
				if(this._selectedline=="contact:"+contact.UserId)
					sb.push(" MessengerContactSelected");
				sb.push("' hoverclass='MessengerContactHover' contact='"+contact.UserId+"'");
				sb.push(" onhoverenter='CuteChatMessenger.BubbleCommand(\"EnterContact\",this,event)' onhoverleave='CuteChatMessenger.BubbleCommand(\"LeaveContact\",this,event)'");
				if(document.addEventListener)
					sb.push(" onclick='CuteChatMessenger.BubbleCommand(\"ClickContact\",this,event)'");
				else
					sb.push(" onmouseup='if(event.button==1)CuteChatMessenger.BubbleCommand(\"ClickContact\",this,event)'");
				sb.push(" oncontextmenu='CuteChatMessenger.BubbleCommand(\"ShowContactMenu\",this,event);return false;'");
				sb.push(">");
				
				var vt=GetItemInfo(contact.Guid,"VideoTime")||0;
				var videolink="";
				if( new Date().getTime() - vt < 10000 )
				{
					videolink="<img src='"+__cc_urlbase+"Images/camera.png' align='texttop' hspace=4/>";
				}
				
				var imgcls=contact.IsOnline?"ContactImageOnline":"ContactImageOffline"
		
				switch(imicon)
				{
					case "Large":						
						sb.push("<table border='0' cellspacing='0' cellpadding='0' align=left width='90%'><tr>");
						sb.push("<td style='vertical-align:top;' width=42 nowrap>");
						sb.push("<div style='border:solid 1px #E0DFE5;margin-top: 4px;padding:1px' hoverclass='AvatarImgHover'>");
						sb.push("<img title='"+TEXT("UI_USER_"+contact.OnlineStatus)+"' class='"+imgcls+"' src='"+ChatUI_GetInstantAvatar(contact)+"' width='40' height='40' />");
						sb.push("</div></td>");
						sb.push("<td title='"+TEXT("UI_USER_"+contact.OnlineStatus)+"' style='vertical-align:top;padding:6px 0 0 3px;' width=16 nowrap><img src='"+__cc_urlbase+"Images/"+imgurl+"'/></td>");
						sb.push("<td style='vertical-align:top;padding:6px 0 0 0;'>");
						sb.push(CuteWebUI.HTML.Encode(contact.DisplayName));
						sb.push(videolink);
						if(contact.Description)
							sb.push("<div class='MessengerContactDesc'>"+CuteWebUI.HTML.Encode(contact.Description)+"</div>");
						if((contact.UserName).toLowerCase()!=(contact.DisplayName).toLowerCase())
							sb.push("<div class='MessengerContactUserName'>"+CuteWebUI.HTML.Encode("<"+contact.UserName+">")+"</div>");
						sb.push("</td>");
						sb.push("</tr></table>");
					break;
					case "Small":	
						sb.push("<div style='margin-top:6px;vertical-align:top;'>");
						sb.push("<img style='float:left' title='"+TEXT("UI_USER_"+contact.OnlineStatus)+"' src='"+__cc_urlbase+"Images/"+imgurl+"' align='absMiddle' hspace='4'/>");
						sb.push("<div style='float:left'><span class='MessengerContactUserName'>");
						sb.push(CuteWebUI.HTML.Encode(contact.DisplayName));
						sb.push("</span>");
						sb.push(videolink);
						if(contact.Description)
							sb.push(" - <span class='MessengerContactDesc'>("+CuteWebUI.HTML.Encode(contact.Description)+")</span>");
						sb.push("</div>");
						sb.push("</div>");
					break;
					default:
						sb.push("<table border='0' cellspacing='0' cellpadding='0' align=left width='90%'><tr>");
						sb.push("<td style='vertical-align:top;padding-left:7px;' width=27 nowrap>");
						sb.push("<div style='border:solid 1px #E0DFE5;margin-top:4px;padding:1px;width:25px;' hoverclass='AvatarImgHover'>");
						sb.push("<img class='"+imgcls+"'  title='"+TEXT("UI_USER_"+contact.OnlineStatus)+"' src='"+ChatUI_GetInstantAvatar(contact)+"' width='25' height='25' />");
						sb.push("</div></td>");
						sb.push("<td title='"+TEXT("UI_USER_"+contact.OnlineStatus)+"' style='vertical-align:top;padding:5px 0 0 2px;' width=16 nowrap><img src='"+__cc_urlbase+"Images/"+imgurl+"'/></td>");
						sb.push("<td style='vertical-align:top;padding:5px 0 0 0;'>");
						sb.push(CuteWebUI.HTML.Encode(contact.DisplayName));
						sb.push(videolink);
						if(contact.Description)
							sb.push("<div class='MessengerContactDesc'>"+CuteWebUI.HTML.Encode(contact.Description)+"</div>");
						if((contact.UserName).toLowerCase()!=(contact.DisplayName).toLowerCase())
							sb.push("<div class='MessengerContactUserName'>"+CuteWebUI.HTML.Encode("<"+contact.UserName+">")+"</div>");
						sb.push("</td>");
						sb.push("</tr></table>");
					break;			
				}
				sb.push("</div>");						
			}
		}
	}
	
	cl.innerHTML=sb.join("");
	
}
CuteChatMessenger.prototype.OnMenuClick=function __CuteChatMessenger_OnMenuClick(menuitem)
{
	switch(menuitem.command)
	{
		case "SetOnlineStatus":
			SetOnlineStatus(menuitem.value);
			break;
		case "SignOut":
			Disconnect(false);
			break;
		case "ChangeName":
			var myinfo=GetMyInfo();
			function OnChangeMyName(newname)
			{
				if(newname)
				{
					ChangeDisplayName(newname);
				}
			}
			Desktop.Prompt(OnChangeMyName,"Please specify new display name","Rename",myinfo.DisplayName);
			break;			
		case "SortByStatus":
			SetPrivateProperty("IMSORT","Online");
			break;
		case "SortByGroup":
			SetPrivateProperty("IMSORT","Group");
			break;
		case "ShowSmallIcon":
			SetPrivateProperty("IMICON","Small");
			break;
		case "ShowMediumIcon":
			SetPrivateProperty("IMICON","Medium");
			break;
		case "ShowLargeIcon":
			SetPrivateProperty("IMICON","Large");
			break;
		case "AddNewGroup":
			function HandleNewGroup(group)
			{
				if(!group)return;
				CuteWebUI.Utility.StringTrim(group);
				if(group.length==0)return;
				var customgroupmap=PropStrToObj(GetMyInfo().PrivateProperties["IMGROUPS"])||{};
				if(customgroupmap.hasOwnProperty(group))
				{
					Desktop.Alert(null,"GroupAlreadyExists");
					return;
				}
				customgroupmap[group]="";
				SetPrivateProperty("IMGROUPS",PropObjToStr(customgroupmap));
			}
			Desktop.Prompt(HandleNewGroup,TEXT("TypeGroupName"));
			break;
		case "RenameGroup":
			var groupname=menuitem.groupname;
			function HandleRenameGroup(group)
			{
				if(!group)return;
				CuteWebUI.Utility.StringTrim(group);
				if(group.length==0)return;
				if(group==groupname)return;
				var customgroupmap=PropStrToObj(GetMyInfo().PrivateProperties["IMGROUPS"])||{};
				customgroupmap[group]=customgroupmap[groupname]
				delete customgroupmap[groupname];
				SetPrivateProperty("IMGROUPS",PropObjToStr(customgroupmap));
			}
			Desktop.Prompt(HandleRenameGroup,TEXT("TypeGroupName"),"Rename",groupname);
			break;
		case "DeleteGroup":
			var groupname=menuitem.groupname;
			function HandleDeleteGroup(res)
			{
				if(res)
				{
					var customgroupmap=PropStrToObj(GetMyInfo().PrivateProperties["IMGROUPS"])||{};
					delete customgroupmap[groupname];
					SetPrivateProperty("IMGROUPS",PropObjToStr(customgroupmap));
				}
			}
			Desktop.Confirm(HandleDeleteGroup,TEXT("DeleteGroup")+" "+groupname+" ?");
			break;
		case "MoveToGroup":
			var contact=GetContactById(menuitem.contactid); 
			var groupname=menuitem.groupname;
			if(!contact)break;
			var customgroupmap=PropStrToObj(GetMyInfo().PrivateProperties["IMGROUPS"])||{};
			for(var gname in customgroupmap)
			{
				var cobj=PropStrToObj(customgroupmap[gname])||{};
				delete cobj[contact.UserId];
				customgroupmap[gname]=PropObjToStr(cobj)||"";
			}
			if(groupname&&customgroupmap.hasOwnProperty(groupname))
			{
				var cobj=PropStrToObj(customgroupmap[groupname])||{};
				cobj[contact.UserId]="";
				customgroupmap[groupname]=PropObjToStr(cobj);
			}
			SetPrivateProperty("IMGROUPS",PropObjToStr(customgroupmap));
			break;
		case "ChatWith":
			var contact=GetContactById(menuitem.contactid);
			if(contact)
			{
				this.ActiveConversation(contact);
			}
			break;
		case "ViewHistory":
			var contact=GetContactById(menuitem.contactid);
			if(contact)
			{
				ChatUI_ShowMessageHistory(contact);
			}
			break;
		case "ViewProfile":
			var contact=GetContactById(menuitem.contactid);
			if(contact)
			{
				ChatUI_ShowProfile(contact);
			}
			break;
		case "ToggleBlock":
			var contact=GetContactById(menuitem.contactid);
			if(contact)
			{
				SetBlock( contact , !IsBlock(contact) );
			}
			break;
		case "RemoveContact":
			var contact=GetContactById(menuitem.contactid);
			if(contact)
			{
				RemoveContact(contact);
			}
			break;
	}
}
CuteChatMessenger.prototype.HandleChatEvent=function __CuteChatMessenger_HandleChatEvent(name,type,info1,info2)
{
	if(this._convs)
	{
		for(var i=0;i<this._convs.length;i++)
		{
			this._convs[i].HandleChatEvent(name,type,info1,info2);
		}
	}
	
	if(name=="MESSAGE"&&type=="DOEMOTE")
	{
		if(info2=="BUZZ")
		{
			var msg=info1;
			if(UserEquals(GetMyInfo(),msg.Target))
			{
				if(!this.IsConversationActived(msg.Sender))
				{
					Desktop.Alert(null,msg.Sender.DisplayName+" just sent you a nudge.");
					ChatUI_PlayBuzz();
				}
				return;
			}
			else if(UserEquals(GetMyInfo(),msg.Sender))
			{
				if(!this.IsConversationActived(msg.Target))
				{
					Desktop.Alert(null,"You just sent "+msg.Target.DisplayName+" a nudge.");
				}
				return;
			}
		}
	}
	
	if(name=="IMCOOKIE")
	{
		if(type=="DOUPDATE")
		{
			ChatUI_IMDeclareMainForm();
		}
	}
	
	if(name=="CONNECTION")
	{
		if(IsConnected())
		{
			this._signpanel.style.display="none";
			this._mainpanel.style.display="";
		}
		else
		{
		
			this._signpanel.style.display="";
			this._mainpanel.style.display="none";
			
			if( IsConnecting() )
			{
				CuteWebUI.HTML.FindChild(this._container,"div_signin").style.display='none';
				//CuteWebUI.HTML.FindChild(this._container,"div_cancel").style.display='';
				CuteWebUI.HTML.FindChild(this._container,"img_signin").src=CuteChatUrlBase+"images/imsignin.gif";
			}
			else
			{
				CuteWebUI.HTML.FindChild(this._container,"div_signin").style.display='';
				//CuteWebUI.HTML.FindChild(this._container,"div_cancel").style.display='none';
				CuteWebUI.HTML.FindChild(this._container,"img_signin").src=CuteChatUrlBase+"images/imbody.gif";
			}

		}
	}
	if(name=="MYINFO"&&type=="UPDATED")
	{
		var myinfo=GetMyInfo();
		var myname=myinfo.DisplayName;
		var myDesc=myinfo.Description;
		if(!myDesc)
			myDesc=TEXT("UI_PersonalMessage");
		CuteWebUI.HTML.FindChild(this._container,"ccm_MyName").innerHTML=CuteWebUI.HTML.Encode(myname)+" (<span style='font:normal 11px Tahoma;'>"+TEXT("UI_MENU_"+myinfo.OnlineStatus)+")</span>";
		CuteWebUI.HTML.FindChild(this._container,"ccm_MyDescription").innerHTML=CuteWebUI.HTML.Encode(myDesc);
		CuteWebUI.HTML.FindChild(this._container,"ccm_MyAvatar").src=ChatUI_GetInstantAvatar(myinfo)
		this.UpdateContactListDelay();
	}
	if(name=="USER")
	{
		this.UpdateContactListDelay();
	}
	if(name=="MESSAGE")
	{
		var msg=info1;
		if(msg.Sender)
		{
			if(msg.Sender && IsBlock(msg.Sender))return;
			
			if(UserEquals(msg.Sender,GetMyInfo()))return;
			
			var contact=GetContactById(msg.Sender.UserId);
			if(contact)
			{
				this.ActiveConversation(contact);
			}
			else
			{
				this.ActiveConversation(msg.Sender);
			}
		}
	}
	if(name=="BROADCAST")
	{
		if(type=="NOTIFYVIDEOSTART")
		{
			var userid=info1[0];
			var videoname=info1[2];
			var contact=GetContactById(userid);
			if(contact)
			{
				this.ActiveConversation(contact);
			}
		}
	}
	if(name=="RAWSTCMSG"&&type=="OPENCONTACT")
	{
		var userid=info1[0]
		var contact=GetContactById(userid);
		if(!contact)
		{
			contact={};
			contact.UserId=info1[0];
			contact.UserName=info1[1];
			contact.DisplayName=info1[1];
			contact.Description="";
			contact.PublicProperties={};
			contact.PrivateProperties={};
			contact.IsContact=false;
		}
		this.ActiveConversation(contact);
	}
}
CuteChatMessenger.Start=function __CuteChatMessenger_Start(fullmode)
{
	if(window._conversationproxy)
	{
		window._conversationproxy.OnLoad();
		return;
	}
	if(window.opener&&!window.opener.closed&&window.opener._last_conversationproxy)
	{
		window.opener._last_conversationproxy.OnLoad();
		return;
	}
			
	if(window.__messenger==null)
		window.__messenger=new CuteChatMessenger(fullmode);
	window.__messenger.Show();
}
CuteChatMessenger.Stop=function __CuteChatMessenger_Stop()
{
	if(window.__messenger)window.__messenger.Dispose();
	if(window.__conversation)
	{
		try
		{
			window.__conversation._messenger.CloseConversation(window.__conversation);
		}
		catch(x)
		{
		}
	}
}
CuteChatMessenger.FindMessenger=function __CuteChatMessenger_FindMessenger(element)
{
	for(;element&&element.nodeType==1;element=element.parentNode)
	{
		if(element.messengerInstance)
			return element.messengerInstance
	}
	return window.__messenger;
}
CuteChatMessenger.BubbleCommand=function __CuteChatMessenger_BubbleCommand(command,element,event)
{
	var messenger=CuteChatMessenger.FindMessenger(element);

	if(command=="ShowMyInfo")
	{
		ChatUI_ShowInstantAvatarDialog();
	}
	if(command=="ShowMyMenu")
	{
		var menu=CreateOldMenuImplementation();
		var menuitem;
		
		menuitem=menu.Add(1,TEXT("UI_MENU_Online"),""+__cc_urlbase+"Images/im_online.png"
			,CuteWebUI.Delegate(messenger,messenger.OnMenuClick),null);
		menuitem.command="SetOnlineStatus";
		menuitem.value="ONLINE";
		
		menuitem=menu.Add(1,TEXT("UI_MENU_Away"),""+__cc_urlbase+"Images/im_away.png"
			,CuteWebUI.Delegate(messenger,messenger.OnMenuClick),null);
		menuitem.command="SetOnlineStatus";
		menuitem.value="AWAY";
		
		menuitem=menu.Add(1,TEXT("UI_MENU_Busy"),""+__cc_urlbase+"Images/im_busy.png"
			,CuteWebUI.Delegate(messenger,messenger.OnMenuClick),null);
		menuitem.command="SetOnlineStatus";
		menuitem.value="BUSY";
		
		menuitem=menu.Add(1,TEXT("UI_MENU_AppearOffline"),""+__cc_urlbase+"Images/im_invisible.png"
			,CuteWebUI.Delegate(messenger,messenger.OnMenuClick),null);
		menuitem.command="SetOnlineStatus";
		menuitem.value="APPEAROFFLINE";
		
		menu.AddSpliter();
		
		menuitem=menu.Add(1,TEXT("Sign Out"),""+__cc_urlbase+"Images/im_offline.png"
			,CuteWebUI.Delegate(messenger,messenger.OnMenuClick),null);
		menuitem.command="SignOut";
		
		if(AllowChangeName)
		{
			menuitem=menu.Add(1,TEXT("UI_ChangeName"),""+__cc_urlbase+"Images/im_offline.png"
			,CuteWebUI.Delegate(messenger,messenger.OnMenuClick),null);
			menuitem.command="ChangeName";
		}
		
		menu.Show(element,0,element.offsetHeight);
	}
	if(command=="EditMyDesc")
	{
		var edesc=CuteWebUI.HTML.FindChild(messenger._container,"ccm_MyDescription");
		if(edesc._isediting)return;
		var input=document.createElement("INPUT");
		input.type="text";
		edesc.innerHTML="";
		edesc.appendChild(input);
		input.style.height=edesc.clientHeight+"px";
		input.style.width=edesc.clientWidth-4+"px";
		input.style.border="none";
		input.style.marginLeft="-10px";
		input.value=GetMyInfo().Description
		input.focus();
		input.select();
		edesc._isediting=true;
		input.onkeydown=function()
		{
			if(event.keyCode==13)
				input.onblur();
			if(event.keyCode==27)
			{
				input.onblur=null;
				input.onkeydown=null;
				edesc.innerHTML=CuteWebUI.HTML.Encode(GetMyInfo().Description)
				edesc._isediting=false;
			}
		}
		input.onblur=function()
		{
			input.onblur=null;
			input.onkeydown=null;
			edesc.innerHTML=CuteWebUI.HTML.Encode(input.value)
			SetDescription(input.value);
			edesc._isediting=false;
		}
	}
	if(command=="ShowAddFriend")
	{
		ChatUI_ShowAddContact();
	}
	if(command=="ShoSizeMenu")
	{
		var menu=CreateOldMenuImplementation();
		var menuitem;
		
		menuitem=menu.Add(1,TEXT("ShowSmallIcon"),null
			,CuteWebUI.Delegate(messenger,messenger.OnMenuClick),null);
		menuitem.command="ShowSmallIcon";
		
		menuitem=menu.Add(1,TEXT("ShowMediumIcon"),null
			,CuteWebUI.Delegate(messenger,messenger.OnMenuClick),null);
		menuitem.command="ShowMediumIcon";
		
		menuitem=menu.Add(1,TEXT("ShowLargeIcon"),null
			,CuteWebUI.Delegate(messenger,messenger.OnMenuClick),null);
		menuitem.command="ShowLargeIcon";
		
		menu.Show(element,0,element.offsetHeight);
	}
	if(command=="ShowSortMenu")
	{
		var menu=CreateOldMenuImplementation();
		var menuitem;
		
		menuitem=menu.Add(1,TEXT("SortByStatus"),null
			,CuteWebUI.Delegate(messenger,messenger.OnMenuClick),null);
		menuitem.command="SortByStatus";
		menuitem=menu.Add(1,TEXT("SortByGroup"),null
			,CuteWebUI.Delegate(messenger,messenger.OnMenuClick),null);
		menuitem.command="SortByGroup";
		
		menu.Show(element,0,element.offsetHeight);
	}
	if(command=="FilterUser")
	{
		messenger._filterword=CuteWebUI.Utility.StringTrim(element.value);
		messenger.UpdateContactList();
	}
	if(command=="ShowHistory")
	{
		ChatUI_ShowMessageHistory();
	}
	if(command=="ToggleSound")
	{
		var enabled=!ChatUI_GetEnableSound();
		ChatUI_SetEnableSound(enabled);
		element.innerHTML='<img src="'+__cc_urlbase+"Images/"+(enabled?'sound_on.png':'sound_off.png')+'" />'
		element.title=TEXT( enabled ?"UI_DisableSound":"UI_EnableSound" );
	}
	if(command=="ClickGroup")
	{
		var groupname=element.getAttribute("group");
		messenger["_collapsed_"+groupname]=!messenger["_collapsed_"+groupname];
		messenger._selectedline="group:"+groupname;
		messenger.UpdateContactList();
	}
	if(command=="AddNewGroup")
	{
		function HandleNewGroup(group)
		{
			if(!group)return;
			CuteWebUI.Utility.StringTrim(group);
			if(group.length==0)return;
			var customgroupmap=PropStrToObj(GetMyInfo().PrivateProperties["IMGROUPS"])||{};
			if(customgroupmap.hasOwnProperty(group))
			{
				Desktop.Alert(null,TEXT("GroupAlreadyExists"));
				return;
			}
			customgroupmap[group]="";
			SetPrivateProperty("IMGROUPS",PropObjToStr(customgroupmap));
			var sortgroup=GetMyInfo().PrivateProperties["IMSORT"]=="Group";
			if(!sortgroup)
			{
				function HandleNewGroup_SortByGroup(yes)
				{
					if(!yes)return;
					SetPrivateProperty("IMSORT","Group");
				}
	//			Desktop.Confirm(HandleNewGroup_SortByGroup,TEXT("DoYouWantToSortByGroup"),TEXT("AddNewGroup"));
				HandleNewGroup_SortByGroup(true);
			}
		}
		Desktop.Prompt(HandleNewGroup,TEXT("TypeGroupName"),TEXT("AddNewGroup"));
	}
	if(command=="ShowGroupMenu")
	{
		var groupname=element.getAttribute("group");
		
		var menu=CreateOldMenuImplementation();
		var menuitem;
		
		menuitem=menu.Add(1,TEXT("AddNewGroup"),""+__cc_urlbase+"Images/add_group.png"
			,CuteWebUI.Delegate(messenger,messenger.OnMenuClick),null);
		menuitem.command="AddNewGroup";

		menuitem=menu.Add(1,TEXT("RenameGroup"),""+__cc_urlbase+"Images/rename_group.png"
			,CuteWebUI.Delegate(messenger,messenger.OnMenuClick),null);
		menuitem.command="RenameGroup";
		menuitem.groupname=groupname;
		
		menuitem=menu.Add(1,TEXT("DeleteGroup"),""+__cc_urlbase+"Images/delete_group.png"
			,CuteWebUI.Delegate(messenger,messenger.OnMenuClick),null);
		menuitem.command="DeleteGroup";
		menuitem.groupname=groupname;

		if(event)
			menu.Show(document.body,event.clientX+Html_GetDocumentScroll().left,event.clientY+Html_GetDocumentScroll().top);
		else
			menu.Show(element,48,element.offsetHeight);
	}
	if(command=="ClickContact")
	{
		var contactid=element.getAttribute("contact");
		if(messenger._selectedline!="contact:"+contactid)
		{
			messenger._selectedline="contact:"+contactid;
			messenger.UpdateContactList();
		}
		else if(messenger._clickcontactTime)
		{
			if(new Date().getTime()-messenger._clickcontactTime<500)
			{
				var contact=GetContactById(contactid);
				messenger.ActiveConversation(contact);
			}
		}
		messenger._clickcontactTime=new Date().getTime();
	}
	if(command=="EnterContact")
	{
		var contactid=element.getAttribute("contact");
		clearTimeout(messenger._contactfloatpaneltimerid);
		if(messenger._contactfloatpanel!=element)
		{
			if(messenger._contactfloatpanel)
			{
				document.body.removeChild(messenger._contactfloatpanel);
				messenger._contactfloatpanel=null;
			}
			messenger._contactfloatpaneltimerid=setTimeout(function(){
				var contact=GetContactById(contactid);
				if(!contact)return;
				var panel=document.createElement("DIV");
				panel.className="ContactFloatPanel";
				panel.style.zIndex=666666;
				document.body.appendChild(panel);
				panel.setAttribute("contact",contactid);
				panel.setAttribute("onhoverenter",element.getAttribute("onhoverenter"));
				panel.setAttribute("onhoverleave",element.getAttribute("onhoverleave"));
						
				var titleimg="im_online.png";
				if(!contact.IsOnline)
				{
					titleimg="im_offline.png";
				}
				else if(contact.OnlineStatus=="AWAY")
				{
					titleimg="im_away.png";
				}
				else if(contact.OnlineStatus=="PARTIAL")
				{
					titleimg="im_away.png";
				}
				else if(contact.OnlineStatus=="BUSY")
				{
					titleimg="im_busy.png";
				}

				var titlehtml="<img width='16' heigh='16' src='"+__cc_urlbase+"Images/"+titleimg+"' align='absMiddle' border=0 style='float:left'/> <div style='float:left'>"+CuteWebUI.HTML.Encode(contact.DisplayName)
				if((contact.UserName).toLowerCase()!=(contact.DisplayName).toLowerCase())
					titlehtml=titlehtml+" "+CuteWebUI.HTML.Encode(" <"+contact.UserName+">");	
				if(contact.Description)
					titlehtml=titlehtml+"<br/><span style='color:#6F8A92;font:normal 11px Tahoma;'>("+CuteWebUI.HTML.Encode(contact.Description)+")</span>";
				titlehtml=titlehtml+"</div>"
		
				var sb=[];
				sb.push("<table border='0' cellspacing='0' cellpadding='0'><tr>");
				sb.push("<td rowspan=2 style='vertical-align:top;'><img width=92 height=92 src='"+ChatUI_GetInstantAvatar(contact)+"'/>");
				sb.push("</td>");
				sb.push("<td style='vertical-align:top;padding:2px 10px 0 5px' nowrap>"+titlehtml+"");
				sb.push("</td></tr><tr>");
				sb.push("<td style='vertical-align:bottom;padding:2px 10px 0 8px'>")
				sb.push("<img class='ContactFloatPanelButton' id='ccm_fpchat' src='"+__cc_urlbase+"Images/chat.gif' title='"+TEXT("UI_MENU_ChatWith")+"' />");
				sb.push("<img class='ContactFloatPanelButton' id='ccm_fphistory' src='"+__cc_urlbase+"Images/notebook.png' title='"+TEXT("UI_MessageHistory")+"' />");
				sb.push("<img class='ContactFloatPanelButton' id='ccm_fpprofile' src='"+__cc_urlbase+"Images/profile.gif'  title='"+TEXT("UI_MENU_ViewProfile")+"'/>");
				sb.push("</td>");
				sb.push("</tr></table>");
				panel.innerHTML=sb.join("");
				
				CuteWebUI.HTML.FindChild(panel,"ccm_fpchat").onclick=function()
				{
					messenger.ActiveConversation(contact);
				}
				CuteWebUI.HTML.FindChild(panel,"ccm_fphistory").onclick=function()
				{
					ChatUI_ShowMessageHistory(contact);
				}
				CuteWebUI.HTML.FindChild(panel,"ccm_fpprofile").onclick=function()
				{
					ChatUI_ShowProfile(contact);
				}
				
				var pos;
				try
				{
					pos=CalcPosition(panel,element);
					if(messenger._fullmode)
					{
						pos.top=pos.top+32;
						pos.left=pos.left+72;
					}
					else
					{
						pos.left=pos.left-panel.offsetWidth;
					}
					AdjustMirror(panel,element,pos);
				}
				catch(x)
				{
					document.body.removeChild(panel);
					return;
				}
				panel.style.left=pos.left+"px";
				panel.style.top=pos.top+"px";
				messenger._contactfloatpanel=panel;
			},50);
		}
	}
	if(command=="LeaveContact")
	{
		var contactid=element.getAttribute("contact");
		clearTimeout(messenger._contactfloatpaneltimerid);
		messenger._contactfloatpaneltimerid=setTimeout(function(){
			if(messenger._contactfloatpanel)
			{
				document.body.removeChild(messenger._contactfloatpanel);
				messenger._contactfloatpanel=null;
			}
		},1000);
	}
	if(command=="ShowContactMenu")
	{
		if(messenger._contactfloatpanel)
		{
			document.body.removeChild(messenger._contactfloatpanel);
			messenger._contactfloatpanel=null;
		}
		var contactid=element.getAttribute("contact");
		var sortgroup=GetMyInfo().PrivateProperties["IMSORT"]=="Group";
		var contact=GetContactById(contactid);
		if(!contact)return;
		
		var menu=CreateOldMenuImplementation();
		var menuitem;
		
		//always enabled?: contact.IsOnline?1:0
		menuitem=menu.Add(1,TEXT("UI_MENU_ChatWith"),""+__cc_urlbase+"Images/chat.gif"
			,CuteWebUI.Delegate(messenger,messenger.OnMenuClick),null);
		menuitem.command="ChatWith";
		menuitem.contactid=contactid;	
		
		menuitem=menu.Add(1,TEXT("UI_History"),""+__cc_urlbase+"Images/notebook.png"
			,CuteWebUI.Delegate(messenger,messenger.OnMenuClick),null);
		menuitem.command="ViewHistory";
		menuitem.contactid=contactid;
		
		menuitem=menu.Add(1,TEXT("UI_MENU_ViewProfile"),""+__cc_urlbase+"Images/profile.gif"
			,CuteWebUI.Delegate(messenger,messenger.OnMenuClick),null);
		menuitem.command="ViewProfile";
		menuitem.contactid=contactid;
		
		menu.AddSpliter();
		
		menuitem=menu.Add(1,IsBlock(contact)?TEXT("UI_MENU_UnBlock"):TEXT("UI_MENU_Block"),IsBlock(contact)?""+__cc_urlbase+"Images/im_online.png":""+__cc_urlbase+"Images/im_blocked.png"
			,CuteWebUI.Delegate(messenger,messenger.OnMenuClick),null);
		menuitem.command="ToggleBlock";
		menuitem.contactid=contactid;
		
		menuitem=menu.Add(1,TEXT("UI_MENU_RemoveContact"),""+__cc_urlbase+"Images/remove.gif"
			,CuteWebUI.Delegate(messenger,messenger.OnMenuClick),null);
		menuitem.command="RemoveContact";
		menuitem.contactid=contactid;
	
		

		if(sortgroup)
		{
			var submenu=menu.Add(1,TEXT("MoveToGroup"),""+__cc_urlbase+"Images/rightarrow.png",null,null);
			var customgroupmap=PropStrToObj(GetMyInfo().PrivateProperties["IMGROUPS"])||{};
			
			menuitem=submenu.Add(1,TEXT("Default"),""+__cc_urlbase+"Images/group.png",
					CuteWebUI.Delegate(messenger,messenger.OnMenuClick),null);
			menuitem.command="MoveToGroup";
			menuitem.contactid=contactid;
			menuitem.groupname=null;
			
			for(var gname in customgroupmap)
			{
				menuitem=submenu.Add(1,gname,""+__cc_urlbase+"Images/group.png",
					CuteWebUI.Delegate(messenger,messenger.OnMenuClick),null);
				menuitem.command="MoveToGroup";
				menuitem.contactid=contactid;
				menuitem.groupname=gname;
			}
			
		}
		if(event)
		{
			menu.Show(document.body,event.clientX+Html_GetDocumentScroll().left-2,event.clientY+Html_GetDocumentScroll().top-2);
		}
		else
		{
			menu.Show(element,48,element.offsetHeight);
		}
	}
	if(command=="InviteDialog")
	{
		CuteChatMessenger.ShowInviteDialog(null);
	}
	//alert([command,messenger!=null]);
}

CuteChatMessenger._HandleChatEvent=function __CuteChatMessenger__HandleChatEvent(name,type,info1,info2)
{
	if(window.__messenger)
	{
		window.__messenger.HandleChatEvent(name,type,info1,info2);
	}

	if(name=="CONNECTION")
	{
		var msghtml;
		
		switch(type)
		{
			case "CONNECTING":
				break;
			case "ERROR":
				Desktop.Alert(null,TEXT("UI_CONNECTION_ERROR",info1));
				break;
			case "READY":
				CuteChatMessenger._connectionReadyCount=1+(CuteChatMessenger._connectionReadyCount||0);
				break;
			case "CANCELLED":
				break;
			case "NOTENABLE":
				break;
			case "NEEDLOGIN":
				if(window.__messenger && window.__messenger._fullmode && LogoutCloseMessenger && (CuteChatMessenger._connectionReadyCount||0)>0 )
					window.close();
				break;
			case "NEEDNAME":
				break;
			case "KICK":
				msghtml=TEXT("UI_CONNECTION_Kick");
				break;
			case "LOCKED":
				msghtml=TEXT("UI_CONNECTION_Locked");
				break;
			case "REACHMAX":
				msghtml=TEXT("UI_CONNECTION_ReachMax");
				break;
			case "NEEDPASSWORD":
				msghtml=TEXT("UI_CONNECTION_NeedPassword");
				break;
			case "DISCONNECT":
				//do not need alert this..
				//msghtml=TEXT("UI_CONNECTION_Disconnect");
				break;
			case "KICK":
				msghtml=TEXT("UI_CONNECTION_KICK");
				break;
			case "REJECTED":
				msghtml=TEXT("UI_CONNECTION_REJECTED");
				break;
			case "REMOVED":
				msghtml=TEXT("UI_CONNECTION_REMOVED");
				break;
			case "NOCONNECTION":
				msghtml=TEXT("UI_CONNECTION_NOCONNECTION");
				break;
			default:
				msghtml=TEXT("UI_CONNECTION_"+type);
				break;
		}
		if(msghtml!=null)
		{
			Desktop.Alert(null,msghtml);
		}
	}
}

function CuteChatConversation(messenger,contact,fullmode)
{
	this._messenger=messenger;
	this._contact=contact;
	
	var templateconv=CCM_LoadTemplate("NewMessengerConv.htm");
	
	this._win=new CuteWebUI.HTML.Window({
		onclose:CuteWebUI.Delegate(this,this._OnClose)
		,
		onresize:CuteWebUI.Delegate(this,this._OnResize)
	});
	
	this._container=this._win.GetContentElement();
	this._container.conversationInstance=this;
	
	this._container.innerHTML=templateconv;
	
	this._inputbox=CuteWebUI.HTML.FindChild(this._container,"ccm_InputBox");
	
	if(Html_IsWinIE && GlobalEnableHtmlBox)
	{
		var box=ChatUI_CreateWinIEInputElement();
		box.className="InputBoxElement";
		box.style.cssText=this._inputbox.style.cssText
		this._inputbox.parentNode.insertBefore(box,this._inputbox);
		this._inputbox.style.display="none";
		this._isieinputbox=true;
		this._inputbox=box;
	}
	
	if(!ShowVideoButton)
	{
		CuteWebUI.HTML.FindChild(this._container,"ccm_SendVideo").style.display='none';
	}
	
	this._inputbox.onkeydown=CuteWebUI.Delegate(this,this._OnInputKeydown);
	
	this._inputbox.oncontextmenu=CuteWebUI.Delegate(this,this._OnInputBoxContextMenu);
	
	CuteWebUI.HTML.FindChild(this._container,"ccm_MessageList").oncontextmenu=CuteWebUI.Delegate(this,this._OnMessageListContextMenu);

	this._win.SetWidth(490);
	this._win.SetHeight(380);
	
	this._win.SetTop(Math.max(0,parseInt((document.documentElement.clientHeight-380)*2/3)-Math.floor(Math.random()*110)));
	this._win.SetLeft(Math.max(0,parseInt((document.documentElement.clientWidth-490)*6/7)-Math.floor(Math.random()*110)));
	
	this._win.SetTitle(contact.DisplayName);
	
	if(fullmode)
	{
		document.title=contact.DisplayName;
		this._win.FullWindow();
	}
	
	var arr=GetMessages();
	for(var i=0;i<arr.length;i++)
	{
		this.HandleChatEvent("MESSAGE","NEW",arr[i]);
	}
	this.HandleChatEvent("CONTACT","UPDATED",this._contact);
	this.HandleChatEvent("MYINFO","UPDATED",this._contact);
}
CuteChatConversation.prototype._OnResize=function __CuteChatConversation__OnResize()
{
	var ce=this._win.GetContentElement();
	var cl=CuteWebUI.HTML.FindChild(this._container,"ccm_MessageList");
	if(this._messenger._fullmode)
	{
		try{cl.style.height=(parseInt(ce.style.height)-200)+"px";}catch(x){}
	}
	else
	{
		try{cl.style.height=(parseInt(ce.style.height)-130)+"px";}catch(x){}
	}
	
	var width=130;
	var panel=CuteWebUI.HTML.FindChild(this._container,"ccm_AvatarPanel");
	if(panel.style.display=="none")
		width=25;
	try{cl.style.width=(parseInt(ce.style.width)-width)+"px";}catch(x){}
}

CuteChatConversation.prototype._OnClose=function __CuteChatConversation__OnClose()
{
	this._messenger.CloseConversation(this);
	return true;
}
CuteChatConversation.prototype.SendMessage=function __CuteChatConversation_SendMessage()
{
	var text;
	var html=null;
	if(this._isieinputbox)
	{
		text=CuteWebUI.Utility.StringTrim(this._inputbox.innerText);
		html=this._inputbox.innerHTML;
	}
	else
	{
		text=CuteWebUI.Utility.StringTrim(this._inputbox.value);
	}
	if(text.length>0||(html!=null&&/\<img/i.test(html)))
	{
		SetInstantContact(this._contact);
		var b=GetFontBold();
		var i=GetFontItalic();
		var u=GetFontUnderline();
		SetFontBold(this._isbold);
		SetFontItalic(this._isitalic);
		SetFontUnderline(this._isunderline);
		if(SendMessage(text,html))
		{
			if(!this._sentmsgs)this._sentmsgs=[];
			if(this._sentmsgs.length>19)this._sentmsgs.splice(0,1);
			this._sentmsgs.push({Text:text,Html:html});
			
			this._inputbox.value="";
			this._inputbox.innerHTML="";
		}
		SetFontBold(b);
		SetFontItalic(i);
		SetFontUnderline(u);
	}
}
CuteChatConversation.prototype._OnInputKeydown=function __CuteChatConversation__OnInputKeydown(event)
{
	InstantSetIsTyping(this._contact)

	event=window.event||event;
	if(event.shiftKey)return;
	if(event.keyCode==13)
	{
		this.SendMessage();
		return event.returnValue=false;
	}
}
CuteChatConversation.prototype._OnInputBoxContextMenu=function __CuteChatConversation__OnInputBoxContextMenu(event)
{
	event=window.event||event;
	if(event.preventDefault)event.preventDefault();
	
	var menu=CreateOldMenuImplementation();
	var menuitem;
	
	menuitem=menu.Add(1,TEXT("UI_SEND"),""+__cc_urlbase+"Images/sendmenu.gif"
		,CuteWebUI.Delegate(this,this.OnMenuClick),null);
	menuitem.command="BoxSend";
	
	menuitem=menu.Add(1,TEXT("UI_MENU_Clear"),""+__cc_urlbase+"Images/cleanup.png"
		,CuteWebUI.Delegate(this,this.OnMenuClick),null);
	menuitem.command="BoxClear";
	
	var sentmenu=menu.Add(this._sentmsgs?1:0,TEXT("UI_MENU_SentList"),""+__cc_urlbase+"Images/notebook.png"
		,null,null);
	
	if(this._sentmsgs)
	{
		for(var k=this._sentmsgs.length-1;k>=0;k--)
		{
			var text1=this._sentmsgs[k].Text;
			var text="";
			var utflen=0;
			for(var i=0;i<text1.length;i++)
			{
				text+=text1.charAt(i);
				utflen++;
				if(text1.charCodeAt(i)>256)
					utflen+=2;
				if(utflen>14)
				{
					text+="..";
					break;
				}
			}
			menuitem=sentmenu.Add(1,text,""+__cc_urlbase+"Images/cleanup.png"
				,CuteWebUI.Delegate(this,this.OnMenuClick),null);
			menuitem.command="BoxFill";
			menuitem.message=this._sentmsgs[k];
		}
	}
	
	menu.Show(document.body,document.documentElement.scrollLeft+event.clientX,document.documentElement.scrollTop+event.clientY);
	
	return event.returnValue=false;
}
CuteChatConversation.prototype._OnMessageListContextMenu=function __CuteChatConversation__OnMessageListContextMenu(event)
{
	event=window.event||event;
	if(event.preventDefault)event.preventDefault();
	
	var menu=CreateOldMenuImplementation();
	var menuitem;
	
	menuitem=menu.Add(1,TEXT("UI_MENU_Clear"),""+__cc_urlbase+"Images/cleanup.png"
		,CuteWebUI.Delegate(this,this.OnMenuClick),null);
	menuitem.command="MsgClear";
	
	menuitem=menu.Add(1,TEXT("UI_MENU_ReloadMessages"),""+__cc_urlbase+"Images/refresh.png"
		,CuteWebUI.Delegate(this,this.OnMenuClick),null);
	menuitem.command="MsgReload";
	
	menuitem=menu.Add(1,TEXT("UI_MENU_SaveMessages"),""+__cc_urlbase+"Images/save.png"
		,CuteWebUI.Delegate(this,this.OnMenuClick),null);
	menuitem.command="MsgSave";
	
	menuitem=menu.Add(1,TEXT(this._stopscroll?"UI_MENU_AutoScroll":"UI_MENU_StopScroll"),null
		,CuteWebUI.Delegate(this,this.OnMenuClick),null);
	menuitem.command="MsgScroll";
	
	menu.Show(document.body,document.documentElement.scrollLeft+event.clientX,document.documentElement.scrollTop+event.clientY);
	
	return event.returnValue=false;
}
CuteChatConversation.prototype.OnMenuClick=function __CuteChatConversation_OnMenuClick(menuitem)
{
	var list=CuteWebUI.HTML.FindChild(this._container,"ccm_MessageList");
	switch(menuitem.command)
	{
		case "MsgClear":
			list.innerHTML="";
			break;
		case "MsgReload":
			list.innerHTML="";
			var arr=GetMessages();
			for(var i=0;i<arr.length;i++)this.HandleChatEvent("MESSAGE","RELOAD",arr[i]);
			break;
		case "MsgSave":
			SaveMessages(list);
			break;
		case "MsgScroll":
			this._stopscroll=!this._stopscroll;
			break;
		case "BoxSend":
			this.SendMessage();
			this._inputbox.focus();
			break;
		case "BoxClear":
			if(this._isieinputbox)
				this._inputbox.innerHTML="";
			else
				this._inputbox.value="";
			this._inputbox.focus();
			break;
		case "BoxFill":
			if(this._isieinputbox)
				this._inputbox.innerHTML=menuitem.message.Html;
			else
				this._inputbox.value=menuitem.message.Text;
			this._inputbox.focus();
			break;
	}
}
CuteChatConversation.prototype.Activate=function __CuteChatConversation_Activate()
{
	this._win.Focus();
}
CuteChatConversation.prototype.HandleChatEvent=function __CuteChatConversation_HandleChatEvent(name,type,info1,info2)
{
	if(name=="MESSAGE"&&type=="DOEMOTE")
	{
		if(info2=="BUZZ")
		{
			var msg=info1;
			if(UserEquals(GetMyInfo(),msg.Target))
			{
				if(UserEquals(msg.Sender,this._contact))
				{
					Desktop.Alert(null,msg.Sender.DisplayName+" just sent you a nudge.");
					ChatUI_PlayBuzz();
				}
				return;
			}
			else if(UserEquals(GetMyInfo(),msg.Sender))
			{
				if(UserEquals(msg.Target,this._contact))
				{
					Desktop.Alert(null,"You just sent "+msg.Target.DisplayName+" a nudge.");
				}
				return;
			}
		}
	}

	if(name=="MESSAGE")
	{
		var msg=info1;
		if(msg.Sender&&msg.Target)
		{
			if(
				(UserEquals(msg.Sender,this._contact)&&UserEquals(msg.Target,GetMyInfo()))
				||
				(UserEquals(msg.Target,this._contact)&&UserEquals(msg.Sender,GetMyInfo()))
				)
			{
				if(msg.Type=="EMOTION")return;
				
				var div=document.createElement("DIV");
				ChatUI_AppendMessage(div,msg);
				var list=CuteWebUI.HTML.FindChild(this._container,"ccm_MessageList");
				list.appendChild(div);
				if(!this._stopscroll)list.scrollTop=list.scrollHeight;
			}
		}
	}
	if(name=="CONTACT"||name=="IGNORE")
	{
		if(UserEquals(info1,this._contact))
		{
			this._contact=info1;
			CuteWebUI.HTML.FindChild(this._container,"ccm_ContactAvatar").src=ChatUI_GetInstantAvatar(this._contact);
			
			var titleimg="im_online.png";
			if(!this._contact.IsOnline)
			{
				titleimg="im_offline.png";
			}
			else if(this._contact.OnlineStatus=="AWAY")
			{
				titleimg="im_away.png";
			}
			else if(this._contact.OnlineStatus=="PARTIAL")
			{
				titleimg="im_away.png";
			}
			else if(this._contact.OnlineStatus=="BUSY")
			{
				titleimg="im_busy.png";
			}

			var titlehtml="<img src='"+__cc_urlbase+"Images/"+titleimg+"' align='absMiddle' border=0/>  "+CuteWebUI.HTML.Encode(this._contact.DisplayName)
			if((this._contact.UserName).toLowerCase()!=(this._contact.DisplayName).toLowerCase())
				titlehtml=titlehtml+CuteWebUI.HTML.Encode(" <"+this._contact.UserName+">");	
			if(this._contact.Description)
				titlehtml=titlehtml+"  <span style='color:#6F8A92;font:normal 11px Tahoma;'>("+CuteWebUI.HTML.Encode(this._contact.Description)+")</span>";
			
			this._win.SetTitle(titlehtml,true);
			
			var blocktitle=TEXT("UI_MENU_Block");
			var blockimg="icon-blocked.gif";
			if(!GetContactById(this._contact.UserId))
			{
				blockimg="icon_add.gif";
				blocktitle=TEXT("UI_MENU_AddContact");
			}
			if(IsBlock(this._contact))
			{
				blockimg="icon_unblocked.gif";
				blocktitle=TEXT("UI_MENU_UnBlock");
			}
			CuteWebUI.HTML.FindChild(this._container,"ccm_AddOrBlock").src=__cc_urlbase+"Images/"+blockimg
			CuteWebUI.HTML.FindChild(this._container,"ccm_AddOrBlock").title=blocktitle;
		}
	}
	if(name=="MYINFO")
	{
		CuteWebUI.HTML.FindChild(this._container,"ccm_MyInfoAvatar").src=ChatUI_GetInstantAvatar(GetMyInfo());
	}
	if(name=="UICOMMAND")
	{
		if(this._messenger._currentconv==this)
		{
			if(type=="EMOTION")
			{
				if(this._isieinputbox)
				{
					var range=document.body.createTextRange();
					range.moveToElementText(this._inputbox);
					range.collapse(false);
					range.pasteHTML('<IMG align="absMiddle" src="'+CuteChatUrlBase+'images/emotions/'+info1+'" meaning="'+'[Emotion='+info1+']'+'" border=0 />&nbsp;')	
				}
				else
				{
					this._inputbox.value+="[Emotion="+info1+"]";
				}
			}
			if(type=="FOCUSINPUT")
			{
				this._inputbox.focus();
			}
		}
	}
	if(name=="BROADCAST")
	{
		if(type=="NOTIFYVIDEOALIVE"||type=="NOTIFYVIDEOSTART")
		{
			var userid=info1[0];
			var videoname=info1[2];
			if(userid==this._contact.UserId)
			{
				var img=CuteWebUI.HTML.FindChild(this._container,"ccm_ContactAvatar");
				if(img.style.display!="none")
				{
					var data=ChatUI_RecieveVideo("Messenger",videoname,this._contact.DisplayName,img.parentNode,92,92,function(){
						img.style.display="";
					});
					if(data)
					{
						img.style.display="none";
						this._recievevideoname=videoname;
					}
				}
			}
		}
		if(type=="NOTIFYVIDEOCLOSE")
		{
			var userid=info1[0];
			var videoname=info1[2];
			if(userid==this._contact.UserId)
			{
				if(this._recievevideoname==videoname)
				{
					ChatUI_StopRecieveVideo(this._recievevideoname)
				}
			}
		}
	}
}
CuteChatConversation.prototype._OnInterval=function __CuteChatConversation__OnInterval()
{
	if(this._isusertyping!=IsUserTyping(this._contact))
	{
		this._isusertyping=IsUserTyping(this._contact);
		var typing=CuteWebUI.HTML.FindChild(this._container,"ccm_typing");
		if(this._isusertyping)
			typing.innerHTML=this._contact.DisplayName+" "+TEXT("UI_USER_TYPING");
		else
			typing.innerHTML="";
	}
}
CuteChatConversation.prototype.Dispose=function __CuteChatConversation_Dispose()
{
	this._win.Dispose();
	if(this._recievevideoname)
	{
		ChatUI_StopRecieveVideo(this._recievevideoname)
	}
	ChatUI_StopPublishVideo(this._contact.UserId);
}

CuteChatConversation.FindConversation=function __CuteChatConversation_FindConversation(element)
{
	for(;element&&element.nodeType==1;element=element.parentNode)
	{
		if(element.conversationInstance)
			return element.conversationInstance
	}
}
CuteChatConversation.BubbleCommand=function __CuteChatConversation_BubbleCommand(command,element,event)
{
	var conv=CuteChatConversation.FindConversation(element);
	
	if(command=="SendMessage")
	{
		conv.SendMessage();
		conv._inputbox.focus();
	}
	if(command=="ToggleAvatarPanel")
	{
		var panel=CuteWebUI.HTML.FindChild(conv._container,"ccm_AvatarPanel");
		var image=CuteWebUI.HTML.FindChild(conv._container,"ccm_ToggleButton");
		var baseurl=image.src.substring(0,image.src.lastIndexOf('/'));
		if(panel.style.display=="none")
		{
			image.src=baseurl+"/IM_Side2.png";
			image.title=TEXT("HideDisplayPictures");
			panel.style.display="";
		}
		else
		{
			image.src=baseurl+"/IM_Side1.png";
			image.title=TEXT("ShowDisplayPictures");
			panel.style.display="none";
		}
		conv._OnResize();
	}
	if(command=="AddOrBlock")
	{
		if(GetContactById(conv._contact.UserId))
		{
			SetBlock( conv._contact , !IsBlock(conv._contact) );
		}
		else
		{
			AddContact(conv._contact);
		}
	}
	if(command=="ShowHistory")
	{
		ChatUI_ShowMessageHistory(conv._contact);
	}
	if(command=="ShowEmotion")
	{
		conv._messenger._currentconv=conv;
		ChatUI_ShowEmotionPanel(element);
	}
	if(command=="SendNudge")
	{
		SetInstantContact(conv._contact);
		ChatUI_SendEmotionWithFloodCoontrol("BUZZ");
	}
	if(command=="SendFile")
	{
		ChatUI_InstantShowSendFile(conv._contact);
	}
	if(command=="SendVideo")
	{
		if(!conv._contact.IsOnline || conv._contact.OnlineStatus=="PARTIAL")
		{
			Desktop.Alert(null,TEXT("TargetUserNotOnline"),TEXT("UI_ALERT"));
			return;
		}
		
		var myavatar=CuteWebUI.HTML.FindChild(conv._container,"ccm_MyInfoAvatar");
		var container=myavatar.parentNode;
		if(ChatUI_IsPublishingVideo(conv._contact.UserId))
		{
			ChatUI_StopPublishVideo(conv._contact.UserId);
			myavatar.style.display="";
		}
		else
		{
			ChatUI_PublishVideo("Messenger",conv._contact.UserId,container,100,100,function(){
				myavatar.style.display="";
			});
			myavatar.style.display="none";
		}
	}
	if(command=="ToggleBold")
	{
		conv._isbold=!conv._isbold;
		element.className=conv._isbold?"ConversationInputButtonDown":"ConversationInputButton";
		element.setAttribute("savedclass",element.className);
		conv._inputbox.style.fontWeight=conv._isbold?"bold":"";
	}
	if(command=="ToggleItalic")
	{
		conv._isitalic=!conv._isitalic;
		element.className=conv._isitalic?"ConversationInputButtonDown":"ConversationInputButton";
		element.setAttribute("savedclass",element.className);
		conv._inputbox.style.fontStyle=conv._isbold?"italic":"";
	}
	if(command=="ToggleUnderline")
	{
		conv._isunderline=!conv._isunderline;
		element.className=conv._isunderline?"ConversationInputButtonDown":"ConversationInputButton";
		element.setAttribute("savedclass",element.className);
		conv._inputbox.style.textDecoration=conv._isbold?"underline":"";
	}
	if(command=="InviteDialog")
	{
		if(!conv._contact.IsOnline)
		{
			ChatUI_PlaySound("Alert");
			Desktop.Alert(null,TEXT("TargetUserNotOnline"),TEXT("UI_ALERT"));
			return;
		}
		CuteChatMessenger.ShowInviteDialog(conv._contact);
	}
}

CuteChatMessenger.ShowInviteDialog=function __CuteChatMessenger_ShowInviteDialog(contact)
{
	var win=new CuteWebUI.HTML.Window({zIndex:88888888});
	CuteWebUI.HTML.QueueDialog(win);
	win.SetTitle(TEXT("UI_MENU_Invite"));

	
	var ce=win.GetContentElement();
	
	ce.innerHTML="<div id='cwui_msg'></div><div id='cwui_btns'><button id='cwui_ok'>OK</button>&nbsp;&nbsp;&nbsp;<button id='cwui_cancel'>Cancel</button></div>";
	var msg=CuteWebUI.HTML.FindChild(ce,"cwui_msg");
	var ok=CuteWebUI.HTML.FindChild(ce,"cwui_ok");
	ok.onclick=function()
	{
		win.result=true;
		win.Close();
	}
	var cc=CuteWebUI.HTML.FindChild(ce,"cwui_cancel");
	cc.onclick=function()
	{
		win.Close()
	}
	
	var title=msg.appendChild(document.createElement("DIV"));
	title.innerHTML=TEXT("UI_SelectName");
//	title.style.fontWeight="bold"
	title.style.marginBottom="8px";
	
	var grid=document.createElement("TABLE");
	grid.style.width="180px";
	grid.style.height="50px";
	grid.cellSpacing=0;
	grid.cellPadding=4;

	msg.appendChild(grid);
	
	var count=0;
	var contacts=GetContacts();
	for(var i=0;i<contacts.length;i++)
	{
		var c=contacts[i];
		
		if(!c.IsOnline)continue;//do not show the offline ?
	
		var row=grid.insertRow(-1);
		
		row.contact=c;
		
		var c1=row.insertCell(-1);
		c1.style.verticalAlign="top";
		c1.style.width="24px"
		var cb=document.createElement("input");
		cb.type="checkbox";
		cb.disabled=!c.IsOnline;
		c1.appendChild(cb);
		
		if(contact!=null)
		{
			cb.checked=UserEquals(contact,c);
		}
		
		var c3=row.insertCell(-1);
		c3.style.verticalAlign="top";
		c3.style.width="24px"
		c3.innerHTML="<img src='"+ChatUI_GetInstantAvatar(c)+"' style='width:24px;height:24px;' />";
		
		
		var c2=row.insertCell(-1);
		c2.style.verticalAlign="top";
		c2.style.textAlign="left";
		c2.style.backgroundColor="";
		var lb=document.createElement("label");
		lb.innerHTML=Html_Encode(c.DisplayName);
		c2.appendChild(lb);
		
		
		count++;
	}
	
	win.SetWidth(300);
	win.SetHeight(120+count*20);
	
	win.MoveToScreenCenter();
	
	win.resulthandler=function(result)
	{
		if(!result)return;
		var contacts=[];
		for(var i=0;i<grid.rows.length;i++)
		{
			var row=grid.rows.item(i);
			var cb=row.cells.item(0).firstChild;
			var contact=row.contact;
			if(cb.checked)
			{
				contacts.push(contact)
			}
		}
		if(contacts.length==0)return;
		InvitePrivateChatBatch(contacts);
	};
}

AttachChatEvent("RAWSTCMSG",CuteChatMessenger._HandleChatEvent);
AttachChatEvent("CONNECTION",CuteChatMessenger._HandleChatEvent);
AttachChatEvent("MESSAGE",CuteChatMessenger._HandleChatEvent);
AttachChatEvent("SENDMESSAGE",CuteChatMessenger._HandleChatEvent);
AttachChatEvent("USER",CuteChatMessenger._HandleChatEvent);
AttachChatEvent("PLACE",CuteChatMessenger._HandleChatEvent);
AttachChatEvent("MYINFO",CuteChatMessenger._HandleChatEvent);
AttachChatEvent("ITEM",CuteChatMessenger._HandleChatEvent);
AttachChatEvent("CONTACT",CuteChatMessenger._HandleChatEvent);
AttachChatEvent("IGNORE",CuteChatMessenger._HandleChatEvent);
AttachChatEvent("UICOMMAND",CuteChatMessenger._HandleChatEvent);
AttachChatEvent("BROADCAST",CuteChatMessenger._HandleChatEvent);
AttachChatEvent("IMCOOKIE",CuteChatMessenger._HandleChatEvent);


window.onbeforeunload=function()
{
	var isconn;
	try
	{
		isconn=IsConnected();
	}
	catch(x)
	{
	}
	if(isconn && window.__messenger)
	{
		return TEXT("UI_AreYouSureQuitMessenger");
	}
}


function window_onunload()
{
	Disconnect(true);
	CuteChatMessenger.Stop();
}
window.onunload=window_onunload;
if(window.attachEvent)
{
	window.attachEvent("onunload",window_onunload);
}
else if(window.addEventListener)
{
	window.addEventListener("unload",window_onunload,true);
}


function Frame_GetContentWindow(frame)
{
	if(frame.contentWindow)
		return frame.contentWindow;
	
	if(frame.contentDocument)
	{
		if(frame.contentDocument.parentWindow)
			return frame.contentDocument.parentWindow;
	}
	
	var win;
	if(frame.id)
	{
		win=window.frames[frame.id];
		if(win)return win;
	}
	
	var len=window.frames.length;
	for(var i=0;i<len;i++)
	{
		win=window.frames[i];
		if(win.frameElement==frame)
			return win;
		if(win.document==frame.contentDocument)
			return win;
	}
	
	throw(new Error("iframe window not found!"));
}

function OpenWindowWaitReturn2(handler,url,name,option)
{
	if(handler!=null)CuteWebUI.HTML.ShowDialogMask();
	
	var win;
	var frame;
	var ce;
	var _topclose=top.close;
	
	function _onclose(arg1,arg)
	{
		var fwin=Frame_GetContentWindow(frame);
		
		try{
		fwin.location.href="about:blank";
		//frame.close();	
		}catch(x){}
		
		if(handler==null)return;

		top.close=_topclose;
		CuteWebUI.HTML.HideDialogMask();

		var res;
		try
		{
			res=fwin.returnValue||top.returnValue;
		}
		catch(x)
		{
		}
		if(handler)
		{
			handler(res);
		}
	}
	function _onresize()
	{
		
	}
	
	win=new CuteWebUI.HTML.Window({
		onclose:_onclose
		,
		onresize:_onresize
		,
		zIndex:handler!=null?80000000:0
	});
	win.Show();
	frame=document.createElement("IFrame");
	frame.style.width="100%";
	frame.style.height="100%";
	if(name)frame.name=name;
	frame.setAttribute('frameBorder', 'no');
	
	ce=win.GetContentElement()
	ce.appendChild(frame);
	setTimeout(function(){frame.src=url;},100);
	
	var optionmap={};
	if(option)
	{
		var items=option.split(",")
		for(var i=0;i<items.length;i++)
		{
			var pair=items[i].split("=")
			optionmap[pair[0]]=pair[1]
		}
	}
	
	var cw=window.document.body.clientWidth;
	var ch=window.document.body.clientHeight;

	if(window.document.compatMode=='CSS1Compat')
	{
		cw=window.document.documentElement.clientWidth;
		ch=window.document.documentElement.clientHeight;
	}
	
	if(optionmap["width"])
	{
		win.SetWidth(Math.min(cw-24,parseInt(optionmap["width"])||100+12));
	}
	if(optionmap["height"])
	{
		win.SetHeight(parseInt(optionmap["height"])||100+32);
	}
	
	_onresize();
	
	win.MoveToScreenCenter();
	
	
	top.close=top.cc_close=function()
	{
		win.Close();
	}

	
	function updateTitle(){
		try
		{
			var fwin=Frame_GetContentWindow(frame);
			win.SetTitle(fwin.document.title);
		}
		catch(x)
		{
		}
	}
	
	setTimeout(updateTitle,100);
	setTimeout(updateTitle,200);
	setTimeout(updateTitle,500);
	setTimeout(updateTitle,1000);
	setTimeout(updateTitle,2000);
	setTimeout(updateTitle,5000);
}
function OpenWindowAsync2(handler,url,name,option)
{
	OpenWindowWaitReturn(handler,url,name,option);
}


var OpenWindowWaitReturn1=OpenWindowWaitReturn;
var OpenWindowAsync1=OpenWindowAsync;

OpenWindowWaitReturn=function(handler,url,name,option)
{
	if(window.__messenger&&window.__messenger._fullmode)
		return OpenWindowWaitReturn1(handler,url,name,option);
	OpenWindowWaitReturn2(handler,url,name,option);
}
OpenWindowAsync=function(handler,url,name,option)
{
	if(window.__messenger&&window.__messenger._fullmode)
		return OpenWindowAsync1(handler,url,name,option);
	OpenWindowAsync2(handler,url,name,option);
}



