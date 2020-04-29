 // 시스템명		: Inzisoft NonActiveX Scan(iFormScan.EXE) 공통모듈 .js
 // 파  일  명		: inziJS-1.0
 // 작  성  자		: 인지소프트 최하영
 // 작성일자		: 2019.01.11
 // 관련모듈		: .
 // 처리내용		: Scan NonActiveX EXE를 위한 공통 기능 구현(실행, 데이터 송수신 등)
 
 // History	: 2019.01.11, 최하영, 최초작성
 //           2019.01.22, scanEXE, APIEXE Ajax 통신 포트(http, https) 두개로 나눔.(LocalWebServer)

//currentVersion
var currentVersion = "1.0.0";
 
//http, https 중 webserver 프로토콜에 맞게 사용
var	protocol = "http";

//CustomURI
var uri = "iFormScan:"; 
var uri_api = "iFormAPI:";
var uri_preview = "iFormPreview:";

//exeCheck
var ExcuteTimeOut = 3000;
var ExcuteTimeOut_API = 3000;

//----------------- 공통 스캔 EXE 실행 -----------------
// --------WorkKind - 0: 담전(채권담보)
// --------           1: 법전(법인)
// --------           2: 부전(부동산)
function startEXE_Scan(WorkKind) 
{
	var task = WorkKind;  		
	
	//var customURI = uri + task;	
	//Excute(customURI);
	
	LauncherConnect("Launch_Scan", task, true);
}

// ------------- 공통 스캔 EXE 종료 -------------
function closeEXE_Scan()
{
	LauncherConnect("Terminate_Scan", null, true);
}

//----------------- 인감 스캔 EXE 실행 ----------------- 
function startEXE_ScanSeal() 
{
	var task = "9";  						//9 -> 인감 스캔
	
	//var broserpos = getBrowserPos();	    //브라우저 좌표 값 획득
	//var customURI = uri + task;	
	
	//Excute(customURI);
	LauncherConnect("Launch_Scan", task, true);
}

// ------------- 인감 스캔 스캔 EXE 종료 -------------
function closeEXE_ScanSeal()
{
	LauncherConnect("Terminate_Scan", null, true);
}

//----------------- 확정일자 스캔 EXE 실행 ----------------- 
function startEXE_DefineDate() 
{
	var task = "19";  						//19 -> 인감 스캔
	
	//var customURI = uri + task;	
	//Excute(customURI);
	
	LauncherConnect("Launch_Scan", task, true);
}

//----------------- 확정일자 스캔 EXE 종료 ----------------- 
function closeEXE_DefineDate()
{
	LauncherConnect("Terminate_Scan", null, true);
}

//----------------- 일반 API(File&Folder Control EXE) 실행 ----------------- 
function startEXE_GeneralAPI() 
{
	var customURI = uri_api;	
	
	//Excute(customURI);
	LauncherConnect("Launch_API", null, true);
}

//----------------- 일반 API(File&Folder Control EXE) 종료 ----------------- 
function closeEXE_GeneralAPI()
{
	LauncherConnect("Terminate_API", null, true);
}

//----------------- 미리보기 EXE 실행----------------- 
function startEXE_Preview(downFilepath) 
{
	var filePath = downFilepath;
	//downFilepath = downFilepath.replace("\\", "/").replace(/\\/g, "/");
	//var customURI = uri_preview + downFilepath;	
	//Excute(customURI);
	LauncherConnect("Launch_Preview", filePath, true);
}

//----------------- 미리보기 EXE 종료----------------- 
function closeEXE_Preview() 
{
	LauncherConnect("Terminate_Preview", null, true);
}

//현재 버젼 체크
function checkVersion(versionCheckSuccessFunc, versionCheckFailFunc)
{
	LauncherConnect("Check_Version", customURI, true);
	
}

//----------------CustonURI Version----------------
//----------------- EXE 실행(IE체크) ----------------- 
// function Excute(customURI)
// {
	// EXEflag = false;
	// var agent = navigator.userAgent.toLowerCase();
	// var exeOpener;
	// if((navigator.appName == 'Netscape' && navigator.userAgent.search('Trident') != -1) || (agent.indexOf("msie") != -1))  //ie이면,
	// {
		// exeOpener = window.open(customURI, "_blank", "toolbar=no,scrollbars=no,resizable=no,top=0,left=0,width=1,height=1");
		// setTimeout(function(){exeOpener.close();},500);
	// }
	// else{ //ie가 아니면
		// exeOpener = window.open(customURI, "_self");
		// // exeOpener = window.open(customURI, "_blank", "toolbar=no,scrollbars=no,resizable=no,top=0,left=0,width=1,height=1");
		// // setTimeout(function(){exeOpener.close();},500);
	// }
	// // location.href = customURI;
	// // location.replace(customURI);
