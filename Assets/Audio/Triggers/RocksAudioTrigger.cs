using Assets.Source.Audio.Conrtrolleur;
using Assets.Source.Audio.Triggers;
using Assets.Source.Systems.Messaging;


namespace Assets.Audio.Triggers {

    public class RocksAudioTrigger : AbstractAudioTrigger {

        private JsAudioSource audioSource;
        private JsAudioSound aSound;
        private JsAudioSound soundSet;

        protected override JsAudioSource source { get { return audioSource; } }

        protected override JsAudioSound sound { get { return aSound; } }

        protected override MessageDispatcher MessageDispatcher { get { return GetComponentInParent<MessageDispatcher>(); } }

        // Use this for initialization
        void Start()
        {
            MessageDispatcher.AddHandler<RockImpactMessage>(OnRockImpact);
            soundSet = GetComponent<JsAudioSound>();
            audioSource = GetComponent<JsAudioSource>();
        }

        private void OnRockImpact(RockImpactMessage message) {
            source.Play(soundSet, "RocksImpact");
        }
    }

}
