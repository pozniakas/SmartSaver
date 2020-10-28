namespace SmartSaver.Desktop
{
    partial class Main
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
            this.panelBottomSidebar = new System.Windows.Forms.Panel();
            this.panelTopSidebar = new System.Windows.Forms.Panel();
            this.buttonResetFilter = new System.Windows.Forms.Button();
            this.buttonFilter = new System.Windows.Forms.Button();
            this.labelTo = new System.Windows.Forms.Label();
            this.labelFrom = new System.Windows.Forms.Label();
            this.dateFilterTo = new System.Windows.Forms.DateTimePicker();
            this.dateFilterFrom = new System.Windows.Forms.DateTimePicker();
            this.labelFilter = new System.Windows.Forms.Label();
            this.panelMain = new System.Windows.Forms.Panel();
            this.listTransactionsView = new System.Windows.Forms.ListView();
            this.buttonAddTransaction = new System.Windows.Forms.Button();
            this.buttonUpload = new System.Windows.Forms.Button();
            this.panelRightSidebar = new System.Windows.Forms.Panel();
            this.incomeLabel = new System.Windows.Forms.Label();
            this.buttonMyBudget = new System.Windows.Forms.Button();
            this.tipButton = new System.Windows.Forms.Button();
            this.buttonSetGoal = new System.Windows.Forms.Button();
            this.buttonExport = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.outcomeLabel = new System.Windows.Forms.Label();
            this.panelTopSidebar.SuspendLayout();
            this.panelMain.SuspendLayout();
            this.panelRightSidebar.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelBottomSidebar
            // 
            this.panelBottomSidebar.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelBottomSidebar.Location = new System.Drawing.Point(0, 391);
            this.panelBottomSidebar.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panelBottomSidebar.Name = "panelBottomSidebar";
            this.panelBottomSidebar.Size = new System.Drawing.Size(788, 31);
            this.panelBottomSidebar.TabIndex = 1;
            // 
            // panelTopSidebar
            // 
            this.panelTopSidebar.Controls.Add(this.buttonResetFilter);
            this.panelTopSidebar.Controls.Add(this.buttonFilter);
            this.panelTopSidebar.Controls.Add(this.labelTo);
            this.panelTopSidebar.Controls.Add(this.labelFrom);
            this.panelTopSidebar.Controls.Add(this.dateFilterTo);
            this.panelTopSidebar.Controls.Add(this.dateFilterFrom);
            this.panelTopSidebar.Controls.Add(this.labelFilter);
            this.panelTopSidebar.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTopSidebar.Location = new System.Drawing.Point(0, 0);
            this.panelTopSidebar.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panelTopSidebar.Name = "panelTopSidebar";
            this.panelTopSidebar.Size = new System.Drawing.Size(788, 65);
            this.panelTopSidebar.TabIndex = 2;
            // 
            // buttonResetFilter
            // 
            this.buttonResetFilter.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonResetFilter.Location = new System.Drawing.Point(657, 37);
            this.buttonResetFilter.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttonResetFilter.Name = "buttonResetFilter";
            this.buttonResetFilter.Size = new System.Drawing.Size(95, 20);
            this.buttonResetFilter.TabIndex = 4;
            this.buttonResetFilter.Text = "Reset filter";
            this.buttonResetFilter.UseVisualStyleBackColor = true;
            this.buttonResetFilter.Click += new System.EventHandler(this.buttonResetFilter_Click);
            // 
            // buttonFilter
            // 
            this.buttonFilter.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonFilter.Location = new System.Drawing.Point(556, 37);
            this.buttonFilter.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttonFilter.Name = "buttonFilter";
            this.buttonFilter.Size = new System.Drawing.Size(95, 20);
            this.buttonFilter.TabIndex = 4;
            this.buttonFilter.Text = "Filter";
            this.buttonFilter.UseVisualStyleBackColor = true;
            this.buttonFilter.Click += new System.EventHandler(this.buttonFilter_Click);
            // 
            // labelTo
            // 
            this.labelTo.AutoSize = true;
            this.labelTo.Location = new System.Drawing.Point(379, 40);
            this.labelTo.Name = "labelTo";
            this.labelTo.Size = new System.Drawing.Size(19, 15);
            this.labelTo.TabIndex = 3;
            this.labelTo.Text = "To";
            // 
            // labelFrom
            // 
            this.labelFrom.AutoSize = true;
            this.labelFrom.Location = new System.Drawing.Point(184, 42);
            this.labelFrom.Name = "labelFrom";
            this.labelFrom.Size = new System.Drawing.Size(35, 15);
            this.labelFrom.TabIndex = 3;
            this.labelFrom.Text = "From";
            // 
            // dateFilterTo
            // 
            this.dateFilterTo.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateFilterTo.Location = new System.Drawing.Point(406, 37);
            this.dateFilterTo.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dateFilterTo.Name = "dateFilterTo";
            this.dateFilterTo.Size = new System.Drawing.Size(115, 23);
            this.dateFilterTo.TabIndex = 2;
            // 
            // dateFilterFrom
            // 
            this.dateFilterFrom.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateFilterFrom.Location = new System.Drawing.Point(227, 37);
            this.dateFilterFrom.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dateFilterFrom.Name = "dateFilterFrom";
            this.dateFilterFrom.Size = new System.Drawing.Size(115, 23);
            this.dateFilterFrom.TabIndex = 1;
            // 
            // labelFilter
            // 
            this.labelFilter.AutoSize = true;
            this.labelFilter.Location = new System.Drawing.Point(10, 42);
            this.labelFilter.Name = "labelFilter";
            this.labelFilter.Size = new System.Drawing.Size(107, 15);
            this.labelFilter.TabIndex = 0;
            this.labelFilter.Text = "Filter transction list";
            // 
            // panelMain
            // 
            this.panelMain.Controls.Add(this.listTransactionsView);
            this.panelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMain.Location = new System.Drawing.Point(0, 65);
            this.panelMain.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panelMain.Name = "panelMain";
            this.panelMain.Size = new System.Drawing.Size(788, 326);
            this.panelMain.TabIndex = 3;
            // 
            // listTransactionsView
            // 
            this.listTransactionsView.Dock = System.Windows.Forms.DockStyle.Left;
            this.listTransactionsView.HideSelection = false;
            this.listTransactionsView.Location = new System.Drawing.Point(0, 0);
            this.listTransactionsView.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.listTransactionsView.Name = "listTransactionsView";
            this.listTransactionsView.Size = new System.Drawing.Size(789, 326);
            this.listTransactionsView.TabIndex = 0;
            this.listTransactionsView.UseCompatibleStateImageBehavior = false;
            // 
            // buttonAddTransaction
            // 
            this.buttonAddTransaction.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonAddTransaction.Location = new System.Drawing.Point(53, 363);
            this.buttonAddTransaction.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttonAddTransaction.Name = "buttonAddTransaction";
            this.buttonAddTransaction.Size = new System.Drawing.Size(159, 22);
            this.buttonAddTransaction.TabIndex = 0;
            this.buttonAddTransaction.Text = "Add Transaction";
            this.buttonAddTransaction.UseVisualStyleBackColor = true;
            this.buttonAddTransaction.Click += new System.EventHandler(this.buttonAddTransaction_Click);
            // 
            // buttonUpload
            // 
            this.buttonUpload.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonUpload.Location = new System.Drawing.Point(6, 9);
            this.buttonUpload.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttonUpload.Name = "buttonUpload";
            this.buttonUpload.Size = new System.Drawing.Size(238, 22);
            this.buttonUpload.TabIndex = 1;
            this.buttonUpload.Text = "Upload bank statment";
            this.buttonUpload.UseVisualStyleBackColor = true;
            this.buttonUpload.Click += new System.EventHandler(this.buttonUpload_Click);
            // 
            // panelRightSidebar
            // 
            this.panelRightSidebar.Controls.Add(this.outcomeLabel);
            this.panelRightSidebar.Controls.Add(this.label2);
            this.panelRightSidebar.Controls.Add(this.label1);
            this.panelRightSidebar.Controls.Add(this.incomeLabel);
            this.panelRightSidebar.Controls.Add(this.buttonMyBudget);
            this.panelRightSidebar.Controls.Add(this.tipButton);
            this.panelRightSidebar.Controls.Add(this.buttonSetGoal);
            this.panelRightSidebar.Controls.Add(this.buttonExport);
            this.panelRightSidebar.Controls.Add(this.buttonUpload);
            this.panelRightSidebar.Controls.Add(this.buttonAddTransaction);
            this.panelRightSidebar.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelRightSidebar.Location = new System.Drawing.Point(788, 0);
            this.panelRightSidebar.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panelRightSidebar.Name = "panelRightSidebar";
            this.panelRightSidebar.Size = new System.Drawing.Size(255, 422);
            this.panelRightSidebar.TabIndex = 0;
            // 
            // incomeLabel
            // 
            this.incomeLabel.AutoSize = true;
            this.incomeLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.incomeLabel.Location = new System.Drawing.Point(118, 107);
            this.incomeLabel.Name = "incomeLabel";
            this.incomeLabel.Size = new System.Drawing.Size(55, 17);
            this.incomeLabel.TabIndex = 4;
            this.incomeLabel.Text = "INCOME";
            // 
            // buttonMyBudget
            // 
            this.buttonMyBudget.Location = new System.Drawing.Point(53, 274);
            this.buttonMyBudget.Name = "buttonMyBudget";
            this.buttonMyBudget.Size = new System.Drawing.Size(159, 23);
            this.buttonMyBudget.TabIndex = 4;
            this.buttonMyBudget.Text = "My Budget";
            this.buttonMyBudget.UseVisualStyleBackColor = true;
            this.buttonMyBudget.Click += new System.EventHandler(this.buttonMyBudget_Click);
            // 
            // tipButton
            // 
            this.tipButton.Location = new System.Drawing.Point(213, 393);
            this.tipButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tipButton.Name = "tipButton";
            this.tipButton.Size = new System.Drawing.Size(40, 27);
            this.tipButton.TabIndex = 4;
            this.tipButton.Text = "Tip";
            this.tipButton.UseVisualStyleBackColor = true;
            this.tipButton.Click += new System.EventHandler(this.tipButton_Click);
            // 
            // buttonSetGoal
            // 
            this.buttonSetGoal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSetGoal.Location = new System.Drawing.Point(53, 312);
            this.buttonSetGoal.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttonSetGoal.Name = "buttonSetGoal";
            this.buttonSetGoal.Size = new System.Drawing.Size(159, 22);
            this.buttonSetGoal.TabIndex = 0;
            this.buttonSetGoal.Text = "Set Goal";
            this.buttonSetGoal.UseVisualStyleBackColor = true;
            this.buttonSetGoal.Click += new System.EventHandler(this.buttonSetGoal_Click);
            // 
            // buttonExport
            // 
            this.buttonExport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonExport.Location = new System.Drawing.Point(6, 35);
            this.buttonExport.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttonExport.Name = "buttonExport";
            this.buttonExport.Size = new System.Drawing.Size(238, 22);
            this.buttonExport.TabIndex = 1;
            this.buttonExport.Text = "Export transactions";
            this.buttonExport.UseVisualStyleBackColor = true;
            this.buttonExport.Click += new System.EventHandler(this.buttonExport_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(19, 106);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 15);
            this.label1.TabIndex = 5;
            this.label1.Text = "INCOME (€)";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(20, 136);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 15);
            this.label2.TabIndex = 6;
            this.label2.Text = "OUTCOME (€)";
            // 
            // outcomeLabel
            // 
            this.outcomeLabel.AutoSize = true;
            this.outcomeLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.outcomeLabel.Location = new System.Drawing.Point(118, 136);
            this.outcomeLabel.Name = "outcomeLabel";
            this.outcomeLabel.Size = new System.Drawing.Size(65, 17);
            this.outcomeLabel.TabIndex = 6;
            this.outcomeLabel.Text = "OUTCOME";
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1043, 422);
            this.Controls.Add(this.panelMain);
            this.Controls.Add(this.panelTopSidebar);
            this.Controls.Add(this.panelBottomSidebar);
            this.Controls.Add(this.panelRightSidebar);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Smart Saver";
            this.Load += new System.EventHandler(this.Main_Load);
            this.panelTopSidebar.ResumeLayout(false);
            this.panelTopSidebar.PerformLayout();
            this.panelMain.ResumeLayout(false);
            this.panelRightSidebar.ResumeLayout(false);
            this.panelRightSidebar.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panelBottomSidebar;
        private System.Windows.Forms.Panel panelTopSidebar;
        private System.Windows.Forms.Panel panelMain;
        private System.Windows.Forms.ListView listTransactionsView;
        private System.Windows.Forms.Button buttonAddTransaction;
        private System.Windows.Forms.Button buttonUpload;
        private System.Windows.Forms.Panel panelRightSidebar;
        private System.Windows.Forms.Button buttonSetGoal;
        private System.Windows.Forms.Button buttonExport;
        private System.Windows.Forms.Button tipButton;
        private System.Windows.Forms.Button buttonResetFilter;
        private System.Windows.Forms.Button buttonFilter;
        private System.Windows.Forms.Label labelTo;
        private System.Windows.Forms.Label labelFrom;
        private System.Windows.Forms.DateTimePicker dateFilterTo;
        private System.Windows.Forms.DateTimePicker dateFilterFrom;
        private System.Windows.Forms.Label labelFilter;
        private System.Windows.Forms.Button buttonMyBudget;
        private System.Windows.Forms.Label incomeLabel;
        private System.Windows.Forms.Label outcomeLabel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
    }
}