using System;
using System.Collections.Generic;
using System.Globalization;
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
    /// UCPlatConfig.xaml 的交互逻辑
    /// </summary>
    public partial class UCPlatConfig : UserControl
    {
        public UCPlatConfig()
        {
            InitializeComponent();
            DataContext = PlatConfig.Inst;
            backup = PlatConfig.Inst.Clone() as PlatConfig;
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            backup?.SaveBackUpFile();
            if(PlatConfig.Inst.SaveConfigToLocal())
            {
                BtnSave.RefreshDefaultColor("保存成功", true);
                backup = PlatConfig.Inst.Clone() as PlatConfig;   //保存成功后重新备份当前参数
            }
            else
            {
                BtnSave.RefreshDefaultColor("保存失败", false);
            }
        }

        private PlatConfig backup;
    }



}
