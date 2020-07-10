using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using BasicClass;
using System.IO;
using Common;
using System.Xml.Serialization;

namespace ProjectSPJ
{
    /// <summary>
    /// 产品参数基类
    /// </summary>
    public class ProdParBase:BaseSerializer
    {

        protected override string NameFile => MethodBase.GetCurrentMethod().DeclaringType.Name;

        /// <summary>
        /// 存储产品参数的路径
        /// </summary>
        public virtual string ProdBaseSavePath
        {
            get
            {
                string path = ParPathRoot.PathRoot + "store\\产品参数\\" + ComConfigPar.C_I.NameModel + "\\";
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                return path;
            }
        }
        /// <summary>
        /// 上一款产品的存储路径，换型时使用
        /// </summary>
        public string OldProdPath
        {
            get
            {
                return ComConfigPar.C_I.PathOldConfigIni.Replace("\\Product.ini", NameFile + ".xml");
            }
        }
        /// <summary>
        /// 默认将参数以xml格式保存到SavePath定义的路径
        /// </summary>
        /// <param name="strFileName">保存的文件名</param>
        /// <returns></returns>
        public virtual bool SaveProductParToLocal()
        {
            try
            {
                string strFileName = this.NameFile + ".xml";
                string path = ProdBaseSavePath + strFileName;

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
        /// 读取产品参数
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T ReadProdFile<T>()
        {
            string strFileName = this.NameFile + ".xml";
            string path = ProdBaseSavePath + strFileName;
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



    }
}
