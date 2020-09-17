using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BasicClass;
using System.Reflection;
using System.IO;
using System.Xml.Serialization;
using System.Windows;

namespace ProjectSPJ
{
    /// <summary>
    /// 粗定位计算结果类
    /// </summary>
    public class RoughlyResult : BaseCalResult, ICloneable
    {
        [field: NonSerialized]
        private static List<RoughlyResult> _roughlyResult_L = new List<RoughlyResult>();
        public static List<RoughlyResult> RoughlyResult_L
        {
            get
            {
                return _roughlyResult_L;
            }
        }

        /// <summary>
        /// 加载当前时间的本地日志记录，软件重启时防止重写数据覆盖
        /// </summary>
        public static void LoadNowFile()
        {
            _roughlyResult_L = LoadLog<List<RoughlyResult>>(new RoughlyResult().LogFile);
        }
        /// <summary>
        /// 加载当前时间的运行数据
        /// </summary>
        /// <param name="data"></param>
        public static void LoadNowFile(out List<RoughlyResult> data)
        {
            data = LoadLog<List<RoughlyResult>>(new RoughlyResult().LogFile);
        }

        /// <summary>
        /// 加载指定小时前的日志文件
        /// </summary>
        /// <param name="hour">指定的时间</param>
        /// <param name="data">加载的数据</param>
        public static void LoadEverFile(int hour, out List<RoughlyResult> data)
        {
            string filePath = new RoughlyResult().GetEverLogFile(hour);
            if (!File.Exists(filePath))
            {
                MessageBox.Show("不存在" + DateTime.Now.AddHours(hour).ToString("yyyy年MM月dd日HH点")
                                + "的残材检测结果文件！");
                data = new List<RoughlyResult>();
                return;
            }
            data = LoadLog<List<RoughlyResult>>(filePath);
        }

        /// <summary>
        /// 加载指定日期的所有日志
        /// </summary>
        /// <param name="dateTime">日期</param>
        /// <param name="data">加载的日志</param>
        public static void LoadAllDayFile(DateTime dateTime, out List<RoughlyResult> data)
        {
            data = new List<RoughlyResult>();
            RoughlyResult roughly = new RoughlyResult();
            for (int i = 0; i < 24; i++)
            {
                string path = roughly.GetDefineTimeLogFile(dateTime.AddHours(i));
                if(File.Exists(path))
                {
                    data.AddRange(LoadLog<List<RoughlyResult>>(path));
                }
            }
        }
        /// <summary>
        /// 文件保存路径名称，如果不重写则会以基类名称来保存
        /// </summary>
        protected override string NameFile => MethodBase.GetCurrentMethod().DeclaringType.Name;

        /// <summary>
        /// 将结果添加到list
        /// </summary>
        public void AddToResultList()
        {
            if (File.Exists(LogFile))
            {
                _roughlyResult_L.Add(this);
            }
            else
            {
                _roughlyResult_L?.Clear();   ///每小时清空一次，记录到不同的文件夹
                _roughlyResult_L.Add(this);
            }
            GetPar();
            SaveLogToLocal(_roughlyResult_L, LogFile);
        }        

        #region 数据内容
        /// <summary>
        /// 工位号
        /// </summary>
        public int StationNo { get; set; }
        /// <summary>
        /// 当前产品所在中片取片序号
        /// </summary>
        public int PickIndex { get; set; }
        /// <summary>
        /// 取料X
        /// </summary>
        public double X { get; set; }
        /// <summary>
        /// 取料Y
        /// </summary>
        public double Y { get; set; }
        /// <summary>
        /// 取料Z
        /// </summary>
        public double Z { get; set; }
        /// <summary>
        /// 取料R
        /// </summary>
        public double R { get; set; }
        [XmlIgnore]
        public Point4D PickPos
        {
            get
            {
                return new Point4D(X, Y, Z, R);
            }
            set
            {
                X = value.DblValue1;
                Y = value.DblValue2;
                Z = value.DblValue3;
                R = value.DblValue4;
            }
        }
        /// <summary>
        /// 像素坐标X
        /// </summary>
        public double PixelX { get; set; }
        /// <summary>
        /// 像素坐标Y
        /// </summary>
        public double PixelY { get; set; }
        /// <summary>
        /// 匹配度分数
        /// </summary>
        public double Score { get; set; }
        /// <summary>
        /// 像素坐标
        /// </summary>
        public Point2D PixelPoint
        { 
            get
            {
                return new Point2D(PixelX, PixelY);
            }
        }
        /// <summary>
        /// 拍照是否OK
        /// </summary>
        public bool IsPhotoOK => PixelX * PixelY != 0;
        #endregion 数据内容

        /// <summary>
        /// 用于拍照OK正常取料时的显示
        /// </summary>
        public string InfoForShow
        {
            get
            {
                return "当前产品序号：" + PickIndex +
                       "\n当前工位：" + StationNo +
                       "\n匹配度：" + Score +
                       "\n取料坐标：" + PickPos;
            }
        }
    }
}
