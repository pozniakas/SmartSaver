using System;
using System.Collections.Generic;

namespace DbEntities.Entities
{
    public class Category
    {
        public string Title { get; set; }
        public decimal? DedicatedAmount { get; set; }

        public long Id { get; set; }
        public User User { get; set; }
        public ICollection<Transaction> Transactions { get; set; }
    }
}
