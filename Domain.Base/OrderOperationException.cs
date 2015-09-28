using System;

namespace Domain.Base
{
    public class OrderOperationException : Exception
    {
        public OrderOperationException(string message):base(message)
        {
        }
    }
}