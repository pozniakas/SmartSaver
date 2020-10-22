using System;
using System.Collections.Generic;
using System.Drawing;
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
            listView1.Columns.Add("To save in a week", 120);
            listView1.Columns.Add("Possibility", 100);
        }

        public void UpdateGoalList()
        {
            GoalList = (List<Goal>)db.GetGoals();
            GoalList.Reverse();
            PopulateGoalListView();
        }

        private void PopulateGoalListView()
        {
            PopulateGoalListView(GoalList);
        }

        public string GoalPossibility(int profit, double worth)
        {
            string Possibility;
            double profitAWeek = profit / 4;
            if (worth / profitAWeek <= 0.5)
            {
                return Possibility = "Huge";
            }

            if (worth / profitAWeek <= 0.8)
            {
                return Possibility = "Real";
            }

            if (worth / profitAWeek <= 1)
            {
                return Possibility = "Small";
            }

            return Possibility = "Not real";
        }

        private void PopulateGoalListView(IEnumerable<Goal> GoalList)
        {
            listView1.Items.Clear();

            foreach (var goal in GoalList)
            {
                var money = Decimal.ToInt32(goal.Amount) /
                            (((DateTime)goal.Deadlinedate).Subtract(DateTime.UtcNow) / 7).Days;
                var profit = 200;
                var possibility = GoalPossibility(profit, money);
                var item = new ListViewItem(new[]
                {
                    goal.Title, ((DateTime)goal.Deadlinedate).ToString("yyyy-MM-dd"), goal.Amount.ToString(),
                    goal.Description, money.ToString(), possibility
                });

                listView1.Items.Add(item);
            }
        }

        private void goalAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            var ch = e.KeyChar;

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
            var date = goalDate.Value;
            var amount = goalMoney.Text;
            var name = goalNameBox.Text;
            var description = descriptionBox.Text;

            ValidateFields(amount, name);

            if (!String.IsNullOrWhiteSpace(amount) && !String.IsNullOrWhiteSpace(name))
            {
                var amountInDecimal = decimal.Parse(amount);

                var db = new Database();

                var newGoal = new Goal
                {
                    Deadlinedate = date, Amount = amountInDecimal, Title = name, Description = description
                };
                db.AddGoal(newGoal);
                UpdateGoalList();
            }
        }

        private void backButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
        }
    }
}
