using BasicClass;
using CalibDataManager;
using DealAlgorithm;
using DealCalibrate;
using DealComprehensive;
using DealResult;
using Main_EX;
using ModulePackage;
using ParComprehensive;
using System;
using System.Collections;
using System.Collections.Generic;
using ProjectSPJ;
using System.Drawing;
using System.Diagnostics;
using Microsoft.Windows.Controls;

namespace Main
{
    public partial class BaseDealComprehensiveResult_Main
    {
        public MonoResult _monoResult = new MonoResult();

        #region   单目双次拍照标定
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pCalibPos1_L"></param>
        /// <param name="pCalibPos2_L"></param>
        /// <param name="monocularAutoCalibPar"></param>
        /// <param name="ProdX"></param>
        /// <param name="ProdY"></param>
        public int CalRotateCenter(List<Point2D> pCalibPos1_L, List<Point2D> pCalibPos2_L, MonocularAutoCalibPar monocularAutoCalibPar,
                                    double ProdX, double ProdY)
        {
            if (pCalibPos1_L?.Count < 2 || pCalibPos2_L?.Count < 2)
            {
                return CameraResult.CONTINUE;
            }
            double rotateAngle = pCalibPos2_L.Count % 2 == 0 ? -monocularAutoCalibPar.RotateAngle : monocularAutoCalibPar.RotateAngle;
            ShowState("用于计算旋转的角度：" + rotateAngle.ToString("f3"));
            //根据参数求旋转中心
            Point2D rcPos1 = new FunCalibRotate().GetOriginPoint(rotateAngle, 
                                                                 pCalibPos1_L[pCalibPos1_L.Count - 2], 
                                                                 pCalibPos1_L[pCalibPos1_L.Count - 1]);
            Point2D rcPos2 = new FunCalibRotate().GetOriginPoint(rotateAngle, 
                                                                 pCalibPos2_L[pCalibPos2_L.Count - 2], 
                                                                 pCalibPos2_L[pCalibPos2_L.Count - 1]);
            ///两个位置的旋转中心，一定一个X方向为正，一个为负
            ///Y方向坐标相差很小
            if (rcPos1.DblValue1 * rcPos2.DblValue1 > 0 || rcPos1.DblValue2 * rcPos2.DblValue2 < 0)
            {
                ShowAlarm("两个拍照位置的旋转中心计算错误！位置1旋转中心：" + rcPos1.ToString() + "位置2旋转中心：" + rcPos2.ToString());
                ///将标定NG的点位存到本地
                BaseSerializer.SaveList(pCalibPos1_L, PathRootLog + "位置1校准NG点位.xml");
                BaseSerializer.SaveList(pCalibPos2_L, PathRootLog + "位置2校准NG点位.xml");
                return CameraResult.NG;
            }
            ShowState("位置1旋转中心：" + rcPos1.ToString());
            ShowState("位置2旋转中心：" + rcPos2.ToString());
            ///两个位置拍照计算的旋转中心X方向的距离
            double disRC = Math.Sqrt(Math.Pow(rcPos1.DblValue1 - rcPos2.DblValue1, 2) + Math.Pow(rcPos1.DblValue2 - rcPos2.DblValue2, 2)) * AMP;

            ShowState("计算的两个旋转中心的距离为：" + disRC.ToString("f3"));
            ///两个位置旋转中心X方向应该只差了一个产品的X
            ///Y方向理论上应该一样
            if (disRC - ProdX > monocularAutoCalibPar.MaxRotateDeviationX ||
               Math.Abs(rcPos2.DblValue2 - rcPos1.DblValue2) * AMP > monocularAutoCalibPar.MaxRotateDeviationY)
            {
                if(!(pCalibPos2_L.Count < monocularAutoCalibPar.MaxCalibTime))
                {
                    ShowAlarm("以达到最大校准次数，标定NG！");
                    ///将标定NG的点位存到本地
                    BaseSerializer.SaveList(pCalibPos1_L, PathRootLog + "位置1校准NG点位.xml");
                    BaseSerializer.SaveList(pCalibPos2_L, PathRootLog + "位置2校准NG点位.xml");
                    return CameraResult.NG;
                }
                return CameraResult.CONTINUE;
            }

            ///当产品中心与旋转中心重合时两个Mark在相机中的位置
            ///相机采用以左下角为原点的右手坐标系，
            ///这里先全部使用Mark1的旋转中心来做计算
            double mark1X = rcPos1.DblValue1 + ProdX / 2 / AMP;
            double mark1Y = rcPos1.DblValue2 - ProdY / 2 / AMP;
            double mark2X = rcPos1.DblValue1 + ProdX / 2 / AMP;
            double mark2Y = rcPos1.DblValue2 - ProdY / 2 / AMP;

            ModelParams.PMonoStdPos1 = new Point2D(mark1X, mark1Y);
            ModelParams.PMonoStdPos2 = new Point2D(mark2X, mark2Y);
            ModelParams.PMonoRCPos1 = new Point2D(rcPos1);
            ModelParams.PMonoRCPos2 = new Point2D(rcPos2);
            ShowState(string.Format("单目定位两个拍照位的旋转中心分别为：{0},{1}", ModelParams.PMonoRCPos1.ToString(), ModelParams.PMonoRCPos2.ToString()));
            ShowState(string.Format("单目定位两个拍照位的基准位分别为：{0},{1}", ModelParams.PMonoStdPos1.ToString(), ModelParams.PMonoStdPos2.ToString()));
            ///将标定OK的点位存到本地
            BaseSerializer.SaveList(pCalibPos1_L, PathRootLog + "位置1校准OK点位.xml");
            BaseSerializer.SaveList(pCalibPos2_L, PathRootLog + "位置2校准OK点位.xml");
            return CameraResult.OK;
        }

