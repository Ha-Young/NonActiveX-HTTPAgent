using IniParser;
using IniParser.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Mois_LocalWebServer_TEST
{
    public static class Config
    {
        public enum Environment
        {
            DEV, TEST, REAL
        }

        public static Environment RunEnvironment;

        public static int DeleteDay = 7;

        public static string LogLevel = "DEBUG";

        private static string configIniPath = AppDomain.CurrentDomain.BaseDirectory + @"\INI\Config.ini";

        public static string GetConfigIni(string section, string key)
        {
            FileIniDataParser parser = new FileIniDataParser();
            IniData iniData = parser.ReadFile(configIniPath);
            return iniData[section][key];
        }

    }
}
