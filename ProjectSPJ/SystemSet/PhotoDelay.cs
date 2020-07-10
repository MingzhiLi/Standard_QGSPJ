using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Reflection;

namespace ProjectSPJ
{
    public class PhotoDelay : BaseSerializer
    {
        public static PhotoDelay Inst = new PhotoDelay();

        /// <summary>
        /// 文件保存路径名称，如果不重写则会以基类名称来保存
        /// </summary>
        protected override string NameFile => MethodBase.GetCurrentMethod().DeclaringType.Name;

        /// <summary>
        /// 读取本地配置文件
        /// </summary>
        public static void ReadLocalFile()
        {
            Inst = Inst.ReadLocalFile<PhotoDelay>();
        }
        /// <summary>
        /// 粗定位拍照延时
        /// </summary>
        public int DelayRoughly { get; set; }
        /// <summary>
        /// 标定时粗定位拍照延时
        /// </summary>
        public int DelayRoughlyCalib { get; set; }
        /// <summary>
        /// 精定位位置1拍照延时
        /// </summary>
        public int DelayPrecisePos1 { get; set; }
        /// <summary>
        /// 精定位位置2拍照延时
        /// </summary>
        public int DelayPrecisePos2 { get; set; }
        /// <summary>
        /// 精定位标定拍照延时
        /// </summary>
        public int DelayPreciseCalib { get; set; }

        /// <summary>
        /// 残材1拍照延时
        /// </summary>
        public int DelayResidue1 { get; set; }
        /// <summary>
        /// 残材2拍照延时
        /// </summary>
        public int DelayResidue2 { get; set; }
        /// <summary>
        /// 残材3拍照延时
        /// </summary>
        public int DelayResidue3 { get; set; }
        /// <summary>
        /// 残材标定拍照延时
        /// </summary>
        public int DelayResidueCalib { get; set; }


        /// <summary>
        /// 单目相机位置1拍照延时
        /// </summary>
        public int DelayMonoPos1 { get; set; }
        /// <summary>
        /// 单目相机位置2拍照延时
        /// </summary>
        public int DelayMonoPos2 { get; set; }
        /// <summary>
        /// 单目相机标定拍照延时
        /// </summary>
        public int DelayMonoCalib { get; set; }

        /// <summary>
        /// 卡塞相机拍照延时
        /// </summary>
        public int DelayCST { get; set; }
        /// <summary>
        /// 卡塞标定拍照延时
        /// </summary>
        public int DelayCSTCalib { get; set; }
    }
}
