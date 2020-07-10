using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;
using DealFile;
using BasicClass;
using DealRobot;
using DealConfigFile;
using DealComInterface;

namespace DealRobot
{
    partial class LogicRobot
    {
        #region 定义
        //int 
        public int g_intCameraNo = 1;//相机序号

        #region 事件
        public event StrAction DataError_event;//数据超出范围,方法，当前值，标准值
        #endregion 事件
        #endregion 定义

      
    }
}
