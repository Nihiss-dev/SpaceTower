using System.Collections;
using UnityEngine;


namespace Assets.Audio.Controller
{

    public  class AbstractAudioEvent 
    {
        protected float timeLimit = 0.0f;
        protected bool eventComplete = false;

        protected AbstractAudioEvent()
        {

        }

        public virtual void EndEvent()
        {

        }

        public virtual IEnumerator TrackEvent()
        {
            yield return new WaitForEndOfFrame();
        }
    }
}
