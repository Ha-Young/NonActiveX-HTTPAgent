using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.IO;
using HY_AjaxAgent_Interface.DATA;
using HY_AjaxAgent_Interface.CONTROL;
using HY_AjaxAgent.CONTROL;

namespace HY_AjaxAgent.LOGIC
{
    public class WEB
    {
        /// <summary>
        /// WEB으로부터 전달받은 req, res 객체를 처리
        /// </summary>
        /// <param name="request">req객체</param>
        /// <param name="response">res객체</param>
        /// <param name="server">server 객체</param>
        public static void Receive(HttpListenerRequest request, HttpListenerResponse response, HY_AjaxAgent server)
        {
            Log.Instance.Info("Receive...!");
            string data_text = new StreamReader(request.InputStream, request.ContentEncoding).ReadToEnd();
            string cleaned_data = System.Web.HttpUtility.UrlDecode(data_text);
             
            //jsonp 처리 hychoi
            bool isJsonp = false;

            Log.Instance.Info("Check post method or jsonp by checking cleaned_data(post) and querystring callback");
            
            string callback = request.QueryString["callback"];
            
            //jsonp 처리 hychoi
            if (string.IsNullOrEmpty(cleaned_data) && !string.IsNullOrEmpty(callback))
            {
                //jsonp
                if (callback == "jsonCallback")
                    cleaned_data = request.QueryString[1];

                if (string.IsNullOrEmpty(cleaned_data) && string.IsNullOrEmpty(callback) && request.QueryString.Count > 1)
                    cleaned_data = request.QueryString[1];

                cleaned_data = System.Web.HttpUtility.UrlDecode(cleaned_data);

                webLogging("jsonp 연결 확인 : ");
                response.ContentType = "application/json";
                isJsonp = true;
            }

            webLogging("받은 데이터: " + cleaned_data);

            AJAX_Request receiveData;
            Json.ToObject<AJAX_Request>(cleaned_data, out receiveData);
            HY_AjaxAgent.TokenData token = new HY_AjaxAgent.TokenData(receiveData, response, new HY_AjaxAgent.TokenData.JsonPCheck(isJsonp, callback));

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
                    //ToDo. process 실행 확인
                    param = param.Replace("\"", "\"\"\"");
                    errorMsg = HY_AjaxAgent.Start_Process(exeKind, param, token, server.Handle);
                    
                    //error발생
                    if (!string.IsNullOrEmpty(errorMsg))
                        Send(new AJAX_Response(new Response_Error(exeKind.ToString(), "500", errorMsg)), token);

                    break;
                case "close":
                    errorMsg = HY_AjaxAgent.Stop_Process(exeKind, token);

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
        public static void Send(AJAX_Response sendData, HY_AjaxAgent.TokenData token)
        {
            //PARAM, PATH 처리
            string responseString = Json.ToString(sendData);

            //jsonp 처리 hychoi
            if (token.jsonpCheck.isJsonp)
            {
                responseString = string.Format("{0}({1});", token.jsonpCheck.callback, responseString);
            }

            HY_AjaxAgent.AddText(HY_AjaxAgent.MSGVIEW.WEB, "보내는 데이터 : " + responseString);

            writeResponse(responseString, token.response);
        }

        public static void writeResponse(string Msg, HttpListenerResponse response)
        {
            byte[] buffer = System.Text.Encoding.UTF8.GetBytes(Msg);
            response.ContentLength64 = buffer.Length;

            using (System.IO.Stream output = response.OutputStream)
                output.Write(buffer, 0, buffer.Length);

            response.Close();
        }

        private static void webLogging(string logMsg, bool isError = false)
        {
            HY_AjaxAgent.AddText(HY_AjaxAgent.MSGVIEW.WEB, logMsg);
            if (isError)
                Log.Instance.Error(logMsg);
            else
                Log.Instance.Info(logMsg);
        }

        private static string DecodeMsgFromWebClient(string orgStr)
        {
            //한글인 글자들은 유니코드형식으로 들어온다.
            //유니코드를 각각 1byte의 askii로 들어오기때문에
            //%로 시작되는 문자가 발견되면, 뒤 5자리(예시...%uAE00) 까지 확인하여
            //한글로 바꿔준다.

            string queryStr = orgStr;
            string returnValue = string.Empty;

            byte[] queryBytes = Encoding.UTF8.GetBytes(queryStr);
            char[] queryChars = queryStr.ToCharArray();


            List<byte> bytesFormatted = new List<byte>();
            for (int i = 0; i < queryChars.Length; i++)
            {
                if (queryChars[i] == '%' && queryChars.Length > i + 5)
                {
                    byte[] bytes = { queryBytes[i + 2], queryBytes[i + 3], queryBytes[i + 4], queryBytes[i + 5] };
                    char[] value = { queryChars[i + 2], queryChars[i + 3], queryChars[i + 4], queryChars[i + 5] };
                    string str = new string(value);

                    int nnn = Convert.ToInt32(str, 16);
                    char word = (char)nnn;

                    byte[] input = Encoding.UTF8.GetBytes(word.ToString());

                    bytesFormatted.InsertRange(bytesFormatted.Count, input);
                    i = i + 5;
                }
                else
                {
                    bytesFormatted.Add(queryBytes[i]);
                }
            }

            returnValue = Encoding.UTF8.GetString(bytesFormatted.ToArray());

            return returnValue;
        }
    }
}
