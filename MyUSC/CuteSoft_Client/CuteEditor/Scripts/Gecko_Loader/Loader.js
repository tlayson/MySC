var OxOce75=["ua","userAgent","isOpera","opera","isSafari","safari","isGecko","gecko","isWinIE","msie","compatMode","document","CSS1Compat","undefined","Microsoft.XMLHTTP","readyState","onreadystatechange","","length","all","childNodes","nodeType","\x0D\x0A","caller","onchange","oninitialized","command","commandui","commandvalue","returnValue","oncommand","string","_fireEventFunction","event","parentNode","_IsCuteEditor","True","value","arguments","target","nodeName","SELECT","OPTION","readOnly","_IsRichDropDown","null","selectedIndex","TR","cells","display","style","nextSibling","innerHTML","\x3Cimg src=\x22","/Load.ashx?type=image\x26file=t-minus.gif\x22\x3E","onclick","CuteEditor_CollapseTreeDropDownItem(this,\x22","\x22)","none","/Load.ashx?type=image\x26file=t-plus.gif\x22\x3E","CuteEditor_ExpandTreeDropDownItem(this,\x22","contains","UNSELECTABLE","on","tabIndex","-1","//TODO: event not found? throw error ?","contentWindow","contentDocument","parentWindow","id","frames","frameElement","//TODO:frame contentWindow not found?","preventDefault","parent","top","opener","head","script","language","javascript","type","text/javascript","src","srcElement","//TODO: srcElement not found? throw error ?","fromElement","relatedTarget","toElement","keyCode","clientX","clientY","offsetX","offsetY","button","ctrlKey","altKey","shiftKey","cancelBubble","stopPropagation","CuteEditor_GetEditor(this).ExecImageCommand(this.getAttribute(\x27Command\x27),this.getAttribute(\x27CommandUI\x27),this.getAttribute(\x27CommandArgument\x27),this)","CuteEditor_GetEditor(this).PostBack(this.getAttribute(\x27Command\x27))","this.onmouseout();CuteEditor_GetEditor(this).DropMenu(this.getAttribute(\x27Group\x27),this)","ResourceDir","Theme","/Load.ashx?type=theme\x26theme=","\x26file=all.png","/Images/blank2020.gif","IMG","alt","title","Command","Group","ThemeIndex","width","20px","height","backgroundImage","url(",")","backgroundPosition","0 -","px","onload","className","separator","CuteEditorButton","onmouseover","CuteEditor_ButtonCommandOver(this)","onmouseout","CuteEditor_ButtonCommandOut(this)","onmousedown","CuteEditor_ButtonCommandDown(this)","onmouseup","CuteEditor_ButtonCommandUp(this)","oncontextmenu","ondragstart","PostBack","ondblclick","_ToolBarID","_CodeViewToolBarID","_FrameID"," CuteEditorFrame"," CuteEditorToolbar","cursor","no-drop","ActiveTab","Edit","Code","View","buttonInitialized","isover","CuteEditorButtonOver","CuteEditorButtonDown","CuteEditorDown","border","solid 1px #0A246A","backgroundColor","#b6bdd2","padding","1px","solid 1px #f5f5f4","inset 1px","IsCommandDisabled","CuteEditorButtonDisabled","IsCommandActive","CuteEditorButtonActive","cmd_fromfullpage","(","href","location",",DanaInfo=",",","+","scriptProperties","initfuncbecalled","GetScriptProperty","/Load.ashx?type=scripts\x26file=Gecko_Implementation\x26culture=","Culture","CuteEditorImplementation","function","POST","\x26getModified=1\x26_temp=","status","responseText","GET","\x26modified=","body","block","contentEditable","InitializeCode","inittime","CuteEditorInitialize"];var _Browser_TypeInfo=null;function Browser__InitType(){if(_Browser_TypeInfo!=null){return ;} ;var Ox4={};Ox4[OxOce75[0]]=navigator[OxOce75[1]].toLowerCase();Ox4[OxOce75[2]]=(Ox4[OxOce75[0]].indexOf(OxOce75[3])>-1);Ox4[OxOce75[4]]=(Ox4[OxOce75[0]].indexOf(OxOce75[5])>-1);Ox4[OxOce75[6]]=(!Ox4[OxOce75[2]]&&Ox4[OxOce75[0]].indexOf(OxOce75[7])>-1);Ox4[OxOce75[8]]=(!Ox4[OxOce75[2]]&&Ox4[OxOce75[0]].indexOf(OxOce75[9])>-1);_Browser_TypeInfo=Ox4;} ;Browser__InitType();function Browser_IsWinIE(){return _Browser_TypeInfo[OxOce75[8]];} ;function Browser_IsGecko(){return _Browser_TypeInfo[OxOce75[6]];} ;function Browser_IsOpera(){return _Browser_TypeInfo[OxOce75[2]];} ;function Browser_IsSafari(){return _Browser_TypeInfo[OxOce75[4]];} ;function Browser_UseIESelection(){return _Browser_TypeInfo[OxOce75[8]];} ;function Browser_IsCSS1Compat(){return window[OxOce75[11]][OxOce75[10]]==OxOce75[12];} ;function CreateXMLHttpRequest(){try{if( typeof (XMLHttpRequest)!=OxOce75[13]){return  new XMLHttpRequest();} ;if( typeof (ActiveXObject)!=OxOce75[13]){return  new ActiveXObject(OxOce75[14]);} ;} catch(x){return null;} ;} ;function LoadXMLAsync(Oxa37,Ox287,Ox234,Oxa38){var Oxe7=CreateXMLHttpRequest();function Oxa39(){if(Oxe7[OxOce75[15]]!=4){return ;} ;Oxe7[OxOce75[16]]= new Function();var Ox28f=Oxe7;Oxe7=null;if(Ox234){Ox234(Ox28f);} ;} ;Oxe7[OxOce75[16]]=Oxa39;Oxe7.open(Oxa37,Ox287,true);Oxe7.send(Oxa38||OxOce75[17]);} ;function Element_GetAllElements(p){var arr=[];if(Browser_IsWinIE()){for(var i=0;i<p[OxOce75[19]][OxOce75[18]];i++){arr.push(p[OxOce75[19]].item(i));} ;return arr;} ;Ox241(p);function Ox241(Ox29){var Ox217=Ox29[OxOce75[20]];var Ox11=Ox217[OxOce75[18]];for(var i=0;i<Ox11;i++){var Ox27=Ox217.item(i);if(Ox27[OxOce75[21]]!=1){continue ;} ;arr.push(Ox27);Ox241(Ox27);} ;} ;return arr;} ;var __ISDEBUG=false;function Debug_Todo(msg){if(!__ISDEBUG){return ;} ;throw ( new Error(msg+OxOce75[22]+Debug_Todo[OxOce75[23]]));} ;function Window_GetElement(Ox1a8,Ox9a,Ox23f){var Ox29=Ox1a8[OxOce75[11]].getElementById(Ox9a);if(Ox29){return Ox29;} ;var Ox31=Ox1a8[OxOce75[11]].getElementsByName(Ox9a);if(Ox31[OxOce75[18]]>0){return Ox31.item(0);} ;return null;} ;function CuteEditor_AddMainMenuItems(Ox667){} ;function CuteEditor_AddDropMenuItems(Ox667,Ox66e){} ;function CuteEditor_AddTagMenuItems(Ox667,Ox670){} ;function CuteEditor_AddVerbMenuItems(Ox667,Ox670){} ;function CuteEditor_OnInitialized(editor){} ;function CuteEditor_OnCommand(editor,Ox4d,Ox4e,Ox4f){} ;function CuteEditor_OnChange(editor){} ;function CuteEditor_FilterCode(editor,Ox26a){return Ox26a;} ;function CuteEditor_FilterHTML(editor,Ox282){return Ox282;} ;function CuteEditor_FireChange(editor){window.CuteEditor_OnChange(editor);CuteEditor_FireEvent(editor,OxOce75[24],null);} ;function CuteEditor_FireInitialized(editor){window.CuteEditor_OnInitialized(editor);CuteEditor_FireEvent(editor,OxOce75[25],null);} ;function CuteEditor_FireCommand(editor,Ox4d,Ox4e,Ox4f){var Ox13e=window.CuteEditor_OnCommand(editor,Ox4d,Ox4e,Ox4f);if(Ox13e==true){return true;} ;var Ox679={};Ox679[OxOce75[26]]=Ox4d;Ox679[OxOce75[27]]=Ox4e;Ox679[OxOce75[28]]=Ox4f;Ox679[OxOce75[29]]=true;CuteEditor_FireEvent(editor,OxOce75[30],Ox679);if(Ox679[OxOce75[29]]==false){return true;} ;} ;function CuteEditor_FireEvent(editor,Ox67b,Ox67c){if(Ox67c==null){Ox67c={};} ;var Ox67d=editor.getAttribute(Ox67b);if(Ox67d){if( typeof (Ox67d)==OxOce75[31]){editor[OxOce75[32]]= new Function(OxOce75[33],Ox67d);} else {editor[OxOce75[32]]=Ox67d;} ;editor._fireEventFunction(Ox67c);} ;} ;function CuteEditor_GetEditor(element){for(var Ox43=element;Ox43!=null;Ox43=Ox43[OxOce75[34]]){if(Ox43.getAttribute(OxOce75[35])==OxOce75[36]){return Ox43;} ;} ;return null;} ;function CuteEditor_DropDownCommand(element,Oxa3b){var Ox142=element[OxOce75[37]];if(CuteEditor_DropDownCommand[OxOce75[23]]){var Ox43=CuteEditor_DropDownCommand[OxOce75[23]][OxOce75[38]][0];if(Ox43&&Ox43[OxOce75[39]]){if(Ox43[OxOce75[39]][OxOce75[40]]==OxOce75[41]){return ;} ;if(Ox43[OxOce75[39]][OxOce75[40]]==OxOce75[42]){Ox142=Ox43[OxOce75[39]][OxOce75[37]];} ;} ;} ;var editor=CuteEditor_GetEditor(element);if(editor[OxOce75[43]]){return ;} ;if(element.getAttribute(OxOce75[44])==OxOce75[36]){var Ox142=element.GetValue();if(Ox142==OxOce75[45]){Ox142=OxOce75[17];} ;var Ox201=element.GetText();if(Ox201==OxOce75[45]){Ox201=OxOce75[17];} ;element.SetSelectedIndex(0);editor.ExecCommand(Oxa3b,false,Ox142,Ox201);} else {if(Ox142){if(Ox142==OxOce75[45]){Ox142=OxOce75[17];} ;element[OxOce75[46]]=0;editor.ExecCommand(Oxa3b,false,Ox142,Ox201);} else {element[OxOce75[46]]=0;} ;} ;editor.FocusDocument();} ;function CuteEditor_ExpandTreeDropDownItem(src,Ox73d){var Oxba=null;while(src!=null){if(src[OxOce75[40]]==OxOce75[47]){Oxba=src;break ;} ;src=src[OxOce75[34]];} ;var Ox1e4=Oxba[OxOce75[48]].item(0);Oxba[OxOce75[51]][OxOce75[50]][OxOce75[49]]=OxOce75[17];Ox1e4[OxOce75[52]]=OxOce75[53]+Ox73d+OxOce75[54];Oxba[OxOce75[55]]= new Function(OxOce75[56]+Ox73d+OxOce75[57]);} ;function CuteEditor_CollapseTreeDropDownItem(src,Ox73d){var Oxba=null;while(src!=null){if(src[OxOce75[40]]==OxOce75[47]){Oxba=src;break ;} ;src=src[OxOce75[34]];} ;var Ox1e4=Oxba[OxOce75[48]].item(0);Oxba[OxOce75[51]][OxOce75[50]][OxOce75[49]]=OxOce75[58];Ox1e4[OxOce75[52]]=OxOce75[53]+Ox73d+OxOce75[59];Oxba[OxOce75[55]]= new Function(OxOce75[60]+Ox73d+OxOce75[57]);} ;function Element_Contains(element,Ox183){if(!Browser_IsOpera()){if(element[OxOce75[61]]){return element.contains(Ox183);} ;} ;for(;Ox183!=null;Ox183=Ox183[OxOce75[34]]){if(element==Ox183){return true;} ;} ;return false;} ;function Element_SetUnselectable(element){element.setAttribute(OxOce75[62],OxOce75[63]);element.setAttribute(OxOce75[64],OxOce75[65]);var arr=Element_GetAllElements(element);var len=arr[OxOce75[18]];if(!len){return ;} ;for(var i=0;i<len;i++){arr[i].setAttribute(OxOce75[62],OxOce75[63]);arr[i].setAttribute(OxOce75[64],OxOce75[65]);} ;} ;function Event_GetEvent(Ox244){Ox244=Event_FindEvent(Ox244);if(Ox244==null){Debug_Todo(OxOce75[66]);} ;return Ox244;} ;function Frame_GetContentWindow(Ox348){if(Ox348[OxOce75[67]]){return Ox348[OxOce75[67]];} ;if(Ox348[OxOce75[68]]){if(Ox348[OxOce75[68]][OxOce75[69]]){return Ox348[OxOce75[68]][OxOce75[69]];} ;} ;var Ox1a8;if(Ox348[OxOce75[70]]){Ox1a8=window[OxOce75[71]][Ox348[OxOce75[70]]];if(Ox1a8){return Ox1a8;} ;} ;var len=window[OxOce75[71]][OxOce75[18]];for(var i=0;i<len;i++){Ox1a8=window[OxOce75[71]][i];if(Ox1a8[OxOce75[72]]==Ox348){return Ox1a8;} ;if(Ox1a8[OxOce75[11]]==Ox348[OxOce75[68]]){return Ox1a8;} ;} ;Debug_Todo(OxOce75[73]);} ;function Array_IndexOf(arr,Ox246){for(var i=0;i<arr[OxOce75[18]];i++){if(arr[i]==Ox246){return i;} ;} ;return -1;} ;function Array_Contains(arr,Ox246){return Array_IndexOf(arr,Ox246)!=-1;} ;function Event_FindEvent(Ox244){if(Ox244&&Ox244[OxOce75[74]]){return Ox244;} ;if(Browser_IsGecko()){return Event_FindEvent_FindEventFromCallers();} else {if(window[OxOce75[33]]){return window[OxOce75[33]];} ;return Event_FindEvent_FindEventFromWindows();} ;return null;} ;function Event_FindEvent_FindEventFromCallers(){var Ox18f=Event_GetEvent[OxOce75[23]];for(var i=0;i<100;i++){if(!Ox18f){break ;} ;var Ox244=Ox18f[OxOce75[38]][0];if(Ox244&&Ox244[OxOce75[74]]){return Ox244;} ;Ox18f=Ox18f[OxOce75[23]];} ;} ;function Event_FindEvent_FindEventFromWindows(){var arr=[];return Ox24d(window);function Ox24d(Ox1a8){if(Ox1a8==null){return null;} ;if(Ox1a8[OxOce75[33]]){return Ox1a8[OxOce75[33]];} ;if(Array_Contains(arr,Ox1a8)){return null;} ;arr.push(Ox1a8);var Ox24e=[];if(Ox1a8[OxOce75[75]]!=Ox1a8){Ox24e.push(Ox1a8.parent);} ;if(Ox1a8[OxOce75[76]]!=Ox1a8[OxOce75[75]]){Ox24e.push(Ox1a8.top);} ;if(Ox1a8[OxOce75[77]]){Ox24e.push(Ox1a8.opener);} ;for(var i=0;i<Ox1a8[OxOce75[71]][OxOce75[18]];i++){Ox24e.push(Ox1a8[OxOce75[71]][i]);} ;for(var i=0;i<Ox24e[OxOce75[18]];i++){try{var Ox244=Ox24d(Ox24e[i]);if(Ox244){return Ox244;} ;} catch(x){} ;} ;return null;} ;} ;function include(Oxc9,Ox287){var Ox288=document.getElementsByTagName(OxOce75[78]).item(0);var Ox289=document.getElementById(Oxc9);if(Ox289){Ox288.removeChild(Ox289);} ;var Ox28a=document.createElement(OxOce75[79]);Ox28a.setAttribute(OxOce75[80],OxOce75[81]);Ox28a.setAttribute(OxOce75[82],OxOce75[83]);Ox28a.setAttribute(OxOce75[84],Ox287);Ox28a.setAttribute(OxOce75[70],Oxc9);Ox288.appendChild(Ox28a);} ;function Event_GetSrcElement(Ox244){Ox244=Event_GetEvent(Ox244);if(Ox244[OxOce75[85]]){return Ox244[OxOce75[85]];} ;if(Ox244[OxOce75[39]]){return Ox244[OxOce75[39]];} ;Debug_Todo(OxOce75[86]);return null;} ;function Event_GetFromElement(Ox244){Ox244=Event_GetEvent(Ox244);if(Ox244[OxOce75[87]]){return Ox244[OxOce75[87]];} ;if(Ox244[OxOce75[88]]){return Ox244[OxOce75[88]];} ;return null;} ;function Event_GetToElement(Ox244){Ox244=Event_GetEvent(Ox244);if(Ox244[OxOce75[89]]){return Ox244[OxOce75[89]];} ;if(Ox244[OxOce75[88]]){return Ox244[OxOce75[88]];} ;return null;} ;function Event_GetKeyCode(Ox244){Ox244=Event_GetEvent(Ox244);return Ox244[OxOce75[90]];} ;function Event_GetClientX(Ox244){Ox244=Event_GetEvent(Ox244);return Ox244[OxOce75[91]];} ;function Event_GetClientY(Ox244){Ox244=Event_GetEvent(Ox244);return Ox244[OxOce75[92]];} ;function Event_GetOffsetX(Ox244){Ox244=Event_GetEvent(Ox244);return Ox244[OxOce75[93]];} ;function Event_GetOffsetY(Ox244){Ox244=Event_GetEvent(Ox244);return Ox244[OxOce75[94]];} ;function Event_IsLeftButton(Ox244){Ox244=Event_GetEvent(Ox244);if(Browser_IsWinIE()){return Ox244[OxOce75[95]]==1;} ;if(Browser_IsGecko()){return Ox244[OxOce75[95]]==0;} ;return Ox244[OxOce75[95]]==0;} ;function Event_IsCtrlKey(Ox244){Ox244=Event_GetEvent(Ox244);return Ox244[OxOce75[96]];} ;function Event_IsAltKey(Ox244){Ox244=Event_GetEvent(Ox244);return Ox244[OxOce75[97]];} ;function Event_IsShiftKey(Ox244){Ox244=Event_GetEvent(Ox244);return Ox244[OxOce75[98]];} ;function Event_PreventDefault(Ox244){Ox244=Event_GetEvent(Ox244);Ox244[OxOce75[29]]=false;if(Ox244[OxOce75[74]]){Ox244.preventDefault();} ;} ;function Event_CancelBubble(Ox244){Ox244=Event_GetEvent(Ox244);Ox244[OxOce75[99]]=true;if(Ox244[OxOce75[100]]){Ox244.stopPropagation();} ;return false;} ;function Event_CancelEvent(Ox244){Ox244=Event_GetEvent(Ox244);Event_PreventDefault(Ox244);return Event_CancelBubble(Ox244);} ;function CuteEditor_BasicInitialize(editor){var Ox158=Browser_IsOpera();var Ox706= new Function(OxOce75[101]);var Oxa3f= new Function(OxOce75[102]);var Oxa40= new Function(OxOce75[103]);var Oxa41=editor.GetScriptProperty(OxOce75[104]);var Oxa42=editor.GetScriptProperty(OxOce75[105]);var Oxa43=Oxa41+OxOce75[106]+Oxa42+OxOce75[107];var Oxa44=Oxa41+OxOce75[108];var images=editor.getElementsByTagName(OxOce75[109]);var len=images[OxOce75[18]];for(var i=0;i<len;i++){var img=images[i];if(img.getAttribute(OxOce75[110])&&!img.getAttribute(OxOce75[111])){img.setAttribute(OxOce75[111],img.getAttribute(OxOce75[110]));} ;var Ox135=img.getAttribute(OxOce75[112]);var Ox66e=img.getAttribute(OxOce75[113]);if(!(Ox135||Ox66e)){continue ;} ;var Oxa45=img.getAttribute(OxOce75[114]);if(parseInt(Oxa45)>=0){img[OxOce75[50]][OxOce75[115]]=OxOce75[116];img[OxOce75[50]][OxOce75[117]]=OxOce75[116];img[OxOce75[84]]=Oxa44;img[OxOce75[50]][OxOce75[118]]=OxOce75[119]+Oxa43+OxOce75[120];img[OxOce75[50]][OxOce75[121]]=OxOce75[122]+(Oxa45*20)+OxOce75[123];img[OxOce75[50]][OxOce75[49]]=OxOce75[17];} ;if(!Ox135&&!Ox66e){if(Ox158){img[OxOce75[124]]=CuteEditor_OperaHandleImageLoaded;} ;continue ;} ;if(img[OxOce75[125]]!=OxOce75[126]){img[OxOce75[125]]=OxOce75[127];img[OxOce75[128]]= new Function(OxOce75[129]);img[OxOce75[130]]= new Function(OxOce75[131]);img[OxOce75[132]]= new Function(OxOce75[133]);img[OxOce75[134]]= new Function(OxOce75[135]);} ;if(!img[OxOce75[136]]){img[OxOce75[136]]=Event_CancelEvent;} ;if(!img[OxOce75[137]]){img[OxOce75[137]]=Event_CancelEvent;} ;if(Ox135){var Ox18f=img.getAttribute(OxOce75[138])==OxOce75[36]?Oxa3f:Ox706;if(img[OxOce75[55]]==null){img[OxOce75[55]]=Ox18f;} ;if(img[OxOce75[139]]==null){img[OxOce75[139]]=Ox18f;} ;} else {if(Ox66e){if(img[OxOce75[55]]==null){img[OxOce75[55]]=Oxa40;} ;} ;} ;} ;var Ox773=Window_GetElement(window,editor.GetScriptProperty(OxOce75[140]),true);var Ox774=Window_GetElement(window,editor.GetScriptProperty(OxOce75[141]),true);var Ox76f=Window_GetElement(window,editor.GetScriptProperty(OxOce75[142]),true);Ox76f[OxOce75[125]]+=OxOce75[143];Ox773[OxOce75[125]]+=OxOce75[144];Ox774[OxOce75[125]]+=OxOce75[144];Element_SetUnselectable(Ox773);Element_SetUnselectable(Ox774);try{editor[OxOce75[50]][OxOce75[145]]=OxOce75[146];} catch(x){} ;var Ox7f9=editor.GetScriptProperty(OxOce75[147]);switch(Ox7f9){case OxOce75[148]:Ox773[OxOce75[50]][OxOce75[49]]=OxOce75[17];break ;;case OxOce75[149]:Ox774[OxOce75[50]][OxOce75[49]]=OxOce75[17];break ;;case OxOce75[150]:break ;;} ;} ;function CuteEditor_OperaHandleImageLoaded(){var img=this;if(img[OxOce75[50]][OxOce75[49]]){img[OxOce75[50]][OxOce75[49]]=OxOce75[58];setTimeout(function Oxa47(){img[OxOce75[50]][OxOce75[49]]=OxOce75[17];} ,1);} ;} ;function CuteEditor_ButtonOver(element){if(!element[OxOce75[151]]){element[OxOce75[136]]=Event_CancelEvent;element[OxOce75[130]]=CuteEditor_ButtonOut;element[OxOce75[132]]=CuteEditor_ButtonDown;element[OxOce75[134]]=CuteEditor_ButtonUp;Element_SetUnselectable(element);element[OxOce75[151]]=true;} ;element[OxOce75[152]]=true;element[OxOce75[125]]=OxOce75[153];} ;function CuteEditor_ButtonOut(){var element=this;element[OxOce75[125]]=OxOce75[127];element[OxOce75[152]]=false;} ;function CuteEditor_ButtonDown(){if(!Event_IsLeftButton()){return ;} ;var element=this;element[OxOce75[125]]=OxOce75[154];} ;function CuteEditor_ButtonUp(){if(!Event_IsLeftButton()){return ;} ;var element=this;if(element[OxOce75[152]]){element[OxOce75[125]]=OxOce75[153];} else {element[OxOce75[125]]=OxOce75[155];} ;} ;function CuteEditor_ColorPicker_ButtonOver(element){if(!element[OxOce75[151]]){element[OxOce75[136]]=Event_CancelEvent;element[OxOce75[130]]=CuteEditor_ColorPicker_ButtonOut;element[OxOce75[132]]=CuteEditor_ColorPicker_ButtonDown;Element_SetUnselectable(element);element[OxOce75[151]]=true;} ;element[OxOce75[152]]=true;element[OxOce75[50]][OxOce75[156]]=OxOce75[157];element[OxOce75[50]][OxOce75[158]]=OxOce75[159];element[OxOce75[50]][OxOce75[160]]=OxOce75[161];} ;function CuteEditor_ColorPicker_ButtonOut(){var element=this;element[OxOce75[152]]=false;element[OxOce75[50]][OxOce75[156]]=OxOce75[162];element[OxOce75[50]][OxOce75[158]]=OxOce75[17];element[OxOce75[50]][OxOce75[160]]=OxOce75[161];} ;function CuteEditor_ColorPicker_ButtonDown(){var element=this;element[OxOce75[50]][OxOce75[156]]=OxOce75[163];element[OxOce75[50]][OxOce75[158]]=OxOce75[17];element[OxOce75[50]][OxOce75[160]]=OxOce75[161];} ;function CuteEditor_ButtonCommandOver(element){element[OxOce75[152]]=true;if(element[OxOce75[164]]){element[OxOce75[125]]=OxOce75[165];} else {element[OxOce75[125]]=OxOce75[153];} ;} ;function CuteEditor_ButtonCommandOut(element){element[OxOce75[152]]=false;if(element[OxOce75[166]]){element[OxOce75[125]]=OxOce75[167];} else {if(element[OxOce75[164]]){element[OxOce75[125]]=OxOce75[165];} else {if(element[OxOce75[70]]!=OxOce75[168]){element[OxOce75[125]]=OxOce75[127];} ;} ;} ;} ;function CuteEditor_ButtonCommandDown(element){if(!Event_IsLeftButton()){return ;} ;element[OxOce75[125]]=OxOce75[154];} ;function CuteEditor_ButtonCommandUp(element){if(!Event_IsLeftButton()){return ;} ;if(element[OxOce75[164]]){element[OxOce75[125]]=OxOce75[165];return ;} ;if(element[OxOce75[152]]){element[OxOce75[125]]=OxOce75[153];} else {if(element[OxOce75[166]]){element[OxOce75[125]]=OxOce75[167];} else {element[OxOce75[125]]=OxOce75[127];} ;} ;} ;var CuteEditorGlobalFunctions=[CuteEditor_GetEditor,CuteEditor_ButtonOver,CuteEditor_ButtonOut,CuteEditor_ButtonDown,CuteEditor_ButtonUp,CuteEditor_ColorPicker_ButtonOver,CuteEditor_ColorPicker_ButtonOut,CuteEditor_ColorPicker_ButtonDown,CuteEditor_ButtonCommandOver,CuteEditor_ButtonCommandOut,CuteEditor_ButtonCommandDown,CuteEditor_ButtonCommandUp,CuteEditor_DropDownCommand,CuteEditor_ExpandTreeDropDownItem,CuteEditor_CollapseTreeDropDownItem,CuteEditor_OnInitialized,CuteEditor_OnCommand,CuteEditor_OnChange,CuteEditor_AddVerbMenuItems,CuteEditor_AddTagMenuItems,CuteEditor_AddMainMenuItems,CuteEditor_AddDropMenuItems,CuteEditor_FilterCode,CuteEditor_FilterHTML];function SetupCuteEditorGlobalFunctions(){for(var i=0;i<CuteEditorGlobalFunctions[OxOce75[18]];i++){var Ox18f=CuteEditorGlobalFunctions[i];var name=Ox18f+OxOce75[17];name=name.substr(8,name.indexOf(OxOce75[169])-8).replace(/\s/g,OxOce75[17]);if(!window[name]){window[name]=Ox18f;} ;} ;} ;SetupCuteEditorGlobalFunctions();var __danainfo=null;var danaurl=window[OxOce75[171]][OxOce75[170]];var danapos=danaurl.indexOf(OxOce75[172]);if(danapos!=-1){var pluspos1=danaurl.indexOf(OxOce75[173],danapos+10);var pluspos2=danaurl.indexOf(OxOce75[174],danapos+10);if(pluspos1!=-1&&pluspos1<pluspos2){pluspos2=pluspos1;} ;__danainfo=danaurl.substring(danapos,pluspos2)+OxOce75[174];} ;function CuteEditor_GetScriptProperty(name){var Ox142=this[OxOce75[175]][name];if(Ox142&&__danainfo){if(name==OxOce75[104]){return Ox142+__danainfo;} ;var Ox381=this[OxOce75[175]][OxOce75[104]];if(Ox142.substr(0,Ox381.length)==Ox381){return Ox381+__danainfo+Ox142.substring(Ox381.length);} ;} ;return Ox142;} ;function CuteEditor_SetScriptProperty(name,Ox142){if(Ox142==null){this[OxOce75[175]][name]=null;} else {this[OxOce75[175]][name]=String(Ox142);} ;} ;function CuteEditorInitialize(Oxa52,Oxa53){var editor=Window_GetElement(window,Oxa52,true);if(editor[OxOce75[176]]){return ;} ;editor[OxOce75[176]]=1;editor[OxOce75[175]]=Oxa53;editor[OxOce75[177]]=CuteEditor_GetScriptProperty;var Ox76f=Window_GetElement(window,editor.GetScriptProperty(OxOce75[142]),true);var editwin,editdoc;try{editwin=Frame_GetContentWindow(Ox76f);editdoc=editwin[OxOce75[11]];} catch(x){} ;var Oxa54=false;var Oxa55;var Oxa56=false;var Oxa57=editor.GetScriptProperty(OxOce75[104])+OxOce75[178]+editor.GetScriptProperty(OxOce75[179]);function Oxa58(){if( typeof (window[OxOce75[180]])==OxOce75[181]){return ;} ;LoadXMLAsync(OxOce75[182],Oxa57+OxOce75[183]+ new Date().getTime(),Oxa59);} ;function Oxa59(Ox28f){var Ox883= new Date().getTime();if(Ox28f[OxOce75[184]]!=200){} else {Ox883=Ox28f[OxOce75[185]];} ;LoadXMLAsync(OxOce75[186],Oxa57+OxOce75[187]+Ox883,Oxa5a);} ;function Oxa5a(Ox28f){if(Ox28f[OxOce75[184]]!=200){return ;} ;CuteEditorInstallScriptCode(Ox28f.responseText,OxOce75[180]);if(Oxa54){Oxa5b();} ;} ;function Oxa5b(){if(Oxa56){return ;} ;Oxa56=true;try{editor[OxOce75[50]][OxOce75[145]]=OxOce75[17];} catch(x){} ;try{editdoc[OxOce75[188]][OxOce75[50]][OxOce75[145]]=OxOce75[17];} catch(x){} ;Ox76f[OxOce75[50]][OxOce75[49]]=OxOce75[189];if(Browser_IsOpera()){editdoc[OxOce75[188]][OxOce75[190]]=true;} else {} ;window.CuteEditorImplementation(editor);var Oxa5c=editor.GetScriptProperty(OxOce75[191]);if(Oxa5c){editor.Eval(Oxa5c);} ;} ;function Oxa5d(){if(!Element_Contains(window[OxOce75[11]].body,editor)){return ;} ;document[OxOce75[111]]=OxOce75[17];try{Ox76f=Window_GetElement(window,editor.GetScriptProperty(OxOce75[142]),true);if(!Ox76f.getAttribute(OxOce75[192])){Ox76f.setAttribute(OxOce75[192], new Date().getTime());} ;editwin=Frame_GetContentWindow(Ox76f);editdoc=editwin[OxOce75[11]];var y=editdoc[OxOce75[188]];} catch(x){if(Ox76f!=null){Ox76f.setAttribute(OxOce75[84],Ox76f.src);} ;setTimeout(Oxa5d,100);return ;} ;if(!editdoc[OxOce75[188]]){setTimeout(Oxa5d,100);return ;} ;if(!Oxa54){Oxa54=true;setTimeout(Oxa5d,50);return ;} ;if( typeof (window[OxOce75[180]])==OxOce75[181]){Oxa5b();} else {try{editdoc[OxOce75[188]][OxOce75[50]][OxOce75[145]]=OxOce75[146];} catch(x){} ;} ;} ;var Oxa5e=0;var Ox43=CuteEditor_Find_DisplayNone(editor);if(Ox43){function Oxa5f(){if(Ox43[OxOce75[50]][OxOce75[49]]!=OxOce75[58]){window.clearInterval(Oxa5e);Oxa5e=OxOce75[17];editor[OxOce75[176]]=false;CuteEditorInitialize(Oxa52,Oxa53);} ;} ;Oxa5e=setInterval(Oxa5f,1000);} else {CuteEditor_BasicInitialize(editor);Oxa58();Oxa5d();} ;function CuteEditor_Find_DisplayNone(element){var Oxa61;for(var Ox43=element;Ox43!=null;Ox43=Ox43[OxOce75[34]]){if(Ox43[OxOce75[50]]&&Ox43[OxOce75[50]][OxOce75[49]]==OxOce75[58]){Oxa61=Ox43;break ;} ;} ;return Oxa61;} ;} ;function CuteEditorInstallScriptCode(Ox9b6,Ox9b7){eval(Ox9b6);window[Ox9b7]=eval(Ox9b7);} ;window[OxOce75[193]]=CuteEditorInitialize;