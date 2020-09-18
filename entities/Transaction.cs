using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Text;

namespace SmartSaver.models
{
    class Transaction
    {
        private DateTime Date { get; set; }
        private decimal Sum { get; set; }
        private string Purpose { get; set; }

        public Transaction(DateTime date, decimal sum, string purpose)
        {
            Date = date;
            Sum = sum;
            Purpose = purpose;
        }
    }
}