// }

//----------------- 첫번째 Connection.(EXE 실행 체크) ----------------- 
function CheckEXEWithConnect(firstConnectionFunc, failConnectionFunc)
{
	ExcuteTimeOut = 3000;
	setTimeout(function(){
		tryConnect(firstConnectionFunc, failConnectionFunc);
	}, 2000);
}

function tryConnect(firstConnectionFunc, failConnectionFunc){
	//console.log("첫번째 연결 시도...");
	EXEConnectWait_("CHECK", "null", 0, firstConnectionFunc
	, function(){
		//console.log("두번째 연결 시도...");
		EXEConnectWait_("CHECK", "null", 0, firstConnectionFunc
		, function(){
			//console.log("세번째 연결 시도...");
			EXEConnectWait_("CHECK", "null", 0, firstConnectionFunc, failConnectionFunc);
		});
	});
}

//------------- EXE 실행 및 버젼체크 --------------
function LauncherConnect(Work_, Data_, isAsync)					
{
	var strWorkString = getWorkString(Work_, Data_, 0);
	AjaxRequest_GetReturn_Launcher(strWorkString, connectLauncherCallbackFunc, isAsync);
}

function connectLauncherCallbackFunc(result)
{
	result = cSharpStringToJsString(result);
	alert("alert result : " + result);
}

