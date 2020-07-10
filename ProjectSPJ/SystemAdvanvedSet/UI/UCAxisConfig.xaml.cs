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
    /// UCAxisConfig.xaml 的交互逻辑
    /// </summary>
    public partial class UCAxisConfig : UserControl
    {
        private List<AxisConfigPar> _backup = new List<AxisConfigPar>();
        private void AddBackupFile()
        {
            _backup.Clear();
            foreach(AxisConfigPar axisConfigPar in AxisConfigPar.AxisConfigs_Arr)
            {
                _backup.Add(axisConfigPar.Clone() as AxisConfigPar);
            }
        }
        public UCAxisConfig()
        {
            InitializeComponent();
            ucAxisSet1.DataContext = AxisConfigPar.AxisConfigs_Arr[0];
            ucAxisSet2.DataContext = AxisConfigPar.AxisConfigs_Arr[1];
            ucAxisSet3.DataContext = AxisConfigPar.AxisConfigs_Arr[2];
            ucAxisSet4.DataContext = AxisConfigPar.AxisConfigs_Arr[3];
            ucAxisSet5.DataContext = AxisConfigPar.AxisConfigs_Arr[4];
            ucAxisSet6.DataContext = AxisConfigPar.AxisConfigs_Arr[5];
            AddBackupFile();
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            foreach (AxisConfigPar axisConfigPar in AxisConfigPar.AxisConfigs_Arr)
            {
                axisConfigPar.SaveConfigToLocal();
            }

            foreach (AxisConfigPar axisConfigPar in _backup)
            {
                axisConfigPar.SaveBackUpFile();
            }
            BtnSave.RefreshDefaultColor("保存成功", true);
        }
    }
}
