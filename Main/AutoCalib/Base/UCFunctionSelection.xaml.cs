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
    /// UCFunctionSelection.xaml 的交互逻辑
    /// </summary>
    public partial class UCFunctionSelection : UserControl
    {
        public UCFunctionSelection()
        {
            InitializeComponent();
            DataContext = ParFunctionSelection.P_I;
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            ParFunctionSelection.P_I.SaveToLocal("FunctionSelectionPar.xml");
            this.btnSave.RefreshDefaultColor("保存成功", true);
        }
    }
}
