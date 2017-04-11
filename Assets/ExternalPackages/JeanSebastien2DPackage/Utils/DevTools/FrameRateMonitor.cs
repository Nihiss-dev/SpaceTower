#pragma warning disable 0414
using Assets.Shared;
using Assets.Source.Utils;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;


namespace Assets.Source.DevTools {

    public class FrameRateMonitor : BaseComponent {

        [SerializeField] private float value;
        private CircularBuffer buffer = new CircularBuffer();

        private Text text;
        
        protected void Awake() {
            text = GetComponent<Text>();
        }

        [UsedImplicitly]
        private void Update() {
            buffer.Put(1/Time.deltaTime);
            text.text = "fps: " + ((int) buffer.AvgValue);
            value = buffer.AvgValue;
        }

    }

}