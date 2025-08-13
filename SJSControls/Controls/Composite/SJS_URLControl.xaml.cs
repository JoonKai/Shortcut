using SJS.Helpers;
using SJS.Utility;
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
    /// SJS_URLControl.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class SJS_URLControl : UserControl, INotifyPropertyChanged
    {
        #region /////Notify
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
        #endregion
        #region /////프로퍼티
        public static readonly DependencyProperty URLControlNameProperty = DependencyProperty.Register("URLControlName", typeof(string), typeof(SJS_URLControl), new PropertyMetadata("root"));
        public static readonly DependencyProperty MyURLNumberProperty = DependencyProperty.Register("MyURLNumber", typeof(string), typeof(SJS_URLControl), new PropertyMetadata(""));


        public string URLControlName
        {
            get { return (string)GetValue(URLControlNameProperty); }
            set { SetValue(URLControlNameProperty, value); }
        }
        public string MyURLNumber
        {
            get { return (string)GetValue(MyURLNumberProperty); }
            set { SetValue(MyURLNumberProperty, value); }
        }
        string titleText = null;
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
        string myURL = null;
        public string MyURL
        {
            get
            {
                return myURL;
            }
            set
            {
                value = myURL;
                OnPropertyChanged("MyURL");
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
        string prefix = "https://";
        public string Prefix
        {
            get
            {
                return prefix;
            }
            set
            {
                value = prefix;
                OnPropertyChanged("Prefix");
            }
        }
        MessageWindow mw;
        public string URLDirectory = AppDomain.CurrentDomain.BaseDirectory + "URL\\";

        #endregion

        public ICommand RunNavigation => new RelayCommand<string>(NavigateURL);
        public ICommand SaveSetting => new RelayCommand<string>(SaveSettingView);
        public ICommand ClearTextBox => new RelayCommand<string>(ClearTB);


        private void ClearTB(string obj)
        {
            WT1.Text = "";
            WT2.Text = "";
            Helper.SJSIni.Write(MyURL, URLControlName, "TitleText", null);
            Helper.SJSIni.Write(MyURL, URLControlName, "MyTag", null);
        }
        private void NavigateURL(string obj)
        {
            try
            {
                if (WT1.Text.ToString() == "" || WT2.Text.ToString() == "")
                {
                    mw = new MessageWindow("텍스트박스가 비어 있습니다.");
                    mw.ShowDialog();
                    return;
                }
                else
                {
                    var url = WT2.Text.ToString();

                    Process.Start(new ProcessStartInfo
                    {
                        FileName = url,
                        UseShellExecute = true
                    });
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message+"에러가 발생했습니다.");
            }

        }

        public SJS_URLControl()
        {
            InitializeComponent();
            this.DataContext = this;
            Loaded += URLControl_Loaded;
        }
        private void URLControl_Loaded(object sender, RoutedEventArgs e)
        {
            myURL = AppDomain.CurrentDomain.BaseDirectory + "URL\\URL.ini";
            ReadSettingVIew();
        }
        private void SaveSettingView(string obj)
        {
            var title = WT1.Text.ToString();
            var tag = WT2.Text.ToString();
            if (title == "" || tag == "")
            {
                return;
            }
            else
            {
                Helper.SJSIni.Write(MyURL, URLControlName, "TitleText", title);
                Helper.SJSIni.Write(MyURL, URLControlName, "MyTag", tag);
            }

        }
        private void ReadSettingVIew()
        {
            if (!File.Exists(MyURL)) return;

            WT1.Text = Helper.SJSIni.Read(MyURL, URLControlName, "TitleText").ToString();
            WT2.Text = Helper.SJSIni.Read(MyURL, URLControlName, "MyTag").ToString();

        }
        private void Border_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            if (myPopup.DataContext == null || myPopup.DataContext != sender)
            {
                myPopup.PlacementTarget = sender as UIElement;
            }
            Point mouse = e.GetPosition(sender as UIElement);
            myTag = WT1.Text.ToString();
            MyTag = WT1.Text.ToString();
            myPopup.HorizontalOffset = mouse.X;
            myPopup.VerticalOffset = mouse.Y + 30;
        }
    }
}
