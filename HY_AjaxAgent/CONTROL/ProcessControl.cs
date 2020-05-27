using HY_AjaxAgent_Interface.DATA;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HY_AjaxAgent.CONTROL
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
                HY_AjaxAgent.AddText(HY_AjaxAgent.MSGVIEW.Process, string.Format("exe file dosen't exist... {0}", exeKind.ToString()));
                HY_AjaxAgent.AddText(HY_AjaxAgent.MSGVIEW.Process, string.Format("exe file path : {0}", exeFilepath));
                Log.Instance.Error(string.Format("exe file dosen't exist... {0}", exeKind.ToString()));
                Log.Instance.Error(string.Format("exe file path : {0}", exeFilepath));
                errorMsg = string.Format("exe file dosen't exist... {0}", exeKind.ToString());
                return null;
            }

            if (processRunningCheck(exeKind))
            {
                HY_AjaxAgent.AddText(HY_AjaxAgent.MSGVIEW.Process, string.Format("exe is already started... {0}", exeKind.ToString()));
                HY_AjaxAgent.AddText(HY_AjaxAgent.MSGVIEW.Process, string.Format("exe file path : {0}", exeFilepath));
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
                HY_AjaxAgent.AddText(HY_AjaxAgent.MSGVIEW.Process, string.Format("exe file dosen't exist... {0}", exeKind.ToString()));
                HY_AjaxAgent.AddText(HY_AjaxAgent.MSGVIEW.Process, string.Format("exe file path : {0}", exeFilepath));
                Log.Instance.Error(string.Format("exe file dosen't exist... {0}", exeKind.ToString()));
                Log.Instance.Error(string.Format("exe file path : {0}", exeFilepath));
                errorMsg = string.Format("exe file dosen't exist... {0}", exeKind.ToString());
                return false;
            }

            if (!processRunningCheck(exeKind))
            {
                HY_AjaxAgent.AddText(HY_AjaxAgent.MSGVIEW.Process, string.Format("exe is not running... {0}", exeKind.ToString()));
                HY_AjaxAgent.AddText(HY_AjaxAgent.MSGVIEW.Process, string.Format("exe file path : {0}", exeFilepath));
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

        public static string getEXEFileName(EXEKIND exeKind)
        {
            int intExeKind = (int)exeKind;
            string fileName = Config.GetConfigIni("EXEKIND", intExeKind.ToString());
            return fileName;
        }

        public static string getEXEFilePath(EXEKIND exeKind)
        {
            string filePath = string.Empty;
            string fileName = getEXEFileName(exeKind);

            try
            {
                filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileName + ".exe");
            }
            catch { }

            return filePath;
        }

        public static bool processRunningCheck(EXEKIND exeKind)
        {
            string fileName = getEXEFileName(exeKind);
            Process[] processList = Process.GetProcessesByName(fileName);

            if (processList.Length > 0)
                return true;
            else
                return false;
        }
    }
}
