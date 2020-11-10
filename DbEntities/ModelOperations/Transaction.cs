using System;
using System.Collections.Generic;

namespace DbEntities.Models
{
    public partial class Transaction
    {
        public bool IsValid()
        {
            return Amount != 0 && TrTime != null;
        }
    }
}
