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
            CategoryList = db.GetCategories();
            PopulateCategoryListView();
        }

        private void PopulateCategoryListView()
        {
            PopulateCategoryListView(CategoryList);
        }

        private void PopulateCategoryListView(IEnumerable<Category> CategoryList)
        {
            this.budgetAndCategoriesView.Rows.Add("1", "Shopping", "30", "30", "0");
            foreach (var category in CategoryList)
            {
                this.budgetAndCategoriesView.Rows.Add(category.Id, category.Title, category.Description, " ", " ");
             
            }
        }

        private void budgetAndCategoriesView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow dgwRow = new DataGridViewRow();
     
            if (budgetAndCategoriesView.CurrentRow != null)
            {
               // DataGridViewRow dgwRow = new DataGridViewRow();
                /* Category newCategory = new Category
                 {
                     Id = 1,
                     Title = dgwRow.Cells["txtCategory"].Value.ToString(),
                     Description = dgwRow.Cells["txtBudgeted"].Value.ToString()

                 };*/

                // db.AddCategory(newCategory);

            }
        }
    }
}
