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

namespace SmartSaver
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Database db = new Database();

            var random = new Random();
            var newTransaction = new Transaction();
            newTransaction.TrTime = new DateTime();
            newTransaction.Amount = random.Next(-100, 100);
            newTransaction.Details = "Maxima groceries";
            newTransaction.CounterParty = "UAB Maxima";
            db.AddTransaction(newTransaction);

            Debug.WriteLine("Transactions:");
            foreach (var tr in db.GetTransactions())
            {
                Debug.WriteLine("{0} | {1} | {2} | {3}", tr.Id, tr.Amount, tr.Details, tr.CounterParty);
            }
        }

        private void Btn_upload_statment(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "CSV Files (*.csv)|*.csv";

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                textBox1.AppendText(" | File: " + dialog.FileName);
                using var reader = new StreamReader(new FileStream(dialog.FileName, FileMode.Open), new UTF8Encoding());
                // For now ignoring header
                reader.ReadLine();
                using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);

                csv.Configuration.Delimiter = ";";
                csv.Configuration.HasHeaderRecord = false;
                //csv.Configuration.MissingFieldFound = null;

                transactions = csv.GetRecords<Transaction>().ToList();

                /*foreach (Transaction tr in transactions)
                    Debug.WriteLine(tr);*/
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
