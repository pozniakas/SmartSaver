using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SmartSaver.Models;

namespace SmartSaver.Desktop
{
    public partial class AddGoalWindow : Form
    {
        public AddGoalWindow()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int input, expenses, goal, months;
            String goalName = goalNameBox.Text;
            goal = int.Parse(goalMoney.Text);
           // Months = goal / (Input - Expenses);
        }
    }
}
