﻿using System;

namespace SmartSaver.Models
{
    public partial class Transaction
    {
        public long Id { get; set; }
        public DateTime? TrTime { get; set; }
        public decimal Amount { get; set; }
        public string Details { get; set; }
        public string CounterParty { get; set; }
        public long? CategoryId { get; set; }

        public virtual Category Category { get; set; }
    }
}
