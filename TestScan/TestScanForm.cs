using HY_AjaxAgent_Interface.CONTROL;
using HY_AjaxAgent_Interface.DATA;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TestScan.Controls;
using TestScan.Params;

namespace TestScan
{
    public partial class TestScanForm : Form
    {
        ImgInfo img;
        public static IntPtr serverHandle;

        public TestScanForm()
        {
            InitializeComponent();
        }

        private void TestScanForm_Load(object sender, EventArgs e)
        {

            Common.initProgram(EXEKIND.TestScan);
            
            //args check
            startArgsCheck();

            //실행 완료신호를 보낸다.
            sendStartSignal();
        }

        /// <summary>
        /// 서버에 성공적으로 실행되었다는 신호를 보낸다.
        /// </summary>
        private void sendStartSignal()
        {
            string startSuccessCommand = "startSuccess";
            WinMessage_ClientToServer successResponse = new WinMessage_ClientToServer(EXEKIND.TestScan, startSuccessCommand, null);
            AgentInterface.SendToServer(successResponse, serverHandle);


            Log.Instance.SetLogLevel(string.Format("# Send To Server\n\tCommand : {0}\n\tParam : ", startSuccessCommand, ""));
        }

        /// <summary>
        /// 시작인수 체크 및 서버핸들, PARAM값 설정
        /// </summary>
        private void startArgsCheck()
        {
            string[] args = Environment.GetCommandLineArgs();
            string argsStr = "commandLine : ";
            for (int i = 0; i < args.Length; i++)
                argsStr += i + " : " + args[i] + "\t";

            Log.Instance.Info(argsStr);
            MessageBox.Show(argsStr);

            int serverHandleTmp;
            if (args.Length != 3 || !int.TryParse(args[1], out serverHandleTmp))
            {
                MessageBox.Show("서버 핸들값을 받지 못하였거나 제대로 된 서버핸들값이 아닙니다.");
                Application.Exit();
            }
            else
            {
                serverHandle = new IntPtr(serverHandleTmp);
            }

            string serverHandleLog = "# receive server Handle : " + serverHandle.ToString();
            MessageBox.Show(serverHandleLog);
            Log.Instance.SetLogLevel(serverHandleLog);

            string param = args[2];
            Log.Instance.SetLogLevel("# Param : " + param);

            ReceiveParam receiveParam;
            bool paramSuccess = Json.ToObject(param, out receiveParam);
            Log.Instance.SetLogLevel("# Param String to Obejct : " + paramSuccess);

            if (receiveParam != null)
                setViewReceiveParam(receiveParam);
        }

        private void setViewReceiveParam(ReceiveParam receiveParam)
        {
            label_systemDate.Text = label_systemDate.Text + receiveParam.SystemDate;
            label_inputDate.Text = label_inputDate.Text + receiveParam.InputDate;
            label_user.Text = label_user.Text + receiveParam.User;
            label_jumin.Text = label_jumin.Text + receiveParam.Jumin;
        }

        private void btn_Save_Click(object sender, EventArgs e)
        {
            if (img == null)
            {
                MessageBox.Show(this, "이미지를 가져와야 합니다.");
                return;
            }

            string filePath = img.FilePath;
            if (!File.Exists(filePath))
            {
                MessageBox.Show(this, "이미지가 존재하지 않습니다.");
                return;
            }

            string imgbinary_base64 = Encode.ImgBinaryToBase64String(filePath);

            SendParam sendParam = new SendParam(img.ScanDate, filePath, imgbinary_base64);

            WinMessage_ClientToServer workCompleteResponse = new WinMessage_ClientToServer(EXEKIND.TestScan, "workComplete", sendParam);

            AgentInterface.SendToServer(workCompleteResponse, serverHandle);
        }
        

        private void btn_Import_Click(object sender, EventArgs e)
        {
            Log.Instance.SetLogLevel("Btn_FileOpen_Click()...");
            
            OpenFileDialog OF = new OpenFileDialog();

            OF.Filter = "Image Files(*.tif; *.tiff; *.jpg; *.jpeg; *.gif; *.bmp; *.png)|*.tif; *.tiff; *.jpg; *.jpeg; *.gif; *.bmp; *.png";

            OF.Title = "로컬 파일 선택";
            //OF.Multiselect = true;
            //OF.InitialDirectory = PathInfo.Work;
            OF.RestoreDirectory = true;
            OF.ShowHelp = false;

            if (OF.ShowDialog() == DialogResult.Cancel)
            {
                Log.Instance.SetLogLevel("파일가져오기 - 취소");
                return;
            }

            string importImg = Path.Combine(PathInfo.Work, "import_" + Path.GetFileName(OF.FileName));            
            File.Copy(OF.FileName, importImg);
            ImgInfo img = new ImgInfo(importImg, ColorType.etc, -1, ImageFormat.UnKnown);

            label_filePath.Text = img.FilePath;
        }

        private void TestScanForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            sendCloseSignal();
        }

        /// <summary>
        /// 서버에 성공적으로 종료되었다는 신호를 보낸다.
        /// </summary>
        private void sendCloseSignal()
        {
            // 종료 신호를 보낸다.
            string closeProcessCommand = "closeProcess";
            WinMessage_ClientToServer closeResponse = new WinMessage_ClientToServer(EXEKIND.TestScan, closeProcessCommand, null);
            AgentInterface.SendToServer(closeResponse, serverHandle);
            Log.Instance.SetLogLevel(string.Format("# Send To Server\n\tCommand : {0}\n\tParam : ", closeProcessCommand, ""));
        }
    }
}
