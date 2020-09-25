using HY_AjaxAgent_Interface.CONTROL;
using HY_AjaxAgent_Interface.DATA;
using HY_AjaxAgent.CONTROL;
using HY_AjaxAgent.LOGIC;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

// Creator: HYCHOI_INZI
// Company: INZISOFT
// StartDay: 2020-04-17
// Project: 행정안전부 차세대 주민등록정보시스템 구축
// Description: NonActiveX를 위한 HTTP Server Template.


// DevNote: HttpListener, Windows Message 사용.
//          1. 시작시, HttpListener를 통해 Thread Task를 통해 Listen.
//          2. Web에서 HTTP통신 전달 받음( HTTP Request Listen )
//          3. 전달받자마자 다시 Listen하는 Task 실행 (지속적으로 다른 요청 처리하기 위함)
//          4. 전달 받은 Request에서 Interface 분석하여 그에 해당하는 작업 실행 (주로 EXE 실행 및 EXE로 데이터 전달이 될 것)
//          5. EXE 실행시킬 때 handle값을 키값으로 하는 'WEB_EXE_TranseToken' Dictionary에 추가 value는 TokenData객체(웹에서 받은 데이터(명령 인터페이스), 웹으로 return 하기위한 Response객체)
//          6. 이 Dictionary는 EXE Process에서 결과값 받았을 때 Windows Message -> WndProc(윈도우프로시져 함수) 여기서 처리하여 웹으로 Response하기 위함
//          7. EXE는 실행되면서 실행인자로 서버의 핸들값을 받고 실행 완료 후에 실행완료 메세지를 서버로 전달
//          8. 서버는 WndProc를 통해 EXE로부터 실행완료 데이터를 받고 해당 데이터에 있는 핸들값과 'WEB_EXE_TranseToken'딕셔너리에 있는 핸들값을 대조하여 맞는 TokenData를 찾음
//          9. Token데이터에 있는 Request를 EXE로 다시 전달.
//          10. EXE로부터 수행 결과값을 받아서 웹으로 리턴.


// History: 이후 개발자 남겨주세요.
//          2020-05-24 : jsonp 처리 추가
//          2020-09-25 : jsonp 서버 실행 확인 추가 (server_check)

namespace HY_AjaxAgent
{
    delegate void Receive_WEB(HttpListenerRequest req, HttpListenerResponse res, HY_AjaxAgent server);
    public delegate void Send_WEB(AJAX_Response sendData, HY_AjaxAgent.TokenData token);
    delegate void Receive_EXE(WinMessage_ClientToServer receiveData, HY_AjaxAgent.TokenData token);
    delegate void Send_EXE(WinMessage_ServerToClient sendData);

    public partial class HY_AjaxAgent : Form
    {
        private int serverPort;
        // HTTP Server .NET HttpListener 사용
        private static HttpListener listener;
        private Thread listenThread;

        Receive_WEB receiveFromWEB = new Receive_WEB(LOGIC.WEB.Receive);
        public static Send_WEB sendToWEB = new Send_WEB(LOGIC.WEB.Send);
        Receive_EXE receiveFromEXE = new Receive_EXE(LOGIC.EXE.Receive);
        Send_EXE SendToEXE = new Send_EXE(LOGIC.EXE.Send);
        
        public class TokenData
        {
            public AJAX_Request ajax_request;
            public HttpListenerResponse response;
            //jsonp 처리 hychoi
            public JsonPCheck jsonpCheck;

            public TokenData(AJAX_Request req, HttpListenerResponse res, JsonPCheck jsonpCheck)
            {
                this.ajax_request = req;
                this.response = res;
                this.jsonpCheck = jsonpCheck;
            }

            //jsonp 처리 hychoi
            public class JsonPCheck
            {
                public bool isJsonp;
                public string callback;

                public JsonPCheck(bool isJsonp = false, string callback = null)
                {
                    this.isJsonp = isJsonp;
                    this.callback = callback;
                }
            }
        }

        // Key : Handle 값. 
        // Value : Web과 EXE를 연결하는 토큰. -> 웹으로 부터 받은 명령인터페이스와 웹으로 리턴 할 수 있는 Response 객체를 가지고 있다.
        private static Dictionary<EXEKIND, TokenData> WEB_EXE_TranseTokens = new Dictionary<EXEKIND, TokenData>();

