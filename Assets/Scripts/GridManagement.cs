using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GridFramework.Grids;
using GridFramework.Renderers.Rectangular;
using GridFramework.Extensions.Nearest;
using GridFramework.Extensions.Align;

using CoordinateSystem = GridFramework.Grids.RectGrid.CoordinateSystem;

namespace SpaceTowers
{
    public static class GridManagement
    {
        #region  Public variables
        // The grid everything is based on
        public static RectGrid _grid;

        // The renderer of the grid
        public static Parallelepiped _renderer;

        // The grid coordinates of the lower left square used for reference
        // (X and Y only)
        public static int[] _originSquare;
        #endregion

        #region  Public methods
        //  Builds the matrix and sets everything up, gets called by a script
        // attached to the grid object.
        public static void Initialize(RectGrid grid, Parallelepiped renderer)
        {
            _grid = grid;
            _renderer = renderer;

            // Builds a default matrix that has all entries set to default.
            //BuildMatrix();

            // Stores the X and Y grid coordinates of the lower left square.
            SetOriginSquare();
        }

        public static void AlignTransform(Transform transformToAlign)
        {
            _grid.AlignTransform(transformToAlign);
        }

        public static Vector3 NearestGridPos(Vector3 posToFind)
        {
            Vector3 nearestCellPos = _grid.NearestCell(posToFind, CoordinateSystem.World);
            return _grid.GridToWorld(nearestCellPos);
        }

        public static Vector2 GetGridUnit()
        {
            return new Vector2(_grid.Spacing.x, _grid.Spacing.y);
        }
        #endregion // public methods

        #region Private methods

        /// Stores the grid coodinates of the lower left square.
        private static void SetOriginSquare()
        {
            var from = _renderer.From;

            _originSquare = new[] {
                Mathf.RoundToInt(from.x),
                Mathf.RoundToInt(from.z)
            };
        }
        #endregion // private methods
    }
}