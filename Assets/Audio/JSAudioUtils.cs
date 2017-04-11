using System;

using Random = UnityEngine.Random;


namespace Assets.Source.Audio
{
    public static class JSAudioUtils
    {
        public static int JsRandom(int i, int count)
        {
           return Random.Range(i, count);
        }

        public static float JsRandom(float i, float count) {
            return Random.Range(i, count);
        }

        public static float FloatToDecibel(float dB)
        {
            return (float) Math.Pow(10.0f, 0.05*dB);
        }
        public static float DecibelToFloat(float volume)
        {

            return (float) (20.0f*Math.Log10(volume));
        }
    }
}