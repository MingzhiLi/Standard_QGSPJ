using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectSPJ
{
    /// <summary>
    /// 相机功能枚举
    /// </summary>
    public enum CameraFunEnum
    {
        粗定位,
        精定位,
        残材,
        单目,
        卡塞,
        NULL,
    }

    /// <summary>
    /// 平台放片位置
    /// </summary>
    public enum PlatPlacePosEnum
    {
        LeftTop,
        LeftBottom,
        RightBottom,
        RightTop
    }

    /// <summary>
    /// X和Y轴方向枚举
    /// </summary>
    public enum AxisDirectionEnum
    {
        NULL = 0,
        向右 = 0x001,
        向左 = 0x002,
        向上 = 0x004,
        向下 = 0x008,
    }

    /// <summary>
    /// 拍照时轴移动的方向
    /// </summary>
    public enum MoveDirecitonEnum
    {
        NULL = 0,
        从左向右 = 0x001,
        从右向左 = 0x002,
        从上向下 = 0x004,
        从下向上 = 0x008,
    }
    /// <summary>
    /// 旋转轴方向枚举
    /// </summary>
    public enum AxisREnum
    {
        /// <summary>
        /// 没有R轴
        /// </summary>
        NULL = 0,
        /// <summary>
        /// 图像方向为逆时针为正，当图像向逆时针方向偏时，补正向顺时针方向补正
        /// </summary>
        顺时针 = 1,
        /// <summary>
        /// 
        /// </summary>
        逆时针 = -1,
    }

    /// <summary>
    /// 残材相机枚举
    /// </summary>
    public enum ResidueEnum
    {
        /// <summary>
        /// 残材1
        /// </summary>
        残材1 = 1,
        /// <summary>
        /// 残材2
        /// </summary>
        残材2 = 2,
        /// <summary>
        /// 残材3
        /// </summary>
        残材3 = 3,
    }


    /// <summary>
    /// 相机打开方式
    /// </summary>
    public enum MethodOpenCamEnum
    {
        序列号,
        相机名称,
    }
}
