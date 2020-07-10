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
    /// UCRoughlyResult.xaml 的交互逻辑
    /// </summary>
    public partial class UCRoughlyResult : UserControl
    {
        public UCRoughlyResult()
        {
            ReadData();
            InitializeComponent();
            //DataContext = RoughlyResult.RoughlyResult_L;
            dgResult.ItemsSource = data;
        }

        private List<RoughlyResult> data = new List<RoughlyResult>();
        /// <summary>
        /// 读取本地参数
        /// </summary>
        private void ReadData()
        {
            RoughlyResult.LoadNowFile(out data);
        }
    }
}
