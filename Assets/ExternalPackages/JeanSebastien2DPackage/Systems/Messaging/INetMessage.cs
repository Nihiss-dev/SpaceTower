using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JetBrains.Annotations;
using UnityEngine.Networking;


namespace Assets.Systems.Messaging
{
    public interface INetMessage {

        short Id { get; }

    }
}
