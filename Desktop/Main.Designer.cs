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
            this.panelMain = new System.Windows.Forms.Panel();
            this.listTransactionsView = new System.Windows.Forms.ListView();
            this.buttonAddTransaction = new System.Windows.Forms.Button();
            this.buttonUpload = new System.Windows.Forms.Button();
            this.panelRightSidebar = new System.Windows.Forms.Panel();
            this.buttonSetGoal = new System.Windows.Forms.Button();
            this.buttonExport = new System.Windows.Forms.Button();
            this.tipButton = new System.Windows.Forms.Button();
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
            this.panelTopSidebar.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTopSidebar.Location = new System.Drawing.Point(0, 0);
            this.panelTopSidebar.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panelTopSidebar.Name = "panelTopSidebar";
            this.panelTopSidebar.Size = new System.Drawing.Size(788, 65);
            this.panelTopSidebar.TabIndex = 2;
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
            this.listTransactionsView.Size = new System.Drawing.Size(765, 326);
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
            // tipButton
            // 
            this.tipButton.Location = new System.Drawing.Point(221, 399);
            this.tipButton.Name = "tipButton";
            this.tipButton.Size = new System.Drawing.Size(34, 23);
            this.tipButton.TabIndex = 4;
            this.tipButton.Text = "Tip";
            this.tipButton.UseVisualStyleBackColor = true;
            this.tipButton.Click += new System.EventHandler(this.tipButton_Click);
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
        private System.Windows.Forms.Button tipButton;
    }
}