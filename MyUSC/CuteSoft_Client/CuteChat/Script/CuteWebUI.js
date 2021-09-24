/********************************\
    CuteWebUI Script Library
    Copyrights CuteSoft 2008
\********************************/


var CuteWebUI={};
window.CuteWebUI=CuteWebUI;

CuteWebUI.Version=1.0;

//CuteWebUI.Resource="/CuteSoft_Client/CuteWebUI/";

CuteWebUI.Delegate=function __CuteWebUI_Delegate(thisobj,method)
{
	return function Function()
	{
		return method.apply(thisobj,arguments);
	}
}

//-Debug
CuteWebUI.Debug={};

CuteWebUI.Debug.Enabled=false;

//.Assert
CuteWebUI.Debug.Assert=function __CuteWebUI_Debug_Assert(condition,message)
{
	if(!CuteWebUI.Debug.Enabled)return;
	if(!condition)throw(new Error(message));
}

//.AssertNotNull
CuteWebUI.Debug.AssertNotNull=function __CuteWebUI_Debug_AssertNotNull(val,name)
{
	if(!CuteWebUI.Debug.Enabled)return;
	if(val==null)throw(new Error("Variable '"+name+"' is null!"));
}

//.Print
CuteWebUI.Debug.Print=function __CuteWebUI_Debug_Print(message)
{
	if(!CuteWebUI.Debug.Enabled)return;
	//TODO:show the message in a float panel
}

//-Utility
CuteWebUI.Utility={}

//.StringTrim
CuteWebUI.Utility.StringTrim=function __CuteWebUI_Utility_StringTrim(str)
{
	CuteWebUI.Debug.AssertNotNull(str,"str");
	return str.replace(/^\s+/,"").replace(/\s+$/,"");
}

//-Ajax
CuteWebUI.Ajax={};
//.CreateXMLHttp
CuteWebUI.Ajax.CreateXMLHttp=function __CuteWebUI_Ajax_CreateXMLHttp()
{
	if(typeof(XMLHttpRequest)!="undefined")
		return new XMLHttpRequest();
	return new ActiveXObject("Microsoft.XMLHTTP")
}
CuteWebUI.Ajax.AsyncGet=function __CuteWebUI_Ajax_AsyncLoad(url,callback,onerror)
{
	var xh=CuteWebUI.Ajax.CreateXMLHttp();
	xh.open("GET",url,true,null,null);
	xh.onreadystatechange=function()
	{
		if(xh.readyState<4)return;
		xh.onreadystatechange=new Function();
		callback(xh);
	}
	xh.send("");
}

//-HTML
CuteWebUI.HTML={}

CuteWebUI.HTML._Initialize=function __CuteWebUI_HTML__Initialize()
{
	var Html_Browser;
	var Html_UserAgent=window.navigator.userAgent;

	if(Html_UserAgent.indexOf("Gecko")!=-1)
	{
		Html_Browser="Gecko";
	}
	else if(Html_UserAgent.indexOf("Opera")!=-1)
	{
		Html_Browser="Opera";
	}
	else
	{
		Html_Browser="IE";
	}

	CuteWebUI.HTML.Browser=Html_Browser;
	CuteWebUI.HTML.IsWinIE=(Html_Browser=="IE");
	CuteWebUI.HTML.IsGecko=(Html_Browser=="Gecko");
	CuteWebUI.HTML.IsOpera=(Html_Browser=="Opera");
	CuteWebUI.HTML.IsGeckoSafari= CuteWebUI.HTML.IsGecko && Html_UserAgent.indexOf("Safari")!=-1;
}

CuteWebUI.HTML._Initialize();



CuteWebUI.HTML.Encode=function __CuteWebUI_HTML_Encode(str)
{
	if(str==null)return "";
	return str.replace(/&/g,"&amp;").replace(/</g,"&lt;").replace(/>/g,"&gt;").replace(/\x22/g,"&quot;").replace(/\x27/g,"&#39;").replace(/\n/g,"<br/>").replace(/\r/g,"");
}

