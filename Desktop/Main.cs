using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using SmartSaver.Controllers;
using SmartSaver.Data;
using SmartSaver.Models;

namespace SmartSaver.Desktop
{
    public partial class Main : Form
    {
        private readonly Database _db = new Database();
        private List<Transaction> _transactionList;

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
            listTransactionsView.View = View.Details;
            listTransactionsView.GridLines = true;

            listTransactionsView.Columns.Add("Datetime", 150);
            listTransactionsView.Columns.Add("Amount", 75);
            listTransactionsView.Columns.Add("Details", 300);
            listTransactionsView.Columns.Add("Counter Party", 200);
        }

        public void UpdateTransactionList()
        {
            _transactionList = (List<Transaction>)_db.GetTransactions();
            _transactionList.Reverse();
            PopulateTransactionListView();
        }

        private void PopulateTransactionListView()
        {
            PopulateTransactionListView(_transactionList);
        }

        private void PopulateTransactionListView(IEnumerable<Transaction> transactionList)
        {
            listTransactionsView.Items.Clear();

            foreach (var transaction in transactionList)
            {
                var item = new ListViewItem(new[]
                {
                    ((DateTime)transaction.TrTime).ToString("yyyy-MM-dd HH:mm"), transaction.Amount.ToString(),
                    transaction.Details, transaction.CounterParty
                });

                listTransactionsView.Items.Add(item);
            }
        }

        private void buttonSetGoal_Click(object sender, EventArgs e)
        {
            var newGoalWindow = new AddGoalWindow();
            newGoalWindow.Show();
        }

        private void buttonAddTransaction_Click(object sender, EventArgs e)
        {
            var newTransactionWindow = new AddTransactionWindow(this);
            newTransactionWindow.Show();
        }

        private void buttonUpload_Click(object sender, EventArgs e)
        {
            var reader = new FileReader();
            reader.Import();
            UpdateTransactionList();
        }

        private void buttonExport_Click(object sender, EventArgs e)
        {
            var writer = new FileWriter();
            writer.Export();
        }

        private void buttonFilter_Click(object sender, EventArgs e)
        {
            var filteredTransactions = _transactionList.Where(transaction =>
                transaction.TrTime >= dateFilterFrom.Value && transaction.TrTime <= dateFilterTo.Value
            );
            PopulateTransactionListView(filteredTransactions);
        }

        private void buttonResetFilter_Click(object sender, EventArgs e)
        {
            UpdateTransactionList();
        }

        private void tipButton_Click(object sender, EventArgs e)
        {
            var tipWindow = new TipWindow();
            tipWindow.Show();

            tipWindow.Location = Location;
        }

        private void buttonMyBudget_Click(object sender, EventArgs e)
        {
            var myBudgetWindow = new MyBudgetWindow();
            myBudgetWindow.Show();
        }
    }
}
