using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Mois_LocalWebServer_TEST.CONTROL
{
    public class ListenerControl
    {
        internal enum HTTP_SERVER_PROPERTY
        {
            HttpServerAuthenticationProperty,
            HttpServerLoggingProperty,
            HttpServerQosProperty,
            HttpServerTimeoutsProperty,
            HttpServerQueueLengthProperty,
            HttpServerStateProperty,
            HttpServer503VerbosityProperty,
            HttpServerBindingProperty,
            HttpServerExtendedAuthenticationProperty,
            HttpServerListenEndpointProperty,
            HttpServerChannelBindProperty,
            HttpServerProtectionLevelProperty
        }

        [DllImport("httpapi.dll", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        internal static extern uint HttpSetRequestQueueProperty(
            CriticalHandle requestQueueHandle,
            HTTP_SERVER_PROPERTY serverProperty,
            IntPtr pPropertyInfo,
            uint propertyInfoLength,
            uint reserved,
            IntPtr pReserved);

        public unsafe static void SetRequestQueueLength(HttpListener listener, long len)
        {
            var listenerType = typeof(HttpListener);
            var requestQueueHandleProperty = listenerType.GetProperties(System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).
                First(p => p.Name == "RequestQueueHandle");

            var requestQueueHandle = (CriticalHandle)requestQueueHandleProperty.GetValue(listener);
            var result = HttpSetRequestQueueProperty(requestQueueHandle, HTTP_SERVER_PROPERTY.HttpServerQueueLengthProperty, new IntPtr((void*)&len), (uint)Marshal.SizeOf(len), 0, IntPtr.Zero);

            if (result != 0)
            {
                throw new HttpListenerException((int)result);
            }
        }
    }
}
