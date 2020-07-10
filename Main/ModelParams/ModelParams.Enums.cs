namespace Main
{
    #region robot std
    public enum BotStd
    {
        /// <summary>
        /// 粗定位取片位置
        /// </summary>
        PickPos = 1,
        /// <summary>
        /// 精定位位置
        /// </summary>
        PrecisePos,
        /// <summary>
        /// 二维码位置
        /// </summary>
        QrCodePos,
        /// <summary>
        /// 平台1位置
        /// </summary>
        Platform1,
        /// <summary>
        /// 平台2位置
        /// </summary>
        Platform2,
        /// <summary>
        /// 插栏交接位置
        /// </summary>
        InsertArmPos,
        /// <summary>
        /// 抛料位置
        /// </summary>
        DumpPos,
        /// <summary>
        /// 皮带线取片基准值
        /// </summary>
        BeltPickPos,
    }
    #endregion

    #region robot adj
    public enum BotAdj
    {
        /// <summary>
        /// 粗定位取片位置
        /// </summary>
        PickPos = 1,
        /// <summary>
        /// 精定位位置
        /// </summary>
        PrecisePos,
        /// <summary>
        /// 二维码位置
        /// </summary>
        QrCodePos,
        /// <summary>
        /// 平台1位置
        /// </summary>
        Platform1,
        /// <summary>
        /// 平台2位置
        /// </summary>
        Platform2,
        /// <summary>
        /// 插栏交接位置
        /// </summary>
        InsertArmPos,
        /// <summary>
        /// 抛料位置
        /// </summary>
        DumpPos,
        /// <summary>
        /// 皮带线取片基准值
        /// </summary>
        BeltPickPos,
        PickOffset,
    }
    #endregion

    #region plc寄存器
    /// <summary>
    /// 配方寄存器 d5020
    /// </summary>
    public enum RecipeRegister
    {
        /// <summary>
        /// 玻璃X
        /// </summary>
        GlassX,
        /// <summary>
        /// 玻璃Y
        /// </summary>
        GlassY,
        /// <summary>
        /// 玻璃厚度
        /// </summary>
        Thickness,
        /// <summary> 
        /// 二维码X   
        /// </summary>
        QrCodeX,
        /// <summary>
        /// 二维码Y
        /// </summary>
        QrCodeY,
        /// <summary>
        /// Mark1X
        /// </summary>
        Mark1X = 5,
        /// <summary>
        /// Mark1Y
        /// </summary>
        Mark1Y,
        /// <summary>
        /// Mark2X
        /// </summary>
        Mark2X,
        /// <summary>
        /// Mark2Y
        /// </summary>
        Mark2Y,

        /// <summary>
        /// 中片平台工位数
        /// </summary>
        PickStationNum,

        /// <summary>
        /// 上电极宽度
        /// </summary>
        TopE = 10,
        /// <summary>
        /// 下电极宽度
        /// </summary>
        BottomE,
        /// <summary>
        /// 左电极宽度
        /// </summary>
        LeftE,
        /// <summary>
        /// 右电极宽度
        /// </summary>
        RightE,
        /// <summary>
        /// 卡塞行数
        /// </summary>
        CSTRows,
        /// <summary>
        /// 卡塞列数
        /// </summary>
        CSTCols = 15,
        /// <summary>
        /// 龙骨间距
        /// </summary>
        KeelInterval,
        /// <summary>
        /// 龙骨层间距
        /// </summary>
        KeelSpacing,
        /// <summary>
        /// 第一列龙骨间距
        /// </summary>
        DisCol1,
        /// <summary>
        /// 插栏方向
        /// </summary>
        DIR_Insert,
        /// <summary>
        /// 抛料方向
        /// </summary>
        DIR_Dump = 20,
        /// <summary>
        /// 中片取片总数
        /// </summary>
        SUMPick,
        /// <summary>
        /// 插栏深度
        /// </summary>
        InsertDepth,
        /// <summary>
        /// 卡塞高度
        /// </summary>
        CSTHeight,
        /// <summary>
        /// 大小卡塞
        /// </summary>
        CSTSize,
        /// <summary>
        /// 龙骨深度
        /// </summary>
        KeelDepth = 25,
        /// <summary>
        /// 插栏起始行
        /// </summary>
        StartRow,
        /// <summary>
        /// 插栏起始列
        /// </summary>
        StartCol,
        /// <summary>
        /// 隔几行插栏
        /// </summary>
        RowInterval,
        /// <summary>
        /// 残材平台工位号
        /// </summary>
        PlatStaionNo,
    }
    /// <summary>
    /// 数据寄存器1，d1200
    /// </summary>
    public enum DataRegister1
    {
        PCAlarm = 1,
        /// <summary>
        /// 皮带线系数
        /// </summary>
        BeltRatioY = 4,
        /// <summary>
        /// 当前工位取片完成
        /// </summary>
        NextStation,
        /// <summary>
        /// 二维码读取结果
        /// </summary>
        QrCodeResult,
        /// <summary>
        /// 当前插栏总数
        /// </summary>
        CurrentInsertSum,
        /// <summary>
        /// LotID结果
        /// </summary>
        LotIDResult,
        /// <summary>
        /// 搬运臂插栏方向
        /// </summary>
        InsertDirectionForTrans,
        /// <summary>
        /// 搬运臂抛料方向
        /// </summary>
        DumpDirectionForTrans = 10,
        /// <summary>
        /// 插栏数据发送完成确认信号
        /// </summary>
        InsertDataConfirm,
        /// <summary>
        /// Trackout结果
        /// </summary>
        TrackOutResult,
        /// <summary>
        /// 巡边交接偏差确认信号
        /// </summary>
        InspDeviationConfirm,
        /// <summary>
        /// 皮带线停止
        /// </summary>
        StopBelt,
        /// <summary>
        /// 重启机器人
        /// </summary>
        RestartBot = 15,
        /// <summary>
        /// LotNum
        /// </summary>
        BeltRatioX = 16,
        /// <summary>
        /// 单目相机标定旋转角度
        /// </summary>
        MonoCalibAngle = 20,
        /// <summary>
        /// 卡塞相机拍照结果
        /// </summary>
        MonocalinResult,
        /// <summary>
        /// 残材标定的结果
        /// </summary>
        ResidueCalibResult,
    }
    /// <summary>
    /// 数据寄存器2，d1250
    /// </summary>
    public enum DataRegister2
    {
        /// <summary>
        /// 插栏1基准值
        /// </summary>
        StdCSTPos1,
        /// <summary>
        /// 插栏2基准值
        /// </summary>
        StdCSTPos2,
        /// <summary>
        /// 插栏3基准值
        /// </summary>
        StdCSTPos3,
        /// <summary>
        /// 插栏4基准值
        /// </summary>
        StdCSTPos4,
        /// <summary>
        /// 平台处玻璃X
        /// </summary>
        WidthAtPlat,
        /// <summary>
        /// 平台处玻璃Y
        /// </summary>
        HeightAtPlat,
        /// <summary>
        /// 巡边交接补偿X
        /// </summary>
        InspDeltaX,
        /// <summary>
        /// 巡边交接补偿Y
        /// </summary>
        InspDeltaY,
        /// <summary>
        /// 巡边交接补偿R
        /// </summary>
        InspDeltaR,
        /// <summary>
        /// 翻转平台处玻璃角度
        /// </summary>
        PlatAngle,
        /// <summary>
        /// 标定时补正角度
        /// </summary>
        CalibDeltaR,
        /// <summary>
        /// 二维码补偿X
        /// </summary>
        CodeComX,
        /// <summary>
        /// 二维码补偿Y
        /// </summary>
        CodeComY,
        /// <summary>
        /// 平台处二维码X
        /// </summary>
        CodeXAtPlat = 13,
        /// <summary>
        /// 平台处二维码Y
        /// </summary>
        CodeYAtPlat,
        /// <summary>
        /// 平台处Mark1X
        /// </summary>
        Mark1XAtPlat,
        /// <summary>
        /// 平台处Mark1Y
        /// </summary>
        Mark1YAtPlat,
        /// <summary>
        /// 平台处Mark2X
        /// </summary>
        Mark2XAtPlat,
        /// <summary>
        /// 平台处Mark2Y
        /// </summary>
        Mark2YAtPlat,
        /// <summary>
        /// 平台处上电极宽度
        /// </summary>
        TopEAtPlat,
        /// <summary>
        /// 平台处下电极宽度
        /// </summary>
        BottomEAtPlat,
        /// <summary>
        /// 平台处左电极宽度
        /// </summary>
        LeftEAtPlat,
        /// <summary>
        /// 平台处右电极宽度
        /// </summary>
        RightEAtPlat,
      
    }
    /// <summary>
    /// 数据寄存器3，d1300
    /// </summary>
    public enum DataRegister3
    {
        InsertStdCom,
        InsertData,
        InsertComZ1,
        KeelSpacing1 = 8,
    }
    #endregion

    #region plc报警代码
    public enum PCAlarm_Enum
    {
        标定失败 = 1,
        卡塞计算失败 = 2,
    }
    #endregion

    #region 运行模式设定
    public enum RunningMode
    {
        PassResidue1,
        PassResidue2,
        PassResidue3,
        Mirror,
        RecordData,
        UseRealRC
    }
    #endregion
}
