/****************************************************************\
	Copyrights CuteSoft 2005-2006
	All rights reserved.
\****************************************************************/

//core.js
Imports("html");
//Imports("oldmenutreeimpl");
Library("htmlmenu");

Interface("IHtmlMenuContainer",IHtmlMenuContainer_Descriptor);
function IHtmlMenuContainer_Descriptor(define)
{
	define.ImplementInterface("IEventSource");
	define.DefineMethod("SetInitializer",IHtmlMenuContainer_SetInitializer);
	define.DefineMethod("AppendMenuItem",IHtmlMenuContainer_AppendMenuItem);
	define.DefineMethod("RemoveMenuItem",IHtmlMenuContainer_RemoveMenuItem);
	define.DefineMethod("GetMenuItems",IHtmlMenuContainer_GetMenuItems);
	define.DefineMethod("InitializeMenuItems",IHtmlMenuContainer_InitializeMenuItems);
	define.DefineMethod("ShowMenuItems",IHtmlMenuContainer_ShowMenuItems);
	define.DefineMethod("GetRootContainer",IHtmlMenuContainer_GetRootContainer);
}
function IHtmlMenuContainer_SetInitializer(func)
{
	this._initializer=func;
}

function IHtmlMenuContainer_AppendMenuItem(menuitem)
{
	this._initialized=false;
	
	var arr=this._menuitems;
	if(!arr)this._menuitems=arr=[];
	arr[arr.length]=menuitem;
	menuitem._sl_state._container=PublicInstance(this);
}
function IHtmlMenuContainer_GetRootContainer()
{
	var last=this;
	for(var menuitem=this;menuitem!=null;menuitem=menuitem._sl_state._container)
	{
		last=menuitem;
	}
	return PublicInstance(last);
}
function IHtmlMenuContainer_RemoveMenuItem(menuitem)
{
	this._initialized=false;
	
	var arr=this._menuitems;
	if(!arr)return -1;
	for(var i=0;i<arr.length;i++)
	{
		if(ReferenceEquals(arr[i],menuitem))
		{
			arr.splice(i,1);
			return i;
		}
	}
	return -1;
}
function IHtmlMenuContainer_GetMenuItems(menuitem)
{
	var arr=this._menuitems;
	if(!arr)return [];
	return arr.concat();
}
function IHtmlMenuContainer_InitializeMenuItems()
{
	if(!this._initialized)
	{
		if(this._initializer)
		{
			this._initializer(PublicInstance(this));
		}
		this.ConvertTo("IEventSource").FireEvent("MenuInitialize");
		this._initialized=true;
	}
}
function IHtmlMenuContainer_ShowMenuItems(parent,x,y,offsetParent)
{

	this.InitializeMenuItems();
	
	this.ConvertTo("IEventSource").FireEvent("MenuPrepair");

	//TODO:
	//the old implementation is not fit for html library
	//make new menu implementation...

	if(this._menuitems&&this._menuitems.length)
	{
		this.ConvertTo("IEventSource").FireEvent("Show",["Show",parent]);
		
		var coremenu=CreateOldMenuImplementation();
		var count=0;
		for(var i=0;i<this._menuitems.length;i++)
		{
			var menuitem=this._menuitems[i];
			menuitem.OnUpdate(parent);
			if(menuitem.GetVisible())
			{
				count++;
				IHtmlMenuContainer__AddMenuIitem(parent,coremenu,this,menuitem);
			}
		}
		if(count)
		{
			if(offsetParent==null||offsetParent==document.body)
			{
				offsetParent=document.body;
				x+=document.documentElement.scrollLeft||document.body.scrollLeft;
				y+=document.documentElement.scrollTop||document.body.scrollTop;
			}
			//document.title=[x,y,document.documentElement.scrollTop,document.body.scrollTop]
			coremenu.Show(offsetParent,x,y);
		}
	}
	
}
function IHtmlMenuContainer__AddMenuIitem(parent,coremenu,menu,menuitem)
{
	menuitem.InitializeMenuItems();
	var items=menuitem.GetMenuItems();
	
	var childcoremenu=coremenu.Add(menuitem.GetEnable()?1:0,Html_Encode(menuitem.GetText()),menuitem.GetImage()
		,menuitem.GetClickable()?onclick:null,items.length?oninitialize:null);
	
	function onclick()
	{
		if(Html_IsWinIE)
		{
			menuitem.OnClick(parent);
		}
		else
		{
			//this implementation use cross frame.
			//XMLHttpRequest has bug on that !
			setTimeout(onclick2,1);
		}
	}
	function onclick2()
	{
		menuitem.OnClick(parent);
	}
	function oninitialize()
	{
		for(var i=0;i<items.length;i++)
		{
			var childmenuitem=items[i];
			childmenuitem.OnUpdate(parent);
			if(childmenuitem.GetVisible())
			{
				IHtmlMenuContainer__AddMenuIitem(parent,childcoremenu,menuitem,childmenuitem);
			}
		}
	}
}

