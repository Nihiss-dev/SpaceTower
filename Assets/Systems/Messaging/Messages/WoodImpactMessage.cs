using Assets.Source.Systems.Messaging;

namespace Assets.Systems.Messaging.Messages {

    public class WoodImpactMessage : Message{

        private string objectTag;

        public WoodImpactMessage(string tagValue) {
            tag = tagValue;
        }
        public string tag { get { return objectTag; } set { objectTag = value; } }

    }

}