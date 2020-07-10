using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DealPLC;
using System.Threading;
using System.Threading.Tasks;
using DealFile;
using DealComprehensive;
using Common;
using SetPar;
using ParComprehensive;
using BasicClass;
using Camera;
using System.Collections;
using DealResult;
using DealConfigFile;
using DealCalibrate;
using DealRobot;
using DealMath;
using DealImageProcess;
using BasicComprehensive;
using System.Diagnostics;
using BasicDisplay;
using Main_EX;
using ProjectSPJ;


namespace Main
{
    public partial class DealComprehensiveResult2 : BaseDealComprehensiveResult_Main
    {
        
        #region 定义

  
        #endregion 定义
       
        #region 位置1拍照
        /// <summary>
        /// 
        /// </summary>
        /// <param name="htResult"></param>
        /// <returns></returns>
        public override StateComprehensive_enum DealComprehensiveResultFun1(TriggerSource_enum trigerSource_e, out Hashtable htResult)
        {
            #region 定义
            htResult = g_HtResult;
            //int pos = 1;
            bool blResult = true;

            Stopwatch sw = new Stopwatch();
            sw.Restart();
            #endregion 定义
            try
            {
                StateComprehensive_enum stateComprehensive_e = StateComprehensive_enum.True;
                if (ParModelValue.IsPreciseMovePhoto)
                {
                    stateComprehensive_e = GetPos1(out htResult);
                }
                else
                {
                    stateComprehensive_e = BackLightAngleCalc(CellResultPos1, out htResult);
                }
                blResult = stateComprehensive_e == StateComprehensive_enum.True;
                return stateComprehensive_e;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
                return StateComprehensive_enum.False;
            }
            finally
            {
                if(ParModelValue.IsPreciseMovePhoto)
                {
                    Display(Pos_enum.Pos2, htResult, blResult, sw);
                }
                else
                {
                    Display(Pos_enum.Pos1, htResult, blResult, sw);
                }
            }
        }
        #endregion 位置1拍照

        #region 位置2拍照
        public override StateComprehensive_enum DealComprehensiveResultFun2(TriggerSource_enum trigerSource_e, out Hashtable htResult)
        {
            #region 定义
            htResult = g_HtResult;
            //int pos = 2;
            bool blResult = true;

            Stopwatch sw = new Stopwatch();
            sw.Restart();
            #endregion 定义
            try
            {
                StateComprehensive_enum stateComprehensive_e = StateComprehensive_enum.True;
                if (ParModelValue.IsPreciseMovePhoto)
                {
                    stateComprehensive_e =  GetPos2(out htResult);
                }
                else
                {
                    stateComprehensive_e = BackLightDeviationCalc(CellResultPos1, out htResult);
                }
                blResult = stateComprehensive_e == StateComprehensive_enum.True;
                return stateComprehensive_e;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
                return StateComprehensive_enum.False;
            }
            finally
            {
                if (ParModelValue.IsPreciseMovePhoto)
                {
                    Display(Pos_enum.Pos3, htResult, blResult, sw);
                }
                else
                {
                    Display(Pos_enum.Pos1, htResult, blResult, sw);
                }
            }
        }
        #endregion 位置2拍照

        #region 位置3拍照
        /// <summary>
        /// 
        /// </summary>
        /// <param name="htResult"></param>
        /// <returns></returns>
        public override StateComprehensive_enum DealComprehensiveResultFun3(TriggerSource_enum trigerSource_e, out Hashtable htResult)
        {
            #region 定义
            htResult = g_HtResult;
            //int pos = 3;
            bool blResult = true;

            Stopwatch sw = new Stopwatch();
            sw.Restart();
            #endregion 定义
            try
            {
                return CalibStdValue(0, CellResultPos1, out htResult);
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
                return StateComprehensive_enum.False;
            }
            finally
            {
                #region 显示和日志记录
                Display(Pos_enum.Pos1, htResult, blResult, sw);
                #endregion 显示和日志记录
            }
        }
        #endregion 位置3拍照

        #region 位置4拍照
        public override StateComprehensive_enum DealComprehensiveResultFun4(TriggerSource_enum trigerSource_e,out Hashtable htResult)
        {
            #region 定义
            htResult = g_HtResult;
            //int pos = 4;
            bool blResult = true;

            Stopwatch sw = new Stopwatch();
            sw.Restart();
            #endregion 定义
            try
            {
                return CalibStdValue(1, CellResultPos1, out htResult);
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
                return StateComprehensive_enum.False;
            }
            finally
            {
                #region 显示和日志记录
                Display(Pos_enum.Pos1, htResult, blResult, sw);
                #endregion 显示和日志记录
            }
        }
        #endregion 位置4拍照

        #region 残材校准第一次拍照，101
        public override StateComprehensive_enum DealComprehensiveResultFun_Calib1(TriggerSource_enum triggerSource_e, out Hashtable htResult)
        {
            #region 定义
            htResult = g_HtResult;
            //int pos = 4;
            bool blResult = true;//结果是否正确
            Stopwatch sw = new Stopwatch();
            sw.Restart();
            #endregion 定义

            try
            {
                StateComprehensive_enum stateComprehensive_e = StateComprehensive_enum.True;
                stateComprehensive_e = BackLightAngleCalc(CellResultPos1, out htResult);
                blResult = stateComprehensive_e == StateComprehensive_enum.True;
                return stateComprehensive_e;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
                return StateComprehensive_enum.False;
            }
            finally
            {
                #region 显示和日志记录

                Display(Pos_enum.Pos1, htResult, blResult, sw);

                #endregion 显示和日志记录

            }
        }
        #endregion 残材校准第一次拍照，101

        #region 残材校准第二次拍照，102
        public override StateComprehensive_enum DealComprehensiveResultFun_Calib2(TriggerSource_enum triggerSource_e, out Hashtable htResult)
        {
            #region 定义
            htResult = g_HtResult;
            bool blResult = true;//结果是否正确
            Stopwatch sw = new Stopwatch();
            sw.Restart();
            #endregion 定义

            try
            {
                StateComprehensive_enum stateComprehensive_e = StateComprehensive_enum.True;

                blResult = ResidueCalibBlobDeviation(CellResultPos1, out htResult);

                return stateComprehensive_e;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
                return StateComprehensive_enum.False;
            }
            finally
            {
                #region 显示和日志记录

                Display(Pos_enum.Pos1, htResult, blResult, sw);

                #endregion 显示和日志记录

            }
        }
        #endregion 残材校准第二次拍照，102
    }
}
