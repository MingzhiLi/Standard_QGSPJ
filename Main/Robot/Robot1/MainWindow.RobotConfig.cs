using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DealRobot;
using System.Threading;
using System.Threading.Tasks;
using BasicClass;
using DealLog;
using DealPLC;
using DealConfigFile;
using ProjectSPJ;

namespace Main
{
    partial class MainWindow
    {
        /// <summary>
        /// 给机器人发送配置参数
        /// </summary>
        public override void ConfigRobot_Task()
        {
            try
            {
                if (ParSetRobot.P_I.TypeRobot_e == TypeRobot_enum.Null)
                {
                    return;
                }
                if (RegeditRobot.R_I.BlOffLineRobot)
                {
                    return;
                }

                //发送参数
                Task task = new Task(LogicRobot.L_I.WriteConfigRobot);
                task.Start();
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }
       
    }
}
