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
            this.txtCategoryId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txtCategory = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txtBudgeted = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txtActivity = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txtAvailable = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.budgetAndCategoriesView = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.budgetAndCategoriesView)).BeginInit();
            this.SuspendLayout();
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
            this.txtBudgeted.HeaderText = "BUDGETED";
            this.txtBudgeted.Name = "txtBudgeted";
            this.txtBudgeted.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // txtActivity
            // 
            this.txtActivity.HeaderText = "ACTIVITY";
            this.txtActivity.Name = "txtActivity";
            this.txtActivity.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // txtAvailable
            // 
            this.txtAvailable.HeaderText = "AVAILABLE";
            this.txtAvailable.Name = "txtAvailable";
            this.txtAvailable.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // MyBudgetWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(586, 262);
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
            this.budgetAndCategoriesView.Size = new System.Drawing.Size(585, 262);
            this.budgetAndCategoriesView.TabIndex = 0;
            this.budgetAndCategoriesView.Text = "dataGridView1";
            this.budgetAndCategoriesView.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.budgetAndCategoriesView_CellValueChanged);
            this.Controls.Add(this.budgetAndCategoriesView);
            this.Name = "MyBudgetWindow";
            this.Text = "MyBudgetWindow";
            this.Load += new System.EventHandler(this.MyBudgetWindow_Load);
            ((System.ComponentModel.ISupportInitialize)(this.budgetAndCategoriesView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.DataGridView budgetAndCategoriesView;
        private System.Windows.Forms.DataGridViewTextBoxColumn txtCategoryId;
        private System.Windows.Forms.DataGridViewTextBoxColumn txtCategory;
        private System.Windows.Forms.DataGridViewTextBoxColumn txtBudgeted;
        private System.Windows.Forms.DataGridViewTextBoxColumn txtActivity;
        private System.Windows.Forms.DataGridViewTextBoxColumn txtAvailable;
    }
}