//.HookForAutoSize
CuteWebUI.HTML.HookForAutoSize=function __CuteWebUI_HTML_HookForAutoSize(element,border,dox,doy,cursor,callback)
{
	border.style.cursor=cursor;
	border.onselectstart=new Function("","return event.returnValue=false;");
	var _down=false;
	var _x=0;
	var _y=0;
	var _l=0,_w=0,_t=0,_h=0;
	var _tid=0;
	border.onmousedown=function()
	{
		if(element._disableresize)return;
		
		_x=event.clientX;
		_y=event.clientY;
		_l=element.offsetLeft;
		_t=element.offsetTop;
		_w=element.offsetWidth;
		_h=element.offsetHeight;
		_down=true;
		border.setCapture();
	}
	border.onmousemove=function()
	{
		if(!_down)return;
		if(dox=="l")
		{
			element.style.left=Math.max(_l+event.clientX-_x,10)+"px";
			element.style.width=Math.max(_w-event.clientX+_x,240)+"px";
		}
		if(dox=="r")
		{
			element.style.width=Math.max(_w+event.clientX-_x,240)+"px";
		}
		if(doy=="b")
		{
			element.style.height=Math.max(_h+event.clientY-_y,180)+"px";
		}
		if(callback)
		{
			clearTimeout(_tid);
			_tid=setTimeout(callback,1);
		}
	}
	border.onmouseup=function()
	{
		border.releaseCapture();
		_down=false;
	}
	border.onlosecapture=function()
	{
		border.releaseCapture();
		_down=false;
	}
}

if(CuteWebUI.HTML.IsGecko)
CuteWebUI.HTML.HookForAutoSize=function __CuteWebUI_HTML_HookForAutoSize_FF(element,border,dox,doy,cursor,callback,arg0)
{
	border.style.cursor=cursor;
	border.onselectstart=new Function("","return event.returnValue=false;");
	var _down=false;
	var _x=0;
	var _y=0;
	var _l=0,_w=0,_t=0,_h=0;
	var _mask;
	var _tid=0;
	border.onmousedown=function(event)
	{
		if(element._disableresize)return;
		
		_x=event.clientX;
		_y=event.clientY;
		_l=element.offsetLeft;
		_t=element.offsetTop;
		_w=element.offsetWidth;
		_h=element.offsetHeight;
		_down=true;
		window.captureEvents(Event.MOUSEMOVE);
		window.captureEvents(Event.MOUOSEUP);
		document.body.addEventListener("mousemove",border.onmousemove,true);
		document.body.addEventListener("mouseup",border.onmouseup,true);
		_mask=document.createElement("DIV");
		_mask.style.zIndex=88888888;
		_mask.style.position="absolute";
		_mask.style.left=document.documentElement.scrollLeft+"px";
		_mask.style.top=document.documentElement.scrollTop+"px";
		_mask.style.width="100%";
		_mask.style.height="100%";
		_mask.style.cursor=cursor;
		document.body.appendChild(_mask);
		return false;
	}
	border.onmousemove=function(event)
	{
		if(!_down)return;
		if(dox=="l")
		{
			element.style.left=Math.max(_l+event.clientX-_x,10)+"px";
			element.style.width=Math.max(_w-event.clientX+_x,240)+"px";
		}
		if(dox=="r")
		{
			element.style.width=Math.max(_w+event.clientX-_x,240)+"px";
		}
		if(doy=="b")
		{
			element.style.height=Math.max(_h+event.clientY-_y,180)+"px";
		}
		_mask.style.left=document.documentElement.scrollLeft+"px";
		_mask.style.top=document.documentElement.scrollTop+"px";
		if(callback)
		{
			clearTimeout(_tid);
			_tid=setTimeout(callback,1);
		}
	}
	border.onmouseup=function()
	{
		border.onlosecapture();
	}
	border.onlosecapture=function()
	{
		_down=false;
		if(_mask)document.body.removeChild(_mask);
		_mask=null;
		window.releaseEvents(Event.MOUSEMOVE);
		window.releaseEvents(Event.MOUOSEUP);
		document.body.removeEventListener("mousemove",border.onmousemove,true);
		document.body.removeEventListener("mouseup",border.onmouseup,true);
	}
}

CuteWebUI.HTML.ReleaseAutoSize=function __CuteWebUI_HTML_ReleaseAutoSize(element,border)
{
	border.onmousedown=null;
	border.onmousemove=null;
	border.onmouseup=null;
	border.onlosecapture=null;
}
CuteWebUI.HTML.FindChild=function __CuteWebUI_FindChild(element,childid)
{
//	if(element.all)
//		return element.all(childid);

	var childs=document.getElementsByName(childid);
	for(var i=0;i<childs.length;i++)
	{
		var child=childs.item(0);
		if(Html_Contains(element,child))
			return child;
	}
	
	var child=CuteWebUI.HTML.FindChildRecursive(element,childid);
	
	if(child==null)
	{
		//alert(element.innerHTML);
	}
	
	return child;
}
CuteWebUI.HTML.FindChildRecursive=function __CuteWebUI_FindChildRecursive(element,childid)
{
	if(element==null)
	{
		//throw(new Error("element is null for : "+childid));
		return null;
	}
	var childs=element.childNodes;
	if(childs&&childs.length)
	{
		for(var i=0;i<childs.length;i++)
		{
			var child=childs.item(i);
			if(child.id==childid)
				return child;
			
			var child2=CuteWebUI.HTML.FindChildRecursive(child,childid);
			if(child2)
				return child2;
		}
	}
	
	return null;
}

