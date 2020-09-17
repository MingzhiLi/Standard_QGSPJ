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
using System.Reflection;
namespace ProjectSPJ
{
    /// <summary>
    /// UCTable.xaml 的交互逻辑
    /// </summary>
    public partial class UCPreciseResult : UserControl
    {
        public UCPreciseResult()
        {
            ReadData();
            InitializeComponent();
            dgResult.ItemsSource = data;
            ucLogViewer.LoadEverHourLog_Event += LoadEverHourLog;
            ucLogViewer.Refresh_Event += Refresh;
            ucLogViewer.LoadAllDayLog_Event += LoadAllDayLog;
        }
        private List<PreciseResult> data = new List<PreciseResult>();
        /// <summary>
        /// 读取本地参数
        /// </summary>
        private void ReadData()
        {
            PreciseResult.LoadNowFile(out data);
        }

        /// <summary>
        /// 加载指定小时前的参数
        /// </summary>
        /// <param name="hours"></param>
        public void LoadEverHourLog(int hours)
        {
            PreciseResult.LoadEverFile(hours, out data);
            dgResult.ItemsSource = data;
            dgResult.Items.Refresh();
        }

        /// <summary>
        /// 加载当日所有文件
        /// </summary>
        /// <param name="dateTime">指定的日期</param>
        public void LoadAllDayLog(string dateTime)
        {
            PreciseResult.LoadAllDayFile(DateTime.Parse(dateTime), out data);
            dgResult.ItemsSource = data;
            dgResult.Items.Refresh();
        }

        /// <summary>
        /// 刷新到当前时间的日志
        /// </summary>
        public void Refresh()
        {
            PreciseResult.LoadNowFile(out data);
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
