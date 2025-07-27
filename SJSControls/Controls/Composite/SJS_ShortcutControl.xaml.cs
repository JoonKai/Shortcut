using Microsoft.Win32;
using SJS.Helpers;
using SJS.Utility;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace SJSControls.Controls.Composite
{
    /// <summary>
    /// SJS_ShortcutControl.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class SJS_ShortcutControl : UserControl, INotifyPropertyChanged
    {
        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        #endregion

        public static readonly DependencyProperty ControlNameProperty = DependencyProperty.Register("ControlName", typeof(string), typeof(SJS_ShortcutControl), new PropertyMetadata("root"));
        public static readonly DependencyProperty MyNumberProperty = DependencyProperty.Register("MyNumber", typeof(string), typeof(SJS_ShortcutControl), new PropertyMetadata(""));
        public static readonly DependencyProperty TXBTextProperty = DependencyProperty.Register("TXBText", typeof(string), typeof(SJS_ShortcutControl), new PropertyMetadata("Path"));
        public static readonly DependencyProperty BTNWidthProperty = DependencyProperty.Register("BTNWidth", typeof(int), typeof(SJS_ShortcutControl), new PropertyMetadata(50));



        public int BTNWidth
        {
            get { return (int)GetValue(BTNWidthProperty); }
            set { SetValue(BTNWidthProperty, value); }
        }
        public string TXBText
        {
            get { return (string)GetValue(TXBTextProperty); }
            set { SetValue(TXBTextProperty, value); }
        }
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
        string programPath = null;
        public string ProgramPath
        {
            get
            {
                return programPath;
            }
            set
            {
                value = programPath;
                OnPropertyChanged("ProgramPath");
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
        MessageWindow mw;
        public string programDirectory = AppDomain.CurrentDomain.BaseDirectory + "Programs\\";
        public ICommand RunProgram => new RelayCommand<string>(StrartProgram);
        public ICommand OpenPath => new RelayCommand<string>(OpenFile);
        public ICommand OpenFolderPath => new RelayCommand<string>(OpenFolder);
        public ICommand ClearTextBox => new RelayCommand<string>(ClearTBX);


        private void ClearTBX(string obj)
        {
            WT1.Text = "";
            WT2.Text = "";
            Helper.SJSIni.Write(programPath, ControlName, "TitleText", null);
            Helper.SJSIni.Write(programPath, ControlName, "ProgramPath", null);
        }

        private void OpenFile(string obj)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                WT2.Text = openFileDialog.FileName;
            }
        }
        private void OpenFolder(string obj)
        {
            //System.Windows.Forms.FolderBrowserDialog fbd = new System.Windows.Forms.FolderBrowserDialog();
            //if (fbd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            //{
            //    WT2.Text = fbd.SelectedPath;
            //}
        }
        private void StrartProgram(string obj)
        {
            try
            {
                if (WT2.Text.ToString() == "")
                {
                    mw = new MessageWindow("텍스트박스가 비어 있습니다.");
                    mw.ShowDialog();
                    return;
                }
                var program = WT2.Text.ToString();
                Process.Start(program);
            }
            catch (Exception e)
            {
                MessageBox.Show("에러가 발생했습니다.");
            }

        }

        public SJS_ShortcutControl()
        {
            InitializeComponent();
            this.DataContext = this;
            Loaded += ProgramControl_Loaded;
        }
        private void ProgramControl_Loaded(object sender, RoutedEventArgs e)
        {
            programPath = AppDomain.CurrentDomain.BaseDirectory + "Programs\\program.ini";
            ReadSettingVIew();
        }

        public ICommand SaveSetting => new RelayCommand<string>(SaveSettingView);

        private void SaveSettingView(string obj)
        {
            if (WT1.Text.ToString() == "" || WT2.Text == "") return;

            DirectoryInfo dir = new DirectoryInfo(programDirectory);

            if (dir.Exists == false)
            {
                Directory.CreateDirectory(programDirectory.ToString());
            }
            Helper.SJSIni.Write(programPath, ControlName, "TitleText", WT1.Text.ToString());
            Helper.SJSIni.Write(programPath, ControlName, "ProgramPath", WT2.Text.ToString());
        }
        private void ReadSettingVIew()
        {
            if (!File.Exists(programPath)) return;

            WT1.Text = Helper.SJSIni.Read(programPath, ControlName, "TitleText").ToString();
            WT2.Text = Helper.SJSIni.Read(programPath, ControlName, "ProgramPath").ToString();

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
