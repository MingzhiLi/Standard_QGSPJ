using System;
using System.Collections.Generic;
using System.Text;
using BasicClass;
using System.Windows;
using System.Reflection;
using System.IO;

namespace ProjectSPJ
{
    /// <summary>
    /// 随型号改变的参数
    /// </summary>
    public sealed class ParModelValue : ProdParBase
    {
        /// <summary>
        /// 静态类实例
        /// </summary>
        public static ParModelValue Inst = new ParModelValue();

        /// <summary>
        /// 读取本地配置文件
        /// </summary>
        public static void ReadLocalFile()
        {
            Inst = Inst.ReadProdFile<ParModelValue>();
        }
        /// <summary>
        /// 文件保存路径名称，如果不重写则会以基类名称来保存
        /// </summary>
        protected override string NameFile => MethodBase.GetCurrentMethod().DeclaringType.Name;

        #region 位置补偿及参数

        #region 皮带线距离补偿
        /// <summary>
        /// 皮带线X方向距离补偿
        /// </summary>
        public double BeltComX { get; set; }

        /// <summary>
        /// 皮带线Y方向距离补偿
        /// </summary>
        public double BeltComY { get; set; }

        /// <summary>
        /// 皮带线X方向运行距离
        /// </summary>
        public double BeltDisX
        {
            get => BeltComX + ProductSet.Inst.GlassX;
        }
        /// <summary>
        /// 皮带线Y方向运行距离
        /// </summary>
        public double BeltDisY
        {
            get => BeltComY + ProductSet.Inst.GlassY;
        }
        /// <summary>
        /// 皮带线运行时间
        /// </summary>
        public  double BeltTimeY
        {
            get
            {
                return BeltDisY / GlobalThreshold.Inst.BeltSpeed;
            }
        }

        /// <summary>
        /// 皮带线运行时间
        /// </summary>
        public double BeltTimeX
        {
            get
            {
                return BeltDisX / GlobalThreshold.Inst.BeltSpeed;
            }
        }
        #endregion 皮带线距离补偿

        #region 粗定位补偿值
        /// <summary>
        /// 粗定位取片位置补偿X
        /// </summary>
        public double ComPickPosX { get; set; }
        /// <summary>
        /// 粗定位取片位置补偿X
        /// </summary>
        public double ComPickPosY { get; set; }
        /// <summary>
        /// 粗定位取片位置补偿Z
        /// </summary>
        public double ComPickPosZ { get; set; }
        /// <summary>
        /// 粗定位取片位置补偿R
        /// </summary>
        public double ComPickPosR { get; set; }
        #endregion 粗定位补偿值

        #region 粗定位取片后位移
        /// <summary>
        /// 粗定位取片后位移X
        /// </summary>
        public double MoveDisX { get; set; }
        /// <summary>
        /// 粗定位取片后位移Y
        /// </summary>
        public double MoveDisY { get; set; }
        /// <summary>
        /// 粗定位取片后位移Z
        /// </summary>
        public double MoveDisZ { get; set; }
        /// <summary>
        /// 粗定位取片后位移R
        /// </summary>
        public double MoveDisR { get; set; }

        public static Point4D MoveDis
        {
            get
            {
                return new Point4D(Inst.MoveDisX, Inst.MoveDisY, Inst.MoveDisZ, Inst.MoveDisR);
            }
        }
        #endregion 粗定位取片后位移

        #region 精定位补偿（补正到最终的放片位置）
        /// <summary>
        /// 残次平台放片位置补偿X
        /// </summary>
        public double ComPutPosX { get; set; }
        /// <summary>
        /// 残次平台放片位置补偿X
        /// </summary>
        public double ComPutPosY { get; set; }
        /// <summary>
        /// 残次平台放片位置补偿Z
        /// </summary>
        public double ComPutPosZ { get; set; }
        /// <summary>
        /// 残次平台放片位置补偿R
        /// </summary>
        public double ComPutPosR { get; set; }

        public static Point4D ComPutPos
        {
            get
            {
                return new Point4D(Inst.ComPutPosX, Inst.ComPutPosY, Inst.ComPutPosZ, Inst.ComPutPosR);
            }
        }
        /// <summary>
        /// 残材平台A面高度补偿
        /// </summary>
        public double ComPutPosZSideA { get; set; }
        /// <summary>
        /// 残材平台B面高度补偿
        /// </summary>
        public double ComPutPosZSideB { get; set; }
        #endregion 精定位补偿

        #region 残材平台高度补偿
        /// <summary>
        /// A面高度补偿
        /// </summary>
        public double ComPlatZ1 { get; set; }
        /// <summary>
        /// B面高度补偿
        /// </summary>
        public double ComPlatZ2 { get; set; }
        #endregion 残材平台高度补偿

