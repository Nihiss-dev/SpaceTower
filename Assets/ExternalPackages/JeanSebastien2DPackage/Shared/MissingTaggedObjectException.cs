using System;


namespace Assets.Shared {

    public class MissingTaggedObjectException : Exception {

        public MissingTaggedObjectException(string tag) : base(tag) {}

    }

}