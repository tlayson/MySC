var mmgdir="../../MouseMoveGame/";
var mmg={};
mmg.UserMap={};

function MMG_Start(panel)
{
	if(panel==null)throw(new Error("argument panel is null."));
	if(mmg.panel)throw(new Error("already started!"));
	mmg.panel=panel;
	mmg.width=panel.offsetWidth;
	mmg.height=panel.offsetHeight;
	
	AttachChatEvent("USER",MMG_OnUserEvent);
	AttachChatEvent("MYINFO",MMG_OnMyInfoEvent);
	AttachChatEvent("CONNECTION",MMG_OnConnection);
	AttachChatEvent("MESSAGE",MMG_OnMessage);
	
	MMG_OnConnection("CONNECTION","READY");
	
	setInterval(MMG_ContinueWork,30);
	panel.onclick=MMG_OnPanelClick;
	//panel.oncontextmenu=MessageList_OnContextMenu
	panel.oncontextmenu=function(event)
	{
		event=window.event||event;
		ChatUI_ShowMyInfoMenu(GetMyInfo(),document.documentElement.scrollLeft+event.clientX,document.documentElement.scrollTop+event.clientY);
		event.cancelBubble=true;
		if(event.preventDefault)event.preventDefault();
		return event.returnValue=false;
	}
}

function MMG_OnConnection(msg,type)
{
	if(type=="READY")
	{
		for(var p in mmg.UserMap)
		{
			var data=mmg.UserMap[p];
			if(data&&data.user)
			{
				MMG_HideUser(data.user);
			}
		}
		var users=GetUsers();
		for(var i=0;i<users.length;i++)
		{
			MMG_ShowUser(users[i]);
		}
	}
}
function MMG_OnMyInfoEvent()
{
	var info=GetMyInfo();
	if(!info.PublicProperties.Location)
	{
		SetPublicProperty("Location","0.5,0.5");
	}
}
function MMG_OnUserEvent(msg,type,info1,info2)
{
	if(type=="ADDED"||type=="UPDATED")
		MMG_ShowUser(info1);
	if(type=="REMOVED")
		MMG_HideUser(info1);
}
function MMG_OnMessage(evt,type,msg)
{
	if(msg.Sender && !msg.IsHistory)
	{
		var user=GetUserByGuid(msg.Sender.Guid)
		if(!user)return;
		if(IsBlock(user))return;
		var data=mmg.UserMap[user.UserId];
		if(!data)return;
		MMG_ShowBubble(data,msg);
	}
}

