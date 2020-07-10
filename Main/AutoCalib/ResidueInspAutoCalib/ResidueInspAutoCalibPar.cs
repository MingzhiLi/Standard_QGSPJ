using System;
using Common;
using BasicClass;
using System.Xml.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.ComponentModel;

namespace Main
{
    [Serializable]
    public class ResidueInspAutoCalibPar : INotifyPropertyChanged
    {
        const string NameClass = "ResidueInspAutoCalibPar";
        [field: NonSerialized]
        public event PropertyChangedEventHandler PropertyChanged;
        public void INotifyPropertyChanged(string propInfo)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propInfo));
        }

        public static ResidueInspAutoCalibPar R_I1 = new ResidueInspAutoCalibPar();
        public static ResidueInspAutoCalibPar R_I2 = new ResidueInspAutoCalibPar();
        public static ResidueInspAutoCalibPar R_I3 = new ResidueInspAutoCalibPar();

        #region 构造函数
        public ResidueInspAutoCalibPar()
        {
            StartStep = 1;
            MinStep = 0.01;
            MaxMoveDis = 2;
            MinTFTSharpness = 400;
            MinRatio = 2;
            MaxCFSharpness = 100;
            MaxCalibTimes = 30;
            E_MoveAxis = MoveAxis_Enum.X轴;
            E_StartDirection = MoveDirection_Enum.Positive;
        }
        #endregion 构造函数

        #region 校准参数
        /// <summary>
        /// 初始时单步移动的距离
        /// </summary>
        public double StartStep { get; set; }
        /// <summary>
        /// 最小单步移动的距离
        /// </summary>
        public double MinStep { get; set; }
        /// <summary>
        /// 朝单个方向的最大移动距离
        /// </summary>
        public double MaxMoveDis { get; set; }

        /// <summary>
        /// 结束时TFT的最小清晰度
        /// </summary>
        public double MinTFTSharpness { get; set; }
        /// <summary>
        /// 结束时CF的最小清晰度
        /// </summary>
        public double MaxCFSharpness { get; set; }
        /// <summary>
        /// 结束时TFT与CF的最小比例
        /// </summary>
        public double MinRatio { get; set; }
        #endregion 校准参数
        /// <summary>
        /// 最大校准次数
        /// </summary>
        public int MaxCalibTimes { get; set; }
        /// <summary>
        /// 当前相机所对应移动的轴
        /// </summary>
        public MoveAxis_Enum E_MoveAxis { get; set; }
        /// <summary>
        /// 标定第一次轴移动的方向
        /// </summary>
        public MoveDirection_Enum E_StartDirection { get; set; }

        public CalibAlgorithm_Enum E_CalibAlgorithm { get => CalibAlgorithm_Enum.Traversing; } 

        #region 绑定及序列化保存
        /// <summary>
        /// 文件保存根目录
        /// </summary>
        public static string PathSave
        {
            get
            {
                string path = ParPathRoot.PathRoot + "store\\AutoCalib\\ResidueInspAutoCalib\\";
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                return path;
            }
        }
        /// <summary>
        /// 用于轴移动绑定的List
        /// </summary>
        public List<MoveAxis_Enum> BindingAxisEnum_L
        {
            get
            {
                var List = new List<MoveAxis_Enum>();
                foreach (MoveAxis_Enum item in Enum.GetValues(typeof(MoveAxis_Enum)))
                {
                    List.Add(item);
                }
                return List;
            }
        }
        /// <summary>
        /// 用于轴初始移动方向的绑定的list
        /// </summary>
        public List<MoveDirection_Enum> BindingMoveDirection_L
        {
            get
            {
                var list = new List<MoveDirection_Enum>();
                foreach (MoveDirection_Enum item in Enum.GetValues(typeof(MoveDirection_Enum)))
                {
                    list.Add(item);
                }
                return list;
            }
        }

        /// <summary>
        /// 将参数保存到本地
        /// </summary>
        /// <param name="par">要保存的参数</param>
        /// <param name="strFileName">保存的文件名</param>
        /// <returns></returns>
        public void SaveToLocal(string strFileName)
        {
            try
            {
                string path = PathSave+ strFileName;
                
                using (StreamWriter sw = new StreamWriter(path))
                {
                    XmlSerializer xmlSerializer = new XmlSerializer(GetType());
                    XmlSerializerNamespaces namespaces = new XmlSerializerNamespaces();
                    namespaces.Add("", "");
                    xmlSerializer.Serialize(sw, this, namespaces);
                }
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }

        /// <summary>
        /// 从本地读取参数
        /// </summary>
        /// <param name="strFileName">读取的文件名</param>
        /// <returns></returns>
        public ResidueInspAutoCalibPar ReadLocalPar(string strFileName = "Par.xml")
        {
            try
            {
                string path = PathSave + strFileName;
                if(File.Exists(path))
                {
                    using (StreamReader sr = new StreamReader(path))
                    {
                        XmlSerializer xmlSerializer = new XmlSerializer(GetType());
                        return xmlSerializer.Deserialize(sr) as ResidueInspAutoCalibPar;
                    }
                }
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
            return new ResidueInspAutoCalibPar();
        }
        #endregion 绑定及序列化等
    }

    /// <summary>
    /// 所要移动的轴
    /// </summary>
    public enum MoveAxis_Enum
    {
        X轴,
        Y轴,
    }
    /// <summary>
    /// 各个条件的判断关系
    /// </summary>
    public enum JudgeRelation_Enum
    {
        And,
        Or,
    }
    /// <summary>
    /// 移动方向
    /// </summary>
    public enum MoveDirection_Enum
    {
        Positive = 1,
        Negative = -1,
    }

    /// <summary>
    /// 标定过程算法
    /// </summary>
    public enum CalibAlgorithm_Enum
    {
        /// <summary>
        /// 遍历找最优点
        /// </summary>
        Traversing,
        /// <summary>
        /// 迭代找最优或符合条件的点
        /// </summary>
        Iteration,
    }
}
