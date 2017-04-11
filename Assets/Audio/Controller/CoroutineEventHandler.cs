using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Audio.Containers;
using Assets.Source.Audio;
using Assets.Source.Audio.Conrtrolleur;


namespace Assets.Audio.Controller
{
    using UnityEngine;
    [HideInInspector]
    public class CoroutineEventHandler : MonoBehaviour {

        private bool called;
        private bool loop;
        private AudioSource source;
        private EnvelopePlayEvent envelopePlayEvent;
        private float timeLimit;
        private JsModulator modulator;

        public void playEnvelopeEvent(JsModulator curve,AudioSource jsource = null, float length = 0, bool loop = false) {
            source = jsource;
            this.loop = loop;
            timeLimit = length;
            modulator = curve;
            called = true;
            // envelopePlayEvent = new EnvelopePlayEvent(curve,jsource, length, loop);
            //StartCoroutine(envelopePlayEvent[envelopePlayEvent.Count-1].TrackEvent());
        }

        void FixedUpdate()
        {
            /*if (envelopePlayEvent != null) {
                /*if (source.isPlaying && loop && envelopePlayEvent.isFinished)
                {
                    StartCoroutine(envelopePlayEvent.TrackEvent());
                }*/
            /*if (!playEvent.isFinished)
                 {
                     Debug.Log("Finished !!");
                     playEvent.isFinished = false;
                     StopCoroutine(playEvent.TrackEvent());

                 }
             }*/
            if (called) {
                float samplePlaybackPosition = source.timeSamples;
                if(timeLimit > samplePlaybackPosition)
                {
                    samplePlaybackPosition = source.timeSamples;
                    if (modulator.currentTarget == JsModulator.AutomationTarget.Volume) {
                        var value = JSAudioUtils.JsRandom(0.1f, 0.5f);
                        modulator.envelope.MoveKey(modulator.envelope.length-1, new Keyframe(value, 0));
                        source.volume = modulator.envelope.Evaluate(samplePlaybackPosition / timeLimit);
                    }
                    if (modulator.currentTarget == JsModulator.AutomationTarget.Pitch)
                    {
                        var value = modulator.envelope.Evaluate(samplePlaybackPosition / timeLimit);
                        source.pitch = value;

                    }



                }
                if (timeLimit == samplePlaybackPosition ) {
                    called = false;
                }
            }
                

            }
            
            
        }
    }

