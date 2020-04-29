using HYWebServer_Interface.DATA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mois_LocalWebServer_TEST.LOGIC
{
    public class EXE
    {
        /// <summary>
        /// EXE로부터 전달받은 메세지를 처리
        /// </summary>
        /// <param name="receiveData"></param>
        /// <param name="token"></param>
        public static void Receive(WinMessage_ClientToServer receiveData, HYWebServer.TokenData token)
        {
            switch (receiveData.RESULT_COMMAND)
            {
                case "workComplete":
                    AJAX_Response sendData = new AJAX_Response(receiveData.PARAM);

                    HYWebServer.sendToWEB(sendData, token);
                    break;

                case "startSuccess":
                    exeLogging(receiveData.PROCESS_KIND.ToString() + "프로그램 실행 완료 메세지 받음");
                    break;

                case "closeProcess":
                    exeLogging(receiveData.PROCESS_KIND.ToString() + "프로그램 종료 메세지");
                    break;

                default:
                    exeLogging("알 수 없는 EXE Command 받음 : " + receiveData.RESULT_COMMAND);
                    break;
            }
        }

        public static void Send(WinMessage_ServerToClient sendData)
        {
            // 해당 부분 사용하려면 각 EXE마다 WndProc(윈도우프로시져) 함수 오버라이딩 되어 있어야 함!
        }

        private static void exeLogging(string logMsg)
        {
            HYWebServer.AddText(HYWebServer.MSGVIEW.Process, "");
        }
    }
}
