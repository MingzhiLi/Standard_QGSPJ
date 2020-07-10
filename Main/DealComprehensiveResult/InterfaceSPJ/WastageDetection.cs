using BasicClass;
using DealComprehensive;
using DealPLC;
using DealResult;
using Main_EX;
using System;
using System.Collections;
using System.Threading;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using Common;
using ProjectSPJ;
using DealLog;

namespace Main
{
    public partial class BaseDealComprehensiveResult_Main
    {
        /// <summary>
        /// 当前的校准的次数
        /// </summary>
        public int CalibTimes = 0;
        /// <summary>
        /// 当次校准计算完成
        /// </summary>
        public bool BlCurFinish = false;

        public AutoCalibResult CurResult;
        /// <summary>
        /// 当前相机校准结束
        /// </summary>
        public bool BlCalibFinish = false;
        /// <summary>
        /// 两个相机都已标定结束，手动测试用的标志位，控制及时停止
        /// </summary>
        public static bool BlAllFinish = false;

        #region 残材检测
        /// <summary>
        /// 处理残材检测
        /// </summary>
        /// <param name="cellName">算子序号</param>
        /// <param name="pos">表明用的第几次拍照的结果</param>
        /// <param name="htResult"></param>
        /// <returns></returns>
        public StateComprehensive_enum DealSharpness(string cellName, Pos_enum pos, ResidueResult residueResult, List<ResidueResult> residueResult_L, out Hashtable htResult)
        {
            htResult = null;
            try
            {
                #region 空跑
                if (ParStateSoft.StateMachine_e == StateMachine_enum.NullRun)
                {
                    ShowState(string.Format("相机{0}空跑默认无残边", g_NoCamera));
                    FinishPhotoPLC((int)CalibResult_Enum.OK);
                    return StateComprehensive_enum.True;
                }
                #endregion

                //获取算子计算结果
                StateComprehensive_enum StateComprehensive_e = g_BaseDealComprehensive.DealComprehensivePosNoDisplay(
                    g_UCDisplayCamera, g_HtUCDisplay, pos, out htResult);
                ResultSharpness result = (ResultSharpness)htResult[cellName];
                residueResult.PhotoPos = (int)pos;

                if(!ResidueCalculate.GetSharpness(result.Deviation_L, residueResult))
                {
                    FinishPhotoPLC(2);
                    ShowAlarm("残材相机" + g_NoCamera + "计算完成，结果NG");
                    SaveBitmapImage(false, "位置" + (int)pos);
                    g_UCDisplayCamera.ShowResult(residueResult.ResultForShow);
                    return StateComprehensive_enum.False;
                }
                if(ResidueCalculate.CalResidueResult(residueResult))
                {
                    FinishPhotoPLC(1);
                    ShowState("残材相机" + g_NoCamera + "计算完成，结果OK");
                    SaveBitmapImage(true, "位置" + (int)pos);
                    g_UCDisplayCamera.ShowResult(residueResult.ResultForShow);
                    return StateComprehensive_enum.True;
                }
                else
                {
                    if(FunctionSelect.Inst.IsSharpnessAlarm && !ResidueCalculate.CheckContinueNG(residueResult_L, 10))
                    {
                        ShowWinError("残材相机" + g_NoCamera + "连续低清晰度报警！");
                    }
                    FinishPhotoPLC(2);
                    g_UCDisplayCamera.ShowResult(residueResult.ResultForShow, false);
                    SaveBitmapImage(false, "位置" + (int)pos);
                    return StateComprehensive_enum.False;
                }

            }
            catch(Exception ex)
            {
                FinishPhotoPLC(2);
                Log.L_I.WriteError(NameClass, ex);
                SaveBitmapImage(false, "位置" + (int)pos);
                ShowWinError("残材相机" + g_NoCamera + "处理过程中发生异常");
                return StateComprehensive_enum.False;
            }
            finally
            {
                residueResult.AddToResultList(residueResult_L);
            }
        }
        #endregion

        #region 自动校准

        /// <summary>
        /// 残材自动校准
        /// </summary>
        /// <param name="cellName">清晰度算子单元格</param>
        /// <param name="pos">拍照位置</param>
        /// <param name="residueInspAutoCalibPar">自动校准参数</param>
        /// <param name="result_L">校准结果的list</param>
        /// <param name="htResult">算子结果哈希表</param>
        /// <returns></returns>
        public StateComprehensive_enum AutoCalibSharpness(string cellName, Pos_enum pos,
                                                          ResidueInspAutoCalibPar residueInspAutoCalibPar,
                                                          List<AutoCalibResult> result_L, out Hashtable htResult)
        {
            htResult = null;
            try
            {
                if (BlAllFinish)
                {
                    ShowState("已完成所有校准，不再继续拍照");
                    return StateComprehensive_enum.True;
                }
                ShowState("残材相机" + g_NoCamera + "触发标定拍照");
                //获取算子计算结果
                StateComprehensive_enum StateComprehensive_e = g_BaseDealComprehensive.DealComprehensivePosNoDisplay(
                    g_UCDisplayCamera, g_HtUCDisplay, pos, out htResult);
                ResultSharpness result = (ResultSharpness)htResult[cellName];

                CalibTimes++;

                ShowState(string.Format("相机{0}自校准，当前第{1}次.", g_NoCamera, CalibTimes));
                AutoCalibResult autoCalibResult = new AutoCalibResult();

                ///获取图像算法结果
                autoCalibResult.SetResult(CalibTimes, result);
                ///获取当次校准的结果
                autoCalibResult.GetResult(residueInspAutoCalibPar, result_L, ShowState);

                CurResult = autoCalibResult;
                ///当前相机当次拍照完成标志位
                BlCurFinish = true;
                ///结果处理
                DealResult(CurResult, result_L, residueInspAutoCalibPar);
                
                ///写入PLC校准结果
                WriteCalibPLCResult();

                return StateComprehensive_enum.True;                
            }
            catch(Exception ex)
            {
                ShowAlarm("相机" + g_NoCamera + "校准处理过程中发生异常");
                Log.L_I.WriteError(NameClass, ex);
                return StateComprehensive_enum.False;
            }
        }

