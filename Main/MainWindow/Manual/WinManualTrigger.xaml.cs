using BasicClass;
using DealConfigFile;
using Main_EX;
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
using System.Windows.Shapes;

namespace Main
{
    /// <summary>
    /// WinManualTrigger.xaml 的交互逻辑
    /// </summary>
    public partial class WinManualTrigger : Window
    {
        const string NameClass = "WinManualTrigger";
        public WinManualTrigger()
        {
            InitializeComponent();
        }

        private static WinManualTrigger g_WinManualTrigger = null;
        public static WinManualTrigger WinInst
        {
            get
            {
                if(g_WinManualTrigger == null)
                {
                    g_WinManualTrigger = new WinManualTrigger();
                    Init();
                }
                return g_WinManualTrigger;
            }
        }


        public static void Init()
        {
            try
            {
                if (ParCameraWork.NumCamera > 4)
                {
                        BaseDealComprehensiveResult[] baseDealComprehensiveResult = new BaseDealComprehensiveResult[8] {
                        DealComprehensiveResult1.D_I,
                        DealComprehensiveResult2.D_I ,
                        DealComprehensiveResult3.D_I,
                        DealComprehensiveResult4.D_I,
                        DealComprehensiveResult5.D_I,
                        DealComprehensiveResult6.D_I,
                        DealComprehensiveResult7.D_I,
                        DealComprehensiveResult8.D_I};
                        g_WinManualTrigger.ucTriggerCamera.Init(baseDealComprehensiveResult);
                }
                else
                {
                    BaseDealComprehensiveResult[] baseDealComprehensiveResult = new BaseDealComprehensiveResult[4] {
                        DealComprehensiveResult1.D_I,
                        DealComprehensiveResult2.D_I ,
                        DealComprehensiveResult3.D_I,
                        DealComprehensiveResult4.D_I,
                    };
                    g_WinManualTrigger.ucTriggerCamera.Init(baseDealComprehensiveResult);
                }
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            g_WinManualTrigger = null;
        }
    }
}
