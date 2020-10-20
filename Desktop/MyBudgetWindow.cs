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
            /*DataGridViewRow dgwRow = budgetAndCategoriesView.CurrentRow;
     
            if (budgetAndCategoriesView.CurrentRow != null)
            {

                Debug.WriteLine(dgwRow.Cells["txtCategory"].Value == null ? " " : dgwRow.Cells["txtCategory"].Value.ToString());
                Debug.WriteLine(Convert.ToInt32(dgwRow.Cells["txtActivity"].Value == null ? "0" : dgwRow.Cells["txtCategory"].Value.ToString()));
                //  Debug.WriteLine(Convert.ToInt32(dgwRow.Cells["txtActivity"].Value == null ? "0" : dgwRow.Cells["txtCategory"].Value.ToString()));
                // Debug.WriteLine(Convert.ToInt32(dgwRow.Cells["txtAvailable"].Value == null ? "0" : dgwRow.Cells["txtCategory"].Value.ToString()));
                Category newCategory = new Category
                {
                    Id = 1,
                    Title = dgwRow.Cells["txtCategory"].Value == null ? " ": dgwRow.Cells["txtCategory"].Value.ToString(),
                    //Title = dgwRow.Cells["txtCategory"].Value.ToString(),
                   // Description = dgwRow.Cells["txtBudgeted"].Value.ToString()

                };

                db.AddCategory(newCategory);
            }*/
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            
             string title = budgetAndCategoriesView.Rows[budgetAndCategoriesView.RowCount - 2].Cells[1].Value.ToString();
             /*int budgeted = Convert.ToInt32(budgetAndCategoriesView.Rows[budgetAndCategoriesView.RowCount - 2].Cells[2].Value.ToString());
             int activity = Convert.ToInt32(budgetAndCategoriesView.Rows[budgetAndCategoriesView.RowCount - 2].Cells[3].Value.ToString());
             int available = Convert.ToInt32(budgetAndCategoriesView.Rows[budgetAndCategoriesView.RowCount - 2].Cells[4].Value.ToString());*/
            // Debug.WriteLine(title + budgeted + activity + available);

             Database db = new Database();

            Category newCategory = new Category
            {
                Title = title
            };

            db.AddCategory(newCategory);
            MessageBox.Show("New category added successfully");

        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {

        }

        private void budgetAndCategoriesView_CellEnter(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
