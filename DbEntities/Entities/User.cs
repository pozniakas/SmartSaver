using System;
using System.Collections.Generic;
using System.Text;

namespace DbEntities.Entities
{
    public class User
    {
        public long Id { get; set; }
        public string Name { get; set; }

        public ICollection<Transaction> Transactions { get; set; }
        public ICollection<Category> Categories { get; set; }
        public ICollection<Goal> Goals { get; set; }
    }
}
