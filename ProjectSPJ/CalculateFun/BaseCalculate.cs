using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BasicClass;
using System.Windows;

namespace ProjectSPJ
{
    /// <summary>
    /// 左边及各项计算的基类
    /// </summary>
    public abstract class BaseCalculate
    {
        public virtual string NameClass => System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name;
        
        /// <summary>
        /// 获取旋转角度的新坐标
        /// </summary>
        /// <param name="rad">旋转角度（弧度制）</param>
        /// <param name="pRotateCenter">旋转中心坐标</param> 
        /// <param name="pInit">旋转前坐标</param>
        /// <returns>旋转之后的坐标</returns>
        public static Point GetPointAfterRotation(double rad, Point pRotateCenter, Point pInit)
        {
            Point dstPoint = new Point();
            try
            {
                double newx = (pInit.X - pRotateCenter.X) * Math.Cos(rad) - (pInit.Y - pRotateCenter.Y) * Math.Sin(rad);
                double newy = (pInit.Y - pRotateCenter.Y) * Math.Cos(rad) + (pInit.X - pRotateCenter.X) * Math.Sin(rad);
                dstPoint.X = Math.Round(pRotateCenter.X + newx, 4);
                dstPoint.Y = Math.Round(pRotateCenter.Y + newy, 4);
                return dstPoint;
            }
            catch (Exception ex)
            {
                //LogError(NameClass, ex);

                return new Point();
            }
        }
    }
}
