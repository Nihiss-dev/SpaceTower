using System;


namespace Assets.Source.ResourceLoading {

    public class MissingResourceException : Exception {

        public MissingResourceException(string message) : base(message) {}

    }

}