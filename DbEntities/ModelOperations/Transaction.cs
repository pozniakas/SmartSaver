using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace DbEntities.Models
{
    public partial class Transaction : IComparable<Transaction>, IEquatable<Transaction>
    {
        public int CompareTo(Transaction other)
        {
            if (TrTime > other.TrTime)
                return 1;

            if (TrTime < other.TrTime)
                return -1;

            else
                return 0;
        }

        public bool Equals(Transaction other)
        {
            return TrTime == other.TrTime && Amount == other.Amount;
        }

        public bool IsValid()
        {
            return Amount != 0 && TrTime != null;
        }
    }
}
