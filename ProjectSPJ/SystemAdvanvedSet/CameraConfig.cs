using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Camera;
using System.Xml.Serialization;
using DealConfigFile;
using BasicClass;

namespace ProjectSPJ
{
    public class CameraConfig : BaseSerializer
    {
        public static CameraConfig Camera1 = new CameraConfig();
        public static CameraConfig Camera2 = new CameraConfig();
        public static CameraConfig Camera3 = new CameraConfig();
        public static CameraConfig Camera4 = new CameraConfig();
        public static CameraConfig Camera5 = new CameraConfig();
        public static CameraConfig Camera6 = new CameraConfig();
        public static CameraConfig Camera7 = new CameraConfig();
        public static CameraConfig Camera8 = new CameraConfig();

        #region 相机基础参数
        /// <summary>
        /// 是否使用软触发
        /// </summary>
        public bool BlUsingTrigger { get; set; }

        /// <summary>
        /// 是否使用相机名称
        /// </summary>
        public bool BlNameCamera => _methodOpenCam == MethodOpenCamEnum.相机名称;

        /// <summary>
        /// 相机类型
        /// </summary>
        public TypeCamera_enum TypeCamera { get; set; } = TypeCamera_enum.HIKSDK;

        /// <summary>
        /// 触发源
        /// </summary>
        public TriggerSourceCamera_enum TriggerSource { get; set; } = TriggerSourceCamera_enum.Software;


        private MethodOpenCamEnum _methodOpenCam;
        /// <summary>
        /// 打开相机的方式，序列号或者名称
        /// </summary>
        public MethodOpenCamEnum MethodOpenCam
        {
            get
            {
                return _methodOpenCam;
            }
            set
            {
                _methodOpenCam = value;
                NotifyPropertyChanged(() => MethodOpenCam);
            }
        }
        /// <summary>
        /// 相机序列号或者名称
        /// </summary>
        public string CameraSerial { get; set; }
        /// <summary>
        /// 相机是否掉线重连
        /// </summary>
        public bool BlAutoConnectCamera { get; set; }

        #endregion 相机基础参数

        #region 相机工作参数
        /// <summary>
        /// 图像坐标类型
        /// </summary>
        public TypeImageCoord_enum TypeImageCoord { get; set; }
        /// <summary>
        /// 图片保存格式
        /// </summary>
        public FormatImage_enum FormatImage { get; set; }
        /// <summary>
        /// 相机拍照次数
        /// </summary>
        public NumPhoto_enum NumPhoto { get; set; }
        /// <summary>
        /// 是否保存全格式
        /// </summary>
        public bool BlAllFormat { get; set; }
        /// <summary>
        /// 金字塔级数
        /// </summary>
        public NumPhoto_enum Pyramid { get; set; }
        #endregion 相机工作参数


        #region Init
        public static void Init()
        {
            InitCamera1();
            InitCamera2();
            InitCamera3();
            InitCamera4();
            InitCamera5();
            InitCamera6();
            InitCamera7();
            InitCamera8();
        }

