using System.Collections.Generic;
using System.IO;
using UnityEngine;


namespace Assets.Source.ResourceLoading {

    public abstract class AbstractResourceLoader<T> where T : Object {

        protected abstract string FileExtension { get; }

        public ICollection<T> Load(DirectoryPath directoryPath) {
            List<T> resources = new List<T>();

            DirectoryInfo dir = new DirectoryInfo(directoryPath.Value);
            FileInfo[] files = dir.GetFiles(FileExtension);

            foreach (FileInfo file in files) {
                T resource = Resources.Load<T>(directoryPath.Value + Path.GetFileNameWithoutExtension(file.Name));
                resources.Add(resource);
            }
            
            return resources;
        }

        public T Load(FilePath filePath) {
            T resource = Resources.Load<T>(filePath.Value);
            if (resource == null) throw new MissingResourceException(filePath.Value);
            return resource;
        }

    }

}