/****************************************************************\
	Copyright (c) CuteSoft 2005-2006
	All rights reserved.
\****************************************************************/

/****************************************************************\
	*
	*	Menu Tree Implementation
	*
\****************************************************************/

var _menuclass_currentmenu;
var menuimagebase="./";

function CreateOldMenuImplementation()
{
	var editor={};
	editor.GetThemeUrl=function(img)
	{
		return menuimagebase+img;
	}

	var const_menuwidth=100;
	var const_submenudelay=100; // Please don't change to 0!
	var absolutevalue="relative";//"absolute";

	return new MenuClass();




	/****************************************************************\
		*
		*	Menu Tree Implementation Version 2
		*
	\****************************************************************/

	function Menu_ShowFrame(frame,target,offsetX,offsetY,subWidth,subHeight)
	{
		frame.style.position="absolute";
		frame.style.visibility="hidden";
		frame.style.display="block";
		
		var pos=CalcPosition(frame,target);
		
		pos.left+=offsetX;
		pos.top+=offsetY;
		if(subWidth)pos.left-=frame.offsetWidth;
		if(subHeight)pos.top-=frame.offsetHeight;
		AdjustMirror(frame,null,pos);
		frame.style.left=pos.left+"px";
		frame.style.top=pos.top+"px";
		
		frame.style.visibility="visible";
	}

	function MenuClass_GetCurrentMenu()
	{
		return window._menuclass_currentmenu;
	}

	function Create_MenuItemDiv_OnMouseOver(menuitem)
	{
		return Handle_OnMouseOver;
		function Handle_OnMouseOver()
		{
			MenuItemDiv_OnMouseOver(menuitem)
		}
	}
	function MenuItemDiv_OnMouseOver(menuitem)
	{
		var itemdiv=menuitem.itemdiv;
		
		menuitem.style_backgroundColor=itemdiv.style.backgroundColor;
		menuitem.style_border=itemdiv.style.border;
		menuitem.style_margin=itemdiv.style.margin;
		
		itemdiv.style.backgroundColor="#B6BDD2";
		itemdiv.style.border="1px solid #0A246A";
		itemdiv.style.margin="0px";
		menuitem.ismouseover=true;
		if(menuitem.onmouseover)menuitem.onmouseover(menuitem);
		
		menuitem.opentimerid=setTimeout(OpenIt,const_submenudelay);
		function OpenIt()
		{
			if(menuitem.isdisposed)return;
			
			CloseOwnerOpenItem(menuitem,true);
			
			if(!menuitem.initialized)
			{
				if(menuitem.oninitialize)
					menuitem.oninitialize(menuitem);
				menuitem.initialized=true;
			}
			if(menuitem.items.length!=0)
			{
				var pos=GetScrollPostion(menuitem.owner.frame);
				var top=pos.top+GetScrollPostion(itemdiv).top;
				var f=menuitem.owner.frame;
				menuitem.Show( (f.ownerDocument||f.document).body,pos.left+itemdiv.offsetWidth,top);
			}
		}
	}
	function Create_MenuItemDiv_OnMouseOut(menuitem)
	{
		return Handle_OnMouseOut;
		function Handle_OnMouseOut()
		{
			MenuItemDiv_OnMouseOut(menuitem);
		}
	}
	function MenuItemDiv_OnMouseOut(menuitem)
	{
		if(!menuitem.ismouseover)return;
		menuitem.ismouseover=false;
		
		var itemdiv=menuitem.itemdiv;
		
		clearTimeout(menuitem.opentimerid)

		itemdiv.style.backgroundColor=menuitem.style_backgroundColor;
		itemdiv.style.border=menuitem.style_border;
		itemdiv.style.margin=menuitem.style_margin;
		
		if(menuitem.onmouseout)menuitem.onmouseout(menuitem);
	}
	function Create_MenuItemDiv_OnMouseDown(menuitem)
	{
		return MenuItemDiv_OnMouseDown;
		function MenuItemDiv_OnMouseDown()
		{
			clearTimeout(menuitem.menu.windowblurCloseTimer);
			menuitem.menu.mouseDownTime=new Date().getTime();
		}
	}
	function Create_MenuItemDiv_OnClick(menuitem)
	{
		return MenuItemDiv_OnClick;
		function MenuItemDiv_OnClick()
		{
			var clickfunc=menuitem.onclick;
			if(clickfunc)
			{
				menuitem.itemdiv.onmouseout();
				menuitem.menu.HideForClick()
				setTimeout(DisposeMenu,1);
				CuteEditor_CallMenuClickFunction(menuitem,clickfunc);
			}
		}
		function DisposeMenu()
		{
			menuitem.menu.Dispose();
		}
	}

	function MenuClass_CreateFrame(menuitem)
	{
		var frame=AllocateFrame();

		frame.frameBorder=0;
		frame.style.cssText="top:0px;left:0px;visibility:hidden;position:absolute;background-color:white;padding:0px;border:0px;width:90px;height:60px;overflow:visible;";
		frame.style.zIndex=menuitem.zIndex;

		var framewin=Frame_GetContentWindow(frame);
		framewin.onerror=CuteEditor_OnMenuError;
		var framedoc=Frame_GetContentDocument(frame);
		
		framedoc.body.style.cssText="border:1px solid #666666;padding:1px;margin:1px;overflow:hidden;background-image:url("+editor.GetThemeUrl("menuleft.gif")+");background-repeat:repeat-y;"
		
		var table=framedoc.createElement("TABLE");

		table.border=0;
		table.cellSpacing=0;
		table.cellPadding=0;
		
		table.style.cssText="cursor:default;overflow:visible;";

		for(var i=0;i<menuitem.items.length;i++)
		{
			var childitem=menuitem.items[i];
			
			var tr=table.insertRow(-1);
			var cell=tr.insertCell(-1);
			cell.style.cssText='overflow:visible;'
			
			if(childitem.html=='-')//spliter
			{
				if(i!=menuitem.items.length-1)
					cell.innerHTML="<div style='overflow:hidden;border-top:1px solid ThreeDShadow;border-bottom:1px solid ThreeDHighlight;height:2px;'></div>";
				continue;
			}

			var tablehtml="<table border=0 cellspacing=0 cellpadding=0 style='width:100%;height:20px;font:menu;overflow:visible;'>";
			tablehtml+="<tr><td style='width:18px'>"
			if(childitem.imgurl)
			{
				//width=20 height=20 
				tablehtml+="<img width=20 height=20 align=absmiddle border=0 src='"+childitem.imgurl+"'"
				if(childitem.state==2)
				{
					tablehtml+=" style='' "
				}
				else if(childitem.enable)
				{
					tablehtml+=" style='' "
				}
				else
				{
					tablehtml+=" style='' "
				}
				tablehtml+="/>";
			}
			else
			{
				tablehtml+="<img width=20 height=0 border=0 style='visibility:hidden' />";
			}

			if(childitem.hotkey)
			{
				tablehtml+="</td><td nowrap nowrap align='left' style='width:"+const_menuwidth+"px;padding-left:4px;padding-top:2px;padding-right:4px;";
				if(childitem.color)
					tablehtml+="color:"+childitem.color;
				tablehtml+="'>"+childitem.html+"</td>";
				tablehtml+="<td align='left' width=43 style='font:menu;padding-left:1px;padding-top:3px;' rem-disabled=true nowrap>"+childitem.hotkey+"</td>";
			}
			else
			{
				tablehtml+="</td><td colspan=2 nowrap align='left' style='width:"+const_menuwidth+"px;padding-left:4px;padding-top:2px;padding-right:4px;";
				if(childitem.color)
					tablehtml+="color:"+childitem.color;
				tablehtml+="'>"+childitem.html+"</td>";
			}
			
			if(true)
			{
				tablehtml+="<td align='right' style='width:20px;padding-top:2px;padding-right:2px;'><span style='width:20px;";
				if(childitem.items.length==0&&childitem.oninitialize==null)
				{
					tablehtml+=";visibility:hidden;"
				}
				tablehtml+=";font-family:webdings;font-size:14px;'> 4 </span></td>";
			}
			
			tablehtml+="</tr></table>";
			
			var itemdiv=framedoc.createElement("DIV");
			childitem.itemdiv=itemdiv;
			itemdiv.style.cssText="cursor:default;margin:1px;width:100%;overflow:visible;"+(childitem.enable?"":"filter:alpha(opacity=40);");
			if(!childitem.enable)itemdiv.style.MozOpacity=0.4;
			itemdiv.innerHTML=tablehtml;

			itemdiv.onmousedown=Create_MenuItemDiv_OnMouseDown(childitem);

			if(childitem.enable)
			{
				itemdiv.onmouseover=Create_MenuItemDiv_OnMouseOver(childitem);
				itemdiv.onmouseout=Create_MenuItemDiv_OnMouseOut(childitem);
				itemdiv.onmouseup=Create_MenuItemDiv_OnClick(childitem);
				
				if(childitem.onclick)
				{
					itemdiv.style.cursor="hand";
				}
			}
			else
			{
				//itemdiv.disabled=!childitem.enable;
			}

			cell.appendChild(itemdiv);
		}
		
		framedoc.body.appendChild(table);

		frame.style.width=(table.offsetWidth+6)+"px";
		frame.style.height=(table.offsetHeight+6)+"px";
		
		Element_SetUnselectable(frame);
		Element_SetUnselectable(framedoc.body);
		
		return frame;
	}

	function CloseOwnerOpenItem(menuitem,checkIsThis)
	{
		if(menuitem.owner&&menuitem.owner.openitem&&menuitem.owner.openitem.Close)
		{
			if(checkIsThis&&menuitem.owner.openitem==menuitem)
				return;
			menuitem.owner.openitem.Close();
			menuitem.owner.openitem=null;
		}
	}

	function MenuClass()
	{
		var frame=null;
		
		var menu=this;
		
		menu.editor=editor;
		menu.menu=menu;
		menu.items=[];
		menu.zIndex=7777777;

		
		function CloseIt()
		{
			if(menu.Dispose)menu.Dispose();
		}
		
		function OnWindowBlur()
		{
			var last=new Date().getTime() - (menu.mouseDownTime||0);
			if(last>10)
				menu.windowblurCloseTimer=setTimeout(CloseIt,1);
		}
		
		function OnDocumentClick()
		{
			setTimeout(CloseIt,1);
		}
		
		menu.Show=function menu_Show(target,offsetX,offsetY,subWidth,subHeight)
		{
			if(window._menuclass_currentmenu)
				window._menuclass_currentmenu.Dispose();
			
			//Window_Focus(window);
			
			if(menu.frame==null)
			{
				menu.frame=MenuClass_CreateFrame(menu);
			}
			
			Menu_ShowFrame(menu.frame,target,offsetX,offsetY,subWidth,subHeight);

			window._menuclass_currentmenu=menu;
			
			window.onblur=OnWindowBlur;
			document.onmousedown=OnDocumentClick

		}
		menu.HideForClick=function menu_HideForClick()
		{
			if(menu.openitem)
			{
				menu.openitem.Close();
				menu.openitem=null;
			}
			if(menu.frame)
			{
				menu.frame.style.display="none";
			}
		}
		menu.Close=function menu_Close()
		{
			Dispose();
		}
		menu.Dispose=function menu_Dispose()
		{
			Dispose();
		}
		function Dispose()
		{
			if(window._menuclass_currentmenu==menu)
				window._menuclass_currentmenu=null;
				
			window.onblur=new Function();
			document.onmousedown=new Function();
			
			if(menu.isdisposed)return;
			menu.isdisposed=true;
			
			if(menu.openitem)
			{
				menu.openitem.Close();
				menu.openitem=null;
			}
			
			if(menu.frame)
			{
				ReleaseFrame(menu.frame);
				menu.frame=null;
			}
			
			for(var i=0;i<menu.items.length;i++)
			{
				menu.items[i].Dispose();
			}

			RemoveMenuMember(menu);
			menu.isdisposed=true;
		}
		menu.AddSpliter=function menu_AddSpliter()
		{
			if(menu.items.length==0||menu.items[menu.items.length-1].html!='-')
				return menu.AddMenuItem(1,"-");
			return menu.items[menu.items.length-1];
		}
		menu.AddMenuItem=
		menu.Add=function menu_Add(state,html,imgurl,onclick,oninitialize)
		{
			if(imgurl=="-")imgurl=null;
			var childmenu=new ChildMenuClass(menu);
			childmenu.zIndex=menu.zIndex+1;
			childmenu.menu=menu;
			childmenu.owner=menu;
			childmenu.enable=state>0?true:false;
			childmenu.html=html;
			childmenu.imgurl=imgurl//==null?null:editor.GetThemeUrl((imgurl.indexOf('.')==-1?(imgurl+".gif"):imgurl));
			childmenu.onclick=onclick;
			childmenu.oninitialize=oninitialize;
			childmenu.editor=editor;
			menu.items[menu.items.length]=childmenu;
			
			return childmenu;
		}
		
		

		return menu;
	}

	function ChildMenuClass(menu)
	{
		var menuitem=this;
		
		menuitem.items=[];
		
		menuitem.AddSpliter=function menuitem_AddSpliter()
		{
			if(menuitem.items.length==0||menuitem.items[menuitem.items.length-1].html!='-')
				return menuitem.AddMenuItem(1,"-");
			return menuitem.items[menuitem.items.length-1];
		}
		menuitem.AddMenuItem=
		menuitem.Add=function menuitem_AddMenuItem(state,html,imgurl,onclick,oninitialize)
		{
			if(imgurl=="-")imgurl=null;
			
			var childmenu=new ChildMenuClass(menu);
			childmenu.zIndex=menuitem.zIndex+1;
			childmenu.menu=menu;
			childmenu.owner=menuitem;
			childmenu.enable=state>0?true:false;
			childmenu.html=html;
			childmenu.imgurl=imgurl==null?null:editor.GetThemeUrl((imgurl.indexOf('.')==-1?(imgurl+".gif"):imgurl));
			childmenu.onclick=onclick;
			childmenu.oninitialize=oninitialize;
			childmenu.editor=editor;

			menuitem.items[menuitem.items.length]=childmenu;
			
			return childmenu;
		}
		
		menuitem.Show=function menuitem_Show(target,offsetX,offsetY)
		{
			if(menuitem.isopen)return;
			
			CloseOwnerOpenItem(menuitem);
			
			if(menuitem.frame==null)
			{
				menuitem.frame=MenuClass_CreateFrame(menuitem);
			}

			Menu_ShowFrame(menuitem.frame,target,offsetX,offsetY);

			menuitem.owner.openitem=menuitem;
			menuitem.isopen=true;
		}
		menuitem.Close=function menuitem_Close()
		{
			if(!menuitem.isopen)return;

			if(menuitem.openitem)
			{
				menuitem.openitem.Close();
				menuitem.openitem=null;
			}

			MenuItemDiv_OnMouseOut(menuitem);

			menuitem.frame.style.display="none";
			menuitem.frame.style.top='0px'
			menuitem.frame.style.left='0px'
			
			//if ..
			if(menuitem.frame)
			{
				ReleaseFrame(menuitem.frame);
				menuitem.frame=null;
			}

			menuitem.owner.openitem=null;
			
			menuitem.isopen=false;
		}
		menuitem.Dispose=function menuitem_Dispose()
		{
			if(menuitem.isdisposed)return;
			menuitem.isdisposed=true;
			
			if(menuitem.isopen)
			{
				menuitem.Close()
			}
					
			for(var i=0;i<menuitem.items.length;i++)
			{
				menuitem.items[i].Dispose();
			}
			
			if(menuitem.frame)
			{
				ReleaseFrame(menuitem.frame);
				menuitem.frame=null;
			}
			
			RemoveMenuMember(menuitem);
			menuitem.isdisposed=true;
		}
		
		return menuitem;
	}

}

