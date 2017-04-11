using Assets.Source.Transforms2D;


namespace Assets.Source.Systems.Camera {

    public interface ICamera {

        ITransform2D Transform { get; }

        void SetZoomOut(float value);

    }

}