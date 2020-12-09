using DbEntities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.DTOModels
{
    public static class ModelExtensions
    {
        public static Goal Update(this Goal goal, GoalDTO updatedGoal)
        {
            goal.Id = updatedGoal.Id;
            goal.Title = updatedGoal.Title;
            goal.Amount = updatedGoal.Amount;
            goal.Description = updatedGoal.Description;
            goal.Deadlinedate = updatedGoal.Deadlinedate;

            return goal;
        }

        public static Goal ToEntity(this GoalDTO goal)
        {
            return new Goal
            {
                Id = goal.Id,
                Title = goal.Title,
                Description = goal.Description,
                Amount = goal.Amount,
                Deadlinedate = goal.Deadlinedate,
                Creationdate = DateTime.UtcNow
            };
        }
    }
}
