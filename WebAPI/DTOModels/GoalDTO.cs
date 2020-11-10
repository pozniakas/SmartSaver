using System;

namespace WebAPI.DTOModels
{
    public class GoalDTO
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public DateTime? Deadlinedate { get; set; }
    }
}
