using UnityEngine;


namespace Assets.Source.ResourceLoading {

    public class SpriteLoader : AbstractResourceLoader<Sprite> {

        protected override string FileExtension { get { return ".png"; } }

    }

}