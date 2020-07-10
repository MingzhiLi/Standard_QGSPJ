using BasicClass;
using DealConfigFile;


namespace Main
{
    public partial class ModelParams
    {
        #region packing
        #region config

        /// <summary>
        /// 配方-玻璃X
        /// </summary>
        public static double ConfGlassX
        {
            get
            {                
                return ParConfigPar.P_I.ParProduct_L[(int)RecipeRegister.GlassX].DblValue;
            }
        }
        /// <summary>
        /// 配方-玻璃Y
        /// </summary>
        public static double ConfGlassY
        {
            get
            {                
                return ParConfigPar.P_I.ParProduct_L[(int)RecipeRegister.GlassY].DblValue;
            }
        }
        /// <summary>
        /// 配方-二维码X
        /// </summary>
        public static double ConfQrCodeX
        {
            get
            {                
                return ParConfigPar.P_I.ParProduct_L[(int)RecipeRegister.QrCodeX].DblValue;
            }
        }
        /// <summary>
        /// 配方-二维码Y
        /// </summary>
        public static double ConfQrCodeY
        {
            get
            {                
                return ParConfigPar.P_I.ParProduct_L[(int)RecipeRegister.QrCodeY].DblValue;
            }
        }
        /// <summary>
        /// 配方-Mark1X
        /// </summary>
        public static double ConfMark1X
        {
            get => ParConfigPar.P_I.ParProduct_L[(int)RecipeRegister.Mark1X].DblValue;
        }
        /// <summary>
        /// 配方-Mark1Y
        /// </summary>
        public static double ConfMark1Y
        {
            get => ParConfigPar.P_I.ParProduct_L[(int)RecipeRegister.Mark1Y].DblValue;
        }
        /// <summary>
        /// 配方-Mark2X
        /// </summary>
        public static double ConfMark2X
        {
            get => ParConfigPar.P_I.ParProduct_L[(int)RecipeRegister.Mark2X].DblValue;
        }
        /// <summary>
        /// 配方-Mark2Y
        /// </summary>
        public static double ConfMark2Y
        {
            get => ParConfigPar.P_I.ParProduct_L[(int)RecipeRegister.Mark2Y].DblValue;
        }
        /// <summary>
        /// 配方-上电极宽度
        /// </summary>
        public static double ConfTopElectrode
        {
            get
            {                
                return ParConfigPar.P_I.ParProduct_L[(int)RecipeRegister.TopE].DblValue;
            }
        }
        /// <summary>
        /// 配方-下电极宽度
        /// </summary>
        public static double ConfBottomElectrode
        {
            get
            {                
                return ParConfigPar.P_I.ParProduct_L[(int)RecipeRegister.BottomE].DblValue;
            }
        }
        /// <summary>
        /// 配方-左电极宽度
        /// </summary>
        public static double ConfLeftElectrode
        {
            get
            {             
                return ParConfigPar.P_I.ParProduct_L[(int)RecipeRegister.LeftE].DblValue;
            }
        }
        /// <summary>
        /// 配方-右电极宽度
        /// </summary>
        public static double ConfRightElectrode
        {
            get
            {                
                return ParConfigPar.P_I.ParProduct_L[(int)RecipeRegister.RightE].DblValue;
            }
        }
        /// <summary>
        /// 配方-玻璃厚度
        /// </summary>
        public static double ConfGlassThicknes
        {
            get
            {                
                return 0;//ParConfigPar.P_I.ParProduct_L[(int)RecipeRegister.Thickness].DblValue;
            }
        }
        /// <summary>
        /// 配方-插栏方向
        /// </summary>
        public static double ConfInsertDirection
        {
            get
            {                
                return ParConfigPar.P_I[(int)RecipeRegister.DIR_Insert].DblValue;
            }
        }
        /// <summary>
        /// 配方-残材平台放片方向
        /// </summary>
        public static double ConfWastageDirection
        {
            get
            {
                return 0;// ParConfigPar.P_I[key_conf_WastageDirection].DblValue;
            }
        }
        /// <summary>
        /// 配方-残材平台放片工位号
        /// </summary>
        public static double ConfWastagePlatStation
        {
            get
            {
                return 1;// ParConfigPar.P_I[(int)RecipeRegister.PlatStaionNo].DblValue;
            }
        }
        /// <summary>
        /// 配方-龙骨列数
        /// </summary>
        public static int KeelCol
        {
            get
            {
                return ConfCSTCol + 1;
            }
        }
        /// <summary>
        /// 配方-插栏列数
        /// </summary>
        public static int ConfCSTCol
        {
            get
            {                
                return (int)ParConfigPar.P_I.ParProduct_L[(int)RecipeRegister.CSTCols].DblValue;
            }
        }
        /// <summary>
        /// 配方-插栏行数
        /// </summary>
        public static int ConfCSTRow
        {
            get
            {                
                return (int)ParConfigPar.P_I.ParProduct_L[(int)RecipeRegister.CSTRows].DblValue;
            }
        }
        /// <summary>
        /// 配方-龙骨间距
        /// </summary>
        public static double ConfKeelInterval
        {
            get
            {                
                return ParConfigPar.P_I.ParProduct_L[(int)RecipeRegister.KeelInterval].DblValue;
            }
        }
        /// <summary>
        /// 配方-第一列龙骨位置
        /// </summary>
        public static double ConfCol1Interval
        {
            get
            {
                return ParConfigPar.P_I.ParProduct_L[(int)RecipeRegister.DisCol1].DblValue;
            }
        }
        /// <summary>
        /// 配方-巡边放片方向
        /// </summary>
        public static double ConfInspDirection
        {
            get
            {
                return 0;//ParConfigPar.P_I.ParProduct_L[(int)RecipeRegister.InspDir].DblValue;
            }
        }
        /// <summary>
        /// 配方-皮带线放片方向
        /// </summary>
        public static double ConfBeltDirection
        {
            get
            {                
                //此处以抛料方向作为皮带线方向
                return ParConfigPar.P_I.ParProduct_L[(int)RecipeRegister.DIR_Dump].DblValue;
            }
        }
        /// <summary>
        /// 配方-龙骨层间距
        /// </summary>
        public static double ConfLayerSpacing
        {
            get
            {                
                return ParConfigPar.P_I.ParProduct_L[(int)RecipeRegister.KeelSpacing].DblValue;
            }
        }
        /// <summary>
        /// 配方-中片取片总数
        /// </summary>
        public static double ConfPickSum
        {
            get
            {                
                return ParConfigPar.P_I.ParProduct_L[(int)RecipeRegister.SUMPick].DblValue;
            }
        }

