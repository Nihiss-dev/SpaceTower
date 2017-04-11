using UnityEngine;


namespace Assets.Source.Transforms2D {

    public class Position2D {

        public Position2D(Vector2 value) {
            Value = value;
        }

        public Position2D(float x, float y) {
            Value = new Vector2(x, y);
        }

        public Vector2 Value { get; private set; }

        public float Y { get { return Value.y; } }

        public float X { get { return Value.x; } }

        public static implicit operator Position2D(Vector3 v) {
            return new Position2D(v.x, v.y);
        }

        public static implicit operator Position2D(Vector2 v) {
            return new Position2D(v.x, v.y);
        }

        public static implicit operator Vector2(Position2D p) {
            return new Vector2(p.X, p.Y);
        }

        public static implicit operator Vector3(Position2D p) {
            return new Vector2(p.X, p.Y);
        }

        public static Vector2 operator -(Position2D p1, Position2D p2) {
            return p1.Value - p2.Value;
        }

        public float DistanceTo(Position2D other) {
            return Vector2.Distance(Value, other);
        }

        public Vector2 DirectionTo(Position2D position) {
            return new Vector2(position.X - X, position.Y - Y).normalized;
        }

    }

}