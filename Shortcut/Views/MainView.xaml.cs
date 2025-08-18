using SJS.Helpers;
using SJS.Utility;
using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;

namespace Shortcut.Views
{
    public partial class MainView : Window
    {
        public int HeightPreview { get; set; }
        public int HeightVNC { get; set; } = 50;
        public int HeightSplitter { get; set; } = 50;
        public int HeightProgram { get; set; } = 50;

        public WindowInteropHelper wi { get; set; }

        [DllImport("User32.dll")]
        private static extern bool RegisterHotKey(
            [In] IntPtr hWnd,
            [In] int id,
            [In] uint fsModifiers,
            [In] uint vk);

        [DllImport("User32.dll")]
        private static extern bool UnregisterHotKey(
            [In] IntPtr hWnd,
            [In] int id);



        private HwndSource _source;
        private const int HOTKEY_ID = 9000;


        public MainView()
        {
            InitializeComponent();

            this.MaxHeight = SystemParameters.VirtualScreenHeight;
            this.MinWidth = 65;
            UISettings();
            if (CHK_VerticalMode.IsChecked == true)
            {
                this.MaxWidth = 65;
            }
            else
            {
                ////this.MaxWidth = 600;

            }
        }
        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);
            var helper = new WindowInteropHelper(this);
            _source = HwndSource.FromHwnd(helper.Handle);
            _source.AddHook(new HwndSourceHook(Maximizer.WindowProc));
            RegisterHotKey();
        }
        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            var helper = new WindowInteropHelper(this);
            _source = HwndSource.FromHwnd(helper.Handle);
            _source.AddHook(new HwndSourceHook(Maximizer.WindowProc));
        }

        protected override void OnClosed(EventArgs e)
        {
            _source.RemoveHook(HwndHook);
            _source = null;
            UnregisterHotKey();
            base.OnClosed(e);
        }

        private void RegisterHotKey()
        {
            var helper = new WindowInteropHelper(this);
            const uint VK_Q = 0x51;
            const uint MOD_CTRL = 0x0002;
            if (!RegisterHotKey(helper.Handle, HOTKEY_ID, MOD_CTRL, VK_Q))
            {
                MessageBox.Show("단축키 에러");
            }
        }

        private void UnregisterHotKey()
        {
            var helper = new WindowInteropHelper(this);
            UnregisterHotKey(helper.Handle, HOTKEY_ID);
        }

        private IntPtr HwndHook(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            const int WM_HOTKEY = 0x0312;
            switch (msg)
            {
                case WM_HOTKEY:
                    switch (wParam.ToInt32())
                    {
                        case HOTKEY_ID:
                            OnHotKeyPressed();
                            handled = true;
                            break;
                    }
                    break;
            }
            return IntPtr.Zero;
        }

        private void OnHotKeyPressed()
        {
            if (CHK_VerticalMode.IsChecked == true && TGB_OpenAndClose.IsChecked == false)
            {
                if (CHK_MinimizeMode.IsChecked == true)
                {
                    this.WindowState = WindowState.Minimized;
                    TGB_OpenAndClose.IsChecked = true;
                }
                else
                {
                    this.MaxWidth = 65;
                    this.Height = 55;
                    TGB_OpenAndClose.IsChecked = true;
                }
            }
            else if (CHK_VerticalMode.IsChecked == true && TGB_OpenAndClose.IsChecked == true)
            {
                if (CHK_MinimizeMode.IsChecked == true)
                {
                    this.WindowState = WindowState.Normal;
                    this.MaxWidth = 65;
                    if (RB_Radio1.IsChecked == true)
                    {
                        MonitorSetting(0);
                    }
                    else if (RB_Radio2.IsChecked == true)
                    {
                        MonitorSetting(1);
                    }
                    TGB_OpenAndClose.IsChecked = false;
                }
                else
                {
                    this.MaxWidth = 65;
                    if (RB_Radio1.IsChecked == true)
                    {
                        MonitorSetting(0);
                    }
                    else if (RB_Radio2.IsChecked == true)
                    {
                        MonitorSetting(1);
                    }
                    TGB_OpenAndClose.IsChecked = false;

                }

            }
            else
            {
                if (CHK_MinimizeMode.IsChecked == true && TGB_OpenAndClose.IsChecked == false)
                {
                    this.WindowState = WindowState.Minimized;
                    TGB_OpenAndClose.IsChecked = true;
                }
                else if (CHK_MinimizeMode.IsChecked == true && TGB_OpenAndClose.IsChecked == true)
                {
                    this.WindowState = WindowState.Normal;
                    TGB_OpenAndClose.IsChecked = false;
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }

        private void UISettings()
        {
            ReadSettings();
        }
        private void MonitorSetting(int monitor)
        {
            this.WindowStartupLocation = WindowStartupLocation.Manual;
            System.Drawing.Rectangle workingArea = System.Windows.Forms.Screen.AllScreens[monitor].WorkingArea;
            this.Left = workingArea.Left;
            this.Top = workingArea.Top;
            this.Width = workingArea.Width;
            this.Height = workingArea.Height;
        }
        private void ReadSettings()
        {
            CHK_AlwaysTopMode.IsChecked = Helper.SJSIni.Read("Nomal", "TopMost").ToString() == "True" ? true : false;
            CHK_VerticalMode.IsChecked = Helper.SJSIni.Read("Nomal", "Horizon").ToString() == "True" ? true : false;
            CHK_MinimizeMode.IsChecked = Helper.SJSIni.Read("Nomal", "Minimize").ToString() == "True" ? true : false;
            RB_Radio1.IsChecked = Helper.SJSIni.Read("Nomal", "Radio1").ToString() == "True" ? true : false;
            RB_Radio2.IsChecked = Helper.SJSIni.Read("Nomal", "Radio2").ToString() == "True" ? true : false;
            RB_Radio3.IsChecked = Helper.SJSIni.Read("Nomal", "Radio3").ToString() == "True" ? true : false;

            if (CHK_VerticalMode.IsChecked == true)
            {
                RB_Radio1.IsEnabled = true;
                RB_Radio2.IsEnabled = true;
                RB_Radio3.IsEnabled = true;
                if (RB_Radio1.IsChecked == true)
                {
                    MonitorSetting(0);
                }
                else if(RB_Radio2.IsChecked == true)
                {
                    MonitorSetting(1);
                }
                else if (RB_Radio3.IsChecked == true)
                {
                    MonitorSetting(2);
                }
                else
                {
                    MonitorSetting(0);
                }
            }
            else
            {
                RB_Radio1.IsEnabled = false;
                RB_Radio2.IsEnabled = false;
                RB_Radio3.IsEnabled = false;
            }

        }
        private void CHK_AlwaysTopMode_Checked(object sender, RoutedEventArgs e)
        {
            if (CHK_AlwaysTopMode.IsChecked == true)
            {
                Helper.SJSIni.Write("Nomal", "TopMost", "True");
                this.Topmost = true;
                ReadSettings();
            }
        }

        private void CHK_AlwaysTopMode_Unchecked(object sender, RoutedEventArgs e)
        {
            if (CHK_AlwaysTopMode.IsChecked == false)
            {
                Helper.SJSIni.Write("Nomal", "TopMost", "False");
                this.Topmost = false;
                ReadSettings();
            }
        }

        private void CHK_VerticalMode_Checked(object sender, RoutedEventArgs e)
        {
            if (CHK_VerticalMode.IsChecked == true)
            {
                Helper.SJSIni.Write("Nomal", "Horizon", "True");
                this.MaxWidth = 65;

                ReadSettings();

            }
        }

        private void CHK_VerticalMode_Unchecked(object sender, RoutedEventArgs e)
        {
            if (CHK_VerticalMode.IsChecked == false)
            {
                Helper.SJSIni.Write("Nomal", "Horizon", "False");
                this.MaxWidth = 600;
                this.Width = 600;
                this.Height = 500;
                ReadSettings();
            }
        }
        private void Radio1_Checked(object sender, RoutedEventArgs e)
        {
            if (RB_Radio1.IsChecked == true)
            {
                Helper.SJSIni.Write("Nomal", "Radio1", "True");
                Helper.SJSIni.Write("Nomal", "Radio2", "False");
                Helper.SJSIni.Write("Nomal", "Radio3", "False");
                ReadSettings();
            }
        }

        private void Radio2_Checked(object sender, RoutedEventArgs e)
        {
            if (RB_Radio2.IsChecked == true)
            {
                Helper.SJSIni.Write("Nomal", "Radio2", "True");
                Helper.SJSIni.Write("Nomal", "Radio1", "False");
                Helper.SJSIni.Write("Nomal", "Radio3", "False");
                ReadSettings();
            }
        }
        private void Radio3_Checked(object sender, RoutedEventArgs e)
        {
            if (RB_Radio3.IsChecked == true)
            {
                Helper.SJSIni.Write("Nomal", "Radio3", "True");
                Helper.SJSIni.Write("Nomal", "Radio1", "False");
                Helper.SJSIni.Write("Nomal", "Radio2", "False");
                ReadSettings();
            }
        }
        private void TGB_OpenAndClose_Checked(object sender, RoutedEventArgs e)
        {


            if (CHK_VerticalMode.IsChecked == true)
            {
                this.MaxWidth = 65;
                this.Height = 75;
            }
        }
        private void TGB_OpenAndClose_Unchecked(object sender, RoutedEventArgs e)
        {
            if (CHK_VerticalMode.IsChecked == true)
            {
                this.MaxWidth = 65;
                if (RB_Radio1.IsChecked == true)
                {
                    MonitorSetting(0);
                }
                else if (RB_Radio2.IsChecked == true)
                {
                    MonitorSetting(1);
                }
            }
        }
        private void CHK_MinimizeMode_Checked(object sender, RoutedEventArgs e)
        {
            if (CHK_MinimizeMode.IsChecked == true)
            {
                Helper.SJSIni.Write("Nomal", "Minimize", "True");
                ReadSettings();
            }
        }

        private void CHK_MinimizeMode_Unchecked(object sender, RoutedEventArgs e)
        {
            if (CHK_MinimizeMode.IsChecked == false)
            {
                Helper.SJSIni.Write("Nomal", "Minimize", "False");
                ReadSettings();
            }
        }
        private void TGB_OpenAndClose_PreviewMouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (myPopup.DataContext == null || myPopup.DataContext != sender)
            {
                myPopup.PlacementTarget = sender as UIElement;
            }
            Point mouse = e.GetPosition(sender as UIElement);
            myPopup.HorizontalOffset = mouse.X;
            myPopup.VerticalOffset = mouse.Y + 30;
        }
    }
}
