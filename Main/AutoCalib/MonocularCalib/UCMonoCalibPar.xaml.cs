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

namespace Main
{
    /// <summary>
    /// UCMonoCalibPar.xaml 的交互逻辑
    /// </summary>
    public partial class UCMonoCalibPar : UserControl
    {
        public UCMonoCalibPar()
        {
            InitializeComponent();
            DataContext = MonocularAutoCalibPar.M_I;
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            MonocularAutoCalibPar.M_I.SaveToLocal("单目校准参数.xml");
            btnSave.RefreshDefaultColor("保存成功", true);
        }
    }
}
