using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DealResult;
using BasicClass;
using Common;
using System.IO;
namespace Main
{
    /// <summary>
    /// 单次标定的结果类
    /// </summary>
    public class AutoCalibResult
    {
        public const string NameClass = "AutoCalibResult";

        /// <summary>
        /// 用于存储每次标定结果的静态类实例1
        /// </summary>
        public static List<AutoCalibResult> AutoCalibResult1_L = new List<AutoCalibResult>();
        /// <summary>
        /// 用于存储每次标定结果的静态类实例2
        /// </summary>
        public static List<AutoCalibResult> AutoCalibResult2_L = new List<AutoCalibResult>();

        /// <summary>
        /// 用于精定位补偿的下一次标定补偿值1，可能X方向也可能Y方向
        /// </summary>
        public static double Residue1NextMove
        {
            get
            {
                return AutoCalibResult1_L?.Count > 0 ? AutoCalibResult1_L[AutoCalibResult1_L.Count - 1].NextMoveDis : 0;
            }
        }

        /// <summary>
        /// 用于精定位补偿的下一次补偿值2，可能X方向也可能Y方向
        /// </summary>
        public static double Residue2NextMove
        {
            get
            {
                return AutoCalibResult2_L?.Count > 0 ? AutoCalibResult2_L[AutoCalibResult2_L.Count - 1].NextMoveDis : 0;
            }
        }

        /// <summary>
        /// 标定过程保存根目录
        /// </summary>
        public static string StrPathSave
        {
            get
            {
                String path = ComValue.c_PathCustom + "标定过程统计\\";
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                return path;
            }
        }

        #region 构造函数
        public AutoCalibResult()
        {

        }
        public AutoCalibResult(int index)
        {
            CurIndex = index;
            E_CurCalibResult = CalibResult_Enum.OK;
            CurCFSharpness = index;
            CurMoveDis = index;
            CurTFTSharpness = index;

            E_NextDiretion = MoveDirection_Enum.Negative;
            NextMoveDis = index;
            NextStep = index;
        }
        /// <summary>
        /// 复制构造函数
        /// </summary>
        /// <param name="sour">复制源</param>
        public AutoCalibResult(AutoCalibResult sour)
        {

        }
        #endregion 构造函数

        #region 当次标定结果

        /// <summary>
        /// 第几次标定
        /// </summary>
        public int CurIndex { get; set; }
        /// <summary>
        /// 当次拍照TFT清晰度
        /// </summary>
        public double CurTFTSharpness { get; set; }
        /// <summary>
        /// 当次拍照CF清晰度
        /// </summary>
        public double CurCFSharpness { get; set; }
        /// <summary>
        /// 当次拍照清晰度比例
        /// </summary>
        public double CurRatio
        {
            get
            {
                if (CurCFSharpness == 0 )
                {
                    return 0;
                }
                return Math.Round(CurTFTSharpness / CurCFSharpness, 3);
            }
        }
        /// <summary>
        /// 当前移动的总距离
        /// </summary>
        public double CurMoveDis { get; set; }

        /// <summary>
        /// 下一次拍照时移动的总距离
        /// </summary>
        public double NextMoveDis { get; set; }

        /// <summary>
        /// 下一次拍照移动的步长
        /// </summary>
        public double NextStep { get; set; }

        /// <summary>
        /// 下一次拍照的轴移动方向
        /// </summary>
        public MoveDirection_Enum E_NextDiretion { get; set; }

        /// <summary>
        /// 当次标定写入寄存器（或者机器人）的结果
        /// </summary>
        public CalibResult_Enum E_CurCalibResult { get; set; }
        #endregion 当次标定结果

        #region 之前的标定结果
        /// <summary>
        /// 上一次的标定结果
        /// </summary>
        [field: NonSerialized]
        public AutoCalibResult LastResult;
        /// <summary>
        /// 上上次的标定结果
        /// </summary>
        [field: NonSerialized]
        public AutoCalibResult EarlierResult;
        #endregion 之前标定的结果

        #region 清晰度变化判断
        /// <summary>
        /// 判断当次标定结果，TRT清晰度大于设置的最小清晰度同时比例但与设置的最小比例 
        /// </summary>
        /// <param name="par"></param>
        /// <returns></returns>
        public bool JudgeResult(ResidueInspAutoCalibPar par)
        {
            return CurTFTSharpness > par.MinTFTSharpness &&
                   CurRatio > par.MinRatio;
        }

        /// <summary>
        /// 判断清晰度及比例是否连续下降
        /// </summary>
        public bool BlContinuousDecline
        {
            get
            {
                return CurRatio < LastResult.CurRatio && CurTFTSharpness < LastResult.CurTFTSharpness &&
                       LastResult.CurRatio < EarlierResult.CurRatio && LastResult.CurTFTSharpness < EarlierResult.CurTFTSharpness;
            }
        }

        /// <summary>
        /// 判断清晰度及比例是否连续上升
        /// </summary>
        public bool BlContinuousRise
        {
            get
            {
                return CurRatio > LastResult.CurRatio && CurTFTSharpness > LastResult.CurTFTSharpness &&
                       LastResult.CurRatio > EarlierResult.CurRatio && LastResult.CurTFTSharpness > EarlierResult.CurTFTSharpness;
            }
        }

        #endregion 清晰度变化判断

