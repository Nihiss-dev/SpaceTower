namespace Assets.Source.Transforms2D {

    public interface ITransform2D {

        Rotation2D Rotation { get; set; }
        Position2D Position { get; set; }
        Position2D LocalPosition { get; set; }
        float X { get; set; }
        float Y { get; set; }
        float Z { get; set; }
        float LocalX { get; set; }
        float LocalY { get; set; }
        float LocalZ { get; set; }
        Scale2D Scale { get; set; }
        float ScaleX { get; set; }
        float ScaleY { get; set; }

        float DistanceTo(ITransform2D other);

    }

}