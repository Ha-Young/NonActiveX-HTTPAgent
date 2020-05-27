using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestScan.Params
{
    class ReceiveParam
    {
        [JsonProperty("SystemDate")]
        public string SystemDate { get; set; }

        [JsonProperty("InputDate")]
        public string InputDate { get; set; }

        [JsonProperty("User")]
        public string User { get; set; }

        [JsonProperty("Jumin")]
        public string Jumin { get; set; }
    }
}
