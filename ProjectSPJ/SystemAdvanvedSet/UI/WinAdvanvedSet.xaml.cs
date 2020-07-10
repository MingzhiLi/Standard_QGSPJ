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
    /// WinBaseSet.xaml 的交互逻辑
    /// </summary>
    public partial class WinAdvancedSet : Window
    {
        public WinAdvancedSet()
        {
            InitializeComponent();
            ucParProduct.DataContext = ProductSet.Inst;
        }

        private static WinAdvancedSet g_WinBaseSet = null;
        public static WinAdvancedSet GetWinInst
        {
            get
            {
                if(g_WinBaseSet == null)
                {
                    g_WinBaseSet = new WinAdvancedSet();
                }
                return g_WinBaseSet;
            }
        }
        private void Window_Closed(object sender, EventArgs e)
        {
            g_WinBaseSet = null;
        }
    }
}
