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
    }
}