//-HTML.Window
CuteWebUI.HTML.Window=function __CuteWebUI_HTML_Window(option)
{
	this._option=option||{};
	this.BaseIndex=this._option.zIndex||1001;
	this._Create();
	if(!CuteWebUI.HTML.__windows)CuteWebUI.HTML.__windows=[];
	CuteWebUI.HTML.__windows.push(this);
	this._floatdiv.style.zIndex=this.BaseIndex+CuteWebUI.HTML.__windows.length;
}

CuteWebUI.HTML.Window.prototype._Create=function __CuteWebUI_HTML_Window__Create()
{
	
	var div=document.createElement("DIV");
	
	div.className="ceifdialog"
	div.style.position="absolute";
	div.style.zIndex=this.BaseIndex;
	
	var table=document.createElement("table");
	table.border=0;
	table.cellSpacing=0;
	table.cellPadding=0;
	table.insertRow(-1);
	table.insertRow(-1);
	table.insertRow(-1);
	table.rows.item(0).insertCell(-1);
	table.rows.item(0).insertCell(-1);
	table.rows.item(0).insertCell(-1);
	table.rows.item(1).insertCell(-1);
	table.rows.item(1).insertCell(-1);
	table.rows.item(1).insertCell(-1);
	table.rows.item(2).insertCell(-1);
	table.rows.item(2).insertCell(-1);
	table.rows.item(2).insertCell(-1);
	var titlebar=table.rows.item(0).cells.item(1)
	table.rows.item(0).cells.item(0).innerHTML="<img height='25' width='15' src='"+CuteChatUrlBase+"Images/1x1.gif'>";
	table.rows.item(0).cells.item(2).innerHTML="<img height='25' width='15' src='"+CuteChatUrlBase+"Images/1x1.gif'>";
	table.rows.item(1).cells.item(0).innerHTML="<img width=15 height='1' src='"+CuteChatUrlBase+"Images/1x1.gif'>";
	table.rows.item(1).cells.item(2).innerHTML="<img width=15 height='1' src='"+CuteChatUrlBase+"Images/1x1.gif'>";
	div.appendChild(table);
	

	titlebar.className="ceifdialogtop";
	table.rows.item(0).cells.item(0).className="ceifdialogtl";
	table.rows.item(0).cells.item(2).className="ceifdialogtr";
	table.rows.item(1).cells.item(0).className="ceifdialogleftbar";
	table.rows.item(1).cells.item(1).className="ceifdialogcenter";
	table.rows.item(1).cells.item(2).className="ceifdialogrightbar";
	table.rows.item(2).cells.item(0).className="ceifdialogbottomleft";
	table.rows.item(2).cells.item(1).className="ceifdialogbottom";
	table.rows.item(2).cells.item(2).className="ceifdialogbottomright";
	
	var divTitle=document.createElement("div");
	divTitle.className="ceifdialogtitletext";
	divTitle.innerHTML="Loading...";
	
	var minbtn=document.createElement("a");
	minbtn.className="btnMinimize";
	if(!CuteWebUI.HTML.IsWinIE)
		minbtn.setAttribute("href","javascript:void(0)");
	else
		minbtn.style.cursor="hand";	
	minbtn.setAttribute("titlebar","Minimize");
	minbtn.setAttribute("title",TEXT("Minimize"));
	
	var maxbtn=document.createElement("a");
	maxbtn.className="btnMaximize";
	if(!CuteWebUI.HTML.IsWinIE)
		maxbtn.setAttribute("href","javascript:void(0)");
	else
		maxbtn.style.cursor="hand";	
	maxbtn.setAttribute("titlebar","Maximize");
	maxbtn.setAttribute("title",TEXT("Maximize"));
	
	var minbtn=document.createElement("a");
	minbtn.className="btnMinimize";
	if(!CuteWebUI.HTML.IsWinIE)
		minbtn.setAttribute("href","javascript:void(0)");
	else
		minbtn.style.cursor="hand";	
	minbtn.setAttribute("titlebar","Minimize");
	minbtn.setAttribute("title",TEXT("Minimize"));
	
	if(!this._option.maxbtn)maxbtn.style.display='none';
	if(!this._option.minbtn)minbtn.style.display='none';
	
	var closebtn=document.createElement("a");
	closebtn.className="btnClose";
	if(!CuteWebUI.HTML.IsWinIE)
		closebtn.setAttribute("href","javascript:void(0)");
	else
		closebtn.style.cursor="hand";	
	closebtn.setAttribute("titlebar","Close");
	closebtn.setAttribute("title",TEXT("Close"));
	
	
	
	titlebar.appendChild(divTitle);
	titlebar.appendChild(closebtn);	
	titlebar.appendChild(maxbtn);
	titlebar.appendChild(minbtn);
	

	var msgCenter=table.rows.item(1).cells.item(1);	
	window.document.body.insertBefore(div,window.document.body.firstChild);
	document.body.appendChild(div);

	div.style.left="0px";
	div.style.top="0px";
	
	div.style.width="400px";
	div.style.height="300px";
	
	var _moving=false;
	var _x=0;
	var _y=0;
	var win=this;
	
	titlebar.onselectstart=function(event)
	{
		event=window.event||event;
		return event.returnValue=false;
	}
	titlebar.ondblclick=function()
	{
		if(win._option.maxbtn)
		{
			win._MaximizeClick();
		}
	}
	titlebar.onmousedown=function(event)
	{
		if(win._winstate=="Maximize")return;
		
		event=window.event||event;
		_moving=true;
		_x=div.offsetLeft-event.clientX;
		_y=div.offsetTop-event.clientY;
		if(titlebar.setCapture)
		{
			titlebar.setCapture(true);
		}
		else
		{
			window.captureEvents(Event.MOUSEMOVE);
			window.captureEvents(Event.MOUOSEUP);
			
			document.addEventListener("mousemove",titlebar.onmousemove,true);
			document.addEventListener("mouseup",titlebar.onmouseup,true);
		}
	}
	titlebar.onmousemove=titlebar.onmouseout=function(event)
	{
		event=window.event||event;
		if(!_moving)return;
		var x=_x+event.clientX;
		if(x<0)x=0;
		var y=_y+event.clientY;
		if(y<0)y=0;
		div.style.left=x+"px";
		div.style.top=y+"px";
	}
	titlebar.onmouseup=function()
	{
		_moving=false;
		if(titlebar.releaseCapture)
		{
			titlebar.releaseCapture(true);
		}
		else
		{
			window.releaseEvents(Event.MOUSEMOVE);
			window.releaseEvents(Event.MOUOSEUP);
			
			document.removeEventListener("mousemove",titlebar.onmousemove,true);
			document.removeEventListener("mouseup",titlebar.onmouseup,true);
		}
	}
	titlebar.onlosecapture=function()
	{
		_moving=false;
	}
	
	minbtn.onclick=CuteWebUI.Delegate(this,this._MinimizeClick);
	maxbtn.onclick=CuteWebUI.Delegate(this,this._MaximizeClick);
	
	//closebtn.onclick=
	closebtn.onmousedown=CuteWebUI.Delegate(this,this._CloseClick);
	div.onmousedown=CuteWebUI.Delegate(this,this.Focus);

	div.style.width=div.offsetWidth+"px";
	div.style.height=div.offsetHeight+"px";
	table.style.width="100%";
	table.style.height=div.style.height;
	
	
	CuteWebUI.HTML.HookForAutoSize(div,table.rows.item(1).cells.item(0),"l",null,"w-resize",CuteWebUI.Delegate(this,this._OnResize));
	CuteWebUI.HTML.HookForAutoSize(div,table.rows.item(1).cells.item(2),"r",null,"e-resize",CuteWebUI.Delegate(this,this._OnResize));
	CuteWebUI.HTML.HookForAutoSize(div,table.rows.item(2).cells.item(0),"l","b","ne-resize",CuteWebUI.Delegate(this,this._OnResize));
	CuteWebUI.HTML.HookForAutoSize(div,table.rows.item(2).cells.item(1),null,"b","n-resize",CuteWebUI.Delegate(this,this._OnResize))
	CuteWebUI.HTML.HookForAutoSize(div,table.rows.item(2).cells.item(2),"r","b","nw-resize",CuteWebUI.Delegate(this,this._OnResize));
	
	this._floatdiv=div;
	this._table=table;
	this._titlebar=titlebar;
	this._titlediv=divTitle;
	this._content=msgCenter;
	this._closebtn=closebtn;
	this._minbtn=minbtn;
	this._maxbtn=maxbtn;

	this._adjustHeight=this._table.offsetHeight-this._content.offsetHeight;
	this._adjustWidth=this._table.offsetWidth-this._content.offsetWidth;
	this._table.style.height=this._floatdiv.style.height;
	this._content.style.height=(parseInt(this._floatdiv.style.height)-this._adjustHeight)+"px";
	this._content.style.width=(parseInt(this._floatdiv.style.width)-this._adjustWidth)+"px";
	
	if(window.navigator.userAgent.indexOf("AppleWebKit")!=-1)
	{
		setTimeout(CuteWebUI.Delegate(this,function(){
			if(!this._content)return;
			var h=parseInt(this._floatdiv.style.height);
			this.SetHeight(h+1)
			setTimeout(CuteWebUI.Delegate(this,function(){
				this.SetHeight(h)
			}),1);
		}),200);
	}
}
CuteWebUI.HTML.Window.prototype._OnResize=function __CuteWebUI_HTML_Window__OnResize()
{
	this._table.style.height=this._floatdiv.style.height;
	try{this._content.style.height=(parseInt(this._floatdiv.style.height)-this._adjustHeight)+"px";}catch(x){}
	try{this._content.style.width=(parseInt(this._floatdiv.style.width)-this._adjustWidth)+"px";}catch(x){}
	if(this._option.onresize)
	{
		this._option.onresize();
	}
}

