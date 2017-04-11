using System.Collections;
using Assets.Audio.Containers;
using Assets.Source.Audio;
using Assets.Source.Audio.Conrtrolleur;
using UnityEngine;


namespace Assets.Audio.Controller
{
    
    public class EnvelopePlayEvent: AbstractAudioEvent {

        private AudioSource source;
        private JsModulator modulator;
        public JsModulator modulatorValue { get { return modulator;} set { modulator = value; } }
        private bool finished;
        public bool isFinished { get { return eventComplete; } set { finished = eventComplete; } }
        private bool loop;
        public EnvelopePlayEvent(JsModulator modulator, AudioSource source = null, float length = 0, bool loop = false) {
            timeLimit = length;
            this.source = source;
            this.loop = loop;
            this.modulator = modulator;
        }
        public override void EndEvent()
        {
            
        }

        public override IEnumerator TrackEvent()
        {
            
            
            eventComplete = true;
            yield return null;

        }

        

    }
}
