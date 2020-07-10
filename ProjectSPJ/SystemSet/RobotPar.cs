using System;
using System.Collections.Generic;
using System.Text;
using BasicClass;
using System.Reflection;

namespace ProjectSPJ
{
    /// <summary>
    /// 机器人参数类，包括，速度，位置等
    /// </summary>
    public sealed class RobotPar : BaseSerializer
    {
        /// <summary>
        /// 静态类实例
        /// </summary>
        public static RobotPar Inst = new RobotPar();

        /// <summary>
        /// 文件保存路径名称，如果不重写则会以基类名称来保存
        /// </summary>
        protected override string NameFile => MethodBase.GetCurrentMethod().DeclaringType.Name;


        /// <summary>
        /// 读取本地配置文件
        /// </summary>
        public static void ReadLocalFile()
        {
            Inst = Inst.ReadLocalFile<RobotPar>();
        }

        #region 机器人速度
        /// <summary>
        /// 高速速度
        /// </summary>
        public double SpeedHigh { get; set; }
        /// <summary>
        /// 低速速度
        /// </summary>
        public double SpeedLow { get; set; }
        /// <summary>
        /// 复位速度
        /// </summary>
        public double SpeedReset { get; set; }
        #endregion 机器人速度

        #region 粗定位取料位
        /// <summary>
        /// 基准取片位X
        /// </summary>
        public double PickPosX { get; set; }
        /// <summary>
        /// 基准取片位Y
        /// </summary>
        public double PickPosY { get; set; }
        /// <summary>
        /// 基准取片位Z
        /// </summary>
        public double PickPosZ { get; set; }
        /// <summary>
        /// 基准取片位R
        /// </summary>
        public double PickPosR { get; set; }

        public static Point4D PickPos
        {
            get
            {
                return new Point4D(Inst.PickPosX, Inst.PickPosY, Inst.PickPosZ, Inst.PickPosR);
            }

        }
        #endregion 粗定位取料位

        #region 精定位拍照位
        /// <summary>
        /// 精定位拍照位X
        /// </summary>
        public double PrecisePhotoPosX { get; set; }
        /// <summary>
        /// 精定位拍照位Y
        /// </summary>
        public double PrecisePhotoPosY { get; set; }
        /// <summary>
        /// 精定位拍照位Z
        /// </summary>
        public double PrecisePhotoPosZ { get; set; }
        /// <summary>
        /// 精定位拍照位R
        /// </summary>
        public double PrecisePhotoPosR { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public static Point4D PrecisePhotoPos
        {
            get
            {
                return new Point4D(Inst.PrecisePhotoPosX, Inst.PrecisePhotoPosY, Inst.PrecisePhotoPosZ, Inst.PrecisePhotoPosR);
            }
        }
        #endregion 精定位拍照位

        #region 残材平台放片位
        /// <summary>
        /// 残材平台放片位X
        /// </summary>
        public double PlatPosX { get; set; }
        /// <summary>
        /// 残材平台放片位Y
        /// </summary>
        public double PlatPosY { get; set; }
        /// <summary>
        /// 残材平台放片位Z
        /// </summary>
        public double PlatPosZ { get; set; }
        /// <summary>
        /// 残材平台放片位R
        /// </summary>
        public double PlatPosR { get; set; }

        /// <summary>
        /// 残材平台1放片位
        /// </summary>
        public static Point4D PlatPutPos
        {
            get
            {
                return new Point4D(Inst.PlatPosX, Inst.PlatPosY, Inst.PlatPosZ, Inst.PlatPosR);
            }
        }

        /// <summary>
        /// 残材平台2放片位置  
        /// </summary>
        public static Point4D Plat2Pos
        {
            get;set;
        }

        #endregion 残材平台放片位

        #region 抛料位
        /// <summary>
        /// 抛料基准位X
        /// </summary>
        public double DumpPosX { get; set; }
        /// <summary>
        /// 抛料基准位Y
        /// </summary>
        public double DumpPosY { get; set; }
        /// <summary>
        /// 抛料基准位Z
        /// </summary>
        public double DumpPosZ { get; set; }
        /// <summary>
        /// 抛料基准位R
        /// </summary>
        public double DumpPosR { get; set; }

        public static Point4D DumpPos
        {
            get
            {
                return new Point4D(Inst.DumpPosX, Inst.DumpPosY, Inst.DumpPosZ, Inst.DumpPosR);
            }
        }
        #endregion 抛料位


        #region 其他

        /// <summary>
        /// 精定位处机器人u轴角度
        /// </summary>
        public double PreciseRobotAngle { get; set; } = 0;
        #endregion
    }
}
