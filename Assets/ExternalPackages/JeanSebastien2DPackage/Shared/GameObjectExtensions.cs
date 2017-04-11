

using System.Collections.Generic;
using UnityEngine;


namespace Assets.Shared
{

    public static class GameObjectExtensions
    {
        public static T[] GetComponentsInChildrenReel<T>(this T gObject) where T : Component
        {
            T[] comp = gObject.GetComponentsInChildren<T>();
            List<T> list = new List<T>();
            foreach (var c in comp)
            {
                if (c != gObject.transform)
                {
                    list.Add(c);
                }
            }
            comp = list.ToArray();
            return comp;
        }
    }
}
