using BasicClass;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Remoting.Proxies;
using System.Text;
using System.Threading.Tasks;

namespace ProjectSPJ
{
    /// <summary>
    /// 产能统计
    /// </summary>
    public class ProductivityStatistic : BaseCalResult
    {
        #region 
        public static ProductivityStatistic CurrentStatistict = new ProductivityStatistic();
        public static List<ProductivityStatistic> HistoryStatistic_L = new List<ProductivityStatistic>();
        #endregion


        #region  public member

        /// <summary>
        /// 日期,班次
        /// </summary>
        public string Date { get; set; }
        /// <summary>
        /// 换班时间
        /// </summary>
        public string ChangeTime { get; set; }
        /// <summary>
        /// 精定位总数
        /// </summary>
        public int PreciseSum { get; set; }
        /// <summary>
        /// 精定位NG数量
        /// </summary>
        public int PreciseNG { get; set; }
        /// <summary>
        /// 残材检测总数
        /// </summary>
        public int ResidueSum { get; set; }
        /// <summary>
        /// 残材1NG数量
        /// </summary>
        public int Residue1NG { get; set; }
        /// <summary>
        /// 残材2NG数量
        /// </summary>
        public int Residue2NG { get; set; }
        #endregion

        #region Record
        /// <summary>
        /// 文件保存目录
        /// </summary>
        public static new string LogPath
        {
            get
            {
                string path = ParPathRoot.PathRoot + "软件运行记录\\Custom\\产能统计\\";
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                return path;
            }
        }

        /// <summary>
        /// 设置班次日期
        /// </summary>
        /// <param name="cmd">从PLC读取到的换班指令</param>
        public void SetDate(int cmd)
        {
            if (cmd == 1)
            {
                Date = DateTime.Now.ToString("yyyy年MM月dd日白班");
            }
            else
            {
                Date = DateTime.Now.AddDays(-1).ToString("yyyy年MM月dd日夜班");
            }
            ChangeTime = DateTime.Now.ToString("yyyy-MM-dd-HH:mm:ss");
        }

        /// <summary>
        /// 文件保存接口
        /// </summary>
        /// <param name="cmd">PLC命令</param>
        /// <param name="preciseSum">精定位拍照总数</param>
        /// <param name="preciseNG">精定位NG数量</param>
        /// <param name="residue1NG">残材1NG数量</param>
        /// <param name="residue2NG">残材2NG数量</param>
        public static void Record(int cmd,
                                  int preciseSum, int preciseNG,
                                  int residue1NG, int residue2NG)
        {
            ProductivityStatistic report = new ProductivityStatistic();
            report.SetDate(cmd);
            report.PreciseSum = preciseSum;
            report.PreciseNG = preciseNG;
            report.Residue1NG = residue1NG;
            report.Residue2NG = residue2NG;
            string fileName = LogPath + report.Date + FormateXML;
            ///防止PLC重复发信号
            for (int i = 1; File.Exists(fileName); i++)
            {
                fileName = LogPath + report.Date + i + FormateXML;
            }

            report.SaveLogToLocal(fileName);
        }
        #endregion

        /// <summary>
        /// 加载当日的生产统计
        /// </summary>
        /// <param name="preciseSum"></param>
        /// <param name="preciseNG"></param>
        /// <param name="residue1NG"></param>
        /// <param name="residue2Ng"></param>
        public static void GetCurrentStatistic(int preciseSum, int preciseNG, int residue1NG, int residue2Ng)
        {
            CurrentStatistict.PreciseSum = preciseSum;
            CurrentStatistict.PreciseNG = preciseNG;
            CurrentStatistict.Residue1NG = residue1NG;
            CurrentStatistict.Residue2NG = residue2Ng;
        }

        /// <summary>
        /// 加载最近日期的生产统计
        /// </summary>
        public static void GetHistoryStatistic()
        {
            HistoryStatistic_L?.Clear();
            for(int i = 0;i < 30;i++)
            {
                string dayLog = LogPath + DateTime.Now.AddDays(0 - i).ToString("yyyy年MM月dd日白班") + FormateXML;
                string nightLog = LogPath + DateTime.Now.AddDays(0 - i).ToString("yyyy年MM月dd日夜班") + FormateXML;
                if (File.Exists(dayLog))
                {
                    HistoryStatistic_L.Add(LoadLog<ProductivityStatistic>(dayLog));
                }
                if (File.Exists(nightLog))
                {
                    HistoryStatistic_L.Add(LoadLog<ProductivityStatistic>(nightLog));
                }
            }
        }
    }

}
