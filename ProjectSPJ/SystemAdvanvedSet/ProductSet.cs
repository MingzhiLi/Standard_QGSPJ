using System;
using System.Collections.Generic;
using System.Text;
using DealConfigFile;
using System.Reflection;
namespace ProjectSPJ
{
    /// <summary>
    /// 产品参数的寄存器设置
    /// </summary>
    public sealed class ProductSet : BaseSerializer
    {
        /// <summary>
        /// 静态类实例
        /// </summary>
        public static ProductSet Inst = new ProductSet();

        /// <summary>
        /// 文件保存路径名称，如果不重写则会以基类名称来保存
        /// </summary>
        protected override string NameFile => MethodBase.GetCurrentMethod().DeclaringType.Name;

        /// <summary>
        /// 读取本地配置文件
        /// </summary>
        public static void ReadLocalFile()
        {
            Inst = Inst.ReadLocalFile<ProductSet>();
        }
        /// <summary>
        /// 产品参数寄存器数量，暂时定义40个
        /// </summary>
        private const int NUMREG = 40;


        #region 产品参数索引
        int indexGlassX;
        /// <summary>
        /// 玻璃X
        /// </summary>
        public int IndexGlassX
        {
            get
            {
                return indexGlassX;
            }
            set
            {
                indexGlassX = value;
                this.NotifyPropertyChanged(() => this.AnnounceGlassX);
                this.NotifyPropertyChanged(() => this.GlassX);
            }
        }

        int indexGlassY;
        /// <summary>
        /// 玻璃Y
        /// </summary>
        public int IndexGlassY
        {
            get
            {
                return indexGlassY;
            }
            set
            {
                indexGlassY = value;
                this.NotifyPropertyChanged(() => this.AnnounceGlassY);
                this.NotifyPropertyChanged(() => this.GlassY);
            }
        }

        int indexThickness;
        /// <summary>
        /// 玻璃厚度
        /// </summary>
        public int IndexThickness
        {
            get
            {
                return indexThickness;
            }
            set
            {
                indexThickness = value;
                this.NotifyPropertyChanged(() => this.AnnounceThickness);
                this.NotifyPropertyChanged(() => this.Thickness);
            }
        }

        int indexQrCodeX;
        /// <summary> 
        /// 二维码X   
        /// </summary>
        public int IndexQrCodeX
        {
            get
            {
                return indexQrCodeX;
            }
            set
            {
                indexQrCodeX = value;
                this.NotifyPropertyChanged(() => this.AnnounceQrCodeX);
                this.NotifyPropertyChanged(() => this.QrCodeX);
            }
        }

        int indexQrCodeY;
        /// <summary>
        /// 二维码Y
        /// </summary>
        public int IndexQrCodeY
        {
            get
            {
                return indexQrCodeY;
            }
            set
            {
                indexQrCodeY = value;
                this.NotifyPropertyChanged(() => this.AnnounceQrCodeY);
                this.NotifyPropertyChanged(() => this.QrCodeY);
            }
        }
        int indexMark1X;
        /// <summary>
        /// Mark1X
        /// </summary>
        public int IndexMark1X
        {
            get
            {
                return indexMark1X;
            }
            set
            {
                indexMark1X = value;
                this.NotifyPropertyChanged(() => this.AnnounceMark1X);
                this.NotifyPropertyChanged(() => this.Mark1X);
            }
        }

        int indexMark1Y;
        /// <summary>
        /// Mark1Y
        /// </summary>
        public int IndexMark1Y
        {
            get
            {
                return indexMark1Y;
            }
            set
            {
                indexMark1Y = value;
                this.NotifyPropertyChanged(() => this.AnnounceMark1Y);
                this.NotifyPropertyChanged(() => this.Mark1Y);
            }
        }
        int indexMark2X;
        /// <summary>
        /// Mark2X
        /// </summary>
        public int IndexMark2X
        {
            get
            {
                return indexMark2X;
            }
            set
            {
                indexMark2X = value;
                this.NotifyPropertyChanged(() => this.AnnounceMark2X);
                this.NotifyPropertyChanged(() => this.Mark2X);
            }
        }

        int indexMark2Y;
        /// <summary>
        /// Mark2Y
        /// </summary>
        public int IndexMark2Y
        {
            get
            {
                return indexMark2Y;
            }
            set
            {
                indexMark2Y = value;
                this.NotifyPropertyChanged(() => this.AnnounceMark2Y);
                this.NotifyPropertyChanged(() => this.Mark2Y);
            }
        }

