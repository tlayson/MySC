var OxOb9d5=["Xml","Brushes","sh","CssClass","dp-xml","Style",".dp-xml .cdata { color: #ff1493; }",".dp-xml .tag, .dp-xml .tag-name { color: #069; font-weight: bold; }",".dp-xml .attribute { color: red; }",".dp-xml .attribute-value { color: blue; }","prototype","Aliases","xml","xhtml","xslt","html","ProcessRegexList","length","(\x26lt;|\x3C)\x5C!\x5C[[\x5Cw\x5Cs]*?\x5C[(.|\x5Cs)*?\x5C]\x5C](\x26gt;|\x3E)","gm","cdata","(\x26lt;|\x3C)!--\x5Cs*.*?\x5Cs*--(\x26gt;|\x3E)","comments","([:\x5Cw-.]+)\x5Cs*=\x5Cs*(\x22.*?\x22|\x27.*?\x27|\x5Cw+)*|(\x5Cw+)","attribute","index","attribute-value","(\x26lt;|\x3C)/*\x5C?*(?!\x5C!)|/*\x5C?*(\x26gt;|\x3E)","tag","(?:\x26lt;|\x3C)/*\x5C?*\x5Cs*([:\x5Cw-.]+)","tag-name"];dp[OxOb9d5[2]][OxOb9d5[1]][OxOb9d5[0]]=function (){this[OxOb9d5[3]]=OxOb9d5[4];this[OxOb9d5[5]]=OxOb9d5[6]+OxOb9d5[7]+OxOb9d5[8]+OxOb9d5[9];} ;dp[OxOb9d5[2]][OxOb9d5[1]][OxOb9d5[0]][OxOb9d5[10]]= new dp[OxOb9d5[2]].Highlighter();dp[OxOb9d5[2]][OxOb9d5[1]][OxOb9d5[0]][OxOb9d5[11]]=[OxOb9d5[12],OxOb9d5[13],OxOb9d5[14],OxOb9d5[15],OxOb9d5[13]];dp[OxOb9d5[2]][OxOb9d5[1]][OxOb9d5[0]][OxOb9d5[10]][OxOb9d5[16]]=function (){function Oxb51(Oxb52,Ox4f){Oxb52[Oxb52[OxOb9d5[17]]]=Ox4f;} ;var Ox1fc=0;var Ox935=null;var Oxb53=null;this.GetMatches( new RegExp(OxOb9d5[18],OxOb9d5[19]),OxOb9d5[20]);this.GetMatches( new RegExp(OxOb9d5[21],OxOb9d5[19]),OxOb9d5[22]);Oxb53= new RegExp(OxOb9d5[23],OxOb9d5[19]);while((Ox935=Oxb53.exec(this.code))!=null){if(Ox935[1]==null){continue ;} ;Oxb51(this.matches, new dp[OxOb9d5[2]].Match(Ox935[1],Ox935.index,OxOb9d5[24]));if(Ox935[2]!=undefined){Oxb51(this.matches, new dp[OxOb9d5[2]].Match(Ox935[2],Ox935[OxOb9d5[25]]+Ox935[0].indexOf(Ox935[2]),OxOb9d5[26]));} ;} ;this.GetMatches( new RegExp(OxOb9d5[27],OxOb9d5[19]),OxOb9d5[28]);Oxb53= new RegExp(OxOb9d5[29],OxOb9d5[19]);while((Ox935=Oxb53.exec(this.code))!=null){Oxb51(this.matches, new dp[OxOb9d5[2]].Match(Ox935[1],Ox935[OxOb9d5[25]]+Ox935[0].indexOf(Ox935[1]),OxOb9d5[30]));} ;} ;