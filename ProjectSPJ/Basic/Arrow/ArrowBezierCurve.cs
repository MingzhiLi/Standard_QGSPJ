using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace DealGeometry
{
    public class ArrowBezierCurve: ArrowBase
    {
        #region 基本成员
        /// <summary>
        /// 贝塞尔曲线
        /// </summary>
        private readonly BezierSegment bezierSegment = new BezierSegment();
        #endregion 基本成员

        #region 属性
        /// <summary>
        /// 控制点1属性依赖
        /// </summary>
        public static readonly DependencyProperty ControlPoint1Property =
            DependencyProperty.Register("ControlPoint1", typeof(Point), typeof(ArrowBezierCurve),
                new FrameworkPropertyMetadata(new Point(), FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsRender));
        /// <summary>
        /// 控制点1
        /// </summary>
        public Point ControlPoint1
        {
            get { return (Point)GetValue(ControlPoint1Property); }
            set { SetValue(ControlPoint1Property, value); }
        }        

        /// <summary>
        /// 控制点2
        /// </summary>
        public static readonly DependencyProperty ControlPoint2Property = DependencyProperty.Register(
            "ControlPoint2", typeof(Point), typeof(ArrowBezierCurve),
            new FrameworkPropertyMetadata(new Point(), FrameworkPropertyMetadataOptions.AffectsMeasure));

        /// <summary>
        /// 控制点2
        /// </summary>
        public Point ControlPoint2
        {
            get { return (Point)GetValue(ControlPoint2Property); }
            set { SetValue(ControlPoint2Property, value); }
        }

        /// <summary>
        /// 结束点属性依赖
        /// </summary>
        public static readonly DependencyProperty EndPointProperty =
            DependencyProperty.Register("ControlPoint2", typeof(Point), typeof(ArrowBezierCurve),
                new FrameworkPropertyMetadata(new Point(), FrameworkPropertyMetadataOptions.AffectsMeasure));
        /// <summary>
        /// 结束点
        /// </summary>
        public Point EndPoint
        {
            get { return (Point)GetValue(EndPointProperty); }
            set { SetValue(EndPointProperty, value); }
        }
        #endregion 属性

        #region 继承的保护方法
        /// <summary>
        /// 填充figure
        /// </summary>
        /// <returns></returns>
        protected override PathSegmentCollection FillFigure()
        {
            bezierSegment.Point1 = ControlPoint1;
            bezierSegment.Point2 = ControlPoint2;
            bezierSegment.Point3 = EndPoint;
            return new PathSegmentCollection { bezierSegment };
        }
        /// <summary>
        /// 获取开始箭头处的结束点
        /// </summary>
        /// <returns></returns>
        protected override Point GetStartArrowEndPoint()
        {
            return ControlPoint1;
        }
        /// <summary>
        /// 获取结束箭头的开始点
        /// </summary>
        /// <returns></returns>
        protected override Point GetEndArrowStartPoint()
        {
            return ControlPoint2;
        }
        /// <summary>
        /// 获取结束箭头的结束点
        /// </summary>
        /// <returns></returns>
        protected override Point GetEndArrowEndPoint()
        {
            return EndPoint;
        }
        #endregion 继承的保护方法
    }
}
