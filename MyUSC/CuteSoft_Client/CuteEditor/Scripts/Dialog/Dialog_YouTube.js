var OxOd86c=["idSource","TargetUrl","value","","$4","$5","\x26","wmode=\x22transparent\x22","allowfullscreen=\x22true\x22","\x3Cembed src=\x22","\x22 width=\x22","\x22 height=\x22","\x22 "," "," type=\x22application/x-shockwave-flash\x22 pluginspage=\x22http://www.macromedia.com/go/getflashplayer\x22 \x3E\x3C/embed\x3E\x0A","\x3Cobject xcodebase=","\x22http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab\x22"," height=\x22","\x22 classid=","\x22clsid:D27CDB6E-AE6D-11cf-96B8-444553540000\x22 \x3E"," \x3Cparam name=\x22Movie\x22 value=\x22","\x22 /\x3E","\x3Cparam name=\x22wmode\x22 value=\x22transparent\x22/\x3E","\x3Cparam name=\x22allowFullScreen\x22 value=\x22true\x22/\x3E","\x3C/object\x3E"];var idSource=Window_GetElement(window,OxOd86c[0],true);var TargetUrl=Window_GetElement(window,OxOd86c[1],true);var editor=Window_GetDialogArguments(window);var oldWidth,OldHeight;function do_preview(){var Ox120=GetEmbed();if(Ox120){if(idSource[OxOd86c[2]]!=Ox120&&idSource[OxOd86c[2]]!=null){idSource[OxOd86c[2]]=Ox120;} ;} ;} ;function do_insert(){var Ox120=GetEmbed();if(Ox120){editor.PasteHTML(Ox120);} ;Window_CloseDialog(window);} ;function do_Close(){Window_CloseDialog(window);} ;function GetEmbed(){if(idSource[OxOd86c[2]]==OxOd86c[3]||idSource[OxOd86c[2]]==null){return ;} ;var Ox649=OxOd86c[3];Ox649=idSource[OxOd86c[2]];var Ox64a=/(<iframe[^\>]*?)(\ssrc=\s*)\s*("|')(.+?)\3([^>]*)(.*<\/iframe>)/gi;var Ox64b=/(<object[^\>]*>[\s|\S]*?)(<embed[^\>]*?)(\ssrc=\s*)\s*("|')(.+?)\4([^>]*)(.*<\/embed>)[\s|\S]*?<\/object>/gi;if(Ox649.match(Ox64a)){Ox649=Ox649.replace(Ox64a,OxOd86c[4]);TargetUrl[OxOd86c[2]]=Ox649;return idSource[OxOd86c[2]];} else {if(Ox649.match(Ox64b)){oldWidth=Ox649.replace(/(<object[^\>]*>[\s|\S]*?)(<embed[^\>]*?)(\swidth=\s*)\s*("|')(.+?)\4([^>]*)(.*<\/embed>)[\s|\S]*?<\/object>/gi,OxOd86c[5]);oldHeight=Ox649.replace(/(<object[^\>]*>[\s|\S]*?)(<embed[^\>]*?)(\sheight=\s*)\s*("|')(.+?)\4([^>]*)(.*<\/embed>)[\s|\S]*?<\/object>/gi,OxOd86c[5]);Ox649=Ox649.replace(Ox64b,OxOd86c[5]);if(Ox649.indexOf(OxOd86c[6])!=-1){TargetUrl[OxOd86c[2]]=Ox649.substring(0,Ox649.indexOf(OxOd86c[6]));} ;var Ox64c=OxOd86c[3];var Oxe1=425;var Ox2d=344;var Ox3e3,Ox3e4;oldWidth=parseInt(oldWidth);if(oldWidth){Oxe1=oldWidth;} ;oldHeight=parseInt(oldHeight);if(oldHeight){Ox2d=oldHeight;} ;Ox3e3=true;if(Ox649==OxOd86c[3]){return ;} ;var Ox3e7,Ox3e9;Ox3e9=OxOd86c[3];Ox3e7=true?OxOd86c[7]:OxOd86c[3];Ox3e9=true?OxOd86c[8]:OxOd86c[3];var Ox3ef=OxOd86c[9]+Ox649+OxOd86c[10]+Oxe1+OxOd86c[11]+Ox2d+OxOd86c[12]+Ox3e9+OxOd86c[13]+Ox3e7+OxOd86c[14];var Ox3f0=OxOd86c[15]+OxOd86c[16]+OxOd86c[17]+Ox2d+OxOd86c[10]+Oxe1+OxOd86c[18]+OxOd86c[19]+OxOd86c[20]+Ox649+OxOd86c[21];if(true){Ox3f0=Ox3f0+OxOd86c[22];} ;if(true){Ox3f0=Ox3f0+OxOd86c[23];} ;Ox3f0=Ox3f0+Ox3ef+OxOd86c[24];return Ox3f0;} ;} ;} ;