Class("HtmlMenuItem",HtmlMenuItem_Descriptor);
function HtmlMenuItem_Descriptor(define)
{
	define.ImplementInterface("IEventSource",true);
	define.ImplementInterface("IHtmlMenuContainer",true);
	define.ImplementInterface("IChildrenParser");
	define.ImplementInterfaceMethod("IChildrenParser","AppendChild",HtmlMenuItem_IChildrenParser_AppendChild);
	define.DefineProperty("public virtual","Text",HtmlMenuItem_GetText,HtmlMenuItem_SetText);
	define.DefineProperty("public virtual","Image",HtmlMenuItem_GetImage,HtmlMenuItem_SetImage);
	define.DefineProperty("public virtual","Enable",HtmlMenuItem_GetEnable,HtmlMenuItem_SetEnable);
	define.DefineProperty("public virtual","Visible",HtmlMenuItem_GetVisible,HtmlMenuItem_SetVisible);
	define.DefineProperty("public virtual","Clickable",HtmlMenuItem_GetClickable,HtmlMenuItem_SetClickable);
	define.DefineProperty("public virtual","DataKey",HtmlMenuItem_GetDataKey,HtmlMenuItem_SetDataKey);
	
	define.DefineMethod("public virtual","OnClick",HtmlMenuItem_OnClick);
	define.DefineMethod("public virtual","OnUpdate",HtmlMenuItem_OnUpdate);
}
function HtmlMenuItem_GetText()
{
	return this._text||"";
}
function HtmlMenuItem_SetText(val)
{
	this._text=String(val);
}
function HtmlMenuItem_GetImage()
{
	return this._image;
}
function HtmlMenuItem_SetImage(val)
{
	this._image=_SL_ExpandVariables(val);
}
function HtmlMenuItem_GetEnable()
{
	return !this._disabled;
}
function HtmlMenuItem_SetEnable(val)
{
	this._disabled=!_SL_ToBoolean(val);
}
function HtmlMenuItem_GetVisible()
{
	return !this._hidden;
}
function HtmlMenuItem_SetVisible(val)
{
	this._hidden=!_SL_ToBoolean(val)
}
function HtmlMenuItem_GetClickable()
{
	return !this._notclick;
}
function HtmlMenuItem_SetClickable(val)
{
	this._notclick=!_SL_ToBoolean(val);
}
function HtmlMenuItem_GetDataKey()
{
	return this._datakey;
}
function HtmlMenuItem_SetDataKey(val)
{
	this._datakey=val;
}
function HtmlMenuItem_OnClick(parent)
{
	var htmlEvent=CreateInstance("HtmlEvent",PublicInstance(this),"Click",null);
	htmlEvent.SetParent(parent);
	this.FireEvent("Click",[htmlEvent]);
}
function HtmlMenuItem_OnUpdate(parent)
{
	var htmlEvent=CreateInstance("HtmlEvent",PublicInstance(this),"Update",null);
	htmlEvent.SetParent(parent);
	this.FireEvent("Update",[htmlEvent]);
}
function HtmlMenuItem_IChildrenParser_AppendChild(obj)
{
	var menu=obj.ConvertAs("HtmlMenuItem");
	if(menu)
	{
		this.AppendMenuItem(menu);
		return;
	}
}

