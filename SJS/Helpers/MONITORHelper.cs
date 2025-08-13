using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Windows;

namespace SJS.Helpers
{
    public class MONITORHelper
    {
        // 1) 콜백 시그니처를 Win32와 1:1로 맞춤 + 호출 규약 명시
        [UnmanagedFunctionPointer(CallingConvention.Winapi)]
        private delegate bool MonitorEnumProc(
            IntPtr hMonitor,         // HMONITOR
            IntPtr hdcMonitor,       // HDC
            ref RECT lprcMonitor,    // LPRECT
            IntPtr dwData            // LPARAM
        );

        [DllImport("user32.dll", ExactSpelling = true)]
        private static extern bool EnumDisplayMonitors(
            IntPtr hdc,              // HDC (null이면 모든 모니터)
            IntPtr lprcClip,         // LPCRECT
            MonitorEnumProc lpfnEnum,
            IntPtr dwData
        );

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern bool GetMonitorInfo(
            IntPtr hMonitor,
            ref MONITORINFOEX lpmi
        );

        [DllImport("shcore.dll")]
        private static extern int GetDpiForMonitor(
            IntPtr hmonitor,
            int dpiType,
            out uint dpiX,
            out uint dpiY
        );

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        private struct MONITORINFOEX
        {
            public int cbSize;
            public RECT rcMonitor;
            public RECT rcWork;
            public int dwFlags;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
            public string szDevice;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct RECT { public int left, top, right, bottom; }

        public sealed class MonitorInfo
        {
            public IntPtr Handle { get; set; }
            public RectPx MonitorArea { get; set; }
            public RectPx WorkArea { get; set; }
            public bool IsPrimary { get; set; }
            public string DeviceName { get; set; }
        }

        public struct RectPx
        {
            public int Left, Top, Right, Bottom;
            public int Width { get { return Right - Left; } }
            public int Height { get { return Bottom - Top; } }
        }

        public static List<MonitorInfo> EnumerateMonitors()
        {
            var list = new List<MonitorInfo>();

            // 2) 매개변수 이름을 선언/사용 모두 동일하게 유지 (hMonitor, hdcMonitor, lprcMonitor, dwData)
            MonitorEnumProc callback = (IntPtr hMonitor, IntPtr hdcMonitor, ref RECT lprcMonitor, IntPtr dwData) =>
            {
                var mi = new MONITORINFOEX { cbSize = Marshal.SizeOf(typeof(MONITORINFOEX)) };
                if (GetMonitorInfo(hMonitor, ref mi))
                {
                    list.Add(new MonitorInfo
                    {
                        Handle = hMonitor,
                        MonitorArea = new RectPx { Left = mi.rcMonitor.left, Top = mi.rcMonitor.top, Right = mi.rcMonitor.right, Bottom = mi.rcMonitor.bottom },
                        WorkArea = new RectPx { Left = mi.rcWork.left, Top = mi.rcWork.top, Right = mi.rcWork.right, Bottom = mi.rcWork.bottom },
                        IsPrimary = (mi.dwFlags & 1) != 0,
                        DeviceName = mi.szDevice
                    });
                }
                return true;
            };

            // 3) HDC/LPCRECT는 전체 모니터 열거 목적이면 null 전달 (IntPtr.Zero)
            EnumDisplayMonitors(IntPtr.Zero, IntPtr.Zero, callback, IntPtr.Zero);
            return list;
        }

        public void MoveToMonitorWorkArea(Window window, int monitorIndex)
        {
            var monitors = EnumerateMonitors();
            if (monitors.Count == 0) return;
            if (monitorIndex < 0 || monitorIndex >= monitors.Count) monitorIndex = 0;

            var m = monitors[monitorIndex];
            uint dpiX = 96, dpiY = 96;
            try
            {
                // MDT_EFFECTIVE_DPI = 0
                if (GetDpiForMonitor(m.Handle, 0, out dpiX, out dpiY) != 0) dpiX = dpiY = 96;
            }
            catch { dpiX = dpiY = 96; }

            double PxToDip(int px, uint dpi) => px * 96.0 / (dpi == 0 ? 96.0 : dpi);

            window.WindowStartupLocation = WindowStartupLocation.Manual;
            window.Left = PxToDip(m.WorkArea.Left, dpiX);
            window.Top = PxToDip(m.WorkArea.Top, dpiY);
            window.Width = PxToDip(m.WorkArea.Width, dpiX);
            window.Height = PxToDip(m.WorkArea.Height, dpiY);
        }
    }
}
