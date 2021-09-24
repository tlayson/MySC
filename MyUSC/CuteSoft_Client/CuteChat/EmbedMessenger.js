
Chat_Sync_Timeout=2000;

var messengerpanel=null;
var messengerpos=null;


AttachChatEvent("IMCOOKIE",MainFormHandleIMCOOKIE);

function MainFormHandleIMCOOKIE(name,type)
{
	if(type=="DOUPDATE")
	{
		ChatUI_IMDeclareMainForm();
	}
}

function ReloadUI()
{
	//Desktop.SuspendLayout();

	LoadSkinClasses(SkinName,"ChatUI.Xml.aspx?Type=InstantMain");

	var td_messenger_container=document.getElementById("td_messenger_container");

	if(messengerpanel!=null)
	{
		messengerpanel.Dispose();
	}
	
	messengerpanel=CreateInstance("HtmlPanel");

	td_messenger_container.appendChild(messengerpanel.GetElement());
	messengerpanel.InternalSetParent(null,td_messenger_container);
		
	var InstantSigninPanel=CreateInstance("InstantLoginForm");
	var InstantMainPanel=window.instantmainform=CreateInstance("InstantMainForm");
	
	InstantSigninPanel.SetAutoDock("Fill");
	InstantMainPanel.SetAutoDock("Fill");
	
	messengerpanel.AppendControl( InstantSigninPanel );
	messengerpanel.AppendControl( InstantMainPanel );

	messengerpos=null;
	AdjustPanelLayout();
	
	//Desktop.ResumeLayout();

}

messenger_window_onload=function()
{
	HtmlInitialize();
		
	_SL_ParseXml(LoadChatClasses("ChatUI.Xml.aspx?Type=InstantMain"),Desktop);

	ReloadUI();
	
	Connect("Messenger");

	setInterval(AdjustPanelLayout,100);

}

function AdjustPanelLayout()
{
	var td_messenger_container=document.getElementById("td_messenger_container");
	var pos=CalcPosition(messengerpanel.GetElement(),td_messenger_container);
	pos.width=td_messenger_container.offsetWidth;
	pos.height=td_messenger_container.offsetHeight;
	
	if(messengerpos!=null)
	{
		var equals=true;
		if(messengerpos.top!=pos.top)
			equals=false;
		if(messengerpos.left!=pos.left)
			equals=false;
		if(messengerpos.width!=pos.width)
			equals=false;
		if(messengerpos.height!=pos.height)
			equals=false;
		if(equals)return;
	}	
	
	messengerpos=pos;
	
	messengerpanel.SetTop(pos.top);
	messengerpanel.SetLeft(pos.left);
	messengerpanel.SetWidth(pos.width);
	messengerpanel.SetHeight(pos.height);
}

function messenger_window_onunload()
{
	Disconnect(true);
}
window.onunload=messenger_window_onunload;
if(window.attachEvent)
{
	window.attachEvent("onload",messenger_window_onload);
	window.attachEvent("onunload",messenger_window_onunload);
}
else if(window.addEventListener)
{
	window.addEventListener("load",messenger_window_onload,true);
	window.addEventListener("unload",messenger_window_onunload,true);
}