        /// <summary>
        /// 将相机1的参数加载过来
        /// </summary>
        /// <returns></returns>
        private static bool InitCamera1()
        {
            try
            {
                Camera1.TypeCamera = ParCamera1.P_I.TypeCamera_e;
                Camera1.CameraSerial = ParCamera1.P_I.Serial_Camera;
                Camera1.BlUsingTrigger = ParCamera1.P_I.BlUsingTrigger;
                Camera1.TriggerSource = ParCamera1.P_I.TriggerSource_e;
                Camera1.BlAutoConnectCamera = ParCamera1.P_I.BlAutoConnectCamera;
                if (ParCamera1.P_I.BlNameCamera)
                {
                    Camera1.MethodOpenCam = MethodOpenCamEnum.相机名称;
                }
                else
                {
                    Camera1.MethodOpenCam = MethodOpenCamEnum.序列号;
                }

                Camera1.NumPhoto = ParCameraWork.P_I.ParCameraWork_L[0].NumPhoto_e;
                Camera1.FormatImage = ParCameraWork.P_I.ParCameraWork_L[0].FormatImage_e;
                Camera1.TypeImageCoord = ParCameraWork.P_I.ParCameraWork_L[0].TypeImageCoord_e;
                Camera1.BlAllFormat = ParCameraWork.P_I.ParCameraWork_L[0].BlFull;
                Camera1.Pyramid = ParCameraWork.P_I.ParCameraWork_L[0].FenjieBit_e;
                return true;
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// 将相机2的参数加载过来
        /// </summary>
        /// <returns></returns>
        private static bool InitCamera2()
        {
            try
            {
                Camera2.TypeCamera = ParCamera2.P_I.TypeCamera_e;
                Camera2.CameraSerial = ParCamera2.P_I.Serial_Camera;
                Camera2.BlUsingTrigger = ParCamera2.P_I.BlUsingTrigger;
                Camera2.TriggerSource = ParCamera2.P_I.TriggerSource_e;
                Camera2.BlAutoConnectCamera = ParCamera2.P_I.BlAutoConnectCamera;
                if (ParCamera2.P_I.BlNameCamera)
                {
                    Camera2.MethodOpenCam = MethodOpenCamEnum.相机名称;
                }
                else
                {
                    Camera2.MethodOpenCam = MethodOpenCamEnum.序列号;
                }

                Camera2.NumPhoto = ParCameraWork.P_I.ParCameraWork_L[1].NumPhoto_e;
                Camera2.FormatImage = ParCameraWork.P_I.ParCameraWork_L[1].FormatImage_e;
                Camera2.TypeImageCoord = ParCameraWork.P_I.ParCameraWork_L[1].TypeImageCoord_e;
                Camera2.BlAllFormat = ParCameraWork.P_I.ParCameraWork_L[1].BlFull;
                Camera2.Pyramid = ParCameraWork.P_I.ParCameraWork_L[1].FenjieBit_e;
                return true;
            }
            catch
            {
                return false;
            }
        }


        /// <summary>
        /// 将相机3的参数加载过来
        /// </summary>
        /// <returns></returns>
        private static bool InitCamera3()
        {
            try
            {
                Camera3.TypeCamera = ParCamera3.P_I.TypeCamera_e;
                Camera3.CameraSerial = ParCamera3.P_I.Serial_Camera;
                Camera3.BlUsingTrigger = ParCamera3.P_I.BlUsingTrigger;
                Camera3.TriggerSource = ParCamera3.P_I.TriggerSource_e;
                Camera3.BlAutoConnectCamera = ParCamera3.P_I.BlAutoConnectCamera;
                if (ParCamera3.P_I.BlNameCamera)
                {
                    Camera3.MethodOpenCam = MethodOpenCamEnum.相机名称;
                }
                else
                {
                    Camera3.MethodOpenCam = MethodOpenCamEnum.序列号;
                }

                Camera3.NumPhoto = ParCameraWork.P_I.ParCameraWork_L[2].NumPhoto_e;
                Camera3.FormatImage = ParCameraWork.P_I.ParCameraWork_L[2].FormatImage_e;
                Camera3.TypeImageCoord = ParCameraWork.P_I.ParCameraWork_L[2].TypeImageCoord_e;
                Camera3.BlAllFormat = ParCameraWork.P_I.ParCameraWork_L[2].BlFull;
                Camera3.Pyramid = ParCameraWork.P_I.ParCameraWork_L[2].FenjieBit_e;
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 将相机4的参数加载过来
        /// </summary>
        /// <returns></returns>
        private static bool InitCamera4()
        {
            try
            {
                Camera4.TypeCamera = ParCamera4.P_I.TypeCamera_e;
                Camera4.CameraSerial = ParCamera4.P_I.Serial_Camera;
                Camera4.BlUsingTrigger = ParCamera4.P_I.BlUsingTrigger;
                Camera4.TriggerSource = ParCamera4.P_I.TriggerSource_e;
                Camera4.BlAutoConnectCamera = ParCamera4.P_I.BlAutoConnectCamera;
                if (ParCamera4.P_I.BlNameCamera)
                {
                    Camera4.MethodOpenCam = MethodOpenCamEnum.相机名称;
                }
                else
                {
                    Camera4.MethodOpenCam = MethodOpenCamEnum.序列号;
                }

                Camera4.NumPhoto = ParCameraWork.P_I.ParCameraWork_L[3].NumPhoto_e;
                Camera4.FormatImage = ParCameraWork.P_I.ParCameraWork_L[3].FormatImage_e;
                Camera4.TypeImageCoord = ParCameraWork.P_I.ParCameraWork_L[3].TypeImageCoord_e;
                Camera4.BlAllFormat = ParCameraWork.P_I.ParCameraWork_L[3].BlFull;
                Camera4.Pyramid = ParCameraWork.P_I.ParCameraWork_L[3].FenjieBit_e;
                return true;
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// 将相机5的参数加载过来
        /// </summary>
        /// <returns></returns>
        private static bool InitCamera5()
        {
            try
            {
                Camera5.TypeCamera = ParCamera5.P_I.TypeCamera_e;
                Camera5.CameraSerial = ParCamera5.P_I.Serial_Camera;
                Camera5.BlUsingTrigger = ParCamera5.P_I.BlUsingTrigger;
                Camera5.TriggerSource = ParCamera5.P_I.TriggerSource_e;
                Camera5.BlAutoConnectCamera = ParCamera5.P_I.BlAutoConnectCamera;
                if (ParCamera5.P_I.BlNameCamera)
                {
                    Camera5.MethodOpenCam = MethodOpenCamEnum.相机名称;
                }
                else
                {
                    Camera5.MethodOpenCam = MethodOpenCamEnum.序列号;
                }

                Camera5.NumPhoto = ParCameraWork.P_I.ParCameraWork_L[4].NumPhoto_e;
                Camera5.FormatImage = ParCameraWork.P_I.ParCameraWork_L[4].FormatImage_e;
                Camera5.TypeImageCoord = ParCameraWork.P_I.ParCameraWork_L[4].TypeImageCoord_e;
                Camera5.BlAllFormat = ParCameraWork.P_I.ParCameraWork_L[4].BlFull;
                Camera5.Pyramid = ParCameraWork.P_I.ParCameraWork_L[4].FenjieBit_e;
                return true;
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// 将相机6的参数加载过来
        /// </summary>
        /// <returns></returns>
        private static bool InitCamera6()
        {
            try
            {
                Camera6.TypeCamera = ParCamera6.P_I.TypeCamera_e;
                Camera6.CameraSerial = ParCamera6.P_I.Serial_Camera;
                Camera6.BlUsingTrigger = ParCamera6.P_I.BlUsingTrigger;
                Camera6.TriggerSource = ParCamera6.P_I.TriggerSource_e;
                Camera6.BlAutoConnectCamera = ParCamera6.P_I.BlAutoConnectCamera;
                if (ParCamera6.P_I.BlNameCamera)
                {
                    Camera6.MethodOpenCam = MethodOpenCamEnum.相机名称;
                }
                else
                {
                    Camera6.MethodOpenCam = MethodOpenCamEnum.序列号;
                }

                Camera6.NumPhoto = ParCameraWork.P_I.ParCameraWork_L[5].NumPhoto_e;
                Camera6.FormatImage = ParCameraWork.P_I.ParCameraWork_L[5].FormatImage_e;
                Camera6.TypeImageCoord = ParCameraWork.P_I.ParCameraWork_L[5].TypeImageCoord_e;
                Camera6.BlAllFormat = ParCameraWork.P_I.ParCameraWork_L[5].BlFull;
                Camera6.Pyramid = ParCameraWork.P_I.ParCameraWork_L[5].FenjieBit_e;
                return true;
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// 将相机7的参数加载过来
        /// </summary>
        /// <returns></returns>
        private static bool InitCamera7()
        {
            try
            {
                Camera7.TypeCamera = ParCamera7.P_I.TypeCamera_e;
                Camera7.CameraSerial = ParCamera7.P_I.Serial_Camera;
                Camera7.BlUsingTrigger = ParCamera7.P_I.BlUsingTrigger;
                Camera7.TriggerSource = ParCamera7.P_I.TriggerSource_e;
                Camera7.BlAutoConnectCamera = ParCamera7.P_I.BlAutoConnectCamera;
                if (ParCamera7.P_I.BlNameCamera)
                {
                    Camera7.MethodOpenCam = MethodOpenCamEnum.相机名称;
                }
                else
                {
                    Camera7.MethodOpenCam = MethodOpenCamEnum.序列号;
                }

                Camera7.NumPhoto = ParCameraWork.P_I.ParCameraWork_L[6].NumPhoto_e;
                Camera7.FormatImage = ParCameraWork.P_I.ParCameraWork_L[6].FormatImage_e;
                Camera7.TypeImageCoord = ParCameraWork.P_I.ParCameraWork_L[6].TypeImageCoord_e;
                Camera7.BlAllFormat = ParCameraWork.P_I.ParCameraWork_L[6].BlFull;
                Camera7.Pyramid = ParCameraWork.P_I.ParCameraWork_L[6].FenjieBit_e;
                return true;
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// 将相机8的参数加载过来
        /// </summary>
        /// <returns></returns>
        private static bool InitCamera8()
        {
            try
            {
                Camera8.TypeCamera = ParCamera8.P_I.TypeCamera_e;
                Camera8.CameraSerial = ParCamera8.P_I.Serial_Camera;
                Camera8.BlUsingTrigger = ParCamera8.P_I.BlUsingTrigger;
                Camera8.TriggerSource = ParCamera8.P_I.TriggerSource_e;
                Camera8.BlAutoConnectCamera = ParCamera8.P_I.BlAutoConnectCamera;
                if (ParCamera8.P_I.BlNameCamera)
                {
                    Camera8.MethodOpenCam = MethodOpenCamEnum.相机名称;
                }
                else
                {
                    Camera8.MethodOpenCam = MethodOpenCamEnum.序列号;
                }

                Camera8.NumPhoto = ParCameraWork.P_I.ParCameraWork_L[7].NumPhoto_e;
                Camera8.FormatImage = ParCameraWork.P_I.ParCameraWork_L[7].FormatImage_e;
                Camera8.TypeImageCoord = ParCameraWork.P_I.ParCameraWork_L[7].TypeImageCoord_e;
                Camera8.BlAllFormat = ParCameraWork.P_I.ParCameraWork_L[7].BlFull;
                Camera8.Pyramid = ParCameraWork.P_I.ParCameraWork_L[7].FenjieBit_e;
                return true;
            }
            catch
            {
                return false;
            }
        }
        #endregion Init
        
        #region Save
        public static void Save()
        {
            SaveCamera1();
            SaveCamera2();
            SaveCamera3();
            SaveCamera4();
            SaveCamera5();
            SaveCamera6();
            SaveCamera7();
            SaveCamera8();

            ParCameraWork.P_I.WriteIni();
        }
        /// <summary>
        /// 保存相机1参数
        /// </summary>
        private static void SaveCamera1()
        {
            ParCamera1.P_I.TypeCamera_e = Camera1.TypeCamera;
            ParCamera1.P_I.Serial_Camera = Camera1.CameraSerial;
            ParCamera1.P_I.BlUsingTrigger = Camera1.BlUsingTrigger;
            ParCamera1.P_I.TriggerSource_e = Camera1.TriggerSource;
            ParCamera1.P_I.BlNameCamera = Camera1.BlNameCamera;
            ParCamera1.P_I.BlAutoConnectCamera = Camera1.BlAutoConnectCamera;

            ParCameraWork.P_I.ParCameraWork_L[0].NumPhoto_e = Camera1.NumPhoto;
            ParCameraWork.P_I.ParCameraWork_L[0].FormatImage_e = Camera1.FormatImage;
            ParCameraWork.P_I.ParCameraWork_L[0].TypeImageCoord_e = Camera1.TypeImageCoord;
            ParCameraWork.P_I.ParCameraWork_L[0].BlFull = Camera1.BlAllFormat;
            ParCameraWork.P_I.ParCameraWork_L[0].FenjieBit_e = Camera1.Pyramid;

            ParCamera1.P_I.WriteIni();
        }

        /// <summary>
        /// 保存相机1参数
        /// </summary>
        private static void SaveCamera2()
        {
            ParCamera2.P_I.TypeCamera_e = Camera2.TypeCamera;
            ParCamera2.P_I.Serial_Camera = Camera2.CameraSerial;
            ParCamera2.P_I.BlUsingTrigger = Camera2.BlUsingTrigger;
            ParCamera2.P_I.TriggerSource_e = Camera2.TriggerSource;
            ParCamera2.P_I.BlNameCamera = Camera2.BlNameCamera;
            ParCamera2.P_I.BlAutoConnectCamera = Camera2.BlAutoConnectCamera;

            ParCameraWork.P_I.ParCameraWork_L[1].NumPhoto_e = Camera2.NumPhoto;
            ParCameraWork.P_I.ParCameraWork_L[1].FormatImage_e = Camera2.FormatImage;
            ParCameraWork.P_I.ParCameraWork_L[1].TypeImageCoord_e = Camera2.TypeImageCoord;
            ParCameraWork.P_I.ParCameraWork_L[1].BlFull = Camera2.BlAllFormat;
            ParCameraWork.P_I.ParCameraWork_L[1].FenjieBit_e = Camera2.Pyramid;

            ParCamera2.P_I.WriteIni();
        }

        /// <summary>
        /// 保存相机1参数
        /// </summary>
        private static void SaveCamera3()
        {
            ParCamera3.P_I.TypeCamera_e = Camera3.TypeCamera;
            ParCamera3.P_I.Serial_Camera = Camera3.CameraSerial;
            ParCamera3.P_I.BlUsingTrigger = Camera3.BlUsingTrigger;
            ParCamera3.P_I.TriggerSource_e = Camera3.TriggerSource;
            ParCamera3.P_I.BlNameCamera = Camera3.BlNameCamera;
            ParCamera3.P_I.BlAutoConnectCamera = Camera3.BlAutoConnectCamera;

            ParCameraWork.P_I.ParCameraWork_L[2].NumPhoto_e = Camera3.NumPhoto;
            ParCameraWork.P_I.ParCameraWork_L[2].FormatImage_e = Camera3.FormatImage;
            ParCameraWork.P_I.ParCameraWork_L[2].TypeImageCoord_e = Camera3.TypeImageCoord;
            ParCameraWork.P_I.ParCameraWork_L[2].BlFull = Camera3.BlAllFormat;
            ParCameraWork.P_I.ParCameraWork_L[2].FenjieBit_e = Camera3.Pyramid;

            ParCamera3.P_I.WriteIni();
        }

        /// <summary>
        /// 保存相机1参数
        /// </summary>
        private static void SaveCamera4()
        {
            ParCamera4.P_I.TypeCamera_e = Camera4.TypeCamera;
            ParCamera4.P_I.Serial_Camera = Camera4.CameraSerial;
            ParCamera4.P_I.BlUsingTrigger = Camera4.BlUsingTrigger;
            ParCamera4.P_I.TriggerSource_e = Camera4.TriggerSource;
            ParCamera4.P_I.BlNameCamera = Camera4.BlNameCamera;
            ParCamera4.P_I.BlAutoConnectCamera = Camera4.BlAutoConnectCamera;

            ParCameraWork.P_I.ParCameraWork_L[3].NumPhoto_e = Camera4.NumPhoto;
            ParCameraWork.P_I.ParCameraWork_L[3].FormatImage_e = Camera4.FormatImage;
            ParCameraWork.P_I.ParCameraWork_L[3].TypeImageCoord_e = Camera4.TypeImageCoord;
            ParCameraWork.P_I.ParCameraWork_L[3].BlFull = Camera4.BlAllFormat;
            ParCameraWork.P_I.ParCameraWork_L[3].FenjieBit_e = Camera4.Pyramid;

            ParCamera4.P_I.WriteIni();
        }

        /// <summary>
        /// 保存相机1参数
        /// </summary>
        private static void SaveCamera5()
        {
            ParCamera5.P_I.TypeCamera_e = Camera5.TypeCamera;
            ParCamera5.P_I.Serial_Camera = Camera5.CameraSerial;
            ParCamera5.P_I.BlUsingTrigger = Camera5.BlUsingTrigger;
            ParCamera5.P_I.TriggerSource_e = Camera5.TriggerSource;
            ParCamera5.P_I.BlNameCamera = Camera5.BlNameCamera;
            ParCamera5.P_I.BlAutoConnectCamera = Camera5.BlAutoConnectCamera;

            ParCameraWork.P_I.ParCameraWork_L[4].NumPhoto_e = Camera5.NumPhoto;
            ParCameraWork.P_I.ParCameraWork_L[4].FormatImage_e = Camera5.FormatImage;
            ParCameraWork.P_I.ParCameraWork_L[4].TypeImageCoord_e = Camera5.TypeImageCoord;
            ParCameraWork.P_I.ParCameraWork_L[4].BlFull = Camera5.BlAllFormat;
            ParCameraWork.P_I.ParCameraWork_L[4].FenjieBit_e = Camera5.Pyramid;

            ParCamera5.P_I.WriteIni();
        }

        /// <summary>
        /// 保存相机1参数
        /// </summary>
        private static void SaveCamera6()
        {
            ParCamera6.P_I.TypeCamera_e = Camera6.TypeCamera;
            ParCamera6.P_I.Serial_Camera = Camera6.CameraSerial;
            ParCamera6.P_I.BlUsingTrigger = Camera6.BlUsingTrigger;
            ParCamera6.P_I.TriggerSource_e = Camera6.TriggerSource;
            ParCamera6.P_I.BlNameCamera = Camera6.BlNameCamera;
            ParCamera6.P_I.BlAutoConnectCamera = Camera6.BlAutoConnectCamera;

            ParCameraWork.P_I.ParCameraWork_L[5].NumPhoto_e = Camera6.NumPhoto;
            ParCameraWork.P_I.ParCameraWork_L[5].FormatImage_e = Camera6.FormatImage;
            ParCameraWork.P_I.ParCameraWork_L[5].TypeImageCoord_e = Camera6.TypeImageCoord;
            ParCameraWork.P_I.ParCameraWork_L[5].BlFull = Camera6.BlAllFormat;
            ParCameraWork.P_I.ParCameraWork_L[5].FenjieBit_e = Camera6.Pyramid;

            ParCamera6.P_I.WriteIni();
        }

        /// <summary>
        /// 保存相机1参数
        /// </summary>
        private static void SaveCamera7()
        {
            ParCamera7.P_I.TypeCamera_e = Camera7.TypeCamera;
            ParCamera7.P_I.Serial_Camera = Camera7.CameraSerial;
            ParCamera7.P_I.BlUsingTrigger = Camera7.BlUsingTrigger;
            ParCamera7.P_I.TriggerSource_e = Camera7.TriggerSource;
            ParCamera7.P_I.BlNameCamera = Camera7.BlNameCamera;
            ParCamera7.P_I.BlAutoConnectCamera = Camera7.BlAutoConnectCamera;

            ParCameraWork.P_I.ParCameraWork_L[6].NumPhoto_e = Camera7.NumPhoto;
            ParCameraWork.P_I.ParCameraWork_L[6].FormatImage_e = Camera7.FormatImage;
            ParCameraWork.P_I.ParCameraWork_L[6].TypeImageCoord_e = Camera7.TypeImageCoord;
            ParCameraWork.P_I.ParCameraWork_L[6].BlFull = Camera7.BlAllFormat;
            ParCameraWork.P_I.ParCameraWork_L[6].FenjieBit_e = Camera7.Pyramid;

            ParCamera7.P_I.WriteIni();
        }

        /// <summary>
        /// 保存相机1参数
        /// </summary>
        private static void SaveCamera8()
        {
            ParCamera8.P_I.TypeCamera_e = Camera8.TypeCamera;
            ParCamera8.P_I.Serial_Camera = Camera8.CameraSerial;
            ParCamera8.P_I.BlUsingTrigger = Camera8.BlUsingTrigger;
            ParCamera8.P_I.TriggerSource_e = Camera8.TriggerSource;
            ParCamera8.P_I.BlNameCamera = Camera8.BlNameCamera;
            ParCamera8.P_I.BlAutoConnectCamera = Camera8.BlAutoConnectCamera;

            ParCameraWork.P_I.ParCameraWork_L[7].NumPhoto_e = Camera8.NumPhoto;
            ParCameraWork.P_I.ParCameraWork_L[7].FormatImage_e = Camera8.FormatImage;
            ParCameraWork.P_I.ParCameraWork_L[7].TypeImageCoord_e = Camera8.TypeImageCoord;
            ParCameraWork.P_I.ParCameraWork_L[7].BlFull = Camera8.BlAllFormat;
            ParCameraWork.P_I.ParCameraWork_L[7].FenjieBit_e = Camera8.Pyramid;

            ParCamera8.P_I.WriteIni();
        }
        #endregion Save

        #region List for binding
        /// <summary>
        /// 用于绑定的相机类型list
        /// </summary>
        [XmlIgnore]
        public List<TypeCamera_enum> TypeCameraBinding_L
        {
            get
            {
                List<TypeCamera_enum> list = new List<TypeCamera_enum>();
                foreach (TypeCamera_enum item in Enum.GetValues(typeof(TypeCamera_enum)))
                {
                    list.Add(item);
                }
                return list;
            }
        }

        /// <summary>
        /// 用于绑定的触发源list
        /// </summary>
        [XmlIgnore]
        public List<TriggerSourceCamera_enum> TriggerSourceBinding_L
        {
            get
            {
                List<TriggerSourceCamera_enum> list = new List<TriggerSourceCamera_enum>();
                foreach (TriggerSourceCamera_enum item in Enum.GetValues(typeof(TriggerSourceCamera_enum)))
                {
                    list.Add(item);
                }
                return list;
            }
        }

        /// <summary>
        /// 打开相机方法list
        /// </summary>
        [XmlIgnore]
        public List<MethodOpenCamEnum> MethodOpenCamBinding_L
        {
            get
            {
                List<MethodOpenCamEnum> list = new List<MethodOpenCamEnum>();
                foreach (MethodOpenCamEnum item in Enum.GetValues(typeof(MethodOpenCamEnum)))
                {
                    list.Add(item);
                }
                return list;
            }
        }

        /// <summary>
        /// 图像坐标选择
        /// </summary>
        [XmlIgnore]
        public List<TypeImageCoord_enum> TypeImageCoordBinding_L
        {
            get
            {
                List<TypeImageCoord_enum> list = new List<TypeImageCoord_enum>();
                foreach (TypeImageCoord_enum item in Enum.GetValues(typeof(TypeImageCoord_enum)))
                {
                    list.Add(item);
                }
                return list;
            }
        }

        /// <summary>
        /// 图片格式list
        /// </summary>
        [XmlIgnore]
        public List<FormatImage_enum> FormatImageBinding_L
        {
            get
            {
                List<FormatImage_enum> list = new List<FormatImage_enum>();
                foreach (FormatImage_enum item in Enum.GetValues(typeof(FormatImage_enum)))
                {
                    list.Add(item);
                }
                return list;
            }
        }
        /// <summary>
        /// 相机拍照次数
        /// </summary>
        [XmlIgnore]
        public List<NumPhoto_enum> NumPhotoBinding_L
        {
            get
            {
                List<NumPhoto_enum> list = new List<NumPhoto_enum>();
                foreach (NumPhoto_enum item in Enum.GetValues(typeof(NumPhoto_enum)))
                {
                    list.Add(item);
                }
                return list;

            }
        }
        #endregion list for binding
    }
}
