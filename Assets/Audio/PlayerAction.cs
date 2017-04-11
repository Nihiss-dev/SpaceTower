using Assets.Shared;
using Assets.Source.Systems.Messaging;
using Assets.Systems.Messaging.Messages;


namespace Assets.Audio {

    public class PlayerAction : BaseComponent {

        private MessageDispatcher dispatcher;
        // Use this for initialization
        void Start () {
            dispatcher = GetComponent<MessageDispatcher>();
        }


        public void ShootAction() {
            dispatcher.Dispatch(new ShootMessage());
        }

        public void MorePoints() {
            dispatcher.Dispatch(new MorePointsMessage());
        }
    }

}
