using System;
using UnityEngine;


namespace Assets.Source.ResourceLoading {

    [Serializable]
    public class DirectoryPath {

        [SerializeField] private string value;

        public DirectoryPath(string value) {
            this.value = value;
        }

        public string Value { get { return value; } }

    }

}