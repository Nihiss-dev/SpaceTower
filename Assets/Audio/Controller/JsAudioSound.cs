using System.Collections.Generic;
using Assets.Audio.Containers;
using Assets.Source.Audio.Containers;
using UnityEngine;
using UnityEngine.Audio;


namespace Assets.Source.Audio.Conrtrolleur {
    //[ExecuteInEditMode]
    public class JsAudioSound : JsAudioBaseComponent //, IJAudioSound
    {
        
        public JsAudioContainer[] soundSets;

        
        private AudioClip selectedClip;

        private Dictionary<string, List<AudioClip>> _soundSetClips = new Dictionary<string, List<AudioClip>>();

        private Dictionary<string, AudioMixerGroup> _soundSetMixers = new Dictionary<string, AudioMixerGroup>();

        private Dictionary<string, List<bool>> _soundSetBools = new Dictionary<string, List<bool>>();

        private Dictionary<string, JsModulator> _soundSetEnvelopes = new Dictionary<string, JsModulator>();

        private Dictionary<string, float> _randomPitchRange = new Dictionary<string, float>();

        public int SampleLength { get { return  selectedClip.samples; } }

        
        public Dictionary<string, List<bool>> SoundSetBools { get { return _soundSetBools; } set { _soundSetBools = value; } }

        public Dictionary<string, List<AudioClip>> SoundSetClips { get { return _soundSetClips; } set { _soundSetClips = value; } }

        public Dictionary<string, AudioMixerGroup> SoundSetMixers { get { return _soundSetMixers; } set { _soundSetMixers = value; } }

        public JsAudioContainer[] SoundSets { get { return soundSets; } set { soundSets = value; } }

        public bool DoesLoop(string action) {
            return _soundSetBools[action][0];
        }

        public bool IsRandomClip(string action) {
            return _soundSetBools[action][1];
        }
        public bool IsRandomPitch(string action)
        {
            return _soundSetBools[action][2];
        }

        public float RandomPitchRange(string action) {
            var value = _randomPitchRange[action];
            return 1+JSAudioUtils.JsRandom(-value, value);
        }


        public AudioClip DefaultClip(string action) {
            selectedClip = _soundSetClips[action][0];
            ;
            return selectedClip;
        }

        public AudioClip Randomclip(string action) {
            selectedClip = _soundSetClips[action][JSAudioUtils.JsRandom(0, _soundSetClips[action].Count)];
            return selectedClip;
        }

        public List<AudioClip> ArrayToList(int index) {
            List<AudioClip> clips = new List<AudioClip>();
            AudioClip[] sounds = SoundSets[index].Audioclips;
            for (var i = 0; i < sounds.Length; i++) {
                clips.Add(sounds[i]);
            }
            return clips;
        }

        public AudioClip DefinedClip(string action, int index) {
            selectedClip = _soundSetClips[action][index];
            ;
            return selectedClip;
        }

        public AudioMixerGroup GetAudioMixerGroup(string action) {
            return _soundSetMixers[action];
        }

        public JsModulator GetModulator(string action) {
            return _soundSetEnvelopes[action];
        }

        void Start() {
            for (var i = 0; i < SoundSets.Length; i++) {
                if (!SoundSetClips.ContainsKey(SoundSets[i].Name)) {
                    SoundSetClips.Add(SoundSets[i].Name, ArrayToList(i));
                }
                if (!SoundSetMixers.ContainsKey(SoundSets[i].Name)) {
                    SoundSetMixers.Add(SoundSets[i].Name, SoundSets[i].Mixer);
                }
                if (!SoundSetBools.ContainsKey(SoundSets[i].Name)) {
                    SoundSetBools.Add(SoundSets[i].Name, SoundSets[i].GetBools());
                }
                if (!_soundSetEnvelopes.ContainsKey(SoundSets[i].Name))
                {
                    _soundSetEnvelopes.Add(SoundSets[i].Name, SoundSets[i].Envelope);
                }
                if (!_randomPitchRange.ContainsKey(SoundSets[i].Name))
                {
                    _randomPitchRange.Add(SoundSets[i].Name, SoundSets[i].pitchRangeValue);
                }
            }
        }

    }

}