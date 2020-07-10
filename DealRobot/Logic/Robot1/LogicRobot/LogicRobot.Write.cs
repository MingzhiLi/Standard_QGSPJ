using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;
using System.Threading.Tasks;
using System.Threading;
using DealFile;
using BasicClass;
using DealConfigFile;

namespace DealRobot
{
    partial class LogicRobot
    {
        #region 定义
        public string Head
        {
            get
            {
                string head = "";
                if (ParSetRobot.P_I.TypeRobot_e == TypeRobot_enum.YAMAH_Ethernet
                    || ParSetRobot.P_I.TypeRobot_e == TypeRobot_enum.YAMAH_Serial)
                {
                    head = "P0= ";
                }
                return head;
            }
        }

        //List
        public List<string> ParRobotCom_L = new List<string>();//公共参数

        public List<Point4D> ParRobotCom_P4L = new List<Point4D>();//公共参数

        //事件
        public event Action ConfigRobot_event;

        Mutex g_MtWrite = new Mutex();
        #endregion 定义

        #region 机器人配置参数
        /// <summary>
        /// 向机器人发送配置参数
        /// </summary>
        /// <returns></returns>
        public void WriteConfigRobot()
        {
            try
            {
                ConfigRobot_event();
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }

        #endregion 机器人配置参数

        #region 写入机器人数据
        void WriteRobot(string value, StrAction strAction)
        {
            ParLogicRobot.P_I.isReadOK = false;
            if (RegeditRobot.R_I.BlOffLineRobot)
            {
                return;
            }
            g_MtWrite.WaitOne();
            try
            {
                string cmd = value;
                g_PortRobotBase.WriteData(cmd);
                //strAction("已写入数据" + cmd);
            }
            catch (Exception ex)
            {

            }
            g_MtWrite.ReleaseMutex();
        }

        /// <summary>
        /// 发送命令
        /// </summary>
        /// <param name="cmd">命令</param>
        public void WriteRobotCMD(string cmd, StrAction strAction)
        {
            try
            {
                string value = Head
                             + "0.0" + " "
                             + "0.0" + " "
                             + "0.0" + " "
                             + "0.0" + " "
                             + "0.0" + " "                              
                             + cmd;
                WriteRobot(value, strAction);
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }

        /// <summary>
        /// 发送一个点位加命令
        /// </summary>
        /// <param name="point">点数据</param>
        /// <param name="cmd">命令</param>
        public void WriteRobotCMD(Point4D point, string cmd, StrAction strAction)
        {
            try
            {
                string value = Head
                    + point.DblValue1.ToString("f2") + " "
                    + point.DblValue2.ToString("f2") + " "
                    + point.DblValue3.ToString("f2") + " "
                    + point.DblValue4.ToString("f2") + " "
                    + "0.0" + " "
                    + cmd;
                WriteRobot(value,strAction);
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }

        }
        #endregion 写入机器人
    }
}
