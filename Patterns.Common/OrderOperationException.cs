using System;

namespace Patterns.Common
{
    public class OrderOperationException : Exception
    {
        public OrderOperationException(string message):base(message)
        {
        }
    }
}