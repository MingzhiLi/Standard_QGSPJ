using BasicClass;
using DealCalibrate;
using DealComprehensive;
using DealResult;
using DealRobot;
using Main_EX;
using ModulePackage;
using ParComprehensive;
using System;
using System.Collections;
using System.Threading;
using DealLog;
using ProjectSPJ;
using InteractiveDataDisplay.WPF;
using Xceed.Wpf.Toolkit.PropertyGrid.Editors;

namespace Main
{
    public partial class BaseDealComprehensiveResult_Main 
    {
        #region 精定位
        /// <summary>
        /// 精定位的拍照结果
        /// </summary>
        private PreciseResult _preciseResult = new PreciseResult();

        #region 精定位左右移动拍照时产品的四个点
        private Point2D pLeftTop = new Point2D();
        private Point2D pLeftBot = new Point2D();
        private Point2D pRightTop = new Point2D();
        private Point2D pRightBot = new Point2D();
        #endregion

        #region 接口
        /// <summary>
        /// 精定位角度计算
        /// </summary>
        /// <param name="cellName"></param>
        /// <param name="htResult"></param>
        /// <returns></returns>        
        public StateComprehensive_enum BackLightAngleCalc(string cellName,out Hashtable htResult)
        {
            ///图像处理
            Thread.Sleep((int)PhotoDelay.Inst.DelayPrecisePos1);  //拍照延时
            StateComprehensive_enum stateComprehensive_e = g_BaseDealComprehensive.DealComprehensivePosNoDisplay(
                g_UCDisplayCamera, g_HtUCDisplay, Pos_enum.Pos1, out htResult);

            ///空跑
            if (ParStateSoft.StateMachine_e == StateMachine_enum.NullRun)
            {
                ShowState(string.Format("相机{0}空跑，默认发送OK", g_NoCamera));
                LogicRobot.L_I.WriteRobotCMD(ParModelValue.PrecisePhotoPos2, LogicRobot.RbtCmdPreciseAngle, ShowState);    ///发送精定位位置2的拍照坐标
                return StateComprehensive_enum.True;
            }
            _preciseResult = new PreciseResult();
            ResultBlob resultBlob = htResult[cellName] as ResultBlob;

            if (PreciseLocation.CalBlobAngle(resultBlob, AxisConfigPar.RobotAxisSystem, ParCalibWorld.V_I[g_NoCamera], _preciseResult))
            {
                _preciseResult.DeltaR += ParModelValue.Inst.ComPutPosR;
                g_UCDisplayCamera.ShowResult(_preciseResult.ResultForShow);
                LogicRobot.L_I.WriteRobotCMD(ParModelValue.PrecisePhotoPos2.Add(3, _preciseResult.DeltaR), LogicRobot.RbtCmdPreciseAngle, ShowState);
                SaveBitmapImage(true, "第一次拍照");
                return StateComprehensive_enum.True;
            }
            else
            {
                g_UCDisplayCamera.ShowResult(_preciseResult.ResultForShow, false);
                _preciseResult.AddToResultList();   ///第一次拍照失败会直接抛料，所以要记录
                LogicRobot.L_I.WriteRobotCMD(LogicRobot.RbtCmdPreciseFailed, ShowState);
                SaveBitmapImage(false, "第一次拍照");
                RegeditMain.R_I.PreciseNG++;
                return StateComprehensive_enum.False;
            }
        }

        /// <summary>
        /// 精定位XY偏差计算
        /// </summary>
        /// <param name="cellName"></param>
        /// <param name="htResult"></param>
        /// <returns></returns>
        public StateComprehensive_enum BackLightDeviationCalc(string cellName,out Hashtable htResult)
        {
            if (PutPosCal(cellName, out htResult))
            {
                return StateComprehensive_enum.True;
            }
            else
            {
                RegeditMain.R_I.PreciseNG++;
                return StateComprehensive_enum.False;
            }
        }

