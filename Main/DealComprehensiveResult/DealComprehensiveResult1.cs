using BasicClass;
using DealComprehensive;
using System;
using System.Collections;
using System.Diagnostics;
using System.Collections.Generic;

namespace Main
{
    public partial class DealComprehensiveResult1 : BaseDealComprehensiveResult_Main
    {
        #region 定义
        //double            



        #endregion 定义

        #region 位置1拍照
        /// <summary>
        /// 
        /// </summary>
        public override StateComprehensive_enum DealComprehensiveResultFun1(TriggerSource_enum trigerSource_e, out Hashtable htResult)
        {
            #region 定义
            htResult = g_HtResult;
            PosNow_e = Pos_enum.Pos1;//当前位置
            bool blResult = true;

            Stopwatch sw = new Stopwatch();
            //sw.Restart();
            #endregion 定义
            try
            {
                return CalcPickPos(1, CellResultPos1, out htResult);                
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
        #endregion 位置1拍照

        #region 位置2拍照
        public override StateComprehensive_enum DealComprehensiveResultFun2(TriggerSource_enum trigerSource_e, out Hashtable htResult)
        {
            #region 定义
            htResult = g_HtResult;
            PosNow_e = Pos_enum.Pos2;//当前位置
            bool blResult = true;//结果是否正确
            Stopwatch sw = new Stopwatch();
            sw.Restart();
            #endregion 定义

            try
            {
                //RefreshStatistics();
                return CalcPickPos(2, CellResultPos1, out htResult);
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
            bool blResult = true;//结果是否正确
            Stopwatch sw = new Stopwatch();
            sw.Restart();
            #endregion 定义
            try
            {
                //   StateComprehensive_enum stateComprehensive_e = g_BaseDealComprehensive.DealComprehensivePosNoDisplay(g_UCDisplayCamera, g_HtUCDisplay, Pos_enum.Pos3, out htResult);

                return CalcPickPos(3, CellResultPos1, out htResult);
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
        public override StateComprehensive_enum DealComprehensiveResultFun4(TriggerSource_enum trigerSource_e, out Hashtable htResult)
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
                //    StateComprehensive_enum stateComprehensive_e = g_BaseDealComprehensive.DealComprehensivePosNoDisplay(g_UCDisplayCamera, g_HtUCDisplay, Pos_enum.Pos4, out htResult);

                return CalcPickPos(4, CellResultPos1, out htResult);
                //return StateComprehensive_enum.True;
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
            PosNow_e = Pos_enum.Pos1;//当前位置
            bool blResult = true;

            Stopwatch sw = new Stopwatch();
            //sw.Restart();
            #endregion 定义
            try
            {
                //RefreshStatistics();
                return CalcPickPos(1, CellResultPos1, out htResult);

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
    }
}
