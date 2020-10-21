using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;
using SmartSaver.Data;
using SmartSaver.Models;

namespace SmartSaver.Controllers
{
    class FileWriter
    {
        public void Export()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            string filter = "CSV file (*.csv)|*.csv| All Files (*.*)|*.*";
            saveFileDialog.Filter = filter;
            const string header = "\"Id\",\"Date\",\"Counter Party\",\"Details\",\"Amount\",";

            Database db = new Database();
            List<Transaction> transactions = db.GetTransactions();

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                StreamWriter writer = new StreamWriter(saveFileDialog.FileName);

                writer.WriteLine(header);
                foreach (Transaction transaction in transactions)
                {
                    DateTime d = (System.DateTime)transaction.TrTime;
                    writer.WriteLine("\"" + transaction.Id.ToString() + "\",\"" + d.ToString("yyyy-MM-dd") + "\",\"" + transaction.CounterParty + "\",\"" +
                        transaction.Details + "\",\"" + transaction.Amount + "\",");
                }

                writer.Close();
            }
        }
    }
}
