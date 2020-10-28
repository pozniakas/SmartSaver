namespace SmartSaver.Desktop
{
    partial class MyBudgetWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.buttonAdd = new System.Windows.Forms.Button();
            this.buttonDelete = new System.Windows.Forms.Button();
            this.txtCategoryId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txtCategory = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txtBudgeted = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txtActivity = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txtAvailable = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.budgetAndCategoriesView = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.budgetAndCategoriesView)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonAdd
            // 
            this.buttonAdd.Location = new System.Drawing.Point(502, 230);
            this.buttonAdd.Name = "buttonAdd";
            this.buttonAdd.Size = new System.Drawing.Size(72, 27);
            this.buttonAdd.TabIndex = 1;
            this.buttonAdd.Text = "Add New";
            this.buttonAdd.UseVisualStyleBackColor = true;
            this.buttonAdd.Click += new System.EventHandler(this.buttonAdd_Click);
            // 
            // buttonDelete
            // 
            this.buttonDelete.Location = new System.Drawing.Point(410, 230);
            this.buttonDelete.Name = "buttonDelete";
            this.buttonDelete.Size = new System.Drawing.Size(74, 27);
            this.buttonDelete.TabIndex = 2;
            this.buttonDelete.Text = "Delete";
            this.buttonDelete.UseVisualStyleBackColor = true;
            this.buttonDelete.Click += new System.EventHandler(this.buttonDelete_Click);
            // 
            // txtCategoryId
            // 
            this.txtCategoryId.HeaderText = "ID";
            this.txtCategoryId.Name = "txtCategoryId";
            this.txtCategoryId.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.txtCategoryId.Visible = false;
            // 
            // txtCategory
            // 
            this.txtCategory.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.txtCategory.HeaderText = "CATEGORY";
            this.txtCategory.Name = "txtCategory";
            this.txtCategory.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // txtBudgeted
            // 
            this.txtBudgeted.HeaderText = "BUDGETED (€ PER MONTH)";
            this.txtBudgeted.Name = "txtBudgeted";
            this.txtBudgeted.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // txtActivity
            // 
            this.txtActivity.HeaderText = "ACTIVITY (€ PER MONTH)";
            this.txtActivity.Name = "txtActivity";
            this.txtActivity.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // txtAvailable
            // 
            this.txtAvailable.HeaderText = "AVAILABLE (€)";
            this.txtAvailable.Name = "txtAvailable";
            this.txtAvailable.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // budgetAndCategoriesView
            // 
            this.budgetAndCategoriesView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.budgetAndCategoriesView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.txtCategoryId,
            this.txtCategory,
            this.txtBudgeted,
            this.txtActivity,
            this.txtAvailable});
            this.budgetAndCategoriesView.Location = new System.Drawing.Point(0, 0);
            this.budgetAndCategoriesView.Name = "budgetAndCategoriesView";
            this.budgetAndCategoriesView.Size = new System.Drawing.Size(586, 224);
            this.budgetAndCategoriesView.TabIndex = 0;
            this.budgetAndCategoriesView.Text = "dataGridView1";
            this.budgetAndCategoriesView.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.budgetAndCategoriesView_CellEnter);
            // 
            // MyBudgetWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(586, 262);
            this.Controls.Add(this.buttonDelete);
            this.Controls.Add(this.buttonAdd);
            this.Controls.Add(this.budgetAndCategoriesView);
            this.Name = "MyBudgetWindow";
            this.Text = "MyBudgetWindow";
            this.Load += new System.EventHandler(this.MyBudgetWindow_Load);
            ((System.ComponentModel.ISupportInitialize)(this.budgetAndCategoriesView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.DataGridView budgetAndCategoriesView;
        private System.Windows.Forms.Button buttonAdd;
        private System.Windows.Forms.Button buttonDelete;
        private System.Windows.Forms.DataGridViewTextBoxColumn txtCategoryId;
        private System.Windows.Forms.DataGridViewTextBoxColumn txtCategory;
        private System.Windows.Forms.DataGridViewTextBoxColumn txtBudgeted;
        private System.Windows.Forms.DataGridViewTextBoxColumn txtActivity;
        private System.Windows.Forms.DataGridViewTextBoxColumn txtAvailable;
    }
}
