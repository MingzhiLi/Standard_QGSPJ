using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DealGeometry
{
    /// <summary>
    /// 箭头所在端枚举
    /// </summary>
    [Flags]
    public enum  ArrowEnds_Enum
    {
        /// <summary>
        /// 无箭头
        /// </summary>
        None  = 0,
        /// <summary>
        /// 箭头在开始方向
        /// </summary>
        Start = 1,
        /// <summary>
        /// 箭头在结束位置
        /// </summary>
        End = 2,
        /// <summary>
        /// 两端都有箭头
        /// </summary>
        Both = 3,
    }
}