        public enum MSGVIEW
        {
            WEB,
            Process
        }

        public HY_AjaxAgent()
        {
            InitializeComponent();

            // 최소화 및 Windows notify설정.
            setHideMode(true);

            // Log Level 설정.
            string value = Config.GetConfigIni("LOG", "Level");
            Log.Instance.SetLogLevel(value);

            // Port 값 지정.
            value = Config.GetConfigIni("SETTINGS", "port");

            if (value != "")
            {
                serverPort = Int32.Parse(value);
            }
            else
            {
                Log.Instance.Error("지정된 포트가 없습니다. 프로그램을 종료 합니다.");
                Close();
            }
        }

        private void HY_AjaxAgent_Load(object sender, EventArgs e)
        {
            Log.Instance.Info("############################################################################");
            Log.Instance.Info("######################  HY_AjaxAgent 프로그램 실행  #####################");
            Log.Instance.Info("############################################################################");
            Log.Instance.DeleteOldLogFile(7);

            Start_Server();
        }
        
        private void HY_AjaxAgent_FormClosing(object sender, FormClosingEventArgs e)
        {
            Log.Instance.Info("close btn click");

            e.Cancel = true;
            setHideMode(true);
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            Start_Server();
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            Exit_Server();
        }

        private void viewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            setHideMode(false);
        }