        #region 精定位拍照位
        /// <summary>
        /// 精定位是否左右移动拍照
        /// </summary>
        public static bool IsPreciseMovePhoto
        {
            get
            {
                return FunctionSelect.Inst.IsPreciseMovePhoto &&        ///左右移动拍照功能启用
                     ((ProductSet.Inst.GlassX > GlobalThreshold.Inst.ThdPreciseMovePhoto)  ///产品X大于移动拍照的阈值
                     ||  (ProductSet.Inst.GlassY > GlobalThreshold.Inst.ThdPreciseMovePhoto));  ///产品Y大于左右拍照的阈值

            }
        }

        /// <summary>
        /// 精定位拍照位置1，这里暂时只考虑了20.3的机器人方向，后面需要将轴方向代入计算
        /// </summary>
        public static Point4D PrecisePhotoPos1
        {
            get
            {
                if (IsPreciseMovePhoto && ProductSet.Inst.GlassX > ProductSet.Inst.GlassY)    
                {
                    return RobotPar.PrecisePhotoPos + 
                        new Point4D(0, ProductSet.Inst.GlassX * GlobalThreshold.Inst.PreciseMoveRatio, 0, 0);   ///后面需要将轴方向写到里面
                }
                else if(IsPreciseMovePhoto && !(ProductSet.Inst.GlassX > ProductSet.Inst.GlassY))
                {
                    return RobotPar.PrecisePhotoPos + 
                        new Point4D(0, ProductSet.Inst.GlassY * GlobalThreshold.Inst.PreciseMoveRatio, 0, 0);
                }
                else
                {
                    return RobotPar.PrecisePhotoPos;
                }
            }
        }
        /// <summary>
        /// 精定位拍照位置2，后面需要将轴方向代入计算
        /// </summary>
        public static Point4D PrecisePhotoPos2
        {
            get
            {
                if (ProductSet.Inst.GlassX > ProductSet.Inst.GlassY &&
                    ProductSet.Inst.GlassX > GlobalThreshold.Inst.ThdPreciseMovePhoto)    
                {
                    return RobotPar.PrecisePhotoPos + 
                        new Point4D(0, -ProductSet.Inst.GlassX * GlobalThreshold.Inst.PreciseMoveRatio, 0, 0);    
                }
                else if (!(ProductSet.Inst.GlassX > ProductSet.Inst.GlassY) && 
                    ProductSet.Inst.GlassY > GlobalThreshold.Inst.ThdPreciseMovePhoto)
                {
                    return RobotPar.PrecisePhotoPos + 
                        new Point4D(0, -ProductSet.Inst.GlassY * GlobalThreshold.Inst.PreciseMoveRatio, 0, 0);
                }
                else
                {
                    return RobotPar.PrecisePhotoPos;
                }
            }
        }
        #endregion 精定位拍照位


        #region 二维码位置补偿
        /// <summary>
        /// 二维码位置补偿X
        /// </summary>
        public double ComQrCodePosX { get; set; }
        /// <summary>
        /// 二维码位置补偿Y
        /// </summary>
        public double ComQrCodePosY { get; set; }
        /// <summary>
        /// 二维码位置补偿R
        /// </summary>
        public double ComQrCodePosR { get; set; }
        #endregion 二维码位置补偿

        #region 巡边位置补偿
        /// <summary>
        /// 巡边位置补偿X
        /// </summary>
        public double ComInspPosX { get; set; }
        /// <summary>
        /// 巡边位置补偿Y
        /// </summary>
        public double ComInspPosY { get; set; }
        /// <summary>
        /// 巡边位置补偿R
        /// </summary>
        public double ComInspPosR { get; set; }
        #endregion 巡边位置补偿

        #region 插栏位置补偿
        /// <summary>
        /// 插栏位置补偿-水平方向
        /// </summary>
        public double ComInsertPosHor { get; set; }

        /// <summary>
        /// 插栏位置补偿-垂直方向
        /// </summary>
        public double ComInsertPosVer { get; set; }
        #endregion 插栏位置补偿

        #region 阈值设置
        /// <summary>
        /// 精定位面积阈值min
        /// </summary>
        public double ThdAreaMin { get; set; }
        /// <summary>
        /// 精定位面积阈值max
        /// </summary>
        public double ThdAreaMax { get; set; }


        /// <summary>
        /// 残材1阈值
        /// </summary>
        public double ThdResidue1 { get; set; }
        /// <summary>
        /// 残材2阈值
        /// </summary>
        public double ThdResidue2 { get; set; }
        /// <summary>
        /// 残材3阈值
        /// </summary>
        public double ThdResidue3 { get; set; }

