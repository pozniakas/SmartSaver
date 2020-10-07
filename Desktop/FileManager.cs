using Microsoft.EntityFrameworkCore.Update;
using SmartSaver.Data;
using SmartSaver.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.AccessControl;
using System.Text;
using System.Windows.Forms;

namespace SmartSaver.Desktop
{
    class FileManager
    {
        public void Import()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            string filter = "CSV file (*.csv)|*.csv";
            openFileDialog.Filter = filter;
            StreamReader reader = null;
            string line = "";
            Database db = new Database();


            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                reader = new StreamReader(openFileDialog.FileName);
                line = reader.ReadLine();
                MessageBox.Show(line.Substring(0, 15));
                if (line == "\"Id\",\"Date\",\"Counter Party\",\"Details\",\"Amount\",")
                {
                    ReadExport(reader, db, line);
                }
                else if (line.Substring(0,15) == "\"Sąskaitos Nr.\"")
                {
                    ReadSwed(reader, db, line);
                }
                else if (line.Substring(0, 15) == "\"SĄSKAITOS  (LT")
                {
                    ReadSEB(reader, db, line);
                }
                else
                {
                    MessageBox.Show("Wrong file type");
                } 

                reader.Close();
            }
        }

        public void Export()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            string filter = "CSV file (*.csv)|*.csv| All Files (*.*)|*.*";
            saveFileDialog.Filter = filter;
            const string header = "\"Id\",\"Date\",\"Counter Party\",\"Details\",\"Amount\",";
            StreamWriter writer = null;

            Database db = new Database();
            List<Transaction> transactions = db.GetTransactions();

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                writer = new StreamWriter(saveFileDialog.FileName);

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
        
        private void ReadExport(StreamReader reader, Database db, string line)
        {
            while (line != null)
            {
                Transaction newTransaction = new Transaction();
                line = reader.ReadLine();
                if (line != null)
                {
                    string pattern = @"""\s*,\s*""";

                    // input.Substring(1, input.Length - 2) removes the first and last " from the string
                    string[] tokens = System.Text.RegularExpressions.Regex.Split(
                        line.Substring(1, line.Length - 2), pattern);

                    tokens[4] = tokens[4].Substring(0, tokens[4].Length - 1);
                    newTransaction.TrTime = DateTime.Parse(tokens[1]);
                    newTransaction.Amount = decimal.Parse(tokens[4]);
                    newTransaction.Details = tokens[3];
                    newTransaction.CounterParty = tokens[2];

                    db.AddTransaction(newTransaction);
                }
            }
        }

        private void ReadSwed(StreamReader reader, Database db, string line)
        {
            reader.ReadLine();
            while (line != null)
            {
                Transaction newTransaction = new Transaction();
                line = reader.ReadLine();
                if (line != null)
                {
                    string pattern = @"""\s*,\s*""";

                    // input.Substring(1, input.Length - 2) removes the first and last " from the string
                    string[] tokens = System.Text.RegularExpressions.Regex.Split(
                        line.Substring(1, line.Length - 2), pattern);

                    if (tokens[5] != "Apyvarta")
                    {
                        newTransaction.TrTime = DateTime.Parse(tokens[2]);
                        decimal d = decimal.Parse(tokens[6]);
                        if (tokens[8] == "D")
                            d = d * -1;
                        newTransaction.Amount = d;
                        newTransaction.Details = tokens[5];
                        newTransaction.CounterParty = tokens[3];

                        db.AddTransaction(newTransaction);
                    }
                    else break;
                }
            }
        }

        private void ReadSEB(StreamReader reader, Database db, string line)
        {
            line = reader.ReadLine();
            while (line != null)
            {
                Transaction newTransaction = new Transaction();
                line = reader.ReadLine();
                if (line != null)
                {
                    string pattern = @"\s*;\s*";

                    string[] tokens = System.Text.RegularExpressions.Regex.Split(
                        line, pattern);

                    MessageBox.Show(tokens[4].Substring(1, tokens[4].Length - 2));
                    newTransaction.TrTime = DateTime.Parse(tokens[1]);
                    decimal d = decimal.Parse(tokens[3]);
                    if (tokens[14] == "\"D\"")
                        d = d * -1;
                    newTransaction.Amount = d/100;
                    newTransaction.Details = tokens[9].Substring(1, tokens[9].Length - 2);
                    newTransaction.CounterParty = tokens[4].Substring(1, tokens[4].Length - 2);

                    db.AddTransaction(newTransaction);
                }
            }
        }
    }
}
