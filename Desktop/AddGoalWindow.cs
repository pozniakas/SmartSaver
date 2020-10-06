using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SmartSaver.Data;
using SmartSaver.Models;

namespace SmartSaver.Desktop
{
  
    public partial class AddGoalWindow : Form
    {
        public AddGoalWindow()
        {
            InitializeComponent();
        }


        private void goalAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;

            if (ch == 46 && goalMoney.Text.IndexOf('.') != -1)
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
                goalMoney.BackColor = Color.Red;
            }
            if (String.IsNullOrWhiteSpace(details))
            {
                goalMoney.BackColor = Color.Red;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        { 
            DateTime date = goalDate.Value;
            string amount = goalMoney.Text;
            string name = goalNameBox.Text;

            ValidateFields(amount, name);

            if (!String.IsNullOrWhiteSpace(amount) && !String.IsNullOrWhiteSpace(name))
            {
                decimal amountInDecimal = decimal.Parse(amount);

                Database db = new Database();

                Goal newGoal = new Goal
                {
                    GoalDate = date,
                    Amount = amountInDecimal,
                    Title = name
                };
               db.AddGoal(newGoal);
            }

            this.Close();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void goalDate_ValueChanged(object sender, EventArgs e)
        {

        }

        private void AddGoalWindow_Load(object sender, EventArgs e)
        {

        }
    }
}
