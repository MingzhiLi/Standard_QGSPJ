using BasicClass;
using DealConfigFile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Main
{
    partial class ModelParams
    {
        #region adj

        /// <summary>
        /// 残材校准计算得到的X方向偏差
        /// </summary>
        public static double adjResidueCalibComX
        {
            get
            {
                string key = key_adj_ResidueCalibCom;
                return ParAdjust.Value1(key);
            }
            set
            {
                string key = key_adj_ResidueCalibCom;
                ParAdjust.SetValue1(key, value);
            }
        }

        /// <summary>
        /// 残材校准计算得到的Y方向偏差
        /// </summary>
        public static double adjResidueCalibComY
        {
            get
            {
                string key = key_adj_ResidueCalibCom;
                return ParAdjust.Value2(key);
            }
            set
            {
                string key = key_adj_ResidueCalibCom;
                ParAdjust.SetValue1(key, value);
            }
        }
        #endregion

    }
}
