var OxOfff7=["SetStyle","length","","GetStyle","GetText",":",";","cssText","sel_font","div_font_detail","sel_fontfamily","cb_decoration_under","cb_decoration_over","cb_decoration_through","cb_style_bold","cb_style_italic","sel_fontTransform","sel_fontsize","inp_fontsize","sel_fontsize_unit","inp_color","inp_color_Preview","outer","div_demo","disabled","selectedIndex","style","value","font","fontFamily","color","backgroundColor","textDecoration","checked","overline","underline","line-through","fontWeight","bold","fontStyle","italic","fontSize","options","textTransform","font-family","overline ","underline ","line-through ","onclick"];function pause(Ox4a1){var Oxa8= new Date();var Ox4a2=Oxa8.getTime()+Ox4a1;while(true){Oxa8= new Date();if(Oxa8.getTime()>Ox4a2){return ;} ;} ;} ;function StyleClass(Ox201){var Ox4a4=[];var Ox4a5={};if(Ox201){Ox4aa();} ;this[OxOfff7[0]]=function SetStyle(name,Ox4f,Ox4a7){name=name.toLowerCase();for(var i=0;i<Ox4a4[OxOfff7[1]];i++){if(Ox4a4[i]==name){break ;} ;} ;Ox4a4[i]=name;Ox4a5[name]=Ox4f?(Ox4f+(Ox4a7||OxOfff7[2])):OxOfff7[2];} ;this[OxOfff7[3]]=function GetStyle(name){name=name.toLowerCase();return Ox4a5[name]||OxOfff7[2];} ;this[OxOfff7[4]]=function Ox4a9(){var Ox201=OxOfff7[2];for(var i=0;i<Ox4a4[OxOfff7[1]];i++){var Ox27=Ox4a4[i];var p=Ox4a5[Ox27];if(p){Ox201+=Ox27+OxOfff7[5]+p+OxOfff7[6];} ;} ;return Ox201;} ;function Ox4aa(){var arr=Ox201.split(OxOfff7[6]);for(var i=0;i<arr[OxOfff7[1]];i++){var p=arr[i].split(OxOfff7[5]);var Ox27=p[0].replace(/^\s+/g,OxOfff7[2]).replace(/\s+$/g,OxOfff7[2]).toLowerCase();Ox4a4[Ox4a4[OxOfff7[1]]]=Ox27;Ox4a5[Ox27]=p[1];} ;} ;} ;function GetStyle(Ox137,name){return  new StyleClass(Ox137.cssText).GetStyle(name);} ;function SetStyle(Ox137,name,Ox4f,Ox4ab){var Ox4ac= new StyleClass(Ox137.cssText);Ox4ac.SetStyle(name,Ox4f,Ox4ab);Ox137[OxOfff7[7]]=Ox4ac.GetText();} ;function ParseFloatToString(Ox24){var Ox8=parseFloat(Ox24);if(isNaN(Ox8)){return OxOfff7[2];} ;return Ox8+OxOfff7[2];} ;var sel_font=Window_GetElement(window,OxOfff7[8],true);var div_font_detail=Window_GetElement(window,OxOfff7[9],true);var sel_fontfamily=Window_GetElement(window,OxOfff7[10],true);var cb_decoration_under=Window_GetElement(window,OxOfff7[11],true);var cb_decoration_over=Window_GetElement(window,OxOfff7[12],true);var cb_decoration_through=Window_GetElement(window,OxOfff7[13],true);var cb_style_bold=Window_GetElement(window,OxOfff7[14],true);var cb_style_italic=Window_GetElement(window,OxOfff7[15],true);var sel_fontTransform=Window_GetElement(window,OxOfff7[16],true);var sel_fontsize=Window_GetElement(window,OxOfff7[17],true);var inp_fontsize=Window_GetElement(window,OxOfff7[18],true);var sel_fontsize_unit=Window_GetElement(window,OxOfff7[19],true);var inp_color=Window_GetElement(window,OxOfff7[20],true);var inp_color_Preview=Window_GetElement(window,OxOfff7[21],true);var outer=Window_GetElement(window,OxOfff7[22],true);var div_demo=Window_GetElement(window,OxOfff7[23],true);UpdateState=function UpdateState_Font(){inp_fontsize[OxOfff7[24]]=sel_fontsize_unit[OxOfff7[24]]=(sel_fontsize[OxOfff7[25]]>0);div_font_detail[OxOfff7[24]]=sel_font[OxOfff7[25]]>0;div_demo[OxOfff7[26]][OxOfff7[7]]=element[OxOfff7[26]][OxOfff7[7]];} ;SyncToView=function SyncToView_Font(){sel_font[OxOfff7[27]]=element[OxOfff7[26]][OxOfff7[28]].toLowerCase()||null;sel_fontfamily[OxOfff7[27]]=element[OxOfff7[26]][OxOfff7[29]];inp_color[OxOfff7[27]]=element[OxOfff7[26]][OxOfff7[30]];inp_color[OxOfff7[26]][OxOfff7[31]]=inp_color[OxOfff7[27]];var Ox5e1=element[OxOfff7[26]][OxOfff7[32]].toLowerCase();cb_decoration_over[OxOfff7[33]]=Ox5e1.indexOf(OxOfff7[34])!=-1;cb_decoration_under[OxOfff7[33]]=Ox5e1.indexOf(OxOfff7[35])!=-1;cb_decoration_through[OxOfff7[33]]=Ox5e1.indexOf(OxOfff7[36])!=-1;cb_style_bold[OxOfff7[33]]=element[OxOfff7[26]][OxOfff7[37]]==OxOfff7[38];cb_style_italic[OxOfff7[33]]=element[OxOfff7[26]][OxOfff7[39]]==OxOfff7[40];sel_fontsize[OxOfff7[27]]=element[OxOfff7[26]][OxOfff7[41]];sel_fontsize_unit[OxOfff7[25]]=0;if(sel_fontsize[OxOfff7[25]]==-1){if(ParseFloatToString(element[OxOfff7[26]].fontSize)){sel_fontsize[OxOfff7[27]]=ParseFloatToString(element[OxOfff7[26]].fontSize);for(var i=0;i<sel_fontsize_unit[OxOfff7[42]][OxOfff7[1]];i++){var Ox142=sel_fontsize_unit.options(i)[OxOfff7[27]];if(Ox142&&element[OxOfff7[26]][OxOfff7[41]].indexOf(Ox142)!=-1){sel_fontsize_unit[OxOfff7[25]]=i;break ;} ;} ;} ;} ;sel_fontTransform[OxOfff7[27]]=element[OxOfff7[26]][OxOfff7[43]];} ;SyncTo=function SyncTo_Font(element){SetStyle(element.style,OxOfff7[28],sel_font.value);if(sel_fontfamily[OxOfff7[27]]){element[OxOfff7[26]][OxOfff7[29]]=sel_fontfamily[OxOfff7[27]];} else {SetStyle(element.style,OxOfff7[44],OxOfff7[2]);} ;try{element[OxOfff7[26]][OxOfff7[30]]=inp_color[OxOfff7[27]]||OxOfff7[2];} catch(x){element[OxOfff7[26]][OxOfff7[30]]=OxOfff7[2];} ;var Ox5e3=cb_decoration_over[OxOfff7[33]];var Ox5e4=cb_decoration_under[OxOfff7[33]];var Ox5e5=cb_decoration_through[OxOfff7[33]];if(!Ox5e3&&!Ox5e4&&!Ox5e5){element[OxOfff7[26]][OxOfff7[32]]=OxOfff7[2];} else {var Ox58=OxOfff7[2];if(Ox5e3){Ox58+=OxOfff7[45];} ;if(Ox5e4){Ox58+=OxOfff7[46];} ;if(Ox5e5){Ox58+=OxOfff7[47];} ;element[OxOfff7[26]][OxOfff7[32]]=Ox58.substr(0,Ox58[OxOfff7[1]]-1);} ;element[OxOfff7[26]][OxOfff7[37]]=cb_style_bold[OxOfff7[33]]?OxOfff7[38]:OxOfff7[2];element[OxOfff7[26]][OxOfff7[39]]=cb_style_italic[OxOfff7[33]]?OxOfff7[40]:OxOfff7[2];element[OxOfff7[26]][OxOfff7[43]]=sel_fontTransform[OxOfff7[27]]||OxOfff7[2];if(sel_fontsize[OxOfff7[25]]>0){element[OxOfff7[26]][OxOfff7[41]]=sel_fontsize[OxOfff7[27]];} else {if(ParseFloatToString(inp_fontsize.value)){element[OxOfff7[26]][OxOfff7[41]]=ParseFloatToString(inp_fontsize.value)+sel_fontsize_unit[OxOfff7[27]];} else {element[OxOfff7[26]][OxOfff7[41]]=OxOfff7[2];} ;} ;} ;inp_color[OxOfff7[48]]=inp_color_Preview[OxOfff7[48]]=function inp_color_onclick(){SelectColor(inp_color,inp_color_Preview);} ;