        int indexPickStationNum;
        /// <summary>
        /// 中片平台工位数
        /// </summary>
        public int IndexPickStationNum
        {
            get
            {
                return indexPickStationNum;
            }
            set
            {
                indexPickStationNum = value;
                this.NotifyPropertyChanged(() => this.AnnouncePickStationNum);
                this.NotifyPropertyChanged(() => this.PickStationNum);
            }
        }

        int indexTopE;
        /// <summary>
        /// 上电极宽度
        /// </summary>
        public int IndexTopE
        {
            get
            {
                return indexTopE;
            }
            set
            {
                indexTopE = value;
                this.NotifyPropertyChanged(() => this.AnnounceTopE);
                this.NotifyPropertyChanged(() => this.TopE);
            }
        }

        int indexBottomE;
        /// <summary>
        /// 下电极宽度
        /// </summary>
        public int IndexBottomE
        {
            get
            {
                return indexBottomE;
            }
            set
            {
                indexBottomE = value;
                this.NotifyPropertyChanged(() => this.AnnounceBottomE);
                this.NotifyPropertyChanged(() => this.BottomE);
            }
        }

        int indexLeftE;
        /// <summary>
        /// 左电极宽度
        /// </summary>
        public int IndexLeftE
        {
            get
            {
                return indexLeftE;
            }
            set
            {
                indexLeftE = value;
                this.NotifyPropertyChanged(() => this.AnnounceLeftE);
                this.NotifyPropertyChanged(() => this.LeftE);
            }
        }
        int indexRightE;
        /// <summary>
        /// 右电极宽度
        /// </summary>
        public int IndexRightE
        {
            get
            {
                return indexRightE;
            }
            set
            {
                indexRightE = value;
                this.NotifyPropertyChanged(() => this.AnnounceRightE);
                this.NotifyPropertyChanged(() => this.RightE);
            }
        }
        int indexCSTRows;
        /// <summary>
        /// 卡塞行数
        /// </summary>
        public int IndexCSTRows
        {
            get
            {
                return indexCSTRows;
            }
            set
            {
                indexCSTRows = value;
                this.NotifyPropertyChanged(() => this.AnnounceCSTRows);
                this.NotifyPropertyChanged(() => this.CSTRows);
            }
        }

