//Warning : this file is for old cutechat only!!!!! code have copied to Channel_New.js









//Warning : this file is for old cutechat only!!!!! code have copied to Channel_New.js









//Warning : this file is for old cutechat only!!!!! code have copied to Channel_New.js


//ModerateMode

function ModerateMode_USER_MESSAGE()
{
	this.toString=function()
	{
		return "ModerateMode_USER_MESSAGE";
	}
	return this;
}
ModerateMode_USER_MESSAGE.prototype.GetLeftHTML=function()
{
	return this.UserName;
}
ModerateMode_USER_MESSAGE.prototype.GetRightHTML=function()
{
	var text=this.Item.Args[6];
	var html=this.Item.Args[7];
	if(!html)html=Html_Encode(text);
	return html;
}




var moderatevars;
function InitModerateVars()
{
	if(moderatevars!=null)return;
	
	var vars={};
	
	var div=document.createElement("DIV");
//	div.style.position="absolute";
//	div.style.right="240px";
//	div.style.top="30px";
//	div.style.border="outset 1px";
//	div.style.backgroundColor="white";
	div.style.paddingLeft="12px";
	div.style.paddingRight="12px";
	div.style.paddingBottom="12px";
	div.style.width="100%";
//	div.style.zIndex=3000;
	
	var newquestion=document.createElement("button");
	newquestion.innerHTML="<img src='"+__cc_urlbase+"Images/help.png' border='0' hspace=4 align='absMiddle'>"+TEXT("CreateQuestion");
	newquestion.onclick=ModerateMode_NewQuestion;
	
	var title=document.createElement("DIV");
	title.style.width="300px";
	title.style.padding="12px";
	title.style.textAlign="center";
	title.innerHTML="  ";
	title.appendChild(newquestion);
	div.appendChild(title);
	
	var table=document.createElement("TABLE");
	table.style.width="100%";
	table.cellSpacing=0;
	table.cellPadding=4;
	table.border=1;
	//table.style.borderCollapse="collapse";
	div.appendChild(table);

	vars.mlist=table;
	
	var table=document.createElement("TABLE");
	table.cellSpacing=0;
	table.cellPadding=4;
	table.border=1;
	table.style.borderCollapse="collapse";
	div.appendChild(table);
	
	vars.qlist=table;
	
	vars.panel=div;
	
	var win=CreateInstance("HtmlWindow");
	var htmlpanel=CreateInstance("HtmlSource");
	htmlpanel.SetAutoDock("FILL");
	win.AppendControl(htmlpanel);
	win.SetLayerMode("TOPMOST");
	win.SetTitle("Moderator message queue");
	win.SetTop(60);
	win.SetLeft(300);
	win.SetWidth(480);
	win.SetHeight(320);
	win.SetBackColor("#E9EDF3");
	Desktop.AppendWindow(win);
	htmlpanel.SetAutoScroll(true);
	htmlpanel.GetElement().appendChild(div);
	vars.window=win;

	//document.body.appendChild(div);
	
	moderatevars=vars;
}



function ModerateMode_NewQuestion()
{
	function HandlePrompt(res)
	{
		if(!res)return;
		var message=JoinToMsg("MODERATOR_COMMAND",null,["POSTQUESTION",res]);
		PushCTSMessage(message);
	}
	Desktop.Prompt(HandlePrompt,TEXT("TypeQuestion"));
}


function ModerateMode_OnPlaceEvent(evt,msgid,place)
{
	if(place.ModerateMode)
	{
		function ShowPanel()
		{
			if(GetMyInfo()!=null)
			{
				if(GetMyInfo().IsAdmin)
				{
					InitModerateVars();
				}
			}
			else
			{
				setTimeout(ShowPanel,100);
			}
		}
		ShowPanel()
	}
	else
	{
		//hide the panel ? ignore the messages which need be moderated?
		if(moderatevars!=null)
		{
	
			Desktop.RemoveWindow(moderatevars.window);
			//moderatevars.panel.parentNode.removeChild(moderatevars.panel);
			moderatevars=null;
		}
	}
}

