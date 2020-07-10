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
using Main_EX;

namespace Main
{
    partial class BaseDealComprehensiveResult_Main
    {
        /// <summary>
        /// 接收触发信号，计算校准,自定义校准,重载，命令字101~999有效
        /// </summary>
        /// <param name="trigerSource_e">触发源</param>
        /// <param name="i">指令</param>
        /// <returns></returns>
        public override StateComprehensive_enum DealComprehensiveResultFun_CalibSelf(TriggerSource_enum trigerSource_e, int cmd, out Hashtable htResult)
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

                switch (cmd)
                {
                    case 101:
                        stateComprehensive_e = DealComprehensiveResultFun_Calib1(trigerSource_e, out htResult);
                        break;

                    case 102:
                        stateComprehensive_e = DealComprehensiveResultFun_Calib2(trigerSource_e, out htResult);
                        break;

                    case 103:
                        stateComprehensive_e = DealComprehensiveResultFun_Calib3(trigerSource_e, out htResult);
                        break;

                    case 104:
                        stateComprehensive_e = DealComprehensiveResultFun_Calib4(trigerSource_e, out htResult);
                        break;

                }

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
