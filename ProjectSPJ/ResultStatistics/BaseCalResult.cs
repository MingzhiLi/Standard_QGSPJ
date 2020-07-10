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
    /// 精定位计算结果基类
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
        public string ImagePath { get; set; }
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
    }
}
