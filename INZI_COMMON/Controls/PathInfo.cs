using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace INZI_COMMON.Controls
{
    public static class PathInfo
    {
        public static string RootDir = AppDomain.CurrentDomain.BaseDirectory;
        public static string Bin = Path.Combine(RootDir,"");
        public static string Log = Path.Combine(RootDir, "Log", Common.Program_NM);
        public static string Work = Path.Combine(RootDir, "Work", Common.Program_NM);
        public static string Temp = Path.Combine(RootDir, "Temp", Common.Program_NM);
    }
}
