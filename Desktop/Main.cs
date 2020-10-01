using SmartSaver.Data;
using SmartSaver.Models;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace SmartSaver.Desktop
{
    public partial class Main : Form
    {
        Database db = new Database();
        List<Transaction> ProductList; 

        public Main()
        {
            InitializeComponent();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            PrepareTransactionListView();
            UpdateTransactionList();
        }

        private void PrepareTransactionListView()
        {
            listTransactions.View = View.Details;
            listTransactions.GridLines = true;

            listTransactions.Columns.Add("Date", 150);
            listTransactions.Columns.Add("Amount", 75);
            listTransactions.Columns.Add("Details", 300);
            listTransactions.Columns.Add("Counter Party", 200);
        }

        public void UpdateTransactionList()
        {
            ProductList = db.GetTransactions();
            PopulateTransactionListView();
        }

        private void PopulateTransactionListView()
        {
            listTransactions.Items.Clear();

            foreach (var transaction in ProductList)
            {
                var item = new ListViewItem(new string[] { 
                    ((DateTime) transaction.TrTime).ToString("yyyy-MM-dd HH:mm"),
                    transaction.Amount.ToString(),
                    transaction.Details,
                    transaction.CounterParty
                });

                listTransactions.Items.Add(item);
            }
        }
    }
}
