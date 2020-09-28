using System;
using System.Collections.Generic;

namespace SmartSaver.Models
{
    public partial class Goal
    {
        public long Id { get; set; }
        public DateTime? GoalDate { get; set; }
        public decimal Amount { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
