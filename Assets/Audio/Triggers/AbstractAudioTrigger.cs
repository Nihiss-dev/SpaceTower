using Assets.Source.Audio.Conrtrolleur;
using Assets.Source.Systems.Messaging;


namespace Assets.Source.Audio.Triggers {

    public abstract class AbstractAudioTrigger: JsAudioBaseComponent {

        protected abstract JsAudioSource source { get; }

        protected abstract JsAudioSound sound { get; }

        protected abstract MessageDispatcher MessageDispatcher { get; }





    }

}


