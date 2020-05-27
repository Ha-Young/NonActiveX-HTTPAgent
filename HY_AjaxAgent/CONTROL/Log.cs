using log4net;
using log4net.Appender;
using log4net.Core;
using log4net.Layout;
using log4net.Repository.Hierarchy;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HY_AjaxAgent.CONTROL
{
    public class Log
    {
        private readonly String _logDirectoryPath = AppDomain.CurrentDomain.BaseDirectory + @"\LOG\" + AppDomain.CurrentDomain.FriendlyName.Split('.')[0];

        private static Log _instance = null;
        private static object _sync = new Object();

        public static Log Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_sync)
                    {
                        _instance = new Log();
                    }
                }
                return _instance;
            }
        }

        private static ILog _logger = null;

        public bool DisplayConsole { get; set; }

        private Log()
        {
            string programName = System.AppDomain.CurrentDomain.FriendlyName;
            _logger = LogManager.GetLogger(programName);

            Hierarchy hierarchy = (Hierarchy)LogManager.GetRepository();
            hierarchy.Configured = true;

            RollingFileAppender rollingAppender = new RollingFileAppender();
            rollingAppender.File = _logDirectoryPath + @"\" + programName;
            rollingAppender.AppendToFile = true;
            rollingAppender.RollingStyle = RollingFileAppender.RollingMode.Date;
            rollingAppender.DatePattern = "_yyyyMMdd-HH\".log\"";
            rollingAppender.StaticLogFileName = false;
            PatternLayout layout = new PatternLayout("%date [%thread] %-5level - %message%newline");
            rollingAppender.Layout = layout;
            (hierarchy.GetLogger(programName) as Logger).AddAppender(rollingAppender);
            rollingAppender.ActivateOptions();
            hierarchy.Root.Level = Level.All;
        }

        public void LogWrite(String logMessage, Level level, bool isInnerCall = false)
        {
            string sFN_NM = "";
            StackFrame sf = new StackFrame(1, true);
            if (isInnerCall)
            {
                sf = new StackFrame(2, true);
            }
            sFN_NM = sf.GetMethod().ToString().PadRight(30);
            int returnType = sFN_NM.IndexOf(" ");
            sFN_NM = sFN_NM.Substring(returnType + 1);
            int startIndex = sFN_NM.IndexOf('(');
            sFN_NM = sFN_NM.Substring(0, startIndex);

            string fileName = sf.GetFileName();
            fileName = Path.GetFileName(fileName);

            string message = logMessage + " >>> " + sFN_NM + "(" + fileName + "[" + sf.GetFileLineNumber() + "])";
            if (level == Level.Debug)
            {
                _logger.Debug(message);
            }
            else if (level == Level.Info)
            {
                _logger.Info(message);
            }
            else if (level == Level.Warn)
            {
                _logger.Warn(message);
            }
            else if (level == Level.Error)
            {
                _logger.Error(message);
            }
            else if (level == Level.Fatal)
            {
                _logger.Fatal(message);
            }
            ConsoleWriteLine("[" + level + "] " + logMessage);
        }

        public void Debug(String logMessage)
        {
            LogWrite(logMessage, Level.Debug, true);
        }

        public void Info(String logMessage)
        {
            LogWrite(logMessage, Level.Info, true);
        }

        public void Warn(String logMessage)
        {
            LogWrite(logMessage, Level.Warn, true);
        }

        public void Error(String logMessage)
        {
            LogWrite(logMessage, Level.Error, true);
        }

        public void Fatal(String logMessage)
        {
            LogWrite(logMessage, Level.Fatal, true);
        }

        public void ConsoleWriteLine(string msg)
        {
            if (DisplayConsole)
            {
                Trace.WriteLine(msg);
            }
        }

        public void ConsoleWriteLine(string format, params object[] args)
        {
            if (DisplayConsole)
            {
                Trace.WriteLine(string.Format(format, args));
            }
        }

        //일정 기간이 지난 로그 파일 삭제
        public void DeleteOldLogFile(int day)
        {
            DirectoryInfo di = new DirectoryInfo(_logDirectoryPath);
            FileInfo[] files = di.GetFiles();
            string lDate = DateTime.Today.AddDays(-1 * day).ToString("yyyy-MM-dd");
             
            foreach (FileInfo file in files)
            {
                if (lDate.CompareTo(file.LastWriteTime.ToString("yyyy-MM-dd")) > 0)
                {
                    File.Delete(di.FullName + @"\" + file.Name);
                }
            }
        }

        // level을 설정하여 선택적으로 로그를 남긴다
        public void SetLogLevel(Level level)
        {
            //Level 순서
            //"ALL", "DEBUG", "INFO", "WARN", "ERROR", "FATAL" and "OFF"
            Hierarchy hierarchy = (Hierarchy)LogManager.GetRepository();
            hierarchy.Root.Level = level;
        }

        public void SetLogLevel(string value)
        {
            if (value.Equals("ALL"))
            {
                Instance.SetLogLevel(Level.All);
            }
            else if (value.Equals("DEBUG"))
            {
                Instance.SetLogLevel(Level.Debug);
            }
            else if (value.Equals("INFO"))
            {
                Instance.SetLogLevel(Level.Info);
            }
            else if (value.Equals("WARN"))
            {
                Instance.SetLogLevel(Level.Warn);
            }
            else if (value.Equals("ERROR"))
            {
                Instance.SetLogLevel(Level.Error);
            }
            else if (value.Equals("FATAL"))
            {
                Instance.SetLogLevel(Level.Fatal);
            }
            else if (value.Equals("OFF"))
            {
                Instance.SetLogLevel(Level.Off);
            }
        }
    }
}
