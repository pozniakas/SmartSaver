using Microsoft.EntityFrameworkCore.Update;
using SmartSaver.Data;
using SmartSaver.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.AccessControl;
using System.Text;
using System.Windows.Forms;

namespace SmartSaver.Controllers
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
                if (line == "\"Id\",\"Date\",\"Counter Party\",\"Details\",\"Amount\",")
                {
                    ReadExport(reader, db, line);
                }
                else if (line.Substring(0,15) == "\"Sąskaitos Nr.\"")
                {
                    ReadSwed(reader, db, line);
                }
                else if (line.Substring(0, 15) == "\"SĄSKAITOS  (LT" || line.Substring(0, 12) == "ACCOUNT  (LT" || line.Substring(0,14) == "SĄSKAITOS  (LT")
                {
                    bool quotes = false;
                    if(line[0] == '\"')
                    {
                        quotes = true;
                    }
                    ReadSEB(reader, db, line, quotes);
                }
                else if (line.Substring(0, 24) == "Operacijos/Balanso Tipas" || line.Substring(0, 16) == "Transaction type")
                {
                    ReadLum(reader, db, line);
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

        private void ReadSEB(StreamReader reader, Database db, string line, bool quotes)
        {
            reader.ReadLine();
            while (line != null)
            {
                Transaction newTransaction = new Transaction();
                line = reader.ReadLine();
                if (line != null)
                {
                    string pattern = @"\s*;\s*";

                    string[] tokens = System.Text.RegularExpressions.Regex.Split(
                        line, pattern);

                    newTransaction.TrTime = DateTime.Parse(tokens[1]);
                    decimal d = decimal.Parse(tokens[3]);
                    if (quotes)
                    {
                        if (tokens[14] == "\"D\"")
                            d = d * -1;
                        newTransaction.Amount = d/100;
                        newTransaction.Details = tokens[9].Substring(1, tokens[9].Length - 2);
                        newTransaction.CounterParty = tokens[4].Substring(1, tokens[4].Length - 2);

                        db.AddTransaction(newTransaction);
                    }
                    else
                    {
                        if (tokens[14] == "D")
                            d = d * -1;
                        newTransaction.Amount = d / 100;
                        newTransaction.Details = tokens[9];
                        newTransaction.CounterParty = tokens[4];

                        db.AddTransaction(newTransaction);
                    }
                    
                }
            }
        }
        private void ReadLum(StreamReader reader, Database db, string line)
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

                    int y = Int32.Parse(tokens[1].Substring(0, 4));
                    int m = Int32.Parse(tokens[1].Substring(4, 2));
                    int day = Int32.Parse(tokens[1].Substring(6, 2));
                    int h = Int32.Parse(tokens[2].Substring(0, 2));
                    int min = Int32.Parse(tokens[2].Substring(2, 2));
                    int s = Int32.Parse(tokens[2].Substring(4, 2));

                    newTransaction.TrTime = new DateTime(y,m,day,h,min,s);
                    decimal d = decimal.Parse(tokens[3]);
                    if (tokens[5] == "D")
                        d = d * -1;
                    newTransaction.Amount = d;
                    newTransaction.Details = tokens[12];
                    newTransaction.CounterParty = tokens[16];

                    db.AddTransaction(newTransaction);
                }
            }
        }
    }
}
