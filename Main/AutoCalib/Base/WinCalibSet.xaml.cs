using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    /// WinCalibSet.xaml 的交互逻辑
    /// </summary>
    public partial class WinCalibSet : Window
    {
        public WinCalibSet()
        {
            InitializeComponent();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ucSetResidueCalibPar1.DataContext = ResidueInspAutoCalibPar.R_I1;
            ucSetResidueCalibPar2.DataContext = ResidueInspAutoCalibPar.R_I2;
            ucSetResidueCalibPar1.StrFileName = "Par1.xml";
            ucSetResidueCalibPar2.StrFileName = "Par2.xml";
            ucSetResidueCalibPar1.SavePar_event += new BasicClass.StrAction(ResidueInspAutoCalibPar.R_I1.SaveToLocal);
            ucSetResidueCalibPar2.SavePar_event += new BasicClass.StrAction(ResidueInspAutoCalibPar.R_I2.SaveToLocal);
        }

        private static WinCalibSet g_WinCalibSet = null;
        public static WinCalibSet GetWinInst
        {
            get
            {
                if(g_WinCalibSet == null)
                {
                    g_WinCalibSet = new WinCalibSet();
                }
                return g_WinCalibSet;
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            g_WinCalibSet = null;
        }
    }
}
