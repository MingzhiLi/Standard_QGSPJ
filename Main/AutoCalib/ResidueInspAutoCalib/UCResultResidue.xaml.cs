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
    /// UCResultResidue.xaml 的交互逻辑
    /// </summary>
    public partial class UCResultResidue : UserControl
    {
        public UCResultResidue()
        {
            InitializeComponent();
            ucResultInfo.ShowLocalFile_event += new ShowCalibResult_del(SetIndexPlot);
            ucResultInfo.ShowLocalFile_event += new ShowCalibResult_del(RefreshTable);
            ucResultInfo.ChangeShow_event += new BasicClass.StrAction(ChangePlot);
        }
        public List<AutoCalibResult> CurShowPar_L = new List<AutoCalibResult>();
        public void SetResultInfo(List<AutoCalibResult> autoCalibResult_L)
        {
            if(autoCalibResult_L?.Count != 0)
            {
                ucResultInfo.DataContext = autoCalibResult_L[autoCalibResult_L.Count - 1];
            }
        }
        public void RefreshTable(List<AutoCalibResult> autoCalibResult_L)
        {
            ucResultTable.dgResult.ItemsSource = autoCalibResult_L;
            //ucResultTable.dgResult.
        }
        /// <summary>
        /// 设置折线图参数
        /// </summary>
        public void SetIndexPlot(List<AutoCalibResult> autoCalibResult_L)
        {
            double[] xIndex1 = new double[10];
            double[] yTFTSharpness1 = new double[10];
            double[] yCFSharpness1 = new double[10];
            double[] yRatio1 = new double[10];

            if (autoCalibResult_L?.Count != 0)
            {
                ucDealPlot.ClearPlot();
                GetIndexPar(autoCalibResult_L,
                            out xIndex1, out yTFTSharpness1, out yCFSharpness1, out yRatio1);
                List<PlotPar> plotPar = new List<PlotPar>();
                plotPar.Add(new PlotPar(xIndex1, yTFTSharpness1, Color.FromRgb(255, 0, 0), "TFT清晰度", "残材校准统计", "次数", "清晰度/比例"));
                plotPar.Add(new PlotPar(xIndex1, yCFSharpness1, Color.FromRgb(0, 255, 0), "CF清晰度", "残材校准统计", "次数", "清晰度/比例"));
                plotPar.Add(new PlotPar(xIndex1, yRatio1, Color.FromRgb(0, 0, 255), "比例", "残材校准统计", "次数", "清晰度/比例"));
                ucDealPlot.DrawPlotLine(plotPar);
                CurShowPar_L = autoCalibResult_L;
            }

        }

        public void ChangePlot(string strType)
        {
            double[] xIndex1 = new double[10];
            double[] yTFTSharpness1 = new double[10];
            double[] yCFSharpness1 = new double[10];
            double[] yRatio1 = new double[10];


            if (CurShowPar_L?.Count != 0)
            {
                ucDealPlot.ClearPlot();
                if (strType == "次数")
                {
                    GetIndexPar(CurShowPar_L,
                            out xIndex1, out yTFTSharpness1, out yCFSharpness1, out yRatio1);
                }
                else
                {
                    GetDisPar(CurShowPar_L,
                            out xIndex1, out yTFTSharpness1, out yCFSharpness1, out yRatio1);
                }
                List<PlotPar> plotPar = new List<PlotPar>();
                plotPar.Add(new PlotPar(xIndex1, yTFTSharpness1, Color.FromRgb(255, 0, 0), "TFT清晰度", "残材校准统计", strType, "清晰度/比例"));
                plotPar.Add(new PlotPar(xIndex1, yCFSharpness1, Color.FromRgb(0, 255, 0), "CF清晰度", "残材校准统计", strType, "清晰度/比例"));
                plotPar.Add(new PlotPar(xIndex1, yRatio1, Color.FromRgb(0, 0, 255), "比例", "残材校准统计", strType, "清晰度/比例"));
                ucDealPlot.DrawPlotLine(plotPar);
                
            }

        }

        #region 获取索引与清晰度
        /// <summary>
        /// 获取校准次数索引与清晰度
        /// </summary>
        /// <param name="result_L">存储校准结果的list</param>
        /// <param name="xIndex">索引号，X轴</param>
        /// <param name="yTFTSharpness">TFT清晰度，Y轴</param>
        /// <param name="yCFSharpness">CF清晰度，Y轴</param>
        /// <param name="yRatio">清晰度比例，Y轴</param>
        public void GetIndexPar(List<AutoCalibResult> result_L,
                                out double[] xIndex,
                                out double[] yTFTSharpness,
                                out double[] yCFSharpness,
                                out double[] yRatio)
        {
            int num = 1;
            num = result_L.Count;
            xIndex = new double[num];
            yTFTSharpness = new double[num];
            yCFSharpness = new double[num];
            yRatio = new double[num];
            if (result_L?.Count == 0)
            {
                return;
            }
            for (int i = 0; i < num; i++)
            {
                xIndex[i] = result_L[i].CurIndex;
                yTFTSharpness[i] = result_L[i].CurTFTSharpness;
                yCFSharpness[i] = result_L[i].CurCFSharpness;
                yRatio[i] = result_L[i].CurRatio;
                //yRatio.Append(result_L[i].CurRatio);
            }
        }
        #endregion 获取索引与清晰度

        #region 获取距离与清晰度
        /// <summary>
        /// 获取校准距离与清晰度
        /// </summary>
        /// <param name="result_L">存储校准结果的list</param>
        /// <param name="xDis">校准补正距离，X轴</param>
        /// <param name="yTFTSharpness">TFT清晰度，Y轴</param>
        /// <param name="yCFSharpness">CF清晰度，Y轴</param>
        /// <param name="yRatio">清晰度比例，Y轴</param>
        public void GetDisPar(List<AutoCalibResult> result_L,
                                out double[] xDis,
                                out double[] yTFTSharpness,
                                out double[] yCFSharpness,
                                out double[] yRatio)
        {
            int num = 1;
            num = result_L.Count;
            xDis = new double[num];
            yTFTSharpness = new double[num];
            yCFSharpness = new double[num];
            yRatio = new double[num];
            if (result_L == null || result_L.Count == 0)
            {
                return;
            }
            for (int i = 0; i < num; i++)
            {
                xDis[i] = result_L[i].CurMoveDis;
                yTFTSharpness[i] = result_L[i].CurTFTSharpness;
                yCFSharpness[i] = result_L[i].CurCFSharpness;
                yRatio[i] = result_L[i].CurRatio;
                //yRatio.Append(result_L[i].CurRatio);
            }
        }
        #endregion 获取索引与清晰度
    }
}
