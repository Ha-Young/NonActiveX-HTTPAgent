using HYWebServer_Interface.CONTROL;
using HYWebServer_Interface.DATA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.IO;

namespace Mois_LocalWebServer_TEST.LOGIC
{
    public class WEB
    {
        /// <summary>
        /// WEB으로부터 전달받은 req, res 객체를 처리
        /// </summary>
        /// <param name="request">req객체</param>
        /// <param name="response">res객체</param>
        /// <param name="server">server 객체</param>
        public static void Receive(HttpListenerRequest request, HttpListenerResponse response, HYWebServer server)
        {
            var data_text = new StreamReader(request.InputStream, request.ContentEncoding).ReadToEnd();
            var cleaned_data = System.Web.HttpUtility.UrlDecode(data_text);

            webLogging("받은 데이터: " + cleaned_data);

            AJAX_Request receiveData;
            Json.ToObject<AJAX_Request>(cleaned_data, out receiveData);
            HYWebServer.TokenData token = new HYWebServer.TokenData(receiveData, response);

            if(receiveData == null)
            {
                // ToDo. null처리 
                webLogging(">> Request 요청은 받았으나, 전달받은 데이터가 없거나 정해진 Interface 가 지켜지지 않았습니다. <<", true);
                Send(new AJAX_Response(new Response_Error("No Interface", "507", "you send data unruled Interface")), token);
            }

            //###################################################################################################

            EXEKIND exeKind = (EXEKIND)receiveData.EXEKIND;
            string command = receiveData.COMMAND;
            string param = Json.ToString(receiveData.PARAM);
            string errorMsg = string.Empty;

            switch (command)
            {
                case "start":
                    //ToDo. process 실행 확인 웹으로 부터 받은 PARAM "를 """로 바꾼다. -> 실행인자로 넘어가는 string값은 "가 없어지므로 """으로 넘기면 실행인자 값에서 "로 되서 Json처리에 무리가 없다.
                    param = param.Replace("\"", "\"\"\"");
                    errorMsg = HYWebServer.Start_Process(exeKind, param, token, server.Handle);

                    //error발생
                    if (!string.IsNullOrEmpty(errorMsg))
                        Send(new AJAX_Response(new Response_Error(exeKind.ToString(), "500", errorMsg)), token);

                    break;
                case "close":
                    errorMsg = HYWebServer.Stop_Process(exeKind, token);

                    //error발생
                    if (!string.IsNullOrEmpty(errorMsg))
                        Send(new AJAX_Response(new Response_Error(exeKind.ToString(), "500", errorMsg)), token);
                    else
                        Send(new AJAX_Response(new Response(exeKind.ToString(), command, "success")), token);
                    break;
            }
        }

        /// <summary>
        /// WEB으로 결과값 전달
        /// </summary>
        /// <param name="sendData"></param>
        /// <param name="token"></param>
        public static void Send(AJAX_Response sendData, HYWebServer.TokenData token)
        {
            //PARAM, PATH 처리
            string responseString = Json.ToString(sendData);

            HYWebServer.AddText(HYWebServer.MSGVIEW.WEB, "보내는 데이터 : " + responseString);

            writeResponse(responseString, token.response);
        }

        private static void writeResponse(string Msg, HttpListenerResponse response)
        {
            byte[] buffer = System.Text.Encoding.UTF8.GetBytes(Msg);
            response.ContentLength64 = buffer.Length;

            using (System.IO.Stream output = response.OutputStream)
                output.Write(buffer, 0, buffer.Length);

            response.Close();
        }

        private static void webLogging(string logMsg, bool isError = false)
        {
            HYWebServer.AddText(HYWebServer.MSGVIEW.WEB, logMsg);
            if (isError)
                Log.Instance.Error(logMsg);
            else
                Log.Instance.Info(logMsg);
        }
    }
}