        /// <summary>
        /// 计算并发送残材平台放片位
        /// </summary>
        /// <param name="cellName"></param>
        /// <param name="htResult"></param>
        /// <returns></returns>
        public bool PutPosCal(string cellName, out Hashtable htResult)
        {
            Thread.Sleep((int)PhotoDelay.Inst.DelayPrecisePos2);
            StateComprehensive_enum stateComprehensive_e = g_BaseDealComprehensive.DealComprehensivePosNoDisplay(
                g_UCDisplayCamera, g_HtUCDisplay, Pos_enum.Pos1, out htResult);

            ///空跑
            if (ParStateSoft.StateMachine_e == StateMachine_enum.NullRun)
            {
                ShowState(string.Format("相机{0}空跑，默认发送OK", g_NoCamera));
                g_UCDisplayCamera.ShowResult("空跑默认OK");
                LogicRobot.L_I.WriteRobotCMD(RobotPar.PlatPutPos.Add(2, 30), LogicRobot.RbtCmdPreciseResult, ShowState);
                return true;
            }


            ResultBlob resultBlob = (ResultBlob)htResult[cellName];
            double amp = ParCalibWorld.V_I[g_NoCamera];
            _preciseResult.Area = Math.Round(resultBlob.Area * amp * amp, 3);
            _preciseResult.DeltaX = Math.Round((resultBlob.X - StdCamPos.Inst.PreciseCamStdX) * amp, 3);
            _preciseResult.DeltaY = Math.Round((resultBlob.Y - StdCamPos.Inst.PreciseCamStdY) * amp, 3);

            if (PreciseLocation.CalPutPos( AxisConfigPar.RobotAxisSystem, _preciseResult))
            {
                _preciseResult.ImagePath = SaveBitmapImage(true, "第二次拍照");
                _preciseResult.BlResult = true;
                g_UCDisplayCamera.ShowResult(_preciseResult.ResultForShow);
                LogicRobot.L_I.WriteRobotCMD(
                         _preciseResult.PutPos,
                         LogicRobot.RbtCmdPreciseResult, ShowState);
            }
            else
            {
                _preciseResult.ImagePath = SaveBitmapImage(false, "第二次拍照");
                g_UCDisplayCamera.ShowResult(_preciseResult.Info, false);
                LogicRobot.L_I.WriteRobotCMD(LogicRobot.RbtCmdPreciseFailed, ShowState);
            }
            PreciseResult preciseResult = _preciseResult.Clone() as PreciseResult;
            preciseResult.AddToResultList();
            return true;
        }

        #region 校准函数
        /// <summary>
        /// 残材校准时精定位的补偿
        /// </summary>
        /// <param name="cellName"></param>
        /// <param name="htResult"></param>
        /// <returns></returns>
        public bool ResidueCalibBlobDeviation(string cellName, out Hashtable htResult)
        {
            htResult = null;
            Point2D pDelta = new Point2D();
            Point4D pt4Dst = new Point4D();
            bool blResult = true;
            double[] dblResult = new double[2];
            try
            {
                Thread.Sleep((int)PhotoDelay.Inst.DelayPrecisePos2);

                StateComprehensive_enum stateComprehensive_e = g_BaseDealComprehensive.DealComprehensivePosNoDisplay(
                    g_UCDisplayCamera, g_HtUCDisplay, Pos_enum.Pos1, out htResult);
                ResultBlob resultBlob = (ResultBlob)htResult[cellName];

                Point2D delta = new Point2D();

                double deltaX = resultBlob.X - StdCamPos.Inst.PreciseCamStdX;
                double deltaY = resultBlob.Y - StdCamPos.Inst.PreciseCamStdY;
                delta = new Point2D(deltaX * AMP, deltaY * AMP);

                if (ResidueInspAutoCalibPar.R_I1.E_MoveAxis == ResidueInspAutoCalibPar.R_I2.E_MoveAxis)
                {
                    WinError.GetWinInst().ShowError("残材1与残材2所选轴均为" + ResidueInspAutoCalibPar.R_I2.E_MoveAxis.ToString() + "！不能使用相同轴！请按照实际情况选择！");
                    return false;
                }
                Point4D pt4ResidueCom = new Point4D();
                if(ResidueInspAutoCalibPar.R_I1.E_MoveAxis == MoveAxis_Enum.X轴)
                {
                    pt4ResidueCom.DblValue1 = AutoCalibResult.Residue1NextMove;
                    pt4ResidueCom.DblValue2 = AutoCalibResult.Residue2NextMove;
                }
                else
                {
                    pt4ResidueCom.DblValue1 = AutoCalibResult.Residue2NextMove;
                    pt4ResidueCom.DblValue2 = AutoCalibResult.Residue1NextMove;
                }
                blResult = false;
                LogicRobot.L_I.WriteRobotCMD(pt4Dst + pt4ResidueCom, LogicRobot.RbtCmdPreciseFailed, ShowState);
                return blResult;
            }
            catch (Exception ex)
            {
                blResult = false;
                Log.L_I.WriteError(NameClass, ex);
                return false;
            }
            finally
            {
                string strResult = "OK";
                if (!blResult)
                {
                    strResult = "NG";
                }
            }
        }

