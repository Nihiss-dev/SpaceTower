
using Assets.Source.Systems.Messaging;
using UnityEngine.Networking;


namespace Assets.Systems.Messaging
{

    public delegate void NetworkMessageDelegate<T>(T message) where T : NetMessage;

    public interface INetMessageDispatcher {

        void AddHandler<T>(NetworkMessageDelegate handler) where T : NetMessage;
        void Dispatch<T>(T message) where T: NetMessage;
       // void RemoveHandler<T>(NetworkMessageDelegate handler) where T : NetMessage;

    }

}