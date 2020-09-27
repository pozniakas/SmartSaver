using CsvHelper;
using SmartSaver.entities;
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
        private List<Transaction> transactions;

        public Form1()
        {
            InitializeComponent();
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

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
