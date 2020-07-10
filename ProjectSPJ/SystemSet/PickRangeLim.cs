using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Shapes;
using System.Reflection;


namespace ProjectSPJ
{
    /// <summary>
    /// 粗定位取料范围限制
    /// </summary>
    public class PickRangeLim : BaseSerializer
    {
        public static PickRangeLim Inst = new PickRangeLim();

        /// <summary>
        /// 文件保存路径名称，如果不重写则会以基类名称来保存
        /// </summary>
        protected override string NameFile => MethodBase.GetCurrentMethod().DeclaringType.Name;


        /// <summary>
        /// 读取本地配置文件
        /// </summary>
        public static void ReadLocalFile()
        {
            Inst = Inst.ReadLocalFile<PickRangeLim>();
        }

        #region private memeber
        /// <summary>
        /// 区域限制的Rect List
        /// </summary>
        private List<LimitRect> _limitRects_L = new List<LimitRect>();
        /// <summary>
        /// 标定的范围
        /// </summary>
        private LimitRect _calibRange = new LimitRect();
        #endregion private

        #region public 
        /// <summary> 
        /// 标定的范围
        /// </summary>
        public LimitRect CalibRange
        {
            get
            {
                return _calibRange;
            }
            set
            {
                _calibRange = value;
            }
        }

        /// <summary>
        /// 区域限制的Rect,主要用于UI界面显示及参数的输入方便
        /// </summary>
        public List<LimitRect> LimitRects_L
        {
            get
            {
                return _limitRects_L;
            }
            set
            {
                _limitRects_L = value;
            }
        }
       
        /// <summary>
        /// 标定的取料范围,用于逻辑比较
        /// </summary>
        public Rect RectCalibRange
        {
            get
            {
                return new Rect(_calibRange.LeftTop, _calibRange.RightBottom);
            }
        }
        /// <summary>
        /// 存储限制区域的list，主要用于逻辑判断，调用C#库比较方便
        /// </summary>
        public List<Rect> Rects_L
        {
            get
            {
                List<Rect> rectList_L = new List<Rect>();
                if (LimitRects_L?.Count != 0)
                {
                    foreach (LimitRect limitRect in LimitRects_L)
                    {
                        rectList_L.Add(new Rect(limitRect.LeftTop, limitRect.RightBottom));
                    }
                }
                return rectList_L;
            }
        }

        /// <summary>
        /// 判断设置的限制范围是否在标定范围内
        /// </summary>
        /// <returns></returns>
        public bool IsLimitRectInCalibRange()
        {
            foreach (Rect rect in Rects_L)
            {
                if (!RectCalibRange.Contains(rect))
                {
                    return false;
                }
            }
            return true;
        }
        #endregion public

        #region public method
        /// <summary>
        /// 增加限制区域
        /// </summary>
        /// <param name="limitRect"></param>
        public void AddLimitRegion(LimitRect limitRect)
        {
            LimitRect test = _calibRange;
            _limitRects_L.Add(new LimitRect());
            this.NotifyPropertyChanged(() => LimitRects_L);
        }
        /// <summary>
        /// 删除限制区域
        /// </summary>
        /// <param name="index"></param>
        public void DeleteLimitRegion(int index)
        {
            _limitRects_L.RemoveAt(index);
        }

        /// <summary>
        /// 判断输入点位是否在限制取料范围内
        /// </summary>
        /// <param name="point">输入点位</param>
        /// <param name="announce">点位所在限制范围内的注释</param>
        /// <returns></returns>
        public bool IsInLimitRegion(Point point, RoughlyResult roughlyResult)
        {
            if(_limitRects_L?.Count > 0)
            {
                foreach(LimitRect limitRect in _limitRects_L)
                {
                    Rect rect = new Rect(limitRect.LeftTop, limitRect.RightBottom);
                    if(rect.Contains(point))
                    {
                        roughlyResult.Info = "当前取料点在限制取料区域：" + limitRect.Announce;
                        return false;
                    }
                }
            }
            return true;
        }
        #endregion public method
    }

    /// <summary>
    /// 主要用于绑定的长方形类，定义长方形的左上角点和右下角点
    /// </summary>
    public class LimitRect : BaseSerializer
    {
        /// <summary>
        /// 左上角点X坐标
        /// </summary>
        public double LeftTopX { get; set; } = 2;
        /// <summary>
        /// 左上角点Y坐标
        /// </summary>
        public double LeftTopY { get; set; } = 2;
        /// <summary>
        /// 右下角点X坐标
        /// </summary>
        public double RightBottomX { get; set; } = 10;
        /// <summary>
        /// 右下角点Y坐标
        /// </summary>
        public double RightBottomY { get; set; } = 10;

        public string Announce { get; set; } = "Test";

        /// <summary>
        /// 左上角点坐标
        /// </summary>
        public Point LeftTop => new Point(LeftTopX, LeftTopY);
        /// <summary>
        /// 右上角点左边
        /// </summary>
        public Point RightBottom => new Point(RightBottomX, RightBottomY);
    }

}
