using UnityEngine;


namespace Assets.Source.Geometry {

    public class Degrees {

        public Degrees(float value) {
            Value = value%360f;
        }

        public float Value { get; private set; }

        public static Degrees BetweenPoints(Vector2 p1, Vector2 p2) {
            var direction = p2 - p1;
            var angle = Mathf.Atan2(direction.y, direction.x)/(2*Mathf.PI)*360;
            if (angle < 0) angle += 360;
            return new Degrees(angle);
        }

        public bool IsBetween(Degrees lower, Degrees upper) {
            return (Value >= lower.Value && Value <= upper.Value);
        }
        
        public static implicit operator Degrees(float value) {
            return new Degrees(value);
        }

        public static implicit operator float(Degrees value) {
            return value.Value;
        }

    }

}