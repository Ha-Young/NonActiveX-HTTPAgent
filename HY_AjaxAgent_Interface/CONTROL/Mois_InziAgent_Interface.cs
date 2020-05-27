using HY_AjaxAgent_Interface.CONTROL;
using HY_AjaxAgent_Interface.DATA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HY_AjaxAgent_Interface.CONTROL
{
    public class AgentInterface
    {
        /// <summary>
        /// 서버로 데이터를 전송합니다.
        /// </summary>
        /// <param name="sendServerData">서버 데이터</param>
        /// <param name="serverHandle">서버 핸들</param>
        public static void SendToServer(WinMessage_ClientToServer sendServerData, IntPtr serverHandle)
        {
            string sendMsg = Json.ToString(sendServerData);
            byte[] buff = System.Text.Encoding.Default.GetBytes(sendMsg);

            Win32API.COPYDATASTRUCT cds = new Win32API.COPYDATASTRUCT();
            cds.dwData = IntPtr.Zero;
            cds.cbData = buff.Length + 1;
            cds.lpData = sendMsg;

            Win32API.SendMessage(serverHandle, Win32API.WM_COPYDATA, 0, ref cds);
        }
    }
}
