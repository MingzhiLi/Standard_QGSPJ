using BasicClass;
using DealCalibrate;
using DealComprehensive;
using DealPLC;
using DealResult;
using DealRobot;
using Main_EX;
using ModulePackage;
using System;
using System.Collections;
using System.Windows;
using DealLog;
using System.Threading;
using ProjectSPJ;

namespace Main
{
    public partial class BaseDealComprehensiveResult_Main
    {
        #region 粗定位
        /// <summary>
        /// 九宫格标定结果计算，只支持匹配T，否则模板角度会导致出错
        /// </summary>
        /// <param name="index">工位号</param>
        /// <param name="cellName"></param>
        /// <param name="htResult"></param>
        /// <returns></returns>
        public StateComprehensive_enum CalcPickPos(int index, string cellName, out Hashtable htResult)
        {
            Thread.Sleep((int)PhotoDelay.Inst.DelayRoughly);  //拍照延时
            StateComprehensive_enum stateComprehensive_e = g_BaseDealComprehensive.DealComprehensivePosNoDisplay(
                g_UCDisplayCamera, g_HtUCDisplay, Pos_enum.Pos1, out htResult);
            RoughlyResult roughlyResult = new RoughlyResult();
            try
            {
                #region 空跑
                if (ParStateSoft.StateMachine_e == StateMachine_enum.NullRun)
                {
                    cnt++;
                    Point4D dst = RobotPar.PickPos.Add(2, 50);  //空跑时机器人高度抬高50
                    LogicRobot.L_I.WriteRobotCMD(dst, LogicRobot.RbtCmdPickPos,ShowState);
                    ShowState(string.Format("空跑模式，发送给机器人坐标:X:{0},Y:{1},Z:{2}",
                        dst.DblValue1.ToString("f3"), dst.DblValue2.ToString("f3"), dst.DblValue3.ToString("f3")));
                    //}
                    roughlyResult.Info = "空跑";
                    FinishPhotoPLC(1);
                    return StateComprehensive_enum.True;
                }
                #endregion


                ResultScaledShape result = (ResultScaledShape)htResult[cellName];
                ResultTemplate template = (ResultTemplate)htResult[cellName + @"T"];

                if(!DealTypeResult(result))
                {
                    FinishPhotoPLC(CameraResult.NG);
                    return StateComprehensive_enum.False;
                }
                roughlyResult.Score = result.Score;
                roughlyResult.PixelX = result.X;
                roughlyResult.PixelY = result.Y;
                roughlyResult.R = Math.Round(result.R_J + template.RCenterProfile / Math.PI * 180, 3);
                roughlyResult.StationNo = index;
                //如果没有匹配到就进行工位相关的判断
                if (!roughlyResult.IsPhotoOK)
                {
                    ShowState(string.Format("粗定位工位{0}未识别到玻璃！", index));
                    g_UCDisplayCamera.ShowResult("未识别到产品", "red");

                    //如果工位号已经和预设的工位数相同，即代表整个中片已经做完
                    if (index == ProductSet.Inst.PickStationNum)
                    {
                        //判断已取片数是否与配方中的中片数一致，如果不一致则弹框提醒，一致则直接通知PLC工位取完
                        if (RegeditMain.R_I.PickCnt < ProductSet.Inst.SumPick &&
                            !WinMsgBox.ShowMsgBox("是否拖拽报表纸"))
                        {
                            roughlyResult.Info = "产品未取完NG";
                            //最大工位号+1，作为人工确认中片未取完
                            LogicPLC.L_I.WriteRegData1((int)DataRegister1.NextStation, index + 1);
                            FinishPhotoPLC(CameraResult.NG);
                            g_BaseDealComprehensive.SaveNGImage("C1");
                            //roughlyResult.ImagePath = SaveBitmapImage(false, "未取完");
                            return StateComprehensive_enum.True;
                        }                        
                        RegeditMain.R_I.PickCnt = 0;
                    }
                    roughlyResult.Info = "当前工位产品已取完";
                    // 通知当前工位取完，index=工位号，plc提供
                    LogicPLC.L_I.WriteRegData1((int)DataRegister1.NextStation, index);
                    FinishPhotoPLC(CameraResult.OK);
                    g_BaseDealComprehensive.SaveNGImage("C1");
                }
                else
                {
                    if (!RoughlyLocation.CalcPickPos(ParCalibRobot1.P_I, roughlyResult))
                    {
                        g_UCDisplayCamera.ShowResult(roughlyResult.Info, "red");
                        WinError.GetWinInst().ShowError(roughlyResult.Info);

                        FinishPhotoPLC(CameraResult.NG);
                        g_BaseDealComprehensive.SaveNGImage("C1");
                        return StateComprehensive_enum.False;
                    }
                    else
                    {
                        roughlyResult.PickIndex = RegeditMain.R_I.PickCnt + 1;
                        g_UCDisplayCamera.ShowResult(roughlyResult.InfoForShow);

                        //来料时标记NG
                        if (ModelParams.DumpList_Solid.Contains((RegeditMain.R_I.PickCnt + 1).ToString()))
                        {
                            ShowState(string.Format("通知机器人当前第{0}标记为NG片", RegeditMain.R_I.PickCnt + 1));
                            LogicRobot.L_I.WriteRobotCMD(LogicRobot.RbtCmdPickDump, ShowState);
                        }
                    }

                    LogicRobot.L_I.WriteRobotCMD(roughlyResult.PickPos, LogicRobot.RbtCmdPickPos, ShowState_Robot);
                    //ShowState("已发送取片坐标:" + roughlyResult.PickPos.ToString());
                    //roughlyResult.ImagePath = SaveBitmapImage(true);
                    g_BaseDealComprehensive.SaveOKImage("C1");
                    FinishPhotoPLC(CameraResult.OK);
                }

                return StateComprehensive_enum.True;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
                FinishPhotoPLC(CameraResult.NG);
                return StateComprehensive_enum.False;
            }
            finally
            {
                roughlyResult.AddToResultList();
            }
        }

        #endregion
    }
}
