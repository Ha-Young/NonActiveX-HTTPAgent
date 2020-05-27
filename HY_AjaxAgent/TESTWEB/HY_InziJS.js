// Mois_InziJS
// writer : hychoi (inzisoft)
// create date : 2020-05-07
// latest update date : 2020-05-22
// history : 
//          2020-05-07 : 생성, ajax통신
//          2020-05-11 : custom uri 추가
//          2020-05-22 : jsonp 추가

var INZI_LocalServerURL = "http://127.0.0.1";
var INZI_LocalServerPORT = "5959";

//CustomURI
var INZI_LocalServer_CustomURI = "MoisInziAgent:";

var timeOutValue = 1000 * 60 * 30; // 30분

//JsonpCallback
var jsonpResultCallbackFunc; //jsonp 콜백함수로 받아 처리 할 콜백함수(EXE로 전송)

function InziEXE_Init() {
    //ToDo 커스텀 URI
    customURIExcute(INZI_LocalServer_CustomURI);
}

//----------------- EXE에 관한 작업 후  -----------------
function InziEXE_GetReturn(interface, callbackFunc) {
    try {
        var remoteURL = INZI_LocalServerURL + ":" + INZI_LocalServerPORT;
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
                if (jqXHR.status == 0) errormsg = "Server None";
                else errormsg = errorMessage;

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
        console.log(remoteURL);
        $.support.cors = true;
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
                if (jqXHR.status == 0) errormsg = "Server None";
                else errormsg = errorMessage;

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
    } catch (ex) {
        // Error 처리
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
    //alert("jsonCallback Message Receive")	// Test

    //var obj = JSON.stringify(json);
    //var ojson = JSON.parse(obj);

    var jsonpString = resultObj;

    if (resultObj.constructor == Object) {
        jsonpString = JSON.stringify(resultObj);
    }

    jsonpString = cSharpStringToJsString(jsonpString);

    // var result = cSharpStringToJsString(jsonpString);
    // ExcuteTimeOut = null;
    jsonpResultCallbackFunc(jsonpString);

    //alert("jsonCallback - Data \n\n" + jsonpString)	// Test
}