using System;

namespace DbEntities.Entities
{
    public class Transaction
    {
        public DateTime TrTime { get; set; }
        public decimal Amount { get; set; }
        public string Details { get; set; }
        public string CounterParty { get; set; }

        public long Id { get; set; }
        public Category Category { get; set; }
        public User User { get; set; }


        public bool IsValid()
        {
            return Amount != 0;
        }
    }
}
