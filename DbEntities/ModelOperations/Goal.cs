using System;
using System.Collections.Generic;

namespace DbEntities.Models
{
    public partial class Goal
    {
        public bool IsValid()
        {
            return Amount > 0 && Creationdate != null;
        }
    }
}
