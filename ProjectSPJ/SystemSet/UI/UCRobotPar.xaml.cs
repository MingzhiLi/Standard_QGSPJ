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
using ControlLib;
namespace ProjectSPJ
{
    /// <summary>
    /// UCRobotPos.xaml 的交互逻辑
    /// </summary>
    public partial class UCRobotPar : UserControl
    {
        private RobotPar _backup;
        public UCRobotPar()
        {
            InitializeComponent();
            DataContext = RobotPar.Inst;
            _backup = RobotPar.Inst.Clone() as RobotPar;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            _backup.SaveBackUpFile();
            if (RobotPar.Inst.SaveConfigToLocal())
            {
                _backup = RobotPar.Inst.Clone() as RobotPar;
                BtnSave.RefreshDefaultColor("保存成功", true);
            }
            else
            {
                BtnSave.RefreshDefaultColor("保存失败", false);
            }
        }

        private void UserControl_Unloaded(object sender, RoutedEventArgs e)
        {
            RobotPar.Inst = _backup.Clone() as RobotPar;
        }
    }
}
