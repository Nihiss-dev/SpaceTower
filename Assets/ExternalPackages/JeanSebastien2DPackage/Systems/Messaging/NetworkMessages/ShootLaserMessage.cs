using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Systems.Messaging.NetworkMessages
{
    public class ShootLaserMessage: NetMessage
    {
        public bool isDrainingNow;

        public ShootLaserMessage()
        {

        }
        public ShootLaserMessage(bool isDraining)
        {
            this.isDrainingNow = isDraining;
        }
    }
}
