﻿using BasicClass;
using System;
using ModulePackage;
using System.Collections.Generic;

namespace Main
{
    public partial class ModelParams
    {
        #region 静态类实例
        //public static ModelParams M_I = new ModelParams();
        #endregion 静态类实例

        #region 定义
        public static List<string> DumpList_Active = new List<string>();
        public static List<string> DumpList_Solid = new List<string>();

        #region 机台硬件配置
        /// <summary>
        /// 机器人安装角度，以x>y^为0度位置，逆时针角度，其值为n*90
        /// </summary>
        public static double BotPlaceAngle => (int)ConfigManager.instance.DirBotEnum;
        /// <summary>
        /// 背光与相机的组合，默认永远处于背光纵向相机朝左这样的配置，则背光纵向为0，横向为90（即逆时针90）
        /// </summary>
        public static double BLPlaceAngle => (int)ConfigManager.instance.DirBLEnum;
        /// <summary>
        /// 相机画面设置当中，画面旋转的角度，顺时针为正
        /// </summary>
        public static double DisplayAngle => (int)ConfigManager.instance.DirDisplayEnum;
        /// <summary>
        /// 图像显示是否做了x镜像
        /// </summary>
        public static bool isMirrorX => ConfigManager.instance.IsMirrorX;
        /// <summary>
        /// 图像显示是否做了y镜像
        /// </summary>
        public static bool isMirrorY => ConfigManager.instance.IsMirrorY;
        /// <summary>
        /// 残材平台上玻璃的放片位置，分为左上，左下，右下，右上-0123
        /// </summary>        
        public static PlatformPlacePos_Enum PlacePos
        {
            get
            {
                PlatformPlacePos_Enum placePos = ConfigManager.instance.PlatformPlacePosEnum;
                if (ConfWastagePlatStation == 2)
                {
                    placePos = (PlatformPlacePos_Enum)(((int)placePos + 2) % 4);
                }

                return placePos;
            }
        }
        /// <summary>
        /// 单电极放片朝向，左右放置为水平即true，竖直放置为false
        /// </summary>
        static bool horizontal => ConfigManager.instance.IsHorizontal;
        /// <summary>
        /// 中片取片工位数
        /// </summary>
        public static int PickStationNum
        {
            get
            {
                return ConfPickStationNum;
            }
        }
        /// <summary>
        /// 背光相机的画面是水平方向还是竖直方向
        /// </summary>
        public static BackLightDisplay_Enum BLDisplayType
        {
            get
            {
                if (DisplayAngle % 180 == 0)
                    return BackLightDisplay_Enum.Horizontal;
                else
                    return BackLightDisplay_Enum.Vertical;
            }
        }
        /// <summary>
        /// 单电极时，默认ok的相机
        /// </summary>
        public static int DefaultOKCamera => 4;
        /// <summary>
        /// 插栏相机朝向
        /// </summary>
        public static DirCstCamera_Enum DirPhoto => ConfigManager.instance.DirCstCameraEnum;
        /// <summary>
        /// 插栏z轴补偿轴 
        /// </summary>
        public static TypeModuleZ_Enum DirZ => ConfigManager.instance.TypeModuleZEnum;
        /// <summary>
        /// 插栏方向
        /// </summary>
        public static DirInsert_Enum DirInsert => ConfigManager.instance.DirInsertEnum;
        /// <summary>
        /// 插栏相机画面显示是否镜像
        /// </summary>
        public static bool CstIsMirrorX => ConfigManager.instance.CstIsMirrorX;
        #endregion


        #region std              
        /// <summary>
        /// 中片行列数
        /// </summary>
        const string key_std_PickRowCol = "std1";
        /// <summary>
        /// 卡塞1补偿
        /// </summary>
        const string key_std_cominsert1 = "std11";
        /// <summary>
        /// 卡塞2补偿
        /// </summary>
        const string key_std_cominsert2 = "std12";
        /// <summary>
        /// 精定位旋转中心标定旋转角度
        /// </summary>
        const string key_std_RCCalibAngle = "std16";
        /// <summary>
        /// 巡边基准补偿
        /// </summary>
        const string key_std_InspTrans = "std2";
        /// <summary>
        /// 卡塞偏差阈值
        /// </summary>
        const string key_std_cstthread = "std10";
        /// <summary>
        /// 机器人速度
        /// </summary>
        const string key_std_RobotSpeed = "std3";
        /// <summary>
        /// 翻转双面分开补偿
        /// </summary>
        const string key_std_PlatHeightCom = "std4";
        /// <summary>
        /// 背光定位基准值
        /// </summary>
        const string key_std_PreciseStdValue = "stdBL";
        /// <summary>
        /// 卡塞基准值
        /// </summary>
        const string key_std_CstStdValue = "stdCst";
        #endregion

