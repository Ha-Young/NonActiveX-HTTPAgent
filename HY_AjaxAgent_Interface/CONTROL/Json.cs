using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HY_AjaxAgent_Interface.CONTROL
{
    public static class Json
    {
        /// <summary>
        /// Json Data Paser
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="objectData">out</param>
        /// <returns></returns>
        public static bool ToObject<T>(string msg, out T objectData)
        {
            objectData = default(T);
            try
            {
                objectData = JsonConvert.DeserializeObject<T>(msg);
            }
            catch
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Json Class 를 Json 형식의 String으로 변환.
        /// </summary>
        /// <param name="value"></param>
        /// <returns>Json String</returns>
        public static string ToString(object value)
        {
            string jsonString = JsonConvert.SerializeObject(value, Formatting.None,
                new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore,
                    DefaultValueHandling = DefaultValueHandling.Ignore
                });
            return jsonString;
        }

        /// <summary>
        /// JsonString을 보기 좋게 정리
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public static string JsonLogPretty(string message)
        {
            var res = "";
            try
            {
                Newtonsoft.Json.Linq.JToken parsedJson = Newtonsoft.Json.Linq.JToken.Parse(message);
                res = parsedJson.ToString(Formatting.Indented);
            }
            catch (Exception)
            {
                return res;
            }

            return res;
        }
    }
}
