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
    /// UCCameraConfig.xaml 的交互逻辑
    /// </summary>
    public partial class UCCameraConfig : UserControl
    {
        public UCCameraConfig()
        {
            InitializeComponent();
            CameraConfig.Init();
            ucCamera1.DataContext = CameraConfig.Camera1;
            ucCamera2.DataContext = CameraConfig.Camera2;
            ucCamera3.DataContext = CameraConfig.Camera3;
            ucCamera4.DataContext = CameraConfig.Camera4;
            ucCamera5.DataContext = CameraConfig.Camera5;
            ucCamera6.DataContext = CameraConfig.Camera6;
            ucCamera7.DataContext = CameraConfig.Camera7;
            ucCamera8.DataContext = CameraConfig.Camera8;
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            CameraConfig.Save();
            this.BtnSave.RefreshDefaultColor("保存成功", true);
        }
    }
}
