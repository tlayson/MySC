var OxO5b07=["idSource","inc_width","inc_height","onload","availWidth","screen","window","availHeight","contentWindow","outerHTML","documentElement","text/html","replace","onresize","dialogWidth","innerWidth","clientWidth","body","dialogHeight","innerHeight","clientHeight","value","contentDocument","document"];var editor=Window_GetDialogArguments(window);var idSource=Window_GetElement(window,OxO5b07[0],true);var inc_width=Window_GetElement(window,OxO5b07[1],true);var inc_height=Window_GetElement(window,OxO5b07[2],true);var ParentW;var ParentH;window[OxO5b07[3]]=function window_onload(){ParentW=top[OxO5b07[6]][OxO5b07[5]][OxO5b07[4]];ParentH=top[OxO5b07[6]][OxO5b07[5]][OxO5b07[7]];var iframe=idSource[OxO5b07[8]];var editdoc=editor.GetDocument();var Oxf5;if(Browser_IsWinIE()){Oxf5=editdoc[OxO5b07[10]][OxO5b07[9]];} else {Oxf5=outerHTML(editdoc.documentElement);} ;var Ox46c=Frame_GetContentDocument(iframe);Ox46c.open(OxO5b07[11],OxO5b07[12]);Ox46c.write(Oxf5);Ox46c.close();ShowSizeInfo();} ;window[OxO5b07[13]]=ShowSizeInfo;function ShowSizeInfo(){var Oxe1,Ox2d;if(window[OxO5b07[14]]){Oxe1=window[OxO5b07[14]];} else {if(window[OxO5b07[15]]){Oxe1=window[OxO5b07[15]];} else {if(document[OxO5b07[10]]&&document[OxO5b07[10]][OxO5b07[16]]){Oxe1=document[OxO5b07[10]][OxO5b07[16]];} else {if(document[OxO5b07[17]]){Oxe1=document[OxO5b07[17]][OxO5b07[16]];} ;} ;} ;} ;if(window[OxO5b07[18]]){Ox2d=window[OxO5b07[18]];} else {if(window[OxO5b07[19]]){Ox2d=window[OxO5b07[19]];} else {if(document[OxO5b07[10]]&&document[OxO5b07[10]][OxO5b07[20]]){Ox2d=document[OxO5b07[10]][OxO5b07[20]];} else {if(document[OxO5b07[17]]){Ox2d=document[OxO5b07[17]][OxO5b07[20]];} ;} ;} ;} ;inc_width[OxO5b07[21]]=Oxe1;inc_height[OxO5b07[21]]=Ox2d;} ;function do_Close(){Window_CloseDialog(window);} ;function Frame_GetContentDocument(Ox348){if(Ox348[OxO5b07[22]]){return Ox348[OxO5b07[22]];} ;return Ox348[OxO5b07[23]];} ;