        public static int ConfPickStationNum
        {
            get
            {
                return (int)ParConfigPar.P_I[(int)RecipeRegister.PickStationNum].DblValue;
            }
        }
        #endregion

        #region setwork
        /// <summary>
        /// 运行设定模式-是否记录中间数据
        /// </summary>
        public static bool IfRecordData
        {
            get
            {
                return ParSetWork.P_I.WorkSelect_L[(int)RunningMode.RecordData].BlSelect;
            }
        }

        /// <summary>
        /// 标定时是否矫正角度
        /// </summary>
        public static bool IfAdjustAngle
        {
            get
            {
                return true;// ParSetWork.P_I.WorkSelect_L[4].BlSelect;
            }
        }

        /// <summary>
        /// 屏蔽相机3
        /// </summary>
        public static bool IfPassResidue1
        {
            get
            {
                return ParSetWork.P_I.WorkSelect_L[(int)RunningMode.PassResidue1].BlSelect;
            }
        }

        /// <summary>
        /// 屏蔽相机4
        /// </summary>
        public static bool IfPassResidue2
        {
            get
            {
                return ParSetWork.P_I.WorkSelect_L[(int)RunningMode.PassResidue2].BlSelect;
            }
        }

        /// <summary>
        /// 屏蔽相机4
        /// </summary>
        public static bool IfPassResidue3
        {
            get
            {
                return ParSetWork.P_I.WorkSelect_L[(int)RunningMode.PassResidue3].BlSelect;
            }
        }

        #endregion

