using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main
{
    [Serializable]
    public class ParFunctionSelection:BaseSerializer
    {
        /// <summary>
        /// 静态类实例
        /// </summary>
        public static ParFunctionSelection P_I = new ParFunctionSelection();

        /// <summary>
        /// 默认构造函数
        /// </summary>
        public ParFunctionSelection()
        {
            BlPreciseCalib = true;
            BlResidueInspCalib1 = true;
            BlResidueInspCalib2 = true;
            BlMonocularCalib = true;
            BlCSTCalib = true;
        }
        /// <summary>
        /// 是否开启精定位校准 
        /// </summary>
        public bool BlPreciseCalib { get; set; }
        /// <summary>
        /// 是否开启残材1校准
        /// </summary>
        public bool BlResidueInspCalib1 { get; set; }
        /// <summary>
        /// 是否开启残材2校准
        /// </summary>
        public bool BlResidueInspCalib2 { get; set; }
        /// <summary>
        /// 是否开启单目相机校准
        /// </summary>
        public bool BlMonocularCalib { get; set; }
        /// <summary>
        /// 是否开启卡塞校准
        /// </summary>
        public bool BlCSTCalib { get; set; }
    }

}
