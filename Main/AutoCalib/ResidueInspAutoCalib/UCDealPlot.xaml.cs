using InteractiveDataDisplay.WPF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Main
{
    /// <summary>
    /// UCDealPlot.xaml 的交互逻辑
    /// </summary>
    public partial class UCDealPlot : UserControl
    {
        public UCDealPlot()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 绘制单条折线
        /// </summary>
        /// <param name="plotPar">折线数据</param>
        public void DrawPlotLine(PlotPar plotPar)
        {
            var lg = new LineGraph();
            lines.Children.Add(lg);
            lg.Stroke = new SolidColorBrush(plotPar.LineColor);
            lg.Description = plotPar.LineName;
            lg.StrokeThickness = 2;
            lg.Plot(plotPar.X, plotPar.Y);
        }

        /// <summary>
        /// 绘制多条折线
        /// </summary>
        /// <param name="plotPar">折线数据List</param>
        public void DrawPlotLine(List<PlotPar> plotPar)
        {
            if (!(plotPar?.Count > 0))
            {
                return;
            }
            ///这里就用List的第一个数据作为图表参数    临时方案  0302
            txtTitle.Text = plotPar[0].ChartTitle;
            txtHorizontal.Text = plotPar[0].HorizontalText;
            txtVertical.Text = plotPar[0].VerticalText;
            foreach(PlotPar par in plotPar)
            {
                var lg = new LineGraph();
                lines.Children.Add(lg);
                lg.Stroke = new SolidColorBrush(par.LineColor);
                lg.Description = par.LineName;
                lg.StrokeThickness = 2;
                lg.Plot(par.X, par.Y);
            }
        }

        public void ClearPlot()
        {
            lines.Children.Clear();
        }
    }

    /// <summary>
    /// 用于绘制折线图的类
    /// </summary>
    public class PlotPar
    {
        public PlotPar()
        {

        }
        public PlotPar(double[] x, double[] y)
        {
            X = x;
            Y = y;
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="x">X轴数据</param>
        /// <param name="y">Y轴数据</param>
        /// <param name="color">折线颜色</param>
        /// <param name="lineName">折线名称</param>
        /// <param name="chartTitle">折线图标题</param>
        /// <param name="horizontalText">横坐标名称</param>
        /// <param name="verticalText">纵坐标名称</param>
        public PlotPar(double[] x, double[] y,Color color,
                      string lineName, string chartTitle, 
                      string horizontalText, string verticalText)
        {
            X = x;
            Y = y;
            LineColor = color;
            LineName = lineName;
            ChartTitle = chartTitle;
            HorizontalText = horizontalText;
            VerticalText = verticalText;
        }

        /// <summary>
        /// X轴方向数据
        /// </summary>
        public double[] X = new double[10];
        /// <summary>
        /// Y轴方向数据
        /// </summary>
        public double[] Y = new double[10];
        /// <summary>
        /// 折线颜色
        /// </summary>
        public Color LineColor = Color.FromRgb(255,0,0);
        /// <summary>
        /// 数据名称
        /// </summary>
        public String LineName = "NO NAME";
        /// <summary>
        /// 折线图标题
        /// </summary>
        public string ChartTitle = "折线统计图";
        /// <summary>
        /// 横坐标名称
        /// </summary>
        public string HorizontalText = "横坐标";
        /// <summary>
        /// 纵坐标名称
        /// </summary>
        public string VerticalText = "纵坐标";
    }
    public class VisibilityToCheckedConverter2 : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return ((Visibility)value) == Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return ((bool)value) ? Visibility.Visible : Visibility.Collapsed;
        }
    }



    public class KButton : Button
    {
        public static readonly DependencyProperty ForeImageProperty;
        public static readonly DependencyProperty BackImageProperty;
        public static readonly DependencyProperty MouseOverBackColorProperty;
        public static readonly DependencyProperty StretchProperty;
        static KButton()
        {
            ForeImageProperty = DependencyProperty.Register("ForeImage", typeof(string), typeof(KButton), null);
            ForeImageProperty = DependencyProperty.Register("BackImage", typeof(string), typeof(KButton), null);
            MouseOverBackColorProperty = DependencyProperty.Register("MouseOverBackColor", typeof(Brush), typeof(KButton), null);
            StretchProperty = DependencyProperty.Register("Stretch", typeof(Stretch), typeof(KButton), null);
            DefaultStyleKeyProperty.OverrideMetadata(typeof(KButton), new FrameworkPropertyMetadata(typeof(KButton)));//使KButton去读取KButton类型的样式，而不是去读取Button的样式
        }
        public string ForeImage
        {
            get { return (string)GetValue(ForeImageProperty); }
            set { SetValue(ForeImageProperty, value); }
        }
        public string BackImage
        {
            get { return (string)GetValue(BackImageProperty); }
            set { SetValue(BackImageProperty, value); }
        }
        public Brush MouseOverBackColor
        {
            get { return (Brush)GetValue(MouseOverBackColorProperty); }
            set { SetValue(MouseOverBackColorProperty, value); }
        }
        public Stretch Stretch
        {
            get { return (Stretch)GetValue(StretchProperty); }
            set { SetValue(StretchProperty, value); }
        }
    }
}
