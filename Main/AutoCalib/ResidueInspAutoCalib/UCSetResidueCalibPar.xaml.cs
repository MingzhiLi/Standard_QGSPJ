using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using BasicClass;

namespace Main
{
    /// <summary>
    /// UCSetResidueCalibPar.xaml 的交互逻辑
    /// </summary>
    public partial class UCSetResidueCalibPar : UserControl
    {
        public UCSetResidueCalibPar()
        {
            InitializeComponent();
        }
        public event StrAction SavePar_event;

        public string StrFileName = string.Empty;
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if(ResidueInspAutoCalibPar.R_I1.E_MoveAxis == ResidueInspAutoCalibPar.R_I2.E_MoveAxis)
            {
                WinMsgBox.ShowMsg("残材1与残材2对应轴均为：" + ResidueInspAutoCalibPar.R_I2.E_MoveAxis.ToString()
                                + "!!!\n请确认后重新选择！！！");
                //MessageBox.Show("残材1与残材2对应轴均为：" + ResidueInspAutoCalibPar.R_I2.E_MoveAxis.ToString()
                //                + "!!!\n请确认后重新选择！！！");
                this.btnSave.RefreshDefaultColor("保存失败", false);
            }
            else
            {
                SavePar_event(StrFileName);
                this.btnSave.RefreshDefaultColor("保存成功", true);
            }
        }
    }
}
