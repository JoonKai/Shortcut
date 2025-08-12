using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace SJSControls.Controls.Basic
{
    public partial class SJS_BaseTextBox : UserControl
    {
        #region 디펜던시프로퍼티
        public static readonly DependencyProperty TBXHeightProperty = DependencyProperty.Register("TBXHeight", typeof(int), typeof(SJS_BaseTextBox), new PropertyMetadata(20));
        public static readonly DependencyProperty TBXWidthProperty = DependencyProperty.Register("TBXWidth", typeof(int), typeof(SJS_BaseTextBox), new PropertyMetadata(100));
        public static new readonly DependencyProperty HorizontalContentAlignmentProperty = DependencyProperty.Register("HorizontalContentAlignment", typeof(HorizontalAlignment), typeof(SJS_WaterMarkTextBox), new UIPropertyMetadata(HorizontalAlignment.Left));
        public static readonly DependencyProperty TextProperty = DependencyProperty.Register("Text", typeof(string), typeof(SJS_BaseTextBox), new FrameworkPropertyMetadata("", FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));
        public static readonly DependencyProperty TBXBorderBrushProperty = DependencyProperty.Register("TBXBorderBrush", typeof(Brush), typeof(SJS_BaseTextBox), new UIPropertyMetadata(Brushes.White));
        public static readonly DependencyProperty TBXBorderThicknessProperty = DependencyProperty.Register("TBXBorderThickness", typeof(Thickness), typeof(SJS_BaseTextBox), new UIPropertyMetadata(new Thickness(0.5)));
        public static readonly DependencyProperty TBXTextColorProperty = DependencyProperty.Register("TBXTextColor", typeof(Brush), typeof(SJS_BaseTextBox), new UIPropertyMetadata(Brushes.Black));
        public static readonly DependencyProperty TBXReadOnlyProperty = DependencyProperty.Register("TBXReadOnly", typeof(bool), typeof(SJS_BaseTextBox), new PropertyMetadata(false));
        public static readonly DependencyProperty TBXTextWrappingProperty = DependencyProperty.Register("TBXTextWrapping", typeof(TextWrapping), typeof(SJS_BaseTextBox), new UIPropertyMetadata(TextWrapping.NoWrap));
        public static readonly DependencyProperty TBXAcceptsReturnProperty = DependencyProperty.Register("TBXAcceptsReturn", typeof(bool), typeof(SJS_BaseTextBox), new UIPropertyMetadata(false));



        public int TBXWidth
        {
            get { return (int)GetValue(TBXWidthProperty); }
            set { SetValue(TBXWidthProperty, value); }
        }
        public int TBXHeight
        {
            get { return (int)GetValue(TBXHeightProperty); }
            set { SetValue(TBXHeightProperty, value); }
        }
        public TextWrapping TBXTextWrapping
        {
            get { return (TextWrapping)GetValue(TBXTextWrappingProperty); }
            set { SetValue(TBXTextWrappingProperty, value); }
        }
        public bool AcceptsReturn
        {
            get { return (bool)GetValue(TBXAcceptsReturnProperty); }
            set { SetValue(TBXAcceptsReturnProperty, value); }
        }
        public new HorizontalAlignment HorizontalContentAlignment
        {
            get { return (HorizontalAlignment)GetValue(HorizontalContentAlignmentProperty); }
            set { SetValue(HorizontalContentAlignmentProperty, value); }
        }
        public bool TBXReadOnly
        {
            get { return (bool)GetValue(TBXReadOnlyProperty); }
            set { SetValue(TBXReadOnlyProperty, value); }
        }
        public Brush TBXTextColor
        {
            get { return (Brush)GetValue(TBXTextColorProperty); }
            set { SetValue(TBXTextColorProperty, value); }
        }
        public Brush TBXBorderBrush
        {
            get { return (Brush)GetValue(TBXBorderBrushProperty); }
            set { SetValue(TBXBorderBrushProperty, value); }
        }
        public Thickness TBXBorderThickness
        {
            get { return (Thickness)GetValue(TBXBorderThicknessProperty); }
            set { SetValue(TBXBorderThicknessProperty, value); }
        }
        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }



        #endregion
        public SJS_BaseTextBox()
        {
            InitializeComponent();
        }
    }
}
