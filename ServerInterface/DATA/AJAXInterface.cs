using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HYWebServer_Interface.DATA
{
    public class AJAX_Request
    {
        [JsonProperty("EXEKIND")]
        public int EXEKIND { get; set; }
        [JsonProperty("COMMAND")]
        public string COMMAND { get; set; }
        [JsonProperty("PARAM")]
        public object PARAM { get; set; }
    }


    public class AJAX_Response
    {
        [JsonProperty("RESULT")]
        public object RESULT { get; set; }

        public AJAX_Response(object result)
        {
            this.RESULT = result;
        }
    }

    public class Response
    {
        [JsonProperty("EXEKIND")]
        public string EXEKIND { get; set; }
        [JsonProperty("COMMAND")]
        public string COMMAND { get; set; }
        [JsonProperty("RESULT")]
        public string RESULT { get; set; }

        public Response(string exeKind, string command, string result)
        {
            this.EXEKIND = exeKind;
            this.COMMAND = command;
            this.RESULT = result;
        }
    }

    public class Response_Error
    {
        [JsonProperty("EXEKIND")]
        public string EXEKIND { get; set; }
        [JsonProperty("ErrorStatus")]
        public string ErrorStatus { get; set; }
        [JsonProperty("ErrorLocation")]
        public string ErrorLocation { get; set; }
        [JsonProperty("ErrorMsg")]
        public string ErrorMsg { get; set; }

        public Response_Error(string exeKind, string errorStatus, string errorMsg)
        {
            this.EXEKIND = exeKind;
            this.ErrorStatus = errorStatus;
            this.ErrorLocation = "Server";
            this.ErrorMsg = errorMsg;
        }
    }
}
