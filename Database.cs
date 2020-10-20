using SmartSaver.Models;
using System.Collections.Generic;
using System.Linq;

namespace SmartSaver.Data
{
    public class Database
    {
        /// <returns>
        ///     The number of state entries written to the database.
        /// </returns>
        public int AddTransaction(Transaction transaction)
        {
            using var db = new postgresContext();
            db.Transaction.Add(transaction);
            return db.SaveChanges();
        }

        public IList<Transaction> GetTransactions()
        {
            using var db = new postgresContext();
            return db.Transaction.ToList();
        }

        /// <returns>
        ///     The number of state entries written to the database.
        /// </returns>
        public int AddGoal(Goal goal)
        {
            using var db = new postgresContext();
            db.Goal.Add(goal);
            return db.SaveChanges();
        }

        public IList<Goal> GetGoals()
        {
            using var db = new postgresContext();
            return db.Goal.ToList();
        }
    }
}
