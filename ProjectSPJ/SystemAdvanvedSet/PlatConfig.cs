using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace ProjectSPJ
{
    public class PlatConfig : BaseSerializer
    {
        public static PlatConfig Inst = new PlatConfig();

        /// <summary>
        /// 读取本地配置文件
        /// </summary>
        public static void ReadLocalFile()
        {
            Inst = Inst.ReadLocalFile<PlatConfig>();
        }

        /// <summary>
        /// 文件保存名称，如果不重写则会以基类名称来保存
        /// </summary>
        protected override string NameFile => MethodBase.GetCurrentMethod().DeclaringType.Name;

        /// <summary>
        /// 平台放片位枚举
        /// </summary>
        public PlatPlacePosEnum PlatPlacePosEnum { get; set; }
        /// <summary>
        /// 单电极时是否为水平放置，
        /// </summary>
        public bool IsHorizontalPut { get; set; } = false;
        /// <summary>
        /// 是否为镜像机台
        /// </summary>
        public bool IsMirrorMachine { get; set; } = false;
    }

}
