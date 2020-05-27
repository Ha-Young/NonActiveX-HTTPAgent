using HY_AjaxAgent_Interface.DATA;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace TestScan.Controls
{
    /// <summary>
    /// 이미지 포맷 enum. iFile ImageFormatConstants enum 참고하였음.
    /// </summary>
    public enum ImageFormat
    {
        UnKnown,
        BMP,
        JBG,
        JPG,
        TIF,
        PNG,
        WSQ
    }

    public enum LogLevel
    {
        Real = 1,
        dev = 2
    }

    public enum ColorType
    {
        etc = 0,
        BW = 1,
        Gray = 8,
        Color = 24
    }

    public class ImgInfo
    {
        public string FilePath = string.Empty;              // 파일 경로
        public string FilePath_ORG = string.Empty;          // 원본 파일 경로
        public string FilePath_BW = string.Empty;           // 흑백변환 파일 경로
        public string FileName = string.Empty;              // 파일명
        public ColorType OriginColorType = ColorType.etc;    // 컬러타입_원본
        public ColorType CurrentColorType = ColorType.etc;        // 현재 컬러타입
        public ImageFormat OriginImgFormat = ImageFormat.BMP;   // 원본 포맷
        public ImageFormat CurrentFormat = ImageFormat.BMP;     // 현재 포맷
        public int Resolution = -1;                         // 해상도
        public string ScanDate;                           // 스캔날자

        public ImgInfo() { }    // 기본 생성자

        public ImgInfo(string _FilePath, ColorType _ColorType, int _Resolution, ImageFormat _ImgFormat)
        {
            this.FilePath = _FilePath;
            this.FilePath_ORG = System.IO.Path.GetDirectoryName(_FilePath) + @"\" + System.IO.Path.GetFileNameWithoutExtension(_FilePath) + "_org.bmp";
            this.FilePath_BW = System.IO.Path.GetDirectoryName(_FilePath) + @"\" + System.IO.Path.GetFileNameWithoutExtension(_FilePath) + "_bw.bmp";
            this.FileName = System.IO.Path.GetFileNameWithoutExtension(_FilePath);
            this.OriginColorType = _ColorType;
            this.CurrentColorType = _ColorType;
            this.OriginImgFormat = _ImgFormat;
            this.CurrentFormat = _ImgFormat;
            this.Resolution = _Resolution;
            this.ScanDate = DateTime.Now.ToString("yyyyMMddHHmmss");
        }

        public static bool FileDelete(ImgInfo img)
        {
            try
            {
                Common.fileDelete(img.FilePath);
                Common.fileDelete(img.FilePath_ORG);
                Common.fileDelete(img.FilePath_BW);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }

    public class Common
    {
        public readonly static string Program_NM = AppDomain.CurrentDomain.FriendlyName.Split('.')[0];

        /// <summary>
        /// 현재 EXE
        /// </summary>
        public static EXEKIND CurrentEXE { get; private set; }

        /// <summary>
        /// 프로그램 초기설정. (필수)
        /// </summary>
        /// <param name="exeKind"></param>
        public static void initProgram(EXEKIND exeKind)
        {
            Common.CurrentEXE = exeKind;
            // 작업폴더 생성
            Common.makeNewDirectory(PathInfo.Work);

            // 로그 폴더 생성
            if (Directory.Exists(PathInfo.Log) == false)
                Directory.CreateDirectory(PathInfo.Log);

            Log.Instance.SetLogLevel("========================================================");
            Log.Instance.SetLogLevel("프로그램 실행...");
            Log.Instance.SetLogLevel("Form_Load...");
            Log.Instance.SetLogLevel("GetCommandLineArgs...");

            // Log Level 설정.
            string value = Config.GetConfigIni("LOG", "Level");
            Log.Instance.SetLogLevel(value);
        }

        /// <summary>
        /// 새 디렉토리를 만든다. 존재하면 지우고 새로 만든다.
        /// </summary>
        /// <param name="strPath">생성할 디렉토리경로</param>
        public static bool makeNewDirectory(string strPath)
        {
            try
            {
                if (Directory.Exists(strPath))
                    Directory.Delete(strPath, true);
                Directory.CreateDirectory(strPath);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 새 디렉토리를 만든다. 존재하면 그대로 둔다.
        /// </summary>
        /// <param name="strPath"></param>
        /// <param name="bNewFlag">존재할 때 새로 만들것인가</param>
        /// <returns></returns>
        public static bool makeDirectory(string strPath, bool bNewFlag = false)
        {
            try
            {
                if (!Directory.Exists(strPath))
                    Directory.CreateDirectory(strPath);
                else
                {
                    if (bNewFlag)
                    {
                        Directory.Delete(strPath, true);
                        Directory.CreateDirectory(strPath);
                    }
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 파일을 이동시킨다
        /// </summary>
        /// <param name="sourceFileName"></param>
        /// <param name="destFileName"></param>
        public static void MoveFileNewDir(string sourceFileName, string destFileName)
        {
            string destFolderPath = Path.GetDirectoryName(destFileName);
            if (!Directory.Exists(destFolderPath))
                Directory.CreateDirectory(destFolderPath);

            File.Move(sourceFileName, destFileName);
        }

        /// <summary>
        /// 파일을 복사시킨다
        /// </summary>
        /// <param name="sourceFileName"></param>
        /// <param name="destFileName"></param>
        /// <returns></returns>
        public static bool CopyFileNewDir(string sourceFileName, string destFileName)
        {
            try
            {
                string destFolderPath = Path.GetDirectoryName(destFileName);
                if (!Directory.Exists(destFolderPath))
                    Directory.CreateDirectory(destFolderPath);

                File.Copy(sourceFileName, destFileName, true);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 파일이 존재하면 삭제한다.
        /// </summary>
        /// <param name="fileName"></param>
        public static void fileDelete(string fileName)
        {
            if(File.Exists(fileName))
                File.Delete(fileName);
        }
    }
}
