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
    /// WinSysSet.xaml 的交互逻辑
    /// </summary>
    public partial class WinSysSet : Window
    {
        public WinSysSet()
        {
            InitializeComponent();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            g_WinSysSet = null;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

        #region 窗体单实例
        private static WinSysSet g_WinSysSet = null;
        public static WinSysSet Inst
        {
            get
            {
                if(g_WinSysSet == null)
                {
                    g_WinSysSet = new WinSysSet();
                }
                return g_WinSysSet;
            }
        }
        #endregion 窗体单实例
    }
}
