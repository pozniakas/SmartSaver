using System;
using System.Globalization;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using SmartSaver.Data;
using SmartSaver.Models;

namespace SmartSaver.Controllers
{
    internal class FileReader
    {
        public void Import()
        {
            var openFileDialog = new OpenFileDialog();
            const string filter = "CSV file (*.csv)|*.csv";
            openFileDialog.Filter = filter;

            if (openFileDialog.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            using var reader = new StreamReader(openFileDialog.FileName);
            var line = reader.ReadLine();
            if (line != null && line.Length > 12)
            {
                //Check the type of header
                switch (line.Substring(0, 12))
                {
                    case "\"Id\",\"Date\",":
                        ReadExport(reader, line);
                        break;
                    case "\"Sąskaitos N":
                        ReadSwed(reader);
                        break;
                    case "\"SĄSKAITOS  ":
                    case "SĄSKAITOS  (":
                    case "ACCOUNT  (LT":
                        var quotes = line[0] == '\"';
                        ReadSEB(reader, quotes);
                        break;
                    case "Operacijos/B":
                    case "Transaction ":
                        ReadLum(reader);
                        break;
                    default:
                        MessageBox.Show(@"Wrong file type");
                        break;
                }
            }
            else
            {
                MessageBox.Show(@"Wrong file type");
            }
        }

        private void ReadExport(TextReader reader, string line)
        {
            while (line != null)
            {
                line = reader.ReadLine();
                if (line != null)
                {
                    //Split line
                    const string pattern = @"""\s*,\s*""";
                    var transaction = Regex.Split(
                        line.Substring(1, line.Length - 2), pattern);

                    //Drop last extra symbol
                    transaction[4] = transaction[4].Substring(0, transaction[4].Length - 1);

                    var amount = decimal.Parse(transaction[4], CultureInfo.InvariantCulture);

                    AddTransaction(DateTime.Parse(transaction[1]), amount, transaction[2], transaction[3]);
                }
            }
        }

        private void ReadSwed(TextReader reader)
        {
            var line = reader.ReadLine();
            while (line != null)
            {
                line = reader.ReadLine();
                if (line != null)
                {
                    //Split line
                    const string pattern = @"""\s*,\s*""";
                    var transaction = Regex.Split(
                        line.Substring(1, line.Length - 2), pattern);

                    //Check if transaction list ended
                    if (transaction[5] != "Apyvarta")
                    {
                        var amount = decimal.Parse(transaction[6], CultureInfo.InvariantCulture);
                        amount = CheckIfDebit(transaction[8], amount);

                        AddTransaction(DateTime.Parse(transaction[2]), amount, transaction[3], transaction[5]);
                    }
                    else
                    {
                        break;
                    }
                }
            }
        }

        private void ReadSEB(TextReader reader, bool quotes)
        {
            var line = reader.ReadLine();
            while (line != null)
            {
                line = reader.ReadLine();
                if (line != null)
                {
                    string details;
                    string counterParty;

                    //Split line
                    const string pattern = @"\s*;\s*";
                    var transaction = Regex.Split(line, pattern);

                    var amount = decimal.Parse(transaction[3].Replace(',', '.'), CultureInfo.InvariantCulture);
                    if (quotes)
                    {
                        amount = CheckIfDebit(transaction[14].Substring(1, transaction[14].Length - 2), amount);

                        details = transaction[9].Substring(1, transaction[9].Length - 2);
                        counterParty = transaction[4].Substring(1, transaction[4].Length - 2);
                    }
                    else
                    {
                        amount = CheckIfDebit(transaction[14], amount);

                        details = transaction[9];
                        counterParty = transaction[4];
                    }

                    AddTransaction(DateTime.Parse(transaction[1]), amount, counterParty, details);
                }
            }
        }

        private void ReadLum(TextReader reader)
        {
            var line = reader.ReadLine();
            while (line != null)
            {
                line = reader.ReadLine();
                if (line != null)
                {
                    //Split line
                    const string pattern = @"\s*;\s*";
                    var transaction = Regex.Split(
                        line, pattern);

                    //Parse yyyymmdd;hhmmss format
                    var y = int.Parse(transaction[1].Substring(0, 4));
                    var m = int.Parse(transaction[1].Substring(4, 2));
                    var day = int.Parse(transaction[1].Substring(6, 2));
                    var h = int.Parse(transaction[2].Substring(0, 2));
                    var min = int.Parse(transaction[2].Substring(2, 2));
                    var s = int.Parse(transaction[2].Substring(4, 2));

                    var amount = decimal.Parse(transaction[3], CultureInfo.InvariantCulture);
                    amount = CheckIfDebit(transaction[5], amount);

                    AddTransaction(new DateTime(y, m, day, h, min, s),
                        amount, transaction[12], transaction[16]);
                }
            }
        }

        private decimal CheckIfDebit(string type, decimal d)
        {
            if (type == "D")
            {
                return -1 * d;
            }

            return d;
        }

        private void AddTransaction(DateTime time, decimal amount, string counterParty, string details = "")
        {
            var db = new Database();
            var newTransaction = new Transaction
            {
                TrTime = time,
                Amount = amount,
                CounterParty = counterParty,
                Details = details
            };
            db.AddTransaction(newTransaction);
        }
    }
}
