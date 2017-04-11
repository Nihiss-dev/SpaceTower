using Assets.Source.ResourceLoading;
using Assets.Source.Systems.Camera;
using Assets.Source.Systems.Messaging;
using Assets.Source.Systems.Messaging.Messages;
using Assets.Source.Transforms2D;
using UnityEngine;
using UnityEngine.Audio;

#pragma warning disable 0414


namespace Assets.Source.Audio.Conrtrolleur {

    public class JsAudioPlayback : JsAudioBaseComponent {

        [SerializeField] private FilePath mixerFilePath = new FilePath("Audio/AudioMixerGroup/Master");
        [SerializeField] private int panningDivider = 35;
        [SerializeField] private float currentVolume;

        private AudioMixer mixer;
        private JsAudioSource source;
        private JsAudioSound sound;
        [SerializeField] private AnimationCurve InboundMusicAttenuation;
        private float StopDetection = 80;

        private ITransform2D cameraTransform;
        private new ITransform2D transform;
        private float distance;

        

        private float PanningX { set { source.Panning = value; } }



        protected void Awake() {
            cameraTransform = GetComponentInScene<ICamera>().Transform;
            transform = GetComponentInParent<ITransform2D>();
            sound = GetComponent<JsAudioSound>();
            mixer = new AudioMixerLoader().Load(mixerFilePath);

            source = GetComponent<JsAudioSource>();

        }

        void Update() {
            var panningX = transform.X - cameraTransform.X;
            distance = transform.DistanceTo(cameraTransform);
            PanningX = panningX/panningDivider;
            var currentDistance = distance/StopDetection;
            var inboundVolume = InboundMusicAttenuation.Evaluate(currentDistance); //* 80;
            source.Volume = inboundVolume; //mixer.SetFloat("MonsterVol", -inboundVolume);  
        }



    }

}