function ModerateMode_OnItemEvent(evt,msgid,newitem,olditem){
	if(newitem.Type!="MODERATORITEM")return;
	
	InitModerateVars();
	vars=moderatevars;

	if(msgid=="ADDED")
	{
		var obj;
		switch(newitem.Args[5])
		{
			case "USER_MESSAGE":
				obj=new ModerateMode_USER_MESSAGE();
				break;
			default:
				return;
		}
		obj.Item=newitem;
		obj.ItemGuid=newitem.Args[1];
		obj.UserGuid=newitem.Args[2];
		obj.UserId=newitem.Args[3];
		obj.UserName=newitem.Args[4];
		obj.MsgId=newitem.Args[5];

		var row=vars.mlist.insertRow(-1);
		row._obj=obj;
		row.insertCell(-1).innerHTML=obj.GetLeftHTML();
		var messagecell=row.insertCell(-1);
		messagecell.style.width="100%";		
		messagecell.innerHTML=obj.GetRightHTML();		
		var opcell=row.insertCell(-1);
		var buttonAccept=document.createElement("img");
		buttonAccept.src=__cc_urlbase+"Images/accept.gif";
		buttonAccept.title=TEXT("Accept");
		var buttonReject=document.createElement("img");
		buttonReject.src=__cc_urlbase+"Images/reject.gif";
		buttonReject.title=TEXT("Reject");
		buttonAccept.style.marginLeft="12px";
		buttonReject.style.marginLeft="12px";
		buttonReject.style.marginRight="12px";
		opcell.appendChild(buttonAccept);
		opcell.appendChild(buttonReject);
		buttonAccept.opmode="accept";
		buttonReject.opmode="reject";
		row.onclick=ModerateMode_MList_RowClick;
	}
	if(msgid=="UPDATED")
	{
		
	}
	if(msgid=="REMOVED")
	{
		for(var i=0;i<vars.mlist.rows.length;i++)
		{
			var row=vars.mlist.rows.item(i);
			
			if(row._obj.ItemGuid==newitem.Args[1])
			{
				row.parentNode.removeChild(row);
			}
		}
	}
	
	
};

function ModerateMode_OnRawMsgEvent(evt,msgid,args)
{
	if(msgid=="POSTQUESTION")
	{
		if(!GetMyInfo().IsAdmin&&GetMyInfo().Level!="Speaker")
			return;
			
		InitModerateVars();
		vars=moderatevars;
	
		var question=args[0];
		var row=vars.qlist.insertRow(-1);
		row.question=question;
		row.insertCell(-1).innerHTML="Question:";
		row.insertCell(-1).innerHTML=question;
		var opcell=row.insertCell(-1);
		var buttonAccept=document.createElement("button");
		buttonAccept.innerHTML="Answer";
		var buttonReject=document.createElement("button");
		buttonReject.innerHTML="Ignore";
		opcell.appendChild(buttonAccept);
		opcell.appendChild(buttonReject);
		buttonAccept.opmode="answer";
		buttonReject.opmode="ignore";
		row.onclick=ModerateMode_QList_RowClick;
	}
}


function ModerateMode_MList_RowClick(event)
{
	event=window.event||event;
	var btn=event.srcElement||event.target;
	if(!btn.opmode)return;
	
	var row=this;
	var obj=row._obj;
	if(btn.opmode=="accept")
	{
		AcceptModerateItem(obj.ItemGuid);
	}
	if(btn.opmode=="reject")
	{
		RejectModerateItem(obj.ItemGuid);
	}
}
function ModerateMode_QList_RowClick(event)
{
	event=window.event||event;
	var btn=event.srcElement||event.target;
	if(!btn.opmode)return;
	
	var row=this;
	if(btn.opmode=="answer")
	{
		function HandleAnswer(msg)
		{
			if(!msg)return;
			var message=JoinToMsg("USER_COMMAND",null,["ANSWERQUESTION",row.question,msg]);
			PushCTSMessage(message);
			row.parentNode.removeChild(row);
		}
		Desktop.Prompt(HandleAnswer,row.question);
	}
	if(btn.opmode=="ignore")
	{
		row.parentNode.removeChild(row);
	}
}


AttachChatEvent("ITEM",ModerateMode_OnItemEvent);
AttachChatEvent("PLACE",ModerateMode_OnPlaceEvent);
AttachChatEvent("RAWSTCMSG",ModerateMode_OnRawMsgEvent);