        /// <summary>
        /// 获取清晰度拍照结果
        /// </summary>
        /// <param name="index">当次拍照索引</param>
        /// <param name="resultSharpness">清晰度算子结果</param>
        public void SetResult(int index, ResultSharpness resultSharpness)
        {
            try
            {
                CurIndex = index;
                CurTFTSharpness = resultSharpness.Deviation_L[0];
                CurCFSharpness = resultSharpness.Deviation_L[1];
            }
            catch(Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }

        }

        /// <summary>
        /// 获取检测结果
        /// </summary>
        /// <param name="par">自动校准的参数</param>
        /// <param name="result_L">存储历史结果的List</param>
        public void GetResult(ResidueInspAutoCalibPar par, List<AutoCalibResult> result_L, StrAction ShowState)
        {
            ///校准第一次拍照
            if(CurIndex ==1)
            {
                E_NextDiretion = par.E_StartDirection;
                NextStep = par.StartStep * (int)par.E_StartDirection;
                CurMoveDis = 0;
                NextMoveDis = NextStep;
                E_CurCalibResult = CalibResult_Enum.CONTINUE;
                result_L.Add(this);
                return;
            }
            ///获取上一次的校准结果
            LastResult = result_L[CurIndex - 2];
            ///相同方向移动两次或以上时获取上上次的标定结果
            if (Math.Abs(CurMoveDis / LastResult.NextStep) > 1)
            {
                EarlierResult = result_L[CurIndex - 3];
            }
            CurMoveDis = LastResult.NextMoveDis;

            if(JudgeResult(par))   ///判断当前结果是否达到要求
            {
                E_NextDiretion = LastResult.E_NextDiretion;
                NextStep = LastResult.NextStep;
                NextMoveDis = LastResult.CurMoveDis;
                E_CurCalibResult = CalibResult_Enum.OK;
            }
            else   ///未达到要求则继续移动拍照
            {
                if (par.E_CalibAlgorithm == CalibAlgorithm_Enum.Traversing)
                {
                    GetTraversingResult(par, ShowState);
                }
                else
                {
                    GetIterationResult(par);
                }
            }
            result_L.Add(this);
        }

        /// <summary>
        /// 获取以遍历算法计算得到的结果
        /// </summary>
        /// <param name="par">校准参数</param>
        /// <param name="lastResult">上一次校准的结果记录</param>
        public void GetTraversingResult(ResidueInspAutoCalibPar par, StrAction ShowState)
        {    
            if(CurIndex > par.MaxCalibTimes)
            {
                ShowState("已到达最大校准次数，校准NG结束！");
                E_NextDiretion = LastResult.E_NextDiretion;
                NextStep = LastResult.NextStep;
                NextMoveDis = LastResult.CurMoveDis;
                E_CurCalibResult = CalibResult_Enum.NG;
                return;
            }
            if(Math.Abs(CurMoveDis) < par.MaxMoveDis)///当前移动距离小于最大移动距离则继续向此方向移动
            {
                if (EarlierResult != null && BlContinuousDecline && 
                    LastResult.E_NextDiretion == par.E_StartDirection) //向初始方向移动时连续清晰度下降   
                {
                    E_NextDiretion = (MoveDirection_Enum)(-(int)LastResult.E_NextDiretion);
                    ShowState("此方向清晰度连续下降，开始反方向继续校准：" + E_NextDiretion.ToString());
                    NextStep = LastResult.NextStep;
                    NextMoveDis = NextStep * (int)E_NextDiretion;
                    E_CurCalibResult = CalibResult_Enum.CONTINUE;
                    return;
                }
                ShowState("继续上一次的移动方向：" + LastResult.E_NextDiretion.ToString());
                E_NextDiretion = LastResult.E_NextDiretion;
                NextStep = LastResult.NextStep;
                NextMoveDis = CurMoveDis + NextStep * (int)E_NextDiretion;
                E_CurCalibResult = CalibResult_Enum.CONTINUE;
            }
            else  //当前移动距离不小于最大移动距离则向另一个方向移动或者结束校准
            {
                if(LastResult.E_NextDiretion == par.E_StartDirection)//本次移动方向与初始移动方向相同，则向反向继续校准
                {
                    E_NextDiretion = (MoveDirection_Enum)(-(int)LastResult.E_NextDiretion);
                    ShowState("此方向已达到最大移动距离，开始反方向继续校准：" + E_NextDiretion.ToString());
                    NextStep = LastResult.NextStep;
                    NextMoveDis = NextStep * (int)E_NextDiretion;
                    E_CurCalibResult = CalibResult_Enum.CONTINUE;
                }
                else  //当次移动方向与初始移动方向不同，结束校准
                {
                    E_NextDiretion = LastResult.E_NextDiretion;
                    NextStep = LastResult.NextStep;
                    NextMoveDis = LastResult.CurMoveDis;
                    E_CurCalibResult = CalibResult_Enum.NG;
                }
                    ShowState("已达到最大移动距离，校准NG结束");
            }
        }

        /// <summary>
        /// 获取迭代算法的校准结果
        /// </summary>
        /// <param name="par"></param>
        public void GetIterationResult(ResidueInspAutoCalibPar par)
        {

        }
    }

    public enum CalibResult_Enum
    {
        OK = 1,
        NG = 2,
        CONTINUE = 3,
    }

}
