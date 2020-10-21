using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using SmartSaver.Data;
using SmartSaver.Models;
using System.Windows.Forms;

namespace SmartSaver.Controllers
{
    class FileReader
    {
        public void Import()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            string filter = "CSV file (*.csv)|*.csv";
            openFileDialog.Filter = filter;
            StreamReader reader = null;
            string line = "";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                reader = new StreamReader(openFileDialog.FileName);
                line = reader.ReadLine();
                switch (line.Substring(0, 12))
                {
                    case "\"Id\",\"Date\",":
                        ReadExport(reader, line);
                        break;
                    case "\"Sąskaitos N":
                        ReadSwed(reader);
                        break;
                    case "\"SĄSKAITOS  ":
                    case "ACCOUNT  (LT":
                        bool quotes = false;
                        if (line[0] == '\"')
                        {
                            quotes = true;
                        }
                        ReadSEB(reader, quotes);
                        break;
                    case "Operacijos/B":
                    case "Transaction ":
                        ReadLum(reader);
                        break;
                    default:
                        MessageBox.Show("Wrong file type");
                        break;
                }
                reader.Close();
            }
        }

        private void ReadExport(StreamReader reader, string line)
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

                    AddTransaction(DateTime.Parse(tokens[1]), decimal.Parse(tokens[4]), tokens[2], tokens[3]);
                }
            }
        }

        private void ReadSwed(StreamReader reader)
        {
            string line = reader.ReadLine();
            while (line != null)
            {
                line = reader.ReadLine();
                if (line != null)
                {
                    string pattern = @"""\s*,\s*""";

                    // input.Substring(1, input.Length - 2) removes the first and last " from the string
                    string[] tokens = System.Text.RegularExpressions.Regex.Split(
                        line.Substring(1, line.Length - 2), pattern);

                    if (tokens[5] != "Apyvarta")
                    {
                        decimal d = decimal.Parse(tokens[6]);
                        if (tokens[8] == "D")
                            d = d * -1;

                        AddTransaction(DateTime.Parse(tokens[2]), d, tokens[3], tokens[5]);
                    }
                    else break;
                }
            }
        }

        private void ReadSEB(StreamReader reader, bool quotes)
        {
            string line = reader.ReadLine();
            while (line != null)
            {
                line = reader.ReadLine();
                if (line != null)
                {
                    string pattern = @"\s*;\s*";

                    string[] tokens = System.Text.RegularExpressions.Regex.Split(
                        line, pattern);

                    decimal d = decimal.Parse(tokens[3]);
                    if (quotes)
                    {
                        if (tokens[14] == "\"D\"")
                            d = d * -1;
                        string details = tokens[9].Substring(1, tokens[9].Length - 2);
                        string counterParty = tokens[4].Substring(1, tokens[4].Length - 2);

                        AddTransaction(DateTime.Parse(tokens[1]), d / 100, counterParty, details);
                    }
                    else
                    {
                        if (tokens[14] == "D")
                            d = d * -1;
                        string details = tokens[9];
                        string counterParty = tokens[4];

                        AddTransaction(DateTime.Parse(tokens[1]), d / 100, counterParty, details);
                    }

                }
            }
        }
        private void ReadLum(StreamReader reader)
        {
            string line = reader.ReadLine();
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

                    decimal d = decimal.Parse(tokens[3]);
                    if (tokens[5] == "D")
                        d = d * -1;
                    AddTransaction(new DateTime(y, m, day, h, min, s), d, tokens[12], tokens[16]);
                    
                }
            }
        }

        private void AddTransaction(DateTime time, decimal amount, string counterParty, string details)
        {
            Database db = new Database();
            Transaction newTransaction = new Transaction();
            newTransaction.TrTime = time;
            newTransaction.Amount = amount;
            newTransaction.CounterParty = counterParty;
            newTransaction.Details = details;
            db.AddTransaction(newTransaction);
        }
    }
}
