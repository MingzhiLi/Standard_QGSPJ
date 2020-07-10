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
    /// UCComSet.xaml 的交互逻辑
    /// </summary>
    public partial class UCComSet : UserControl
    {
        public UCComSet()
        {
            InitializeComponent();
            DataContext = ParModelValue.Inst;
            backup = ParModelValue.Inst.Clone() as ParModelValue;
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            backup?.SaveBackUpFile();  //进行数据备份
            if (ParModelValue.Inst.SaveProductParToLocal())
            {
                backup = ParModelValue.Inst.Clone() as ParModelValue; //保存成功后以当前副本备份
                BtnSave.RefreshDefaultColor("保存成功", true);
            }
            else
            {
                BtnSave.RefreshDefaultColor("保存失败", false);
            }
        }

        private ParModelValue backup;

        private void UserControl_Unloaded(object sender, RoutedEventArgs e)
        {
            ParModelValue.Inst = backup.Clone() as ParModelValue;
        }
    }
}
