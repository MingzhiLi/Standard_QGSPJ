using BasicClass;
using DealPLC;
using ProjectSPJ;
using System;

namespace Main
{
    /// <summary>
    /// PLC的相关定义和实现都在WinInitMain类里面 20181219-zx
    /// </summary>
    partial class MainWindow
    {
        #region 重启外部通信
        protected override void L_I_RestartCommunicate_event(TriggerSource_enum trrigerSource_e, int i)
        {
            try
            {
                switch (i)
                {
                    case 1:
                        ShowState("PLC通知重启机器人");
                        //BaseDealComprehensiveResult_Main.BlReadyPickPos = false;
                        RobotReStart();
                        break;

                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }
        #endregion 重启外部通信

        #region PLC触发响应
        /// <summary>
        /// 报警
        /// </summary>
        /// <param name="trrigerSource_e"></param>
        /// <param name="i"></param>
        protected override void LogicPLC_Inst_PLCAlarm_event(TriggerSource_enum trrigerSource_e, int i)
        {
            try
            {
                ShowState("设备发送报警信息!");
            }
            catch (Exception ex)
            {

            }
        }

        /// <summary>
        /// 物料信息
        /// </summary>
        /// <param name="trrigerSource_e"></param>
        /// <param name="i"></param>
        protected override void L_I_PLCMaterial_event(TriggerSource_enum trrigerSource_e, int i)
        {
            try
            {

                ShowState("设备发送物料信息!");
            }
            catch (Exception ex)
            {

            }
        }

        /// <summary>
        /// 语音信息
        /// </summary>
        /// <param name="trrigerSource_e"></param>
        /// <param name="i"></param>
        protected override void L_I_VoiceState_event(TriggerSource_enum trrigerSource_e, int i)
        {
            try
            {
                ShowVoice(i);
            }
            catch (Exception ex)
            {

            }
        }

        /// <summary>
        /// 设备状态
        /// </summary>
        /// <param name="trrigerSource_e"></param>
        /// <param name="intState"></param>
        protected override void LogicPLC_Inst_PLCState_event(TriggerSource_enum trrigerSource_e, int intState)
        {
            try
            {

            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }

        /// <summary>
        /// 机器人状态
        /// </summary>
        /// <param name="trrigerSource_e"></param>
        /// <param name="intState"></param>
        protected override void LogicPLC_Inst_RobotState_event(TriggerSource_enum trrigerSource_e, int intState)
        {

        }

        /// <summary>
        /// 数据超限
        /// </summary>
        /// <param name="str"></param>
        protected override void L_I_WriteDataOverFlow(string str)
        {
            ShowAlarm("PLC输出数据超出范围");

            LogicPLC.L_I.PCAlarm();
        }
        #endregion PLC触发响应

        #region PLC换型相关
        /// <summary>
        /// 换型的时候写入PLC的值
        /// </summary>
        public override void WritePLCModelPar()
        {
            try
            {

                //判断配方有没有输错
                VerifyRecipe();

                LogicPLC.L_I.WriteRegData2((int)DataRegister2.WidthAtPlat, ParModelValue.WastageX);
                LogicPLC.L_I.WriteRegData2((int)DataRegister2.HeightAtPlat, ParModelValue.WastageY);
                ShowState(string.Format("平台处玻璃X,Y：{0},{1}", ParModelValue.WastageX.ToString("f3"), ParModelValue.WastageY.ToString("f3")));
                LogicPLC.L_I.WriteRegData2((int)DataRegister2.CodeXAtPlat,  ParModelValue.CodeXInWastage);
                LogicPLC.L_I.WriteRegData2((int)DataRegister2.CodeYAtPlat, ParModelValue.CodeYInWastage);
                ShowState(string.Format("平台处二维码位置：{0},{1}", ParModelValue.CodeXInWastage.ToString("f3"), ParModelValue.CodeYInWastage.ToString("f3")));
                LogicPLC.L_I.WriteRegData2((int)DataRegister2.Mark1XAtPlat, 0);// ModelParams.PMonoPhotoMark1.DblValue1);
                LogicPLC.L_I.WriteRegData2((int)DataRegister2.Mark1YAtPlat, 0);// ModelParams.PMonoPhotoMark1.DblValue2);
                ShowState("平台处Mark1位置：" + ModelParams.PMonoPhotoMark1);
                LogicPLC.L_I.WriteRegData2((int)DataRegister2.Mark2XAtPlat, 0);// ModelParams.PMonoPhotoMark2.DblValue1);
                LogicPLC.L_I.WriteRegData2((int)DataRegister2.Mark2YAtPlat, 0);//ModelParams.PMonoPhotoMark2.DblValue2);
                ShowState("平台处Mark2位置：" + ModelParams.PMonoPhotoMark2);
                ShowState(string.Format("残才平台玻璃角度:{0}", ParModelValue.WastageAngle));
                LogicPLC.L_I.WriteRegData2((int)DataRegister2.TopEAtPlat, ParModelValue.GetCurElectordeWidth(0));
                ShowState(string.Format("残才平台上电极宽度:{0}", ParModelValue.GetCurElectordeWidth(0)));
                LogicPLC.L_I.WriteRegData2((int)DataRegister2.LeftEAtPlat, ParModelValue.GetCurElectordeWidth(1));
                ShowState(string.Format("残才平台左电极宽度:{0}", ParModelValue.GetCurElectordeWidth(1)));
                LogicPLC.L_I.WriteRegData2((int)DataRegister2.BottomEAtPlat, ParModelValue.GetCurElectordeWidth(2));
                ShowState(string.Format("残才平台下电极宽度:{0}", ParModelValue.GetCurElectordeWidth(2)));
                LogicPLC.L_I.WriteRegData2((int)DataRegister2.RightEAtPlat, ParModelValue.GetCurElectordeWidth(3));
                ShowState(string.Format("残才平台右电极宽度:{0}", ParModelValue.GetCurElectordeWidth(3)));
                LogicPLC.L_I.WriteRegData2((int)DataRegister2.PlatAngle, (-ParModelValue.WastageAngle + 360) % 360);
                LogicPLC.L_I.WriteRegData1((int)DataRegister1.BeltRatioX, ParModelValue.Inst.BeltTimeX);
                ShowState(string.Format("皮带线移动时间X:{0}，距离：{1}", ParModelValue.Inst.BeltTimeX, ParModelValue.Inst.BeltDisX));
                LogicPLC.L_I.WriteRegData1((int)DataRegister1.BeltRatioY, ParModelValue.Inst.BeltTimeY);
                ShowState(string.Format("皮带线移动时间Y:{0}, 距离：{1}", ParModelValue.Inst.BeltTimeY, ParModelValue.Inst.BeltDisY));
                ShowState("机器人取片角度：" + ParModelValue.PickAngle);
                ShowState("残材平台玻璃角度：" + ParModelValue.WastageAngle);
                ShowState("残材平台机器人角度:" + ParModelValue.WastageRobotAngle);
                ShowState("残材平台无偏差放片坐标：" + ParModelValue.PosWastagePlat1);


                LogicPLC.L_I.WriteRegData1(3, 1);

            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }

        public void VerifyRecipe()
        {
            int cnt = 0;
            bool blResult = true;
            string errorInfo = string.Empty;
            foreach (var item in ModelParams.ElectrodeArray)
            {
                if (item != 0)
                    cnt++;
                if (cnt > 2)
                {
                    blResult = false;
                    ShowAlarm("配方中电极宽度出错");
                    errorInfo += "配方中电极宽度出错!\n";
                    break;
                }
            }

            //if(!(ModelParams.ConfLayerSpacing>0))
            //{
            //    blResult = false;
            //    ShowAlarm("配方中卡塞层间距错误");
            //    errorInfo += "卡塞层间距错误!\n";
            //}

            if(!blResult)
            {                
                LogicPLC.L_I.PCAlarm();
            }
                
        }
        #endregion PLC换型相关
    }
}
