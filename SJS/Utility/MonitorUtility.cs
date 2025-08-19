using SJS.Models;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Interop;
using System.Windows.Media;

namespace SJS.Utility
{
    public class MonitorUtility
    {
        private Matrix GetFromDeviceMatrix(Window w)
        {
            var src = PresentationSource.FromVisual(w) as HwndSource;
            if (src != null && src.CompositionTarget != null)
                return src.CompositionTarget.TransformFromDevice;
            return Matrix.Identity;
        }

        private Rect PixelToDip(Matrix fromDevice, Rect px)
        {
            var tl = fromDevice.Transform(new System.Windows.Point(px.Left, px.Top));
            var br = fromDevice.Transform(new System.Windows.Point(px.Right, px.Bottom));
            return new Rect(tl, br);
        }

        /// <summary>
        /// 지정 인덱스 모니터의 작업영역을 DIP로 얻음
        /// </summary>
        public Rect GetWorkAreaDip(Window w, int monitorIndex)
        {
            var screens = Screen.AllScreens;
            if (screens == null || screens.Length == 0)
                return SystemParameters.WorkArea; // 안전한 기본값

            if (monitorIndex < 0) monitorIndex = 0;
            if (monitorIndex >= screens.Length) monitorIndex = screens.Length - 1;

            var s = screens[monitorIndex];
            var workPx = new Rect(s.WorkingArea.Left, s.WorkingArea.Top, s.WorkingArea.Width, s.WorkingArea.Height);

            var fromDevice = GetFromDeviceMatrix(w);
            return PixelToDip(fromDevice, workPx);
        }

        /// <summary>
        /// 창을 지정 모니터 작업영역에 맞춰 배치
        /// </summary>
        public void ApplyWindowToMonitor(Window w, int monitorIndex, bool verticalMode, double verticalMaxWidth, double? collapsedFixedHeight)
        {
            var work = GetWorkAreaDip(w, monitorIndex);

            w.WindowStartupLocation = WindowStartupLocation.Manual;

            if (verticalMode)
            {
                w.MaxWidth = verticalMaxWidth;
                w.Left = work.Left;
                w.Top = work.Top;
                w.Width = verticalMaxWidth;
                if (collapsedFixedHeight.HasValue)
                    w.Height = collapsedFixedHeight.Value;   // 접힘(축소) 높이
                else
                    w.Height = work.Height;                  // 펼침(전체)
            }
            else
            {
                w.MaxWidth = work.Width;
                w.Left = work.Left;
                w.Top = work.Top;
                w.Width = work.Width;
                w.Height = work.Height;
            }
        }
    }
}
