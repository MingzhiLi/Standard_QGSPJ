using BasicClass;
using DealComprehensive;
using System;
using System.Collections;
using System.Diagnostics;
using DealPLC;
using Main_EX;
using ProjectSPJ;
using System.Threading;

namespace Main
{
    public partial class DealComprehensiveResult4 : BaseDealComprehensiveResult_Main
    {

        #region 定义

        #endregion 定义

        #region 位置1拍照
        /// <summary>
        /// 
        /// </summary>
        /// <param name="htResult"></param>
        /// <returns></returns>
        public override StateComprehensive_enum DealComprehensiveResultFun1(TriggerSource_enum trigerSource_e, out Hashtable htResult)
        {
            #region 定义
            htResult = g_HtResult;
            //int pos = 1;
            bool blResult = true;//结果是否正确
            Stopwatch sw = new Stopwatch();
            sw.Restart();
            #endregion 定义
            try
            {
                Thread.Sleep(PhotoDelay.Inst.DelayResidue2);
                StateComprehensive_enum stateComprehensive_e = StateComprehensive_enum.True;
                if (!FunctionSelect.Inst.IsEnableResidue2)
                {
                    FinishPhotoPLC(1);
                    ShowState("残材2Pass");
                    g_UCDisplayCamera.ShowResult("残材2PASS");
                    return StateComprehensive_enum.True;
                }
                ResidueResult residueResult = new ResidueResult(ResidueEnum.残材2);
                residueResult.Threshold = ParModelValue.Inst.ThdResidue2;
                stateComprehensive_e = DealSharpness(CellResultPos1, Pos_enum.Pos1, 
                                                     residueResult,ResidueResult.ResidueResult2_L, 
                                                     out htResult);
                blResult = stateComprehensive_e == StateComprehensive_enum.True;
                return stateComprehensive_e;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
                return StateComprehensive_enum.False;
            }
            finally
            {
                #region 显示和日志记录

                Display(Pos_enum.Pos1, htResult, blResult, sw);

                #endregion 显示和日志记录 
            }
        }
        #endregion 位置1拍照

        #region 位置2拍照
        public override StateComprehensive_enum DealComprehensiveResultFun2(TriggerSource_enum trigerSource_e, out Hashtable htResult)
        {
            #region 定义
            htResult = g_HtResult;
            //int pos = 2;
            bool blResult = true;//结果是否正确
            Stopwatch sw = new Stopwatch();
            sw.Restart();
            #endregion 定义
            try
            {
                Thread.Sleep(PhotoDelay.Inst.DelayResidue2);
                StateComprehensive_enum stateComprehensive_e = StateComprehensive_enum.True;
                if (!FunctionSelect.Inst.IsEnableResidue2)
                {
                    FinishPhotoPLC(1);
                    ShowState("相机4Pass");
                    g_UCDisplayCamera.ShowResult("残材2PASS");
                    return StateComprehensive_enum.True;
                }

                ResidueResult residueResult = new ResidueResult(ResidueEnum.残材2);
                residueResult.Threshold = ParModelValue.Inst.ThdResidue2;
                stateComprehensive_e = DealSharpness(CellResultPos2, Pos_enum.Pos2,
                                                     residueResult, ResidueResult.ResidueResult2_L,
                                                     out htResult);
                blResult = stateComprehensive_e == StateComprehensive_enum.True;
                return stateComprehensive_e;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
                return StateComprehensive_enum.False;
            }
            finally
            {
                #region 显示和日志记录

                Display(Pos_enum.Pos2, htResult, blResult, sw);

                #endregion 显示和日志记录
            }
        }
        #endregion 位置2拍照

        #region 位置3拍照
        /// <summary>
        /// 
        /// </summary>
        /// <param name="htResult"></param>
        /// <returns></returns>
        public override StateComprehensive_enum DealComprehensiveResultFun3(TriggerSource_enum trigerSource_e, out Hashtable htResult)
        {
            #region 定义
            htResult = g_HtResult;
            //int pos = 3;
            bool blResult = true;//结果是否正确
            Stopwatch sw = new Stopwatch();
            sw.Restart();
            #endregion 定义
            try
            {
                if (BlCalibFinish)
                {
                    g_UCDisplayCamera.ShowResult(string.Format("相机{0}清晰度已达到要求\n不再继续拍照", g_NoCamera));
                    return StateComprehensive_enum.True;
                }
                StateComprehensive_enum stateComprehensive_e = StateComprehensive_enum.True;
                stateComprehensive_e = AutoCalibSharpness(CellResultPos1, Pos_enum.Pos1,
                                         ResidueInspAutoCalibPar.R_I2, AutoCalibResult.AutoCalibResult2_L, out htResult);
                return StateComprehensive_enum.True;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
                return StateComprehensive_enum.False;
            }
            finally
            {
                #region 显示和日志记录

                Display(Pos_enum.Pos1, htResult, blResult, sw);

                #endregion 显示和日志记录
            }
        }
        #endregion 位置3拍照