function MMG_ShowUser(user)
{
	var pos=null;
	if(user.PublicProperties.Location)
	{
		var loc=String(user.PublicProperties.Location).split(',');
		if(loc.length==2)
		{
			loc={x:parseFloat(loc[0]),y:parseFloat(loc[1])}
			if(loc.x&&loc.x>0&&loc.x<1 && loc.y&&loc.y>0&&loc.y<1)
			{
				pos={x:parseInt(mmg.width*loc.x),y:parseInt(mmg.height*loc.y)};
			}
		}
	}
	if(!pos)
	{
		pos={x:parseInt(mmg.width/2),y:parseInt(mmg.height/2)}
		//MMG_HideUser(user);
		//return;
	}

	var data=mmg.UserMap[user.UserId];
	if(data==null)
	{
		//create
		var data={};
		data.user=user;
		data.x=pos.x;
		data.y=pos.y;
		data.currentX=data.x;
		data.currentY=data.y;
		
		data.panel=document.createElement("DIV");
		data.panel.style.position="absolute";
		
		data.toptable=document.createElement("TABLE");
		var row=data.toptable.insertRow(-1)
		
		data.label=document.createElement("DIV");
		if(CuteWebUI.HTML.IsWinIE)
			data.label.style.cssText="filter: glow(color=#FF1122,strength=4); font:normal 12px arial;color:yellow;height:12px;width:1px;";
		else if(CuteWebUI.HTML.IsOpera||CuteWebUI.HTML.IsGeckoSafari)
			data.label.style.cssText="text-shadow:2px 1px 2px #c00000;font:normal 12px arial; color:yellow;";
		else	
			data.label.style.cssText="color:Orange;font:bold 10px verdana;";
			

		data.cameraimage=document.createElement("IMG");
		data.cameraimage.src=CuteChatUrlBase+"Images/camera.png";
		data.cameraimage.style.width="16px";
		data.cameraimage.style.height="16px";
		
		data.statusimage=document.createElement("IMG");
		data.statusimage.style.width="16px";
		data.statusimage.style.height="16px";
		
		row.insertCell(-1).appendChild(data.statusimage);
		row.insertCell(-1).appendChild(data.label);
		row.insertCell(-1).appendChild(data.cameraimage);
		row.cells.item(1).style.textAlign="center";
		
		data.imagecell=data.toptable.insertRow(-1).insertCell(-1);
		data.imagecell.colSpan=3;
		
		data.image=document.createElement("DIV");
		data.image.style.width="32px";
		data.image.style.height="52px";
		data.image.style.backgroundPosition="-16px -6px";
		
		data.imagecell.appendChild(data.image);
		
		data.panel.appendChild(data.toptable);
		
		mmg.panel.appendChild(data.panel);
		
		
		if(UserEquals(user,GetMyInfo()))
		{
	//		data.label.style.color="red";
		}
	}
	
	data.label.innerHTML=Html_Encode(user.DisplayName);	

	var imgurl=CuteChatUrlBase+"AvatarChat/a"+(user.PublicProperties.AvatarCharacter||1)+".png";//ChatUI_GetInstantAvatar(user);
	if(data.imgurl!=imgurl)
	{
		data.imgurl=imgurl
		data.image.style.backgroundImage="url("+imgurl+")";
	}

	if(UserEquals(GetSelectedUser(),user))
	{
		data.toptable.style.borderBottom="dashed 2px blue";
	}
	else
	{
		data.toptable.style.borderBottom="";
	}
	
	if(data.laststatus!=user.OnlineStatus)
	{
		if(IsUserTyping(user)||user.OnlineStatus!="ONLINE")
		{
			data.statusimage.style.visibility="";
		}
		else
		{
			data.statusimage.style.visibility="hidden";
		}
		if(IsUserTyping(user))
		{
			data.statusimage.src=CuteChatUrlBase+"Images/keyboard.png";
		}
		else if(user.OnlineStatus=="AWAY")
		{
			data.statusimage.src=CuteChatUrlBase+"Images/im_away.png";
		}
		else if(user.OnlineStatus=="PARTIAL")
		{
			data.statusimage.src=CuteChatUrlBase+"Images/im_away.png";
		}
		else if(user.OnlineStatus=="BUSY")
		{
			data.statusimage.src=CuteChatUrlBase+"Images/im_busy.png";
		}
	}
	
	data.SetImageBackPosition=function(x,y)
	{
		data.image.style.backgroundPosition="-"+x+"px -"+y+"px";
	}
	
	if(mmg.UserMap[user.UserId]==null)
	{
		mmg.UserMap[user.UserId]=data;
		MMG_SetImagePosition(data);
	}
	else
	{
		data.user=user;
		
		data.x=pos.x;
		data.y=pos.y;
	}
	
	var vt=GetItemInfo(user.Guid,"VideoTime")||0;
	if( new Date().getTime() - vt < 10000 )
	{
		data.cameraimage.style.visibility="";
		
		if(!UserEquals(user,GetMyInfo()))
		{
			data.cameraimage.onclick=function()
			{
				ChatUI_ShowRecieveVideoWindow(GetPlace().Name,GetItemInfo(user.Guid,"VideoName"),user.DisplayName)
			}
		}
	}
	else
	{
		data.cameraimage.style.visibility="hidden";
	}
	
	
	data.imagecell.style.paddingLeft=data.imagecell.style.paddingRight=parseInt((data.panel.offsetWidth-36)/2)+"px";
	
	data.panel.onclick=function(event)
	{
		event=window.event||event
		event.cancelBubble=true;
		if(!UserEquals(user,GetMyInfo()))
		{
			SetSelectedUser(user);
		}
	}
	data.panel.oncontextmenu=function(event)
	{
		event=window.event||event
		if(UserEquals(GetMyInfo(),data.user))
		{
			ChatUI_ShowMyInfoMenu(GetMyInfo(),document.documentElement.scrollLeft+event.clientX,document.documentElement.scrollTop+event.clientY);
		}
		else
		{
			ChatUI_ShowUserMenu(data.user,document.documentElement.scrollLeft+event.clientX,document.documentElement.scrollTop+event.clientY);
		}
		
		event.cancelBubble=true;
		if(event.preventDefault)event.preventDefault();
		return event.returnValue=false;
	}
	
}
function MMG_HideUser(user)
{
	var data=mmg.UserMap[user.UserId];
	if(data==null)return;
	mmg.UserMap[user.UserId]=null;
	mmg.panel.removeChild(data.panel);
	if(data.bubblepanel)
	{
		mmg.panel.removeChild(data.bubblepanel);
	}
}

