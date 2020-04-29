using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace HYWebServer_Interface.CONTROL
{
    public class Win32API
    {
        public const Int32 WM_COPYDATA = 0x004A;

        public struct COPYDATASTRUCT
        {
            public IntPtr dwData;
            public int cbData;
            [MarshalAs(UnmanagedType.LPStr)]
            public string lpData;
        }

        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        public extern static int FindWindow(string className, string windowName);

        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        public extern static bool PostMessage(IntPtr hWnd, uint msg, uint wParam, IntPtr lParam);

        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        public extern static bool PostMessage(IntPtr hWnd, uint msg, uint wParam, ref COPYDATASTRUCT lParam);

        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        public extern static bool SendMessage(IntPtr hWnd, uint msg, uint wParam, ref COPYDATASTRUCT lParam);
    }
}
