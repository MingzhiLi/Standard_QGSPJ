using Camera;
using DealPLC;
using DealRobot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectSPJ
{
    public class OtherAvancedSet
    {
        public static OtherAvancedSet Inst = new OtherAvancedSet();
        public bool BlCameraOffline
        {
            get
            {
                return RegeditCamera.R_I.BlOffLineCamera;
            }
            set
            {
                RegeditCamera.R_I.BlOffLineCamera = value;
            }
        }

        public bool BlPLCOffline
        {
            get
            {
                return RegeditPLC.R_I.BlOffLinePLC;
            }
            set
            {
                RegeditPLC.R_I.BlOffLinePLC = value;
            }
        }
        public bool BlRobotOffline
        {
            get
            {
                return RegeditRobot.R_I.BlOffLineRobot;
            }
            set
            {
                RegeditRobot.R_I.BlOffLineRobot = value;
            }
        }

    }
}