        /// <summary>
        /// 背光不规则区域基准值保存
        /// </summary>
        /// <param name="index"></param>
        /// <param name="cellName"></param>
        /// <param name="htResult"></param>
        /// <returns></returns>
        public StateComprehensive_enum CalibStdValue(int index, string cellName, out Hashtable htResult)
        {
            htResult = null;
            try
            {
                StateComprehensive_enum stateComprehensive_e = g_BaseDealComprehensive.DealComprehensivePosNoDisplay(
                    g_UCDisplayCamera, g_HtUCDisplay, 1, out htResult);
                ResultBlob resultBlob = (ResultBlob)htResult[cellName];

                if (!DealTypeResult(resultBlob))
                {
                    ShowState(string.Format("精定位自动标定第{0}点拍照失败", index + 1));
                    return StateComprehensive_enum.False;
                }
                //保存当前匹配结果       
                pt2Calib[index] = new Point2D(resultBlob.X, resultBlob.Y);

                if (index == 1)
                {
                    LogicRobot.L_I.WriteRobotCMD(LogicRobot.RbtCmdPreciseCalib2, ShowState);

                    //第二次的话保存基准值到算子中
                    if (!(RecordStdValue(cellName, pt2Calib[0], pt2Calib[1])))
                        return StateComprehensive_enum.False;
                }
                else
                {
                    ShowState(string.Format("相机{0}基准值标定第1点计算成功", g_NoCamera));
                    LogicRobot.L_I.WriteRobotCMD(LogicRobot.RbtCmdPreciseCalib1, ShowState);
                }

                return StateComprehensive_enum.True;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
                return StateComprehensive_enum.False;
            }
        }
        #endregion 校准函数


        #endregion
        /// <summary>
        /// 将计算结果保存到算子中
        /// </summary>
        /// <param name="cellName"></param>
        /// <param name="mark1"></param>
        /// <param name="mark2"></param>
        /// <returns></returns>
        public bool RecordStdValue(string cellName, Point2D mark1, Point2D mark2)
        {            
            return ModuleBase.RecordStdValue(GetParStdByCell(cellName), mark1, mark2);
        }


        #region 精定位左右移动拍照处理
        /// <summary>
        /// 获取位置1的两个点,右上，右下
        /// </summary>
        /// <param name="htResult"></param>
        /// <returns></returns>
        public StateComprehensive_enum GetPos1(out Hashtable htResult)
        {
            int pos = 1;
            Thread.Sleep((int)PhotoDelay.Inst.DelayPrecisePos1);  //拍照延时
            StateComprehensive_enum stateComprehensive_e = g_BaseDealComprehensive.DealComprehensivePosNoDisplay(
                g_UCDisplayCamera, g_HtUCDisplay, Pos_enum.Pos2, out htResult);
            ///空跑
            if (ParStateSoft.StateMachine_e == StateMachine_enum.NullRun)
            {
                ShowState(string.Format("相机{0}空跑，默认发送OK", g_NoCamera));
                LogicRobot.L_I.WriteRobotCMD(ParModelValue.PrecisePhotoPos2, LogicRobot.RbtCmdPreciseAngle, ShowState);    ///发送精定位位置2的拍照坐标
                return StateComprehensive_enum.True;
            }
            string errorInfo = string.Empty;
            if (CheckMLine2(pos, CellMlineMatch1, CellMlineCross1, CellMlineCross2,
                ParModelValue.Inst.StdMLineAngle, ParModelValue.Inst.ThdMLineDev,
                htResult, pRightTop, pRightBot, out errorInfo))
            {
                g_UCDisplayCamera.ShowResult("位置1OK");
                SaveBitmapImage(true, "第一次拍照");
                LogicRobot.L_I.WriteRobotCMD(ParModelValue.PrecisePhotoPos2, LogicRobot.RbtCmdPreciseAngle, ShowState);    ///发送精定位位置2的拍照坐标
                return StateComprehensive_enum.True;
            }
            _preciseResult.Info = errorInfo;
            g_UCDisplayCamera.ShowResult(errorInfo, false);
            LogicRobot.L_I.WriteRobotCMD(LogicRobot.RbtCmdPreciseFailed, ShowState);
            SaveBitmapImage(false, "第一次拍照");
            RegeditMain.R_I.PreciseNG++;
            return StateComprehensive_enum.False;
        }

