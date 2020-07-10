using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DealRobot
{
    partial class LogicRobot
    {
        #region bot cmd
        /// <summary>
        /// 粗定位取片位置计算结果
        /// </summary>
        public static string RbtCmdPickPos { get => "11"; }
        /// <summary>
        /// 皮带线取片
        /// </summary>
        public static string RbtCmdBeltPick { get => "12"; }
        /// <summary>
        /// 机器人取片成功后进行的短距离位移
        /// </summary>
        public static string RbtCmdPickOffset { get => "14"; }
        /// <summary>
        /// 取片直接抛料
        /// </summary>
        public static string RbtCmdPickDump { get => "19"; }
        /// <summary>
        /// 精定位第一次拍照(角度计算)结果
        /// </summary>
        public static string RbtCmdPreciseAngle { get => "21"; }
        /// <summary>
        /// 精定位第二次拍照（偏差计算）结果
        /// </summary>
        public static string RbtCmdPreciseResult { get => "22"; }
        public static string RbtCmdPreciseCalib1 { get => "23"; }
        public static string RbtCmdPreciseCalib2 { get => "24"; }
        public static string RbtCmdPreciseAutoCalib { get => "25"; }
        /// <summary>
        /// 残材平台AB面高度补偿
        /// </summary>
        public static string RbtCmdPlatCom { get => "26"; }
        /// <summary>
        /// 精定位拍照失败 
        /// </summary>
        public static string RbtCmdPreciseFailed { get => "29"; }
        /// <summary>
        /// 精定位拍照位
        /// </summary>
        public static string RbtCmdPrecisePhotoPos { get => "500"; }
        /// <summary>
        /// 抛料位
        /// </summary>
        public static string RbtCmdDumpPos { get => "501"; }
        /// <summary>
        /// 机器人速度
        /// </summary>
        public static string RbtCmdSpeed { get => "502"; }

        /// <summary>
        /// 机器人握手
        /// </summary>
        public static string RbtCmdShake { get => "10000"; }
        /// <summary>
        /// 机器人空跑
        /// </summary>
        public static string RbtCmdNullRun { get => "10001"; }
        /// <summary>
        /// 机器人退出空跑
        /// </summary>
        public static string RbtCmdExitNullRun { get => "10002"; }

        #endregion
    }
}
