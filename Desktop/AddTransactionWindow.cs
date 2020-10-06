using SmartSaver.Data;
using SmartSaver.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SmartSaver.Desktop
{
    public partial class AddTransactionWindow : Form
    {
        private MainWindow mainWindow;
        public AddTransactionWindow(MainWindow mw)
        {
            mainWindow = mw;
            InitializeComponent();
        }

        private void transactionAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;

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
            DateTime date = transactionDate.Value;
            string amount = transactionAmount.Text;
            string details = transactionDetailsReasons.Text;
            string counterParty = transactionCategory.Text;

            ValidateFields(amount, details);

            if (!String.IsNullOrWhiteSpace(amount) && !String.IsNullOrWhiteSpace(details))
            {
                decimal amountInDecimal = decimal.Parse(amount);

                Database db = new Database();

                // Transaction newTransaction = new Transaction(date, amountInDecimal, details, counterParty); 
                Transaction newTransaction = new Transaction { 
                    TrTime = date,
                    Amount = amountInDecimal,
                    Details = details
                };

                
                db.AddTransaction(newTransaction);

                mainWindow.DrawTable(db.GetTransactions());                
            }
            this.Close();
        }

        private void transactionDate_ValueChanged(object sender, EventArgs e)
        {

        }

        private void transactionAmount_TextChanged(object sender, EventArgs e)
        {

        }

        private void transactionDetailsReasons_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
