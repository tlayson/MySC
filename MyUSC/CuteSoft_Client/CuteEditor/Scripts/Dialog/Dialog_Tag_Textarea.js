var OxOf538=["inp_name","inp_cols","inp_rows","inp_value","sel_Wrap","inp_id","inp_access","inp_index","inp_Disabled","inp_Readonly","Name","value","name","id","cols","","rows","checked","disabled","readOnly","wrap","tabIndex","accessKey","textContent"];var inp_name=Window_GetElement(window,OxOf538[0],true);var inp_cols=Window_GetElement(window,OxOf538[1],true);var inp_rows=Window_GetElement(window,OxOf538[2],true);var inp_value=Window_GetElement(window,OxOf538[3],true);var sel_Wrap=Window_GetElement(window,OxOf538[4],true);var inp_id=Window_GetElement(window,OxOf538[5],true);var inp_access=Window_GetElement(window,OxOf538[6],true);var inp_index=Window_GetElement(window,OxOf538[7],true);var inp_Disabled=Window_GetElement(window,OxOf538[8],true);var inp_Readonly=Window_GetElement(window,OxOf538[9],true);UpdateState=function UpdateState_Textarea(){} ;SyncToView=function SyncToView_Textarea(){if(element[OxOf538[10]]){inp_name[OxOf538[11]]=element[OxOf538[10]];} ;if(element[OxOf538[12]]){inp_name[OxOf538[11]]=element[OxOf538[12]];} ;inp_id[OxOf538[11]]=element[OxOf538[13]];inp_value[OxOf538[11]]=element[OxOf538[11]];if(element[OxOf538[14]]){if(element[OxOf538[14]]==20){inp_cols[OxOf538[11]]=OxOf538[15];} else {inp_cols[OxOf538[11]]=element[OxOf538[14]];} ;} ;if(element[OxOf538[16]]){if(element[OxOf538[16]]==2){inp_rows[OxOf538[11]]=OxOf538[15];} else {inp_rows[OxOf538[11]]=element[OxOf538[16]];} ;} ;inp_Disabled[OxOf538[17]]=element[OxOf538[18]];inp_Readonly[OxOf538[17]]=element[OxOf538[19]];sel_Wrap[OxOf538[11]]=element[OxOf538[20]];if(element[OxOf538[21]]==0){inp_index[OxOf538[11]]=OxOf538[15];} else {inp_index[OxOf538[11]]=element[OxOf538[21]];} ;if(element[OxOf538[22]]){inp_access[OxOf538[11]]=element[OxOf538[22]];} ;} ;SyncTo=function SyncTo_Textarea(element){element[OxOf538[12]]=inp_name[OxOf538[11]];if(element[OxOf538[10]]){element[OxOf538[10]]=inp_name[OxOf538[11]];} else {if(element[OxOf538[12]]){element.removeAttribute(OxOf538[12],0);element[OxOf538[10]]=inp_name[OxOf538[11]];} else {element[OxOf538[10]]=inp_name[OxOf538[11]];} ;} ;element[OxOf538[13]]=inp_id[OxOf538[11]];element[OxOf538[11]]=inp_value[OxOf538[11]];if(!Browser_IsWinIE()){try{element[OxOf538[23]]=inp_value[OxOf538[11]];} catch(x){} ;} ;element[OxOf538[21]]=inp_index[OxOf538[11]];element[OxOf538[18]]=inp_Disabled[OxOf538[17]];element[OxOf538[19]]=inp_Readonly[OxOf538[17]];element[OxOf538[22]]=inp_access[OxOf538[11]];if(inp_cols[OxOf538[11]]==OxOf538[15]){element[OxOf538[14]]=20;} else {element[OxOf538[14]]=inp_cols[OxOf538[11]];} ;if(inp_rows[OxOf538[11]]==OxOf538[15]){element[OxOf538[16]]=2;} else {element[OxOf538[16]]=inp_rows[OxOf538[11]];} ;try{element[OxOf538[20]]=sel_Wrap[OxOf538[11]];} catch(e){element.removeAttribute(OxOf538[20]);} ;element[OxOf538[21]]=inp_index[OxOf538[11]];if(element[OxOf538[21]]==OxOf538[15]){element.removeAttribute(OxOf538[21]);} ;if(element[OxOf538[22]]==OxOf538[15]){element.removeAttribute(OxOf538[22]);} ;} ;