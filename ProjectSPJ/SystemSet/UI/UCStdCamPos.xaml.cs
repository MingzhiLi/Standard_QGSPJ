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
    /// UCStdCamPos.xaml 的交互逻辑
    /// </summary>
    public partial class UCStdCam : UserControl
    {
        private StdCamPos _backup;
        public UCStdCam()
        {
            InitializeComponent();
            DataContext = StdCamPos.Inst;
            _backup = StdCamPos.Inst.Clone() as StdCamPos;
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            _backup.SaveBackUpFile();
            if (StdCamPos.Inst.SaveConfigToLocal())
            {
                _backup = StdCamPos.Inst.Clone() as StdCamPos;
                BtnSave.RefreshDefaultColor("保存成功", true);
            }
            else
            {
                BtnSave.RefreshDefaultColor("保存失败", false);
            }
        }

        private void UserControl_Unloaded(object sender, RoutedEventArgs e)
        {
            StdCamPos.Inst = _backup.Clone() as StdCamPos;
        }
    }
}
