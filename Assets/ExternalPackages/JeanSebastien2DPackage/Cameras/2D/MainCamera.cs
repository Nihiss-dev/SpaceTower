using Assets.Shared;
using Assets.Source.Transforms2D;
using UnityEngine;


namespace Assets.Source.Systems.Camera {

    [ExecuteInEditMode]
    public class MainCamera : BaseComponent, ICamera, IScreenRelativeSpace {

        [SerializeField] private float defaultSize = 5;
        [SerializeField] private float zoomOutLevel = 1;

        private new UnityEngine.Camera camera;

        public void SetZoomOut(float value) {
            zoomOutLevel = value;
        }

        public ITransform2D Transform { get { return GetComponent<ITransform2D>(); } }

        public Position2D ConvertToWorldSpace(int x, int y) {
            return camera.ScreenToWorldPoint(new Vector2(x, y));
        }

        public Position2D ConvertToWorldSpace(Vector2 p) {
            return camera.ScreenToWorldPoint(p);
        }

        public Position2D GetPositionRelativeToCenter(Position2D position) {
            return position - Center;
        }

        public Position2D Center { get { return ConvertToWorldSpace(camera.pixelWidth/2, camera.pixelHeight/2); } }

        public Position2D UpperRightCorner { get { return ConvertToWorldSpace(camera.pixelWidth, camera.pixelHeight); } }

        public Position2D LowerRightCorner { get { return ConvertToWorldSpace(camera.pixelWidth, 0); } }

        public Position2D UpperLeftCorner { get { return ConvertToWorldSpace(0, camera.pixelHeight); } }

        public Position2D LowerLeftCorner { get { return ConvertToWorldSpace(0, 0); } }

        public Position2D UpperCenter { get { return ConvertToWorldSpace(camera.pixelWidth/2, camera.pixelHeight); } }

        public Position2D LowerCenter { get { return ConvertToWorldSpace(camera.pixelWidth/2, 0); } }

        public Position2D RightCenter { get { return ConvertToWorldSpace(camera.pixelWidth, camera.pixelHeight/2); } }

        public Position2D LeftCenter { get { return ConvertToWorldSpace(0, camera.pixelHeight/2); } }

        public float Height { get { return camera.orthographicSize; } }

        public float Width { get { return camera.orthographicSize*camera.aspect; } }

        protected void Awake() {
            camera = GetComponent<UnityEngine.Camera>();
        }

        private void Update() {
            camera.orthographicSize = defaultSize*zoomOutLevel;
        }

    }

}