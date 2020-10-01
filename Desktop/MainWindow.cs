using CsvHelper;
using SmartSaver.Models;
using SmartSaver.Data;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.CompilerServices;

namespace SmartSaver
{
    public partial class MainWindow : Form
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Database db = new Database();

            var random = new Random();
            var newTransaction = new Transaction();
            newTransaction.TrTime = DateTime.UtcNow;
            newTransaction.Amount = random.Next(-100, 100);
            newTransaction.Details = "Maxima groceries";
            newTransaction.CounterParty = "UAB Maxima";
            db.AddTransaction(newTransaction);

            Debug.WriteLine("Transactions:");
            foreach (var tr in db.GetTransactions())
            {
                Debug.WriteLine("{0} | {1} | {2} | {3} | {4}", tr.Id, tr.TrTime, tr.Amount, tr.Details, tr.CounterParty);
            }

            DrawTable(db.GetTransactions());
        }

        public void DrawTable(List<Transaction> transactions)
        {
            tableLayoutPanel1.AutoScroll = true;
            tableLayoutPanel1.Dock = DockStyle.Fill;

            // Add rows + data
            for (int rowIndex = 0; rowIndex < transactions.Count; rowIndex++)
            {
                AddRow(transactions[rowIndex], rowIndex);
            }

            //for (int x = 0; x < tableLayoutPanel1.ColumnCount; x++)
            //{
            //    //First add a column
            //    tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));

            //    for (int y = 0; y < tableLayoutPanel1.RowCount; y++)
            //    {
            //        //Next, add a row.  Only do this when once, when creating the first column
            //        if (x == 0)
            //        {
            //            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            //        }

            //        //Create the control, in this case we will add a button
            //        Label cell = new Label();
            //        cell.Text = transactions[x].Amount.ToString();
            //        tableLayoutPanel1.Controls.Add(cell, x, y);
            //    }
            //}
        }

        private void AddRow(Transaction transaction, int rowIndex)
        {
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));

            tableLayoutPanel1.GrowStyle = TableLayoutPanelGrowStyle.AddRows;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tableLayoutPanel1.SuspendLayout();
            tableLayoutPanel1.RowCount += 1;

            Label details = new Label() { Text = transaction.Details };
            tableLayoutPanel1.Controls.Add(details, 0, rowIndex);

            Label amount = new Label() { Text = transaction.Amount.ToString() };
            tableLayoutPanel1.Controls.Add(amount, 0, rowIndex);

            Label date = new Label() { Text = transaction.TrTime.ToString() };
            tableLayoutPanel1.Controls.Add(date, 0, rowIndex);

            tableLayoutPanel1.ResumeLayout();
        }

        private void Btn_upload_statment(object sender, EventArgs e)
        {
            //OpenFileDialog dialog = new OpenFileDialog();
            //dialog.Filter = "CSV Files (*.csv)|*.csv";

            //if (dialog.ShowDialog() == DialogResult.OK)
            //{
            //    textBox1.AppendText(" | File: " + dialog.FileName);
            //    using var reader = new StreamReader(new FileStream(dialog.FileName, FileMode.Open), new UTF8Encoding());
            //    // For now ignoring header
            //    reader.ReadLine();
            //    using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);

            //    csv.Configuration.Delimiter = ";";
            //    csv.Configuration.HasHeaderRecord = false;
            //    //csv.Configuration.MissingFieldFound = null;

            //    transactions = csv.GetRecords<Transaction>().ToList();

            //    /*foreach (Transaction tr in transactions)
            //        Debug.WriteLine(tr);*/
            //}
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            string filter = "CSV file (*.csv)|*.csv| All Files (*.*)|*.*";
            saveFileDialog.Filter = filter;
            const string header = "Id,Date,Counter Party,Details,Amount";
            StreamWriter writer = null;

            Database db = new Database();
            List<Transaction> transactions = db.GetTransactions();

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                writer = new StreamWriter(saveFileDialog.FileName);

                writer.WriteLine(header);
                foreach(Transaction transaction in transactions)
                {
                    writer.WriteLine(transaction.Id.ToString()+","+transaction.TrTime + "," + transaction.CounterParty + "," + 
                        transaction.Details + "," + transaction.Amount);
                }

                writer.Close();
            }
        }

        private void btnUpload_Click(object sender, EventArgs e)
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
                reader.ReadLine();
                reader.ReadLine();
                while(line != null)
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
                reader.Close();
                DrawTable(db.GetTransactions());
            }

        }

    }
}
