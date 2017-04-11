namespace Assets.Source.Systems.Messaging {

    public interface IMessageHandler<T> where T: Message {

        void OnMessage(T message);

    }

}