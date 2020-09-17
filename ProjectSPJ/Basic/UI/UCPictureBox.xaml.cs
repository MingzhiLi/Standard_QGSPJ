using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Drawing;
using System.IO;

namespace ProjectSPJ
{
    /// <summary>
    /// Interaction logic for UCPictureBox.xaml
    /// </summary>
    public partial class UCPictureBox : UserControl
    {
        public UCPictureBox()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 加载的源图像
        /// </summary>
        public Bitmap SourceImage = null;

        /// <summary>
        /// 图像宽
        /// </summary>
        public int ImgWidth
        {
            get
            {
                return (int)SourceImage?.Width;
            }
        }

        /// <summary>
        /// 图像高
        /// </summary>
        public int ImgHeight
        {
            get
            {
                return (int)SourceImage?.Height;
            }
        }

        /// <summary>
        /// 窗口宽度
        /// </summary>
        public int WinWidth
        {
            get
            {
                return pictureBox.Width;
            }
            set
            {
                pictureBox.Width = value;
            }
        }

        /// <summary>
        /// 窗口高度
        /// </summary>
        public int WinHeight
        {
            get
            {
                return pictureBox.Height;
            }
            set
            {
                pictureBox.Height = value;
            }
        }

        /// <summary>
        /// 在控件中绘制图片
        /// </summary>
        public void DrawImgCanves()
        {
            try
            {
                Graphics graphics = pictureBox.CreateGraphics();
                System.Drawing.Size sizeImage = new System.Drawing.Size(ImgWidth, ImgHeight);
                System.Drawing.Point pointLT = new System.Drawing.Point();

                //图片中要绘制的区域
                System.Drawing.Rectangle recRegionImage = new System.Drawing.Rectangle(pointLT, sizeImage);

                //绘制的窗口区域
                System.Drawing.Rectangle recRegionWin = new System.Drawing.Rectangle(new Point(0, 0), new Size(WinWidth, WinHeight));
                //graphics.Clear(this.pictureBox1.BackColor);
                graphics.DrawImage(SourceImage, recRegionWin, recRegionImage, GraphicsUnit.Pixel);
                //graphics.DrawImage(SourceImage, 0, 0);
                graphics.Dispose();
            }
            catch (Exception ex)
            {
                
            }
        }

        /// <summary>
        /// 加载图像
        /// </summary>
        /// <param name="image">加载的图像源</param>
        public void LoadImage(string fileName)
        {
            try
            {
                fileName += ".jpg";
                if(File.Exists(fileName))
                {
                    SourceImage?.Dispose();
                    SourceImage = new Bitmap(System.Drawing.Image.FromFile(fileName));
                    DrawImgCanves();
                }
            }
            catch
            {
            }
        }
    }
}
