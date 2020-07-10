using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace DealGeometry
{
    /// <summary>
    /// 两点之间带箭头的直线
    /// </summary>
    public class ArrowLine : ArrowBase
    {
        #region 基本成员
        /// <summary>
        /// 线段
        /// </summary>
        private readonly LineSegment lineSegment = new LineSegment();
        #endregion 基本成员

        #region 属性
        /// <summary>
        /// 结束点属性依赖
        /// </summary>
        public static readonly DependencyProperty EndPointProperty =
            DependencyProperty.Register("EndPoint", typeof(Point), typeof(ArrowBase),
                new FrameworkPropertyMetadata(default(Point), FrameworkPropertyMetadataOptions.AffectsMeasure));

        /// <summary>
        /// 结束点
        /// </summary>
        public Point EndPoint
        {
            get { return (Point)GetValue(EndPointProperty); }
            set { SetValue(EndPointProperty, value); }
        }
        #endregion 属性

        #region 继承的保护接口
        /// <summary>
        /// 填充figure
        /// </summary>
        /// <returns></returns>
        protected override PathSegmentCollection FillFigure()
        {
            lineSegment.Point = EndPoint;
            return new PathSegmentCollection { lineSegment };
        }

        /// <summary>
        /// 获取开始箭头处的结束点
        /// </summary>
        /// <returns></returns>
        protected override Point GetStartArrowEndPoint()
        {
            return EndPoint;
        }

        /// <summary>
        /// 获取结束箭头处的开始点
        /// </summary>
        /// <returns></returns>
        protected override Point GetEndArrowStartPoint()
        {
            return StartPoint;
        }

        /// <summary>
        /// 获取结束箭头处的结束点
        /// </summary>
        /// <returns></returns>
        protected override Point GetEndArrowEndPoint()
        {
            return EndPoint;
        }
        #endregion 继承的保护接口
    }
}
