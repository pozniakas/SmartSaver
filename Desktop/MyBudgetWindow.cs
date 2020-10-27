using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SmartSaver.Data;
using SmartSaver.Models;

namespace SmartSaver.Desktop
{
    public partial class MyBudgetWindow : Form
    {
        private Database db = new Database();
        private List<Category> CategoryList;
        private List<Transaction> TransactionList;

        public MyBudgetWindow()
        {
            InitializeComponent();
        }

        private void MyBudgetWindow_Load(object sender, EventArgs e)
        {
            UpdateCategoriesList();
        }

        public void UpdateCategoriesList()
        {
            CategoryList = (List<Category>) db.GetCategories();
            TransactionList = (List<Transaction>) db.GetTransactions();
            PopulateCategoryListView();
            Calculation(CategoryList, TransactionList);
        }

        private void PopulateCategoryListView()
        {
            PopulateCategoryListView(CategoryList);
        }

        public void Calculation (IEnumerable<Category> CategoryList, IEnumerable<Transaction> TransactionList)
        {
            decimal calc = 0;
            int index = 0;
           
            foreach (var category in CategoryList)
            {
                foreach (var transaction in TransactionList)
                {
                    if (category.Id == transaction.CategoryId)
                    {
                        calc += transaction.Amount;
                    }
                }
                this.budgetAndCategoriesView.Rows[index].Cells[3].Value = System.Math.Abs(calc);
                this.budgetAndCategoriesView.Rows[index].Cells[4].Value = category.DedicatedAmount - System.Math.Abs(calc);
                if (category.DedicatedAmount - calc < 0)
                {
                    this.budgetAndCategoriesView.Rows[index].Cells[4].Style.BackColor = Color.Red;
                }
                else if (category.DedicatedAmount >= 0)
                {
                    this.budgetAndCategoriesView.Rows[index].Cells[4].Style.BackColor = Color.Green;
                }
                else if (category.DedicatedAmount == null) 
                { 
                    this.budgetAndCategoriesView.Rows[index].Cells[4].Style.BackColor = Color.Orange;
                }
               
                calc = 0;
                index++;
            }
        }

        private void PopulateCategoryListView(IEnumerable<Category> CategoryList)
        {
            foreach (var category in CategoryList)
            {
                this.budgetAndCategoriesView.Rows.Add(category.Id, category.Title, category.DedicatedAmount);
            }
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            string title = budgetAndCategoriesView.Rows[budgetAndCategoriesView.RowCount - 2].Cells[1].Value.ToString();
            decimal dedicatedAmount = 0;
            var dedicated = budgetAndCategoriesView
                    .Rows[budgetAndCategoriesView.RowCount - 2]
                    .Cells[2].Value;
            
             if (dedicated != null)
             {
                dedicatedAmount = decimal.Parse(dedicated.ToString());
             }

            Database db = new Database();

            Category newCategory = new Category
            {
                Title = title,
                DedicatedAmount = dedicatedAmount
            };

            try
            {
                db.AddCategory(newCategory); 
                MessageBox.Show("New category added successfully");
            }
            catch (Exception)
            {
                MessageBox.Show("Category dublicate");
                budgetAndCategoriesView.Rows[budgetAndCategoriesView.RowCount - 2].Cells[0].Value = null;
                budgetAndCategoriesView.Rows[budgetAndCategoriesView.RowCount - 2].Cells[1].Value = null;
                budgetAndCategoriesView.Rows[budgetAndCategoriesView.RowCount - 2].Cells[2].Value = null;
                budgetAndCategoriesView.Rows[budgetAndCategoriesView.RowCount - 2].Cells[3].Value = null;
                budgetAndCategoriesView.Rows[budgetAndCategoriesView.RowCount - 2].Cells[4].Value = null;
            }
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            string selectedTitle = budgetAndCategoriesView.Rows[selectedId].Cells[1].Value.ToString();
            Database db = new Database();
           
            db.RemoveCategory(selectedTitle);
            Debug.WriteLine(selectedId);
            budgetAndCategoriesView.Rows.RemoveAt(selectedId);
            MessageBox.Show("Row deleted");
        }

        int selectedId = 0;
        private void budgetAndCategoriesView_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            selectedId = e.RowIndex;
        }

    }
}
