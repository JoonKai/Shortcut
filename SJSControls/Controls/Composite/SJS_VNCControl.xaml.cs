using SJS.Helpers;
using SJS.Utility;
using SJSControls.Utils;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace SJSControls.Controls.Composite
{
    /// <summary>
    /// SJS_VNCControl.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class SJS_VNCControl : UserControl, INotifyPropertyChanged
    {
        #region /////Notify
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
        #endregion

        #region /////프로퍼티
        public static readonly DependencyProperty ControlNameProperty = DependencyProperty.Register("ControlName", typeof(string), typeof(SJS_VNCControl), new PropertyMetadata("root"));
        public string vncDirectory = AppDomain.CurrentDomain.BaseDirectory + "VNC\\";

        public string ControlName
        {
            get { return (string)GetValue(ControlNameProperty); }
            set { SetValue(ControlNameProperty, value); }
        }
        public string MyNumber
        {
            get { return (string)GetValue(MyNumberProperty); }
            set { SetValue(MyNumberProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyNumber.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MyNumberProperty =
            DependencyProperty.Register("MyNumber", typeof(string), typeof(SJS_VNCControl), new PropertyMetadata(""));
        string titleText = "";
        public string TitleText
        {
            get
            {
                return titleText;
            }
            set
            {
                value = titleText;
                OnPropertyChanged("TitleText");
            }
        }
        string ipText = "";
        public string IPText
        {
            get
            {
                return ipText;
            }
            set
            {
                value = ipText;
                OnPropertyChanged("IPText");
            }
        }
        string myTag = "";
        public string MyTag
        {
            get
            {
                return myTag;
            }
            set
            {
                value = myTag;
                OnPropertyChanged("MyTag");
            }
        }

        UsingVNC cv = new UsingVNC();
        MessageWindow mw;

        #endregion

        public SJS_VNCControl()
        {
            InitializeComponent();
            this.DataContext = this;
            Loaded += VNCControl_Loaded;
        }

        public ICommand RunVNC => new RelayCommand<string>(RunVNCProcess);
        public ICommand SaveSetting => new RelayCommand<string>(SaveSettingView);
        public ICommand ClearTextbox => new RelayCommand<string>(ClearTB);



        private void VNCControl_Loaded(object sender, RoutedEventArgs e)
        {
            ReadSettingVIew();
        }
        private void RunVNCProcess(string obj)
        {
            try
            {
                if (WT2.Text.ToString() == "")
                {
                    mw = new MessageWindow("텍스트박스가 비어 있습니다.");
                    mw.ShowDialog();
                    return;
                }
                var program = vncDirectory + obj.ToString() + ".vnc";
                Process.Start(program);
            }
            catch (Exception e)
            {
                Helper.SJSLog.Error("RunVNCProcess Error");
            }
            
        }
        private void ClearTB(string obj)
        {
            WT1.Text = "";
            WT2.Text = "";
            var program = vncDirectory + obj.ToString() + ".vnc";

            if (File.Exists(program))
            {
                File.Delete(program);
            }
        }

        private void SaveSettingView(string obj)
        {
            if (WT1.Text == "" || WT2.Text == "") return;

            DirectoryInfo dir = new DirectoryInfo(vncDirectory);

            if (dir.Exists == false)
            {
                Directory.CreateDirectory(vncDirectory.ToString());
            }
            var path = vncDirectory + ControlName + ".vnc";


            cv.setVNC(path, "host", WT2.Text.ToString(), "TitleText", WT1.Text.ToString());
        }
        private void ReadSettingVIew()
        {
            var path = vncDirectory + ControlName + ".vnc";
            if (!File.Exists(path)) return;

            WT1.Text = cv.ReadVNC("Title", "TitleText", path).ToString();
            WT2.Text = cv.ReadVNC("connection", "host", path).ToString();
            myTag = WT1.Text.ToString();
        }
        private void Border_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            if (myPopup.DataContext == null || myPopup.DataContext != sender)
            {
                myPopup.PlacementTarget = sender as UIElement;
            }
            Point mouse = e.GetPosition(sender as UIElement);
            MyTag = WT1.Text.ToString();
            myPopup.HorizontalOffset = mouse.X;
            myPopup.VerticalOffset = mouse.Y + 30;
        }
    }
}