        private void serverStartToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Start_Server();
        }

        private void serverStopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Stop_Server();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Exit_Server();
        }

        private void btnWeb_Click(object sender, EventArgs e)
        {
            string testHtml = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TESTWEB", "WebServerTest.htm");
            MessageBox.Show("파일 위치 : " + testHtml);
            if (File.Exists(testHtml))
                Process.Start(testHtml);
            else
                MessageBox.Show("파일이 존재하지 않습니다.");
        }

        private void btnScan_Click(object sender, EventArgs e)
        {
            Start_Process(EXEKIND.TestScan, "{temp}", null, this.Handle);
        }

        private void setHideMode(bool hide)
        {
            if (hide)
            {
                this.WindowState = FormWindowState.Minimized;
                this.ShowInTaskbar = false;
                this.Visible = false;
                this.notifyIcon.Visible = true;
            }
            else
            {
                this.WindowState = FormWindowState.Normal;
                this.ShowInTaskbar = true;
                this.Visible = true;
                this.notifyIcon.Visible = false;
            }
        }

        public static void AddText(MSGVIEW kind, string msg)
        {
            RichTextBox msgView;
            
            if (kind == MSGVIEW.WEB)
                msgView = webToServerMessage;
            else
                msgView = serverToProcessMessage;

            msg += "\n";
            ClearText(msgView);

            msgView.Invoke((MethodInvoker)delegate { msgView.AppendText(msg); msgView.ScrollToCaret(); });
        }

        public static void ClearText(RichTextBox msgView)
        {
            msgView.Invoke((MethodInvoker)delegate {
                if (msgView.Lines.Length > 1000)
                {
                    msgView.Clear();
                }
            });
        }

        /// <summary>
        /// Local HTTP Web Serve Start
        /// </summary>
        public async void Start_Server()
        {
            Log.Instance.Info("## HTTP Web Server Start ##");
            AddText(MSGVIEW.WEB, "서버시작 중...");
            try
            {
                if (listener != null && listener.IsListening)
                {
                    Log.Instance.Info("## HTTP Web Serve is already start ##");
                    Log.Instance.Info("## HTTP Web Server ReStart (Stop -> Start) ##");
                    AddText(MSGVIEW.WEB, "이미 서버가 실행중이므로 종료 후 재시작");
                    Stop_Server();
                    AddText(MSGVIEW.WEB, "서버 재시작...");
                }
                listener = new HttpListener();
                listener.Prefixes.Add(string.Format("http://localhost:{0}/", serverPort));
                listener.Prefixes.Add(string.Format("http://127.0.0.1:{0}/", serverPort));
                listener.AuthenticationSchemes = AuthenticationSchemes.Anonymous;

                listener.Start();
                // RequestQueue Length Size Up.
                ListenerControl.SetRequestQueueLength(listener, 5000);
                
                AddText(MSGVIEW.WEB, "서버시작 완료\n");
                AddText(MSGVIEW.WEB, string.Format("서버 : 로컬영역, 포트 : {0}", serverPort));
                AddText(MSGVIEW.WEB, "http://localhost:" + serverPort);
                
                Log.Instance.Info("## HTTP Web Server Start Success ##");

                await handleClientConnection(listener);
            }
            catch (ObjectDisposedException error)
            {
                Log.Instance.Info(">> HTTP Web Server is Stopped..." + error.Message);
            }
            catch (NullReferenceException error)
            {
                Log.Instance.Error(">> HTTP Web Server NullReferenceException... Error MSG : " + error.Message);
            }
            catch (TaskCanceledException error)
            {
                Log.Instance.Error(">> HTTP Web Server TaskCanceledException... Error MSG : " + error.Message);
            }
            catch (Exception error)
            {
                Log.Instance.Error(">> HTTP Web Server Start Faild... Error MSG : " + error.Message);
                AddText(MSGVIEW.WEB, "서버 시작 실패\n");
            }
        }

        /// <summary>
        /// Web Server Stop
        /// </summary>
        public void Stop_Server()
        {
            Log.Instance.Info("## HTTP Web Serve Stop ##");
            AddText(MSGVIEW.WEB, "서버 종료 중...");

            if (listener != null && listener.IsListening)
            {
                if (listenThread != null && listenThread.IsAlive)
                {
                    listenThread.Abort();
                    listenThread = null;
                    Log.Instance.Debug("Abort listenThread");
                }
                listener.Abort();
                listener = null;
                Log.Instance.Debug("Abort Previous HTTPListener");
            }
            else
            {
                Log.Instance.Info("## HTTP Web Serve is none ##");
                AddText(MSGVIEW.WEB, "서버가 실행되어 있지 않습니다.");
            }

            AddText(MSGVIEW.WEB, "서버종료 성공\n");
            Log.Instance.Info("## Local HTTP Web Server Stop Success ##");
        }

        /// <summary>
        /// Web Server Exit
        /// </summary>
        private void Exit_Server()
        {
            Stop_Server();
            Log.Instance.Info("############################################################################");
            Log.Instance.Info("######################  Local HTTP Web Server 프로그램 종료  #####################");
            Log.Instance.Info("############################################################################");
            Process.GetCurrentProcess().Kill();
            Application.Exit();
        }

        public async Task handleClientConnection(HttpListener listener_)
        {
            var context = await listener_.GetContextAsync();
            var ret = handleClientConnection(listener_);

            HttpListenerRequest request = context.Request;
            HttpListenerResponse response = context.Response;
            
            response.StatusCode = 200;
            response.KeepAlive = false;
            response.StatusDescription = "OK";
            response.ContentType = "Application/x-www-form-urlencoded";
            response.Headers.Add("Access-Control-Allow-Origin", "*");
            response.Headers.Add("Access-Control-Allow-Methods", "POST, GET");

            receiveFromWEB(request, response, this);

            //string responseString = "{return: \"success\"}";
            //AddText(MSGVIEW.WEB, "보내는 데이터 : " + responseString);
            //byte[] buffer = System.Text.Encoding.UTF8.GetBytes(responseString);
            //context.Response.ContentLength64 = buffer.Length;

            //using (System.IO.Stream output = response.OutputStream)
            //    output.Write(buffer, 0, buffer.Length);

            await ret;
        }

        /// <summary>
        /// Server로부터 Process를 실행시킨다.
        /// </summary>
        /// <param name="exeKind">EXE 종류</param>
        /// <param name="param">Start COMMAND로 받은 PARAM 값(String)</param>
        /// <param name="token">서버에서 가지고 있을 TokenData</param>
        /// <param name="serverHandle">서버핸들값</param>
        public static string Start_Process(EXEKIND exeKind, string param, TokenData token, IntPtr serverHandle)
        {
            AddText(MSGVIEW.Process, string.Format("EXE 실행... 종류 : {0}", exeKind.ToString()));
            AddText(MSGVIEW.Process, string.Format("서버핸들 값 : {0}", serverHandle.ToString()));
            Log.Instance.Info("## Process Start ##");
            Log.Instance.Info(string.Format("EXE : {0}, SERVER_HANDLE : {1}", exeKind.ToString(), serverHandle));

            // Token이 이미 있을 때 재전송 할것인가?
            //if (WEB_EXE_TranseTokens.ContainsKey(exeKind))
            //{
            //    Log.Instance.Error(string.Format("## {0} EXE가 이미 실행되어 있음. ##", exeKind.ToString()));
            //    WEB_EXE_TranseTokens[exeKind] = token;
            //}

            string retMsg = string.Empty;

            //Process를 실행시킨 후 핸들값을 받아온다.
            string executedProcessHandle = ProcessControl.executeEXE(exeKind, param, serverHandle, out retMsg);

            if (executedProcessHandle != null)
            {
                if (WEB_EXE_TranseTokens.ContainsKey(exeKind))
                {
                    Log.Instance.Warn(string.Format("## {0} Token이 이미 존재. 새로 받은 token으로 교체 ##", exeKind.ToString()));
                    WEB_EXE_TranseTokens[exeKind] = token;
                }
                else
                    WEB_EXE_TranseTokens.Add(exeKind, token);
                
                AddText(MSGVIEW.Process, string.Format("실행 EXE 종류 : {0}, 실행 EXE 핸들 값 : {1}", exeKind.ToString(), executedProcessHandle));
                Log.Instance.Info("## Process Start Success ##");
            }
            else
            {
                AddText(MSGVIEW.Process, string.Format("{0}", retMsg));
                Log.Instance.Error(">>" + retMsg + "<<");
                Log.Instance.Error("## Process Start Faild ##");
            }

            return retMsg;
        }

        /// <summary>
        /// Server로부터 Process를 종료시킨다.
        /// </summary>
        /// <param name="exeKind">EXE 종류</param>
        /// <param name="token">서버에서 가지고 있을 TokenData</param>
        public static string Stop_Process(EXEKIND exeKind, TokenData token)
        {
            AddText(MSGVIEW.Process, string.Format("EXE 종료... 종류 : {0}", exeKind.ToString()));
            Log.Instance.Info("## Process Stop ##..." + exeKind.ToString());

            string retMsg;
            //Process를 실행시킨 후 핸들값을 받아온다.
            bool bRet = ProcessControl.closeEXE(exeKind, out retMsg);

            if (bRet)
            {
                if (WEB_EXE_TranseTokens.ContainsKey(exeKind))
                {
                    Log.Instance.Warn(string.Format("## {0} Token삭제 ##", exeKind.ToString()));
                    WEB_EXE_TranseTokens.Remove(exeKind);
                }

                Log.Instance.Info("## Process Stop Success ##");
                return null;
            }
            else
            {
                AddText(MSGVIEW.Process, string.Format("{0}", retMsg));
                Log.Instance.Error(">>" + retMsg + "<<");
                Log.Instance.Error("## Process Stop Faild ##");
                return retMsg;
            }
        }

        protected override void WndProc(ref Message m)
        {
            try
            {
                switch (m.Msg)
                {
                    case Win32API.WM_COPYDATA:
                        Win32API.COPYDATASTRUCT cds = (Win32API.COPYDATASTRUCT)m.GetLParam(typeof(Win32API.COPYDATASTRUCT));
                        AddText(MSGVIEW.Process, "EXE로 부터 받은 데이터 : " + cds.lpData);
                        //Do Work...

                        string receiveString = cds.lpData;
                        WinMessage_ClientToServer receiveData;
                        Json.ToObject(receiveString, out receiveData);

                        //Todo. Token 없을때 처리

                        //WEB_EXE_TranseToken
                        HY_AjaxAgent.TokenData token = WEB_EXE_TranseTokens[receiveData.PROCESS_KIND];

                        receiveFromEXE(receiveData, token);

                        break;
                }
                base.WndProc(ref m);
            }
            catch (Exception ex)
            {
                Log.Instance.Error(string.Format("WndProc Error! Msg : {0}", ex.Message));
            }
        }
    }
}