        int indexCSTCols;
        /// <summary>
        /// 卡塞列数
        /// </summary>
        public int IndexCSTCols
        {
            get
            {
                return indexCSTCols;
            }
            set
            {
                indexCSTCols = value;
                this.NotifyPropertyChanged(() => this.AnnounceCSTCols);
                this.NotifyPropertyChanged(() => this.CSTCols);
            }
        }
        int indexKeelInterval;
        /// <summary>
        /// 龙骨间距
        /// </summary>
        public int IndexKeelInterval
        {
            get
            {
                return indexKeelInterval;
            }
            set
            {
                indexKeelInterval = value;
                this.NotifyPropertyChanged(() => this.AnnounceKeelInterval);
                this.NotifyPropertyChanged(() => this.KeelInterval);
            }
        }
        int indexKeelSpacing;
        /// <summary>
        /// 龙骨层间距
        /// </summary>
        public int IndexKeelSpacing
        {
            get
            {
                return indexKeelSpacing;
            }
            set
            {
                indexKeelSpacing = value;
                this.NotifyPropertyChanged(() => this.AnnounceKeelSpacing);
                this.NotifyPropertyChanged(() => this.KeelSpacing);
            }
        }
        int indexDisCol1;
        /// <summary>
        /// 第一列龙骨间距
        /// </summary>
        public int IndexDisCol1
        {
            get
            {
                return indexDisCol1;
            }
            set
            {
                indexDisCol1 = value;
                this.NotifyPropertyChanged(() => this.AnnounceDisCol1);
                this.NotifyPropertyChanged(() => this.DisCol1);
            }
        }
        int indexDirecInsert;
        /// <summary>
        /// 插栏方向
        /// </summary>
        public int IndexDirecInsert
        {
            get
            {
                return indexDirecInsert;
            }
            set
            {
                indexDirecInsert = value;
                this.NotifyPropertyChanged(() => this.AnnounceDirecInsert);
                this.NotifyPropertyChanged(() => this.DirecInsert);
            }
        }
        int indexDirecDump;
        /// <summary>
        /// 抛料方向
        /// </summary>
        public int IndexDirecDump
        {
            get
            {
                return indexDirecDump;
            }
            set
            {
                indexDirecDump = value;
                this.NotifyPropertyChanged(() => this.AnnounceDirecDump);
                this.NotifyPropertyChanged(() => this.DirecDump);
            }
        }
        int indexSumPick;
        /// <summary>
        /// 中片玻璃总数
        /// </summary>
        public int IndexSumPick
        {
            get
            {
                return indexSumPick;
            }
            set
            {
                indexSumPick = value;
                this.NotifyPropertyChanged(() => this.AnnounceSumPick);
                this.NotifyPropertyChanged(() => this.SumPick);
            }
        }
        int indexInsertDepth;
        /// <summary>
        /// 插栏深度
        /// </summary>
        public int IndexInsertDepth
        {
            get
            {
                return indexInsertDepth;
            }
            set
            {
                indexInsertDepth = value;
                this.NotifyPropertyChanged(() => this.AnnounceInsertDepth);
                this.NotifyPropertyChanged(() => this.InsertDepth);
            }
        }
        int indexCSTHeight;
        /// <summary>
        /// 卡塞高度
        /// </summary>
        public int IndexCSTHeight
        {
            get
            {
                return indexCSTHeight;
            }
            set
            {
                indexCSTHeight = value;
                this.NotifyPropertyChanged(() => this.AnnounceCSTHeight);
                this.NotifyPropertyChanged(() => this.CSTHeight);
            }
        }
        int indexCSTSize;
        /// <summary>
        /// 大小卡塞
        /// </summary>
        public int IndexCSTSize
        {
            get
            {
                return indexCSTSize;
            }
            set
            {
                indexCSTSize = value;
                this.NotifyPropertyChanged(() => this.AnnounceCSTSize);
                this.NotifyPropertyChanged(() => this.CSTSize);
            }
        }
        int indexKeelDepth;
        /// <summary>
        /// 龙骨深度
        /// </summary>
        public int IndexKeelDepth
        {
            get
            {
                return indexKeelDepth;
            }
            set
            {
                indexKeelDepth = value;
                this.NotifyPropertyChanged(() => this.AnnounceKeelDepth);
                this.NotifyPropertyChanged(() => this.KeelDepth);
            }
        }
        int indexStartRow;
        /// <summary>
        /// 插栏起始行
        /// </summary>
        public int IndexStartRow
        {
            get
            {
                return indexStartRow;
            }
            set
            {
                indexStartRow = value;
                this.NotifyPropertyChanged(() => this.AnnounceStartRow);
                this.NotifyPropertyChanged(() => this.StartRow);
            }
        }
        int indexStartCol;
        /// <summary>
        /// 插栏起始列
        /// </summary>
        public int IndexStartCol
        {
            get
            {
                return indexStartCol;
            }
            set
            {
                indexStartCol = value;
                this.NotifyPropertyChanged(() => this.AnnounceStartCol);
                this.NotifyPropertyChanged(() => this.StartCol);
            }
        }

        int indexRowInterval;
        /// <summary>
        /// 隔几行插栏
        /// </summary>
        public int IndexRowInterval
        {
            get
            {
                return indexRowInterval;
            }
            set
            {
                indexRowInterval = value;
                this.NotifyPropertyChanged(() => this.AnnounceRowInterval);
                this.NotifyPropertyChanged(() => this.RowInterval);
            }
        }
        int indexPlatStationNo;
        /// <summary>
        /// 残材平台工位号
        /// </summary>
        public int IndexPlatStationNo
        {
            get
            {
                return indexPlatStationNo;
            }
            set
            {
                indexPlatStationNo = value;
                this.NotifyPropertyChanged(() => this.AnnouncePlatStationNo);
                this.NotifyPropertyChanged(() => this.PlatStationNo);
            }
        }
        int indexReserve1;
        /// <summary>
        /// 预留1
        /// </summary>
        public int IndexReserve1
        {
            get
            {
                return indexReserve1;
            }
            set
            {
                indexReserve1 = value;
                this.NotifyPropertyChanged(() => this.AnnounceReserve1);
                this.NotifyPropertyChanged(() => this.Reserve1);
            }
        }


