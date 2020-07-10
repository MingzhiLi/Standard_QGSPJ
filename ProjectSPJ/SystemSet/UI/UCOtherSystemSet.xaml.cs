using DealLog;
using SetPar;
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
    /// UCOtherSystemSet.xaml 的交互逻辑
    /// </summary>
    public partial class UCOtherSystemSet : UserControl
    {
        public UCOtherSystemSet()
        {
            InitializeComponent();
        }

        private void BtnLoginSet_Click(object sender, RoutedEventArgs e)
        {

            WinSetLogin winSetLogin = new WinSetLogin();
            winSetLogin.ShowDialog();
        }

        private void BtnMonitorSpace_Click(object sender, RoutedEventArgs e)
        {
            WinHardDisk winHardDisk = new WinHardDisk();
            winHardDisk.ShowDialog();
        }

        private void BtnFolderSet_Click(object sender, RoutedEventArgs e)
        {
            WinSetFolder winSetFolder = new WinSetFolder();
            winSetFolder.ShowDialog();
        }

        private void BtnLogSet_Click(object sender, RoutedEventArgs e)
        {
            WinSetLog win = new WinSetLog();
            win.ShowDialog();
        }

    }
}
