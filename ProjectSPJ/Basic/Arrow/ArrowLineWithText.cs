using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.Windows;
using System.Windows.Media;

namespace DealGeometry
{
    /// <summary>
    /// 带有文本注释的箭头类
    /// </summary>
    public class ArrowLineWithText: ArrowLine
    {
        #region 基本属性
        /// <summary>
        /// 文本依赖属性
        /// </summary>
        public static readonly DependencyProperty TextProperty = DependencyProperty.Register(
             "Text", typeof(string), typeof(ArrowLineWithText),
              new FrameworkPropertyMetadata(string.Empty, FrameworkPropertyMetadataOptions.AffectsRender));
        /// <summary>
        /// 文本
        /// </summary>
        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        /// <summary>
        /// 文本对齐方式依赖属性
        /// </summary>
        public static readonly DependencyProperty TextAlignmentProperty = DependencyProperty.Register(
            "TextAlignment", typeof(TextAlignment), typeof(ArrowLineWithText),
            new FrameworkPropertyMetadata(TextAlignment.Center, FrameworkPropertyMetadataOptions.AffectsRender));
        /// <summary>
        /// 文本对齐方式
        /// </summary>
        public TextAlignment TextAlignment
        {
            get { return (TextAlignment)GetValue(TextAlignmentProperty); }
            set { SetValue(TextAlignmentProperty, value); }
        }

        /// <summary>
        /// 文本是否朝上依赖属性
        /// </summary>
        public static readonly DependencyProperty IsTextUpProperty = DependencyProperty.Register(
            "IsTextUp", typeof(bool), typeof(ArrowLineWithText),
            new FrameworkPropertyMetadata(true, FrameworkPropertyMetadataOptions.AffectsRender));
        /// <summary>
        /// 文本是否朝上
        /// </summary>
        public bool IsTextUp
        {
            get { return (bool)GetValue(IsTextUpProperty); }
            set { SetValue(IsTextUpProperty, value); }
        }

        /// <summary>
        /// 是否显示文本依赖属性
        /// </summary>
        public static readonly DependencyProperty IsShowTextProperty = DependencyProperty.Register(
            "IsShowText", typeof(bool), typeof(ArrowLineWithText),
            new FrameworkPropertyMetadata(true, FrameworkPropertyMetadataOptions.AffectsRender));
        /// <summary>
        /// 是否显示文本
        /// </summary>
        public bool IsShowText
        {
            get { return (bool)GetValue(IsShowTextProperty); }
            set { SetValue(IsShowTextProperty, value); }
        }
        #endregion 基本属性

        #region 绘制方法
        /// <summary>
        /// 重载渲染事件
        /// </summary>
        /// <param name="drawingContext">绘图上下文</param>
        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);
            if(IsShowText && (Text != null))
            {
                var txt = Text.Trim();
                var startPoint = StartPoint;
                if(!string.IsNullOrEmpty(txt))
                {
                    var vec = EndPoint - startPoint;
                    var angle = GetAngle(StartPoint, EndPoint);
              
                    //使用旋转变换，使其与线平等
                    var transform = new RotateTransform(angle) { CenterX = startPoint.X, CenterY = startPoint.Y };
                    drawingContext.PushTransform(transform);

                    var defaultTypeface = new Typeface(SystemFonts.StatusFontFamily, SystemFonts.StatusFontStyle,
                        SystemFonts.StatusFontWeight, new FontStretch());
                    FormattedText formattedText = new FormattedText(txt, CultureInfo.CurrentCulture,
                        FlowDirection.LeftToRight,
                        defaultTypeface, SystemFonts.StatusFontSize, Brushes.Black)
                    {
                        MaxTextWidth = vec.Length,
                        TextAlignment = TextAlignment
                    };

                    var offsetY = StrokeThickness;
                    if (IsTextUp)
                    {
                        double textLineCount = formattedText.Width / formattedText.MaxTextWidth;
                        if (textLineCount < 1)
                        {
                            textLineCount = 1;
                        }
                        offsetY = -formattedText.Height * textLineCount - StrokeThickness;
                    }
                    startPoint = startPoint + new Vector(0, offsetY);
                    drawingContext.DrawText(formattedText, startPoint);
                    drawingContext.Pop();
                }
            }
        }


        /// <summary>
        /// 获取两个点的倾角
        /// </summary>
        /// <param name="start">起点</param>
        /// <param name="end">终点</param>
        /// <returns>角度值</returns>
        private double GetAngle(Point start, Point end)
        {
            var vec = end - start;
            var xAxis = new Vector(1, 0);
            return Vector.AngleBetween(xAxis, vec);
        }
        #endregion
    }
}
