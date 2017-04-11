using System.Linq;


namespace Assets.Source.Utils {

    public class CircularBuffer {

        private float[] buffer;

        private int i;

        public CircularBuffer(int bufferSize = 30) {
            buffer = new float[bufferSize];
        }

        public float AvgValue { get; private set; }

        public void Put(float value) {
            if (i >= buffer.Length) i = 0;
            buffer[i] = value;
            AvgValue = buffer.Sum()/buffer.Length;
            i++;
        }

    }

}