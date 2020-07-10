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
using System.IO;
using System.Diagnostics;
using BasicDisplay;
using Main_EX;
using ProjectSPJ;

namespace Main
{
    public partial class DealComprehensiveResult5 : BaseDealComprehensiveResult_Main
    {

        /// <summary>
        /// 位置1处理
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
                if (ParStateSoft.StateMachine_e == StateMachine_enum.NullRun)
                {
                    ShowState(string.Format("空跑，相机{0}默认OK", g_NoCamera));
                    FinishPhotoPLC(CameraResult.OK);
                    return StateComprehensive_enum.True;
                }
                if ((!FunctionSelect.Inst.IsEnableMono))
                {
                    FinishPhotoPLC(1);
                    ShowStateCamera("相机5Pass");
                    return StateComprehensive_enum.True;
                }

                Thread.Sleep(PhotoDelay.Inst.DelayMonoPos1);
                StateComprehensive_enum stateComprehensive_e = g_BaseDealComprehensive.DealComprehensivePosNoDisplay(g_UCDisplayCamera, g_HtUCDisplay, Pos_enum.Pos1, out htResult);
                ResultCrossLines resultCross = htResult[CellResultPos1] as ResultCrossLines;
                if (DealMonoPos1(resultCross))
                {
                    return StateComprehensive_enum.True;
                }
                return StateComprehensive_enum.False;
            }
            catch (Exception ex)
            {
                LogicPLC.L_I.FinishPhoto(g_regClearCamera + g_regFinishPhoto, 2);
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

        /// <summary>
        /// 位置2
        /// </summary>
        /// <param name="htResult"></param>
        /// <returns></returns>
        public override StateComprehensive_enum DealComprehensiveResultFun2(TriggerSource_enum trigerSource_e, out Hashtable htResult)
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
                //TestTime();
                StateComprehensive_enum stateComprehensive_e = StateComprehensive_enum.True;

                if (ParStateSoft.StateMachine_e == StateMachine_enum.NullRun)
                {
                    WritePLC(1, (int)DataRegister1.InspDeviationConfirm, 1);
                    ShowState(string.Format("空跑，相机{0}默认OK", g_NoCamera));
                    FinishPhotoPLC(CameraResult.OK);
                    return StateComprehensive_enum.True;
                }
                if ((!FunctionSelect.Inst.IsEnableMono))
                {
                    WritePLC(1, (int)DataRegister1.InspDeviationConfirm, 1);
                    FinishPhotoPLC(1);
                    ShowStateCamera("相机5Pass");
                    return StateComprehensive_enum.True;
                }

                Thread.Sleep(PhotoDelay.Inst.DelayMonoPos2);
                stateComprehensive_e = g_BaseDealComprehensive.DealComprehensivePosNoDisplay(g_UCDisplayCamera, g_HtUCDisplay, Pos_enum.Pos2, out htResult);
                ResultCrossLines resultCross = htResult[CellResultPos2] as ResultCrossLines;

                if (!DealMonoPos2(resultCross))
                {
                    ShowAlarmCamera(_monoResult.Info);
                    FinishPhotoPLC(CameraResult.NG);
                    SaveBitmapImage(false, "位置2");
                    return StateComprehensive_enum.False;
                }
                MonoResult monoResult;
                if (!CalDelta(ParModelValue.WastageX,
                    ParModelValue.Inst.MonoRotCenter,
                    StdCamPos.Inst.MonoCamStd, AxisConfigPar.MonoAxisSystem))
                {
                    g_UCDisplayCamera.ShowResult(_monoResult.Info, false);
                    FinishPhotoPLC(CameraResult.NG);
                    monoResult = _monoResult.Clone() as MonoResult;
                    monoResult.AddToResultList();
                    return StateComprehensive_enum.False;
                }
                double[] deltaInsp = new double[3];
                //-------------------发送结果给PLC---------------
                FinishPhotoPLC(CameraResult.OK);
                g_UCDisplayCamera.ShowResult("位置2拍照OK");
                LogicPLC.L_I.WriteRegData2((int)DataRegister2.InspDeltaX, 3,
                                            new double[3]{ _monoResult.DeltaInspX, _monoResult.DeltaInspY, _monoResult.DeltaInspR });
                LogicPLC.L_I.WriteRegData2((int)DataRegister2.CodeComX, 2,
                                           new double[2]{ _monoResult.DeltaQrX, _monoResult.DeltaQrY });
                LogicPLC.L_I.WriteRegData1((int)DataRegister1.InspDeviationConfirm, 1);

                //-------------------结果记录-----------------
                ShowState("单目计算偏差：" + new Point3D(_monoResult.DeltaX, _monoResult.DeltaY, _monoResult.DeltaR).ToString());
                ShowState("发送巡边补偿：" 
                          + new Point3D(_monoResult.DeltaInspX, _monoResult.DeltaInspY, _monoResult.DeltaInspR).ToString());
                ShowState("二维码因角度导致的偏差："
                          + new Point2D(_monoResult.DeltaQrAngleX, _monoResult.DeltaQrAngleY));
                ShowState("发送二维码偏差：" 
                          + new Point2D(_monoResult.DeltaQrX, _monoResult.DeltaQrY));


                monoResult = _monoResult.Clone() as MonoResult;
                monoResult.AddToResultList();
                return StateComprehensive_enum.True;
            }
            catch (Exception ex)
            {
                LogicPLC.L_I.FinishPhoto(g_regClearCamera + g_regFinishPhoto, 2);
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
    }
}
