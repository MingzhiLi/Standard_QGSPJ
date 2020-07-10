using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
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
        /// 文件存储路径
        /// </summary>
        public string LogFile => LogPath + this.NameFile + ".xml";

        /// <summary>
        /// 文件保存名称，如果不重写则会以基类名称来保存
        /// </summary>
        protected override string NameFile => MethodBase.GetCurrentMethod().DeclaringType.Name;

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
    }
}
