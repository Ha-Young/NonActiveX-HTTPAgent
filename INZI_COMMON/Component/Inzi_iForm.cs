using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using INZI_COMMON.Controls;
using HY_InziAgent_Interface.DATA;
using System.IO;
using AxINZIIMGVIEWLib;
using AxINZISCANTWAINLib;

namespace INZI_COMMON.Component
{
    /// <summary>
    /// INZI iForm 공통모듈 (행안부 전용)
    /// </summary>
    public partial class Inzi_iForm : UserControl
    {
        public ScanTwain_ ScanTwain;
        public ImgViewer_ ImageView;

        public Inzi_iForm()
        {
            InitializeComponent();
            ScanTwain = new ScanTwain_(Inzi_ScanTwain);
            ImageView = new ImgViewer_(Inzi_ImgViewer);
        }

        /// <summary>
        /// Inzi iForm중에서 초기화 세팅이 필요한 모듈에 초기화 작업을 수행한다.
        /// </summary>
        /// <param name="handle">폼 핸들</param>
        public void Init(IntPtr handle)
        {
            Inzi_ScanTwain.Init((int)handle);
        }

        public class ImgViewer_
        {
            private AxInziImgView inzi_ImgViewer;

            // ############### 이벤트  #################
            public delegate void MouseSelectAfter(ImgInfo cropImg);
            public MouseSelectAfter MouseSelectAfterEvt;
            // #############################################

            public ImgViewer_(AxInziImgView inzi_ImgViewer)
            {
                this.inzi_ImgViewer = inzi_ImgViewer;
            }

            public void ShowImage(string fileName)
            {
                if(File.Exists(fileName))
                {
                    inzi_ImgViewer.ImageFileName(fileName);
                }
            }

            public void CropModeStart()
            {
                if (!string.IsNullOrEmpty(inzi_ImgViewer.IsImageView()))
                {
                    inzi_ImgViewer.MouseSelection = true;
                    Log.Instance.SetLogLevel("mouseSelection : true");
                }
                else
                {
                    Log.Instance.SetLogLevel("메인 뷰어에 이미지 없음.");
                    MessageBox.Show("이미지 스캔을 먼저 해주세요.");
                    return;
                }
            }
        }

        public class ScanTwain_
        {
            // ############### 스캔 세팅 값 ################
            public ImageFormat basicFormat_ = ImageFormat.BMP;
            public int resolution_ = 500;
            public ColorType colorType_ = ColorType.Color;
            // #############################################

            // ############### 이벤트  #################
            public delegate void OneScannedAfter(ImgInfo scannedImg);
            public OneScannedAfter OneScannedAfterEvt;

            public delegate void ScanEndedAfter(EventArgs e);
            public ScanEndedAfter ScanEndedAfterEvt;
            // #############################################


            private AxInziScanTwain inzi_ScanTwain;

            public ScanTwain_(AxInziScanTwain inzi_ScanTwain)
            {
                this.inzi_ScanTwain = inzi_ScanTwain;
            }

            // #########################################

            /// <summary>
            /// ScanTwain을 설정합니다.
            /// </summary>
            /// <param name="basicFormat">스캔 될 포맷</param>
            /// <param name="Resolution">해상도</param>
            /// <param name="colorType">컬러타입(흑백,그레이,칼라)</param>
            public void ScanSetting(ImageFormat basicFormat = ImageFormat.BMP, int resolution = 500, ColorType colorType = ColorType.Color)
            {
                basicFormat_ = basicFormat;
                resolution_ = resolution;
                colorType_ = colorType;

                int colorTypeValue;

                switch (colorType_)
                {
                    case ColorType.Color:
                        colorTypeValue = 4;
                        break;
                    case ColorType.Gray:
                        colorTypeValue = 2;
                        break;
                    case ColorType.BW:
                        colorTypeValue = 0;
                        break;
                    default:
                        colorTypeValue = 4;
                        break;
                }

                inzi_ScanTwain.SetSaveFormat(basicFormat_.ToString().ToLower());
                inzi_ScanTwain.SetImagePath(PathInfo.Work);
                inzi_ScanTwain.SetShowProgress(true);
                inzi_ScanTwain.SetFilenameScheme(DateTime.Now.ToString("MMddHHmmss"));
                inzi_ScanTwain.SetShowUI(false);

                switch (Common.CurrentEXE)
                {
                    case EXEKIND.TestScan:
                        inzi_ScanTwain.ChannelMake("testScan", colorTypeValue, resolution_, 1, 2002, 2002, 2002, 2002, 2002, 0, "");
                        inzi_ScanTwain.ChannelSetCur("testScan");
                        break;

                    case EXEKIND.SCAN:
                        //화상스캔
                        break;

                    case EXEKIND.Signiture:
                        //서명
                        break;

                    case EXEKIND.LiveFinger:
                        //십지지문
                        break;

                    case EXEKIND.IDCheck:
                        //신분증진위확인
                        break;
                }
            }

            /// <summary>
            /// 스캔 시작
            /// </summary>
            /// <param name="index"></param>
            public void Acquire(int index)
            {
                inzi_ScanTwain.Acquire(index);
            }

            /// <summary>
            /// 스캐너 드라이버 선택
            /// </summary>
            public void SelectScanner()
            {
                inzi_ScanTwain.SelectSource();
            }
        }

        private void Inzi_ScanTwain_OneScanned(object sender, AxINZISCANTWAINLib._DInziScanTwainEvents_OneScannedEvent e)
        {
            try
            {
                Log.Instance.SetLogLevel("Inzi_ScanTwain_OneScanned()");
                Inzi_ScanTwain.SetFilenameScheme(DateTime.Now.ToString("MMddHHmmss"));

                ImgInfo imginfo = new ImgInfo(e.sFilename, ScanTwain.colorType_, ScanTwain.resolution_, ScanTwain.basicFormat_);
                if (ScanTwain.OneScannedAfterEvt != null)
                    ScanTwain.OneScannedAfterEvt(imginfo);
            }
            catch (Exception Err)
            {
                Log.Instance.SetLogLevel("InziScanTwain_OneScanned() Exception Error - Message: " + Err.Message + ", StackTrace: " + Err.StackTrace);
            }
        }

        private void Inzi_ScanTwain_ScanEnded(object sender, EventArgs e)
        {
            Log.Instance.SetLogLevel("InziScanTwain_ScanEnded()...");
            if(ScanTwain.ScanEndedAfterEvt != null)
                ScanTwain.ScanEndedAfterEvt(e);
        }

        private void Inzi_ImgViewer_MouseSelectEnd(object sender, AxINZIIMGVIEWLib._DInziImgViewEvents_MouseSelectEndEvent e)
        {
            string cropArea = string.Format("{0}, {1}, {2}, {3}", e.left, e.top, e.right, e.bottom);
            Log.Instance.SetLogLevel("Inzi_ImgViewer_MouseSelectEnd()... area : " + cropArea);
            string imgPath = Inzi_ImgViewer.IsImageView();

            string cropFilePath = Path.Combine(Path.GetDirectoryName(imgPath), "Crop_" + Path.GetFileName(imgPath));

            Inzi_iFile.ClipImage(imgPath, e.left, e.top, e.right, e.bottom, cropFilePath);

            Inzi_ImgViewer.MouseSelection = false;

            ImgInfo cropImg = new ImgInfo(cropFilePath, ScanTwain.colorType_, ScanTwain.resolution_, ScanTwain.basicFormat_);

            if (ImageView.MouseSelectAfterEvt != null)
                ImageView.MouseSelectAfterEvt(cropImg);
        }
    }
}
