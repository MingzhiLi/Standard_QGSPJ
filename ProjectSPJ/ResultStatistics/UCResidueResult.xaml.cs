using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
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
    /// UCResidueResult.xaml 的交互逻辑
    /// </summary>
    public partial class UCResidueResult : UserControl
    {
        private ResidueEnum _residueEnum = ResidueEnum.残材1;
        private List<ResidueResult> _data_L = new List<ResidueResult>();

        public UCResidueResult()
        {
            InitializeComponent();
            ucLogViewer.LoadEverHourLog_Event += LoadEverHourLog;
            ucLogViewer.Refresh_Event += Refresh;
            ucLogViewer.LoadAllDayLog_Event += LoadAllDayLog;
        }


        /// <summary>
        /// 读取本地参数
        /// </summary>
        private void ReadData()
        {
            ResidueResult.LoadNowFile(_residueEnum, out _data_L);
        }

        /// <summary>
        /// 加载指定小时前的参数
        /// </summary>
        /// <param name="hours"></param>
        public void LoadEverHourLog(int hours)
        {
            ResidueResult.LoadEverFile(hours, _residueEnum, out _data_L);
            dgResult.ItemsSource = _data_L;
            dgResult.Items.Refresh();
        }

        /// <summary>
        /// 加载当日所有文件
        /// </summary>
        /// <param name="dateTime">指定的日期</param>
        public void LoadAllDayLog(string dateTime)
        {
            ResidueResult.LoadAllDayFile(_residueEnum, DateTime.Parse(dateTime), out _data_L);
            dgResult.ItemsSource = _data_L;
            dgResult.Items.Refresh();
        }

        /// <summary>
        /// 刷新到当前时间的日志
        /// </summary>
        public void Refresh()
        {
            ResidueResult.LoadNowFile(_residueEnum, out _data_L);
            dgResult.ItemsSource = _data_L;
            dgResult.Items.Refresh();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            string tag = (string)this.Tag;
            if (Enum.IsDefined(typeof(ResidueEnum), this.Tag))
            {
                _residueEnum = (ResidueEnum)Enum.Parse(typeof(ResidueEnum), tag, true);
            }
            ReadData();
            dgResult.ItemsSource = _data_L;
        }


        private void dgResult_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            if(dgResult.SelectedIndex < 0)
            {
                return;
            }
            int numImage = (int)_data_L[dgResult.SelectedIndex].ScreenImage?.Count;

            if(numImage> 0)
            {
                picturePos1.LoadImage(_data_L[dgResult.SelectedIndex].ScreenImage[0]);
            }
            if (numImage > 1)
            {
                picturePos2.LoadImage(_data_L[dgResult.SelectedIndex].ScreenImage[1]);
            }
        }
    }
}
