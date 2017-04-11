using Assets.Source.Audio.Conrtrolleur;
using Assets.Source.Systems.Messaging;
using Assets.Source.Systems.Messaging.Messages;


namespace Assets.Source.Audio.Triggers {

    public class ThirdPersonAudioTrigger : AbstractAudioTrigger {

        protected override JsAudioSource source { get {return GetComponent<JsAudioSource>(); } }

        protected override JsAudioSound sound { get { return GetComponent<JsAudioSound>(); } }

        protected override MessageDispatcher MessageDispatcher { get { return GetComponentInParent<MessageDispatcher>(); } }


        protected void Awake()
        {

            
            MessageDispatcher.AddHandler<DeathMessage>(OnDeathMessage);
        }

        private void OnDeathMessage(DeathMessage message)
        {
            if (!source.IsPlaying) {
                source.Play(sound, "Death");
            }
            
        }


     }

}
