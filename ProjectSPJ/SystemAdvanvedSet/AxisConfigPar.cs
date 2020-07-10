using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.IO;
using System.Windows;
using BasicClass;
using System.Reflection;
using System.Diagnostics;

namespace ProjectSPJ
{
    public class AxisConfigPar : BaseSerializer
    {
        #region 轴系统
        /// <summary>
        /// 机器人的轴系统方向，暂时先写死
        /// </summary>
        public static AxisConfigPar RobotAxisSystem = new AxisConfigPar() {AxisSystemName = "机器人轴方向" };
        /// <summary>
        /// 单目相机补正到巡边平台的补正方向
        /// </summary>
        public static AxisConfigPar MonoAxisSystem = new AxisConfigPar() { AxisSystemName = "巡边补正轴方向" };
        /// <summary>
        /// 二维码相机的轴方向
        /// </summary>
        public static AxisConfigPar QrCodeAxisSystem = new AxisConfigPar() { AxisSystemName = "二维码相机轴方向" };
        /// <summary>
        /// 插栏轴方向
        /// </summary>
        public static AxisConfigPar CstAxisSystem = new AxisConfigPar() { AxisSystemName = "插栏轴方向" };
        public static AxisConfigPar ReserveAxisSystem1 = new AxisConfigPar() { AxisSystemName = "备用轴系统1" };
        public static AxisConfigPar ReserveAxisSystem2 = new AxisConfigPar() { AxisSystemName = "备用轴系统2" };



        public static AxisConfigPar[] AxisConfigs_Arr = new AxisConfigPar[6] { RobotAxisSystem, MonoAxisSystem, QrCodeAxisSystem, CstAxisSystem,
                                                                             ReserveAxisSystem1, ReserveAxisSystem2};
        #endregion 轴系统


        #region 文件读写
        /// <summary>
        /// 文件保存路径名称，如果不重写则会以基类名称来保存
        /// </summary>
        protected override string NameFile => MethodBase.GetCurrentMethod().DeclaringType.Name;

        /// <summary>
        /// 默认将参数以xml格式保存到SavePath定义的路径
        /// </summary>
        /// <param name="strFileName">保存的文件名</param>
        /// <returns></returns>
        public override bool SaveConfigToLocal()
        {
            try
            {
                string strFileName = this.AxisSystemName + ".xml";
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
                Log.L_I.WriteError(NameFile, ex);
                return false;
            }
        }

        /// <summary>
        /// <para>重写的读取默认目录下的配置文件xml格式，</para>
        /// <para>默认路径为：ParPathRoot.PathRoot + "store\\收片机\\" + 对象名称</para>
        /// </summary>
        /// <typeparam name="T">文件类型</typeparam>
        /// <returns>读取到的数据</returns>
        public override T ReadLocalFile<T>()
        {
            string strFileName = this.AxisSystemName + ".xml";
            string path = ConfigBaseSavePath + strFileName;
            if (File.Exists(path))
            {
                using (StreamReader sr = new StreamReader(path))
                {
                    XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
                    return (T)xmlSerializer.Deserialize(sr);
                }
            }
            return default(T);
        }

