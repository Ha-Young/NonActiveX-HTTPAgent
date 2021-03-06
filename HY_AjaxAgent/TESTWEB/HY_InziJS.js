// Mois_InziJS
// writer : hychoi (inzisoft)
// create date : 2020-05-07
// latest update date : 2020-05-22
// history : 
//          2020-05-07 : 생성, ajax통신
//          2020-05-11 : custom uri 추가
//          2020-05-22 : jsonp 추가
//          2020-09-25 : jsonp servercheck 추가 (jsonp로 서버 가동 안되어있을 때 재실행 유도하기 위해.)

var INZI_LocalServerURL = "http://127.0.0.1";
var INZI_LocalServerPORT = "5959";

//CustomURI
var INZI_LocalServer_CustomURI = "MoisInziAgent:";

var timeOutValue = 1000 * 60 * 30; // 30분
//2020-09-25 hychoi jsonp server check 추가
var serverCheckTimeOut = 1500; // server check tiemout 2초

//JsonpCallback
var jsonpResultCallbackFunc; //jsonp 콜백함수로 받아 처리 할 콜백함수(EXE로 전송)
//2020-09-25 hychoi jsonp server check 추가
var servercheck_success;

function InziEXE_Init() {
    //ToDo 커스텀 URI
    customURIExcute(INZI_LocalServer_CustomURI);
}

//----------------- EXE에 관한 작업 후  -----------------
function InziEXE_GetReturn(interface, callbackFunc) {
    try {
        var remoteURL = INZI_LocalServerURL + ":" + INZI_LocalServerPORT;
        var errStatus;
        console.log(remoteURL);
        $.support.cors = true;
        $.ajax({
            url: remoteURL,
            type: "POST",
            dataType: "text",
            data: interface,
            async: true, //false로 동기방식 전환 가능. 동기 방식시 웹페이지 DisEnable처리 필요(블러 같은)
            crossDomain: true,
            timeout: timeOutValue,

            error: function(jqXHR, errorMessage) {
                // Error 처리
                alert(jqXHR.status);
                if (jqXHR.status == 0 || jqXHR.status == 12029) {
                    errormsg = "Server None";
                    errStatus = 0;
                } else {
                    errormsg = errorMessage;
                    errStatus = jqXHR.status;
                }

                var errorObj = {
                    ErrorStatus: errStatus,
                    ErrorLocation: "InziJS",
                    ErrorMsg: errormsg,
                    detail: jqXHR,
                };

                callbackFunc(JSON.stringify(errorObj));
            },
            success: function(result) {
                // result : EXE로 받은 리턴값
                result = cSharpStringToJsString(result);
                callbackFunc(result); // 콜백함수로 처리
            },
        }).done(function(response) {
            alert(response);
            console.log(response);
        });
    } catch (ex) {
        // Error 처리
    }
}

