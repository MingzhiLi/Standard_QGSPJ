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
using System.Reflection;
namespace ProjectSPJ
{
    /// <summary>
    /// UCTable.xaml 的交互逻辑
    /// </summary>
    public partial class UCPreciseResult : UserControl
    {
        public UCPreciseResult()
        {
            ReadData();
            InitializeComponent();
            dgResult.ItemsSource = data;
        }
        private List<PreciseResult> data = new List<PreciseResult>();
        /// <summary>
        /// 读取本地参数
        /// </summary>
        private void ReadData()
        {
            PreciseResult.LoadNowFile(out data);
        }

    }
}
