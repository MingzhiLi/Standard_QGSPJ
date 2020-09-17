using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.IO;
using System.Windows;

namespace ProjectSPJ
{
    public sealed class ResidueResult : BaseCalResult
    {
        public ResidueResult()
        {

        }
        public ResidueResult(ResidueEnum residueEnum)
        {
            _residueEnum = residueEnum;
        }

        #region 存储相关
        [field: NonSerialized]
        public static List<ResidueResult> ResidueResult1_L = new List<ResidueResult>();
        public static List<ResidueResult> ResidueResult2_L = new List<ResidueResult>();
        public static List<ResidueResult> ResidueResult3_L = new List<ResidueResult>();

        /// <summary>
        /// 软件启动时加载当前时间的本地日志记录，软件重启时防止重写数据覆盖
        /// </summary>
        public static void LoadNowFile()
        {
            ResidueResult1_L = LoadLog<List<ResidueResult>>(new ResidueResult(ResidueEnum.残材1).LogFile);
            ResidueResult2_L = LoadLog<List<ResidueResult>>(new ResidueResult(ResidueEnum.残材2).LogFile);
            ResidueResult3_L = LoadLog<List<ResidueResult>>(new ResidueResult(ResidueEnum.残材3).LogFile);
        }
        /// <summary>
        /// 加载当前时间的结果日志用于显示
        /// </summary>
        /// <param name="data">数据</param>
        public static void LoadNowFile(ResidueEnum residueEnum, out List<ResidueResult> data)
        {
            data = LoadLog<List<ResidueResult>>(new ResidueResult(residueEnum).LogFile);
        }

        /// <summary>
        /// 残材1文件存储路径
        /// </summary>
        public override string LogFile => LogPath + NameFile + _residueEnum + FormateXML;

        /// <summary>
        /// 文件保存名称，如果不重写则会以基类名称来保存
        /// </summary>
        protected override string NameFile => MethodBase.GetCurrentMethod().DeclaringType.Name;


        /// <summary>
        /// 将结果添加到list1
        /// </summary>
        public void AddToResultList(List<ResidueResult> residueResult_L)
        {
            if (File.Exists(LogFile))
            {
                residueResult_L.Add(this);
            }
            else
            {
                residueResult_L?.Clear();   ///每小时清空一次，记录到不同的文件夹
                residueResult_L.Add(this);
            }
            GetPar();
            SaveLogToLocal(residueResult_L, LogFile);
        }

        /// <summary>
        /// 获取指定小时前的文件
        /// </summary>
        /// <param name="hours">指定的小时</param>
        /// <returns></returns>
        public override string GetEverLogFile(int hours)
        {
            return GetEverLogPath(hours) + NameFile + _residueEnum + FormateXML;
        }

        /// <summary>
        /// 获取指定时间的日志文件路径
        /// </summary>
        /// <param name="dateTime">指定的时间</param>
        /// <returns>路径</returns>
        public override string GetDefineTimeLogFile(DateTime dateTime)
        {
            return GetDefineTimePath(dateTime) + NameFile + _residueEnum + FormateXML;
        }
        /// <summary>
        /// 加载指定小时前的日志文件
        /// </summary>
        /// <param name="hour">指定的时间</param>
        /// <param name="data">加载的数据</param>
        public static void LoadEverFile(int hour,ResidueEnum residueEnum, out List<ResidueResult> data)
        {
            string filePath = new ResidueResult(residueEnum).GetEverLogFile(hour);
            if (!File.Exists(filePath))
            {
                MessageBox.Show("不存在" + DateTime.Now.AddHours(hour).ToString("yyyy年MM月dd日HH点")
                                + "的残材检测记录！");
                data = new List<ResidueResult>();
                return;                       
            }
            data = LoadLog<List<ResidueResult>>(filePath);
        }

        /// <summary>
        /// 加载指定日期的所有日志
        /// </summary>
        /// <param name="dateTime">日期</param>
        /// <param name="data">加载的日志</param>
        public static void LoadAllDayFile(ResidueEnum residueEnum, DateTime dateTime, out List<ResidueResult> data)
        {            
            data = new List<ResidueResult>();
            ResidueResult residue = new ResidueResult(residueEnum);
            for (int i = 0; i < 24; i++)
            {
                string path = residue.GetDefineTimeLogFile(dateTime.AddHours(i));
                if (File.Exists(path))
                {
                    data.AddRange(LoadLog<List<ResidueResult>>(path));
                }
            }
        }

        #endregion 存储相关

        #region 私有字段
        private double _sharpnessTFT;
        private double _sharpnessCF;

        private ResidueEnum _residueEnum = ResidueEnum.残材1;
        #endregion 私有字段

        #region 公有属性
        /// <summary>
        /// 拍照位置
        /// </summary>
        public int PhotoPos { get; set; }
        /// <summary>
        /// TFT清晰度
        /// </summary>
        public double SharpnessTFT 
        {
            get
            {
                return Math.Round(_sharpnessTFT > 0 ? _sharpnessTFT : 1, 3);
            }
            set
            {
                _sharpnessTFT = value;
            }
        }
        /// <summary>
        /// CF清晰度
        /// </summary>
        public double SharpnessCF
        {
            get
            {
                return Math.Round(_sharpnessCF > 0 ? _sharpnessCF : 1, 3);
            }
            set
            {
                _sharpnessCF = value;
            }
        }
        /// <summary>
        /// 清晰度比例
        /// </summary>
        public double Ratio
        {
            get
            {
                return Math.Round(SharpnessTFT / SharpnessCF, 3);
            }
            set
            {
                ///写set只是为了存xml
            }
        }
        /// <summary>
        /// 本次计算结果
        /// </summary>
        public bool BlResult { get; set; }

        /// <summary>
        /// 设定的阈值
        /// </summary>
        public double Threshold { get; set; }
        #endregion 公有属性

        /// <summary>
        /// 字符串形式的结果
        /// </summary>
        public string StrResult => BlResult ? OK : NG;

        /// <summary>
        /// 用于相机窗口显示的结果字符串 
        /// </summary>
        public string ResultForShow
        {
            get
            {
                return "拍照位置：" + PhotoPos +
                        "\nTFT清晰度：" + SharpnessTFT.ToString("f3") +
                        "\nCF清晰度：" + SharpnessCF.ToString("f3") +
                        "\n比例：" + Ratio.ToString("f3") +
                        "\n阈值：" + Threshold.ToString("f3") +
                        "\n" + StrResult;
            }
        }

    }
}
