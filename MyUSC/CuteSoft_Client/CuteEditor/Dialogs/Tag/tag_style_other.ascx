<%@ Control Inherits="CuteEditor.EditorUtilityCtrl" Language="c#" AutoEventWireup="false" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<fieldset style="padding:4px;">
	<legend>
	[[Cursor]]
	</legend>
	<select id="sel_cursor">
		<option value="">[[NotSet]]</option>
		<option value="Default">[[Default]]</option>
		<option value="Move">[[Move]]</option>
		<option value="Text">Text</option>
		<option value="Wait">Wait</option>
		<option value="Help">Help</option>
		<option value="Pointer">Pointer</option>
		<option value="Hand">Hand</option>
		<option value="Progress">Progress </option>
		<option value="not-allowed">Not-Allowed</option>
		<!-- x-resize -->
	</select>
</fieldset>

<fieldset style="padding:4px;">
	<legend>
	[[Filter]]
	</legend>
	<input type="text" id="inp_filter" style="width:240px" /> <!--button filter builder-->
</fieldset>
<div id="outer" style="height:100px; margin-bottom:10px; padding:5px;"><div id="div_demo">[[DemoText]]</div></div><br />

<script type="text/javascript">

var OxOd501=["sel_cursor","inp_filter","outer","div_demo","cssText","style","value","cursor","filter"];var sel_cursor=Window_GetElement(window,OxOd501[0],true);var inp_filter=Window_GetElement(window,OxOd501[1],true);var outer=Window_GetElement(window,OxOd501[2],true);var div_demo=Window_GetElement(window,OxOd501[3],true);function UpdateState(){div_demo[OxOd501[5]][OxOd501[4]]=element[OxOd501[5]][OxOd501[4]];} ;function SyncToView(){sel_cursor[OxOd501[6]]=element[OxOd501[5]][OxOd501[7]];if(Browser_IsWinIE()){inp_filter[OxOd501[6]]=element[OxOd501[5]][OxOd501[8]];} ;} ;function SyncTo(element){element[OxOd501[5]][OxOd501[7]]=sel_cursor[OxOd501[6]];if(Browser_IsWinIE()){element[OxOd501[5]][OxOd501[8]]=inp_filter[OxOd501[6]];} ;} ;

</script>
