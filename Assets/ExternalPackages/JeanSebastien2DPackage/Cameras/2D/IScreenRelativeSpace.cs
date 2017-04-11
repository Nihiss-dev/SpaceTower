using Assets.Source.Transforms2D;
using UnityEngine;


namespace Assets.Source.Systems.Camera {

    public interface IScreenRelativeSpace {

        Position2D Center { get; }
        Position2D UpperRightCorner { get; }
        Position2D LowerRightCorner { get; }
        Position2D UpperLeftCorner { get; }
        Position2D LowerLeftCorner { get; }
        Position2D UpperCenter { get; }
        Position2D LowerCenter { get; }
        Position2D RightCenter { get; }
        Position2D LeftCenter { get; }
        float Height { get; }
        float Width { get; }
        Position2D ConvertToWorldSpace(int x, int y);
        Position2D ConvertToWorldSpace(Vector2 p);

        Position2D GetPositionRelativeToCenter(Position2D position);

    }

}