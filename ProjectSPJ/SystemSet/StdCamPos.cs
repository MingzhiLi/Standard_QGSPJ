using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using BasicClass;

namespace ProjectSPJ
{
    public class StdCamPos : BaseSerializer
    {
        public static StdCamPos Inst = new StdCamPos();

        /// <summary>
        /// 文件保存路径名称，如果不重写则会以基类名称来保存
        /// </summary>
        protected override string NameFile => MethodBase.GetCurrentMethod().DeclaringType.Name;

        /// <summary>
        /// 读取本地配置文件
        /// </summary>
        public static void ReadLocalFile()
        {
            Inst = Inst.ReadLocalFile<StdCamPos>();
        }

        #region 精定位基准位
        /// <summary>
        /// 精定位基准位X,像素坐标
        /// </summary>
        public double PreciseCamStdX { get; set; }
        /// <summary>
        /// 精定位基准位Y，像素坐标  
        /// </summary>
        public double PreciseCamStdY { get; set; }
        #endregion 精定位基准位

        #region 单目相机基准
        public Point2D MonoCamStd => new Point2D(MonoCamStdX, MonoCamStdY);
        /// <summary>
        /// 单目相机基准位X，像素坐标
        /// </summary>
        public double MonoCamStdX { get; set; } = 480;
        /// <summary>
        /// 精定位基准位Y，像素坐标
        /// </summary>
        public double MonoCamStdY { get; set; } = 640;
        #endregion 单目相机基准

        #region 卡塞相机基准位
        /// <summary>
        /// 卡塞相机基准X
        /// </summary>
        public double CSTCamStdX { get; set; } = 640;
        /// <summary>
        /// 卡塞相机基准Y 
        /// </summary>
        public double CSTCamStdY { get; set; } = 480;
        #endregion 卡塞相机基准位
    }
}
