using System;

namespace Patterns.Contract.Domain
{
    public class OrderOperationException : Exception
    {
        public OrderOperationException(string message):base(message)
        {
        }
    }
}