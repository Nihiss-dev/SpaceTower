using UnityEngine.Audio;


namespace Assets.Source.ResourceLoading {

    public class AudioMixerLoader : AbstractResourceLoader<AudioMixer> {

        protected override string FileExtension { get { return ".mixer"; } }

    }

}