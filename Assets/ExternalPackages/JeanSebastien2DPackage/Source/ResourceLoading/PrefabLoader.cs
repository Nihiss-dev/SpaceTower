using UnityEngine;


namespace Assets.Source.ResourceLoading {

    public class PrefabLoader : AbstractResourceLoader<GameObject> {

        protected override string FileExtension { get { return ".prefab"; } }

    }

}