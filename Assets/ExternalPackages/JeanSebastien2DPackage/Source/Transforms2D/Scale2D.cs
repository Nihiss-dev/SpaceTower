using UnityEngine;


namespace Assets.Source.Transforms2D {

    public class Scale2D {

        public Scale2D(Vector2 value) {
            Value = value;
        }

        public Scale2D(float x, float y) {
            Value = new Vector2(x, y);
        }

        public Vector2 Value { get; private set; }

        public float Y {
            get { return Value.y; }
        }

        public float X {
            get { return Value.x; }
        }

        public static implicit operator Scale2D(Vector3 v) {
            return new Scale2D(v.x, v.y);
        }

    }

}