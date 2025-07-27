using System.Windows;

namespace SJSControls.Controls.Composite
{
    /// <summary>
    /// MessageWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MessageWindow : Window
    {
        public MessageWindow(string msg)
        {
            InitializeComponent();
            message.Text = msg;
        }
    }
}
