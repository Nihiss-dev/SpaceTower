using System;
using System.Collections.Generic;
using Assets.Audio.Containers;
using Assets.Source.Audio.Conrtrolleur;
using Assets.Source.Audio.Containers;
using UnityEngine;
using UnityEngine.Audio;

namespace Assets.Source.Audio
{
    [System.Serializable]

    public class JSAudioSoundSet : JsAudioBaseComponent
    {
        [SerializeField]
        protected string EmitterName;
        public bool IsLooping;
        public bool Is3D;
        public JsAudioMixerGroup EmitterMixer;
        public JsAudioSound[] EmitterSounds;
        public JsModulator[] JsAudioModulator;

        public JSAudioSoundSet(string name)
        {
            EmitterName = name;
            

        }

        public JSAudioSoundSet()
        {
        }
    }
}