using Assets.Source.Geometry;


namespace Assets.Source.Transforms2D {

    public class Rotation2D {

        public Rotation2D(Degrees value) {
            Value = value;
        }

        public Degrees Value { get; private set; }

        public static implicit operator Rotation2D(Degrees value) {
            return new Rotation2D(value);
        }

        public static implicit operator Rotation2D(float value) {
            return new Rotation2D(value);
        }

        public static implicit operator float(Rotation2D value) {
            return value.Value;
        }

        public bool IsBetween(float lower, float upper) {
            return (Value >= lower && Value <= upper);
        }

    }

}