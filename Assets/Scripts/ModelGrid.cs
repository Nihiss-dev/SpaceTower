using UnityEngine;
using GridFramework.Grids;
using GridFramework.Renderers.Rectangular;

namespace SpaceTowers
{
    [RequireComponent(typeof(RectGrid))]
    [RequireComponent(typeof(Parallelepiped))]
    public class ModelGrid : MonoBehaviour
    {
        // Awake is called before Start()
        void Awake()
        {
            var grid = GetComponent<RectGrid>();
            var para = GetComponent<Parallelepiped>();

            GridManagement.Initialize(grid, para);
        }
    }
}
