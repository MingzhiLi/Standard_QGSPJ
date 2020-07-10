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
    /// UCPhotoDelay.xaml 的交互逻辑
    /// </summary>
    public partial class UCPhotoDelay : UserControl
    {
        private PhotoDelay backup;
        public UCPhotoDelay()
        {
            InitializeComponent();
            DataContext = PhotoDelay.Inst;
            backup = PhotoDelay.Inst.Clone() as PhotoDelay;
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            backup.SaveBackUpFile();
            if (PhotoDelay.Inst.SaveConfigToLocal())
            {
                BtnSave.RefreshDefaultColor("保存成功", true);
                backup = PhotoDelay.Inst.Clone() as PhotoDelay;
            }
            else
            {
                BtnSave.RefreshDefaultColor("保存失败", false);
            }
        }

        private void UserControl_Unloaded(object sender, RoutedEventArgs e)
        {
            PhotoDelay.Inst = backup.Clone() as PhotoDelay;
        }
    }
}
