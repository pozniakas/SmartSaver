using System;

namespace SmartSaver.Models
{
    public class Goal
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public DateTime? Deadlinedate { get; set; }
        public DateTime Creationdate { get; set; }
    }
}
