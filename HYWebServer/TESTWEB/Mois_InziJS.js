var INZI_LocalServerURL = "http://127.0.0.1";
var INZI_LocalServerPORT = "5959";

//CustomURI
var INZI_LocalServer_CustomURI = "INZIMoisAgent:";

var setTimeOut = 1000 * 60 * 30; // 30분


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
            async: true,
            crossDomain: true,
            timeout: setTimeOut,

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