function MMG_ShowBubble(data,msg)
{
	var text=msg.Text;
	if(data.bubblepanel)
	{
		mmg.panel.removeChild(data.bubblepanel);
	}
	
	var centerwidth=80;
	var centerheight=20;
	if(text.length>12)
	{
		var larger=text.length/12;
		centerwidth=Math.floor(centerwidth*Math.pow(larger,3/5));
		centerheight=Math.floor(centerheight*Math.pow(larger,2/5));
	}
	
	var p=document.createElement("TABLE");
	p.style.position="absolute";
	p.style.width=(centerwidth+24)+"px";
	p.style.height=(centerheight+52)+"px";
	p.style.left="0px";
	p.style.top="0px";
	p.cellSpacing=0;
	p.cellPadding=0;
	p.insertRow(-1);
	p.insertRow(-1);
	p.insertRow(-1);
	
	Html_SetCssText(p.rows[0].insertCell(-1),"background:url(Images/bubble1.png?v7);width:12px;height:12px;");
	Html_SetCssText(p.rows[0].insertCell(-1),"background:url(Images/bubble1.png?v7) top;width:"+centerwidth+"px;height:12px;");
	Html_SetCssText(p.rows[0].insertCell(-1),"background:url(Images/bubble1.png?v7) top right;width:12px;height:12px;");
	Html_SetCssText(p.rows[1].insertCell(-1),"background:url(Images/bubble1.png?v7) left;width:12px;height:"+centerheight+";");
	Html_SetCssText(p.rows[1].insertCell(-1),"background:url(Images/bubble1.png?v7) center;width:"+centerwidth+"px;height:"+centerheight+";color:red;");
	Html_SetCssText(p.rows[1].insertCell(-1),"background:url(Images/bubble1.png?v7) right;width:12px;height:"+centerheight+";");
	
	Html_SetCssText(p.rows[2].insertCell(-1),"background:url(Images/bubble1.png?v7) left bottom;width:12px;height:40px;");
	Html_SetCssText(p.rows[2].insertCell(-1),"background:url(Images/bubble1.png?v7) -28px bottom;width:"+centerwidth+"px;height:40px;");
	Html_SetCssText(p.rows[2].insertCell(-1),"background:url(Images/bubble1.png?v7) right bottom;width:12px;height:40px;");
	
	var ctd=p.rows[1].cells[1];
	ctd.wrap="wrap";
	ctd.style.wordWrap="break-word";

	if(msg.Html)
		ctd.innerHTML=ChatUI_TranslateHtml(msg.Html);
	else
		ctd.innerHTML=ChatUI_TranslateText(msg.Text);
	
	if(msg.Target)
	{
		//var tspan=document.createElement("SPAN");
		//ChatUI_AppendUser(tspan,msg.Target);
		//ctd.insertBefore(tspan,ctd.firstChild);
	}

	mmg.panel.appendChild(p);
	data.bubblepanel=p;
	data.bubbletime=new Date().getTime();
	
	MMG_SetImagePosition(data)
}

function MMG_SetImagePosition(data)
{

	var width=data.panel.offsetWidth;
	var height=data.panel.offsetHeight;
	
	var w=mmg.width-width;
	var h=mmg.height-height;
	var x=Math.min(w,data.currentX)-width/2;
	var y=Math.min(h,data.currentY)-height/2;
	x=Math.max(width/2,x);
	y=Math.max(height/2,y);
	data.panel.style.left=x+"px";
	data.panel.style.top=y+"px";
	
	if(data.bubblepanel)
	{
		data.bubblepanel.style.left=(x+5)+"px";
		data.bubblepanel.style.top=(y-data.bubblepanel.offsetHeight+5)+"px";
	}
	
}

function MMG_ContinueWork()
{
	for(var userid in mmg.UserMap)
	{
		var data=mmg.UserMap[userid];
		if(data&&data.user.UserId==userid)
		{
			MMG_ContinueUserWork(data);
		}
	}
}
function MMG_ContinueUserWork(data)
{
	if(data.bubblepanel)
	{
		var span=new Date().getTime()-data.bubbletime;
		if(span>12000)
		{
			mmg.panel.removeChild(data.bubblepanel);
			data.bubblepanel=null;
		}
	}

	if(data.currentX==data.x&&data.currentY==data.y)
	{
		data.SetImageBackPosition(16,6+64*(data._lastface||0));
		//data.SetImageBackPosition(16,4);
	}
	else
	{
		
		var w=data.currentX-data.x
		var h=data.currentY-data.y;
		var d=Math.sqrt(w*w+h*h);
		var s=3;
		var x=s*w/d;
		var y=s*h/d;
		x=data.currentX-x;
		y=data.currentY-y;
		if( data.x>Math.min(x,data.currentX) && data.x<Math.max(x,data.currentX) )
			data.currentX=data.x;
		else
			data.currentX=x;
		if( data.y>Math.min(y,data.currentY) && data.y<Math.max(y,data.currentY) )
			data.currentY=data.y;
		else
			data.currentY=y;
		
		var face=0;
		
		var asin=Math.abs(Math.asin(h/d));
		var is45=false;
		if(asin>0.2&&asin<0.8)
			is45=true;
		
		if(h<0)
		{
			if(Math.abs(h)>Math.abs(w))
			{
				face=0;
			}
			else if(w>0)
			{
				if(is45)
					face=4
				else
					face=1;
			}
			else
			{
				if(is45)
					face=5
				else
					face=2;
			}
		}
		else
		{
			if(Math.abs(h)>Math.abs(w))
			{
				face=3;
			}
			else if(w>0)
			{
				if(is45)
					face=6
				else
					face=1;
			}
			else
			{
				if(is45)
					face=7
				else
					face=2;
			}
		}
		
		var step=parseInt(new Date().getTime()/200)%4;
		data.SetImageBackPosition(16+64*step,6+64*face);
		data._lastface=face;

		MMG_SetImagePosition(data);
	}
}

function MMG_OnPanelClick(event)
{
	event=window.event||event;
	var pos=GetClientPosition(mmg.panel);
	
	SetPublicProperty("Location",(event.clientX-pos.left)/mmg.width+","+(event.clientY-pos.top)/mmg.height);
}
















