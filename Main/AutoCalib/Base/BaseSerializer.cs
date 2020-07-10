using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.IO;
using BasicClass;
using System.ComponentModel;

namespace Main
{
    [Serializable]
    public class BaseSerializer : INotifyPropertyChanged
    {
        [field: NonSerialized]
        public event PropertyChangedEventHandler PropertyChanged;
        public void INotifyPropertyChanged(string propInfo)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propInfo));
        }

        public static string NameClass = "BaseSerializer";

        #region 绑定及序列化保存

        #region 文件保存根目录
        /// <summary>
        /// 文件保存根目录
        /// </summary>
        public static string BaseSavePath
        {
            get
            {
                string path = ParPathRoot.PathRoot + "store\\AutoCalib\\";
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                return path;
            }
        }
        #endregion 文件保存根目录

        #region 文件保存
        /// <summary>
        /// 将参数保存到SavePath定义的路径
        /// </summary>
        /// <param name="strFileName">保存的文件名</param>
        /// <returns></returns>
        public virtual bool SaveToLocal(string strFileName)
        {
            try
            {
                string path = BaseSavePath + strFileName;

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
        /// 保存到指定文件目录
        /// </summary>
        /// <param name="strPath">要保存的目录</param>
        /// <param name="strFileName">保存的文件名</param>
        /// <returns></returns>
        public virtual bool SaveToLocal(string strPath,string strFileName)
        {
            try
            {
                string path = strPath + strFileName;

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
        /// 保存list数据
        /// </summary>
        /// <typeparam name="T">list中模板数据类型</typeparam>
        /// <param name="data">要保存的数据</param>
        /// <param name="pathSave">保存路径</param>
        public static void SaveList<T>(T data, string pathSave)
        {
            using (StreamWriter sw = new StreamWriter(pathSave))
            {
                XmlSerializer xmlSerializer = new XmlSerializer(data.GetType());

                XmlSerializerNamespaces namespaces = new XmlSerializerNamespaces();
                namespaces.Add("", "");
                xmlSerializer.Serialize(sw, data, namespaces);
            }
        }


        public static void SaveListAppend<T>(T data, string pathSave)
        {
            using (StreamWriter sw = new StreamWriter(pathSave, true))
            {
                XmlSerializer xmlSerializer = new XmlSerializer(data.GetType());

                XmlSerializerNamespaces namespaces = new XmlSerializerNamespaces();
                namespaces.Add("", "");
                xmlSerializer.Serialize(sw, data, namespaces);
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
        /// 读取默认目录下的指定文件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="filePath"></param>
        /// <param name="strFileName"></param>
        /// <returns></returns>
        public static T ReadDefaultPathFile<T>(string strFileName)
        {
            string path = BaseSavePath + strFileName;
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

        #endregion 绑定及序列化等

    }
}