        #endregion

        
        public bool DealMonoPos1(ResultCrossLines resultCross)
        {
            _monoResult = new MonoResult();

            if (!DealTypeResult(resultCross))
            {
                _monoResult.Info = "位置1拍照NG";
                ShowAlarmCamera(_monoResult.Info);
                FinishPhotoPLC(CameraResult.NG);
                SaveBitmapImage(false, "位置1");
                return false;
            }
            _monoResult.Mark1X = resultCross.X;
            _monoResult.Mark1Y = resultCross.Y;
            if(_monoResult.Mark1X * _monoResult.Mark1Y == 0)
            {
                _monoResult.Info = "位置1拍照NG";
                ShowAlarmCamera(_monoResult.Info);
                FinishPhotoPLC(CameraResult.NG);
                SaveBitmapImage(false, "位置1");
                return false;
            }
            FinishPhotoPLC(CameraResult.OK);
            g_UCDisplayCamera.ShowResult("位置1拍照OK");
            SaveBitmapImage(true, "位置1");
            return true;
        }


        public bool DealMonoPos2(ResultCrossLines resultCross)
        {
            if (!DealTypeResult(resultCross))
            {
                _monoResult.Info = "位置2拍照NG";
                return false;
            }
            if (Math.Abs(resultCross.IncludedR_J - ParModelValue.Inst.StdMLineAngle) > ParModelValue.Inst.ThdMLineDev)
            {
                _monoResult.Info = string.Format("位置2直线夹角{0}°，超出偏差阈值{1}",
                     resultCross.IncludedR_J.ToString("f3"), ParModelValue.Inst.ThdMLineDev.ToString());
                return false;
            }

            _monoResult.Mark2X = resultCross.X;
            _monoResult.Mark2Y = resultCross.Y;
            if (_monoResult.Mark2X * _monoResult.Mark2Y == 0)
            {
                _monoResult.Info = "位置2拍照NG";
                return false;
            }

            return true;
        }

