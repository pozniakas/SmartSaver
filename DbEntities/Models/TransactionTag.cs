using System;
using System.Collections.Generic;

namespace Models
{
    public partial class TransactionTag
    {
        public long TransactionId { get; set; }
        public long TagId { get; set; }

        public virtual Tag Tag { get; set; }
        public virtual Transaction Transaction { get; set; }
    }
}
