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

namespace ProjectSPJ
{
    /// <summary>
    /// UCRoughlyResult.xaml 的交互逻辑
    /// </summary>
    public partial class UCRoughlyResult : UserControl
    {
        public UCRoughlyResult()
        {
            ReadData();
            InitializeComponent();
            //DataContext = RoughlyResult.RoughlyResult_L;
            dgResult.ItemsSource = data;
            ucLogViewer.LoadEverHourLog_Event += LoadEverHourLog;
            ucLogViewer.Refresh_Event += Refresh;
            ucLogViewer.LoadAllDayLog_Event += LoadAllDayLog;
        }

        private List<RoughlyResult> data = new List<RoughlyResult>();
        /// <summary>
        /// 读取本地参数
        /// </summary>
        private void ReadData()
        {
            RoughlyResult.LoadNowFile(out data);
        }

        /// <summary>
        /// 加载指定小时前的参数
        /// </summary>
        /// <param name="hours"></param>
        public void LoadEverHourLog(int hours)
        {
            RoughlyResult.LoadEverFile(hours, out data);
            dgResult.ItemsSource = data;
            dgResult.Items.Refresh();
        }

        /// <summary>
        /// 加载当日所有文件
        /// </summary>
        /// <param name="dateTime">指定的日期</param>
        public void LoadAllDayLog(string dateTime)
        {
            RoughlyResult.LoadAllDayFile(DateTime.Parse(dateTime), out data);
            dgResult.ItemsSource = data;
            dgResult.Items.Refresh();
        }

        public void LoadDefineTimeLog(string dateTime)
        {

        }
        /// <summary>
        /// 刷新到当前时间的日志
        /// </summary>
        public void Refresh()
        {
            RoughlyResult.LoadNowFile(out data);
            dgResult.ItemsSource = data;
            dgResult.Items.Refresh();
        }

        private void dgResult_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            if (dgResult.SelectedIndex < 0)
            {
                return;
            }
            int numImage = (int)data[dgResult.SelectedIndex].ScreenImage?.Count;

            if (numImage > 0)
            {
                picturePos1.LoadImage(data[dgResult.SelectedIndex].ScreenImage[0]);
            }
            if (numImage > 1)
            {
                picturePos2.LoadImage(data[dgResult.SelectedIndex].ScreenImage[1]);
            }
        }
    }
}
