
Chat_Sync_Timeout=2000;

var channelpanel=null;
var channelpos=null;

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

	LoadSkinClasses(SkinName,"ChatUI.Xml.aspx?Type=ChannelMain");

	var td_channel_container=document.getElementById("td_channel_container");
	
	if(channelpanel!=null)
	{
		channelpanel.Dispose();
	}

	channelpanel=CreateInstance("HtmlPanel");
	
	td_channel_container.appendChild(channelpanel.GetElement());
	channelpanel.InternalSetParent(null,td_channel_container);

	var ChannelMainForm=CreateInstance("ChannelMainForm");
	channelpanel.AppendControl( ChannelMainForm );
	
	channelpos=null;
	AdjustPanelLayout();

	//Desktop.ResumeLayout();
}

channel_window_onload=function()
{
	HtmlInitialize();

	_SL_ParseXml(LoadChatClasses("ChatUI.Xml.aspx?Type=ChannelMain"),null);

	ReloadUI();

	setInterval(AdjustPanelLayout,100);

	Connect(Embed_Place);
}

function AdjustPanelLayout()
{
	var td_channel_container=document.getElementById("td_channel_container");
	var pos=CalcPosition(channelpanel.GetElement(),td_channel_container);
	pos.width=td_channel_container.offsetWidth;
	pos.height=td_channel_container.offsetHeight;
	
	if(channelpos!=null)
	{
		var equals=true;
		if(channelpos.top!=pos.top)
			equals=false;
		if(channelpos.left!=pos.left)
			equals=false;
		if(channelpos.width!=pos.width)
			equals=false;
		if(channelpos.height!=pos.height)
			equals=false;
		if(equals)return;
	}	
	
	channelpos=pos;
	
	channelpanel.SetTop(pos.top);
	channelpanel.SetLeft(pos.left);
	channelpanel.SetWidth(pos.width);
	channelpanel.SetHeight(pos.height);
	
}

function channel_window_onunload()
{
	Disconnect(true);
}
window.onunload=channel_window_onunload;
if(window.attachEvent)
{
	window.attachEvent("onload",channel_window_onload);
	window.attachEvent("onunload",channel_window_onunload);
}
else if(window.addEventListener)
{
	window.addEventListener("load",channel_window_onload,true);
	window.addEventListener("unload",channel_window_onunload,true);
}








