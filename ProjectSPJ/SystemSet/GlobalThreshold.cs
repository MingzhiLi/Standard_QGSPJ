using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace ProjectSPJ
{
    /// <summary>
    /// 全局阈值，用于所有型号的阈值
    /// </summary>
    public sealed class GlobalThreshold : BaseSerializer
    {
        public static GlobalThreshold Inst = new GlobalThreshold();
        /// <summary>
        /// 文件保存路径名称，如果不重写则会以基类名称来保存
        /// </summary>
        protected override string NameFile => MethodBase.GetCurrentMethod().DeclaringType.Name;

        /// <summary>
        /// 读取本地配置文件
        /// </summary>
        public static void ReadLocalFile()
        {
            Inst = Inst.ReadLocalFile<GlobalThreshold>();
        }
        /// <summary>
        /// 精定位X方向偏差阈值
        /// </summary>
        public double ThdPreciseDeltaX { get; set; } = 10;
        /// <summary>
        /// 精定位Y方向偏差阈值
        /// </summary>
        public double ThdPreciseDeltaY { get; set; } = 10;
        /// <summary>
        /// 精定位角度偏差阈值
        /// </summary>
        public double ThdPreciseDeltaR { get; set; } = 5;

        /// <summary>
        /// 残材清晰度最小值
        /// </summary>
        public double ThdMinSharpness { get; set; } = 30;

        /// <summary>
        /// 单目定位偏差阈值X
        /// </summary>
        public double ThdMonoX { get; set; } = 10;
        /// <summary>
        /// 单目定位偏差阈值Y
        /// </summary>
        public double ThdMonoY { get; set; } = 10;
        /// <summary>
        /// 单目定位偏差阈值R
        /// </summary>
        public double ThdMonoR { get; set; } = 5;


        /// <summary>
        /// 左右偏差阈值
        /// </summary>
        public double ThdCSTDevX { get; set; } = 5;
        /// <summary>
        /// 高度偏差阈值
        /// </summary>
        public double ThdCSTHeightDev { get; set; } = 2;
        /// <summary>
        /// 层间距偏差阈值
        /// </summary>
        public double ThdCSTLayerSpacing { get; set; } = 0.02;
        /// <summary>
        /// 左右龙骨高度偏差阈值
        /// </summary>
        public double ThdCSTHeightLRDiff { get; set; } = 1.2;
        /// <summary>
        /// 龙骨间距偏差阈值
        /// </summary>
        public double ThdCSTKeelSpacing { get; set; } = 5;

        /// <summary>
        /// 精定位左右移动拍照的产品长宽阈值
        /// </summary>
        public double ThdPreciseMovePhoto { get; set; } = 400;

        /// <summary>
        /// 插栏基准补偿
        /// </summary>
        public double ComStdCst { get; set; }

        private double _preciseMoveRation = 0.3;
        /// <summary>
        /// 左右拍照时移动的产品参数比例
        /// </summary>
        public double PreciseMoveRatio
        {
            get
            {
                if(_preciseMoveRation < 0.1)
                {
                    return 0.1;
                }
                else if(_preciseMoveRation > 0.9)
                {
                    return 0.9;
                }
                return _preciseMoveRation;
            }
            set
            {
                _preciseMoveRation = value;
            }
        }

        private double _beltSpeed = 5;
        /// <summary>
        /// 皮带线速度
        /// </summary>
        public double BeltSpeed
        {
            get
            {
                return _beltSpeed < 1 ? 5 : _beltSpeed;
            }
            set
            {
                _beltSpeed = value;
            }
        }
    }
}