CuteWebUI.HTML.Window.prototype.MoveToScreenCenter=function __CuteWebUI_HTML_Window_MoveToScreenCenter()
{
	var cw=window.document.body.clientWidth;
	var ch=window.document.body.clientHeight;

	if(window.document.compatMode=='CSS1Compat')
	{
		cw=window.document.documentElement.clientWidth;
		ch=window.document.documentElement.clientHeight;
	}
	
	if(cw>this.GetWidth())
	{
		this.SetLeft( Math.floor((cw-this.GetWidth())/2) );
	}
	if(ch>this.GetHeight())
	{
		this.SetTop( Math.floor((ch-this.GetHeight())/2) );
	}
	
}
		
CuteWebUI.HTML.Window.prototype.Show=function __CuteWebUI_HTML_Window_Show()
{
	this._floatdiv.style.display="";
}
CuteWebUI.HTML.Window.prototype.Hide=function __CuteWebUI_HTML_Window_Hide()
{
	this._floatdiv.style.display="none";
}
CuteWebUI.HTML.Window.prototype.SetTitle=function __CuteWebUI_HTML_Window_SetTitle(value,ishtml)
{
	this._titlediv.innerHTML=ishtml?value:CuteWebUI.HTML.Encode(value);
}
CuteWebUI.HTML.Window.prototype.GetTitle=function __CuteWebUI_HTML_Window_GetTitle()
{
	return this._titlediv.innerText||this._titlediv.textContent||"";
}

