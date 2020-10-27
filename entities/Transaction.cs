using System;
using CsvHelper.Configuration.Attributes;

namespace SmartSaver.entities
{
    internal class Transaction
    {
        public Transaction(DateTime aDate, decimal aAmount, string aDetails, string aCounterParty)
        {
            Date = aDate;
            Amount = aAmount;
            Details = aDetails;
            CounterParty = aCounterParty;
        }

        [Index(0)] public DateTime Date { get; set; }

        [Index(1)] public decimal Amount { get; set; }

        [Index(2)] public string Details { get; set; }

        [Index(3)] public string CounterParty { get; set; }

        override
            public string ToString()
        {
            return Date.ToShortDateString() + "; "
                                            + Amount + "; "
                                            + Details + "; "
                                            + CounterParty;
        }
    }
}
