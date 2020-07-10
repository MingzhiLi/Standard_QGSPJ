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
using BasicClass;

namespace Main
{
    public delegate void  ShowCalibResult_del(List<AutoCalibResult> autoCalibResult_L);
    /// <summary>
    /// UCResultInfo.xaml 的交互逻辑
    /// </summary>
    public partial class UCResultInfo : UserControl
    {
        public UCResultInfo()
        {
            InitializeComponent();
        }
        public event ShowCalibResult_del ShowLocalFile_event;
        public event StrAction ChangeShow_event;
        private void btnHistory_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.OpenFileDialog histoyFile = new System.Windows.Forms.OpenFileDialog();
            histoyFile.InitialDirectory = AutoCalibResult.StrPathSave;
            histoyFile.Filter = "XML files (*.xml)|*.xml";
            if (histoyFile.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                List<AutoCalibResult>  localData = BaseSerializer.ReadLocalFile<List<AutoCalibResult>>(histoyFile.FileName);
                ShowLocalFile_event(localData);
            }
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(ChangeShow_event == null)
            {
                return;
            }
            if(cbxType.SelectedIndex == 0)
            {
                ChangeShow_event("次数");
            }
            else
            {
                ChangeShow_event("距离");
            }
        }
    }
}