        /// <summary>
        /// 结果处理
        /// </summary>
        /// <param name="autoCalibResult">当次校准的结果</param>
        /// <param name="result_L">存储历史校准结果的list</param>
        public void DealResult(AutoCalibResult autoCalibResult, List<AutoCalibResult> result_L,
                               ResidueInspAutoCalibPar residueInspAutoCalibPar)
        {
            if (autoCalibResult.E_CurCalibResult == CalibResult_Enum.NG)
            {
                BlCalibFinish = true;
                ShowAlarm("相机" + g_NoCamera + "校准NG结束");
                return;
            }
            if(autoCalibResult.E_CurCalibResult == CalibResult_Enum.CONTINUE)
            {
                return;
            }
            ///当前相机校准完成
            BlCalibFinish = true;

            ShowState("相机" + g_NoCamera + "校准已达到设定阈值，该相机校准结束");
            ShowState(residueInspAutoCalibPar.E_MoveAxis.ToString()
                      + ",校准补正为:" + CurResult.CurMoveDis);
            if (residueInspAutoCalibPar.E_MoveAxis == MoveAxis_Enum.X轴)
            {
                ModelParams.adjResidueCalibComX = CurResult.CurMoveDis;
            }
            else
            {
                ModelParams.adjResidueCalibComY = CurResult.CurMoveDis;
            }

            ///将校准过程存储XML
            BaseSerializer.SaveList(result_L, AutoCalibResult.StrPathSave
                                             + "相机" + g_NoCamera + "标定结果" +
                                             DateTime.Now.ToShortDateString() + ".xml");
            if (DealComprehensiveResult3.D_I.BlCalibFinish &&
                DealComprehensiveResult4.D_I.BlCalibFinish)
            {
                DealComprehensiveResult3.D_I.BlCalibFinish = false;
                DealComprehensiveResult4.D_I.BlCalibFinish = false;
                BlAllFinish = true;

                ///显示结果窗体
                System.Windows.Application.Current.Dispatcher.Invoke(new Action(() =>
                {
                    WinCalibResult.GetWinInst.Show();
                }));
            }
        }

        #endregion 自动校准

        #region 写入PLC校准结果
        /// <summary>
        /// 写入PLC残材校准结果
        /// </summary>
        public static void WriteCalibPLCResult()
        {
            if(!(DealComprehensiveResult3.D_I.BlCurFinish && DealComprehensiveResult4.D_I.BlCurFinish))
            {
                return;
            }
            DealComprehensiveResult3.D_I.BlCurFinish = false;
            DealComprehensiveResult4.D_I.BlCurFinish = false;
            if (DealComprehensiveResult3.D_I.CurResult?.E_CurCalibResult == CalibResult_Enum.NG ||
                DealComprehensiveResult4.D_I.CurResult?.E_CurCalibResult == CalibResult_Enum.NG)
            {
                ShowState("写入PLC标定NG信号");
                WriteResidueCalibResult(CameraResult.NG);
            }
            else if (DealComprehensiveResult3.D_I.CurResult?.E_CurCalibResult == CalibResult_Enum.CONTINUE ||
                     DealComprehensiveResult4.D_I.CurResult?.E_CurCalibResult == CalibResult_Enum.CONTINUE)
            {
                ShowState("写入PLC继续标定信号");
                WriteResidueCalibResult(CameraResult.CONTINUE);
            }
            else
            {
                ShowState("写入PLC标定OK信号");
                WriteResidueCalibResult(CameraResult.OK);
            }
        }

        #endregion 写入PLC校准结果

        #region 标志位复位
        /// <summary>
        /// 校准开始时复位残材检测的标志位
        /// </summary>
        public static void  ResetResidueCalibFlag()
        {

            ///残材1校准启用则将标志位置为false，不启用则置为true
            if(ParFunctionSelection.P_I.BlResidueInspCalib1)
            {
                ShowState("残材1校准开启");
                DealComprehensiveResult3.D_I.BlCalibFinish = false;
                DealComprehensiveResult3.D_I.BlCurFinish = false;
            }
            else
            {
                ShowState("残材1校准已屏蔽");
                DealComprehensiveResult3.D_I.BlCalibFinish = true;
                DealComprehensiveResult3.D_I.BlCurFinish = true;
            }

            if(ParFunctionSelection.P_I.BlResidueInspCalib2)
            {
                ShowState("残材2校准开启");
                DealComprehensiveResult4.D_I.BlCalibFinish = false;
                DealComprehensiveResult4.D_I.BlCurFinish = false;
            }
            else
            {
                ShowState("残材2校准已屏蔽");
                DealComprehensiveResult4.D_I.BlCalibFinish = true;
                DealComprehensiveResult4.D_I.BlCurFinish = true;
            }

            BlAllFinish = false;

            ///残材检测校准次数归0
            DealComprehensiveResult3.D_I.CalibTimes = 0;
            DealComprehensiveResult4.D_I.CalibTimes = 0;
            DealComprehensiveResult5.D_I.CalibTimes = 0;
            AutoCalibResult.AutoCalibResult1_L?.Clear();
            AutoCalibResult.AutoCalibResult2_L?.Clear();
        }
        #endregion 标志位复位
    }
}