        /// <summary>
        /// M直线夹角标准角度
        /// </summary>
        public double StdMLineAngle { get; set; }
        /// <summary>
        /// M直线角度偏差范围
        /// </summary>
        public double ThdMLineDev { get; set; }

        /// <summary>
        /// 产品长度偏差阈值
        /// </summary>
        public double ThdLengthDev { get; set; }
        /// <summary>
        /// 产品宽度偏差阈值
        /// </summary>
        public double ThdWidthDev { get; set; }

        /// <summary>
        /// 精定位是否移动拍照，用于控件绑定
        /// </summary>
        public bool IsMovePhoto => IsPreciseMovePhoto;
        #endregion 阈值设置

        #endregion

        #region 放片位置相关
        /// <summary>
        /// 精定位处机器人u轴角度
        /// </summary>
        public static double PreciseRobotAngle
        {
            get
            {
                return RobotPar.Inst.PreciseRobotAngle;
            }
        }


        /// <summary>
        /// 残材平台处机器人u轴角度
        /// </summary>
        public static double WastageRobotAngle
        {
            get
            {
                double angle = (WastageAngle + PickAngle + 360) % 360;
                return angle > 180 ? 180 - angle : angle;
            }
        }

        /// <summary>
        /// 粗定位取片角度
        /// </summary>
        public static double PickAngle
        {
            get
            {
                return ProductSet.Inst.GlassX > ProductSet.Inst.GlassY ? 0 : -90;
            }
        }
        /// <summary>
        /// 电极边数组
        /// </summary>
        private static double[] _electrodeArray
        {
            get
            {
                return new double[] { ProductSet.Inst.TopE,
                                      ProductSet.Inst.LeftE,
                                      ProductSet.Inst.BottomE,
                                      ProductSet.Inst.RightE };
            }
        }


        /// <summary>
        /// 残材平台上玻璃的放片位置，分为左上，左下，右下，右上-0123
        /// </summary>        
        public static PlatPlacePosEnum PlacePos
        {
            get
            {
                PlatPlacePosEnum placePos = PlatConfig.Inst.PlatPlacePosEnum;
                ///这里的意思是当平台翻转旋转轴在对角线时，
                if (ConfWastagePlatStation == 2)
                {
                    placePos = (PlatPlacePosEnum)(((int)placePos + 2) % 4);
                }

                return placePos;
            }
        }
        /// <summary>
        /// 平台放片电极朝向，上左下右-0123，单电极时
        /// </summary>
        public static int[] PlatPlaceDir
        {
            get
            {
                //这边的做法是，把单电极朝向的方向，放在数组[0]，另一个放在数组[1]，这是为了单电极计算的时候直接计算第一位
                //假设是左上或者右下，左上朝向是（1，0），右下是（3，2）
                //假设是左下和右上，左下朝向是（1，2），右上是（3，0） 
                if (PlatConfig.Inst.IsHorizontalPut)
                    return new int[2] {
                        (int)PlacePos % 2 == 0 ? ((int)PlacePos + 1) % 4 : (int)PlacePos,
                        (int)PlacePos % 2 == 0 ? (int)PlacePos : ((int)PlacePos + 1) % 4 };
                //如果是竖直方向，那与上面相反
                //左上（0，1），右下（2，3）
                //左下（2，1），右上（3，2）
                else
                    return new int[2] {
                        (int)PlacePos % 2 == 0 ? (int)PlacePos : ((int)PlacePos + 1) % 4,
                        (int)PlacePos % 2 == 0 ? ((int)PlacePos + 1) % 4 : (int)PlacePos };
            }
        }

         
        /// <summary>
        /// 是否单电极 
        /// </summary>
        public static bool IfSingleElectrode
        {
            get
            {
                int cnt = 0;
                if (ProductSet.Inst.TopE != 0)
                    cnt++;
                if (ProductSet.Inst.BottomE != 0)
                    cnt++;
                if (ProductSet.Inst.LeftE != 0)
                    cnt++;
                if (ProductSet.Inst.RightE != 0)
                    cnt++;

                return cnt == 1;
            }
        }

        /// <summary>
        /// 配方-残材平台放片工位号
        /// </summary>
        public static double ConfWastagePlatStation
        {
            get
            {
                return 1;// ParConfigPar.P_I[(int)RecipeRegister.PlatStaionNo].DblValue;
            }
        }

