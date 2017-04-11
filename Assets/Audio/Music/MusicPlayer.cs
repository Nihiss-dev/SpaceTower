using Assets.Shared;
using Assets.Source.Audio.Conrtrolleur;
using Assets.Source.Audio.Containers;
using UnityEngine;


namespace Assets.Source.Audio.Music {

    public class MusicPlayer : BaseComponent {

        private JsAudioSource Jsource;
        [SerializeField] private JsAudioMixerGroup mixer;
        private AudioSource source;

        public bool IsMixerAudible { get; /*{return mixer;}*/ set; }

        public bool SourceIsPlaying { get { return Jsource.IsPlaying; } }

        public JsAudioSource GetAudiosource { get { return GetComponent<JsAudioSource>(); } }

        public AudioSource Getsource { get { return GetComponent<AudioSource>(); } }

        protected void Awake() {
            Jsource = GetAudiosource;
            
        }

        void Update() {
            if (IsMixerAudible && SourceIsPlaying) {}
        }

    }

}