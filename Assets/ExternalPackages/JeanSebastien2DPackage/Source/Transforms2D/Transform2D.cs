using Assets.Shared;
using UnityEngine;


namespace Assets.Source.Transforms2D {

    public class Transform2D : BaseComponent, ITransform2D {

        public float DistanceTo(ITransform2D other) {
            return Position.DistanceTo(other.Position);
        }
        
        public Rotation2D Rotation {
            get { return Transform.eulerAngles.z; }
            set { Transform.eulerAngles = new Vector3(Transform.eulerAngles.x, Transform.eulerAngles.y, value); }
        }

        public Position2D Position {
            get { return Transform.position; }
            set { Transform.position = new Vector3(value.X, value.Y, Transform.position.z); }
        }

        public Position2D LocalPosition {
            get { return Transform.localPosition; }
            set { Transform.localPosition = new Vector3(value.X, value.Y, Transform.localPosition.z); }
        }

        public float X {
            get { return Transform.position.x; }
            set { Transform.position = new Vector3(value, Transform.position.y, Transform.position.z); }
        }

        public float Y {
            get { return Transform.position.y; }
            set { Transform.position = new Vector3(Transform.position.x, value, Transform.position.z); }
        }

        public float Z {
            get { return Transform.position.z; }
            set { Transform.position = new Vector3(Transform.position.x, Transform.position.y, value); }
        }

        public float LocalX {
            get { return Transform.localPosition.x; }
            set { Transform.localPosition = new Vector3(value, Transform.localPosition.y, Transform.localPosition.z); }
        }

        public float LocalY {
            get { return Transform.localPosition.y; }
            set { Transform.position = new Vector3(Transform.localPosition.x, value, Transform.localPosition.z); }
        }

        public float LocalZ {
            get { return Transform.localPosition.z; }
            set { Transform.position = new Vector3(Transform.localPosition.x, Transform.localPosition.y, value); }
        }

        public float ScaleX {
            get { return Transform.localScale.x; }
            set { Transform.localScale = new Vector3(value, Transform.localScale.y, Transform.localScale.z); }
        }

        public float ScaleY {
            get { return Transform.localScale.y; }
            set { Transform.localScale = new Vector3(Transform.localScale.x, value, Transform.localScale.z); }
        }

        public Scale2D Scale {
            get { return Transform.localScale; }
            set { Transform.localScale = new Vector3(value.X, value.Y, Transform.localScale.z); }
        }

        private Transform Transform { get { return GetComponent<Transform>(); } }

    }

}