using Assets.Source.Systems.Messaging;


namespace Assets.Systems.Messaging.Messages {

    public class MessageTimeMagic : Message {

        private bool IsGlobal;

        public bool isGlobal { get { return IsGlobal; }  set { IsGlobal = value; }  }

    }

}