        int indexReserve2;
        /// <summary>
        /// 预留2
        /// </summary>
        public int IndexReserve2
        {
            get
            {
                return indexReserve2;
            }
            set
            {
                indexReserve2 = value;
                this.NotifyPropertyChanged(() => this.AnnounceReserve2);
                this.NotifyPropertyChanged(() => this.Reserve2);
            }
        }
        int indexReserve3;
        public int IndexReserve3
        {
            get
            {
                return indexReserve3;
            }
            set
            {
                indexReserve3 = value;
                this.NotifyPropertyChanged(() => this.AnnounceReserve3);
                this.NotifyPropertyChanged(() => this.Reserve3);
            }
        }
        int indexReserve4;
        public int IndexReserve4
        {
            get
            {
                return indexReserve4;
            }
            set
            {
                indexReserve4 = value;
                this.NotifyPropertyChanged(() => this.AnnounceReserve4);
                this.NotifyPropertyChanged(() => this.Reserve4);
            }
        }
        int indexReserve5;
        public int IndexReserve5
        {
            get
            {
                return indexReserve5;
            }
            set
            {
                indexReserve5 = value;
                this.NotifyPropertyChanged(() => this.AnnounceReserve5);
                this.NotifyPropertyChanged(() => this.Reserve5);
            }
        }
        int indexReserve6;
        public int IndexReserve6
        {
            get
            {
                return indexReserve6;
            }
            set
            {
                indexReserve6 = value;
                this.NotifyPropertyChanged(() => this.AnnounceReserve6);
                this.NotifyPropertyChanged(() => this.Reserve6);
            }
        }
        #endregion 产品参数索引 

        #region 产品参数值
        /// <summary>
        /// 玻璃X
        /// </summary>
        public double GlassX
        {
            get
            {
                return ParConfigPar.P_I.ParProduct_L[IndexGlassX].DblValue;
            }
        }
        /// <summary>
        /// 玻璃Y
        /// </summary>
        public double GlassY
        {
            get
            {
                return ParConfigPar.P_I.ParProduct_L[IndexGlassY].DblValue;
            }
        }
        /// <summary>
        /// 玻璃厚度
        /// </summary>
        public double Thickness
        {
            get
            {
                return ParConfigPar.P_I.ParProduct_L[IndexThickness].DblValue;
            }
        }
        /// <summary> 
        /// 二维码X   
        /// </summary>
        public double QrCodeX
        {
            get
            {
                return ParConfigPar.P_I.ParProduct_L[IndexQrCodeX].DblValue;
            }
        }
        /// <summary>
        /// 二维码Y
        /// </summary>
        public double QrCodeY
        {
            get
            {
                return ParConfigPar.P_I.ParProduct_L[IndexQrCodeY].DblValue;
            }
        }
        /// <summary>
        /// Mark1X
        /// </summary>
        public double Mark1X
        {
            get
            {
                return ParConfigPar.P_I.ParProduct_L[IndexMark1X].DblValue;
            }
        }
        /// <summary>
        /// Mark1Y
        /// </summary>
        public double Mark1Y
        {
            get
            {
                return ParConfigPar.P_I.ParProduct_L[IndexMark1Y].DblValue;
            }
        }
        /// <summary>
        /// Mark2X
        /// </summary>
        public double Mark2X
        {
            get
            {
                return ParConfigPar.P_I.ParProduct_L[IndexMark2X].DblValue;
            }
        }
        /// <summary>
        /// Mark2Y
        /// </summary>
        public double Mark2Y
        {
            get
            {
                return ParConfigPar.P_I.ParProduct_L[IndexMark2Y].DblValue;
            }
        }

        /// <summary>
        /// 中片平台工位数
        /// </summary>
        public double PickStationNum
        {
            get
            {
                return ParConfigPar.P_I.ParProduct_L[IndexPickStationNum].DblValue;
            }
        }