CuteWebUI.HTML.Window.prototype.SetTop=function __CuteWebUI_HTML_Window_SetTop(value)
{
	this._floatdiv.style.top=parseInt(value)+"px";
}
CuteWebUI.HTML.Window.prototype.GetTop=function __CuteWebUI_HTML_Window_GetTop()
{
	return this._floatdiv.offsetTop;
}
CuteWebUI.HTML.Window.prototype.SetLeft=function __CuteWebUI_HTML_Window_SetLeft(value)
{
	this._floatdiv.style.left=parseInt(value)+"px";
}
CuteWebUI.HTML.Window.prototype.GetLeft=function __CuteWebUI_HTML_Window_GetLeft()
{
	return this._floatdiv.offsetLeft;
}
CuteWebUI.HTML.Window.prototype.SetWidth=function __CuteWebUI_HTML_Window_SetWidth(value)
{
	this._floatdiv.style.width=parseInt(value)+"px";
	this._OnResize();
}
CuteWebUI.HTML.Window.prototype.GetWidth=function __CuteWebUI_HTML_Window_GetWidth()
{
	return this._floatdiv.offsetWidth;
}
CuteWebUI.HTML.Window.prototype.SetHeight=function __CuteWebUI_HTML_Window_SetHeight(value)
{
	this._floatdiv.style.height=parseInt(value)+"px";
	this._OnResize();
}
CuteWebUI.HTML.Window.prototype.GetHeight=function __CuteWebUI_HTML_Window_GetHeight()
{
	return this._floatdiv.offsetHeight;
}
CuteWebUI.HTML.Window.prototype.GetWindowElement=function __CuteWebUI_HTML_Window_GetWindowElement()
{
	return this._floatdiv;
}
CuteWebUI.HTML.Window.prototype.GetContentElement=function __CuteWebUI_HTML_Window_GetContentElement()
{
	return this._content;
}
CuteWebUI.HTML.Window.prototype.HideCloseButton=function __CuteWebUI_HTML_Window_HideCloseButton()
{
	this._closebtn.style.display='none';
}
CuteWebUI.HTML.Window.prototype.Close=function __CuteWebUI_HTML_Window_Close()
{
	if(this._option.onclose)
	{
		//return true to skip close..
		if(this._option.onclose(this,"close"))
			return;
	}
	this.Dispose();
}
CuteWebUI.HTML.Window.prototype.FullWindow=function __CuteWebUI_HTML_Window_FullWindow()
{
	if(this._isfull)return;
	
	this._SetWindowState("Normal");
	this._isfull=true;
	
	this._table.rows.item(0).style.display='none';
	this._table.rows.item(2).style.display='none';
	this._table.rows.item(1).cells.item(0).style.display='none';
	this._table.rows.item(1).cells.item(2).style.display='none';
	
	this._OnFullWindowResize();
	
	this._fullonresize=CuteWebUI.Delegate(this,this._OnFullWindowResize);
	
	if(window.attachEvent)
		window.attachEvent("onresize",this._fullonresize);
	else
		window.addEventListener("resize",this._fullonresize,false);
}
CuteWebUI.HTML.Window.prototype._OnFullWindowResize=function __CuteWebUI_HTML_Window__OnFullWindowResize()
{
	if(!this._isfull)return;
	
	var cw=window.document.body.clientWidth;
	var ch=window.document.body.clientHeight;

	if(window.document.compatMode=='CSS1Compat')
	{
		cw=window.document.documentElement.clientWidth;
		ch=window.document.documentElement.clientHeight;
	}
	
	this._floatdiv.style.borderWidth="0px";
	this._table.style.borderWidth="0px";
	this._floatdiv.style.top="-1px";
	this._floatdiv.style.left="-1px";
	this._adjustWidth=0;
	this._adjustHeight=1;
	this.SetWidth(cw+2);
	this.SetHeight(ch+2);
}
CuteWebUI.HTML.Window.prototype._SetWindowState=function __CuteWebUI_HTML_Window__SetWindowState(state)
{
	if(this._isfull)return;
	//state => Minimize Maximize Normal
	if(this._winstate==state)return;
	if(this._winstate==null||this._winstate=="Normal")
	{
		this._normalwidth=this.GetWidth();
		this._normalheight=this.GetHeight();
	}
	if(this._winstate!="Maximize")
	{
		this._normaltop=this.GetTop();
		this._normalleft=this.GetLeft();
	}

	var oldstate=this._winstate;
	this._winstate=state;

	var rl=this._table.rows.length;
	if(this._winstate=="Minimize")
	{
		this._floatdiv._disableresize=true;
		var height=this._table.rows.item(0).offsetHeight+this._table.rows.item(rl-1).offsetHeight;
		
		for(var i=1;i<rl-1;i++)
			this._table.rows.item(i).style.display="none";
		
		this.SetHeight(height);
		this.SetWidth(this._normalwidth);
		this.SetLeft(this._normalleft);
		this.SetTop(this._normaltop);
	}
	else
	{
		this._floatdiv._disableresize=false;
		
		for(var i=1;i<rl-1;i++)
			this._table.rows.item(i).style.display="";
		
		if(this._winstate=="Normal")
		{
			this.SetHeight(this._normalheight);
			this.SetWidth(this._normalwidth);
			this.SetLeft(this._normalleft);
			this.SetTop(this._normaltop);
			
			this._maximized=false;
			this._maxbtn.className="btnMaximize";
		}
		if(this._winstate=="Maximize")
		{			
			var cw=window.document.body.clientWidth;
			var ch=window.document.body.clientHeight;

			if(window.document.compatMode=='CSS1Compat')
			{
				cw=window.document.documentElement.clientWidth;
				ch=window.document.documentElement.clientHeight;
			}
			
			this.SetHeight(ch);
			this.SetWidth(cw);
			this.SetLeft(0);
			this.SetTop(0);
			
			this._maximized=true;
			this._maxbtn.className="btnMaximized";
		}
	}
	this._OnResize();
}
CuteWebUI.HTML.Window.prototype._MinimizeClick=function __CuteWebUI_HTML_Window__MinimizeClick()
{
	switch(this._winstate)
	{
		case "Minimize":
			if(this._maximized)
				this._SetWindowState("Maximize");
			else
				this._SetWindowState("Normal");
			break;
		default:
			this._SetWindowState("Minimize");
			break;
	}
}
CuteWebUI.HTML.Window.prototype._MaximizeClick=function __CuteWebUI_HTML_Window__MaximizeClick()
{
	if(this._maximized)
		this._SetWindowState("Normal");
	else
		this._SetWindowState("Maximize");
}

