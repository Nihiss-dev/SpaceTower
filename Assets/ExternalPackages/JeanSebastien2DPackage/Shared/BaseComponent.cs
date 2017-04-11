using UnityEngine;


namespace Assets.Shared {

    public abstract class BaseComponent : MonoBehaviour {

        private GameObject scene;

        public new  T GetComponent<T>() {
            T comp = base.GetComponent<T>();
            CheckNotNull<T>(comp);
            return comp;
        }

        public new T GetComponentInChildren<T>() {
            T comp = base.GetComponentInChildren<T>();
            CheckNotNull<T>(comp);
            return comp;
        }
        

        public new T GetComponentInParent<T>() {
            T comp = base.GetComponentInParent<T>();
            CheckNotNull<T>(comp);
            return comp;
        }

        public bool HasComponent<T>() {
            return base.GetComponent<T>() != null;
        }

        protected T GetComponentInScene<T>() {
            if(scene == null) {
                scene = GameObject.FindWithTag("Root");
                if (scene == null) throw new MissingTaggedObjectException("Root");
            }

            var comp = scene.GetComponentInChildren<T>();
            CheckNotNull<T>(comp);
            return comp;
        }

        protected T[] GetComponentsInScene<T>() {
            var comp = scene.GetComponentsInChildren<T>();
            return comp;
        }

        private void CheckNotNull<T>(object component) {
            if (component == null) throw new MissingComponentException(typeof(T).Name);
        }
        
    }

}