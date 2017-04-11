using Assets.Source.Audio.Conrtrolleur;
using Assets.Source.Audio.Triggers;
using Assets.Source.Systems.Messaging;


namespace Assets.Audio.Triggers {

    public class PlayerAudioTrigger : AbstractAudioTrigger {

        private MessageDispatcher messageDispatcher;
        private JsAudioSound soundSet;
        private JsAudioSource aSource;
        protected override JsAudioSource source { get { throw new System.NotImplementedException(); } }

        protected override JsAudioSound sound { get { throw new System.NotImplementedException(); } }

        protected override MessageDispatcher MessageDispatcher { get { return messageDispatcher; } }

        void Awake() {
           messageDispatcher =  GetComponentInParent<MessageDispatcher>();
            soundSet = GetComponent<JsAudioSound>();
            aSource = GetComponent<JsAudioSource>();
        }
        void Start () {
            messageDispatcher.AddHandler<ShootMessage>(OnShootMessage);

            messageDispatcher.AddHandler<MorePointsMessage>(OnMoarPoinst);
            
        }

        private void OnMoarPoinst(MorePointsMessage message) {
            aSource.Play(soundSet, "MorePoints");
        }

        private void OnShootMessage(ShootMessage message) {
            if (!aSource.IsPlaying) {
                aSource.Play(soundSet, "Shoot");
            }
           
        }
        

    }

}
