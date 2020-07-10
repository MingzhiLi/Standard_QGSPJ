using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using BasicClass;

namespace ProjectSPJ
{
    /// <summary>
    /// 残材相机的计算类
    /// </summary>
    public class ResidueCalculate: BaseCalculate
    {
        /// <summary>
        /// 重写类名
        /// </summary>
        private new static string NameClass = MethodBase.GetCurrentMethod().DeclaringType.Name;

        /// <summary>
        /// 计算残材
        /// </summary>
        /// <param name="residueResult"></param>
        /// <returns></returns>
        public static bool CalResidueResult (ResidueResult residueResult)
        {
            try
            {
                if(residueResult.SharpnessTFT < GlobalThreshold.Inst.ThdMinSharpness)
                {
                    residueResult.Info = "残材TFT清晰度:"  +
                                         residueResult.SharpnessTFT.ToString("f3") + 
                                         ",小于设定阈值" +
                                         GlobalThreshold.Inst.ThdMinSharpness;
                    residueResult.BlResult = false;
                    return false;
                }
                if(residueResult.Ratio < residueResult.Threshold)
                {
                    residueResult.Info = "残材清晰度比例:" +
                                         residueResult.Ratio.ToString("f3") +
                                         ",小于设定阈值" + 
                                         residueResult.Threshold;
                    residueResult.BlResult = false;
                    return false;
                }
                residueResult.BlResult = true;
                return true;
            }
            catch(Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
                return false;
            }
        }
        /// <summary>
        /// 获取 拍照的清晰度
        /// </summary>
        /// <param name="list">清晰度list</param>
        /// <param name="residueResult">残材计算的结果</param>
        /// <returns></returns>
        public static bool GetSharpness(List<double> list, ResidueResult residueResult)
        {
            if(!(list?.Count > 1))
            {
                residueResult.BlResult = false;
                residueResult.Info = "设定的ROI区域小于2";
                return false;
            }
            residueResult.SharpnessTFT = list[0];
            residueResult.SharpnessCF = list[1];
            return true;
        }

        /// <summary>
        /// 检查是否连续NG,如果是连续NG则返回false
        /// </summary>
        /// <param name="residueResults"></param>
        /// <param name="numSet"></param>
        /// <returns></returns>
        public static bool CheckContinueNG(List<ResidueResult> residueResults, int numSet)
        {
            if(residueResults?.Count < numSet)   ///当前结果数量小于设定的连续NG数量
            {
                return true;  //如果小于直接返回true
            }
            bool blResult = true;
            int numResult = residueResults.Count;

            for(int i = 0;i < numSet && blResult; i++)  ///在设定的数量范围内，只要有一个为OK时则直接返回OK
            {
                blResult = !residueResults[numResult - i - 1].BlResult;   ///当存在OK时直接将标志位置位false终止循环
            }
            return blResult;
        }
    }
}
