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
    /// WinProductPar.xaml 的交互逻辑
    /// </summary>
    public partial class WinProductPar : Window
    {
        public WinProductPar()
        {
            InitializeComponent();
        }
        private static WinProductPar g_WinProductPar = null;
        public static WinProductPar Inst
        {
            get
            {
                if (g_WinProductPar == null)
                {
                    g_WinProductPar = new WinProductPar();
                }
                return g_WinProductPar;
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            g_WinProductPar = null;
        }
    }
}
