using UnityEngine.Networking;


namespace Assets.Systems.Messaging.NetworkMessages
{
    internal class ChangeSeasonMessage:NetMessage
    {
        public int seasonIndex;

        public ChangeSeasonMessage(int chosenSeason)
        {
            this.seasonIndex = chosenSeason;
        }

        public ChangeSeasonMessage() {
            
        }

    }
}