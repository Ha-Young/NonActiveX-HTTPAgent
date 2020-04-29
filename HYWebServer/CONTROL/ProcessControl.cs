using HYWebServer_Interface.DATA;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mois_LocalWebServer_TEST
{
    public class ProcessControl
    {
        /// <summary>
        /// 프로세스(EXE)를 실행시킨다.
        /// </summary>
        /// <param name="exeKind">EXE 종류</param>
        /// <param name="arguments">실행인자</param>
        /// <param name="serverHandle">실행인자 첫번째로 넘어가게 될 서버 핸들</param>
        /// <param name="errorMsg"></param>
        /// <returns></returns>
        public static string executeEXE(EXEKIND exeKind, string arguments, IntPtr serverHandle, out string errorMsg)
        {
            string exeFilepath = getEXEFilePath(exeKind);
            errorMsg = string.Empty;

            if (!File.Exists(exeFilepath))
            {
                HYWebServer.AddText(HYWebServer.MSGVIEW.Process, string.Format("exe file dosen't exist... {0}", exeKind.ToString()));
                HYWebServer.AddText(HYWebServer.MSGVIEW.Process, string.Format("exe file path : {0}", exeFilepath));
                Log.Instance.Error(string.Format("exe file dosen't exist... {0}", exeKind.ToString()));
                Log.Instance.Error(string.Format("exe file path : {0}", exeFilepath));
                errorMsg = string.Format("exe file dosen't exist... {0}", exeKind.ToString());
                return null;
            }

            if (processRunningCheck(exeKind))
            {
                HYWebServer.AddText(HYWebServer.MSGVIEW.Process, string.Format("exe is already started... {0}", exeKind.ToString()));
                HYWebServer.AddText(HYWebServer.MSGVIEW.Process, string.Format("exe file path : {0}", exeFilepath));
                Log.Instance.Error(string.Format("exe is already started... {0}", exeKind.ToString()));
                Log.Instance.Error(string.Format("exe file path : {0}", exeFilepath));
                errorMsg = string.Format("exe is already started... {0}", exeKind.ToString());
                return null;
            }

            arguments = serverHandle.ToString() + " " + arguments;
            Process process = Process.Start(exeFilepath, arguments);

            return process.Handle.ToString();
        }

        public static bool closeEXE(EXEKIND exeKind, out string errorMsg)
        {
            string exeFilepath = getEXEFilePath(exeKind);
            errorMsg = string.Empty;

            if (!File.Exists(exeFilepath))
            {
                HYWebServer.AddText(HYWebServer.MSGVIEW.Process, string.Format("exe file dosen't exist... {0}", exeKind.ToString()));
                HYWebServer.AddText(HYWebServer.MSGVIEW.Process, string.Format("exe file path : {0}", exeFilepath));
                Log.Instance.Error(string.Format("exe file dosen't exist... {0}", exeKind.ToString()));
                Log.Instance.Error(string.Format("exe file path : {0}", exeFilepath));
                errorMsg = string.Format("exe file dosen't exist... {0}", exeKind.ToString());
                return false;
            }

            if (!processRunningCheck(exeKind))
            {
                HYWebServer.AddText(HYWebServer.MSGVIEW.Process, string.Format("exe is not running... {0}", exeKind.ToString()));
                HYWebServer.AddText(HYWebServer.MSGVIEW.Process, string.Format("exe file path : {0}", exeFilepath));
                Log.Instance.Error(string.Format("exe is not running...... {0}", exeKind.ToString()));
                Log.Instance.Error(string.Format("exe file path : {0}", exeFilepath));
                errorMsg = string.Format("exe is not running...... {0}", exeKind.ToString());
                return false;
            }

            try
            {
                Process[] runningProcess = Process.GetProcesses();
                foreach (Process process in runningProcess)
                {
                    try
                    {
                        if (string.Compare(process.MainModule.FileName, exeFilepath) == 0)
                            process.Kill();
                    }
                    catch
                    {

                    }
                }
            }
            catch(Exception error)
            {
                errorMsg = error.Message;
                return false;
            }

            return true;
        }

        public static string getEXEFilePath(EXEKIND exeKind)
        {
            string filePath = string.Empty;

            switch (exeKind)
            {
                case EXEKIND.TestScan:
                    filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TestScan.exe");
                    break;
            }

            return filePath;
        }

        public static bool processRunningCheck(EXEKIND exeKind)
        {
            Process[] processList = Process.GetProcessesByName(exeKind.ToString());

            if (processList.Length > 0)
                return true;
            else
                return false;
        }
    }
}
