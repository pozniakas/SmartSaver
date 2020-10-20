using SmartSaver.Models;
using System.Collections.Generic;

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

        public IEnumerable<Transaction> GetTransactions()
        {
            using var db = new postgresContext();
            return db.Transaction;
        }

        public int AddGoal(Goal goal)
        {
            using var db = new postgresContext();
            db.Goal.Add(goal);
            return db.SaveChanges();
        }

        public IEnumerable<Goal> GetGoals()
        {
            using var db = new postgresContext();
            return db.Goal;
        }
    }
}
