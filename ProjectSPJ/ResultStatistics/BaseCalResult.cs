using BasicClass;
using Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectSPJ
{
    /// <summary>
    /// 计算结果基类
    /// </summary>
    public class BaseCalResult : BaseSerializer, ICloneable
    {
        protected const string OK = "OK";
        protected const string NG = "NG";
        /// <summary>
        /// 异常信息或者NG原因
        /// </summary>
        public string Info { get; set; }
        /// <summary>
        /// 图片保存路径
        /// </summary>
        public List<string> ImagePath { get; set; } = new List<string>();
        /// <summary>
        /// 屏幕截图路径
        /// </summary>
        public List<string> ScreenImage { get; set; } = new List<string>();
        /// <summary>
        /// 时间
        /// </summary>
        public string Time { get; set; }
        /// <summary>
        /// 型号名称
        /// </summary>
        public string ModelName { get; set; }
        /// <summary>
        /// 获取当前时间，型号等参数
        /// </summary>
        public void GetPar()
        {
            Time = DateTime.Now.ToString("HH:mm:ss:fff");
            ModelName = ComConfigPar.C_I.NameModel;
        }
        /// <summary>
        /// 结果数据存储路径
        /// </summary>
        public static string LogPath
        {
            get
            {
                string path =  BaseLogPath + StrDate + "\\" + DateTime.Now.ToString("HH") + "\\";
                if(!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                return path;
            }
        }

        /// <summary>
        /// 文件存储路径
        /// </summary>
        public virtual string LogFile => LogPath + this.NameFile + FormateXML;

        /// <summary>
        /// 获取指定小时前的路径
        /// </summary>
        /// <param name="hours"></param>
        /// <returns></returns>
        public static string GetEverLogPath(int hours)
        {
            return BaseLogPath + DateTime.Now.AddHours(hours).ToString("yyyy-MM-dd") + "\\" 
                               + DateTime.Now.AddHours(hours).ToString("HH") + "\\";
        }

        /// <summary>
        /// 获取指定时间的日志路径
        /// </summary>
        /// <param name="dateTime">指定的时间</param>
        /// <returns>文件路径</returns>
        public static string GetDefineTimePath(DateTime dateTime)
        {
            return BaseLogPath + dateTime.ToString("yyyy-MM-dd") + "\\"
                          + dateTime.ToString("HH") + "\\";
            
        }

        /// <summary>
        /// 获取指定小时前的文件
        /// </summary>
        /// <param name="hours">指定的小时</param>
        /// <returns></returns>
        public virtual string GetEverLogFile(int hours)
        {
            return GetEverLogPath(hours) + NameFile + FormateXML;
        }

        /// <summary>
        /// 获取指定时间的文件
        /// </summary>
        /// <param name="dateTime">指定的时间</param>
        /// <returns>文件路径</returns>
        public virtual string GetDefineTimeLogFile(DateTime dateTime)
        {
            return GetDefineTimePath(dateTime) + NameFile + FormateXML;
        }
        /// <summary>
        /// 读取指定目录的日志文件
        /// </summary>
        /// <typeparam name="T">文件类型</typeparam>
        /// <param name="logPath">文件目录</param>
        /// <returns>读取到的文件</returns>
        protected static T LoadLog<T>(string logPath)
        {
            return ReadLocalFile<T>(logPath);
        }
        /// <summary>
        /// 读取当前时间的日志文件
        /// </summary>
        /// <typeparam name="T">文件类型</typeparam>
        /// <returns>读取到的文件</returns>
        protected static T LoadLog<T>()
        {
            return ReadLocalFile<T>(LogPath);
        }

        /// <summary>
        /// 用于combobox日期选择绑定的list
        /// </summary>
        public static List<string> DateForBinding_L
        {
            get
            {
                List<string> date_L = new List<string>();
                for(int i = 0; i < 30; i++)
                {
                    date_L.Add(DateTime.Now.AddDays(-1 * i).ToString("yyyy-MM-dd"));
                }
                return date_L;
            }
        }
        /// <summary>
        /// 用于combobox时间选择的绑定的list
        /// </summary>
        public static List<string> HourForBinding_L
        {
            get
            {
                List<string> hour_L = new List<string>();
                for (int i = 0; i < 24; i++)
                {
                    hour_L.Add(DateTime.Now.AddHours(-1 * i).ToString("HH"));
                }
                return hour_L;
            }
        }
    }
}
