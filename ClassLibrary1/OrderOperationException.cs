using System;

namespace ClassLibrary1
{
    public class OrderOperationException : Exception
    {
        public OrderOperationException(string message):base(message)
        {
        }
    }
}