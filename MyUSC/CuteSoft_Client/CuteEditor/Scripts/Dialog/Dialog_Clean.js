var OxO2321=["ig","\x3C/?[^\x3E]*\x3E","","allhtml","\x3C\x5C?xml[^\x3E]*\x3E","\x3C/?[a-z]+:[^\x3E]*\x3E","(\x3C[^\x3E]+) class=[^ |^\x3E]*([^\x3E]*\x3E)","$1 $2","(\x3C[^\x3E]+) style=\x22[^\x22]*\x22([^\x3E]*\x3E)","\x3Cspan[^\x3E]*\x3E\x3C/span[^\x3E]*\x3E","\x3Cspan\x3E\x3Cspan\x3E","\x3Cspan\x3E","\x3C/span\x3E\x3C/span\x3E","\x3C/span\x3E","[ ]*\x3E","\x3E","word","css","\x3C/?font[^\x3E]*\x3E","font","\x3C/?span[^\x3E]*\x3E","span"];var editor=Window_GetDialogArguments(window);function execRE(Ox295,Ox296,Oxce){var Ox297= new RegExp(Ox295,OxO2321[0]);return Oxce.replace(Ox297,Ox296);} ;function getContent(){return editor.GetBodyInnerHTML();} ;function setContent(Oxce){editor.SetHTML(Oxce);} ;function codeCleaner(Ox216){var Oxce=getContent();switch(Ox216){case OxO2321[3]:Oxce=execRE(OxO2321[1],OxO2321[2],Oxce);break ;;case OxO2321[16]:Oxce=execRE(OxO2321[4],OxO2321[2],Oxce);Oxce=execRE(OxO2321[5],OxO2321[2],Oxce);Oxce=execRE(OxO2321[6],OxO2321[7],Oxce);Oxce=execRE(OxO2321[8],OxO2321[7],Oxce);Oxce=execRE(OxO2321[9],OxO2321[2],Oxce);Oxce=execRE(OxO2321[10],OxO2321[11],Oxce);Oxce=execRE(OxO2321[12],OxO2321[13],Oxce);Oxce=execRE(OxO2321[14],OxO2321[15],Oxce);break ;;case OxO2321[17]:Oxce=execRE(OxO2321[6],OxO2321[7],Oxce);Oxce=execRE(OxO2321[8],OxO2321[7],Oxce);break ;;case OxO2321[19]:Oxce=execRE(OxO2321[18],OxO2321[2],Oxce);break ;;case OxO2321[21]:Oxce=execRE(OxO2321[20],OxO2321[2],Oxce);break ;;} ;setContent(Oxce);} ;