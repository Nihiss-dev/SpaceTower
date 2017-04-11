#pragma warning disable 0414
using Assets.Source.Audio;
using Assets.Source.Audio.Conrtrolleur;
using Assets.Source.Audio.Music;
using Assets.Source.ResourceLoading;
using Assets.Source.Systems.Camera;
using Assets.Source.Transforms2D;
using UnityEngine;
using UnityEngine.Audio;


namespace Assets.Audio.Controller {

    public class JsAudioControl : JsAudioBaseComponent{

        [SerializeField] private FilePath mixerFilePath = new FilePath("Audio/AudioMixerGroup/Master");
        [SerializeField] private int panningDivider = 35;
        [SerializeField] private float StartDetection =100;
        [SerializeField] private float StopDetection =200;
        [SerializeField] private float currentVolume;
        [SerializeField] private float inboundVolume;
        private float currentPosition;
        [SerializeField] private AnimationCurve CurrentMusicAttenuation;
        [SerializeField] private AnimationCurve InboundMusicAttenuation;
        private bool isMainCamera;

        private AudioMixer mixer;
        private JsAudioSource source;
        private JsAudioSource source2;
        private MusicPlayer[] musicPlayers;
        
        private ITransform2D cameraTransform;
        private new ITransform2D transform;
    
        public bool IsMainCamera
        {
            get
            {
                return isMainCamera;
            }
        }


        // Use this for initialization
        protected void Awake() {
            isMainCamera = HasComponent<UnityEngine.Camera>();
            cameraTransform = GetComponentInScene<ICamera>().Transform;
            transform = GetComponent<ITransform2D>();
            mixer = new AudioMixerLoader().Load(mixerFilePath);

            if (IsMainCamera)
            {
                musicPlayers = GetComponentsInScene<MusicPlayer>();
                source = musicPlayers[0].GetAudiosource;
                source2 = musicPlayers[1].GetAudiosource;
             
            }

            if (!IsMainCamera)
            {
                source = GetComponent<JsAudioSource>();
            }
            ResetMixers();

            
        }
        private float PanningX { set { source.Panning = value; } }
        // Update is called once per frame
        void Update()
        {
            var panningX = transform.X - cameraTransform.X;
            var distance = transform.DistanceTo(cameraTransform);
            var worldDistance = cameraTransform.Y;
            PanningX = panningX/panningDivider;
            mixer.SetFloat("FishVol", -distance);
            if (worldDistance >= StartDetection && worldDistance <= StopDetection && isMainCamera)
            {
                var divider = StopDetection - StartDetection;
                currentPosition = (cameraTransform.Y - StartDetection)/divider;
                currentVolume = CurrentMusicAttenuation.Evaluate(currentPosition)*80;
                inboundVolume = InboundMusicAttenuation.Evaluate(currentPosition)*80;
                mixer.SetFloat("HapAmbMusic", -inboundVolume);
                mixer.SetFloat("DarAmbMusic", -currentVolume);
            }


        }

        public void  ResetMixers()
        {
            mixer.SetFloat("HapAmbMusic", -80);
            mixer.SetFloat("DarAmbMusic", 0);
        }
    }

}