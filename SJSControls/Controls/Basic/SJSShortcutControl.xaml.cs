using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace SJSControls.Controls.Basic
{
    /// <summary>
    /// SJSShortcutControl.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class SJSShortcutControl : UserControl
    {
        #region /////Dependency Property
        public static readonly DependencyProperty TBWidthProperty = DependencyProperty.Register("TBWidth", typeof(int), typeof(SJSShortcutControl), new PropertyMetadata(100));
        public static readonly DependencyProperty THeaderTextProperty = DependencyProperty.Register("THeaderText", typeof(string), typeof(SJSShortcutControl), new PropertyMetadata("Default"));
        public static readonly DependencyProperty THeaderWidthProperty = DependencyProperty.Register("THeaderWidth", typeof(int), typeof(SJSShortcutControl), new PropertyMetadata(50));
        public static readonly DependencyProperty THeaderColorProperty = DependencyProperty.Register("THeaderColor", typeof(Brush), typeof(SJSShortcutControl), new PropertyMetadata(Brushes.Black));
        public static readonly DependencyProperty ItemTextProperty = DependencyProperty.Register("ItemText", typeof(string), typeof(SJSShortcutControl), new FrameworkPropertyMetadata("", FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));
        public static readonly DependencyProperty TBColorProperty = DependencyProperty.Register("TBColor", typeof(Brush), typeof(SJSShortcutControl), new UIPropertyMetadata(Brushes.Black));
        public static readonly DependencyProperty TSemiColonColorProperty = DependencyProperty.Register("TSemiColonColor", typeof(Brush), typeof(SJSShortcutControl), new UIPropertyMetadata(Brushes.Black));
        public static readonly DependencyProperty BTNWidthProperty = DependencyProperty.Register("BTNWidth", typeof(int), typeof(SJSShortcutControl), new PropertyMetadata(100));
        public static readonly DependencyProperty BTNTextColorProperty = DependencyProperty.Register("BTNTextColor", typeof(Brush), typeof(SJSShortcutControl), new UIPropertyMetadata(Brushes.Black));



        public int BTNWidth
        {
            get { return (int)GetValue(BTNWidthProperty); }
            set { SetValue(BTNWidthProperty, value); }
        }
        public Brush BTNTextColor
        {
            get { return (Brush)GetValue(BTNTextColorProperty); }
            set { SetValue(BTNTextColorProperty, value); }
        }
        public Brush THeaderColor
        {
            get { return (Brush)GetValue(THeaderColorProperty); }
            set { SetValue(THeaderColorProperty, value); }
        }
        public string THeaderText
        {
            get { return (string)GetValue(THeaderTextProperty); }
            set { SetValue(THeaderTextProperty, value); }
        }
        public int THeaderWidth
        {
            get { return (int)GetValue(THeaderWidthProperty); }
            set { SetValue(THeaderWidthProperty, value); }
        }
        public int TBWidth
        {
            get { return (int)GetValue(TBWidthProperty); }
            set { SetValue(TBWidthProperty, value); }
        }
        public Brush TSemiColonColor
        {
            get { return (Brush)GetValue(TSemiColonColorProperty); }
            set { SetValue(TSemiColonColorProperty, value); }
        }
        public Brush TBColor
        {
            get { return (Brush)GetValue(TBColorProperty); }
            set { SetValue(TBColorProperty, value); }
        }
        public string ItemText
        {
            get { return (string)GetValue(ItemTextProperty); }
            set { SetValue(ItemTextProperty, value); }
        }
        #endregion
        
        public SJSShortcutControl()
        {
            InitializeComponent();
        }
    }
}
