using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DealPLC;
using System.Threading;
using System.Threading.Tasks;
using DealFile;
using DealComprehensive;
using Common;
using SetPar;
using ParComprehensive;
using BasicClass;
using Camera;
using System.Collections;
using DealResult;
using DealConfigFile;
using DealCalibrate;
using DealRobot;
using DealMath;
using DealImageProcess;
using BasicComprehensive;
using System.Diagnostics;
using BasicDisplay;
using Main_EX;
using ProjectSPJ;

namespace Main
{
    public partial class DealComprehensiveResult3 : BaseDealComprehensiveResult_Main
    {
        #region 定义
        //double            
        double WastageInvalidCnt = 0;

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
            bool blResult = true;

            Stopwatch sw = new Stopwatch();
            sw.Restart();
            #endregion 定义
            ResidueResult residueResult = new ResidueResult(ResidueEnum.残材1);
            try
            {
                Thread.Sleep(PhotoDelay.Inst.DelayResidue1);
                StateComprehensive_enum stateComprehensive_e = StateComprehensive_enum.True;
                if (!FunctionSelect.Inst.IsEnableResidue1)
                {
                    FinishPhotoPLC(1);
                    ShowState("相机3Pass");
                    g_UCDisplayCamera.ShowResult("相机3PASS");
                    return StateComprehensive_enum.True;
                }
                residueResult.Threshold = ParModelValue.Inst.ThdResidue1;
                stateComprehensive_e = DealSharpness(CellResultPos1, Pos_enum.Pos1,
                                                     residueResult, ResidueResult.ResidueResult1_L,
                                                     out htResult) ;
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
                if(blResult)
                {
                    residueResult.ImagePath.Add(SaveBitmapImage(true, "位置1"));
                    residueResult.ScreenImage.Add(SaveScreenImage(true, "位置1"));
                }
                else
                {
                    residueResult.ImagePath.Add(SaveBitmapImage(false, "位置1"));
                    residueResult.ScreenImage.Add(SaveScreenImage(false, "位置1"));
                }
                residueResult.AddToResultList(ResidueResult.ResidueResult1_L);

                #endregion 显示和日志记录
            }
        }
        #endregion 位置1拍照

        #region 位置2拍照
        public override StateComprehensive_enum DealComprehensiveResultFun2(TriggerSource_enum trigerSource_e,out Hashtable htResult)
        {
            #region 定义
            htResult = g_HtResult;
            //int pos = 2;
            bool blResult = true;//结果是否正确
            Stopwatch sw = new Stopwatch();
            sw.Restart();
            #endregion 定义

            ResidueResult residueResult = new ResidueResult(ResidueEnum.残材1);
            try
            {
                Thread.Sleep(PhotoDelay.Inst.DelayResidue1);
                StateComprehensive_enum stateComprehensive_e;
                if (!FunctionSelect.Inst.IsEnableResidue1)
                {
                    FinishPhotoPLC(1);
                    ShowState("相机3Pass");
                    return StateComprehensive_enum.True;
                }
                residueResult.Threshold = ParModelValue.Inst.ThdResidue1;
                stateComprehensive_e = DealSharpness(CellResultPos2, Pos_enum.Pos2,
                                                     residueResult, ResidueResult.ResidueResult1_L, 
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

                if (blResult)
                {
                    residueResult.ImagePath.Add(SaveBitmapImage(true, "位置2"));
                    residueResult.ScreenImage.Add(SaveScreenImage(true, "位置2"));
                }
                else
                {
                    residueResult.ImagePath.Add(SaveBitmapImage(false, "位置2"));
                    residueResult.ScreenImage.Add(SaveScreenImage(false, "位置2"));
                }
                residueResult.AddToResultList(ResidueResult.ResidueResult1_L);
                #endregion 显示和日志记录
            }
        }
        #endregion 位置2拍照

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
                if(!ParFunctionSelection.P_I.BlResidueInspCalib1)   ///屏蔽残材1
                {
                    g_UCDisplayCamera.ShowResult(string.Format("参数设定中已屏蔽该相机\n默认OK"));
                    ShowState("已屏蔽相机" + g_NoCamera + "校准，默认OK。");
                    ///如果残材2也屏蔽了，则直接写入完成信号
                    if (!ParFunctionSelection.P_I.BlResidueInspCalib2)
                    {
                        WriteResidueCalibResult(CameraResult.OK);
                    }
                    return StateComprehensive_enum.True;
                }
                if (BlCalibFinish)
                {
                    BlCurFinish = true;
                    g_UCDisplayCamera.ShowResult(string.Format("相机{0}清晰度已达到要求\n不再继续拍照", g_NoCamera));
                    return StateComprehensive_enum.True;
                }
                StateComprehensive_enum stateComprehensive_e = StateComprehensive_enum.True;
                stateComprehensive_e = AutoCalibSharpness(CellResultPos1, Pos_enum.Pos1,
                                         ResidueInspAutoCalibPar.R_I1, AutoCalibResult.AutoCalibResult1_L, out htResult);
                return StateComprehensive_enum.True;
            }
            catch(Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
                return StateComprehensive_enum.False;
            }
            finally
            {
                LogicPLC.L_I.ClearPLC(RegMonitor.R_I[8]);

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
