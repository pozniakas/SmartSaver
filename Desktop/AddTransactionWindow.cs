using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using SmartSaver.Data;
using SmartSaver.Models;

namespace SmartSaver.Desktop
{
    public partial class AddTransactionWindow : Form
    {
        private List<Category> CategoryList;
        private readonly Database db = new Database();
        private readonly Main mainWindow;

        public AddTransactionWindow(Main mw)
        {
            mainWindow = mw;
            InitializeComponent();
        }

        private void transactionAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            var ch = e.KeyChar;

            if (ch == 46 && transactionAmount.Text.IndexOf('.') != -1)
            {
                e.Handled = true;
                return;
            }

            if (!Char.IsDigit(ch) && ch != 8 && ch != 46)
            {
                e.Handled = true;
            }
        }

        public void ValidateFields(string amount, string details)
        {
            if (String.IsNullOrWhiteSpace(amount))
            {
                transactionAmount.BackColor = Color.Red;
            }

            if (String.IsNullOrWhiteSpace(details))
            {
                transactionDetailsReasons.BackColor = Color.Red;
            }
        }

        private void addNewTransactionButton_Click(object sender, EventArgs e)
        {
            var date = transactionDate.Value;
            var amount = transactionAmount.Text;
            var details = transactionDetailsReasons.Text;
            var counterParty = transactionCategory.Text;

            ValidateFields(amount, details);

            if (!String.IsNullOrWhiteSpace(amount) && !String.IsNullOrWhiteSpace(details))
            {
                var amountInDecimal = decimal.Parse(amount);
                var db = new Database();

                var newTransaction = new Transaction {TrTime = date, Amount = amountInDecimal, Details = details};

                db.AddTransaction(newTransaction);
                mainWindow.UpdateTransactionList();
                MessageBox.Show("Transaction added");
                Close();
            }
        }

        private void PopulateComboBox(IEnumerable<Category> CategoryList)
        {
            foreach (var category in CategoryList)
            {
                transactionCategory.Items.Add(category.Title);
            }
        }

        private void AddTransactionWindow_Load(object sender, EventArgs e)
        {
            CategoryList = (List<Category>)db.GetCategories();
            PopulateComboBox(CategoryList);
        }
    }
}
