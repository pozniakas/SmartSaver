using SmartSaver.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace SmartSaver.Data
{
    public class Database
    {
        // Returns:
        //     The number of state entries written to the database.
        public int AddTransaction(Transaction transaction)
        {
            using var db = new postgresContext();
            db.Transaction.Add(transaction);
            return db.SaveChanges();
        }

        public List<Transaction> GetTransactions()
        {
            using var db = new postgresContext();
            List<Transaction> transactions = new List<Transaction>();
            transactions.AddRange(db.Transaction);

            return transactions;
        }
        public int AddGoal(Goal goal)
        {
            using var db = new postgresContext();
            db.Goal.Add(goal);
            return db.SaveChanges();
        }

        public List<Goal> GetGoals()
        {
            using var db = new postgresContext();
            List<Goal> goal = new List<Goal>();
            goal.AddRange(db.Goal);

            return goal;
        }
    }
}
