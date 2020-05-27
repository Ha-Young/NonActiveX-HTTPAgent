using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestScan.Params
{
    class SendParam
    {
        [JsonProperty("ScanDate")]
        public string ScanDate { get; set; }
        [JsonProperty("FilePath")]
        public string FilePath { get; set; }
        [JsonProperty("ImageBase64")]
        public string ImageBase64 { get; set; }
        [JsonProperty("Return_Val")]
        public string Return_Val { get; set; }
        [JsonProperty("ErrMsg")]
        public string ErrMsg { get; set; }

        public SendParam(string scanDate, string filePath, string imageBinary_base64)
        {
            this.ScanDate = scanDate;
            this.FilePath = filePath;
            this.ImageBase64 = imageBinary_base64;
        }
    }
}
