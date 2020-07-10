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
using System.IO;
using Microsoft.Win32;

namespace ProjectSPJ
{
    /// <summary>
    /// UCFunctionSelect.xaml 的交互逻辑
    /// </summary>
    public partial class UCFunctionSelect : UserControl
    {
        /// <summary>
        /// 数据备份
        /// </summary>
        private FunctionSelect backup;
        public UCFunctionSelect()
        {
            InitializeComponent();
            DataContext = FunctionSelect.Inst;
            backup = FunctionSelect.Inst.Clone() as FunctionSelect;
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            backup?.SaveBackUpFile();  //进行数据备份
            if (FunctionSelect.Inst.SaveConfigToLocal())
            {
                backup = FunctionSelect.Inst.Clone() as FunctionSelect; //保存成功后以当前副本备份
                BtnSave.RefreshDefaultColor("保存成功", true);
            }
            else
            {
                BtnSave.RefreshDefaultColor("保存失败", false);
            }
        }

        private void btnHistory_Click(object sender, RoutedEventArgs e)
        {
        }

        private void UserControl_Unloaded(object sender, RoutedEventArgs e)
        {
            FunctionSelect.Inst = backup.Clone() as FunctionSelect;
        }
    }
}
