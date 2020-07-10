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
using Camera;

namespace ProjectSPJ
{
    /// <summary>
    /// UCCameraFun.xaml 的交互逻辑
    /// </summary>
    public partial class UCCameraSetBase : UserControl
    {
        public UCCameraSetBase()
        {
            InitializeComponent();

        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            ParCamera1.P_I.WriteIni();
            ParCamera2.P_I.WriteIni();
            ParCamera3.P_I.WriteIni();
            ParCamera4.P_I.WriteIni();
            ParCamera5.P_I.WriteIni();
            ParCamera6.P_I.WriteIni();
            ParCamera7.P_I.WriteIni();
            ParCamera8.P_I.WriteIni();
        }
    }
}
