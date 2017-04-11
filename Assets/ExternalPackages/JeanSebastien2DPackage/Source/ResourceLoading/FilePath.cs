using System;
using UnityEngine;


namespace Assets.Source.ResourceLoading {

    [Serializable]
    public class FilePath {

        [SerializeField] private string value;

        public FilePath(string value) {
            this.value = value;
        }

        public string Value { get { return value; } }

    }

}