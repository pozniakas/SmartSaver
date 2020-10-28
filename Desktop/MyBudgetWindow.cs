using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using SmartSaver.Data;
using SmartSaver.Models;

namespace SmartSaver.Desktop
{
    public partial class MyBudgetWindow : Form
    {
        private readonly Database _db = new Database();
        private List<Category> _categoryList;
        private List<Transaction> _transactionList;
        private int _selectedId;

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
            _categoryList = (List<Category>) _db.GetCategories();
            _transactionList = (List<Transaction>) _db.GetTransactions();
            PopulateCategoryListView();
            Calculation(_categoryList, _transactionList);
        }

        private void PopulateCategoryListView()
        {
            PopulateCategoryListView(_categoryList);
        }

        public void Calculation (IEnumerable<Category> categoryList, IEnumerable<Transaction> transactionList)
        {
            if (categoryList == null)
            {
                throw new ArgumentNullException(nameof(categoryList));
            }

            if (transactionList == null)
            {
                throw new ArgumentNullException(nameof(transactionList));
            }

            decimal calc = 0;
            int index = 0;
           
            foreach (var category in categoryList)
            {
                foreach (var transaction in transactionList)
                {
                    if (category.Id == transaction.CategoryId)
                    {
                        calc += transaction.Amount;
                    }
                }
                this.budgetAndCategoriesView.Rows[index].Cells[3].Value = Math.Abs(calc);
                this.budgetAndCategoriesView.Rows[index].Cells[4].Value = category.DedicatedAmount - Math.Abs(calc);
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

        private void PopulateCategoryListView(IEnumerable<Category> categoryList)
        {
            foreach (var category in categoryList)
            {
                this.budgetAndCategoriesView.Rows.Add(category.Id, category.Title, category.DedicatedAmount);
            }
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            try
            {

                var title = budgetAndCategoriesView.Rows[budgetAndCategoriesView.RowCount - 2].Cells[1].Value;
                var dedicated = budgetAndCategoriesView
                    .Rows[budgetAndCategoriesView.RowCount - 2]
                    .Cells[2].Value;

                if (title == null || dedicated == null)
                {
                    MessageBox.Show("Invalid catgory");
                    return;
                }

                var dedicatedAmount = decimal.Parse(dedicated.ToString()!);

                Database db = new Database();

                Category newCategory = new Category {Title = title.ToString(), DedicatedAmount = dedicatedAmount};


                db.AddCategory(newCategory);
                MessageBox.Show(@"New category added successfully");
            }
            catch (Exception ex)
            {
                MessageBox.Show(@"Category duplicate");
                budgetAndCategoriesView.Rows[budgetAndCategoriesView.RowCount - 2].Cells[0].Value = null;
                budgetAndCategoriesView.Rows[budgetAndCategoriesView.RowCount - 2].Cells[1].Value = null;
                budgetAndCategoriesView.Rows[budgetAndCategoriesView.RowCount - 2].Cells[2].Value = null;
                budgetAndCategoriesView.Rows[budgetAndCategoriesView.RowCount - 2].Cells[3].Value = null;
                budgetAndCategoriesView.Rows[budgetAndCategoriesView.RowCount - 2].Cells[4].Value = null;
                Debug.WriteLine(ex);
            }
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            string selectedTitle = budgetAndCategoriesView.Rows[_selectedId].Cells[1].Value.ToString();
            Database db = new Database();
           
            db.RemoveCategory(selectedTitle);
            Debug.WriteLine(_selectedId);
            budgetAndCategoriesView.Rows.RemoveAt(_selectedId);
            MessageBox.Show(@"Row deleted");
        }

        private void budgetAndCategoriesView_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            _selectedId = e.RowIndex;
        }

    }
}
