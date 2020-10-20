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
        private readonly Database db = new Database();
        private List<Goal> GoalList;
        public AddGoalWindow()
        {
            InitializeComponent();
        }


        private void AddGoalWindow_Load(object sender, EventArgs e)
        {
            PrepareGoalListView();
            UpdateGoalList();
        }

        private void PrepareGoalListView()
        {
            listView1.View = View.Details;
            listView1.GridLines = true;

            listView1.Columns.Add("Title", 100);
            listView1.Columns.Add("Datetime", 70);
            listView1.Columns.Add("Amount", 100);
            listView1.Columns.Add("Details", 146);
            listView1.Columns.Add("To save a week", 246);

        }

        public void UpdateGoalList()
        {
            GoalList = db.GetGoals();
            GoalList.Reverse();
            PopulateGoalListView();
        }

        private void PopulateGoalListView()
        {
            PopulateGoalListView(GoalList);
        }

        private void PopulateGoalListView(IEnumerable<Goal> GoalList)
        {
            listView1.Items.Clear();

            foreach (var goal in GoalList)
            {
                int money = (Decimal.ToInt32(goal.Amount)) / ((((DateTime)goal.GoalDate).Subtract(DateTime.UtcNow) / 7).Days);
                var item = new ListViewItem(new string[] {
                    goal.Title,
                    ((DateTime) goal.GoalDate).ToString("yyyy-MM-dd"),
                    goal.Amount.ToString(),
                    goal.Description,
                    money.ToString()
                });

                listView1.Items.Add(item);
            }
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
            string description = descriptionBox.Text;

            ValidateFields(amount, name);

            if (!String.IsNullOrWhiteSpace(amount) && !String.IsNullOrWhiteSpace(name))
            {
                decimal amountInDecimal = decimal.Parse(amount);

                Database db = new Database();

                Goal newGoal = new Goal
                {
                    GoalDate = date,
                    Amount = amountInDecimal,
                    Title = name,
                    Description = description,
                };
                db.AddGoal(newGoal);
                UpdateGoalList();
            }

        }
        private void backButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}