Class("HtmlContextMenu",HtmlContextMenu_Descriptor);
function HtmlContextMenu_Descriptor(define)
{
	define.ImplementInterface("IEventSource",true);
	define.ImplementInterface("IHtmlMenuContainer",true);
	define.ImplementInterface("IChildrenParser");
	define.ImplementInterfaceMethod("IChildrenParser","AppendChild",HtmlContextMenu_IChildrenParser_AppendChild);
}
function HtmlContextMenu_IChildrenParser_AppendChild(obj)
{
	var menu=obj.ConvertAs("HtmlMenuItem");
	if(menu)
	{
		this.AppendMenuItem(menu);
		return;
	}
}

Class("HtmlMenuButton",HtmlMenuButton_Descriptor,"HtmlButton");
function HtmlMenuButton_Descriptor(define)
{
}

Class("HtmlMenuControl",HtmlMenuControl_Descriptor,"HtmlComplexControl");
function HtmlMenuControl_Descriptor(define)
{
	define.SetConstructor(HtmlMenuControl_Constructor);
	
	define.ImplementInterfaceMethod("IChildrenParser","AppendChild",HtmlMenuControl_IChildrenParser_AppendChild);
	
	define.ImplementInterface("IHtmlMenuContainer",true);
	define.ImplementInterfaceMethod("IHtmlMenuContainer","AppendMenuItem",HtmlMenuControl_AppendMenuItem);
	define.ImplementInterfaceMethod("IHtmlMenuContainer","RemoveMenuItem",HtmlMenuControl_RemoveMenuItem);
	
	define.ImplementInterface("ISupportInit");
	define.ImplementInterfaceMethod("ISupportInit","BeginInit",HtmlMenuControl_ISupportInit_BeginInit);
	define.ImplementInterfaceMethod("ISupportInit","EndInit",HtmlMenuControl_ISupportInit_EndInit);
	
	define.DefineProperty("public virtual","ButtonDock",HtmlMenuControl_GetButtonDock,HtmlMenuControl_SetButtonDock);
	define.DefineProperty("public","ContextParent",HtmlMenuControl_GetContextParent,HtmlMenuControl_SetContextParent);
	define.DefineMethod("public virtual","UpdateMenuItem",HtmlMenuControl_UpdateMenuItem);
	define.DefineMethod("public virtual","UpdateMenuItemAt",HtmlMenuControl_UpdateMenuItemAt);
	define.DefineMethod("protected virtual","HandleButtonClick",HtmlMenuControl_HandleButtonClick);
	
}
function HtmlMenuControl_Constructor()
{
	var div=Desktop.CreateElement("DIV");
	this.base_constructor(div);
	this.SetHeight(20);
	this.SetWidth(90);
}
function HtmlMenuControl_ISupportInit_BeginInit()
{
}
function HtmlMenuControl_ISupportInit_EndInit()
{
	var arr=this.GetMenuItems();
	for(var i=0;i<arr.length;i++)
	{
		var menuitem=arr[i];
		var button=this.GetControlAt(i);
		var text=menuitem.GetText();
		button.SetText(text);
		button.SetVisible( text!="-" );
	}
}
function HtmlMenuControl_AppendMenuItem(menuitem)
{
	IHtmlMenuContainer_AppendMenuItem.apply(this.ConvertTo("IHtmlMenuContainer"),arguments);
	
	var button=CreateInstance("HtmlMenuButton");
	
	button.SetWidth(60);
	
	button.GetElement().style.cursor="hand";
	
	button.SetBackColor("transparent");
	
	button.SetBorderStyle("None");
	
	button.AttachEvent("Click",this.HandleButtonClick);
	
	button.SetAutoDock(true);
	
	button.SetDockMargins("0,0,1,1");
	
	var text=menuitem.GetText();
	button.SetText(text);
	button.SetVisible( text!="-" );
	button.SetEnable( menuitem.GetEnable() );
	
	button.SetDockEdge(this._btndock||"Left");
	button.menuitem=menuitem;
	this.InternalAppendControl(button);
}
function HtmlMenuControl_RemoveMenuItem()
{
	var index=IHtmlMenuContainer_RemoveMenuItem.apply(this.ConvertTo("IHtmlMenuContainer"),arguments);
	if(index==-1)return;
	
	var ctrl=this.InternalRemoveControlAt(index);
	
	ctrl.Dispose();
}
function HtmlMenuControl_UpdateMenuItem(menuitem)
{
	var arr=this.GetMenuItems();
	for(var i=0;i<arr.length;i++)
	{
		if(ReferenceEquals(arr[i],menuitem))
		{
			var button=this.GetControlAt(i);
			var text=menuitem.GetText();
			button.SetText(text);
			button.SetVisible( text!="-" );
			button.SetEnable( menuitem.GetEnable() );
		}
	}
}
function HtmlMenuControl_UpdateMenuItemAt(index)
{
	var menuitem=this.GetMenuItems()[index];
	if(menuitem)
	{
		var button=this.GetControlAt(index);
		var text=menuitem.GetText();
		button.SetText(text);
		button.SetVisible( text!="-" );
		button.SetEnable( menuitem.GetEnable() );
	}
}
function HtmlMenuControl_IChildrenParser_AppendChild(obj)
{
	var menu=obj.ConvertAs("HtmlMenuItem");
	if(menu)
	{
		this.AppendMenuItem(menu);
		return;
	}
	HtmlControl_IChildrenParser_AppendChild.apply(this,arguments);
}
function HtmlMenuControl_GetButtonDock()
{
	return this._btndock||"Left";
}
function HtmlMenuControl_SetButtonDock(val)
{
	var oldEdge=this._btndock;
	switch(String(val||"").toLowerCase())
	{
		case "top":
			this._btndock="Top";
			break;
		case "left":
			this._btndock="Left";
			break;
		case "right":
			this._btndock="Right";
			break;
		case "bottom":
			this._btndock="Bottom";
			break;
		default:
			this._btndock="Top";
			break;
	}
	if(this._btndock==(oldEdge||"Left"))return;
	
	var c=this.GetControlCount();
	if(c)
	{
		this.SuspendLayout();

		for(var i=0;i<c;i++)
		{
			var ctrl=this.GetControlAt(i);
			ctrl.SetDockEdge(this._btndock);
		}
		
		this.ResumeLayout();
	}
}
function HtmlMenuControl_GetContextParent()
{
	return this._menuparent;
}
function HtmlMenuControl_SetContextParent(value)
{
	this._menuparent=value;
}
function HtmlMenuControl_HandleButtonClick(arg)
{
	var button=arg.GetEventSource();
	var menuitem=button.menuitem;
	
	this.FireEvent("Prepair");
	var parent=this._menuparent||PublicInstance(this);
	
	menuitem.OnClick(parent);
	menuitem.InitializeMenuItems();
	if(menuitem.GetMenuItems().length)
	{
		var l=Html_GetPageClientLeft(button);
		var t=Html_GetPageClientTop(button);
		switch(this._btndock||"Left")
		{
			case "Left":
			case "Right":
				t+=button.GetCurrentHeight();
				break;
			case "Top":
			case "Bottom":
				l+=button.GetCurrentWidth();
				break;
		}
		
		menuitem.ShowMenuItems(parent,l,t);
	}
}