CuteWebUI.HTML.Window.prototype._CloseClick=function __CuteWebUI_HTML_Window__CloseClick()
{
	if(this._option.onclose)
	{
		//return true to skip close..
		if(this._option.onclose(this,"click"))
			return false;
	}
	this.Dispose();
	return false;
}
CuteWebUI.HTML.Window.prototype.Focus=function __CuteWebUI_HTML_Window_Focus()
{
	if(this._isfull)return;
	
	var win=this;
	var arr=CuteWebUI.HTML.__windows.concat();
	arr.sort(function(win1,win2){
		var z1=parseInt(win1._floatdiv.style.zIndex);
		var z2=parseInt(win2._floatdiv.style.zIndex);
		if(win1==win)z1=100000000;
		if(win2==win)z2=100000000;
		return z1-z2;
	});
	for(var i=0;i<arr.length;i++)
	{
		arr[i]._floatdiv.style.zIndex=arr[i].BaseIndex+i;
	}
}
CuteWebUI.HTML.Window.prototype.Dispose=function __CuteWebUI_HTML_Window_Dispose()
{
	if(!this._floatdiv)return;
	
	for(var i=0;i<CuteWebUI.HTML.__windows.length;i++)
	{
		if(CuteWebUI.HTML.__windows[i]==this)
		{
			CuteWebUI.HTML.__windows.splice(i,1);
			break;
		}
	}
	
	this._titlebar.onselectstart=null;
	this._titlebar.onmousedown=null;
	this._titlebar.onmouseup=null;
	this._titlebar.onmouseover=null;
	this._titlebar.onmousemove=null;
	this._titlebar.onlosecapture=null;
	this._closebtn.onclick=null;
	this._floatdiv.onmousedown=null;
	
	var div=this._floatdiv;
	var table=this._table;
	
	div.parentNode.removeChild(div);
	
	CuteWebUI.HTML.ReleaseAutoSize(div,table.rows.item(1).cells.item(0));
	CuteWebUI.HTML.ReleaseAutoSize(div,table.rows.item(1).cells.item(2));
	CuteWebUI.HTML.ReleaseAutoSize(div,table.rows.item(2).cells.item(0));
	CuteWebUI.HTML.ReleaseAutoSize(div,table.rows.item(2).cells.item(1))
	CuteWebUI.HTML.ReleaseAutoSize(div,table.rows.item(2).cells.item(2));
	
	this._floatdiv=null;
	this._titlebar=null;
	this._titlediv=null;
	this._closebtn=null;
	this._content=null;
	this._table=null;
}

