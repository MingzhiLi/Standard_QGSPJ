using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
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
using BasicClass;
using Microsoft.Win32;

namespace ProjectSPJ
{
    /// <summary>
    /// Interaction logic for UCLogViewer.xaml
    /// </summary>
    public partial class UCLogViewer : UserControl
    {
        #region Event
        /// <summary>
        /// 加载指定小时前的日志事件
        /// </summary>
        public IntAction LoadEverHourLog_Event;
        /// <summary>
        /// 刷新到当前时间的日志事件
        /// </summary>
        public Action Refresh_Event;
        /// <summary>
        /// 加载当日所有记录事件
        /// </summary>
        public StrAction LoadAllDayLog_Event;
        /// <summary>
        /// 加载指定时间的日志
        /// </summary>
        public StrAction LoadDefineTimeLog_Event;
        /// <summary>
        /// 导出Excel事件
        /// </summary>
        public Action ImportExcel_Event;
        /// <summary>
        /// 加载图片
        /// </summary>
        public StrAction LoadImage_event;
        #endregion event

        public UCLogViewer()
        {
            InitializeComponent();
            cbxDate.ItemsSource = BaseCalResult.DateForBinding_L;
            cbxHour.ItemsSource = BaseCalResult.HourForBinding_L;
        }
        /// <summary>
        /// 指定几小时
        /// </summary>
        private int _everHours = 0;
        private void btnPreHour_Click(object sender, RoutedEventArgs e)
        {
            LoadEverHourLog_Event?.Invoke(--_everHours);
            
        }

        private void btnNextHour_Click(object sender, RoutedEventArgs e)
        {
            LoadEverHourLog_Event?.Invoke(++_everHours);
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            _everHours = 0;
            Refresh_Event?.Invoke();
        }

        private void cbxDate_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void cbxHour_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(IsMouseOver)
            {
                DateTime dt = DateTime.ParseExact((string)cbxDate.SelectedItem + "-" + (string)cbxHour.SelectedItem,
                                             "yyyy-MM-dd-HH",
                                             CultureInfo.CurrentCulture);
                _everHours = (int)dt.Subtract(DateTime.Now).TotalHours;
                LoadEverHourLog_Event?.Invoke(_everHours);
            }
        }

        private void btnImportExcel_Click(object sender, RoutedEventArgs e)
        {
            ImportExcel_Event?.Invoke();
        }

        private void btnLoadAllDay_Click(object sender, RoutedEventArgs e)
        {
            LoadAllDayLog_Event?.Invoke((string)cbxDate.SelectedItem);
        }
    }
}