        /// <summary>
        /// 计算单目相机的偏差
        /// </summary>
        /// <param name="markLength">Mark间距</param>
        /// <param name="pRotateCenter1">旋转中心</param>
        /// <param name="pStdMark">基准Mark</param>
        /// <param name="axis">轴系统</param>
        /// <returns>计算是否成功</returns>
        public bool CalDelta(double markLength, Point2D pRotateCenter1,  Point2D pStdMark, AxisConfigPar axis)
        {
            double rad;
            Point2D pointCal = _monoResult.Mark2;
            if (axis.MoveDireciton == MoveDirecitonEnum.从左向右)
            {
                rad = Math.Asin((_monoResult.Mark1Y - _monoResult.Mark2Y) * AMP / markLength);   ///计算角度，弧度制
            }
            else
            {
                rad = Math.Asin((_monoResult.Mark2Y - _monoResult.Mark1Y) * AMP / markLength);   ///计算角度，弧度制
            }

            Point2D result = new FunCalibRotate().GetPoint_AfterRotation(-rad, pRotateCenter1, pointCal * AMP);

            Point2D pDelta1 = pStdMark * AMP - result;


            _monoResult.DeltaX = pDelta1.DblValue1 * axis.CalRatioX;
            _monoResult.DeltaY = pDelta1.DblValue2 * axis.CalRatioY;
            if(axis.AxisR == AxisREnum.顺时针)
            {
                _monoResult.DeltaR = rad * 180 / Math.PI + ParModelValue.Inst.ComInspPosR;
            }
            else
            {
                _monoResult.DeltaR = -rad * 180 / Math.PI + ParModelValue.Inst.ComInspPosR;
            }

            if(_monoResult.DeltaX > GlobalThreshold.Inst.ThdMonoX)
            {
                _monoResult.Info = string.Format("单目定位偏差X：{0}，超出阈值{1}",_monoResult.DeltaX, GlobalThreshold.Inst.ThdMonoX);
                return false;
            }
            if (_monoResult.DeltaY > GlobalThreshold.Inst.ThdMonoY)
            {
                _monoResult.Info = string.Format("单目定位偏差Y：{0}，超出阈值{1}", _monoResult.DeltaY, GlobalThreshold.Inst.ThdMonoY);
                return false;
            }
            if (_monoResult.DeltaR > GlobalThreshold.Inst.ThdMonoR)
            {
                _monoResult.Info = string.Format("单目定位偏差R：{0}，超出阈值{1}", _monoResult.DeltaR, GlobalThreshold.Inst.ThdMonoR);
                return false;
            }
            _monoResult.DeltaInspX = _monoResult.DeltaX + ParModelValue.Inst.ComInspPosX;
            _monoResult.DeltaInspY = _monoResult.DeltaY + ParModelValue.Inst.ComInspPosY;
            _monoResult.DeltaInspR = _monoResult.DeltaR + ParModelValue.Inst.ComInspPosR;
            ///二维码在产品上的坐标，这里以产品左下角为原点
            Point2D pCode = new Point2D((ParModelValue.CodeXInWastage - ParModelValue.WastageX) / 2, 
                                        (ParModelValue.CodeYInWastage - ParModelValue.WastageY) / 2);
            ///当前二维码因角度导致的偏差
            Point2D pDevCodeOfAngle = pCode - new FunCalibRotate().GetPoint_AfterRotation(-rad, new Point2D(), pCode);
            _monoResult.DeltaQrAngleX = pDevCodeOfAngle.DblValue1;
            _monoResult.DeltaQrAngleY = pDevCodeOfAngle.DblValue2;
            _monoResult.DeltaQrX = _monoResult.DeltaX - pDevCodeOfAngle.DblValue1 + ParModelValue.Inst.ComQrCodePosX;
            _monoResult.DeltaQrY = _monoResult.DeltaY - pDevCodeOfAngle.DblValue2 + ParModelValue.Inst.ComQrCodePosY;
            return true;

        }

    }
}