        /// <summary>
        /// 上电极宽度
        /// </summary>
        public double TopE
        {
            get
            {
                return ParConfigPar.P_I.ParProduct_L[IndexTopE].DblValue;
            }
        }
        /// <summary>
        /// 下电极宽度
        /// </summary>
        public double BottomE
        {
            get
            {
                return ParConfigPar.P_I.ParProduct_L[IndexBottomE].DblValue;
            }
        }
        /// <summary>
        /// 左电极宽度
        /// </summary>
        public double LeftE
        {
            get
            {
                return ParConfigPar.P_I.ParProduct_L[IndexLeftE].DblValue;
            }
        }
        /// <summary>
        /// 右电极宽度
        /// </summary>
        public double RightE
        {
            get
            {
                return ParConfigPar.P_I.ParProduct_L[IndexRightE].DblValue;
            }
        }
        /// <summary>
        /// 卡塞行数
        /// </summary>
        public double CSTRows
        {
            get
            {
                return ParConfigPar.P_I.ParProduct_L[IndexCSTRows].DblValue;
            }
        }
        /// <summary>
        /// 卡塞列数
        /// </summary>
        public double CSTCols
        {
            get
            {
                return ParConfigPar.P_I.ParProduct_L[IndexCSTCols].DblValue;
            }
        }
        /// <summary>
        /// 龙骨间距
        /// </summary>
        public double KeelInterval
        {
            get
            {
                return ParConfigPar.P_I.ParProduct_L[IndexKeelInterval].DblValue;
            }
        }
        /// <summary>
        /// 龙骨层间距
        /// </summary>
        public double KeelSpacing
        {
            get
            {
                return ParConfigPar.P_I.ParProduct_L[IndexKeelSpacing].DblValue;
            }
        }
        /// <summary>
        /// 第一列龙骨间距
        /// </summary>
        public double DisCol1
        {
            get
            {
                return ParConfigPar.P_I.ParProduct_L[IndexDisCol1].DblValue;
            }
        }
        /// <summary>
        /// 插栏方向
        /// </summary>
        public double DirecInsert
        {
            get
            {
                return ParConfigPar.P_I.ParProduct_L[IndexDirecInsert].DblValue;
            }
        }
        /// <summary>
        /// 抛料方向
        /// </summary>
        public double DirecDump
        {
            get
            {
                return ParConfigPar.P_I.ParProduct_L[IndexDirecDump].DblValue;
            }
        }
        /// <summary>
        /// 中片平台玻璃总数
        /// </summary>
        public double SumPick
        {
            get
            {
                return ParConfigPar.P_I.ParProduct_L[IndexSumPick].DblValue;
            }
        }
        /// <summary>
        /// 插栏深度
        /// </summary>
        public double InsertDepth
        {
            get
            {
                return ParConfigPar.P_I.ParProduct_L[IndexInsertDepth].DblValue;
            }
        }
        /// <summary>
        /// 卡塞高度
        /// </summary>
        public double CSTHeight
        {
            get
            {
                return ParConfigPar.P_I.ParProduct_L[IndexCSTHeight].DblValue;
            }
        }
        /// <summary>
        /// 大小卡塞
        /// </summary>
        public double CSTSize
        {
            get
            {
                return ParConfigPar.P_I.ParProduct_L[IndexCSTSize].DblValue;
            }
        }
        /// <summary>
        /// 龙骨深度
        /// </summary>
        public double KeelDepth
        {
            get
            {
                return ParConfigPar.P_I.ParProduct_L[IndexKeelDepth].DblValue;
            }
        }
        /// <summary>
        /// 插栏起始行
        /// </summary>
        public double StartRow
        {
            get
            {
                return ParConfigPar.P_I.ParProduct_L[IndexStartRow].DblValue;
            }
        }
        /// <summary>
        /// 插栏起始列
        /// </summary>
        public double StartCol
        {
            get
            {
                return ParConfigPar.P_I.ParProduct_L[IndexStartCol].DblValue;
            }
        }
        /// <summary>
        /// 隔几行插栏
        /// </summary>
        public double RowInterval
        {
            get
            {
                return ParConfigPar.P_I.ParProduct_L[IndexRowInterval].DblValue;
            }
        }
        /// <summary>
        /// 残材平台工位号
        /// </summary>
        public double PlatStationNo
        {
            get
            {
                return ParConfigPar.P_I.ParProduct_L[IndexPlatStationNo].DblValue;
            }
        }
        public double Reserve1
        {
            get
            {
                return ParConfigPar.P_I.ParProduct_L[IndexReserve1].DblValue;
            }
        }
        public double Reserve2
        {
            get
            {
                return ParConfigPar.P_I.ParProduct_L[IndexReserve2].DblValue;
            }
        }
        public double Reserve3
        {
            get
            {
                return ParConfigPar.P_I.ParProduct_L[IndexReserve3].DblValue;
            }
        }
        public double Reserve4
        {
            get
            {
                return ParConfigPar.P_I.ParProduct_L[IndexReserve4].DblValue;
            }
        }
        public double Reserve5
        {
            get
            {
                return ParConfigPar.P_I.ParProduct_L[IndexReserve5].DblValue;
            }
        }
        public double Reserve6
        {
            get
            {
                return ParConfigPar.P_I.ParProduct_L[IndexReserve6].DblValue;
            }
        }
        #endregion 产品参数索引 

