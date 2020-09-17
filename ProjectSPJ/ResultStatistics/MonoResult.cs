using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using BasicClass;
using MahApps.Metro.Converters;

namespace ProjectSPJ
{
    /// <summary>
    /// 单目相机计算结果
    /// </summary>
    public sealed class MonoResult : BaseCalResult
    {
        [field: NonSerialized]
        private static List<MonoResult> _monoResult_L = new List<MonoResult>();

        /// <summary>
        /// 文件保存名称，如果不重写则会以基类名称来保存
        /// </summary>
        protected override string NameFile => MethodBase.GetCurrentMethod().DeclaringType.Name;

        /// <summary>
        /// 软件启动时使用，加载当前时间的本地日志记录，软件重启时防止重写数据覆盖
        /// </summary>
        public static void LoadNowFile()
        {
            _monoResult_L = LoadLog<List<MonoResult>>(new MonoResult().LogFile);
        }
        /// <summary>
        /// UI界面显示时使用，不直接使用静态PreciseResult_L的原因是防止内存冲突
        /// </summary>
        /// <param name="data">读取的数据</param>
        public static void LoadNowFile(out List<MonoResult> data)
        {
            data = LoadLog<List<MonoResult>>(new MonoResult().LogFile);
        }

        /// <summary>
        /// 加载指定小时前的日志文件
        /// </summary>
        /// <param name="hour">指定的时间</param>
        /// <param name="data">加载的数据</param>
        public static void LoadEverFile(int hour, out List<MonoResult> data)
        {
            string filePath = new MonoResult().GetEverLogFile(hour);
            if (!File.Exists(filePath))
            {
                MessageBox.Show("不存在" + DateTime.Now.AddHours(hour).ToString("yyyy年MM月dd日HH点") + "的单目结果文件！");
                data = new List<MonoResult>();
                return;
            }
            data = LoadLog<List<MonoResult>>(filePath);
        }

        /// <summary>
        /// 加载指定日期的所有日志
        /// </summary>
        /// <param name="dateTime">日期</param>
        /// <param name="data">加载的日志</param>
        public static void LoadAllDayFile(DateTime dateTime, out List<MonoResult> data)
        {
            data = new List<MonoResult>();
            MonoResult mono = new MonoResult();
            for (int i = 0; i < 24; i++)
            {
                string path = mono.GetDefineTimeLogFile(dateTime.AddHours(i));
                if (File.Exists(path))
                {
                    data.AddRange(LoadLog<List<MonoResult>>(path));
                }
            }
        }
        /// <summary>
        /// 将结果添加到list
        /// </summary>
        public void AddToResultList()
        {
            if (File.Exists(LogFile))
            {
                _monoResult_L.Add(this);
            }
            else
            {
                _monoResult_L?.Clear();   ///每小时清空一次，记录到不同的文件夹
                _monoResult_L.Add(this);
            }
            GetPar();
            SaveLogToLocal(_monoResult_L, LogFile);
        }

        #region Data Content
        public const string Header = "Mark1X," +
                                     "Mark1Y," +
                                     "Mark2X," +
                                     "Mark2Y," +
                                     "偏差X," +
                                     "偏差Y," +
                                     "偏差R," +
                                     "巡边补偿X," +
                                     "巡边补偿Y," +
                                     "巡边补偿R," +
                                     "二维码偏差X," +
                                     "二维码偏差Y,";
        public override string ToString()
        {
            return Mark1X.ToString("f3") +
                   Mark1Y.ToString("f3") +
                   Mark2X.ToString("f3") +
                   Mark2X.ToString("f3") +
                   DeltaX.ToString("f3") +
                   DeltaY.ToString("f3") +
                   DeltaR.ToString("f3") +
                   DeltaInspX.ToString("f3") +
                   DeltaInspY.ToString("f3") +
                   DeltaInspR.ToString("f3") +
                   DeltaQrX.ToString("f3") +
                   DeltaQrY.ToString("f3");
        }


        private double _mark1X = 0;
        /// <summary>
        /// Mark1像素坐标X
        /// </summary>
        public double Mark1X
        {
            get
            {
                return Math.Round(_mark1X, 3);
            }
            set
            {
                _mark1X = value;
            }
        }

