using System;
using UnityEngine;
using UnityEngine.Audio;


namespace Assets.Source.Audio.Containers {

    [Serializable]
    [ExecuteInEditMode]
    public class JsAudioMixerGroup //: JsAudioBaseComponent,IJsAudioMixerGroup
    {

        [SerializeField] private AudioMixerGroup mixerGroup;

        public AudioMixerGroup MixerGroup { get { return mixerGroup; } set { mixerGroup = value; } }

    }

}