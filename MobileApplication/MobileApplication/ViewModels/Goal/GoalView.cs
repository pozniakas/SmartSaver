using DbEntities.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MobileApplication.ViewModels
{
    public class GoalView : Goal
    {
            public GoalView(Goal goal)
            {
                Title = goal.Title;
                Description = goal.Description;
                Id = goal.Id;
                User = goal.User;
                Amount = goal.Amount;
                Deadlinedate = goal.Deadlinedate;
                Creationdate = goal.Creationdate;
            }

            public string Possibility { get; set; }
            public decimal? MonthlyGoalAmount { get; set; }
    }
}
