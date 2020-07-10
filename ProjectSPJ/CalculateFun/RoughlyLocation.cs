using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using BasicClass;
using DealCalibrate;
using System.Reflection;
using System.Windows;
using System.Windows.Annotations;

namespace ProjectSPJ
{
    /// <summary>
    /// 粗定位计算类
    /// </summary>
    public class RoughlyLocation:BaseCalculate
    {
        private new static string NameClass = MethodBase.GetCurrentMethod().DeclaringType.Name;



        /// <summary>
        /// 计算取片坐标
        /// </summary>
        /// <param name="parCalibrate">标定参数</param>
        /// <param name="roughlyResult">粗定位结果,计算的坐标也在这里</param>
        /// <returns></returns>
        public static bool CalcPickPos(BaseParCalibRobot parCalibrate,RoughlyResult roughlyResult)
        {
            try
            {                
                Point p2DPickPos = new Point();
                if (!CalcPickPos(parCalibrate,roughlyResult, out p2DPickPos))
                {
                    return false;
                }
                if (!PickRangeLim.Inst.IsInLimitRegion(p2DPickPos, roughlyResult))
                {
                    return false;
                }

                roughlyResult.Info = "坐标计算OK";
                roughlyResult.PickPos = new Point4D(p2DPickPos.X + ParModelValue.Inst.ComPickPosR, 
                                      p2DPickPos.Y + ParModelValue.Inst.ComPickPosY, 
                                      RobotPar.Inst.PickPosZ + ProductSet.Inst.Thickness + ParModelValue.Inst.ComPickPosZ, 
                                      RobotPar.Inst.PickPosR + ParModelValue.PickAngle + roughlyResult.R);
            }
            catch (Exception ex)
            {
                roughlyResult.Info = "粗定位坐标计算异常";
                Log.L_I.WriteError(NameClass, ex);
                return false;
            }
            return true;
        }


        /// <summary>
        /// 计算机器人取片二维坐标（X,Y)
        /// </summary>
        /// <param name="pt2Pixel">粗定位相机的像素结果</param>
        /// <param name="parCalibrate">机器人校准算子参数</param>
        /// <param name="result"></param>
        /// <returns></returns>
        private static bool CalcPickPos(BaseParCalibRobot parCalibrate, RoughlyResult roughlyLocation, out Point result)
        {
            result = new Point();
            try
            {
                Point2D P2 = FunCalibRobot.F_I.GetWordCord(roughlyLocation.PixelPoint, parCalibrate);
                if (P2.DblValue1 * P2.DblValue2 == 0)
                {
                    roughlyLocation.Info = "粗定位计算坐标为0，请检查是否误抓或者产品超出标定范围";
                    return false;
                }
                result = new Point(P2.DblValue1, P2.DblValue2);
                return true;
            }
            catch (Exception ex)
            {
                roughlyLocation.Info = "坐标计算异常";
                Log.L_I.WriteError(RoughlyLocation.NameClass, ex);
                return false;
            }
        }
    }
}
