using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.RightsManagement;
using System.Text;
using System.Threading.Tasks;

namespace ProjectSPJ
{
    /// <summary>
    /// 用于显示的参数
    /// </summary>
    public class ParForShow: BaseSerializer
    {
        public static ParForShow Inst = new ParForShow();

        #region 粗定位处参数
        public double RoughlyX => ProductSet.Inst.GlassX;
        public double RoughlyY => ProductSet.Inst.GlassY;

        public double Pickangle => Math.Round(ParModelValue.PickAngle, 3);
        public double PickHeight => Math.Round(RobotPar.Inst.PickPosZ + ProductSet.Inst.Thickness + ParModelValue.Inst.ComPickPosZ, 3);
        #endregion 粗定位处参数

        #region 精定位处参数
        public double PreciseRobotAngle => Math.Round(ParModelValue.PreciseRobotAngle, 3);
        public double PrecisePhotoPos1X => Math.Round(ParModelValue.PrecisePhotoPos1.DblValue1, 3);
        public double PrecisePhotoPos1Y => Math.Round(ParModelValue.PrecisePhotoPos1.DblValue2, 3);
        public double PrecisePhotoPos2X => Math.Round(ParModelValue.PrecisePhotoPos2.DblValue1, 3);
        public double PrecisePhotoPos2Y => Math.Round(ParModelValue.PrecisePhotoPos2.DblValue2, 3);
        public double PrecisePhotoPosZ => Math.Round(RobotPar.Inst.PrecisePhotoPosZ, 3);
        #endregion 精定位处参数

        #region 残材平台处参数
        public double WastageGlassAngle => Math.Round(ParModelValue.WastageAngle, 3);
        public double WastageGlassX => Math.Round(ParModelValue.WastageX, 3);
        public double WastageGlassY => Math.Round(ParModelValue.WastageY, 3);

        public double WastageCodeX => Math.Round(ParModelValue.CodeXInWastage, 3);
        public double WastageCodeY => Math.Round(ParModelValue.CodeYInWastage, 3);

        public double WastageRobotAngle => Math.Round(ParModelValue.WastageRobotAngle, 3);
        public double WastageRobotX => Math.Round(ParModelValue.PosWastagePlat1.DblValue1, 3);
        public double WastageRobotY => Math.Round(ParModelValue.PosWastagePlat1.DblValue2, 3);
        public double WastageRobotZ1 => Math.Round(ParModelValue.PosWastagePlat1.DblValue3 + ParModelValue.Inst.ComPlatZ1, 3);
        public double WastageRobotZ2 => Math.Round(ParModelValue.PosWastagePlat1.DblValue3 + ParModelValue.Inst.ComPlatZ2, 3);

        #endregion 残材平台处参数
    }
}
