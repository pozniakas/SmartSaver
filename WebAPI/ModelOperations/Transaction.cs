using Serilog;
using System;
using System.Collections.Generic;

namespace WebAPI.Models
{
    public partial class Transaction
    {
        public bool IsValid()
        {
            return Amount != 0 && TrTime != null;
        }
    }
}
