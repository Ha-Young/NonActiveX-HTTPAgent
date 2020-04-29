using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mois_LocalWebServer_TEST
{
    static class Program
    {
        /// <summary>
        /// 해당 응용 프로그램의 주 진입점입니다.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // System.Diagnostics.Process.GetCurrentProcess().ProcessName
            Process[] procs = System.Diagnostics.Process.GetProcessesByName("HYWebServer");
            int thisID = System.Diagnostics.Process.GetCurrentProcess().Id;

            try
            {
                if(procs.Length > 1)
                {
                    foreach(Process proc in procs)
                    {
                        if (proc.Id != thisID)
                            proc.Kill();
                    }
                }
            }
            catch(Exception err)
            {
                Log.Instance.Error("프로그램 중복방지. 새 프로그램 실행시 문제발생 : " + err.Message);
            }

            Application.Run(new HYWebServer());
        }
    }
}
