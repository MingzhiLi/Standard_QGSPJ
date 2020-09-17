using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using System.IO;
using System.ComponentModel;
using BasicClass;
using System.Linq.Expressions;
using System.Reflection;
using System.Data;
using Microsoft.Win32;

namespace ProjectSPJ
{
    [Serializable]
    public class BaseSerializer : INotifyPropertyChanged, ICloneable
    {
        [field: NonSerialized]
        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(string propInfo)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propInfo));
        }
        protected virtual void NotifyPropertyChanged(PropertyChangedEventArgs args)
        {
            this.PropertyChanged?.Invoke(this, args);
        }

        protected void NotifyPropertyChanged<T>(Expression<Func<T>> propertyExpression)
        {
            this.NotifyPropertyChanged(new PropertyChangedEventArgs(GetPropertyName(propertyExpression)));
        }

        protected string GetPropertyName<T>(Expression<Func<T>> propertyExpression)
        {
            var expression = propertyExpression.Body as System.Linq.Expressions.MemberExpression;
            return expression.Member.Name;
        }
        /// <summary>
        /// 类名，用于静态方法的调用
        /// </summary>
        public static string NameClass = MethodBase.GetCurrentMethod().DeclaringType.Name;
        /// <summary>
        /// 类名，用于非静态方法的调用
        /// </summary>
        protected virtual string NameFile => MethodBase.GetCurrentMethod().DeclaringType.Name;

        /// <summary>
        /// 克隆
        /// </summary>
        /// <returns>当前对象克隆副本</returns>
        public object Clone()
        {
            return this.MemberwiseClone();
        }


        #region 绑定及序列化保存

        #region 文件保存根目录
        /// <summary>
        /// xml文件保存格式
        /// </summary>
        protected static string FormateXML => ".xml";
        /// <summary>
        /// csv文件保存格式
        /// </summary>
        protected static string FormateCSV => ".csv";

        /// <summary>
        /// 配置文件文件保存根目录
        /// </summary>
        protected static string ConfigBaseSavePath
        {
            get
            {
                string path = ParPathRoot.PathRoot + "store\\收片机\\";
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                return path;
            }
        }

        /// <summary>
        /// 文件备份目录
        /// </summary>
        protected static string ConfigBackUpPath
        {
            get
            {
                string path = ParPathRoot.PathRoot + "软件运行记录\\数据备份\\收片机\\";
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                return path;
            }
        }


        /// <summary>
        /// 运行记录文件保存根目录
        /// </summary>
        protected static string BaseLogPath
        {
            get
            {
                string path = ParPathRoot.PathRoot + "软件运行记录\\运行日志\\";
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                return path;
            }
        }

        /// <summary>
        /// 读取当前日期
        /// </summary>
        protected static string StrDate
        {
            get
            {
                return DateTime.Today.ToString("yyyy-MM-dd");
            }
        }

        /// <summary>
        /// 读取当前时间，格式为：  小时_分钟_秒_毫秒
        /// </summary>
        protected static string StrTime
        {
            get
            {
                return DateTime.Now.Hour + "_" +
                       DateTime.Now.Minute + "_" +
                       DateTime.Now.Second + "_" +
                       DateTime.Now.Millisecond;
            }
        }
        #endregion 文件保存根目录

        #region 文件保存
        /// <summary>
        /// 默认将参数以xml格式保存到SavePath定义的路径
        /// </summary>
        /// <param name="strFileName">保存的文件名</param>
        /// <returns></returns>
        public virtual bool SaveConfigToLocal()
        {
            try
            {
                string strFileName = this.NameFile + ".xml";
                string path = ConfigBaseSavePath + strFileName;

                using (StreamWriter sw = new StreamWriter(path))
                {
                    XmlSerializer xmlSerializer = new XmlSerializer(GetType());
                    XmlSerializerNamespaces namespaces = new XmlSerializerNamespaces();
                    namespaces.Add("", "");
                    xmlSerializer.Serialize(sw, this, namespaces);
                }
                return true;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
                return false;
            }
        }

        /// <summary>
        /// 保存备份文件
        /// </summary>
        /// <returns></returns>
        public virtual bool SaveBackUpFile()
        {
            try
            {
                string strFileName = this.NameFile + "-" + StrTime + ".xml";
                string path = ConfigBackUpPath + strFileName;

                using (StreamWriter sw = new StreamWriter(path))
                {
                    XmlSerializer xmlSerializer = new XmlSerializer(GetType());
                    XmlSerializerNamespaces namespaces = new XmlSerializerNamespaces();
                    namespaces.Add("", "");
                    xmlSerializer.Serialize(sw, this, namespaces);
                }
                return true;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
                return false;
            }

        }



        /// <summary>
        /// 将当前对象保存到指定文件目录
        /// </summary>
        /// <param name="strPath">要保存的目录</param>
        /// <returns></returns>
        public virtual bool SaveLogToLocal(string strPath)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(strPath))
                {
                    XmlSerializer xmlSerializer = new XmlSerializer(GetType());
                    XmlSerializerNamespaces namespaces = new XmlSerializerNamespaces();
                    namespaces.Add("", "");
                    xmlSerializer.Serialize(sw, this, namespaces);
                }
                return true;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
                return false;
            }
        }


        /// <summary>
        /// 保存到指定文件目录
        /// </summary>
        /// <param name="strPath">要保存的目录</param>
        /// <param name="strFileName">保存的文件名</param>
        /// <returns></returns>
        public virtual bool SaveLogToLocal<T>(T log, string strPath)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(strPath))
                {
                    XmlSerializer xmlSerializer = new XmlSerializer(log.GetType());
                    XmlSerializerNamespaces namespaces = new XmlSerializerNamespaces();
                    namespaces.Add("", "");
                    xmlSerializer.Serialize(sw, log, namespaces);
                }
                return true;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
                return false;
            }
        }
        #endregion 文件保存

        #region 文件读取
        /// <summary>
        /// 读取指定目录下的本地的XML文件
        /// </summary>
        /// <typeparam name="T">模板类型</typeparam>
        /// <param name="filePath">文件目录</param>
        /// <returns></returns>
        public static T ReadLocalFile<T>(string filePath, string strFileName = "")
        {
            string path = filePath + strFileName;
            if (File.Exists(path))
            {
                using (StreamReader sr = new StreamReader(path))
                {
                    XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
                    return (T)xmlSerializer.Deserialize(sr);
                }
            }
            return Activator.CreateInstance<T>();
        }


        /// <summary>
        /// <para>读取默认目录下的配置文件xml格式，</para>
        /// <para>默认路径为：ParPathRoot.PathRoot + "store\\收片机\\" + 类名</para>
        /// </summary>
        /// <typeparam name="T">文件类型</typeparam>
        /// <returns>读取到的数据</returns>
        public virtual T ReadLocalFile<T>()
        {
            string strFileName = this.NameFile + ".xml";
            string path = ConfigBaseSavePath + strFileName;
            if (File.Exists(path))
            {
                using (StreamReader sr = new StreamReader(path))
                {
                    XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
                    return (T)xmlSerializer.Deserialize(sr);
                }
            }
            return Activator.CreateInstance<T>();
        }

        #endregion 文件读取


        #region 打开文件夹
        /// <summary>
        /// 打开本地备份文件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="localData"></param>
        /// <returns></returns>
        public static bool OpenBackUpFile<T>(out T localData)
        {
            OpenFileDialog histoyFile = new OpenFileDialog();
            histoyFile.InitialDirectory = ConfigBackUpPath;
            histoyFile.Filter = "XML files (*.xml)|*.xml";
            if ((bool)histoyFile.ShowDialog())
            {
                localData = ReadLocalFile<T>(histoyFile.FileName);
                return true;
            }
            localData = default(T);
            return false;
        }
        #endregion

        #region Save CSV
        /// <summary>
        /// 保存CSV文件
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <param name="append"></param>
        /// <param name="data">数据</param>
        public static void WriteCSV(string filePath, bool append, string[][] data)
        {
            try
            {
                using (StreamWriter fileWriter = new StreamWriter(filePath + FormateCSV, append, Encoding.Default))
                {
                    foreach (String[] strArr in data)
                    {
                        fileWriter.WriteLine(String.Join(",", strArr));
                    }
                    fileWriter.Flush();
                    fileWriter.Close();
                }
            }
            catch
            {
            }
            finally
            {
              
            }
        }
        #endregion SaveCSV

        #endregion 绑定及序列化等
    }
}
