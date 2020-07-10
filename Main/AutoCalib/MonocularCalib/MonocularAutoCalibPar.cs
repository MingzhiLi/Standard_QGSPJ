using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main
{
    [Serializable]
    public class MonocularAutoCalibPar:BaseSerializer
    {
        public static MonocularAutoCalibPar M_I = new MonocularAutoCalibPar();

        public MonocularAutoCalibPar()
        {
            MaxCalibTime = 5;
            RotateAngle = 3;
            MaxRotateDeviationX = 3;
            MaxRotateDeviationY = 3;
        }


        /// <summary>
        /// 最大校准次数
        /// </summary>
        public int MaxCalibTime { get; set; }
        /// <summary>
        /// 测旋转中心时旋转角度
        /// </summary>
        public double RotateAngle { get; set; }
        /// <summary>
        /// 旋转中心X方向最大偏差范围
        /// </summary>
        public double MaxRotateDeviationX { get; set; }
        /// <summary>
        /// 旋转中心Y方向最大偏差范围
        /// </summary>
        public double MaxRotateDeviationY { get; set; }

    }
}
