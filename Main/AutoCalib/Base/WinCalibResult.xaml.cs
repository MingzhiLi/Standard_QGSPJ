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
using System.Windows.Shapes;

namespace Main
{
    /// <summary>
    /// WinCalibResult.xaml 的交互逻辑
    /// </summary>
    public partial class WinCalibResult : Window
    {
        public WinCalibResult()
        {
            InitializeComponent();
        }

        private static WinCalibResult g_WinCalibResult = null;
        public static WinCalibResult GetWinInst
        {
            get
            {
                if (g_WinCalibResult == null)
                {
                    g_WinCalibResult = new WinCalibResult();
                }
                return g_WinCalibResult;
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            g_WinCalibResult = null;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ucResultResidue1.DataContext = AutoCalibResult.AutoCalibResult1_L;
            ucResultResidue1.SetIndexPlot(AutoCalibResult.AutoCalibResult1_L);
            ucResultResidue1.SetResultInfo(AutoCalibResult.AutoCalibResult1_L);
            ucResultResidue2.DataContext = AutoCalibResult.AutoCalibResult2_L;
            ucResultResidue2.SetIndexPlot(AutoCalibResult.AutoCalibResult2_L);
            ucResultResidue2.SetResultInfo(AutoCalibResult.AutoCalibResult2_L);

        }

    }
}
