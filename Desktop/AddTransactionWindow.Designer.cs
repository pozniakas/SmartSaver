namespace SmartSaver.Desktop
{
    partial class AddTransactionWindow
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.transactionDate = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.transactionCategory = new System.Windows.Forms.ComboBox();
            this.label = new System.Windows.Forms.Label();
            this.transactionDetailsReasons = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.transactionAmount = new System.Windows.Forms.TextBox();
            this.addNewTransactionButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(167, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(94, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "New Transaction";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 40);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(94, 15);
            this.label2.TabIndex = 1;
            this.label2.Text = "Transaction Date";
            // 
            // transactionDate
            // 
            this.transactionDate.Location = new System.Drawing.Point(12, 58);
            this.transactionDate.Name = "transactionDate";
            this.transactionDate.Size = new System.Drawing.Size(200, 23);
            this.transactionDate.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(302, 40);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(55, 15);
            this.label3.TabIndex = 3;
            this.label3.Text = "Category";
            // 
            // transactionCategory
            // 
            this.transactionCategory.FormattingEnabled = true;
            this.transactionCategory.Items.AddRange(new object[] {
            "Category 1",
            "Category 2",
            "Category 3",
            "Category 4"});
            this.transactionCategory.Location = new System.Drawing.Point(302, 58);
            this.transactionCategory.Name = "transactionCategory";
            this.transactionCategory.Size = new System.Drawing.Size(121, 23);
            this.transactionCategory.TabIndex = 4;
            // 
            // label
            // 
            this.label.AutoSize = true;
            this.label.Location = new System.Drawing.Point(12, 105);
            this.label.Name = "label";
            this.label.Size = new System.Drawing.Size(153, 15);
            this.label.TabIndex = 5;
            this.label.Text = "Transaction Details/Reasons";
            // 
            // transactionDetailsReasons
            // 
            this.transactionDetailsReasons.Location = new System.Drawing.Point(12, 123);
            this.transactionDetailsReasons.Name = "transactionDetailsReasons";
            this.transactionDetailsReasons.Size = new System.Drawing.Size(200, 23);
            this.transactionDetailsReasons.TabIndex = 6;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(302, 105);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(114, 15);
            this.label4.TabIndex = 7;
            this.label4.Text = "Transaction Amount";
            // 
            // transactionAmount
            // 
            this.transactionAmount.Location = new System.Drawing.Point(302, 123);
            this.transactionAmount.Name = "transactionAmount";
            this.transactionAmount.Size = new System.Drawing.Size(121, 23);
            this.transactionAmount.TabIndex = 8;
            this.transactionAmount.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.transactionAmount_KeyPress);
            // 
            // addNewTransactionButton
            // 
            this.addNewTransactionButton.Location = new System.Drawing.Point(348, 170);
            this.addNewTransactionButton.Name = "addNewTransactionButton";
            this.addNewTransactionButton.Size = new System.Drawing.Size(75, 23);
            this.addNewTransactionButton.TabIndex = 9;
            this.addNewTransactionButton.Text = "Add";
            this.addNewTransactionButton.UseVisualStyleBackColor = true;
            this.addNewTransactionButton.Click += new System.EventHandler(this.addNewTransactionButton_Click);
            // 
            // AddTransactionWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(436, 209);
            this.Controls.Add(this.addNewTransactionButton);
            this.Controls.Add(this.transactionAmount);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.transactionDetailsReasons);
            this.Controls.Add(this.label);
            this.Controls.Add(this.transactionCategory);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.transactionDate);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "AddTransactionWindow";
            this.Text = "AddTransactionWindow";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker transactionDate;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox transactionCategory;
        private System.Windows.Forms.Label transactionDetails;
        private System.Windows.Forms.Label label;
        private System.Windows.Forms.TextBox transactionDetailsReasons;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox transactionAmount;
        private System.Windows.Forms.Button addNewTransactionButton;
    }
}