        private double _mark1Y = 0;
        /// <summary>
        /// Mark1像素坐标Y
        /// </summary>
        public double Mark1Y
        {
            get
            {
                return Math.Round(_mark1Y, 3);
            }
            set
            {
                _mark1Y = value;
            }
        }
        public Point2D Mark1 => new Point2D(Mark1X, Mark1Y);

        private double _mark2X = 0;
        /// <summary>
        /// Mark2像素坐标X
        /// </summary>
        public double Mark2X
        {
            get
            {
                return Math.Round(_mark2X, 3);
            }
            set
            {
                _mark2X = value;
            }
        }

        private double _mark2Y = 0;
        /// <summary>
        /// Mark2像素坐标Y
        /// </summary>
        public double Mark2Y
        {
            get
            {
                return Math.Round(_mark2Y, 3);
            }
            set
            {
                _mark2Y = value;
            }
        }

        public Point2D Mark2 => new Point2D(Mark2X, Mark2Y);

        private double _deltaX;
        /// <summary>
        /// 初始计算偏差X 
        /// </summary>
        public double DeltaX
        {
            get
            {
                return Math.Round(_deltaX, 3);
            }
            set
            {
                _deltaX = value;
            }
        }

        private double _deltaY { get; set; } 
        /// <summary>
        /// 初始计算偏差Y 
        /// </summary>
        public double DeltaY
        {
            get
            {
                return Math.Round(_deltaY, 3);
            }
            set
            {
                _deltaY = value;
            }
        }

        private double _deltaR = 0;
        /// <summary>
        /// 初始计算偏差R 
        /// </summary>
        public double DeltaR
        {
            get
            {
                return Math.Round(_deltaR, 3);
            }
            set
            {
                _deltaR = value;
            }
        }

        /// <summary>
        /// 处理结果
        /// </summary>
        public bool BlReuslt { get; set; } = false;

        private double _deltaInspX = 0;
        /// <summary>
        /// 巡边偏差X
        /// </summary>
        public double DeltaInspX
        {
            get
            {
                return Math.Round(_deltaInspX, 3);
            }
            set
            {
                _deltaInspX = value;
            }
        }

        private double _deltaInspY = 0;
        /// <summary>
        /// 巡边偏差Y
        /// </summary>
        public double DeltaInspY
        {
            get
            {
                return Math.Round(_deltaInspY, 3);
            }
            set
            {
                _deltaInspY = value;
            }
        }

        private double _deltaInspR = 0;
        /// <summary>
        /// 巡边偏差R
        /// </summary>
        public double DeltaInspR
        {
            get
            {
                return Math.Round(_deltaInspR, 3);
            }
            set
            {
                _deltaInspR = value;
            }
        }

        private double _deltaQrX = 0;
        /// <summary>
        /// 二维码偏差X
        /// </summary>
        public double DeltaQrX
        {
            get
            {
                return Math.Round(_deltaQrX, 3);
            }
            set
            {
                _deltaQrX = value;
            }
        }

        private double _deltaQrY = 0;
        /// <summary>
        /// 二维码偏差Y
        /// </summary>
        public double DeltaQrY
        {
            get
            {
                return Math.Round(_deltaQrY, 3);
            }
            set
            {
                _deltaQrY = value;
            }
        }

        private double _deltaQrAngleX = 0;
        /// <summary>
        /// 因角度偏差导致的二维码偏差X
        /// </summary>
        public double DeltaQrAngleX
        {
            get
            {
                return Math.Round(_deltaQrAngleX, 3);
            }
            set
            {
                _deltaQrAngleX = value;
            }
        }


        private double _deltaQrAngleY = 0;
        /// <summary>
        /// 因角度偏差导致的二维码偏差Y
        /// </summary>
        public double DeltaQrAngleY
        {
            get
            {
                return Math.Round(_deltaQrAngleY, 3);
            }
            set
            {
                _deltaQrAngleY = value;
            }
        }
        #endregion Data Content
    }
}
