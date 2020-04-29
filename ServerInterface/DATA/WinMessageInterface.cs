using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace HYWebServer_Interface.DATA
{
    public class WinMessage_ServerToClient
    {
        [JsonProperty("SERVER_HANDLE")]
        public string SERVER_HANDLE { get; set; }
        [JsonProperty("COMMAND")]
        public string COMMAND { get; set; }
        [JsonProperty("PARAM")]
        public string PARAM { get; set; }
    }

    public class WinMessage_ClientToServer
    {
        [JsonProperty("PROCESS_KIND")]
        public EXEKIND PROCESS_KIND { get; set; }
        [JsonProperty("RESULT_COMMAND")]
        public string RESULT_COMMAND { get; set; }
        [JsonProperty("PARAM")]
        public object PARAM { get; set; }

        public WinMessage_ClientToServer(EXEKIND exeKind, string resultCommand, object param)
        {
            this.PROCESS_KIND = exeKind;
            this.RESULT_COMMAND = resultCommand;
            this.PARAM = param;
        }
    }
}
