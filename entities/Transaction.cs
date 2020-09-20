using CsvHelper.Configuration.Attributes;
using System;

namespace SmartSaver.entities
{
    class Transaction
    {
        [Index(0)]
        public DateTime Date { get; set; }
        [Index(1)]
        public decimal Amount { get; set; }
        [Index(2)]
        public string Details { get; set; }
        [Index(3)]
        public string CounterParty { get; set; }

        override
        public string ToString() => 
            Date.ToShortDateString() + "; " 
            + Amount + "; " 
            + Details + "; " 
            + CounterParty;
    }
}
