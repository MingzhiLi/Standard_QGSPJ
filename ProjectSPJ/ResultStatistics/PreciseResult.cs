using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BasicClass;
using System.IO;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Reflection;
using System.Xml.Serialization;

namespace ProjectSPJ
{
    /// <summary>
    /// 精定位计算结果类
    /// </summary>
    [Serializable]
    public class PreciseResult : BaseCalResult,ICloneable
    {
        [field:NonSerialized]
        private static List<PreciseResult> _preciseResult_L = new List<PreciseResult>();

        /// <summary>
        /// 软件启动时使用，加载当前时间的本地日志记录，软件重启时防止重写数据覆盖
        /// </summary>
        public static void LoadNowFile()
        {
            _preciseResult_L = LoadLog<List<PreciseResult>>(new PreciseResult().LogFile);
        }
        /// <summary>
        /// UI界面显示时使用，不直接使用静态PreciseResult_L的原因是防止内存冲突
        /// </summary>
        /// <param name="data">读取的数据</param>
        public static void LoadNowFile(out List<PreciseResult> data)
        {
            data = LoadLog<List<PreciseResult>>(new PreciseResult().LogFile);
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
            if(File.Exists(LogFile))
            {
                _preciseResult_L.Add(this);                
            }
            else
            {
                _preciseResult_L?.Clear();   ///每小时清空一次，记录到不同的文件夹
                _preciseResult_L.Add(this);
            }
            GetPar();
            SaveLogToLocal(_preciseResult_L, LogFile);
        }
        /// <summary>
        /// 偏差X
        /// </summary>
        public double DeltaX { get; set; }
        /// <summary>
        /// 偏差Y
        /// </summary>
        public double DeltaY { get; set; }
        /// <summary>
        /// 偏差R
        /// </summary>
        public double DeltaR { get; set; }
        /// <summary>
        /// 面积
        /// </summary>
        public double Area { get; set; } 
        /// <summary>
        /// 面积比例
        /// </summary>
        public double AreaRatio { get; set; }
        /// <summary>
        /// 放片位X
        /// </summary>
        public double PutPosX { get; set; }
        /// <summary>
        /// 放片位Y
        /// </summary>
        public double PutPosY { get; set; }
        /// <summary>
        /// 放片位R
        /// </summary>
        public double PutPosR { get; set; }
        /// <summary>
        /// 放片高度
        /// </summary>
        public double PutPosZ { get; set; }
        /// <summary>
        /// 最小面积阈值
        /// </summary>
        public double RatioMin { get; set; }
        /// <summary>
        /// 最大面积阈值
        /// </summary>
        public double RatioMax { get; set; }

        private double _width;
        /// <summary>
        /// 产品宽度
        /// </summary>
        public double Width
        {
            get
            {
                return Math.Round(_width, 3);
            }
            set
            {
                _width = value;
            }
        }

        private double _length;
        /// <summary>
        /// 产品长度
        /// </summary>
        public double Length
        {
            get
            {
                return Math.Round(_length, 3);
            }
            set
            {
                _length = value;
            }
        }

        /// <summary>
        /// 计算结果
        /// </summary>
        public bool BlResult { get; set; } = false;
        /// <summary>
        /// 字符串形式的结果
        /// </summary>
        public string StrResult
        {
            get
            {
                return BlResult ? OK : NG;
            }
        }
        /// <summary>
        /// 放片位置，方便调用
        /// </summary>
        [XmlIgnore]
        public Point4D PutPos
        {
            get
            {                
                return new Point4D(PutPosX, PutPosY, PutPosZ, PutPosR);
            }
            set
            {
                PutPosX = value.DblValue1;
                PutPosY = value.DblValue2;
                PutPosZ = value.DblValue3;
                PutPosR = value.DblValue4;
            }
        }


        /// <summary>
        /// 用于相机窗口显示的结果字符串
        /// </summary>
        public string ResultForShow
        {
            get
            {
                return "面积比例：" + AreaRatio.ToString("f3") + 
                       "\n面积阈值：【" + RatioMin + "," + RatioMax +
                        "】\n偏差X：" + DeltaX.ToString("f3") +
                        "\n偏差Y：" + DeltaY.ToString("f3") +
                        "\n偏差R：" + DeltaR.ToString("f3") +
                        "\n" + StrResult;
            }
        }
    }
}
