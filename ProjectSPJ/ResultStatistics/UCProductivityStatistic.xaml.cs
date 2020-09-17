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
    /// Interaction logic for UCProductivityStatistic.xaml
    /// </summary>
    public partial class UCProductivityStatistic : UserControl
    {
        public UCProductivityStatistic()
        {
            InitializeComponent();
            wpCurent.DataContext = ProductivityStatistic.CurrentStatistict;
            dgHistory.ItemsSource = ProductivityStatistic.HistoryStatistic_L;
        }
    }
}
