using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Text.RegularExpressions;

namespace ProjectSPJ
{
    /// <summary>
    /// UCRangeLimitation.xaml 的交互逻辑
    /// </summary>
    public partial class UCRangeLimitation : UserControl
    {
        private PickRangeLim backup;
        public UCRangeLimitation()
        {
            InitializeComponent();
            DataContext = PickRangeLim.Inst;
            backup = PickRangeLim.Inst.Clone() as PickRangeLim;
        }
        /// <summary>
        /// 标定区域显示的缩放比例
        /// </summary>
        private double _ratio = 0;
        /// <summary>
        /// 标定的区域
        /// </summary>
        private Rect _calibRect = new Rect();

        /// <summary>
        ///限制输入为数字
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Tb_PreviewTextInput(object sender, TextCompositionEventArgs e)

        {

            Regex re = new Regex("[^0-9.-]+");

            e.Handled = re.IsMatch(e.Text);

        }

        private void UserControl_SizeChanged(object sender, SizeChangedEventArgs e)
        {            
            DrawCalibRegion(PickRangeLim.Inst.RectCalibRange);
            DrawLimitRange(PickRangeLim.Inst.Rects_L);
        }
        /// <summary>
        /// 绘制标定区域
        /// </summary>
        /// <param name="rect">标定区域</param>
        private void DrawCalibRegion(Rect rect)
        {
            if (rect.Width * rect.Height == 0)
            {
                return;
            }
            _ratio = rect.Width > rect.Height ?
                    gdRoot.ActualWidth / rect.Width : gdRoot.ActualHeight / rect.Height;
            _calibRect = rect;
            DrawRect(rect, Colors.Green);

        }
        /// <summary>
        /// 窗口内绘制长方形
        /// </summary>
        /// <param name="rect">长方形</param>
        /// <param name="colors">应纳税额</param>
        private void DrawRect(Rect rect, Color colors)
        {
            Rectangle rectangle = new Rectangle
            {
                Width = gdRoot.ActualHeight,
                Height = rect.Height * _ratio,
                Fill = new SolidColorBrush(colors)
            };
            gdRoot.Children.Add(rectangle);
        }
        
        /// <summary>
        /// 绘制取料限制区域
        /// </summary>
        /// <param name="rect_L">取料限制区域</param>
        private void DrawLimitRange(List<Rect> rect_L)
        {
            //gdRoot.Children.Clear();
            if(gdRoot.Children.Count > 1)
            {
                gdRoot.Children.RemoveRange(1, gdRoot.Children.Count - 1);
            }
            foreach(Rect rect in rect_L)
            {
                if(rect.Width * rect.Height == 0)
                {
                    continue;
                }
                if(!_calibRect.Contains(rect))
                {
                    MessageBox.Show("输入的区域不在标定范围内！！");
                    return;
                }

                double left = (gdRoot.ActualWidth - _calibRect.Width * _ratio) / 2 +
                    (rect.Left - _calibRect.Left) * _ratio;
                double right = (gdRoot.ActualWidth - _calibRect.Width * _ratio) / 2 +
                    (_calibRect.Right - rect.Right) * _ratio;
                double top = (gdRoot.ActualHeight - _calibRect.Height * _ratio) / 2 -
                    (_calibRect.Top - rect.Top) * _ratio;
                double bottom = (gdRoot.ActualHeight - _calibRect.Height * _ratio) / 2 -
                    (rect.Bottom - _calibRect.Bottom) * _ratio;

                Rectangle rectangle1 = new Rectangle
                {
                    Width = rect.Width * _ratio,
                    Height = rect.Height * _ratio,
                    Fill = new SolidColorBrush(Colors.Red),
                    HorizontalAlignment = HorizontalAlignment.Left,
                    VerticalAlignment = VerticalAlignment.Bottom,
                    Margin = new Thickness(left, top, right, bottom),
                };
                gdRoot.Children.Add(rectangle1);
            }            
        }


        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            PickRangeLim.Inst.AddLimitRegion(new LimitRect());
            DrawLimitRange(PickRangeLim.Inst.Rects_L);
            RgvLimtRange.Items.Refresh();
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            if(RgvLimtRange.SelectedIndex < 0)
            {
                return;
            }
            PickRangeLim.Inst.DeleteLimitRegion(RgvLimtRange.SelectedIndex);
            RgvLimtRange.Items.Refresh();

        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            if(PickRangeLim.Inst.SaveConfigToLocal())
            {
                BtnSave.RefreshDefaultColor("保存成功", true);
                backup = PickRangeLim.Inst.Clone() as PickRangeLim;
            }
            else
            {
                BtnSave.RefreshDefaultColor("保存失败", false);
            }
        }

        private void BtnRefresh_Click(object sender, RoutedEventArgs e)
        {
            gdRoot.Children.Clear();
            DrawCalibRegion(PickRangeLim.Inst.RectCalibRange);
            DrawLimitRange(PickRangeLim.Inst.Rects_L);
        }

        private void TextBox_TextInput(object sender, TextCompositionEventArgs e)
        {
            DrawCalibRegion(PickRangeLim.Inst.RectCalibRange);
            DrawLimitRange(PickRangeLim.Inst.Rects_L);
        }

        private void UserControl_Unloaded(object sender, RoutedEventArgs e)
        {
            PickRangeLim.Inst = backup.Clone() as PickRangeLim;
        }
    }
}