        /// <summary>
        /// 玻璃在残材平台的角度(0123-上左下右)默认来料时角度为0
        /// </summary>
        public static double WastageAngle
        {
            get
            {
                int i = 0;
                foreach (var item in _electrodeArray)
                {
                    if (item > 0)
                        ++i;
                }
                if (i == 0 || i > 2)
                    return 0;

                GetWastageAngle(_electrodeArray, PlatPlaceDir, out double angle, IfSingleElectrode);
                
                if (ConfWastagePlatStation == 1)
                {
                    return angle;
                }
                else
                {
                    if (!IfSingleElectrode)
                        return (angle + 180) % 360;
                    else
                        return (angle - 90) % 360;//针对对角线翻转，如果不是对角线也不会有2工位
                }

            }
        }

        /// <summary>
        /// 残材平台处角度计算
        /// </summary>
        /// <param name="array">配方中电极数据，上左下右-0123</param>
        /// <param name="platDir">放在平台上时，电极需要朝向的位置，上左下右-0123，最多两个朝向</param>
        /// <param name="angle">计算得到的角度</param> 
        /// <param name="isSingleE">是否单电极</param>
        /// <returns></returns>
        public static bool GetWastageAngle(double[] array, int[] platDir, out double angle, bool isSingleE)
        {
            angle = 0;
            try
            {
                if (array.Length != 4)
                    return false;

                //计算方式为，把来料的电极方向和放到平台上时电极需要对应的朝向，抽象成两个数组，
                //固定其中一个数组，另一个数组的值进行位移操作，直到两个数组中有值的索引能够对应，累加得到的角度即为玻璃旋转的角度
                int platDir1 = platDir[0];
                int platDir2 = platDir[1];
                int i = 0;
                if (isSingleE)
                {
                    while (array[platDir1] == 0)
                    {
                        platDir1 = (--platDir1 + 4) % 4;
                        angle += 90;
                        if (++i > 4)
                            return false;
                    }
                }
                else
                {
                    while (array[platDir1] == 0 || array[platDir2] == 0)
                    {
                        platDir1 = (--platDir1 + 4) % 4;
                        platDir2 = (--platDir2 + 4) % 4;
                        angle += 90;
                        if (++i > 4)
                            return false;
                    }
                }
            }
            catch (Exception ex)
            {
                //Log.L_I.WriteError(ClassName, ex);
                return false;
            }
            return true;
        }


        /// <summary>
        /// 残材平台处玻璃X
        /// </summary>
        public static double WastageX
        {
            get
            {
                return WastageAngle % 180 == 0 ? ProductSet.Inst.GlassX : ProductSet.Inst.GlassY;
            }
        }
        /// <summary>
        /// 残材平台处玻璃Y
        /// </summary>
        public static double WastageY
        {
            get
            {
                return WastageAngle % 180 == 0 ? ProductSet.Inst.GlassY : ProductSet.Inst.GlassX;
            }
        }
        /// <summary>
        /// 玻璃在残材平台长宽尺寸的一半，用于补偿在残材基准位置上，计算玻璃放片位
        /// 这里需要增加考虑机器人的方向问题
        /// </summary>
        public static Point ProdSizeHalf
        {
            get
            {
                return new Point((int)PlacePos < 2 ? -WastageX / 2 : WastageX / 2, //如果是left,则向右移动
                    ((int)PlacePos == 1 || (int)PlacePos == 2) ? WastageY / 2 : -WastageY / 2);//如果是bottom，则向上移动
               
            }
        }

        /// <summary>
        /// 机器人残材平台1放片位置,已考虑产品旋转的位置(基准位+产品长宽+放片补偿+产品厚度）
        /// </summary>
        public static Point4D PosWastagePlat1
        {
            get
            {
                double X = 0;
                double Y = 0;
                ///根据轴方向，将产品在残材平台上的XY加进来
                switch(AxisConfigPar.RobotAxisSystem.AxisX)   
                {
                    case AxisDirectionEnum.向上:
                        X = ProdSizeHalf.Y;
                        break;
                    case AxisDirectionEnum.向下:
                        X = -ProdSizeHalf.Y;
                        break;
                    case AxisDirectionEnum.向右:
                        X = ProdSizeHalf.X;
                        break;
                    case AxisDirectionEnum.向左:
                        X = -ProdSizeHalf.X;
                        break;
                }
                switch (AxisConfigPar.RobotAxisSystem.AxisY)
                {
                    case AxisDirectionEnum.向上:
                        Y = ProdSizeHalf.Y;
                        break;
                    case AxisDirectionEnum.向下:
                        Y = -ProdSizeHalf.Y;
                        break;
                    case AxisDirectionEnum.向右:
                        Y = -ProdSizeHalf.X;
                        break;
                    case AxisDirectionEnum.向左:
                        Y = ProdSizeHalf.X;
                        break;
                }

                return RobotPar.PlatPutPos +  ///残材平台的基准位
                    new Point4D(Inst.ComPutPosX, Inst.ComPutPosY, Inst.ComPutPosZ, Inst.ComPutPosR) +   ///残材放片位的补偿
                    new Point4D(X, Y, ProductSet.Inst.Thickness, WastageRobotAngle);    ///产品的长宽，厚度

            }
        }
        /// <summary>
        /// 机器人残材平台2放片位置
        /// </summary>
        public static Point4D PosWastagePlat2
        {
            get
            {
                return RobotPar.Plat2Pos +///残材平台的基准位
                    new Point4D(Inst.ComPutPosX, Inst.ComPutPosY, Inst.ComPutPosZ, Inst.ComPutPosR) +   ///残材放片位的补偿
                    new Point4D(ProdSizeHalf.X, ProdSizeHalf.Y, 0, 0);    ///
            }
        }