//----------------- EXE에 관한 작업 후  -----------------
function InziEXE_GetReturn_jsonp(interface, callbackFunc) {
    try {
        jsonpResultCallbackFunc = callbackFunc;
        interface = escape(encodeURIComponent(interface));

        var remoteURL = INZI_LocalServerURL + ":" + INZI_LocalServerPORT;
        var errStatus;
        console.log(remoteURL);
        $.support.cors = true;
        //2020-09-25 hychoi jsonp server check 추가
        servercheck_success = function() {
            $.ajax({
                url: remoteURL,
                type: "POST",
                dataType: "jsonp",
                jsonp: "callback",
                jsonpCallback: "jsonCallback",
                contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                data: interface,
                async: true, //false로 동기방식 전환 가능. 동기 방식시 웹페이지 DisEnable처리 필요(블러 같은)
                crossDomain: true,
                timeout: timeOutValue,
    
                error: function(jqXHR, errorMessage) {
                    // Error 처리
    
                    if (jqXHR.status == 0 || jqXHR.status == 12029) {
                        errormsg = "Server None";
                        errStatus = 0;
                    } else {
                        errormsg = errorMessage;
                        errStatus = jqXHR.status;
                    }
    
                    var errorObj = {
                        errorStatus: jqXHR.status,
                        errorLocation: "InziJS",
                        errorMsg: errormsg,
                        detail: jqXHR,
                    };
    
                    callbackFunc(JSON.stringify(errorObj));
                },
                success: function(result) {
                    // result : EXE로 받은 리턴값
                    // callbackFunc(result); // 콜백함수로 처리
                    // jsonpCallback 함수로 처리된다.
                },
            }).done(function(response) {
                // alert(response);
                console.log(response);
            });
        }
        
        //2020-09-25 hychoi jsonp server check 추가
        // AJAX Request로 전달 될 Interface 객체
        var serverCheckInterface = {
            EXEKIND: 0,
            COMMAND: "server_check", // 명령어 (start : 실행 ... 추후 구현 가능)
            PARAM: 0 // 명령어에 대한 파라미터 
        }
        var serverCheckInterface = JSON.stringify(serverCheckInterface);
        serverCheckInterface = escape(encodeURIComponent(serverCheckInterface));
        //2020-09-25 hychoi jsonp server check 추가
        $.support.cors = true;
        $.ajax({
            url: remoteURL,
            type: "POST",
            dataType: "jsonp",
            jsonp: "callback",
            contentType: "application/x-www-form-urlencoded; charset=UTF-8",
            data: serverCheckInterface,
            async: true, //false로 동기방식 전환 가능. 동기 방식시 웹페이지 DisEnable처리 필요(블러 같은)
            crossDomain: true,
            timeout: serverCheckTimeOut,
            jsonpCallback: "successServerCheckCallback",
            
            // 서버 체크 실패시에 처리.
            error: function(jqXHR, errorMessage) {
                // Error 처리
                if (errorMessage === "timeout") {
                    errormsg = "Server None";
                    errStatus = 0;
                } else if (jqXHR.status == 0 || jqXHR.status == 12029) {
                    errormsg = "Server None";
                    errStatus = 0;
                } else {
                    errormsg = errorMessage;
                    errStatus = jqXHR.status;
                }

                var errorObj = {
                    errorStatus: jqXHR.status,
                    errorLocation: "InziJS",
                    errorMsg: errormsg,
                    detail: jqXHR,
                };

                callbackFunc(JSON.stringify(errorObj));
            },
        }).done(function(response) {
            // alert(response);
            console.log(response);
        });
    } catch (ex) {
        var errorObj = {
            errorStatus: 0,
            errorLocation: "InziJS",
            errorMsg: ex,
        };
        
        callbackFunc(JSON.stringify(errorObj));
    }
}

function GetCustomURI() {
    return INZI_LocalServer_CustomURI;
}

function SetEXETimeout(timeOut) {
    timeOutValue = timeOut;
}


//----------------- C#에서 넘어 온 string을 javascript 형식에 맞게끔 치환 -----------------
function cSharpStringToJsString(cSharpString) {
    var result;
    result = cSharpString
        .replace(/\\n/g, "\\n")
        .replace(/\\'/g, "'")
        .replace(/\\"/g, '"')
        .replace(/\\&/g, "\\&")
        .replace(/\\r/g, "\\r")
        .replace(/\\t/g, "\\t")
        .replace(/\\b/g, "\\b")
        .replace(/\\f/g, "\\f")
        .replace('\\"', '"')
        .replace("\\'", "'");

    result = result.replace(/[\u0000-\u0019]+/g, "");

    return result;
}

function customURIExcute(customURI) {
    var doc = (window.parent) ? window.parent.document : window.document;

    var tmp = doc.querySelector("#InziCustomURILink");

    if (!tmp) {
        var uriLink = doc.createElement("a");

        uriLink.id = 'InziCustomURILink';
        uriLink.href = INZI_LocalServer_CustomURI;
        uriLink.innerHTML = "INZI CustomURI Link";
        uriLink.setAttribute("style", "display:none");
        doc.body.appendChild(uriLink);
    }

    $('#InziCustomURILink').get(0).click();
}

//--------- jsonp Callback 처리
function jsonCallback(resultObj) {
    var jsonpString = resultObj;

    if (resultObj.constructor == Object) {
        jsonpString = JSON.stringify(resultObj);
    }

    jsonpString = cSharpStringToJsString(jsonpString);

    jsonpResultCallbackFunc(jsonpString);
}

//2020-09-25 hychoi jsonp server check 추가
function successServerCheckCallback(resultObj) {
    if (resultObj.constructor == Object && resultObj.RESULT.COMMAND == "server_check" && resultObj.RESULT.RESULT == "success") {
        servercheck_success();
    }
    else {
        var errorObj = {
            errorStatus: 0,
            errorLocation: "InziJS",
            errorMsg: "jsonp server check error",
            detail: "jsonp server check error...!",
        };

        jsonpResultCallbackFunc(JSON.stringify(errorObj));
    }
}