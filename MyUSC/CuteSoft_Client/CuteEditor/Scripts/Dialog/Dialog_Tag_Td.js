var OxO5bb2=["inp_width","inp_height","sel_align","sel_valign","inp_bgColor","inp_borderColor","inp_borderColorLight","inp_borderColorDark","inp_class","inp_id","inp_tooltip","sel_noWrap","sel_CellScope","value","bgColor","backgroundColor","style","id","borderColor","borderColorLight","borderColorDark","className","width","height","align","vAlign","title","noWrap","scope","[[ValidNumber]]","[[ValidID]]","","class","valign","onclick"];var inp_width=Window_GetElement(window,OxO5bb2[0],true);var inp_height=Window_GetElement(window,OxO5bb2[1],true);var sel_align=Window_GetElement(window,OxO5bb2[2],true);var sel_valign=Window_GetElement(window,OxO5bb2[3],true);var inp_bgColor=Window_GetElement(window,OxO5bb2[4],true);var inp_borderColor=Window_GetElement(window,OxO5bb2[5],true);var inp_borderColorLight=Window_GetElement(window,OxO5bb2[6],true);var inp_borderColorDark=Window_GetElement(window,OxO5bb2[7],true);var inp_class=Window_GetElement(window,OxO5bb2[8],true);var inp_id=Window_GetElement(window,OxO5bb2[9],true);var inp_tooltip=Window_GetElement(window,OxO5bb2[10],true);var sel_noWrap=Window_GetElement(window,OxO5bb2[11],true);var sel_CellScope=Window_GetElement(window,OxO5bb2[12],true);SyncToView=function SyncToView_Td(){inp_bgColor[OxO5bb2[13]]=element.getAttribute(OxO5bb2[14])||element[OxO5bb2[16]][OxO5bb2[15]];inp_id[OxO5bb2[13]]=element.getAttribute(OxO5bb2[17]);inp_bgColor[OxO5bb2[16]][OxO5bb2[15]]=inp_bgColor[OxO5bb2[13]];inp_borderColor[OxO5bb2[13]]=element.getAttribute(OxO5bb2[18]);inp_borderColor[OxO5bb2[16]][OxO5bb2[15]]=inp_borderColor[OxO5bb2[13]];inp_borderColorLight[OxO5bb2[13]]=element.getAttribute(OxO5bb2[19]);inp_borderColorLight[OxO5bb2[16]][OxO5bb2[15]]=inp_borderColorLight[OxO5bb2[13]];inp_borderColorDark[OxO5bb2[13]]=element.getAttribute(OxO5bb2[20]);inp_borderColorDark[OxO5bb2[16]][OxO5bb2[15]]=inp_borderColorDark[OxO5bb2[13]];inp_class[OxO5bb2[13]]=element[OxO5bb2[21]];inp_width[OxO5bb2[13]]=element.getAttribute(OxO5bb2[22])||element[OxO5bb2[16]][OxO5bb2[22]];inp_height[OxO5bb2[13]]=element.getAttribute(OxO5bb2[23])||element[OxO5bb2[16]][OxO5bb2[23]];sel_align[OxO5bb2[13]]=element.getAttribute(OxO5bb2[24]);sel_valign[OxO5bb2[13]]=element.getAttribute(OxO5bb2[25]);inp_tooltip[OxO5bb2[13]]=element.getAttribute(OxO5bb2[26]);sel_noWrap[OxO5bb2[13]]=element.getAttribute(OxO5bb2[27]);sel_CellScope[OxO5bb2[13]]=element.getAttribute(OxO5bb2[28]);} ;SyncTo=function SyncTo_Td(element){if(inp_bgColor[OxO5bb2[13]]){if(element[OxO5bb2[16]][OxO5bb2[15]]){element[OxO5bb2[16]][OxO5bb2[15]]=inp_bgColor[OxO5bb2[13]];} else {element[OxO5bb2[14]]=inp_bgColor[OxO5bb2[13]];} ;} else {element.removeAttribute(OxO5bb2[14]);} ;element[OxO5bb2[18]]=inp_borderColor[OxO5bb2[13]];element[OxO5bb2[19]]=inp_borderColorLight[OxO5bb2[13]];element[OxO5bb2[20]]=inp_borderColorDark[OxO5bb2[13]];element[OxO5bb2[21]]=inp_class[OxO5bb2[13]];if(element[OxO5bb2[16]][OxO5bb2[22]]||element[OxO5bb2[16]][OxO5bb2[23]]){try{element[OxO5bb2[16]][OxO5bb2[22]]=inp_width[OxO5bb2[13]];element[OxO5bb2[16]][OxO5bb2[23]]=inp_height[OxO5bb2[13]];} catch(er){alert(OxO5bb2[29]);} ;} else {try{element[OxO5bb2[22]]=inp_width[OxO5bb2[13]];element[OxO5bb2[23]]=inp_height[OxO5bb2[13]];} catch(er){alert(OxO5bb2[29]);} ;} ;var Ox375=/[^a-z\d]/i;if(Ox375.test(inp_id.value)){alert(OxO5bb2[30]);return ;} ;element[OxO5bb2[24]]=sel_align[OxO5bb2[13]];element[OxO5bb2[17]]=inp_id[OxO5bb2[13]];element[OxO5bb2[25]]=sel_valign[OxO5bb2[13]];element[OxO5bb2[27]]=sel_noWrap[OxO5bb2[13]];element[OxO5bb2[26]]=inp_tooltip[OxO5bb2[13]];element[OxO5bb2[28]]=sel_CellScope[OxO5bb2[13]];if(element[OxO5bb2[17]]==OxO5bb2[31]){element.removeAttribute(OxO5bb2[17]);} ;if(element[OxO5bb2[28]]==OxO5bb2[31]){element.removeAttribute(OxO5bb2[28]);} ;if(element[OxO5bb2[27]]==OxO5bb2[31]){element.removeAttribute(OxO5bb2[27]);} ;if(element[OxO5bb2[14]]==OxO5bb2[31]){element.removeAttribute(OxO5bb2[14]);} ;if(element[OxO5bb2[18]]==OxO5bb2[31]){element.removeAttribute(OxO5bb2[18]);} ;if(element[OxO5bb2[19]]==OxO5bb2[31]){element.removeAttribute(OxO5bb2[19]);} ;if(element[OxO5bb2[7]]==OxO5bb2[31]){element.removeAttribute(OxO5bb2[7]);} ;if(element[OxO5bb2[21]]==OxO5bb2[31]){element.removeAttribute(OxO5bb2[21]);} ;if(element[OxO5bb2[21]]==OxO5bb2[31]){element.removeAttribute(OxO5bb2[32]);} ;if(element[OxO5bb2[24]]==OxO5bb2[31]){element.removeAttribute(OxO5bb2[24]);} ;if(element[OxO5bb2[25]]==OxO5bb2[31]){element.removeAttribute(OxO5bb2[33]);} ;if(element[OxO5bb2[26]]==OxO5bb2[31]){element.removeAttribute(OxO5bb2[26]);} ;if(element[OxO5bb2[22]]==OxO5bb2[31]){element.removeAttribute(OxO5bb2[22]);} ;if(element[OxO5bb2[23]]==OxO5bb2[31]){element.removeAttribute(OxO5bb2[23]);} ;} ;inp_borderColor[OxO5bb2[34]]=function inp_borderColor_onclick(){SelectColor(inp_borderColor);} ;inp_bgColor[OxO5bb2[34]]=function inp_bgColor_onclick(){SelectColor(inp_bgColor);} ;inp_borderColorLight[OxO5bb2[34]]=function inp_borderColorLight_onclick(){SelectColor(inp_borderColorLight);} ;inp_borderColorDark[OxO5bb2[34]]=function inp_borderColorDark_onclick(){SelectColor(inp_borderColorDark);} ;