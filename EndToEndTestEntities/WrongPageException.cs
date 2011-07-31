namespace EndToEndTestEntities
{
    #region Using Directives

    using System;
    using System.Runtime.Serialization;

    #endregion

    [Serializable]
    public class WrongPageException : Exception
    {
        public WrongPageException()
        {
        }

        public WrongPageException(string message)
            : base(message)
        {
        }

        public WrongPageException(string message, Exception inner)
            : base(message, inner)
        {
        }

        protected WrongPageException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}