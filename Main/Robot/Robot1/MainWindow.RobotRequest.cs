using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DealRobot;
using BasicClass;
using System.Threading;
namespace Main
{
    partial class MainWindow
    {
        public static bool BlRequestPickPos = false;
        public static bool BlReadyPickPos = false;
        public static bool BlReadyPlatHeightCom = false;
        //public static bool 

        public static bool BlRequestPickOffset = false;

        public static bool BlRequestPrecisePos = false;
        public static bool BlRequestDumpPos = false;
        public static bool BlRequestSpeed = false;
        public void DealRbtRequest()
        {
            try
            {
                ShowState("已启动机器人请求数据线程。");
            }
            catch
            {

            }
        }
    }
}
