<%@ Page Language="c#" Inherits="CuteChat.DialogSendFile" %>
<%@ Import Namespace="System.IO" %>
<%@ Import Namespace="CuteChat" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
	<HEAD runat=server>
		<title>[[UI_SendFile]]</title>
        <link rel="icon" href="Icons/file.ico" type="image/x-icon" />
		<link rel="shortcut icon" href="Icons/file.ico" type="image/x-icon" />
		<link rel="stylesheet" href="IM_Style.css">
		   
    <script>
      //#
     
     
      function breakWord(dEl){
        
        
        if(!dEl || dEl.nodeType !== 1){
          
          return false;
        
        } else if(dEl.currentStyle && typeof dEl.currentStyle.wordBreak === 'string'){
          
          //Lazy Function Definition Pattern, Peter's Blog
          //From http://peter.michaux.ca/article/3556
          
          breakWord = function(dEl){
            //For Internet Explorer
            dEl.runtimeStyle.wordBreak = 'break-all';
            return true;
          }
          
          return breakWord(dEl);
         
        }else if(document.createTreeWalker){
       
          //Faster Trim in Javascript, Flagrant Badassery
          //http://blog.stevenlevithan.com/archives/faster-trim-javascript
          
          var trim = function  (str) {
            var	str = str.replace(/^\s\s*/, ''),
            ws = /\s/,
            i = str.length;
            while (ws.test(str.charAt(--i)));
            return str.slice(0, i + 1);
          }
          
          //Lazy Function Definition Pattern, Peter's Blog
          //From http://peter.michaux.ca/article/3556
          
          breakWord = function(dEl){
            
            //For Opera, Safari, and Firefox
            var dWalker = document.createTreeWalker(dEl, NodeFilter.SHOW_TEXT, null, false);
            
            var c = String.fromCharCode('8203');
            
            while (dWalker.nextNode())
            {
              var node = dWalker.currentNode;
              //we need to trim String otherwise Firefox will display 
              //incorect text-indent with space characters
              var s = trim( node.nodeValue ) .split('').join(c);
              node.nodeValue = s;
            }
            return true;
          }
          
          return breakWord(dEl);
          
          
        }else{
          return false;
        }
      }
      
      
      
    </script>
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
	</head>
	<BODY style="BACKGROUND-COLOR: #ffffff">
		<form id="Form1" method="post" runat="server">
			<div class="dialogPageHeader">
				<table width="100%" background='images/up.gif' cellpadding="4" cellspacing="0" border="0">
					<tr>
						<td width="60" height="50" align="center" valign="middle"><img src='images/upload48.png'>
						<td>
						<td valign="middle">
							<strong>[[UI_SendFile]]</strong>
							<br>
							[[UI_Send_a_File_subTitle]]
						</td>
					</tr>
				</table>
			</div>
			<div Style="margin-bottom: 10px; padding: 15px 10px 10px 10px;"> 					
					<fieldset id="fieldsetUpload" style="border:1px solid #9EB3E1; padding:10px">
						<legend>
							[[MaxFileSizeMustBeLessThan]] <%= ChatWebUtility.FormatSize(setting_maxfilesize) %>
						</legend>
						<br>						
						<asp:Label ID="Label_PictureUpload" Runat="server"></asp:Label>
						<input id="input_file" size="30" type="file" runat="server" style="HEIGHT:20px;Width:300px" NAME="input_file">&nbsp; <br><br>
						<asp:Button id="btnUpload" runat="server" Text="[[UI_SEND]]"></asp:Button>&nbsp;
					</fieldset>		
					<br>
					<div class="break-word">
						<asp:Label id="result" runat="server" ForeColor="#4886C5">Label</asp:Label>
					</div>	
				</div>
			<div class="dialogPageButtons">
				<table width="100%" border="0" cellspacing="0" cellpadding="0" background='images/up.gif'>
					<tr height="60">
						<td align="center" valign="top" class="dialogButtonRow">
							<button id="btncancel" style="WIDTH:72px" type="button">[[UI_Close]]</button>
						</td>
					</tr>
				</table>
			</div>
		</form>
		<script>
		document.getElementById("btncancel").onclick=function()
		{
			(top.cc_close||top.close)();
		}
		if(<%=filesendok?"1":"0"%>)
		{
			var msg=document.getElementById("result").innerHTML.replace(/<[^>]*>/g,"");;
			alert(msg);
			(top.cc_close||top.close)();
		}
		      //Break All Words
      void function(){
        var aEl = document.getElementsByTagName('div');
        var dEl,i;
        var sName = "break-word";
        for(i=0;dEl = aEl[i];i++){
          if(dEl.className.match(new RegExp('(\\s|^)' + sName + '(\\s|$)'))){
            breakWord(dEl);
          }
        }
      }();
		</script>
	</BODY>
</html>

<script runat=server>


</script>