        /// <summary>
        /// 获取位置2的两个点，左上左下
        /// </summary>
        /// <param name="htResult"></param>
        /// <returns></returns>
        public StateComprehensive_enum GetPos2(out Hashtable htResult)
        {
            int pos = 2;
            Thread.Sleep((int)PhotoDelay.Inst.DelayPrecisePos2);  //拍照延时
            StateComprehensive_enum stateComprehensive_e = g_BaseDealComprehensive.DealComprehensivePosNoDisplay(
                g_UCDisplayCamera, g_HtUCDisplay, Pos_enum.Pos3, out htResult);
            try
            {
                ///空跑
                if (ParStateSoft.StateMachine_e == StateMachine_enum.NullRun)
                {
                    ShowState(string.Format("相机{0}空跑，默认发送OK", g_NoCamera));
                    g_UCDisplayCamera.ShowResult("空跑默认OK");
                    LogicRobot.L_I.WriteRobotCMD(RobotPar.PlatPutPos.Add(2, 30), LogicRobot.RbtCmdPreciseResult, ShowState);
                    return StateComprehensive_enum.True;
                }
                _preciseResult = new PreciseResult();
                string errorInfo;
                if (CheckMLine2(pos, CellMlineMatch1, CellMlineCross3, CellMlineCross4,
                    ParModelValue.Inst.StdMLineAngle, ParModelValue.Inst.ThdMLineDev,
                    htResult, pLeftTop, pLeftBot, out errorInfo))
                {
                    double angle = GetDelta(_preciseResult);
                    if (CheckLength() ///检查产品长宽
                        && PreciseLocation.GetAngle(angle, AxisConfigPar.RobotAxisSystem, _preciseResult) //计算角度
                        && PreciseLocation.CalPutPos(AxisConfigPar.RobotAxisSystem, _preciseResult))  //计算偏差及放片位置
                    {
                        _preciseResult.DeltaR += ParModelValue.Inst.ComPutPosR;
                        _preciseResult.ImagePath = SaveBitmapImage(true, "第二次拍照");
                        _preciseResult.BlResult = true;
                        g_UCDisplayCamera.ShowResult(_preciseResult.ResultForShow);
                        LogicRobot.L_I.WriteRobotCMD(
                                 _preciseResult.PutPos,
                                 LogicRobot.RbtCmdPreciseResult, ShowState);
                        stateComprehensive_e = StateComprehensive_enum.True;
                    }
                    else
                    {
                        _preciseResult.ImagePath = SaveBitmapImage(false, "第二次拍照");
                        g_UCDisplayCamera.ShowResult(_preciseResult.Info, false);
                        LogicRobot.L_I.WriteRobotCMD(LogicRobot.RbtCmdPreciseFailed, ShowState);
                        ShowAlarm(_preciseResult.Info);
                        stateComprehensive_e = StateComprehensive_enum.False;
                    }
                }
                else
                {
                    _preciseResult.Info = errorInfo;
                    _preciseResult.ImagePath = SaveBitmapImage(false, "第二次拍照");
                    g_UCDisplayCamera.ShowResult(errorInfo, false);
                    LogicRobot.L_I.WriteRobotCMD(LogicRobot.RbtCmdPreciseFailed, ShowState);
                    stateComprehensive_e = StateComprehensive_enum.False;
                }
                PreciseResult preciseResult = _preciseResult.Clone() as PreciseResult;
                preciseResult.AddToResultList();
                return stateComprehensive_e;

            }
            catch
            {
                return StateComprehensive_enum.False;
            }
            finally
            {

            }
        }

