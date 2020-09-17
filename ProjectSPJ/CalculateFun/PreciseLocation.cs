using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BasicClass;
using System.Collections;
using DealResult;
using System.Windows;
using System.Reflection;
using Xceed.Wpf.Toolkit.PropertyGrid.Editors;

namespace ProjectSPJ
{
    /// <summary>
    /// 精定位计算类
    /// </summary>
    public class PreciseLocation:BaseCalculate
    {
        /// <summary>
        /// 类名
        /// </summary>
        public static string NameClass => MethodBase.GetCurrentMethod().DeclaringType.Name;

        /// <summary>
        /// 获取精定位不规则区域计算的角度
        /// </summary>
        /// <param name="resultBlob">不规则区域计算结果</param>
        /// <param name="axisConfig">轴方向配置</param>
        /// <param name="amp">相机系数</param>
        /// <param name="result">精定位计算结果</param>
        /// <returns>角度计算是否成功</returns>
        public static bool CalBlobAngle(ResultBlob resultBlob, AxisConfigPar axisConfig, 
            double amp, PreciseResult result)
        {
            result.Area = Math.Round(resultBlob.Area * amp * amp, 3);
            double angle = resultBlob.R_J;
            if(AreaDetect(result))
            {
                return GetAngle(angle, axisConfig, result);
            }
            return false;
        }

        /// <summary>
        /// 计算放片位置,已加入补偿值，偏差也已旋转 
        /// </summary>
        /// <param name="axisConfig">轴方向配置</param>
        /// <param name="result">精定位结果</param>
        /// <returns>计算是否成功</returns>
        public static bool CalPutPos( AxisConfigPar axisConfig, PreciseResult result)
        {
            if (AreaDetect(result))
            {
                return GetPutPos(result, axisConfig);
            }

            return false;
        }
        /// <summary>
        /// 精定位面积检测
        /// </summary>
        /// <param name="area">已转换为以mm为单位的面积</param>
        /// <param name="result"><精定位计算结果</param>
        /// <returns></returns>
        private static bool AreaDetect(PreciseResult result)
        {
            try
            {
                double stdArea = ProductSet.Inst.GlassX * ProductSet.Inst.GlassY;

                if (!(result.Area * stdArea > 0))
                {
                    result.Info = "面积计算或基准面积为0";
                    return false;
                }

                result.AreaRatio = Math.Round(result.Area / stdArea, 3);
                result.RatioMin = ParModelValue.Inst.ThdAreaMin;
                result.RatioMax = ParModelValue.Inst.ThdAreaMax;                

                if (result.AreaRatio > ParModelValue.Inst.ThdAreaMax ||
                    result.AreaRatio < ParModelValue.Inst.ThdAreaMin)
                {
                    result.Info = string.Format("面积比例{0}超出阈值【{1}，{2}】"
                                                , result.AreaRatio.ToString("f3")
                                                , ParModelValue.Inst.ThdAreaMin
                                                , ParModelValue.Inst.ThdAreaMax);
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                result.Info = "面积计算进入异常";
                Log.L_I.WriteError(NameClass, ex);
                return false;
            }
        }

        /// <summary>
        /// 获取不规则区域的角度
        /// </summary>
        /// <param name="deltar">图像角度</param>
        /// <param name="threadR">角度阈值</param>
        /// <param name="axisConfig">轴方向配置</param>
        /// <returns></returns>
        public static bool GetAngle(double deltar, AxisConfigPar axisConfig, PreciseResult result)
        {
            try
            {
                ///根据轴方向返回偏差
                result.DeltaR = Math.Round(deltar * (int)axisConfig.AxisR, 3);

                if (Math.Abs(result.DeltaR) > GlobalThreshold.Inst.ThdPreciseDeltaR)
                {
                    result.Info = "精确定位处角度偏差:" + result.DeltaR.ToString("f3") +
                        "，超过设定阈值:" + GlobalThreshold.Inst.ThdPreciseDeltaR.ToString("f3");
                    
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                result.Info = "角度计算进入异常";
                Log.L_I.WriteError(NameClass, ex);
                return false;
            }
            finally
            {
            }
        }

        /// <summary>
        /// 获取放片位置
        /// </summary>
        /// <param name="result">精定位结果</param>
        /// <returns>是否获取成功</returns>
        private static bool GetPutPos(PreciseResult result, AxisConfigPar axisConfig)
        {
            try
            {
                Point delta = new Point(result.DeltaX, result.DeltaY);
                double rad =                     
                    (ParModelValue.WastageRobotAngle - ParModelValue.PrecisePhotoPos1.DblValue4 + axisConfig.DeltaRotateR) / 180 * Math.PI;  ///放片角度 + 轴方向不同导致的旋转角度 - 精定位的拍照角度取片角度即为偏差需要旋转的角度

                Point rotateDelta = GetPointAfterRotation(rad, new Point(), delta);  ///将精定位拍照的偏差转换到放片偏差
                
                if (Math.Abs(delta.X) > GlobalThreshold.Inst.ThdPreciseDeltaX || 
                    Math.Abs(delta.Y) > GlobalThreshold.Inst.ThdPreciseDeltaY)
                {
                    result.Info = string.Format("精定位偏差超出阈值[{0},{1}]",
                        GlobalThreshold.Inst.ThdPreciseDeltaX, GlobalThreshold.Inst.ThdPreciseDeltaY);
                    return false;
                }
                result.PutPos = ParModelValue.PosWastagePlat1 +
                    new Point4D(rotateDelta.X, rotateDelta.Y, 0, result.DeltaR); 
                                                   ///这里还需要加一个残材标定的补偿
               
                return true;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
                return false;
            }
        }

        /// <summary>
        /// 检查产品长宽
        /// </summary>
        /// <param name="preciseResult"></param>
        /// <returns></returns>
        public bool CheckLength(PreciseResult preciseResult)
        {
            double stdLength;
            double stdWidth;
            if (ProductSet.Inst.GlassX > ProductSet.Inst.GlassY)
            {
                stdLength = ProductSet.Inst.GlassX;
                stdWidth = ProductSet.Inst.GlassY;
            }
            else
            {
                stdLength = ProductSet.Inst.GlassY;
                stdWidth = ProductSet.Inst.GlassX;
            }
            double widthDev = Math.Abs(preciseResult.Width - stdWidth);
            double lengthDev = Math.Abs(preciseResult.Length - stdLength);
            if (widthDev > ParModelValue.Inst.ThdWidthDev)
            {
                preciseResult.Info = string.Format("产品宽度偏差:{0},超出设定阈值{1}！", widthDev.ToString("f3"), ParModelValue.Inst.ThdWidthDev);

                return false;
            }
            if (lengthDev > ParModelValue.Inst.ThdLengthDev)
            {
                preciseResult.Info = string.Format("产品长度偏差:{0},超出设定阈值{1}！", lengthDev.ToString("f3"), ParModelValue.Inst.ThdLengthDev);
                return false;
            }
            return true;
        }
    }
}
