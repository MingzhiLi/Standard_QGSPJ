using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DealPLC;
using System.Threading;
using System.Threading.Tasks;
using DealFile;
using DealComprehensive;
using DealRobot;
using Common;
using ParComprehensive;
using BasicClass;
using BasicComprehensive;
using Camera;
using System.Collections;
using DealComInterface;
using DealResult;
using DealLog;
using DealAlgorithm;
using System.IO;
using DealConfigFile;
using System.Windows;
using DealCalibrate;


namespace Main_EX
{
    /// <summary>
    ///校准的触发响应函数 20181111-zx
    /// </summary>
    partial class BaseDealComprehensiveResult
    {
        #region 定义
       
        #endregion 定义

        /// <summary>
        /// 接收触发信号，计算校准,自定义校准
        /// </summary>
        /// <param name="trigerSource_e">触发源</param>
        /// <param name="i">指令</param>
        /// <returns></returns>
        public virtual StateComprehensive_enum DealComprehensiveResultFun_CalibSelf(TriggerSource_enum trigerSource_e, int i, out Hashtable htResult)
        {
            htResult = null;
            try
            {
                if (ParStateSoft.StateMachine_e != StateMachine_enum.Calib)
                {
                    if (WinMsgBox.ShowMsgBox("软件非校准模式，不允许进行校准触发，是否切换到校准模式"))
                    {
                        //校准模式
                        ParStateSoft.StateMachine_e = StateMachine_enum.Calib;
                        if (Fun_SoftState != null)
                        {
                            Fun_SoftState();
                        }
                    }
                }
                StateComprehensive_enum stateComprehensive_e = StateComprehensive_enum.False;

              

                return stateComprehensive_e;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
                return StateComprehensive_enum.False;
            }
        }

    }

}
