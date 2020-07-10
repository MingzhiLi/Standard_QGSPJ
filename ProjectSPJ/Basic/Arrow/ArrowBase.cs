using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;
using System.Windows;
using System.Windows.Media;

namespace DealGeometry
{
    /// <summary>
    /// 箭头形状的基类
    /// </summary>
    public abstract class ArrowBase: Shape
    {
        #region 基本成员
        /// <summary>
        /// 整个箭头形状（包括前端箭头及形状）
        /// </summary>
        private readonly PathGeometry wholeGeometry = new PathGeometry();

        /// <summary>
        /// 除去箭头外的形状
        /// </summary>
        private readonly PathFigure figureConcerte = new PathFigure();

        /// <summary>
        /// 开始处的箭头形状
        /// </summary>
        private readonly PathFigure figureStart = new PathFigure();

        /// <summary>
        /// 结尾处的箭头形状
        /// </summary>
        private readonly PathFigure figureEnd = new PathFigure();
        #endregion

        protected static string GetPropertyName<T>(Expression<Func<T>> propertyExpression)
        {
            var expression = propertyExpression.Body as System.Linq.Expressions.MemberExpression;
            return expression.Member.Name;
        }

        #region 依赖属性-定义的Arrow控件的属性
        /// <summary>
        /// 箭头两边夹角的依赖属性
        /// </summary>
        public static readonly DependencyProperty ArrowAngleProperty =
            DependencyProperty.Register("ArrowAngle", typeof(double), typeof(ArrowBase),
                new FrameworkPropertyMetadata(45.0, FrameworkPropertyMetadataOptions.AffectsMeasure));
        /// <summary>
        /// 箭头两边的夹角
        /// </summary>
        public double ArrowAngle
        {
            get { return (double)GetValue(ArrowAngleProperty); }
            set { SetValue(ArrowAngleProperty, value); }
        }

        /// <summary>
        /// 箭头前端长度的依赖属性
        /// </summary>
        public static readonly DependencyProperty ArrowLengthProperty =
            DependencyProperty.Register("ArrowLength", typeof(double), typeof(ArrowBase),
                new FrameworkPropertyMetadata(10.0, FrameworkPropertyMetadataOptions.AffectsMeasure));
        /// <summary>
        /// 箭头前端长度
        /// </summary>
        public double ArrowLength
        {
            get { return (double)GetValue(ArrowLengthProperty); }
            set { SetValue(ArrowAngleProperty, value); }
        }

        /// <summary>
        /// 箭头所在端属性依赖
        /// </summary>
        public static readonly DependencyProperty ArrowEndsProperty =
            DependencyProperty.Register("ArrowEnds", typeof(ArrowEnds_Enum), typeof(ArrowBase),
                new FrameworkPropertyMetadata(ArrowEnds_Enum.End, FrameworkPropertyMetadataOptions.AffectsMeasure));
        /// <summary>
        /// 箭头所在端属性
        /// </summary>
        public ArrowEnds_Enum ArrowEnds
        {
            get { return (ArrowEnds_Enum)GetValue(ArrowEndsProperty); }
            set { SetValue(ArrowEndsProperty, value); }
        }


        /// <summary>
        /// 箭头是否闭合依赖属性
        /// </summary>
        public static readonly DependencyProperty IsArrowClosedProperty =
            DependencyProperty.Register("IsArrowClosed", typeof(bool), typeof(ArrowBase),
                new FrameworkPropertyMetadata(true, FrameworkPropertyMetadataOptions.AffectsMeasure));
        /// <summary>
        /// 箭头是否闭合
        /// </summary>
        public bool IsArrowClosed
        {
            get { return (bool)GetValue(IsArrowClosedProperty); }
            set { SetValue(IsArrowClosedProperty, value); }
        }

