using Assets.Audio.Containers;
using Assets.Audio.Controller;
using Assets.Source.Audio.Containers;
using UnityEngine;

#pragma warning disable 0414


namespace Assets.Source.Audio.Conrtrolleur {

    public class JsAudioSource : JsAudioBaseComponent {

        private AudioSource source;
        private JsAudioSource Jsource;
        public CoroutineEventHandler coroutine;

        [Range(0f, 1.0f)] [SerializeField] private float volume = 1;
        [Range(-3.0f, 3.0f)] [SerializeField] private float pitch;
        [Range(-1.0f, 1.0f)] [SerializeField] private float panning;

        private JsModulator envelope;

        public bool IsPlaying { get { return source.isPlaying; } }

        public float Panning { get { return source.panStereo; } set { source.panStereo = value; } }

        public float Pitch { get { return source.pitch; } set { source.pitch = value; } }

        public float Volume { get { return source.volume; } set { source.volume = value; } }

        public AudioSource Source { get { return source; } set { source = value; } }

        public JsAudioSource JsAudiosource { get { return Jsource; } set { Jsource = value; } }

        public void Play(JsAudioSound sound, string eventAction, int index = -1) {
            source.clip = null;
            source.loop = sound.DoesLoop(eventAction);
            source.outputAudioMixerGroup = sound.GetAudioMixerGroup(eventAction);
            envelope = sound.GetModulator(eventAction);
            if (sound.IsRandomClip(eventAction) && index == -1)
            {
                source.clip = sound.Randomclip(eventAction);
            }
            if (index > -1 && !sound.IsRandomClip(eventAction)) {
                source.clip = sound.DefinedClip(eventAction, index);
            }
            else if(source.clip == null) {
                source.clip = sound.DefaultClip(eventAction);
            }
            if (sound.IsRandomPitch(eventAction)) {
                source.pitch = sound.RandomPitchRange(eventAction);
            }
            if (envelope != null && source != null && !source.isPlaying) {
                source.Play();
                coroutine.playEnvelopeEvent(envelope,source, sound.SampleLength, sound.DoesLoop(eventAction));
            }
            
        }

        public void Stop() {
            source.Stop();
        }

        protected void Awake() {
            
            if (source == null) {
                source = GetComponent<Transform>().gameObject.AddComponent<AudioSource>();
            }
            coroutine = gameObject.AddComponent<CoroutineEventHandler>();
            // source.hideFlags = HideFlags.HideInInspector;
        }

        void Update() {
            volume = Volume;
            pitch = Pitch;
            panning = Panning;
        }

    }

}