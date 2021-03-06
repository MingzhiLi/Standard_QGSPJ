﻿using System;
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
    /// UCParProduct.xaml 的交互逻辑
    /// </summary>
    public partial class UCProductReg : UserControl
    {
        private ProductSet backup;

        public UCProductReg()
        {
            InitializeComponent();
            backup = ProductSet.Inst.Clone() as ProductSet;
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            backup?.SaveBackUpFile();
            if (ProductSet.Inst.SaveConfigToLocal())
            {
                BtnSave.RefreshDefaultColor("保存成功", true);
                backup = ProductSet.Inst.Clone() as ProductSet;   //保存成功后重新备份当前参数
            }
            else
            {
                BtnSave.RefreshDefaultColor("保存失败", false);
            }

        }

    }
}
