using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq.Expressions;
using System.Windows.Forms;
using Microsoft.EntityFrameworkCore;
using SmartSaver.Data;
using SmartSaver.Models;

namespace SmartSaver.Desktop
{
    public partial class AddGoalWindow : Form
    {
        private readonly Database db = new Database();
        private List<Goal> _goalList;

        private int selectedId;

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
            goalWindowListView.View = View.Details;
            goalWindowListView.FullRowSelect = true;
            goalWindowListView.GridLines = true;

            goalWindowListView.Columns.Add("Title", 150);
            goalWindowListView.Columns.Add("Datetime", 70);
            goalWindowListView.Columns.Add("Amount", 100);
            goalWindowListView.Columns.Add("Details", 158);
            goalWindowListView.Columns.Add("To save in a week", 120);
            goalWindowListView.Columns.Add("Possibility", 100);

            goalWindowListView.Select();
        }

        public void UpdateGoalList()
        {
            _goalList = (List<Goal>)db.GetGoals();
            _goalList.Reverse();
            PopulateGoalListView();
        }

        public void ClearBox()
        {
            goalNameBox.Clear();
            goalMoney.Clear();
            descriptionBox.Clear();
        }

        private void PopulateGoalListView()
        {
            PopulateGoalListView(_goalList);
        }

        public string GoalPossibility(int profit, double worth)
        {
            double profitAWeek = profit / 4.0;
            if (worth / profitAWeek <= 0.5)
            {
                return "Huge";
            }

            if (worth / profitAWeek <= 0.8)
            {
                return "Real";
            }

            if (worth / profitAWeek <= 1)
            {
                return "Small";
            }

            return "Not real";
        }


        private void PopulateGoalListView(IEnumerable<Goal> goalList)
        {
            goalWindowListView.Items.Clear();

            foreach (var goal in goalList)
            {

                int money;
                if (((DateTime)goal.Deadlinedate).Subtract(DateTime.UtcNow).Days > 7)
                {
                    money = (decimal.ToInt32(goal.Amount)) /
                            ((((DateTime)goal.Deadlinedate).Subtract(DateTime.UtcNow) / 7).Days);
                }
                else
                {
                    money = (decimal.ToInt32(goal.Amount));
                }

                int profit = 200;
                string possibility = GoalPossibility(profit, money);
                var item = new ListViewItem(new string[]
                {
                    goal.Title, ((DateTime)goal.Deadlinedate).ToString("yyyy-MM-dd"), goal.Amount.ToString(),
                    goal.Description, money.ToString(), possibility
                });

                goalWindowListView.Items.Add(item);
            }
        }

        private void goalMoney_KeyPress(object sender, KeyPressEventArgs e)
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

        public bool ValidateFields(string amount, string details, string name, DateTime date)
        {

            if (string.IsNullOrWhiteSpace(name))
            {
                goalNameBox.BackColor = Color.Red;
                return false;
            }

            if (string.IsNullOrWhiteSpace(amount))
            {
                goalMoney.BackColor = Color.Red;
                return false;
            }

            if (string.IsNullOrWhiteSpace(details))
            {
                descriptionBox.BackColor = Color.Red;
                return false;
            }

            if (DateTime.Compare(date, DateTime.Now) <= 0)
            {
                MessageBox.Show("Wrong date");
                return false;
            }

            return true;
        }

        private void addGoal_Click(object sender, EventArgs e)
        {
            try
            {
                var date = goalDate.Value;
                var amount = goalMoney.Text;
                var name = goalNameBox.Text;
                var description = descriptionBox.Text;

                if (ValidateFields(amount, description, name, date))
                {
                    var newGoal = new Goal
                    {
                        Deadlinedate = date,
                        Amount = decimal.Parse(amount),
                        Title = name,
                        Description = description,
                        Creationdate = DateTime.UtcNow
                    };

                    try
                    {
                        db.AddGoal(newGoal);
                        UpdateGoalList();
                        ClearBox();
                    }
                    catch (DbUpdateException)
                    {
                        MessageBox.Show("Something went wrong. ");
                    }
                }
                else
                {
                    MessageBox.Show("Wrong format");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            try
            {
                var selectedTitle = goalWindowListView.SelectedItems[0].Text;
                goalWindowListView.Items.RemoveAt(selectedId);
                db.RemoveGoal(selectedTitle);
                UpdateGoalList();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        private void backButton_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
