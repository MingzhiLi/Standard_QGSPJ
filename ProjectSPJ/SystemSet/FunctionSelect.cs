using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Security.RightsManagement;

namespace ProjectSPJ
{
    public sealed class FunctionSelect : BaseSerializer
    {
        public static FunctionSelect Inst = new FunctionSelect();
        /// <summary>
        /// 文件保存路径名称，如果不重写则会以基类名称来保存
        /// </summary>
        protected override string NameFile => MethodBase.GetCurrentMethod().DeclaringType.Name;

        /// <summary>
        /// 读取本地配置文件
        /// </summary>
        public static void ReadLocalFile()
        {
            Inst = Inst.ReadLocalFile<FunctionSelect>();
        }
        /// <summary>
        /// 启用精定位相机
        /// </summary>
        public bool IsEnablePrecise { get; set; } = true;
        /// <summary>
        /// 大尺寸产品精定位左右移动拍照
        /// </summary>
        public bool IsPreciseMovePhoto { get; set; } = true;
        /// <summary>
        /// 左右移动拍照时，检查产品长宽
        /// </summary>
        public bool IsCheckLength { get; set; } = true;
        /// <summary>
        /// 启用单目相机
        /// </summary>
        public bool IsEnableMono { get; set; } = true;

        #region 残材相关
        /// <summary>
        /// 连续低清晰度报警
        /// </summary>
        public bool IsSharpnessAlarm { get; set; } = true;
        /// <summary>
        /// 连续低清晰度次数
        /// </summary>
        public int ContinuousTimes { get; set; } = 5;
        /// <summary>
        /// 启用残材检测1
        /// </summary>
        public bool IsEnableResidue1 { get; set; } = true;
        /// <summary>
        /// 启用残材检测2
        /// </summary>
        public bool IsEnableResidue2 { get; set; } = true;
        /// <summary>
        /// 启用残材检测3
        /// </summary>
        public bool IsEnableResudue3 { get; set; } = false;

        #endregion 残材相关

        /// <summary>
        /// 检查卡塞左右偏差
        /// </summary>
        public bool IsCheckCSTDevX { get; set; } = true;
        /// <summary>
        /// 检查卡塞高度偏差
        /// </summary>
        public bool IsCheckCSTHeightDev { get; set; } = true;
        /// <summary>
        /// 检查卡塞层间距偏差
        /// </summary>
        public bool IsCheckCSTLayerSpacing { get; set; } = true;
        /// <summary>
        /// 检查卡塞左右龙骨高度差
        /// </summary>
        public bool IsCheckCSTHeightLRDiff { get; set; } = true;
        /// <summary>
        /// 检查龙骨左右间距偏差
        /// </summary>
        public bool IsCheckCSTKeelSpacing { get; set; } = true;

        /// <summary>
        /// 是否保存OK图片
        /// </summary>
        public bool IsSaveOKImage { get; set; } = true;
        /// <summary>
        /// 是否保存NG图片
        /// </summary>
        public bool IsSaveNGImage { get; set; } = true;
        /// <summary>
        /// 保存OK屏幕截图
        /// </summary>
        public bool IsSaveOKScreenShot { get; set; } = false;
        /// <summary>
        /// 保存NG屏幕截图
        /// </summary>
        public bool IsSaveNGScreenShot { get; set; } = true;

    }
}