        #region 产品参数注释
        /// <summary>
        /// 玻璃X
        /// </summary>
        public string AnnounceGlassX
        {
            get
            {
                return ParConfigPar.P_I.ParProduct_L[IndexGlassX].Annotation;
            }
        }
        /// <summary>
        /// 玻璃Y
        /// </summary>
        public string AnnounceGlassY
        {
            get
            {
                return ParConfigPar.P_I.ParProduct_L[IndexGlassY].Annotation;
            }
        }
        /// <summary>
        /// 玻璃厚度
        /// </summary>
        public string AnnounceThickness
        {
            get
            {
                return ParConfigPar.P_I.ParProduct_L[IndexThickness].Annotation;
            }
        }
        /// <summary> 
        /// 二维码X   
        /// </summary>
        public string AnnounceQrCodeX
        {
            get
            {
                return ParConfigPar.P_I.ParProduct_L[IndexQrCodeX].Annotation;
            }
        }
        /// <summary>
        /// 二维码Y
        /// </summary>
        public string AnnounceQrCodeY
        {
            get
            {
                return ParConfigPar.P_I.ParProduct_L[IndexQrCodeY].Annotation;
            }
        }
        /// <summary>
        /// Mark1X
        /// </summary>
        public string AnnounceMark1X
        {
            get
            {
                return ParConfigPar.P_I.ParProduct_L[IndexMark1X].Annotation;
            }
        }
        /// <summary>
        /// Mark1Y
        /// </summary>
        public string AnnounceMark1Y
        {
            get
            {
                return ParConfigPar.P_I.ParProduct_L[IndexMark1Y].Annotation;
            }
        }
        /// <summary>
        /// Mark2X
        /// </summary>
        public string AnnounceMark2X
        {
            get
            {
                return ParConfigPar.P_I.ParProduct_L[IndexMark2X].Annotation;
            }
        }
        /// <summary>
        /// Mark2Y
        /// </summary>
        public string AnnounceMark2Y
        {
            get
            {
                return ParConfigPar.P_I.ParProduct_L[IndexMark2Y].Annotation;
            }
        }

        /// <summary>
        /// 中片平台工位数
        /// </summary>
        public string AnnouncePickStationNum
        {
            get
            {
                return ParConfigPar.P_I.ParProduct_L[IndexPickStationNum].Annotation;
            }
        }

