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
            var filter = "CSV file (*.csv)|*.csv";
            openFileDialog.Filter = filter;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                var reader = new StreamReader(openFileDialog.FileName);
                var line = reader.ReadLine();
                if (line != null && line.Length>12)
                {
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
                            bool quotes = line[0] == '\"';
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
                else MessageBox.Show(@"Wrong file type");
                reader.Close();
            }
        }

        private void ReadExport(StreamReader reader, string line)
        {
            while (line != null)
            {
                line = reader.ReadLine();
                if (line != null)
                {
                    var pattern = @"""\s*,\s*""";
                    var tokens = Regex.Split(
                        line.Substring(1, line.Length - 2), pattern);

                    tokens[4] = tokens[4].Substring(0, tokens[4].Length - 1);

                    AddTransaction(DateTime.Parse(tokens[1]), decimal.Parse(tokens[4], CultureInfo.InvariantCulture),
                        tokens[2], tokens[3]);
                }
            }
        }

        private void ReadSwed(StreamReader reader)
        {
            var line = reader.ReadLine();
            while (line != null)
            {
                line = reader.ReadLine();
                if (line != null)
                {
                    var pattern = @"""\s*,\s*""";
                    var tokens = Regex.Split(
                        line.Substring(1, line.Length - 2), pattern);

                    if (tokens[5] != "Apyvarta")
                    {
                        var d = decimal.Parse(tokens[6], CultureInfo.InvariantCulture);
                        d = CheckIfDebit(tokens[8], d);

                        AddTransaction(DateTime.Parse(tokens[2]), d, tokens[3], tokens[5]);
                    }
                    else
                    {
                        break;
                    }
                }
            }
        }

        private void ReadSEB(StreamReader reader, bool quotes)
        {
            var line = reader.ReadLine();
            while (line != null)
            {
                line = reader.ReadLine();
                if (line != null)
                {
                    string details;
                    string counterParty;

                    var pattern = @"\s*;\s*";
                    var tokens = Regex.Split(line, pattern);

                    var d = decimal.Parse(tokens[3].Replace(',','.'), CultureInfo.InvariantCulture);
                    if (quotes)
                    {
                        d = CheckIfDebit(tokens[14].Substring(1, tokens[14].Length - 2), d);
                        
                        details = tokens[9].Substring(1, tokens[9].Length - 2);
                        counterParty = tokens[4].Substring(1, tokens[4].Length - 2);
                    }
                    else
                    {
                        d = CheckIfDebit(tokens[14], d);

                        details = tokens[9];
                        counterParty = tokens[4];
                    }
                    AddTransaction(DateTime.Parse(tokens[1]), d, counterParty, details);
                }
            }
        }

        private void ReadLum(StreamReader reader)
        {
            var line = reader.ReadLine();
            while (line != null)
            {
                line = reader.ReadLine();
                if (line != null)
                {
                    var pattern = @"\s*;\s*";
                    var tokens = Regex.Split(
                        line, pattern);

                    var y = Int32.Parse(tokens[1].Substring(0, 4));
                    var m = Int32.Parse(tokens[1].Substring(4, 2));
                    var day = Int32.Parse(tokens[1].Substring(6, 2));
                    var h = Int32.Parse(tokens[2].Substring(0, 2));
                    var min = Int32.Parse(tokens[2].Substring(2, 2));
                    var s = Int32.Parse(tokens[2].Substring(4, 2));

                    var d = decimal.Parse(tokens[3], CultureInfo.InvariantCulture);
                    d = CheckIfDebit(tokens[5], d);

                    AddTransaction(new DateTime(y, m, day, h, min, s),
                        d, tokens[12], tokens[16]);
                }
            }
        }

        private decimal CheckIfDebit(string type, decimal d)
        {
            if (type == "D")
            {
                return d *= -1;
            }
            return d;
        }

        private void AddTransaction(DateTime time, decimal amount, string counterParty, string details)
        {
            var db = new Database();
            var newTransaction = new Transaction
            {
                TrTime = time, Amount = amount, CounterParty = counterParty, Details = details
            };
            db.AddTransaction(newTransaction);
        }
    }
}
