using System;

namespace Common.Domain
{
    public class OrderOperationException : Exception
    {
        public OrderOperationException(string message) : base(message) { }
    }
}