        #region adj
        /// <summary>
        /// 调整值-巡边平台1调整值xyzr
        /// </summary>
        static Point4D adjInspPosPlat1
        {
            get
            {
                return ParBotAdj.P_I[(int)BotAdj.Platform1];
            }
        }
        /// <summary>
        /// 调整值-巡边平台2调整值xyzr
        /// </summary>
        static Point4D adjInspPosPlat2
        {
            get
            {
                return ParBotAdj.P_I[(int)BotAdj.Platform2];
            }
        }
        
        /// <summary>
        /// 调整值-清晰度阈值
        /// </summary>
        static double adjSharpnessThread1
        {
            get
            {               
                string key = key_adj_SharpnessRatio;
                return ParAdjust.Value1(key);
            }
        }

        /// <summary>
        /// 调整值-清晰度阈值
        /// </summary>
        static double adjSharpnessThread2
        {
            get
            {
                string key = key_adj_SharpnessRatio;
                return ParAdjust.Value2(key);
            }
        }
        /// <summary>
        /// 调整值-残边平台1补偿Xyzr
        /// </summary>
        static Point4D adjPosWastagePlat1
        {
            get
            {
                return UnifyAdj(ParBotAdj.P_I[(int)BotAdj.Platform1]);
            }
        }
        /// <summary>
        /// 调整值-残边平台1补偿Xyzr
        /// </summary>
        static Point4D adjPosWastagePlat2
        {
            get
            {
                return UnifyAdj(ParBotAdj.P_I[(int)BotAdj.Platform2]);
            }
        }
        
        /// <summary>
        /// 调整值-抛料位补偿
        /// </summary>
        static Point4D adjDumpPos
        {
            get
            {
                return UnifyAdj(ParBotAdj.P_I[(int)BotAdj.DumpPos]);
            }
        }

       


        /// <summary>
        /// 皮带线运行时间
        /// </summary>
        static double adjBeltRatio
        {
            get
            {
                string key = key_adj_BeltRatio;
                return ParAdjust.Value1(key);
            }
        }

        /// <summary>
        /// 单目相机位置1基准，像素坐标
        /// </summary>
        public static Point2D PMonoStdPos1
        {
            get
            {
                return new Point2D(480, 640);
                string key = key_adj_MonoStdPos1;
                return new Point2D(ParAdjust.Value1(key), ParAdjust.Value2(key));
            }
            set
            {
                string key = key_adj_MonoStdPos1;
                ParAdjust.SetValue1(key, value.DblValue1);
                ParAdjust.SetValue2(key, value.DblValue2);
            }
        }
        
        /// <summary>
        /// 单目相机位置2基准，像素坐标
        /// </summary>
        public static Point2D PMonoStdPos2
        {
            get
            {
                return new Point2D(480, 640);
                string key = key_adj_MonoStdPos2;
                return new Point2D(ParAdjust.Value1(key), ParAdjust.Value2(key));
            }
            set
            {
                string key = key_adj_MonoStdPos2;
                ParAdjust.SetValue1(key, value.DblValue1);
                ParAdjust.SetValue2(key, value.DblValue2);
            }
        }

        /// <summary>
        /// 单目相机位置1旋转中心,像素坐标
        /// </summary>
        public static Point2D PMonoRCPos1
        {
            get
            {
                
                string key = key_adj_MonoRCPos1;
                return new Point2D(ParAdjust.Value1(key), ParAdjust.Value2(key));
            }
            set
            {
                string key = key_adj_MonoRCPos1;
                ParAdjust.SetValue1(key, value.DblValue1);
                ParAdjust.SetValue2(key, value.DblValue2);
            }
        }

        /// <summary>
        /// 单目相机位置2旋转中心，像素坐标
        /// </summary>
        public static Point2D PMonoRCPos2
        {
            get
            {
                string key = key_adj_MonoRCPos2;
                return new Point2D(ParAdjust.Value1(key), ParAdjust.Value2(key));
            }
            set
            {
                string key = key_adj_MonoRCPos2;
                ParAdjust.SetValue1(key, value.DblValue1);
                ParAdjust.SetValue2(key, value.DblValue2);
            }
        }
        #endregion

        #endregion  
    }
}
