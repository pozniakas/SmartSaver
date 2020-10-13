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
            this.buttonSetGoal = new System.Windows.Forms.Button();
            this.buttonExport = new System.Windows.Forms.Button();
            this.panelTopSidebar.SuspendLayout();
            this.panelMain.SuspendLayout();
            this.panelRightSidebar.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelBottomSidebar
            // 
            this.panelBottomSidebar.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelBottomSidebar.Location = new System.Drawing.Point(0, 521);
            this.panelBottomSidebar.Name = "panelBottomSidebar";
            this.panelBottomSidebar.Size = new System.Drawing.Size(901, 41);
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
            this.panelTopSidebar.Name = "panelTopSidebar";
            this.panelTopSidebar.Size = new System.Drawing.Size(901, 87);
            this.panelTopSidebar.TabIndex = 2;
            // 
            // buttonResetFilter
            // 
            this.buttonResetFilter.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonResetFilter.Location = new System.Drawing.Point(751, 49);
            this.buttonResetFilter.Name = "buttonResetFilter";
            this.buttonResetFilter.Size = new System.Drawing.Size(109, 27);
            this.buttonResetFilter.TabIndex = 4;
            this.buttonResetFilter.Text = "Reset filter";
            this.buttonResetFilter.UseVisualStyleBackColor = true;
            this.buttonResetFilter.Click += new System.EventHandler(this.buttonResetFilter_Click);
            // 
            // buttonFilter
            // 
            this.buttonFilter.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonFilter.Location = new System.Drawing.Point(636, 49);
            this.buttonFilter.Name = "buttonFilter";
            this.buttonFilter.Size = new System.Drawing.Size(109, 27);
            this.buttonFilter.TabIndex = 4;
            this.buttonFilter.Text = "Filter";
            this.buttonFilter.UseVisualStyleBackColor = true;
            this.buttonFilter.Click += new System.EventHandler(this.buttonFilter_Click);
            // 
            // labelTo
            // 
            this.labelTo.AutoSize = true;
            this.labelTo.Location = new System.Drawing.Point(433, 54);
            this.labelTo.Name = "labelTo";
            this.labelTo.Size = new System.Drawing.Size(25, 20);
            this.labelTo.TabIndex = 3;
            this.labelTo.Text = "To";
            // 
            // labelFrom
            // 
            this.labelFrom.AutoSize = true;
            this.labelFrom.Location = new System.Drawing.Point(210, 56);
            this.labelFrom.Name = "labelFrom";
            this.labelFrom.Size = new System.Drawing.Size(43, 20);
            this.labelFrom.TabIndex = 3;
            this.labelFrom.Text = "From";
            // 
            // dateFilterTo
            // 
            this.dateFilterTo.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateFilterTo.Location = new System.Drawing.Point(464, 49);
            this.dateFilterTo.Name = "dateFilterTo";
            this.dateFilterTo.Size = new System.Drawing.Size(131, 27);
            this.dateFilterTo.TabIndex = 2;
            // 
            // dateFilterFrom
            // 
            this.dateFilterFrom.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateFilterFrom.Location = new System.Drawing.Point(259, 49);
            this.dateFilterFrom.Name = "dateFilterFrom";
            this.dateFilterFrom.Size = new System.Drawing.Size(131, 27);
            this.dateFilterFrom.TabIndex = 1;
            // 
            // labelFilter
            // 
            this.labelFilter.AutoSize = true;
            this.labelFilter.Location = new System.Drawing.Point(12, 56);
            this.labelFilter.Name = "labelFilter";
            this.labelFilter.Size = new System.Drawing.Size(134, 20);
            this.labelFilter.TabIndex = 0;
            this.labelFilter.Text = "Filter transction list";
            // 
            // panelMain
            // 
            this.panelMain.Controls.Add(this.listTransactionsView);
            this.panelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMain.Location = new System.Drawing.Point(0, 87);
            this.panelMain.Name = "panelMain";
            this.panelMain.Size = new System.Drawing.Size(901, 434);
            this.panelMain.TabIndex = 3;
            // 
            // listTransactionsView
            // 
            this.listTransactionsView.Dock = System.Windows.Forms.DockStyle.Left;
            this.listTransactionsView.HideSelection = false;
            this.listTransactionsView.Location = new System.Drawing.Point(0, 0);
            this.listTransactionsView.Name = "listTransactionsView";
            this.listTransactionsView.Size = new System.Drawing.Size(901, 434);
            this.listTransactionsView.TabIndex = 0;
            this.listTransactionsView.UseCompatibleStateImageBehavior = false;
            // 
            // buttonAddTransaction
            // 
            this.buttonAddTransaction.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonAddTransaction.Location = new System.Drawing.Point(61, 484);
            this.buttonAddTransaction.Name = "buttonAddTransaction";
            this.buttonAddTransaction.Size = new System.Drawing.Size(182, 29);
            this.buttonAddTransaction.TabIndex = 0;
            this.buttonAddTransaction.Text = "Add Transaction";
            this.buttonAddTransaction.UseVisualStyleBackColor = true;
            this.buttonAddTransaction.Click += new System.EventHandler(this.buttonAddTransaction_Click);
            // 
            // buttonUpload
            // 
            this.buttonUpload.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonUpload.Location = new System.Drawing.Point(7, 12);
            this.buttonUpload.Name = "buttonUpload";
            this.buttonUpload.Size = new System.Drawing.Size(272, 29);
            this.buttonUpload.TabIndex = 1;
            this.buttonUpload.Text = "Upload bank statment";
            this.buttonUpload.UseVisualStyleBackColor = true;
            this.buttonUpload.Click += new System.EventHandler(this.buttonUpload_Click);
            // 
            // panelRightSidebar
            // 
            this.panelRightSidebar.Controls.Add(this.buttonSetGoal);
            this.panelRightSidebar.Controls.Add(this.buttonExport);
            this.panelRightSidebar.Controls.Add(this.buttonUpload);
            this.panelRightSidebar.Controls.Add(this.buttonAddTransaction);
            this.panelRightSidebar.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelRightSidebar.Location = new System.Drawing.Point(901, 0);
            this.panelRightSidebar.Name = "panelRightSidebar";
            this.panelRightSidebar.Size = new System.Drawing.Size(291, 562);
            this.panelRightSidebar.TabIndex = 0;
            // 
            // buttonSetGoal
            // 
            this.buttonSetGoal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSetGoal.Location = new System.Drawing.Point(61, 416);
            this.buttonSetGoal.Name = "buttonSetGoal";
            this.buttonSetGoal.Size = new System.Drawing.Size(182, 29);
            this.buttonSetGoal.TabIndex = 0;
            this.buttonSetGoal.Text = "Set Goal";
            this.buttonSetGoal.UseVisualStyleBackColor = true;
            this.buttonSetGoal.Click += new System.EventHandler(this.buttonSetGoal_Click);
            // 
            // buttonExport
            // 
            this.buttonExport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonExport.Location = new System.Drawing.Point(7, 47);
            this.buttonExport.Name = "buttonExport";
            this.buttonExport.Size = new System.Drawing.Size(272, 29);
            this.buttonExport.TabIndex = 1;
            this.buttonExport.Text = "Export transactions";
            this.buttonExport.UseVisualStyleBackColor = true;
            this.buttonExport.Click += new System.EventHandler(this.buttonExport_Click);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1192, 562);
            this.Controls.Add(this.panelMain);
            this.Controls.Add(this.panelTopSidebar);
            this.Controls.Add(this.panelBottomSidebar);
            this.Controls.Add(this.panelRightSidebar);
            this.Name = "Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Smart Saver";
            this.Load += new System.EventHandler(this.Main_Load);
            this.panelTopSidebar.ResumeLayout(false);
            this.panelTopSidebar.PerformLayout();
            this.panelMain.ResumeLayout(false);
            this.panelRightSidebar.ResumeLayout(false);
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
        private System.Windows.Forms.Button buttonResetFilter;
        private System.Windows.Forms.Button buttonFilter;
        private System.Windows.Forms.Label labelTo;
        private System.Windows.Forms.Label labelFrom;
        private System.Windows.Forms.DateTimePicker dateFilterTo;
        private System.Windows.Forms.DateTimePicker dateFilterFrom;
        private System.Windows.Forms.Label labelFilter;
    }
}