        public double GetDelta(PreciseResult preciseResult)
        {
            ///拍照位置距离
            double disPhoto = Math.Abs(ParModelValue.PrecisePhotoPos1.DblValue2 - ParModelValue.PrecisePhotoPos2.DblValue2);
            Point2D pCenter = new Point2D();
            pCenter.DblValue1 = (pRightTop.DblValue1 + pRightBot.DblValue1 + pLeftTop.DblValue1 + pLeftBot.DblValue1) / 4;
            pCenter.DblValue2 = (pRightTop.DblValue2 + pRightBot.DblValue2 + pLeftTop.DblValue2 + pLeftBot.DblValue2) / 4;

            Point2D pCenterLeft = new Point2D();
            pCenterLeft.DblValue1 = (pLeftBot.DblValue1 + pLeftTop.DblValue1) / 2;
            pCenterLeft.DblValue2 = (pLeftBot.DblValue2 + pLeftTop.DblValue2) / 2;

            Point2D pCenterRight = new Point2D();
            pCenterRight.DblValue1 = (pRightBot.DblValue1 + pRightTop.DblValue1) / 2;
            pCenterRight.DblValue2 = (pRightBot.DblValue2 + pRightTop.DblValue2) / 2;

            double angle = Math.Atan(((pCenterRight.DblValue2 - pCenterLeft.DblValue2) * AMP) / 
                                      (Math.Abs(pCenterRight.DblValue1 - pCenterLeft.DblValue1) * AMP + disPhoto)) * 180 / Math.PI;
            double length = Math.Sqrt(Math.Pow((pCenterRight.DblValue2 - pCenterLeft.DblValue2) * AMP, 2) +
                                      Math.Pow(Math.Abs(pCenterRight.DblValue1 - pCenterLeft.DblValue1) * AMP + disPhoto, 2));

            ///左边计算的宽度
            double widthLeft = Math.Sqrt(Math.Pow(pLeftTop.DblValue1 - pLeftBot.DblValue1, 2) +
                                      Math.Pow(pLeftTop.DblValue2 - pLeftBot.DblValue2, 2)) * AMP;
            ///右边计算的宽度
            double widthRight = Math.Sqrt(Math.Pow(pRightTop.DblValue1 - pRightBot.DblValue1, 2) +
                                      Math.Pow(pRightTop.DblValue2 - pRightBot.DblValue2, 2)) * AMP;

            preciseResult.Width = Math.Round((widthLeft + widthRight) / 2, 3);
            preciseResult.Length = Math.Round(length, 3);
            preciseResult.Area = Math.Round(preciseResult.Width * preciseResult.Length, 3);
            preciseResult.DeltaX = Math.Round((pCenter.DblValue1 - StdCamPos.Inst.PreciseCamStdX) * AMP, 3);
            preciseResult.DeltaY = Math.Round((pCenter.DblValue2 - StdCamPos.Inst.PreciseCamStdY) * AMP, 3);

            return angle;
        }

        public bool CheckLength()
        {
            if(!FunctionSelect.Inst.IsCheckLength)
            {
                return true;
            }
            double stdLength;
            double stdWidth;
            if(ProductSet.Inst.GlassX > ProductSet.Inst.GlassY)
            {
                stdLength = ProductSet.Inst.GlassX;
                stdWidth = ProductSet.Inst.GlassY;
            }
            else
            {
                stdLength = ProductSet.Inst.GlassY;
                stdWidth = ProductSet.Inst.GlassX;
            }
            double widthDev = Math.Abs(_preciseResult.Width - stdWidth);
            double lengthDev = Math.Abs(_preciseResult.Length - stdLength);
            if (widthDev > ParModelValue.Inst.ThdWidthDev )
            {
                _preciseResult.Info = string.Format("产品宽度偏差:{0},超出设定阈值{1}！", widthDev.ToString("f3"), ParModelValue.Inst.ThdWidthDev);
                
                return false;
            }
            if (lengthDev > ParModelValue.Inst.ThdLengthDev)
            {
                _preciseResult.Info = string.Format("产品长度偏差:{0},超出设定阈值{1}！", lengthDev.ToString("f3"), ParModelValue.Inst.ThdLengthDev);
                
                return false;
            }
            return true;
        }
        #endregion 精定位左右移动拍照处理

        #endregion
    }
}
