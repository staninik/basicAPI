using System;

namespace BasicAPI.Exceptions
{
    public class NotUniqueException : Exception
    {
        public NotUniqueException()
        {

        }

        public NotUniqueException(string message)
            : base(message)
        {

        }
    }
}