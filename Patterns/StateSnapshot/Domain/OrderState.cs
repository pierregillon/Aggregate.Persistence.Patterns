﻿using System;
using System.Collections.Generic;
using Patterns.Common;
using Patterns.Common.Domain;

namespace Patterns.StateSnapshot.Domain
{
    public class OrderState
    {
        public Guid Id { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public DateTime? SubmitDate { get; set; }
        public double TotalCost { get; set; }
        public List<OrderLineState> Lines { get; set; }

        public OrderState()
        {
            Lines = new List<OrderLineState>();
        }
    }
}