        /// <summary>
        /// 保存备份文件
        /// </summary>
        /// <returns></returns>
        public override bool SaveBackUpFile()
        {
            try
            {
                string strFileName = this.AxisSystemName + "-" + StrTime + ".xml";
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
        /// 读取本地的配置文件
        /// </summary>
        public static void ReadLocalFile()
        {
            int index = 0;
            RobotAxisSystem = RobotAxisSystem.ReadLocalFile<AxisConfigPar>();
            MonoAxisSystem = MonoAxisSystem.ReadLocalFile<AxisConfigPar>();
            QrCodeAxisSystem = QrCodeAxisSystem.ReadLocalFile<AxisConfigPar>();
            CstAxisSystem = CstAxisSystem.ReadLocalFile<AxisConfigPar>(); 
            ReserveAxisSystem1 = ReserveAxisSystem1.ReadLocalFile<AxisConfigPar>();
            ReserveAxisSystem2 = ReserveAxisSystem2.ReadLocalFile<AxisConfigPar>();
            foreach (AxisConfigPar axisConfigPar in AxisConfigs_Arr)
            {
                AxisConfigPar axisConfig = axisConfigPar.ReadLocalFile<AxisConfigPar>();
                if (axisConfig != null)
                {
                    AxisConfigs_Arr[index] = axisConfigPar.ReadLocalFile<AxisConfigPar>();
                }
                index++;
            }
        }
        #endregion 文件读写

        #region 偏差计算时的轴系数
        /// <summary>
        /// X方向轴系数，注意需要保证相机画面中X向右为正，Y向上为正
        /// </summary>
        public int CalRatioX
        {
            get
            {
                if(AxisX == AxisDirectionEnum.向右 ||
                   AxisX == AxisDirectionEnum.向上)
                {
                    return 1;
                }
                else if(AxisX == AxisDirectionEnum.NULL)
                {
                    return 0;
                }
                else
                {
                    return -1;
                }
            }
        }

        /// <summary>
        /// Y方向轴系数，注意需要保证相机画面中X向右为正，Y向上为正
        /// </summary>
        public int CalRatioY
        {
            get
            {
                if (AxisY == AxisDirectionEnum.向右 ||
                   AxisY == AxisDirectionEnum.向上)
                {
                    return 1;
                }
                else if (AxisY == AxisDirectionEnum.NULL)
                {
                    return 0;
                }
                else
                {
                    return -1;
                }
            }
        }
        #endregion 偏差计算时的轴系数


        #region 轴方向选择

        #region 私有字段 
        /// <summary>
        /// x轴方向
        /// </summary>
        private AxisDirectionEnum _axisX = AxisDirectionEnum.NULL;
        /// <summary>
        /// y轴方向
        /// </summary>
        private AxisDirectionEnum _axisY = AxisDirectionEnum.NULL;
        /// <summary>
        /// r轴方向
        /// </summary>
        private AxisREnum _axisR = AxisREnum.NULL;
        /// <summary>
        /// 拍照时轴移动的方向
        /// </summary>
        private MoveDirecitonEnum _moveDiection = MoveDirecitonEnum.NULL;
        #endregion 私有字段

        #region 公有属性
        /// <summary>
        /// 轴系统名称
        /// </summary>
        public string AxisSystemName { get; set; } = "轴系统";
        /// <summary>
        /// x轴方向
        /// </summary>
        public AxisDirectionEnum AxisX
        {
            get
            {
                return _axisX;
            }
            set
            {
                _axisX = value;
                NotifyPropertyChanged(() => AxisYBinding_L);
                NotifyPropertyChanged(() => StartPoint);
                NotifyPropertyChanged(() => XEndPoint);
                NotifyPropertyChanged(() => YEndPoint);
            }
        }

        /// <summary>
        /// Y轴方向
        /// </summary>
        public AxisDirectionEnum AxisY
        {
            get
            {
                return _axisY;
            }
            set
            {
                _axisY = value;
                NotifyPropertyChanged(() => AxisXBinding_L);
                NotifyPropertyChanged(() => StartPoint);
                NotifyPropertyChanged(() => XEndPoint);
                NotifyPropertyChanged(() => YEndPoint);
            }
        }

        public MoveDirecitonEnum MoveDireciton
        {
            get
            {
                return _moveDiection;
            }
            set
            {
                _moveDiection = value;
            }
        }
        /// <summary>
        /// R轴方向
        /// </summary>
        public AxisREnum AxisR
        {
            get
            {
                return _axisR;
            }
            set
            {
                _axisR = value;
                NotifyPropertyChanged(() => RControlPoint);
                NotifyPropertyChanged(() => RStartPoint);
                NotifyPropertyChanged(() => REndPoint);
                NotifyPropertyChanged(() => RlblContent);
                NotifyPropertyChanged(() => RlblMargin);
            }
        }
        /// <summary>
        /// 轴方向与相机画面方向不同导致的偏差旋转角度，
        /// 这里的前提条件是相机坐标系是标准的笛卡尔坐标系，
        /// 即图像X向右为正，Y向上为正
        /// 暂未考虑XY镜像之后的计算
        /// </summary>
        public double DeltaRotateR
        {
            get
            {
                if (_axisX == AxisDirectionEnum.向右 && _axisY == AxisDirectionEnum.向上)
                {
                    return 0;
                }
                else if (_axisX == AxisDirectionEnum.向上 && _axisY == AxisDirectionEnum.向左)
                {
                    return 90;
                }
                else if (_axisX == AxisDirectionEnum.向左 && _axisY == AxisDirectionEnum.向下)
                {
                    return 180;

                }
                else if (_axisX == AxisDirectionEnum.向下 && _axisY == AxisDirectionEnum.向右)
                {
                    return -90;
                }

                return 0;
            }
        }
        #endregion 公有属性

        #endregion 轴方向选择

        #region 用于绑定的轴方向选择list
        /// <summary>
        /// 用于绑定到X轴方向选项的combobox的list
        /// </summary>
        [XmlIgnore]
        public List<AxisDirectionEnum> AxisXBinding_L
        {
            get
            {
                List<AxisDirectionEnum> list = new List<AxisDirectionEnum>();
                foreach (AxisDirectionEnum item in Enum.GetValues(typeof(AxisDirectionEnum)))
                {
                    if ((int)AxisY == 0)
                    {
                        list.Add(item);
                    }
                    else if ((int)AxisY < 3 && ((int)item > 2 || (int)item == 0))
                    {
                        list.Add(item);
                    }
                    else if ((int)AxisY > 2 && (int)item < 3)
                    {
                        list.Add(item);
                    }
                }
                return list;
            }
        }


        /// <summary>
        /// 用于绑定到Y轴方向选项的combobox的list
        /// </summary>
        [XmlIgnore]
        public List<AxisDirectionEnum> AxisYBinding_L
        {
            get
            {
                List<AxisDirectionEnum> list = new List<AxisDirectionEnum>();
                foreach (AxisDirectionEnum item in Enum.GetValues(typeof(AxisDirectionEnum)))
                {
                    if ((int)AxisX == 0)
                    {
                        list.Add(item);
                    }
                    else if ((int)AxisX < 3 && ((int)item > 2 || (int)item == 0))
                    {
                        list.Add(item);
                    }
                    else if ((int)AxisX > 2 && (int)item < 3)
                    {
                        list.Add(item);
                    }
                }
                return list;
            }
        }

        /// <summary>
        /// 用于绑定轴移动方向的combox的list
        /// </summary>
        [XmlIgnore]
        public List<MoveDirecitonEnum> MoveDirectionBinding_L
        {
            get
            {
                List<MoveDirecitonEnum> list = new List<MoveDirecitonEnum>();
                foreach (MoveDirecitonEnum item in Enum.GetValues(typeof(MoveDirecitonEnum)))
                {
                    list.Add(item);
                }
                return list;
            }
        }
        /// <summary>
        /// 用于绑定到R轴方向的list
        /// </summary>
        [XmlIgnore]
        public List<AxisREnum> AxisRBinding_L
        {
            get
            {
                List<AxisREnum> list = new List<AxisREnum>();
                foreach (AxisREnum item in Enum.GetValues(typeof(AxisREnum)))
                {
                    list.Add(item);
                }
                return list;
            }
        }
        #endregion 轴方向选择list

        #region 坐标轴绘制属性

        #region 画布尺寸及点位
        /// <summary>
        /// 画布尺寸
        /// </summary>
        public int CanvssSize { get => 200; }

        /// <summary>
        /// 绘制坐标轴时的左右留白
        /// </summary>
        public int Space { get => 30; }
        /// <summary>
        /// 左上角点
        /// </summary>
        private Point leftTopPoint => new Point(Space, Space);
        /// <summary>
        /// 右上角点
        /// </summary>
        private Point rightTopPoint => new Point(CanvssSize - Space, Space);
        /// <summary>
        /// 左下角点
        /// </summary>
        private Point leftBottomPoint => new Point(Space, CanvssSize - Space);
        /// <summary>
        /// 右下角点
        /// </summary>
        private Point rightBottomPoint => new Point(CanvssSize - Space, CanvssSize - Space);
        #endregion 画布尺寸及点位

        #region 绘图参数
        /// <summary>
        /// R方向说明lbl的margin
        /// </summary>
        public Thickness RlblMargin
        {
            get
            {
                return new Thickness(CanvssSize / 2, CanvssSize / 2, 0, 0);
            }
        }
        /// <summary>
        /// R方向说明lbl的content
        /// </summary>
        public string RlblContent
        {
            get
            {
                if (_axisR == AxisREnum.NULL)
                {
                    return string.Empty;
                }
                else
                {
                    return "R轴方向";
                }
            }
        }
        /// <summary>
        /// 坐标轴起始点
        /// </summary>
        [XmlIgnore]
        public Point StartPoint
        {
            get
            {
                if ((int)(_axisX | _axisY) == 0x01 ||           //X,Y轴一个为null另一个为向右为正
                        (int)(_axisX | _axisY) == 0x09)             //一个向右为正，一个向下为正
                {
                    return leftTopPoint;   //起点在左上
                }
                else if ((int)(_axisX | _axisY) == 0x2 ||           //X,Y轴一个为null另一个为向左为正
                         (int)(_axisX | _axisY) == 0x6)             //一个向左为正，一个向上为正
                {
                    return rightBottomPoint; //起点在右下
                }
                else if ((int)(_axisX | _axisY) == 0x04 ||           //X,Y轴一个为null另一个为向上为正
                        (int)(_axisX | _axisY) == 0x5)              //X,Y一个像右为正，一个向上为正
                {
                    return leftBottomPoint;    //起点在左下
                }
                else if ((int)(_axisX | _axisY) == 0x08 ||          //X,Y轴一个为null另一个为向下为正
                        (int)(_axisX | _axisY) == 0x0A)             //一个向左为正，一个向下为正
                {
                    return rightTopPoint;   //起点在右下
                }
                else
                {
                    return new Point();  //两者都为null时
                }
            }
        }

        /// <summary>
        /// X轴坐标结束点
        /// </summary>
        [XmlIgnore]
        public Point XEndPoint
        {
            get
            {
                if ((_axisX == AxisDirectionEnum.向上 && _axisY == AxisDirectionEnum.向右) ||
                   (_axisX == AxisDirectionEnum.向左 && _axisY == AxisDirectionEnum.向下))
                {
                    return leftTopPoint;
                }
                else if ((_axisX == AxisDirectionEnum.向上 && _axisY == AxisDirectionEnum.向左) ||
                   (_axisX == AxisDirectionEnum.向右 && _axisY == AxisDirectionEnum.向下))
                {
                    return rightTopPoint;
                }
                else if ((_axisX == AxisDirectionEnum.向下 && _axisY == AxisDirectionEnum.向右) ||
                   (_axisX == AxisDirectionEnum.向左 && _axisY == AxisDirectionEnum.向上))
                {
                    return leftBottomPoint;
                }
                else if ((_axisX == AxisDirectionEnum.向下 && _axisY == AxisDirectionEnum.向左) ||
                   (_axisX == AxisDirectionEnum.向右 && _axisY == AxisDirectionEnum.向上))
                {
                    return rightBottomPoint;
                }
                else if (_axisY == AxisDirectionEnum.NULL && _axisX != AxisDirectionEnum.NULL)
                {
                    if (StartPoint == leftTopPoint)
                    {
                        return rightTopPoint;
                    }
                    else if (StartPoint == rightTopPoint)
                    {
                        return leftTopPoint;
                    }
                    else if (StartPoint == leftBottomPoint)
                    {
                        return rightBottomPoint;
                    }
                    else
                    {
                        return leftBottomPoint;
                    }
                }
                else
                {
                    return StartPoint;
                }
            }
        }

        /// <summary>
        /// Y轴坐标结束点
        /// </summary>
        [XmlIgnore]
        public Point YEndPoint
        {
            get
            {
                if ((_axisY == AxisDirectionEnum.向上 && _axisX == AxisDirectionEnum.向右) ||
                   (_axisY == AxisDirectionEnum.向左 && _axisX == AxisDirectionEnum.向下))
                {
                    return leftTopPoint;
                }
                else if ((_axisY == AxisDirectionEnum.向上 && _axisX == AxisDirectionEnum.向左) ||
                   (_axisY == AxisDirectionEnum.向右 && _axisX == AxisDirectionEnum.向下))
                {
                    return rightTopPoint;
                }
                else if ((_axisY == AxisDirectionEnum.向下 && _axisX == AxisDirectionEnum.向右) ||
                   (_axisY == AxisDirectionEnum.向左 && _axisX == AxisDirectionEnum.向上))
                {
                    return leftBottomPoint;
                }
                else if ((_axisY == AxisDirectionEnum.向下 && _axisX == AxisDirectionEnum.向左) ||
                   (_axisY == AxisDirectionEnum.向右 && _axisX == AxisDirectionEnum.向上))
                {
                    return rightBottomPoint;
                }
                else if (_axisX == AxisDirectionEnum.NULL && _axisY != AxisDirectionEnum.NULL)
                {
                    if (StartPoint == leftTopPoint)
                    {
                        return rightTopPoint;
                    }
                    else if (StartPoint == rightTopPoint)
                    {
                        return leftTopPoint;
                    }
                    else if (StartPoint == leftBottomPoint)
                    {
                        return rightBottomPoint;
                    }
                    else
                    {
                        return leftBottomPoint;
                    }
                }
                else
                {
                    return StartPoint;
                }
            }
        }

        /// <summary>
        /// 旋转方向起始点
        /// </summary>
        [XmlIgnore]
        public Point RStartPoint
        {
            get
            {
                if (AxisR == AxisREnum.NULL)
                {
                    return new Point(CanvssSize / 2, CanvssSize / 2);
                }
                else if (AxisR == AxisREnum.逆时针)
                {
                    return new Point((CanvssSize + Space) / 2, CanvssSize / 2);
                }
                else
                {
                    return new Point((CanvssSize - Space) / 2, CanvssSize / 2);
                }
            }
        }

        /// <summary>
        /// 旋转方向显示结束点
        /// </summary>
        [XmlIgnore]
        public Point REndPoint
        {
            get
            {
                if (AxisR == AxisREnum.NULL)
                {
                    return new Point(CanvssSize / 2, CanvssSize / 2);
                }
                else if (AxisR == AxisREnum.逆时针)
                {
                    return new Point((CanvssSize - Space) / 2, CanvssSize / 2);
                }
                else
                {
                    return new Point((CanvssSize + Space) / 2, CanvssSize / 2);
                }
            }
        }

        /// <summary>
        /// 旋转方向显示控制点
        /// </summary>
        [XmlIgnore]
        public Point RControlPoint
        {
            get
            {
                if (AxisR == AxisREnum.NULL)
                {
                    return new Point(CanvssSize / 2, CanvssSize / 2);
                }
                else
                {
                    return new Point(CanvssSize / 2, (CanvssSize - Space) / 2);
                }
            }
        }

        #endregion 绘图参数

        #endregion 坐标轴绘制属性
    }
}
