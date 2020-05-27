using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HY_AjaxAgent_Interface.CONTROL
{
    public class Encode
    {
        /// <summary>
        /// Image파일을 Base64 String값으로 반환해줍니다.
        /// </summary>
        /// <param name="filePath">파일경로</param>
        /// <returns></returns>
        public static string ImgBinaryToBase64String(string filePath)
        {
            string imgBase64Str = string.Empty;

            try
            {
                byte[] data = System.IO.File.ReadAllBytes(filePath);
                imgBase64Str = Convert.ToBase64String(data);
            }
            catch
            {
                imgBase64Str = string.Empty;
            }
            return imgBase64Str;
        }
    }
}