//----------------- C#에서 넘어 온 string을 javascript 형식에 맞게끔 치환 ----------------- 
function cSharpStringToJsString(cSharpString)
{
	var result;
	result = cSharpString.replace(/\\n/g, "\\n")
								.replace(/\\'/g, "\\'")
								.replace(/\\"/g, '\\"')
								.replace(/\\&/g, "\\&")
								.replace(/\\r/g, "\\r")
								.replace(/\\t/g, "\\t")
								.replace(/\\b/g, "\\b")
								.replace(/\\f/g, "\\f");
								
	result = result.replace(/[\u0000-\u0019]+/g,"");
	
	return result;
}

//----------------- 브라우저 좌표 획득 ----------------- 
function getBrowserPos()
{
	var xpos = window.screenX;
	var ypos = window.screenY;
	var width = window.outerWidth;
	var height = window.outerHeight;
	
	return xpos + "," + ypos + "," + width + "," +height;
}

function restartAPIEXE()
{
	startEXE_GeneralAPI();
	alert("GeneralAPI가 실행되어 있지 않아 GeneralAPI를 실행합니다.");
}

//----------------- TLSLocalServer1.dll에 연결 (iFormScan.EXE) ----------------- 
function AjaxRequest_GetReturn(WorkString, callbackFunc, timeOutCallback)
{
	var port = "";
	
	try{
		if(protocol == "https"){
			port = "4490";
		}else if(protocol == "http"){
			port = "4491";
		}
		Call_Ajax(port, WorkString, callbackFunc, timeOutCallback);
	}
	catch(ex)
	{
		alert(ex.message);
	}
}

//----------------- TLSLocalServer2.dll에 연결 (iFormAPI.EXE) ----------------- 
function AjaxRequest_GetReturn_API(WorkString, callbackFunc, isAsync)
{
	var port = "";
	
	try{
		if(protocol == "https"){
			port = "4492";
		}else if(protocol == "http"){
			port = "4493";
		}
		Call_Ajax_API(port, WorkString, callbackFunc, restartAPIEXE, isAsync);
	}
	catch(ex)
	{
		alert(ex.message);
	}
}

//----------------- TLSLocalServer.dll3에 연결 (Launcher) ----------------- 
function AjaxRequest_GetReturn_Launcher(WorkString, callbackFunc, isAsync)
{
	var port = "";
	
	try{
		if(protocol == "https"){
			port = "4494"; 			//Launcher가 사용하는 TLSLocalServer의 port로 바꿔주셔야 합니다.
		}else if(protocol == "http"){
			port = "4495";			//Launcher가 사용하는 TLSLocalServer의 port로 바꿔주셔야 합니다.
		}
		Call_Ajax_Launcher(port, WorkString, callbackFunc, LauncherErrcallbackFunc, isAsync);
	}
	catch(ex)
	{
		alert(ex.message);
	}
}
//----------------- Launcher ErrorCallbacks
function LauncherErrcallbackFunc()
{
	
}

//----------------- TLSLocalServer URL획득 ----------------- 
function getURL(port){
	var param = "/NeedWait/1";
	var encodeString=encodeURI(param);
	encodeString=encodeString.replace("#", "%23");
	return protocol + "://127.0.0.1:" + port + encodeString;
}
//----------------- Ajax call And get Return for callbackFunc ----------------- 
function Call_Ajax(port, WorkString, callbackFunc, timeOutCallback)
{
		var remoteURL = getURL(port);
		var workException=null;
		var result=null;
		$.support.cors=true;
		$.ajax({
			url :  remoteURL,
			type : "POST",
			dataType : "text",
			data : WorkString,
			async : true,
			cache : false,
			crossDomain : true,
			crossOrigin : null,
			contentType: "application/x-www-form-urlencoded; charset=UTF-8",
			timeout:ExcuteTimeOut,
			error : function(request,status,error) {
				//alert("code:"+request.status+"\n"+"message:"+request.responseText+"\n"+"error:"+error);
				if(error != null && timeOutCallback != null)
					timeOutCallback();
				else
					defaultErrCallback(request, error);
			},
			beforeSend:function(){
				$('.wrap-loading').removeClass('display-none');
			},
			success : function(result){
				result = cSharpStringToJsString(result);
				ExcuteTimeOut = null;
				callbackFunc(result);
			},
			complete : function(){
				$('.wrap-loading').addClass('display-none');
			}
		});
		if(workException!=null)
			throw new Error(workException);
		else
		{
			
		}
}

//----------------- Ajax call And get Return for callbackFunc For APIEXE----------------- 
function Call_Ajax_API(port, WorkString, callbackFunc, errorCallback, isAsync)
{
		var remoteURL = getURL(port);
		var workException=null;
		var result=null;
		$.support.cors=true;
		$.ajax({
			url : remoteURL,
			type : "POST",
			dataType : "text",
			data : WorkString,
			async : isAsync,
			cache : false,
			crossDomain : true,
			crossOrigin : null,
			contentType: "application/x-www-form-urlencoded; charset=UTF-8",
			timeout:ExcuteTimeOut_API,
			error : function(request,status,error) {
				//alert("code:"+request.status+"\n"+"message:"+request.responseText+"\n"+"error:"+error);
				
				if(request.status == 0 || request.status == 12029) //API EXE가 없어서 응답오류가 날 때.
					errorCallback();
				else
					defaultErrCallback(request, error);
			},
			beforeSend:function(){
				$('.wrap-loading').removeClass('display-none');
			},
			success : function(result){
				result = cSharpStringToJsString(result);
				callbackFunc(result);
			},
			complete : function(){
				$('.wrap-loading').addClass('display-none');
			}
		});
		if(workException!=null)
			throw new Error(workException);
		else
		{
			
		}
}

//----------------- Ajax call And get Return for callbackFunc For APIEXE----------------- 
function Call_Ajax_Launcher(port, WorkString, callbackFunc, errorCallback, isAsync)
{
		var remoteURL = getURL(port);
		var workException=null;
		var result=null;
		$.support.cors=true;
		$.ajax({
			url : remoteURL,
			type : "POST",
			dataType : "text",
			data : WorkString,
			async : isAsync,
			cache : false,
			crossDomain : true,
			crossOrigin : null,
			contentType: "application/x-www-form-urlencoded; charset=UTF-8",
			timeout:ExcuteTimeOut_API,
			error : function(request,status,error) {
				//alert("code:"+request.status+"\n"+"message:"+request.responseText+"\n"+"error:"+error);
				
				if(request.status == 0 || request.status == 12029) //API EXE가 없어서 응답오류가 날 때.
					errorCallback();
				else
					defaultErrCallback(request, error);
			},
			beforeSend:function(){
				$('.wrap-loading').removeClass('display-none');
			},
			success : function(result){
				result = cSharpStringToJsString(result);
				callbackFunc(result);
			},
			complete : function(){
				$('.wrap-loading').addClass('display-none');
			}
		});
		if(workException!=null)
			throw new Error(workException);
		else
		{
			
		}
}


function defaultErrCallback(request, status, error)
{
	workException="서버와 통신 중 오류가 발생하였습니다.";
	workException+="\n";
	workException+="code : " + request.status + " , " + request.statusText + " , " + error.message;
}

//----------------- scanEXE에 Data보내고 Wait(callbackFunc) ----------------- 
function EXEConnectWait(Work_, Data_, dataType, callbackFunc)   //dataType : Data_의 타입 지정 (0 : string Data Type/ 1 : Json or JsonArray) 
{
	var strWorkString = getWorkString(Work_, Data_, dataType);
	AjaxRequest_GetReturn(strWorkString, callbackFunc, null);
}

function EXEConnectWait_(Work_, Data_, dataType, callbackFunc, timeOutCallback)
{
	var strWorkString = getWorkString(Work_, Data_, dataType);
	AjaxRequest_GetReturn(strWorkString, callbackFunc, timeOutCallback);
}

//----------------- scanEXE에 Data보내기 (리턴 값 없을 때)  ----------------- 
function EXEConnect(Work_, Data_, dataType)						//dataType : Data_의 타입 지정 (0 : string Data Type/ 1 : Json or JsonArray) 
{
	var strWorkString = getWorkString(Work_, Data_, dataType);
	AjaxRequest_GetReturn(strWorkString, defaultcallbackFunc, null);
}

//----------------- API EXE에 Data보내고 Wait(callbackFunc)  ----------------- 
function APIConnect (Work_, Data_, dataType)					//dataType : Data_의 타입 지정 (0 : string Data Type/ 1 : Json or JsonArray) 
{
	var strWorkString = getWorkString(Work_, Data_, dataType);
	AjaxRequest_GetReturn_API(strWorkString, defaultcallbackFunc, true);
}

//----------------- API EXE에 Data보내기 (리턴 값 없을 때)  ----------------- 
function APIConnectWait(Work_, Data_, dataType, callbackFunc)	//dataType : Data_의 타입 지정 (0 : string Data Type/ 1 : Json or JsonArray) 
{
	var strWorkString = getWorkString(Work_, Data_, dataType);
	AjaxRequest_GetReturn_API(strWorkString, callbackFunc, true);
}

//---------------- API EXE에 Data보내기 <동기방식> ---------------
function APIConnect_Sync(Work_, Data_, dataType)
{
	var strWorkString = getWorkString(Work_, Data_, dataType);
	AjaxRequest_GetReturn_API(strWorkString, defaultcallbackFunc, false);
}
//----------------- EXE에 전송 할 WorkString 가져오기 ----------------- 
function getWorkString(Work_, Data_, dataType)					//dataType : Data_의 타입 지정 (0 : string Data Type/ 1 : Json or JsonArray) 
{
	if(dataType == 0){
		return "{ 'Work_': '" + Work_ + "', 'Data_': '" + Data_ + "' }";
	}else{
		return "{ 'Work_': '" + Work_ + "', 'Data_': " + Data_ + " }";
	}
}

//----------------- Default callbackFunc. 아무 기능 X. 리턴 값 없을 때 사용 ----------------- 
function defaultcallbackFunc(result)
{
	//result = cSharpStringToJsString(result);
	//...
	//console.log("DefaultCallbackFunc... Do Nothing...");
}

function ajaxGetSync(Work_, Data_)
{
	var WorkString = getWorkString(Work_, Data_, 0);
	var port = "";
	
	try{
		if(protocol == "https"){
			port = "4492";
		}else if(protocol == "http"){
			port = "4493";
		}
		
		var remoteURL = getURL(port);
		
		$.ajax({
			url : remoteURL,
			type : "POST",
			dataType : "text",
			data : WorkString,
			async : false
		});
	}
	catch(ex)
	{
		console.error("InziJS-1.0.js - ajaxGetSync() ERROR!!!! Check this Func \nMessage:" + ex.message);
	}
}

function setCloseAPIEXE_beforeUnload(){
	window.onbeforeunload = function(e){
		e = e || window.event;
		
		if(e){
			e.returnValue = "API EXE를 종료합니다.";
		}
		alert("asdf");
		ajaxGetSync("CLOSEPROGRAM", "null");
		
		return "API EXE를 종료합니다.";
	}
}

function initJS(){
	var css = $("<link>", { 
		"rel" : "stylesheet",
		"type" : "text/css",
		"href" : "C:\\INZISOFT\\NONACTIVEX\\iFormScan\\BIN\\inziCSS.css"
	})[0];
	document.getElementsByTagName("head")[0].appendChild(css);
	
	//DIV 생성
	var div_ = document.createElement('div');
	var img_ = document.createElement('img');
	img_.src = "C:\\INZISOFT\\NONACTIVEX\\iFormScan\\BIN\\loading.gif";
	div_.className='wrap-loading display-none';
	div_.appendChild(img_);	
	
	if(document.body != null)
		document.getElementsByTagName('body')[0].appendChild(div_);
	
}
initJS();