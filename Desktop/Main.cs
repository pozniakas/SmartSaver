using SmartSaver.Data;
using SmartSaver.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace SmartSaver.Desktop
{
    public partial class Main : Form
    {
        private readonly Database db = new Database();
        private List<Transaction> TransactionList;

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
            TransactionList = db.GetTransactions();
            TransactionList.Reverse();
            PopulateTransactionListView();
        }

        private void PopulateTransactionListView()
        {
            PopulateTransactionListView(TransactionList);
        }

        private void PopulateTransactionListView(IEnumerable<Transaction> transactionList)
        {
            listTransactionsView.Items.Clear();

            foreach (var transaction in transactionList)
            {
                var item = new ListViewItem(new string[] { 
                    ((DateTime) transaction.TrTime).ToString("yyyy-MM-dd HH:mm"),
                    transaction.Amount.ToString(),
                    transaction.Details,
                    transaction.CounterParty
                });

                listTransactionsView.Items.Add(item);
            }
        }

        private void buttonSetGoal_Click(object sender, EventArgs e)
        {
            AddGoalWindow newGoalWindow = new AddGoalWindow();
            newGoalWindow.Show();
        }

        private void buttonAddTransaction_Click(object sender, EventArgs e)
        {
            AddTransactionWindow newTransactionWindow = new AddTransactionWindow(this);
            newTransactionWindow.Show();
        }

        private void buttonUpload_Click(object sender, EventArgs e)
        {
            FileManager fileManager = new FileManager();
            fileManager.Import();
            UpdateTransactionList();
        }

        private void buttonExport_Click(object sender, EventArgs e)
        {
            FileManager fileManager = new FileManager();
            fileManager.Export();
        }

        private void buttonFilter_Click(object sender, EventArgs e)
        {
            IEnumerable<Transaction> filteredTransactions = TransactionList.Where(transaction =>
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
            TipWindow tipWindow = new TipWindow();
            tipWindow.Show();

            tipWindow.Location = this.Location;

        }

        private void buttonMyBudget_Click(object sender, EventArgs e)
        {
            MyBudgetWindow myBudgetWindow = new MyBudgetWindow();
            myBudgetWindow.Show();
        }
    }
}
