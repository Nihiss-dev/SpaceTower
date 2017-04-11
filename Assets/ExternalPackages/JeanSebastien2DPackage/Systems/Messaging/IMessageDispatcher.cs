
using Assets.Source.Systems.Messaging;

namespace Assets.Systems.Messaging
{

    public delegate void Handler<T>(T message) where T : Message;

    public interface IMessageDispatcher {

        void AddHandler<T>(Handler<T> handler) where T : Message;
        void Dispatch<T>(T message) where T: Message;
        void RemoveHandler<T>(Handler<T> handler) where T : Message;

    }

}