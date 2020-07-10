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

namespace ProjectSPJ
{
    /// <summary>
    /// WinResul.xaml 的交互逻辑
    /// </summary>
    public partial class WinResultStatistics : Window
    {
        public WinResultStatistics()
        {
            InitializeComponent();
            ucResidueResult1.dgResult.ItemsSource = ResidueResult.ResidueResult1_L;
            ucResidueResult2.dgResult.ItemsSource = ResidueResult.ResidueResult2_L;
        }
        private static WinResultStatistics g_WinResult = null;
        public static WinResultStatistics Inst
        {
            get
            {
                if(g_WinResult == null)
                {
                    g_WinResult = new WinResultStatistics();
                }
                return g_WinResult;
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            g_WinResult = null;
        }
    }
}