function __CuteWebUI_HTML_QueueDialog_OnClose(arg)
{
	var option=this;
	var win=option.win;
	
	if(win._option.oldclose&&win._option.oldclose(arg))
		return true;
	win._option.onclose=win._option.oldclose;
	
	var list=CuteWebUI.HTML.QueueDialog._list;
	list.shift();
	if(list[0])
	{
		list[0].Show();
	}
	else
	{
		CuteWebUI.HTML.HideDialogMask();
	}
	if(win.resulthandler)
	{
		win.resulthandler(win.result);
	}
}
CuteWebUI.HTML.QueueDialog=function __CuteWebUI_HTML_QueueDialog(win)
{
	win._option.win=win;
	win._option.oldclose=win._option.onclose;
	win._option.onclose=__CuteWebUI_HTML_QueueDialog_OnClose;
	
	var list=CuteWebUI.HTML.QueueDialog._list;
	if(list==null)list=CuteWebUI.HTML.QueueDialog._list=[];
	if(list.length==0)
	{
		list[0]=win;
	}
	else
	{
		list.push(win);
		win.Hide();
	}
	CuteWebUI.HTML.ShowDialogMask();
}

CuteWebUI.HTML.__dialogshadow=null;
CuteWebUI.HTML.ShowDialogMask=function __CuteWebUI_HTML_ShowDialogMask()
{
	var de=document.body;
	if(window.document.compatMode=='CSS1Compat')
		de=document.documentElement;
	
	if(CuteWebUI.HTML.__dialogshadow==null)
	{
		CuteWebUI.HTML.__dialogshadow = document.createElement("div");
		CuteWebUI.HTML.__dialogshadow.className="ceifdialogshadow";
		var style=CuteWebUI.HTML.__dialogshadow.style;
		style.zIndex=100000;
		style.backgroundColor="336699";
		style.position="absolute";
		style.top="0px";
		style.left="0px";
		style.width="100%";
		style.height="100%";
		style.cursor="not-allowed";
		style.filter="alpha(opacity=20)";
		style.opacity="0.2";
		style.mozOpacity="0.2";
		window.document.body.insertBefore(CuteWebUI.HTML.__dialogshadow,window.document.body.firstChild);
	}
	CuteWebUI.HTML.__dialogshadow.style.display="";
	CuteWebUI.HTML.__dialogshadow.style.width=de.scrollWidth+"px";
	CuteWebUI.HTML.__dialogshadow.style.height=de.scrollHeight+"px";
}
CuteWebUI.HTML.HideDialogMask=function __CuteWebUI_HTML_HideDialogMask()
{
	if(CuteWebUI.HTML.__dialogshadow)
	{
		CuteWebUI.HTML.__dialogshadow.style.display="none";
	}
}

