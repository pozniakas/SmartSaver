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
            this.panelRightSidebar = new System.Windows.Forms.Panel();
            this.panelBottomSidebar = new System.Windows.Forms.Panel();
            this.panelMain = new System.Windows.Forms.Panel();
            this.listTransactions = new System.Windows.Forms.ListView();
            this.panelMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelRightSidebar
            // 
            this.panelRightSidebar.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelRightSidebar.Location = new System.Drawing.Point(901, 0);
            this.panelRightSidebar.Name = "panelRightSidebar";
            this.panelRightSidebar.Size = new System.Drawing.Size(291, 562);
            this.panelRightSidebar.TabIndex = 0;
            // 
            // panelBottomSidebar
            // 
            this.panelBottomSidebar.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelBottomSidebar.Location = new System.Drawing.Point(0, 437);
            this.panelBottomSidebar.Name = "panelBottomSidebar";
            this.panelBottomSidebar.Size = new System.Drawing.Size(901, 125);
            this.panelBottomSidebar.TabIndex = 1;
            // 
            // panelMain
            // 
            this.panelMain.Controls.Add(this.listTransactions);
            this.panelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMain.Location = new System.Drawing.Point(0, 0);
            this.panelMain.Name = "panelMain";
            this.panelMain.Size = new System.Drawing.Size(901, 437);
            this.panelMain.TabIndex = 2;
            // 
            // listTransactions
            // 
            this.listTransactions.BackColor = System.Drawing.SystemColors.Control;
            this.listTransactions.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.listTransactions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listTransactions.HideSelection = false;
            this.listTransactions.Location = new System.Drawing.Point(0, 0);
            this.listTransactions.Name = "listTransactions";
            this.listTransactions.Size = new System.Drawing.Size(901, 437);
            this.listTransactions.TabIndex = 1;
            this.listTransactions.UseCompatibleStateImageBehavior = false;
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1192, 562);
            this.Controls.Add(this.panelMain);
            this.Controls.Add(this.panelBottomSidebar);
            this.Controls.Add(this.panelRightSidebar);
            this.Name = "Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Smart Saver";
            this.Load += new System.EventHandler(this.Main_Load);
            this.panelMain.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelRightSidebar;
        private System.Windows.Forms.Panel panelBottomSidebar;
        private System.Windows.Forms.Panel panelMain;
        private System.Windows.Forms.ListView listTransactions;
    }
}