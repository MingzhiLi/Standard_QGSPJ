using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DealPLC;
using BasicClass;

namespace Main
{
    public partial class BaseDealComprehensiveResult_Main
    {

        /// <summary>
        /// 写入本次残材校准的结果
        /// </summary>
        /// <param name="result">结果</param>
        public static void WriteResidueCalibResult(int result)
        {
            LogicPLC.L_I.WriteRegData1((int)DataRegister1.ResidueCalibResult, result);
        }

        /// <summary>
        /// 写入本次标定的计算结果
        /// </summary>
        /// <param name="result"></param>
        public static void WriteMonoCalibResult(int result)
        {
            LogicPLC.L_I.WriteRegData1((int)DataRegister1.MonocalinResult, result);
        }

        /// <summary>
        /// 写入下一次标定时的旋转角度
        /// </summary>
        /// <param name="delta">补偿值X,Y,R</param>
        public static void WriteMonoCalibAngle(double delta)
        {
            LogicPLC.L_I.WriteRegData1((int)DataRegister1.MonoCalibAngle, delta);
        }
    }
}