        #region 位置4拍照
        public override StateComprehensive_enum DealComprehensiveResultFun4(TriggerSource_enum trigerSource_e, out Hashtable htResult)
        {
            #region 定义
            htResult = g_HtResult;
            //int pos = 4;
            bool blResult = true;//结果是否正确
            Stopwatch sw = new Stopwatch();
            sw.Restart();
            #endregion 定义
            try
            {
                StateComprehensive_enum stateComprehensive_e = g_BaseDealComprehensive.DealComprehensivePosNoDisplay(g_UCDisplayCamera, g_HtUCDisplay, Pos_enum.Pos4, out htResult);

                return StateComprehensive_enum.True;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
                return StateComprehensive_enum.False;
            }
            finally
            {
                #region 显示和日志记录

                Display(Pos_enum.Pos4, htResult, blResult, sw);

                #endregion 显示和日志记录
            }
        }
        #endregion 位置4拍照


        #region 自定义校准
        public override StateComprehensive_enum DealComprehensiveResultFun_Calib1(TriggerSource_enum triggerSource_e, out Hashtable htResult)
        {
            #region 定义
            htResult = g_HtResult;
            //int pos = 4;
            bool blResult = true;//结果是否正确
            Stopwatch sw = new Stopwatch();
            sw.Restart();
            #endregion 定义

            try
            {
                if (!ParFunctionSelection.P_I.BlResidueInspCalib2)
                {
                    BlCalibFinish = true;
                    BlCurFinish = true;
                    ShowState("已屏蔽相机" + g_NoCamera + "校准，默认OK。");
                    g_UCDisplayCamera.ShowResult(string.Format("参数设定中已屏蔽该相机\n默认OK"));
                    if (DealComprehensiveResult3.D_I.BlCalibFinish)
                    {
                        WriteResidueCalibResult(CameraResult.OK);
                        BlCalibFinish = false;
                        DealComprehensiveResult3.D_I.BlCalibFinish = false;
                    }
                    return StateComprehensive_enum.True;
                }
                if (BlCalibFinish)
                {
                    g_UCDisplayCamera.ShowResult(string.Format("相机{0}清晰度已达到要求\n不再继续拍照", g_NoCamera));
                    return StateComprehensive_enum.True;
                }
                StateComprehensive_enum stateComprehensive_e = StateComprehensive_enum.True;
                stateComprehensive_e = AutoCalibSharpness(CellResultPos1, Pos_enum.Pos1,
                                         ResidueInspAutoCalibPar.R_I2, AutoCalibResult.AutoCalibResult2_L, out htResult);
                return StateComprehensive_enum.True;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
                return StateComprehensive_enum.False;
            }
            finally
            {
                LogicPLC.L_I.ClearPLC(RegMonitor.R_I[9]);
                #region 显示和日志记录

                Display(Pos_enum.Pos1, htResult, blResult, sw);

                #endregion 显示和日志记录
            }
        }

        public override StateComprehensive_enum DealComprehensiveResultFun_Calib2(TriggerSource_enum triggerSource_e, out Hashtable htResult)
        {
            #region 定义
            htResult = g_HtResult;
            //int pos = 4;
            bool blResult = true;//结果是否正确
            Stopwatch sw = new Stopwatch();
            sw.Restart();
            #endregion 定义

            try
            {
                StateComprehensive_enum stateComprehensive_e =
                    g_BaseDealComprehensive.DealComprehensivePosNoDisplay(g_UCDisplayCamera, g_HtUCDisplay, Pos_enum.Pos1, out htResult);

                return StateComprehensive_enum.True;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
                return StateComprehensive_enum.False;
            }
            finally
            {
                #region 显示和日志记录

                Display(Pos_enum.Pos1, htResult, blResult, sw);

                #endregion 显示和日志记录
            }
        }
        #endregion 自定义校准
    }
}