        /// <summary>
        /// 残材平台处二维码X
        /// </summary>
        public static double CodeXInWastage
        {
            get
            {
                return GetCurMarkPos(new Point2D(ProductSet.Inst.QrCodeX, ProductSet.Inst.QrCodeY)).DblValue1;
            }
        }
        /// <summary>
        /// 残材平台处二维码Y
        /// </summary>
        public static double CodeYInWastage
        {
            get
            {
                return GetCurMarkPos(new Point2D(ProductSet.Inst.QrCodeX, ProductSet.Inst.QrCodeY)).DblValue2;
            }
        }


        public static double GetCurElectordeWidth(int index)
        {
            return GetCurElectordeWidth(_electrodeArray, index, (int)WastageAngle);
        }

        /// <summary>
        /// 根据配方电极，和残材处玻璃角度，计算残材处电极宽度
        /// </summary>
        /// <param name="array">配方中电极数据，上左下右-0123</param>
        /// <param name="index">所需要获得的，在指定角度情况下的电极索引，上左下右-0123</param>
        /// <param name="r">角度差</param>
        /// <returns></returns>
        public static double GetCurElectordeWidth(double[] array, int index, int r)
        {
            return array[(index - r / 90 + 4) % 4];
        }

        #region 没时间改，直接复制的，后面改掉
        public static Point2D GetCurMarkPos(Point2D markpos)
        {
            return GetCurMarkPos(markpos, ProductSet.Inst.GlassX, ProductSet.Inst.GlassY, WastageAngle);
        }
        public static Point2D GetCurMarkPos(Point2D markpos, double width, double height, double r)
        {
            Point2D delta = new Point2D(markpos.DblValue1 - width / 2, markpos.DblValue2 - height / 2);
            Point2D size = new Point2D(r % 180 == 0 ? width : height, r % 180 == 0 ? height : width);

            delta = TransDelta(delta, r, 0);
            return new Point2D(delta.DblValue1 + size.DblValue1 / 2, delta.DblValue2 + size.DblValue2 / 2);
        }

        public static Point2D TransDelta(Point2D oriPt, double dstAngle, double startAngle)
        {
            return MultMatrix(oriPt, dstAngle - startAngle);
        }

        static Point2D MultMatrix(Point2D pt, double angle)
        {
            double radius = angle / 180 * Math.PI;
            double x = pt.DblValue1 * Math.Cos(radius) - pt.DblValue2 * Math.Sin(radius);
            double y = pt.DblValue1 * Math.Sin(radius) + pt.DblValue2 * Math.Cos(radius);
            return new Point2D(x, y);
        }
        #endregion



        #endregion 放片位置相关

        #region 单目相机相关
        /// <summary>
        /// 单目相机旋转中心
        /// </summary>
        public Point2D MonoRotCenter
        {
            get
            {
                double CenterX = WastageX / 2;
                double CenterY = WastageY / 2;
                if(AxisConfigPar.MonoAxisSystem.MoveDireciton == MoveDirecitonEnum.从右向左)
                {
                    return new Point2D(-CenterX, CenterY);
                }
                else
                {
                    return new Point2D(CenterX, CenterY);
                }
            }
        }
        #endregion 单目相机相关

        /// <summary>
        /// 换型时调用的函数
        /// </summary>
        public void ChangeModel()
        {
            string strFileName = this.NameFile + ".xml";
            string filePath = ProdBaseSavePath + strFileName;
            if (File.Exists(filePath))
            {
                Inst = ReadProdFile<ParModelValue>();
            }
            else
            {
                Inst.SaveProductParToLocal();
            }
        }
    }
}