        /// <summary>
        /// 起始点坐标依赖属性
        /// </summary>
        public static readonly DependencyProperty StartPointProperty =
            DependencyProperty.Register("StartPoint", typeof(Point), typeof(ArrowBase),
                new FrameworkPropertyMetadata(default(Point), FrameworkPropertyMetadataOptions.AffectsMeasure));
        /// <summary>
        /// 起始点坐标
        /// </summary>
        public Point StartPoint
        {
            get { return (Point)GetValue(StartPointProperty); }
            set { SetValue(StartPointProperty, value); }
        }

        #endregion 属性


        #region 保护级方法
        /// <summary>
        /// 构造函数
        /// </summary>
        protected ArrowBase()
        {
            var polyLineSegStart = new PolyLineSegment();
            figureStart.Segments.Add(polyLineSegStart);

            var polyLineSegEnd = new PolyLineSegment();
            figureEnd.Segments.Add(polyLineSegEnd);

            wholeGeometry.Figures.Add(figureConcerte);
            wholeGeometry.Figures.Add(figureStart);
            wholeGeometry.Figures.Add(figureEnd);
        }

        /// <summary>
        /// 获取具体形状的各个部分
        /// </summary>
        /// <returns></returns>
        protected abstract PathSegmentCollection FillFigure();

        /// <summary>
        /// 获取开始箭头处的结束点
        /// </summary>
        /// <returns></returns>
        protected abstract Point GetStartArrowEndPoint();
        /// <summary>
        /// 获取结束箭头处的开始点
        /// </summary>
        /// <returns></returns>
        protected abstract Point GetEndArrowStartPoint();
        /// <summary>
        /// 获取结束箭头处的结束点
        /// </summary>
        /// <returns></returns>
        protected abstract Point GetEndArrowEndPoint();

        /// <summary>
        /// 定义形状
        /// </summary>
        protected override Geometry DefiningGeometry
        {
            get
            {
                figureConcerte.StartPoint = StartPoint;
                figureConcerte.Segments.Clear();
                var segements = FillFigure();
                if (segements != null)
                {
                    foreach (var segment in segements)
                    {
                        figureConcerte.Segments.Add(segment);
                    }
                }
                ///绘制起始处箭头
                if ((ArrowEnds & ArrowEnds_Enum.Start) == ArrowEnds_Enum.Start)
                {
                    CalculateArrow(figureStart, GetStartArrowEndPoint(), StartPoint);
                }
                ///绘制结束处箭头
                if((ArrowEnds & ArrowEnds_Enum.End) == ArrowEnds_Enum.End)
                {
                    CalculateArrow(figureEnd, GetEndArrowStartPoint(), GetEndArrowEndPoint());
                }

                return wholeGeometry;
            }
        }

        #endregion 保护级方法

        #region 私有方法
        /// <summary>
        /// 计算两点之间的有向箭头
        /// </summary>
        /// <param name="pathfig">箭头所在的型形状</param>
        /// <param name="startPoint">起始点</param>
        /// <param name="endPoint">结束点</param>
        private void CalculateArrow(PathFigure pathfig, Point startPoint ,Point endPoint)
        {
            var polyseg = pathfig.Segments[0] as PolyLineSegment;
            if(polyseg!= null)
            {
                ///用于变换的3*3矩阵
                var matx = new Matrix();
                //箭头向量
                Vector vect = startPoint - endPoint;
                ///将此向量转换位单位向量
                vect.Normalize();
                vect *= ArrowLength;
                ///旋转夹角的一半
                matx.Rotate(ArrowAngle / 2);
                ///上半段箭头
                pathfig.StartPoint = endPoint + vect * matx;    
                polyseg.Points.Clear();
                ///箭头顶端
                polyseg.Points.Add(endPoint);
                ///将角度转回并反方向旋转旋转夹角的一半
                matx.Rotate(-ArrowAngle);
                ///下半段箭头
                polyseg.Points.Add(endPoint + vect * matx);  
            }
            pathfig.IsClosed = IsArrowClosed;
        }
        #endregion 私有方法
    }
}