        /// <summary>
        /// 上电极宽度
        /// </summary>
        public string AnnounceTopE
        {
            get
            {
                return ParConfigPar.P_I.ParProduct_L[IndexTopE].Annotation;
            }
        }
        /// <summary>
        /// 下电极宽度
        /// </summary>
        public string AnnounceBottomE
        {
            get
            {
                return ParConfigPar.P_I.ParProduct_L[IndexBottomE].Annotation;
            }
        }
        /// <summary>
        /// 左电极宽度
        /// </summary>
        public string AnnounceLeftE
        {
            get
            {
                return ParConfigPar.P_I.ParProduct_L[IndexLeftE].Annotation;
            }
        }
        /// <summary>
        /// 右电极宽度
        /// </summary>
        public string AnnounceRightE
        {
            get
            {
                return ParConfigPar.P_I.ParProduct_L[IndexRightE].Annotation;
            }
        }
        /// <summary>
        /// 卡塞行数
        /// </summary>
        public string AnnounceCSTRows
        {
            get
            {
                return ParConfigPar.P_I.ParProduct_L[IndexCSTRows].Annotation;
            }
        }
        /// <summary>
        /// 卡塞列数
        /// </summary>
        public string AnnounceCSTCols
        {
            get
            {
                return ParConfigPar.P_I.ParProduct_L[IndexCSTCols].Annotation;
            }
        }
        /// <summary>
        /// 龙骨间距
        /// </summary>
        public string AnnounceKeelInterval
        {
            get
            {
                return ParConfigPar.P_I.ParProduct_L[IndexKeelInterval].Annotation;
            }
        }
        /// <summary>
        /// 龙骨层间距
        /// </summary>
        public string AnnounceKeelSpacing
        {
            get
            {
                return ParConfigPar.P_I.ParProduct_L[IndexKeelSpacing].Annotation;
            }
        }
        /// <summary>
        /// 第一列龙骨间距
        /// </summary>
        public string AnnounceDisCol1
        {
            get
            {
                return ParConfigPar.P_I.ParProduct_L[IndexDisCol1].Annotation;
            }
        }
        /// <summary>
        /// 插栏方向
        /// </summary>
        public string AnnounceDirecInsert
        {
            get
            {
                return ParConfigPar.P_I.ParProduct_L[IndexDirecInsert].Annotation;
            }
        }
        /// <summary>
        /// 抛料方向
        /// </summary>
        public string AnnounceDirecDump
        {
            get
            {
                return ParConfigPar.P_I.ParProduct_L[IndexDirecDump].Annotation;
            }
        }
        /// <summary>
        /// 中片取片总数
        /// </summary>
        public string AnnounceSumPick
        {
            get
            {
                return ParConfigPar.P_I.ParProduct_L[IndexSumPick].Annotation;
            }
        }
        /// <summary>
        /// 插栏深度
        /// </summary>
        public string AnnounceInsertDepth
        {
            get
            {
                return ParConfigPar.P_I.ParProduct_L[IndexInsertDepth].Annotation;
            }
        }
        /// <summary>
        /// 卡塞高度
        /// </summary>
        public string AnnounceCSTHeight
        {
            get
            {
                return ParConfigPar.P_I.ParProduct_L[IndexCSTHeight].Annotation;
            }
        }
        /// <summary>
        /// 大小卡塞
        /// </summary>
        public string AnnounceCSTSize
        {
            get
            {
                return ParConfigPar.P_I.ParProduct_L[IndexCSTSize].Annotation;
            }
        }
        /// <summary>
        /// 龙骨深度
        /// </summary>
        public string AnnounceKeelDepth
        {
            get
            {
                return ParConfigPar.P_I.ParProduct_L[IndexKeelDepth].Annotation;
            }
        }
        /// <summary>
        /// 插栏起始行
        /// </summary>
        public string AnnounceStartRow
        {
            get
            {
                return ParConfigPar.P_I.ParProduct_L[IndexStartRow].Annotation;
            }
        }
        /// <summary>
        /// 插栏起始列
        /// </summary>
        public string AnnounceStartCol
        {
            get
            {
                return ParConfigPar.P_I.ParProduct_L[IndexStartCol].Annotation;
            }
        }
        /// <summary>
        /// 隔几行插栏
        /// </summary>
        public string AnnounceRowInterval
        {
            get
            {
                return ParConfigPar.P_I.ParProduct_L[IndexRowInterval].Annotation;
            }
        }
        /// <summary>
        /// 残材平台工位号
        /// </summary>
        public string AnnouncePlatStationNo
        {
            get
            {
                return ParConfigPar.P_I.ParProduct_L[IndexPlatStationNo].Annotation;
            }
        }

        public string AnnounceReserve1
        {
            get
            {
                return ParConfigPar.P_I.ParProduct_L[IndexReserve1].Annotation;
            }
        }
        public string AnnounceReserve2
        {
            get
            {
                return ParConfigPar.P_I.ParProduct_L[IndexReserve2].Annotation;
            }
        }
        public string AnnounceReserve3
        {
            get
            {
                return ParConfigPar.P_I.ParProduct_L[IndexReserve3].Annotation;
            }
        }
        public string AnnounceReserve4
        {
            get
            {
                return ParConfigPar.P_I.ParProduct_L[IndexReserve4].Annotation;
            }
        }
        public string AnnounceReserve5
        {
            get
            {
                return ParConfigPar.P_I.ParProduct_L[IndexReserve5].Annotation;
            }
        }
        public string AnnounceReserve6
        {
            get
            {
                return ParConfigPar.P_I.ParProduct_L[IndexReserve6].Annotation;
            }
        }
        #endregion 产品参数注释 

        #region ComboBox选项List
        public List<int> ParProductIndex_L
        {
            get
            {
                List<int> list = new List<int>();
                for(int i = 0;i < NUMREG; i++)
                {
                    list.Add(i);
                }
                return list;
            }
        }
        #endregion ComboBox选项List
    }

}
