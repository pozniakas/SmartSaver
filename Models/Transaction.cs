using System;
using System.Collections.Generic;

namespace SmartSaver.Models
{
    public partial class Transaction
    {
        public long Id { get; set; }
        public DateTime? TrTime { get; set; }
        public decimal Amount { get; set; }
        public string Details { get; set; }
        public string CounterParty { get; set; }

       /* public Transaction(DateTime aDate, decimal aAmount, string aDetails, string aCounterParty)
        {
            this.TrTime = aDate;
            this.Amount = aAmount;
            this.Details = aDetails;
            this.CounterParty = aCounterParty;
        }

        public Transaction() { }*/
    }

}
