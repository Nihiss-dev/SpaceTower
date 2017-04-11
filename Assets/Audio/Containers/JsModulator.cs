using UnityEngine;

namespace Assets.Audio.Containers
{
    [System.Serializable]
    public class JsModulator
    {
        public AnimationCurve envelope;
        
        [Range(0.082f,0.5f)]
        private float A;
        [Range(0.5f, 0.7f)]
        private float D;
        [Range(0f, 1f)]
        private float S;
        [Range(0.7f, 1f)]
        private float R;
        //[SerializeField]
        private bool isLFO = false;
        //[SerializeField]
        private Waveforms waveform;
        [SerializeField]
        private AutomationTarget Automationtarget;
        public AutomationTarget currentTarget { get { return Automationtarget; } }
        private Keyframe[] EnvelopePoints;
        private int NbofPoints = 5;
        private enum Waveforms 
        {
            Sine,Triangle,Saw,Square
        }
        public enum AutomationTarget {

            Volume, None, Pitch
        }


        
        [Range(0f, 1.0f)] private float Amplitude;

        public bool IsLFO
        {
            get { return isLFO; }
            set { isLFO = value; }
        }

        public void UpdateCurve()
        {
            if (!isLFO)
            {
                EnvelopePoints = new Keyframe[NbofPoints];
                EnvelopePoints[0] = new Keyframe(0, 0);
                EnvelopePoints[1] = new Keyframe(A, 1f);
                EnvelopePoints[2] = new Keyframe(D, S);
                EnvelopePoints[3] = new Keyframe(R, 0.1f);
                EnvelopePoints[4] = new Keyframe(1f, 0f);
                envelope = new AnimationCurve(EnvelopePoints);
            }
            if (isLFO)
            {
                CreateWaveform(waveform);
            }
            
            
        }

        private void CreateWaveform(Waveforms waveforms)
        {
            if (waveforms == Waveforms.Sine)
            {
                EnvelopePoints = new Keyframe[3];
                EnvelopePoints[0] = new Keyframe(0, 0);
                EnvelopePoints[1] = new Keyframe(0.5f, 1f);
                EnvelopePoints[2] = new Keyframe(1f, 0f);
                envelope = new AnimationCurve(EnvelopePoints);
                for (var i = 0; i < EnvelopePoints.Length; i++)
                {
                    //AnimationUtility.SetKeyRightTangentMode(envelope, i, AnimationUtility.TangentMode.Free);
                    //AnimationUtility.SetKeyLeftTangentMode(envelope, i, AnimationUtility.TangentMode.Free);
                }
            }
            if (waveforms == Waveforms.Triangle)
            {
                EnvelopePoints = new Keyframe[NbofPoints];
                EnvelopePoints[0] = new Keyframe(0, 0);
                EnvelopePoints[1] = new Keyframe(0.25f, 0.5f);
                EnvelopePoints[2] = new Keyframe(0.5f, 1f);
                EnvelopePoints[3] = new Keyframe(0.75f, 0.5f);
                EnvelopePoints[4] = new Keyframe(1f, 0f);
                envelope = new AnimationCurve(EnvelopePoints);
                for (var i =0; i<EnvelopePoints.Length;i++)
                {
                    //AnimationUtility.SetKeyRightTangentMode(envelope,i,AnimationUtility.TangentMode.Linear);
                    //AnimationUtility.SetKeyLeftTangentMode(envelope, i, AnimationUtility.TangentMode.Linear);
                }
                
            }
            if (waveforms == Waveforms.Saw)
            {
                EnvelopePoints = new Keyframe[3];
                EnvelopePoints[0] = new Keyframe(0, 0);
                EnvelopePoints[1] = new Keyframe(0.05f, 1f);
                EnvelopePoints[2] = new Keyframe(1f, 0f);
                envelope = new AnimationCurve(EnvelopePoints);
                for (var i = 1; i < EnvelopePoints.Length; i++)
                {
                    //AnimationUtility.SetKeyRightTangentMode(envelope, i, AnimationUtility.TangentMode.Linear);
                    //AnimationUtility.SetKeyLeftTangentMode(envelope, i, AnimationUtility.TangentMode.Linear);
                }
            }
            if (waveforms == Waveforms.Square)
            {
                EnvelopePoints = new Keyframe[6];
                EnvelopePoints[0] = new Keyframe(0, 0);
                EnvelopePoints[1] = new Keyframe(0.1f, 1f);
                EnvelopePoints[2] = new Keyframe(0.45f, 1f);
                EnvelopePoints[3] = new Keyframe(0.5f, 0f);
                EnvelopePoints[4] = new Keyframe(0.95f, 0f);
                EnvelopePoints[5] = new Keyframe(1f, 0.01f);
                envelope = new AnimationCurve(EnvelopePoints);
            }
        }

        //***TODO Play Function;*/
        
    }
}