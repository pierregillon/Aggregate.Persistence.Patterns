using System;

namespace Patterns.Common.Domain
{
    public class OrderOperationException : Exception
    {
        public OrderOperationException(string message):base(message)
        {
        }
    }
}