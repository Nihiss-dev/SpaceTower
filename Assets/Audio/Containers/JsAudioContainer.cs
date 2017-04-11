using System;
using System.Collections.Generic;
using Assets.Audio.Containers;
using UnityEngine;
using UnityEngine.Audio;


namespace Assets.Source.Audio.Containers {

    [Serializable]
    public class JsAudioContainer {

        [SerializeField] private string soundSetName;
        [SerializeField] private JsAudioMixerGroup mixer;
        [SerializeField] private bool _soundLoops = false;
        [SerializeField] private bool _randomSound = false;
        [SerializeField] private bool _randomizePitch =false;
        [SerializeField] [Range(0, 0.5f)] private float randomPitchRange;
        [SerializeField] private AudioClip[] _audioclips;
        [SerializeField] private JsModulator envelope;

        public JsModulator Envelope { get { return envelope; } set { envelope = value; } }

        public string Name { get { return soundSetName; } set { soundSetName = value; } }

        public AudioClip[] Audioclips { get { return _audioclips; } set { _audioclips = value; } }

        public AudioMixerGroup Mixer { get { return mixer.MixerGroup; } set { mixer.MixerGroup = value; } }

        public float pitchRangeValue { get { return randomPitchRange; } }
        public List<bool> GetBools() {
            List<bool> Bools = new List<bool>();
            Bools.Add(_soundLoops);
            Bools.Add(_randomSound);
            Bools.Add(_randomizePitch);
            return Bools;
        }

    }

}