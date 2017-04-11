namespace Assets.Source.Audio.Conrtrolleur
{
    public interface IJSAudioSource
    {
        bool IsPlaying { get; }
        float Panning { get; }
        float Pitch { get; }
        float Volume { get; }
        JsAudioSource GetAudioSource();
        void Play(JsAudioSound sound, string eventAction);
        void Stop();

    }
}