var OxO79d8=["stringSearch","stringReplace","MatchWholeWord","MatchCase","document","checked","length","value","Nothing to search.","selection","body","type","Control","text","Finished Searching the document. Would you like to start again from the top?","","textedit","[[WordNotFound]]","[[WordReplaced]] : ","Please use replace funtion."];var editwin=Window_GetDialogArguments(window);var stringSearch=Window_GetElement(window,OxO79d8[0],true);var stringReplace=Window_GetElement(window,OxO79d8[1],true);var MatchWholeWord=Window_GetElement(window,OxO79d8[2],true);var MatchCase=Window_GetElement(window,OxO79d8[3],true);var editdoc=editwin[OxO79d8[4]];function get_ie_matchtype(){var Ox310=0;var Ox311=0;var Ox312=0;if(MatchCase[OxO79d8[5]]){Ox311=4;} ;if(MatchWholeWord[OxO79d8[5]]){Ox312=2;} ;Ox310=Ox311+Ox312;return (Ox310);} ;function checkInputString(){if(stringSearch[OxO79d8[7]][OxO79d8[6]]<1){alert(OxO79d8[8]);return false;} else {return true;} ;} ;function IsMatchSearchValue(Ox24){if(!Ox24){return false;} ;if(stringSearch[OxO79d8[7]]==Ox24){return true;} ;if(MatchCase[OxO79d8[5]]){return false;} ;return stringSearch[OxO79d8[7]].toLowerCase()==Ox24.toLowerCase();} ;var _ie_range=null;function IE_Restore(){editwin.focus();if(_ie_range!=null){_ie_range.select();} ;} ;function IE_Save(){editwin.focus();_ie_range=editdoc[OxO79d8[9]].createRange();} ;function MoveToBodyStart(){if(Browser_UseIESelection()){range=document[OxO79d8[10]].createTextRange();range.collapse(true);range.select();IE_Save();} else {editwin.getSelection().collapse(editdoc.body,0);} ;} ;function DoFind(){if(Browser_UseIESelection()){IE_Restore();var Ox136=editdoc[OxO79d8[9]];if(Ox136[OxO79d8[11]]==OxO79d8[12]){MoveToBodyStart();} ;var Ox228=Ox136.createRange();Ox228.collapse(false);if(Ox228.findText(stringSearch.value,1000000000,get_ie_matchtype())){Ox228.select();IE_Save();return true;} ;} else {var Ox228=editwin.getSelection().getRangeAt(0);if(editwin.find(stringSearch.value,MatchCase.checked,false,false,MatchWholeWord.checked,false,false)){return true;} ;} ;} ;function DoReplace(){if(Browser_UseIESelection()){IE_Restore();var Ox136=editdoc[OxO79d8[9]];if(Ox136[OxO79d8[11]]!=OxO79d8[12]){var Ox228=Ox136.createRange();if(IsMatchSearchValue(Ox228.text)){Ox228[OxO79d8[13]]=stringReplace[OxO79d8[7]];Ox228.collapse(false);IE_Save();return true;} ;} ;} else {var Ox136=editwin.getSelection();if(IsMatchSearchValue(Ox136.toString())){Ox136.deleteFromDocument();Ox136.getRangeAt(0).insertNode(editdoc.createTextNode(stringReplace.value));Ox136.getRangeAt(0).collapse(false);return true;} ;} ;return false;} ;function FindTxt(){if(!checkInputString()){return false;} ;while(true){if(DoFind()){return ;} ;if(!confirm(OxO79d8[14])){return ;} ;MoveToBodyStart();} ;} ;function ReplaceTxt(){if(!checkInputString()){return ;} ;DoReplace();FindTxt();} ;function ReplaceAllTxt(){if(!checkInputString()){return ;} ;var Ox31e=0;var msg=OxO79d8[15];MoveToBodyStart();if(Browser_UseIESelection()){var Ox136=editdoc[OxO79d8[9]];if(Ox136[OxO79d8[11]]==OxO79d8[12]){MoveToBodyStart();} ;var Ox31f=Ox136.createRange();var Ox31e=0;var msg=OxO79d8[15];Ox31f.expand(OxO79d8[16]);Ox31f.collapse();Ox31f.select();while(Ox31f.findText(stringSearch.value,1000000000,get_ie_matchtype())){Ox31f.select();Ox31f[OxO79d8[13]]=stringReplace[OxO79d8[7]];Ox31e++;} ;if(Ox31e==0){msg=OxO79d8[17];} else {msg=OxO79d8[18]+Ox31e;} ;alert(msg);} else {if((stringReplace[OxO79d8[7]]).indexOf(stringSearch.value)==-1){DoFind();while(DoReplace()){Ox31e++;DoFind();FindTxt();} ;if(Ox31e==0){msg=OxO79d8[17];} else {msg=OxO79d8[18]+Ox31e;} ;alert(msg);} else {FindTxt();alert(OxO79d8[19]);} ;} ;} ;