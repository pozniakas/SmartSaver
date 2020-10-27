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
                if (line != null && !line.Equals(""))
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
                            var quotes = false;
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
                            MessageBox.Show(@"Wrong file type");
                            break;
                    }
                }

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

                    // input.Substring(1, input.Length - 2) removes the first and last " from the string
                    var tokens = Regex.Split(
                        line.Substring(1, line.Length - 2), pattern);

                    tokens[4] = tokens[4].Substring(0, tokens[4].Length - 1);

                    AddTransaction(DateTime.Parse(tokens[1]), decimal.Parse(tokens[4], CultureInfo.InvariantCulture), tokens[2], tokens[3]);
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

                    // input.Substring(1, input.Length - 2) removes the first and last " from the string
                    var tokens = Regex.Split(
                        line.Substring(1, line.Length - 2), pattern);

                    if (tokens[5] != "Apyvarta")
                    {
                        var d = decimal.Parse(tokens[6], CultureInfo.InvariantCulture);
                        if (tokens[8] == "D")
                        {
                            d = d * -1;
                        }

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
                    var pattern = @"\s*;\s*";

                    var tokens = Regex.Split(
                        line, pattern);
                    var d = decimal.Parse(tokens[3].Replace(',','.'), CultureInfo.InvariantCulture);
                    if (quotes)
                    {
                        if (tokens[14] == "\"D\"")
                        {
                            d = d * -1;
                        }

                        var details = tokens[9].Substring(1, tokens[9].Length - 2);
                        var counterParty = tokens[4].Substring(1, tokens[4].Length - 2);

                        AddTransaction(DateTime.Parse(tokens[1]), d, counterParty, details);
                    }
                    else
                    {
                        if (tokens[14] == "D")
                        {
                            d = d * -1;
                        }

                        var details = tokens[9];
                        var counterParty = tokens[4];

                        AddTransaction(DateTime.Parse(tokens[1]), d, counterParty, details);
                    }
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
                    if (tokens[5] == "D")
                    {
                        d = d * -1;
                    }

                    AddTransaction(new DateTime(y, m, day, h, min, s), d, tokens[12], tokens[16]);
                }
            }
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
