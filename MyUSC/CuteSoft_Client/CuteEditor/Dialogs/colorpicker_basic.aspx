<%@ Page Language="C#" Inherits="CuteEditor.EditorUtilityPage" %>
<script runat="server">
string GetDialogQueryString;
override protected void OnInit(EventArgs args)
{
	if(Context.Request.QueryString["Dialog"]=="Standard")
	{	
	if(Context.Request.QueryString["IsFrame"]==null)
	{
		string FrameSrc="colorpicker_basic.aspx?IsFrame=1&"+Request.ServerVariables["QUERY_STRING"];
		CuteEditor.CEU.WriteDialogOuterFrame(Context,"[[MoreColors]]",FrameSrc);
		Context.Response.End();
	}
	}
	string s="";
	if(Context.Request.QueryString["Dialog"]=="Standard")	
		s="&Dialog=Standard";
	
	GetDialogQueryString="Theme="+Context.Request.QueryString["Theme"]+s;	
	base.OnInit(args);
}
</script>
<html xmlns="http://www.w3.org/1999/xhtml">
	<head runat="server">
		<meta http-equiv="Page-Enter" content="blendTrans(Duration=0.1)" />
		<meta http-equiv="Page-Exit" content="blendTrans(Duration=0.1)" />
		<script type="text/javascript" src="Load.ashx?type=dialogscript&verfix=1006&file=DialogHead.js"></script>
		<script type="text/javascript" src="Load.ashx?type=dialogscript&verfix=1006&file=Dialog_ColorPicker.js"></script>
		<link href='Load.ashx?type=themecss&file=dialog.css&theme=[[_Theme_]]' type="text/css"
			rel="stylesheet" />
		<style type="text/css">
			.colorcell
			{
				width:16px;
				height:17px;
				cursor:hand;
			}
			.colordiv,.customdiv
			{
				border:solid 1px #808080;
				width:16px;
				height:17px;
				font-size:1px;
			}
			#ajaxdiv{padding:10px;margin:0;text-align:center; background:#eeeeee;}
		</style>
		<title>[[NamedColors]]</title>
		<script>
								
		var OxOd7c4=["Green","#008000","Lime","#00FF00","Teal","#008080","Aqua","#00FFFF","Navy","#000080","Blue","#0000FF","Purple","#800080","Fuchsia","#FF00FF","Maroon","#800000","Red","#FF0000","Olive","#808000","Yellow","#FFFF00","White","#FFFFFF","Silver","#C0C0C0","Gray","#808080","Black","#000000","DarkOliveGreen","#556B2F","DarkGreen","#006400","DarkSlateGray","#2F4F4F","SlateGray","#708090","DarkBlue","#00008B","MidnightBlue","#191970","Indigo","#4B0082","DarkMagenta","#8B008B","Brown","#A52A2A","DarkRed","#8B0000","Sienna","#A0522D","SaddleBrown","#8B4513","DarkGoldenrod","#B8860B","Beige","#F5F5DC","HoneyDew","#F0FFF0","DimGray","#696969","OliveDrab","#6B8E23","ForestGreen","#228B22","DarkCyan","#008B8B","LightSlateGray","#778899","MediumBlue","#0000CD","DarkSlateBlue","#483D8B","DarkViolet","#9400D3","MediumVioletRed","#C71585","IndianRed","#CD5C5C","Firebrick","#B22222","Chocolate","#D2691E","Peru","#CD853F","Goldenrod","#DAA520","LightGoldenrodYellow","#FAFAD2","MintCream","#F5FFFA","DarkGray","#A9A9A9","YellowGreen","#9ACD32","SeaGreen","#2E8B57","CadetBlue","#5F9EA0","SteelBlue","#4682B4","RoyalBlue","#4169E1","BlueViolet","#8A2BE2","DarkOrchid","#9932CC","DeepPink","#FF1493","RosyBrown","#BC8F8F","Crimson","#DC143C","DarkOrange","#FF8C00","BurlyWood","#DEB887","DarkKhaki","#BDB76B","LightYellow","#FFFFE0","Azure","#F0FFFF","LightGray","#D3D3D3","LawnGreen","#7CFC00","MediumSeaGreen","#3CB371","LightSeaGreen","#20B2AA","DeepSkyBlue","#00BFFF","DodgerBlue","#1E90FF","SlateBlue","#6A5ACD","MediumOrchid","#BA55D3","PaleVioletRed","#DB7093","Salmon","#FA8072","OrangeRed","#FF4500","SandyBrown","#F4A460","Tan","#D2B48C","Gold","#FFD700","Ivory","#FFFFF0","GhostWhite","#F8F8FF","Gainsboro","#DCDCDC","Chartreuse","#7FFF00","LimeGreen","#32CD32","MediumAquamarine","#66CDAA","DarkTurquoise","#00CED1","CornflowerBlue","#6495ED","MediumSlateBlue","#7B68EE","Orchid","#DA70D6","HotPink","#FF69B4","LightCoral","#F08080","Tomato","#FF6347","Orange","#FFA500","Bisque","#FFE4C4","Khaki","#F0E68C","Cornsilk","#FFF8DC","Linen","#FAF0E6","WhiteSmoke","#F5F5F5","GreenYellow","#ADFF2F","DarkSeaGreen","#8FBC8B","Turquoise","#40E0D0","MediumTurquoise","#48D1CC","SkyBlue","#87CEEB","MediumPurple","#9370DB","Violet","#EE82EE","LightPink","#FFB6C1","DarkSalmon","#E9967A","Coral","#FF7F50","NavajoWhite","#FFDEAD","BlanchedAlmond","#FFEBCD","PaleGoldenrod","#EEE8AA","Oldlace","#FDF5E6","Seashell","#FFF5EE","PaleGreen","#98FB98","SpringGreen","#00FF7F","Aquamarine","#7FFFD4","PowderBlue","#B0E0E6","LightSkyBlue","#87CEFA","LightSteelBlue","#B0C4DE","Plum","#DDA0DD","Pink","#FFC0CB","LightSalmon","#FFA07A","Wheat","#F5DEB3","Moccasin","#FFE4B5","AntiqueWhite","#FAEBD7","LemonChiffon","#FFFACD","FloralWhite","#FFFAF0","Snow","#FFFAFA","AliceBlue","#F0F8FF","LightGreen","#90EE90","MediumSpringGreen","#00FA9A","PaleTurquoise","#AFEEEE","LightCyan","#E0FFFF","LightBlue","#ADD8E6","Lavender","#E6E6FA","Thistle","#D8BFD8","MistyRose","#FFE4E1","Peachpuff","#FFDAB9","PapayaWhip","#FFEFD5"];var colorlist=[{n:OxOd7c4[0],h:OxOd7c4[1]},{n:OxOd7c4[2],h:OxOd7c4[3]},{n:OxOd7c4[4],h:OxOd7c4[5]},{n:OxOd7c4[6],h:OxOd7c4[7]},{n:OxOd7c4[8],h:OxOd7c4[9]},{n:OxOd7c4[10],h:OxOd7c4[11]},{n:OxOd7c4[12],h:OxOd7c4[13]},{n:OxOd7c4[14],h:OxOd7c4[15]},{n:OxOd7c4[16],h:OxOd7c4[17]},{n:OxOd7c4[18],h:OxOd7c4[19]},{n:OxOd7c4[20],h:OxOd7c4[21]},{n:OxOd7c4[22],h:OxOd7c4[23]},{n:OxOd7c4[24],h:OxOd7c4[25]},{n:OxOd7c4[26],h:OxOd7c4[27]},{n:OxOd7c4[28],h:OxOd7c4[29]},{n:OxOd7c4[30],h:OxOd7c4[31]}];var colormore=[{n:OxOd7c4[32],h:OxOd7c4[33]},{n:OxOd7c4[34],h:OxOd7c4[35]},{n:OxOd7c4[36],h:OxOd7c4[37]},{n:OxOd7c4[38],h:OxOd7c4[39]},{n:OxOd7c4[40],h:OxOd7c4[41]},{n:OxOd7c4[42],h:OxOd7c4[43]},{n:OxOd7c4[44],h:OxOd7c4[45]},{n:OxOd7c4[46],h:OxOd7c4[47]},{n:OxOd7c4[48],h:OxOd7c4[49]},{n:OxOd7c4[50],h:OxOd7c4[51]},{n:OxOd7c4[52],h:OxOd7c4[53]},{n:OxOd7c4[54],h:OxOd7c4[55]},{n:OxOd7c4[56],h:OxOd7c4[57]},{n:OxOd7c4[58],h:OxOd7c4[59]},{n:OxOd7c4[60],h:OxOd7c4[61]},{n:OxOd7c4[62],h:OxOd7c4[63]},{n:OxOd7c4[64],h:OxOd7c4[65]},{n:OxOd7c4[66],h:OxOd7c4[67]},{n:OxOd7c4[68],h:OxOd7c4[69]},{n:OxOd7c4[70],h:OxOd7c4[71]},{n:OxOd7c4[72],h:OxOd7c4[73]},{n:OxOd7c4[74],h:OxOd7c4[75]},{n:OxOd7c4[76],h:OxOd7c4[77]},{n:OxOd7c4[78],h:OxOd7c4[79]},{n:OxOd7c4[80],h:OxOd7c4[81]},{n:OxOd7c4[82],h:OxOd7c4[83]},{n:OxOd7c4[84],h:OxOd7c4[85]},{n:OxOd7c4[86],h:OxOd7c4[87]},{n:OxOd7c4[88],h:OxOd7c4[89]},{n:OxOd7c4[90],h:OxOd7c4[91]},{n:OxOd7c4[92],h:OxOd7c4[93]},{n:OxOd7c4[94],h:OxOd7c4[95]},{n:OxOd7c4[96],h:OxOd7c4[97]},{n:OxOd7c4[98],h:OxOd7c4[99]},{n:OxOd7c4[100],h:OxOd7c4[101]},{n:OxOd7c4[102],h:OxOd7c4[103]},{n:OxOd7c4[104],h:OxOd7c4[105]},{n:OxOd7c4[106],h:OxOd7c4[107]},{n:OxOd7c4[108],h:OxOd7c4[109]},{n:OxOd7c4[110],h:OxOd7c4[111]},{n:OxOd7c4[112],h:OxOd7c4[113]},{n:OxOd7c4[114],h:OxOd7c4[115]},{n:OxOd7c4[116],h:OxOd7c4[117]},{n:OxOd7c4[118],h:OxOd7c4[119]},{n:OxOd7c4[120],h:OxOd7c4[121]},{n:OxOd7c4[122],h:OxOd7c4[123]},{n:OxOd7c4[124],h:OxOd7c4[125]},{n:OxOd7c4[126],h:OxOd7c4[127]},{n:OxOd7c4[128],h:OxOd7c4[129]},{n:OxOd7c4[130],h:OxOd7c4[131]},{n:OxOd7c4[132],h:OxOd7c4[133]},{n:OxOd7c4[134],h:OxOd7c4[135]},{n:OxOd7c4[136],h:OxOd7c4[137]},{n:OxOd7c4[138],h:OxOd7c4[139]},{n:OxOd7c4[140],h:OxOd7c4[141]},{n:OxOd7c4[142],h:OxOd7c4[143]},{n:OxOd7c4[144],h:OxOd7c4[145]},{n:OxOd7c4[146],h:OxOd7c4[147]},{n:OxOd7c4[148],h:OxOd7c4[149]},{n:OxOd7c4[150],h:OxOd7c4[151]},{n:OxOd7c4[152],h:OxOd7c4[153]},{n:OxOd7c4[154],h:OxOd7c4[155]},{n:OxOd7c4[156],h:OxOd7c4[157]},{n:OxOd7c4[158],h:OxOd7c4[159]},{n:OxOd7c4[160],h:OxOd7c4[161]},{n:OxOd7c4[162],h:OxOd7c4[163]},{n:OxOd7c4[164],h:OxOd7c4[165]},{n:OxOd7c4[166],h:OxOd7c4[167]},{n:OxOd7c4[168],h:OxOd7c4[169]},{n:OxOd7c4[170],h:OxOd7c4[171]},{n:OxOd7c4[172],h:OxOd7c4[173]},{n:OxOd7c4[174],h:OxOd7c4[175]},{n:OxOd7c4[176],h:OxOd7c4[177]},{n:OxOd7c4[178],h:OxOd7c4[179]},{n:OxOd7c4[180],h:OxOd7c4[181]},{n:OxOd7c4[182],h:OxOd7c4[183]},{n:OxOd7c4[184],h:OxOd7c4[185]},{n:OxOd7c4[186],h:OxOd7c4[187]},{n:OxOd7c4[188],h:OxOd7c4[189]},{n:OxOd7c4[190],h:OxOd7c4[191]},{n:OxOd7c4[192],h:OxOd7c4[193]},{n:OxOd7c4[194],h:OxOd7c4[195]},{n:OxOd7c4[196],h:OxOd7c4[197]},{n:OxOd7c4[198],h:OxOd7c4[199]},{n:OxOd7c4[200],h:OxOd7c4[201]},{n:OxOd7c4[202],h:OxOd7c4[203]},{n:OxOd7c4[204],h:OxOd7c4[205]},{n:OxOd7c4[206],h:OxOd7c4[207]},{n:OxOd7c4[208],h:OxOd7c4[209]},{n:OxOd7c4[210],h:OxOd7c4[211]},{n:OxOd7c4[212],h:OxOd7c4[213]},{n:OxOd7c4[214],h:OxOd7c4[215]},{n:OxOd7c4[216],h:OxOd7c4[217]},{n:OxOd7c4[218],h:OxOd7c4[219]},{n:OxOd7c4[220],h:OxOd7c4[221]},{n:OxOd7c4[156],h:OxOd7c4[157]},{n:OxOd7c4[222],h:OxOd7c4[223]},{n:OxOd7c4[224],h:OxOd7c4[225]},{n:OxOd7c4[226],h:OxOd7c4[227]},{n:OxOd7c4[228],h:OxOd7c4[229]},{n:OxOd7c4[230],h:OxOd7c4[231]},{n:OxOd7c4[232],h:OxOd7c4[233]},{n:OxOd7c4[234],h:OxOd7c4[235]},{n:OxOd7c4[236],h:OxOd7c4[237]},{n:OxOd7c4[238],h:OxOd7c4[239]},{n:OxOd7c4[240],h:OxOd7c4[241]},{n:OxOd7c4[242],h:OxOd7c4[243]},{n:OxOd7c4[244],h:OxOd7c4[245]},{n:OxOd7c4[246],h:OxOd7c4[247]},{n:OxOd7c4[248],h:OxOd7c4[249]},{n:OxOd7c4[250],h:OxOd7c4[251]},{n:OxOd7c4[252],h:OxOd7c4[253]},{n:OxOd7c4[254],h:OxOd7c4[255]},{n:OxOd7c4[256],h:OxOd7c4[257]},{n:OxOd7c4[258],h:OxOd7c4[259]},{n:OxOd7c4[260],h:OxOd7c4[261]},{n:OxOd7c4[262],h:OxOd7c4[263]},{n:OxOd7c4[264],h:OxOd7c4[265]},{n:OxOd7c4[266],h:OxOd7c4[267]},{n:OxOd7c4[268],h:OxOd7c4[269]},{n:OxOd7c4[270],h:OxOd7c4[271]},{n:OxOd7c4[272],h:OxOd7c4[273]}];
		
		</script>
	</head>
	<body>
		<div id="ajaxdiv">
			<div class="tab-pane-control tab-pane" id="tabPane1">
				<div class="tab-row">
					<h2 class="tab">
						<a tabindex="-1" href='colorpicker.aspx?<%=GetDialogQueryString%>'>
							<span style="white-space:nowrap;">
								[[WebPalette]]
							</span>
						</a>
					</h2>
					<h2 class="tab selected">
							<a tabindex="-1" href='colorpicker_basic.aspx?<%=GetDialogQueryString%>'>
								<span style="white-space:nowrap;">
									[[NamedColors]]
								</span>
							</a>
					</h2>
					<h2 class="tab">
							<a tabindex="-1" href='colorpicker_more.aspx?<%=GetDialogQueryString%>'>
								<span style="white-space:nowrap;">
									[[CustomColor]]
								</span>
							</a>
					</h2>
				</div>
				<div class="tab-page">			
					<table class="colortable" align="center">
						<tr>
							<td colspan="16" height="16"><p align="left">Basic:
								</p>
							</td>
						</tr>
						<tr>
							<script>
								var OxO32e4=["length","\x3Ctd class=\x27colorcell\x27\x3E\x3Cdiv class=\x27colordiv\x27 style=\x27background-color:","\x27 title=\x27"," ","\x27 cname=\x27","\x27 cvalue=\x27","\x27\x3E\x3C/div\x3E\x3C/td\x3E",""];var arr=[];for(var i=0;i<colorlist[OxO32e4[0]];i++){arr.push(OxO32e4[1]);arr.push(colorlist[i].n);arr.push(OxO32e4[2]);arr.push(colorlist[i].n);arr.push(OxO32e4[3]);arr.push(colorlist[i].h);arr.push(OxO32e4[4]);arr.push(colorlist[i].n);arr.push(OxO32e4[5]);arr.push(colorlist[i].h);arr.push(OxO32e4[6]);} ;document.write(arr.join(OxO32e4[7]));
							</script>
						</tr>
						<tr>
							<td colspan="16" height="12"><p align="left"></p>
							</td>
						</tr>
						<tr>
							<td colspan="16"><p align="left">Additional:
								</p>
							</td>
						</tr>
						<script>
							var OxOa702=["length","\x3Ctr\x3E","\x3Ctd class=\x27colorcell\x27\x3E\x3Cdiv class=\x27colordiv\x27 style=\x27background-color:","\x27 title=\x27"," ","\x27 cname=\x27","\x27 cvalue=\x27","\x27\x3E\x3C/div\x3E\x3C/td\x3E","\x3C/tr\x3E",""];var arr=[];for(var i=0;i<colormore[OxOa702[0]];i++){if(i%16==0){arr.push(OxOa702[1]);} ;arr.push(OxOa702[2]);arr.push(colormore[i].n);arr.push(OxOa702[3]);arr.push(colormore[i].n);arr.push(OxOa702[4]);arr.push(colormore[i].h);arr.push(OxOa702[5]);arr.push(colormore[i].n);arr.push(OxOa702[6]);arr.push(colormore[i].h);arr.push(OxOa702[7]);if(i%16==15){arr.push(OxOa702[8]);} ;} ;if(colormore%16>0){arr.push(OxOa702[8]);} ;document.write(arr.join(OxOa702[9]));
						</script>
						<tr>
							<td colspan="16" height="8">
							</td>
						</tr>
						<tr>
							<td colspan="16" height="12">
								<input checked id="CheckboxColorNames" style="width: 16px; height: 20px" type="checkbox">
								<span style="width: 118px;">Use color names</span>
							</td>
						</tr>
						<tr>
							<td colspan="16" height="12">
							</td>
						</tr>
						<tr>
							<td colspan="16" valign="middle" height="24">
							<span style="height:24px;width:50px;vertical-align:middle;">Color : </span>&nbsp;
							<input type="text" id="divpreview" size="7" maxlength="7" style="width:180px;height:24px;border:#a0a0a0 1px solid; Padding:4;"/>
					
							</td>
						</tr>
				</table>
			</div>
		</div>
		<div id="container-bottom">
			<input type="button" id="buttonok" value="[[OK]]" class="formbutton" style="width:70px"	onclick="do_insert();" /> 
			&nbsp;&nbsp;&nbsp;&nbsp; 
			<input type="button" id="buttoncancel" value="[[Cancel]]" class="formbutton" style="width:70px"	onclick="do_Close();" />	
		</div>
	</div>
	</body>
</html>

