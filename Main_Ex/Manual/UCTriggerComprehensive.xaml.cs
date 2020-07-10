using BasicClass;
using DealLog;
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

namespace Main_EX
{
    /// <summary>
    /// UCTriggerComprehensive.xaml 的交互逻辑
    /// </summary>
    public partial class UCTriggerComprehensive : UserControl
    {
        const string NameClass = "WinTrrigerComprehensive";
        public UCTriggerComprehensive()
        {
            InitializeComponent();
        }


        public void Init(BaseDealComprehensiveResult[] baseDealComprehensiveResult)
        {
            try
            {
                baseUCTrrigerComprehensive1.Init(baseDealComprehensiveResult[0]);
                baseUCTrrigerComprehensive2.Init(baseDealComprehensiveResult[1]);
                baseUCTrrigerComprehensive3.Init(baseDealComprehensiveResult[2]);
                baseUCTrrigerComprehensive4.Init(baseDealComprehensiveResult[3]);
                baseUCTrrigerComprehensive5.Init(baseDealComprehensiveResult[4]);
                baseUCTrrigerComprehensive6.Init(baseDealComprehensiveResult[5]);
                baseUCTrrigerComprehensive7.Init(baseDealComprehensiveResult[6]);
                baseUCTrrigerComprehensive8.Init(baseDealComprehensiveResult[7]);
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }

        private void UserControl_Unloaded(object sender, RoutedEventArgs e)
        {
            try
            {
                if (
                       baseUCTrrigerComprehensive1.Close()
                    && baseUCTrrigerComprehensive2.Close()
                    && baseUCTrrigerComprehensive3.Close()
                    && baseUCTrrigerComprehensive4.Close()
                    && baseUCTrrigerComprehensive5.Close()
                    && baseUCTrrigerComprehensive6.Close()
                    && baseUCTrrigerComprehensive7.Close()
                    && baseUCTrrigerComprehensive8.Close()
                    )
                {
                }
                else
                {
                }

            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }

        }
    }
}
