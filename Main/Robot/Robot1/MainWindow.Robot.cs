using System;
using System.Threading.Tasks;
using DealPLC;
using DealRobot;
using System.Threading;
using BasicClass;
using DealComprehensive;
using Main_EX;
using ProjectSPJ;

namespace Main
{
    partial class MainWindow
    {
        #region 机器人HomeThrow
        /// <summary>
        /// 机器人复位完成
        /// </summary>
        /// <param name="i"></param>
        protected override void L_I_RobotReset_event(int i)
        {
            try
            {
                ShowState("机器人复位完成");
                MainCom.M_I.ResetRobot = true;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }
        /// <summary>
        /// 机器人回到Home点
        /// </summary>
        /// <param name="i"></param>
        protected override void L_I_RobotHome_event(int i)
        {
            try
            {
                ShowState("机器人回到Home点");
                MainCom.M_I.HomeRobot = true;               
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }

        /// <summary>
        /// 抛料
        /// </summary>
        /// <param name="i"></param>
        protected override void L_I_RobotThrow_event(int i)
        {
            try
            {
                ShowState("机器人进行抛料");
                MainCom.M_I.HomeRobot = true;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }
        #endregion 机器人HomeThrow

        public override void LogicRobot_Inst_ConfigRobot_event()
        {
            ShowState("写入机器人精定位拍照位1");
            LogicRobot.L_I.WriteRobotCMD(ParModelValue.PrecisePhotoPos1, LogicRobot.RbtCmdPrecisePhotoPos, ShowState);

            ShowState("写入机器人取片后位移距离");
            LogicRobot.L_I.WriteRobotCMD(ParModelValue.MoveDis, LogicRobot.RbtCmdPickOffset, ShowState);

            ShowState("写入机器人抛料位");
            LogicRobot.L_I.WriteRobotCMD(RobotPar.DumpPos, LogicRobot.RbtCmdDumpPos, ShowState);

            ShowState("写入机器人运行速度");
            LogicRobot.L_I.WriteRobotCMD(new Point4D(RobotPar.Inst.SpeedHigh, RobotPar.Inst.SpeedLow, RobotPar.Inst.SpeedReset, 0),
                                         LogicRobot.RbtCmdSpeed, ShowState);
            ShowState("写入AB面高度补偿");
            LogicRobot.L_I.WriteRobotCMD(new Point4D(ParModelValue.Inst.ComPlatZ1, ParModelValue.Inst.ComPlatZ2, 0, 0),
                                         LogicRobot.RbtCmdPlatCom, ShowState);

        }

        #region 东芝——请求数据
        public override void SendPrecisePos()
        {
            ShowState("机器人请求精定位拍照位");
            //LogicRobot.L_I.WriteRobotCMD(ModelParams.PrecisePos, LogicRobot.RbtCmdPrecisePhotoPos, ShowState);
        }
        public override void SendPlatHeightCom()
        {
            ShowState("机器人请求放片高度补偿");
            //LogicRobot.L_I.WriteRobotCMD(new Point4D(), LogicRobot.RbtCmdPlatCom, ShowState);
        }
        public override void SendPickOffset()
        {
            ShowState("机器人请求取片后位移距离");
            //LogicRobot.L_I.WriteRobotCMD(ModelParams.AdjPickOffset, LogicRobot.RbtCmdPickOffset, ShowState);
        }
        public override void SendDumpPos()
        {
            ShowState("机器人请求抛料位");
            //LogicRobot.L_I.WriteRobotCMD(ModelParams.DumpPos, LogicRobot.RbtCmdDumpPos,ShowState);
        }
        public override void SendSpeed()
        {
            ShowState("机器人请求速度");
            //LogicRobot.L_I.WriteRobotCMD(new Point4D(ModelParams.stdRobotAutoSpeed, ModelParams.stdRobotLowSpeed, ModelParams.stdRobotResetSpeed, 0),
            //                             LogicRobot.RbtCmdSpeed, ShowState);
        }

        public override void SendShakeHand()
        {
            ShowState("机器人请求握手！");
            LogicRobot.L_I.WriteRobotCMD(LogicRobot.RbtCmdShake,ShowState);
        }
        #endregion
    }
}