        #region adj        
        /// <summary>
        /// 面积比阈值
        /// </summary>
        const string key_adj_AreaRatio = "adj7";
        /// <summary>
        /// 残材检测阈值
        /// </summary>
        const string key_adj_SharpnessRatio = "adj8";
        /// <summary>
        /// 精定位偏差阈值
        /// </summary>
        const string key_adj_precisethread = "adj9";        
        /// <summary>
        /// 插栏1 补偿值
        /// </summary>
        const string key_adj_InsertX1 = "adj11";
        /// <summary>
        /// 插栏2 补偿值
        /// </summary>
        const string key_adj_InsertX2 = "adj12";
        /// <summary>
        /// 巡边前补偿
        /// </summary>
        const string key_adj_insp = "adj6";
        /// <summary>
        /// 皮带线系数调整值
        /// </summary>
        const string key_adj_BeltRatio = "adj1";
        /// <summary>
        /// 取片后短距偏移
        /// </summary>
        const string key_adj_PickOffset = "adj2";
        /// <summary>
        /// 二维码补偿值
        /// </summary>
        const string key_adj_CodeCom = "adj3";
        /// <summary>
        /// 粗定位拍照延时
        /// </summary>
        const string key_adj_DelayRoughPhoto = "adj13";
        /// <summary>
        /// 精定位拍照延时
        /// </summary>
        const string key_adj_DelayPrecisePhoto = "adj14";
        /// <summary>
        /// 残材拍照延时
        /// </summary>
        const string key_adj_DelayResidueInspect = "adj15";
        /// <summary>
        /// 单目相机拍照延时
        /// </summary>
        const string key_adj_DelayMono = "adj16";
        /// <summary>
        /// 卡塞拍照延时
        /// </summary>
        const string key_adj_DelayCST = "adj17";
                           
        /// <summary>
        /// 残材校准计算得到的偏差补正X
        /// </summary>
        const string key_adj_ResidueCalibCom = "adjCalibResidueCom";

        /// <summary>
        /// 单目相机拍照位置1的基准
        /// </summary>
        const string key_adj_MonoStdPos1 = "adjMonoStdPos1";
        /// <summary>
        /// 单目相机拍照位置2的基准
        /// </summary>
        const string key_adj_MonoStdPos2 = "adjMonoStdPos2";
        /// <summary>
        /// 单目相机位置1拍照的旋转中心
        /// </summary>
        const string key_adj_MonoRCPos1 = "adjMonoRCPos1";
        /// <summary>
        /// 单目相机位置2拍照的旋转中心
        /// </summary>
        const string key_adj_MonoRCPos2 = "adjMonoRCPos2";
        #endregion

        #endregion

        #region tool
        /// <summary>
        /// 统一坐标系，目前用于统一所有调整值到视觉坐标系
        /// </summary>
        /// <param name="pt"></param>
        /// <param name="dstAngle"></param>
        /// <param name="srcAngle"></param>
        /// <returns></returns>
        public static Point4D UnifyCoordinate(Point4D pt, double dstAngle, double srcAngle)
        {
            Point2D xy = TransDelta(new Point2D(pt.DblValue1, pt.DblValue2), dstAngle, srcAngle);

            return new Point4D(xy.DblValue1, xy.DblValue2, pt.DblValue3, pt.DblValue4);
        }

        public static Point4D UnifyAdj(Point4D pt)
        {
            return UnifyCoordinate(pt, 0, BotPlaceAngle);
        }
        //机器人和精定位坐标系夹角，机器人转？符合
        public static Point2D TransDelta(Point2D oriPt, double dstAngle, double startAngle)
        {
            return MultMatrix(oriPt, dstAngle - startAngle);
        }

        static Point2D MultMatrix(Point2D pt, double angle)
        {
            double radius = angle / 180 * Math.PI;
            double x = pt.DblValue1 * Math.Cos(radius) - pt.DblValue2 * Math.Sin(radius);
            double y = pt.DblValue1 * Math.Sin(radius) + pt.DblValue2 * Math.Cos(radius);
            return new Point2D(x, y);
        }
        #endregion
    }
}