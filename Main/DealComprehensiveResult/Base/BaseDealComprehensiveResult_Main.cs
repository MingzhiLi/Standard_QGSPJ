using BasicClass;
using CalibDataManager;
using DealCalibrate;
using Main_EX;
using System;
using DealConfigFile;

namespace Main
{
    /// <summary>
    /// 继承于Main_EX中基类
    /// </summary>
    public partial class BaseDealComprehensiveResult_Main : BaseDealComprehensiveResult
    {
        #region 变量定义            
        //粗定位空跑用
        static Random rd = new Random();
        int glassnum = rd.Next(1, 10);
        int cnt = 0;

        const string strRotateCalibCell1 = @"C10";
        const string strRotateCalibCell2 = @"C16";
        public Point2D[] pt2Calib = new Point2D[2] { new Point2D(), new Point2D() };

        public static Point2D[] pt2Mark1 = new Point2D[2] { new Point2D(), new Point2D() };
        public static Point2D[] pt2Mark2 = new Point2D[2] { new Point2D(), new Point2D() };
        public static Point2D[] pt2MarkArray = new Point2D[10] { new Point2D(), new Point2D(),
            new Point2D(), new Point2D(), new Point2D(), new Point2D(), new Point2D(), new Point2D(),new Point2D(),new Point2D() };
        public static double[] calcResultInsp = new double[3];
        public static double[] calcResultInsert = new double[3];

        public static Action StopBelt;
        public static Action RefreshStatistics = null;
        /// <summary>
        /// 单目定位两个位置的Mark坐标
        /// </summary>
        //public static Point2D PWorkMark1 = new Point2D();
        //public static Point2D PWorkMark2 = new Point2D();
        /// <summary>
        /// 放大系数 --YF
        /// </summary>
        public double AMP { get => ParCalibWorld.V_I[g_NoCamera]; }

        public static  string ReservDigits
        {
            get => "f3";
        }
        #endregion
        
        #region 算子序号定义
        /// <summary>
        /// 位置1的结果单元格
        /// </summary>
        public static string CellResultPos1
        {
            get => @"C4";
        }
        /// <summary>
        /// 位置2的结果单元格
        /// </summary>
        public static string CellResultPos2
        {
            get => @"C11";
        }
        /// <summary>
        /// 旋转中心单元格
        /// </summary>
        public static string CellRC
        {
            get => @"C30";
        }
        #region 精定位左右移动拍照单元格
        /// <summary>
        /// 第一拍照位置的匹配
        /// </summary>
        public static string CellMlineMatch1 => @"C10";
        /// <summary>
        /// 第一拍照位M直线交点1，左上角
        /// </summary>
        public static string CellMlineCross1 => @"C11";
        /// <summary>
        /// 第一拍照位M直线交点2，左下角
        /// </summary>
        public static string CellMlineCross2 => @"C12";
               
        /// <summary>
        /// 第二拍照位置匹配
        /// </summary>
        public static string CellMlineMatch2 => @"C18";
        /// <summary>
        /// 第二拍照位置M直线交点1，右上角
        /// </summary>
        public static string CellMlineCross3 => @"C19";
        /// <summary>
        /// 第二拍照位置M直线交点2，右下角
        /// </summary>
        public static string CellMlineCross4 => @"C20";
        #endregion 精定位左右移动拍照单元格

        #endregion
    }
}
