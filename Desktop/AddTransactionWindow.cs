using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using SmartSaver.Data;
using SmartSaver.Models;

namespace SmartSaver.Desktop
{
    public partial class AddTransactionWindow : Form
    {
        private readonly Database _db = new Database();
        private readonly Main _mainWindow;
        private List<Category> _categoryList;

        public AddTransactionWindow(Main mw)
        {
            _mainWindow = mw;
            InitializeComponent();
        }

        private void transactionAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            var ch = e.KeyChar;

            if ((ch == 46 && transactionAmount.Text.IndexOf('.') != -1)
                || (ch == 45 && transactionAmount.Text.IndexOf('-') != -1))
            {
                e.Handled = true;
                return;
            }

            if (!Char.IsDigit(ch) && ch != 8 && ch != 46 && ch != 45)
            {
                e.Handled = true;
            }
        }


        public bool ValidateFields(string amount, string details)
        {
            if (string.IsNullOrWhiteSpace(amount))
            {
                transactionAmount.BackColor = Color.Red;
                return false;
            }

            if (string.IsNullOrWhiteSpace(details))
            {
                transactionDetailsReasons.BackColor = Color.Red;
                return false;
            }

            return true;
        }

        private void addNewTransactionButton_Click(object sender, EventArgs e)
        {
            var date = transactionDate.Value;
            var amount = transactionAmount.Text;
            var details = transactionDetailsReasons.Text;
            var category = (Category) transactionCategory.SelectedItem;

            if (!ValidateFields(amount, details))
            {
                MessageBox.Show(@"Invalid Values");
                return;
            }

            try
            {
                var amountInDecimal = decimal.Parse(amount);
                var db = new Database();

                var newTransaction = new Transaction
                {
                    TrTime = date,
                    Amount = amountInDecimal,
                    Details = details,
                    CategoryId = category.Id
                };

                db.AddTransaction(newTransaction);
                _mainWindow.UpdateTransactionList();
                MessageBox.Show(@"Transaction added");
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(@"Invalid values");
                Debug.WriteLine(ex);
            }

        }

        private void PopulateComboBox(IEnumerable<Category> categoryList)
        {
            transactionCategory.DataSource = categoryList;
            transactionCategory.DisplayMember = nameof(Category.Title);
        }

        private void AddTransactionWindow_Load(object sender, EventArgs e)
        {
            _categoryList = (List<Category>)_db.GetCategories();
            PopulateComboBox(_categoryList);
        }
    }
}
