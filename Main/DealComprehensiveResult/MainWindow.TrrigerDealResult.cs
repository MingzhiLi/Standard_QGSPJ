using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DealPLC;
using System.Threading;
using System.Threading.Tasks;
using DealComprehensive;
using Common;
using BasicClass;
using System.Collections;
using DealDisplay;
using BasicDisplay;
using Camera;
using Main_EX;

namespace Main
{
    /// <summary>
    /// 处理外部触发拍照的命令,并调用DealComprehensiveResult类中的处理方法，进行处理并输出结果
    /// </summary>
    public partial class MainWindow
    {
        #region 定义
        Hashtable g_HtUCDisplayImage = new Hashtable();

        #endregion 定义

        #region 初始化
        /// <summary>
        /// 初始化控件和参数
        /// </summary>
        public override void Init_TrrigerDealResult()
        {
            try
            {
                BaseDealComprehensiveResult[] baseDealComprehensiveResults = new BaseDealComprehensiveResult[8] { 
                    DealComprehensiveResult1.D_I,
                    DealComprehensiveResult2.D_I,
                    DealComprehensiveResult3.D_I,
                    DealComprehensiveResult4.D_I,
                    DealComprehensiveResult5.D_I,
                    DealComprehensiveResult6.D_I,
                    DealComprehensiveResult7.D_I,
                    DealComprehensiveResult8.D_I};

                //初始化
                base.Init_TrrigerDealResult(baseDealComprehensiveResults);
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }
        #endregion 初始化

    }
}