CuteWebUI.HTML.Alert=function __CuteWebUI_HTML_Alert(func,message,title)
{
	var win=new CuteWebUI.HTML.Window({zIndex:88888888});
	win.resulthandler=func;
	win.SetTitle(title||"Alert");
	win.SetHeight(150);
	win.SetWidth(300);
	win.MoveToScreenCenter();
	
	CuteWebUI.HTML.QueueDialog(win);
	
	var ce=win.GetContentElement();
	ce.innerHTML="<div id='cwui_msg'></div><div id='cwui_btns'><button id='cwui_ok'>OK</button></div>";
	var msg=CuteWebUI.HTML.FindChild(ce,"cwui_msg");
	msg.innerHTML=message;
	var ok=CuteWebUI.HTML.FindChild(ce,"cwui_ok");
	ok.onclick=function()
	{
		win.result=true;
		win.Close();
	}
	if(win._floatdiv.style.display!="none" && GetWindowIsFocus() )
	{
		ok.focus();
	}
}

CuteWebUI.HTML.Confirm=function __CuteWebUI_HTML_Confirm(func,message,title,oktext,canceltext)
{
	var win=new CuteWebUI.HTML.Window({zIndex:88888888});
	win.resulthandler=func;
	win.SetTitle(title||"Confirm");
	win.SetHeight(150);
	win.SetWidth(300);
	win.MoveToScreenCenter();
	
	CuteWebUI.HTML.QueueDialog(win);
	
	var ce=win.GetContentElement();
	ce.innerHTML="<div id='cwui_msg'></div><div id='cwui_btns'><button id='cwui_ok'>OK</button>&nbsp;&nbsp;&nbsp;<button id='cwui_cancel'>Cancel</button></div>";
	var msg=CuteWebUI.HTML.FindChild(ce,"cwui_msg");
	msg.innerHTML=message;
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
	win.SetOKText=function(text)
	{
		ok.innerHTML=CuteWebUI.HTML.Encode(text);
	}
	win.SetCancelText=function(text)
	{
		cc.innerHTML=CuteWebUI.HTML.Encode(text);
	}
	win.SetOKText(oktext||TEXT("OK"));
	win.SetCancelText(canceltext||TEXT("Cancel"));
	return win;
}

CuteWebUI.HTML.Prompt=function __CuteWebUI_HTML_Prompt(func,message,title,inputText)
{
	var win=new CuteWebUI.HTML.Window({zIndex:88888888});
	win.resulthandler=func;
	win.SetTitle(title||"Prompt");
	win.SetHeight(180);
	win.SetWidth(300);
	win.MoveToScreenCenter();
	
	CuteWebUI.HTML.QueueDialog(win);
	
	var ce=win.GetContentElement();
	ce.innerHTML="<div id='cwui_msg'></div><div id='cwui_text'><input type=text id='cwui_inp' style='width:180px;' /></div><div id='cwui_btns'><button id='cwui_ok'>OK</button>&nbsp;&nbsp;&nbsp;<button id='cwui_cancel'>Cancel</button></div>";
	var msg=CuteWebUI.HTML.FindChild(ce,"cwui_msg");
	msg.innerHTML=message;
	var tb=CuteWebUI.HTML.FindChild(ce,"cwui_inp");
	var ok=CuteWebUI.HTML.FindChild(ce,"cwui_ok");
	ok.onclick=function()
	{
		win.result=tb.value;
		win.Close();
	}
	var cc=CuteWebUI.HTML.FindChild(ce,"cwui_cancel");
	cc.onclick=function()
	{
		win.Close()
	}
	if(win._floatdiv.style.display!="none"  && GetWindowIsFocus() )
	{
		tb.focus();
	}
	tb.onkeydown=function(event)
	{
		event=window.event||event;
		if(event.keyCode==13)
		{
			win.result=tb.value;
			win.Close();
			if(event.preventDefault)event.preventDefault()
			return event.returnValue=false;
		}
		if(event.keyCode==27)
		{
			win.Close();
			if(event.preventDefault)event.preventDefault()
			return event.returnValue=false;
		}
	}
	win.SetOKText=function(text)
	{
		ok.innerHTML=CuteWebUI.HTML.Encode(text);
	}
	win.SetCancelText=function(text)
	{
		cc.innerHTML=CuteWebUI.HTML.Encode(text);
	}
	return win;
}




















