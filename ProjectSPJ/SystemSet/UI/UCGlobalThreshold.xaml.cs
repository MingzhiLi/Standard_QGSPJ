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
    /// UCGlobalThreshold.xaml 的交互逻辑
    /// </summary>
    public partial class UCGlobalThreshold : UserControl
    {
        private GlobalThreshold backup;
        public UCGlobalThreshold()
        {
            InitializeComponent();
            DataContext = GlobalThreshold.Inst;
            backup = GlobalThreshold.Inst.Clone() as GlobalThreshold;
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            backup.SaveBackUpFile();
            if (GlobalThreshold.Inst.SaveConfigToLocal())
            {
                BtnSave.RefreshDefaultColor("保存成功", true);
                backup = GlobalThreshold.Inst.Clone() as GlobalThreshold;
            }
            else
            {
                BtnSave.RefreshDefaultColor("保存失败", false);
            }
        }

        private void UserControl_Unloaded(object sender, RoutedEventArgs e)
        {
            GlobalThreshold.Inst = backup.Clone() as GlobalThreshold;
        }
    }
}