function RemoveMenuMember(menuitem)
{
	menuitem.menu=null;
	menuitem.owner=null;
	menuitem.items=null;
	menuitem.onclick=null;
	menuitem.onmouseover=null;
	menuitem.onmouseout=null;
	menuitem.oninitialize=null;
	
	menuitem.frame=null;

	for(var p in menuitem)
	{
		var v=menuitem[p];
		if(!v)continue;
		if(typeof(v)=="function")
		{
			menuitem[p]=null;
			continue;
		}
	}
}


//FramePool.js
window.__framepool=[];
function AllocateFrame()
{
	var isnew=false;
	var frame;
	if(window.__framepool.length!=0)
	{
		frame=window.__framepool.shift();
	}
	else
	{
		frame=document.createElement("IFRAME");
		frame.style.position="absolute";
		frame.style.display="none";
		document.body.appendChild(frame);
		isnew=true;
	}
	
	var framedoc=Frame_GetContentDocument(frame);

	//open the frame document will let the FireFox show download progress
	//TODO:if do not renew the document , maybe cause some bugs.
	if(isnew)
	{
		framedoc.open("text/html","replace");
		framedoc.write("<html><TITLE></TITLE><BODY style=''></BODY></html>");
		framedoc.close();
	}
	
	return frame;
}
function ReleaseFrame(frame)
{
	frame.style.display="none";
	var framedoc=Frame_GetContentDocument(frame);
	framedoc.body.innerHTML="";
	
	//if(frame.parentNode)frame.parentNode.removeChild(frame);
	window.__framepool.push(frame);
}
function Frame_GetContentDocument(frame)
{
	if(frame.contentDocument)
		return frame.contentDocument;
	return Frame_GetContentWindow(frame).document;
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
	
	Debug_Todo("//TODO:frame contentWindow not found?");
}

function CuteEditor_CallMenuClickFunction(menuitem,func)
{
	//try
	//{
		func(menuitem);
	//}
	//catch(x)
	//{
	//	Debug_Todo("Error:"+x);
	//}
}
function CuteEditor_OnMenuError()
{
}
function Element_SetUnselectable(element)
{
	element.setAttribute("UNSELECTABLE","on")
	element.setAttribute("tabIndex","-1");
	//if(!element.all)return;
	var arr=Element_GetAllElements(element);
	var len=arr.length;
	if(!len)return;
	for(var i=0;i<len;i++)
	{
		arr[i].setAttribute("UNSELECTABLE","on");
		arr[i].setAttribute("tabIndex","-1");
	}
}

function Element_GetAllElements(p)
{
	var arr=[];
	for(var i=0;i<p.all.length;i++)
		arr.push(p.all.item(i));
	return arr;
}
function Element_GetAllElements(p)
{
	var arr=[];	
	FillChildren(p);
	function FillChildren(el)
	{
		var c=el.childNodes;
		var l=c.length;
		for(var i=0;i<l;i++)
		{
			var n=c.item(i);
			if(n.nodeType!=1)continue;
			arr.push(n);
			FillChildren(n);
		}
	}
	
	return arr;
}