var OxOfdb3=["is_spring_image","1","gid","zIndex","style","srcImg","documentElement","body","getBoundingClientRect","left","top","getBoxObjectFor","x","y","offsetWidth","offsetHeight","offsetLeft","offsetTop","offsetParent","R","width","px","height","M","spring_expand(\x27","id","\x27)","lastActiveElement","spring_collapse(\x27","display","none","parentNode","block","hidetip","src","url","className","spring_image_popup","void(0)","expand","collapse","onmouseout","onmouseover","onclick","tooltip","click","prototype","MouseEvents","ownerDocument","addEventListener","scroll","attachEvent","onscroll"];function hidetip(){} ;function render_spring_image(Ox29){var Ox323;if(Ox29.getAttribute(OxOfdb3[0])==OxOfdb3[1]){return ;} ;Ox29.setAttribute(OxOfdb3[0],OxOfdb3[1]);render_spring_image[OxOfdb3[2]]=render_spring_image[OxOfdb3[2]]||1;render_spring_image[OxOfdb3[2]]++;function Ox324(){clearTimeout(this.M);render_spring_image[OxOfdb3[2]]++;this[OxOfdb3[4]][OxOfdb3[3]]=1000000+render_spring_image[OxOfdb3[2]];var Ox325=this[OxOfdb3[5]];var Ox326,Ox327,Ox328,Ox329;Ox328=Math.max(document[OxOfdb3[6]].scrollTop,document[OxOfdb3[7]].scrollTop);Ox329=Math.max(document[OxOfdb3[6]].scrollLeft,document[OxOfdb3[7]].scrollLeft);if(Ox325[OxOfdb3[8]]){Ox326=Ox325.getBoundingClientRect()[OxOfdb3[9]];Ox327=Ox325.getBoundingClientRect()[OxOfdb3[10]];} else {if(document[OxOfdb3[11]]){Ox326=document.getBoxObjectFor(Ox325)[OxOfdb3[12]]-Ox329;Ox327=document.getBoxObjectFor(Ox325)[OxOfdb3[13]]-Ox328;} else {var Ox32a=Ox32b(Ox325);Ox326=Ox32a[OxOfdb3[12]]-Ox329;Ox327=Ox32a[OxOfdb3[13]]-Ox328;} ;} ;function Ox32b(element){var Ox32a={x:0,y:0,width:element[OxOfdb3[14]],height:element[OxOfdb3[15]]};while(element){Ox32a[OxOfdb3[12]]+=element[OxOfdb3[16]];Ox32a[OxOfdb3[13]]+=element[OxOfdb3[17]];element=element[OxOfdb3[18]];} ;return Ox32a;} ;if(this[OxOfdb3[19]]<1.35){this[OxOfdb3[19]]+=0.1;this[OxOfdb3[4]][OxOfdb3[20]]=Math.floor(Ox325[OxOfdb3[14]]*this[OxOfdb3[19]])+OxOfdb3[21];this[OxOfdb3[4]][OxOfdb3[22]]=Math.floor(Ox325[OxOfdb3[15]]*this[OxOfdb3[19]])+OxOfdb3[21];Ox327=Math.floor(Ox327+Ox328-(this[OxOfdb3[14]]-Ox325[OxOfdb3[14]])/2)+OxOfdb3[21];;;Ox326=Math.floor(Ox326+Ox329-(this[OxOfdb3[15]]-Ox325[OxOfdb3[15]])/2)+OxOfdb3[21];this[OxOfdb3[4]][OxOfdb3[10]]=Ox327;this[OxOfdb3[4]][OxOfdb3[9]]=Ox326;this[OxOfdb3[23]]=setTimeout(OxOfdb3[24]+this[OxOfdb3[25]]+OxOfdb3[26],20);} else {if(render_spring_image[OxOfdb3[27]]!=this){this[OxOfdb3[23]]=setTimeout(OxOfdb3[28]+this[OxOfdb3[25]]+OxOfdb3[26],20);} ;} ;} ;function Ox32c(){clearTimeout(this.M);var Ox325=this[OxOfdb3[5]];var Ox326,Ox327,Ox328,Ox329;Ox328=Math.max(document[OxOfdb3[6]].scrollTop,document[OxOfdb3[7]].scrollTop);Ox329=Math.max(document[OxOfdb3[6]].scrollLeft,document[OxOfdb3[7]].scrollLeft);if(Ox325[OxOfdb3[8]]){Ox326=Ox325.getBoundingClientRect()[OxOfdb3[9]];Ox327=Ox325.getBoundingClientRect()[OxOfdb3[10]];} else {if(document[OxOfdb3[11]]){Ox326=document.getBoxObjectFor(Ox325)[OxOfdb3[12]]-Ox329;Ox327=document.getBoxObjectFor(Ox325)[OxOfdb3[13]]-Ox328;} ;} ;if(this[OxOfdb3[19]]>1){this[OxOfdb3[19]]-=0.1;this[OxOfdb3[4]][OxOfdb3[20]]=Math.ceil(Ox325[OxOfdb3[14]]*this[OxOfdb3[19]])+OxOfdb3[21];this[OxOfdb3[4]][OxOfdb3[22]]=Math.ceil(Ox325[OxOfdb3[15]]*this[OxOfdb3[19]])+OxOfdb3[21];Ox327=Math.ceil(Ox327+Ox328-(this[OxOfdb3[14]]-Ox325[OxOfdb3[14]])/2)+OxOfdb3[21];;;Ox326=Math.ceil(Ox326+Ox329-(this[OxOfdb3[15]]-Ox325[OxOfdb3[15]])/2)+OxOfdb3[21];this[OxOfdb3[4]][OxOfdb3[10]]=Ox327;this[OxOfdb3[4]][OxOfdb3[9]]=Ox326;this[OxOfdb3[23]]=setTimeout(OxOfdb3[28]+this[OxOfdb3[25]]+OxOfdb3[26],0);} else {this[OxOfdb3[4]][OxOfdb3[29]]=OxOfdb3[30];} ;} ;function Ox32d(){var Ox32e=Ox323;if(Ox32e[OxOfdb3[31]]==null){document[OxOfdb3[7]].appendChild(Ox32e);} ;if((render_spring_image[OxOfdb3[27]]!=null)&&(render_spring_image[OxOfdb3[27]]!=this)){render_spring_image[OxOfdb3[27]][OxOfdb3[23]]=setTimeout(OxOfdb3[28]+render_spring_image[OxOfdb3[27]][OxOfdb3[25]]+OxOfdb3[26],0);} ;render_spring_image[OxOfdb3[27]]=Ox32e;Ox32e[OxOfdb3[4]][OxOfdb3[29]]=OxOfdb3[32];Ox32e.expand();} ;function Ox32f(){try{if(window[OxOfdb3[33]]){hidetip();} ;this.collapse();} catch(x){} ;} ;Ox323= new Image();Ox323[OxOfdb3[34]]=Ox29.getAttribute(OxOfdb3[35])||Ox29[OxOfdb3[34]];Ox323[OxOfdb3[36]]=OxOfdb3[37];Ox323[OxOfdb3[25]]=OxOfdb3[37]+render_spring_image[OxOfdb3[2]];Ox323[OxOfdb3[23]]=setTimeout(OxOfdb3[38],0);Ox323[OxOfdb3[19]]=1;Ox323[OxOfdb3[5]]=Ox29;Ox323[OxOfdb3[39]]=Ox324;Ox323[OxOfdb3[40]]=Ox32c;Ox323[OxOfdb3[41]]=Ox32f;Ox323[OxOfdb3[42]]=Ox330;Ox323[OxOfdb3[43]]=function (){insert(Ox29.getAttribute(OxOfdb3[35]));} ;function Ox330(){var Ox331=Ox29.getAttribute(OxOfdb3[44]);showTooltip(Ox331,this);} ;try{Ox29[OxOfdb3[42]]=Ox32d;} catch(x){} ;} ;if(document[OxOfdb3[11]]!=null){HTMLElement[OxOfdb3[46]][OxOfdb3[45]]=function (){var Ox332=this[OxOfdb3[48]].createEvent(OxOfdb3[47]);Ox332.initMouseEvent(OxOfdb3[45],true,true,this[OxOfdb3[48]].defaultView,1,0,0,0,0,false,false,false,false,0,null);this.dispatchEvent(Ox332);} ;} ;function spring_image_scrcoll(){render_spring_image[OxOfdb3[27]]=null;} ;if(window[OxOfdb3[49]]){window.addEventListener(OxOfdb3[50],spring_image_scrcoll,true);} else {if(window[OxOfdb3[51]]){window.attachEvent(OxOfdb3[52],spring_image_scrcoll);} ;} ;function spring_expand(Ox9a){var Ox29=document.getElementById(Ox9a);if(Ox29){Ox29.expand();} ;} ;function spring_collapse(Ox9a){var Ox29=document.getElementById(Ox9a);if(Ox29){Ox29.collapse();} ;} ;