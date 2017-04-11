using Assets.Shared;
using UnityEngine;


namespace Assets.Utils
{
    public class Pivot: BaseComponent
    {
        public Transform PivoTransform { get { return GetComponent<Transform>(); } }
    }
}
