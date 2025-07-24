using SJS.Helpers;
using System;
using System.Windows;
using System.Windows.Controls;

namespace Shortcut.Views.ControlViews
{
    /// <summary>
    /// ShorcutView.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class ShorcutView : UserControl
    {
        public ShorcutView()
        {
            InitializeComponent();
            //GetGridSplitterPosion();
        }
        //private void Splitter_DragCompleted(object sender, System.Windows.Controls.Primitives.DragCompletedEventArgs e)
        //{
        //    var VNCHeight = MainGrid.RowDefinitions[0].ActualHeight.ToString();
        //    var ControlHeight = MainGrid.RowDefinitions[2].ActualHeight.ToString();
        //    Helper.SJSIni.Write("GRIDSIZE", "VNCHeight", VNCHeight);
        //    Helper.SJSIni.Write("GRIDSIZE", "ControlHeight", ControlHeight);
        //}
        //private void GetGridSplitterPosion()
        //{
        //    if (Helper.SJSIni.Read("GRIDSIZE", "VNCHeight") != "")
        //    {
        //        MainGrid.RowDefinitions[0].Height = new GridLength(Convert.ToDouble(Helper.SJSIni.Read("GRIDSIZE", "VNCHeight")), GridUnitType.Star);
        //    }
        //    if (Helper.SJSIni.Read("GRIDSIZE", "ControlHeight") != "")
        //    {
        //        MainGrid.RowDefinitions[2].Height = new GridLength(Convert.ToDouble(Helper.SJSIni.Read("GRIDSIZE", "ControlHeight")), GridUnitType.Star);
        //    }
        //}
    }
}
