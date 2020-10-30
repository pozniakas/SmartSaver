namespace SmartSaver.Desktop
{
    partial class TipWindow
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
            this.textLabel = new System.Windows.Forms.Label();
            this.tipLinkLabel = new System.Windows.Forms.LinkLabel();
            this.SuspendLayout();
            // 
            // textLabel
            // 
            this.textLabel.AutoSize = true;
            this.textLabel.Font = new System.Drawing.Font("Century Schoolbook", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.textLabel.Location = new System.Drawing.Point(171, 9);
            this.textLabel.Name = "textLabel";
            this.textLabel.Size = new System.Drawing.Size(154, 23);
            this.textLabel.TabIndex = 0;
            this.textLabel.Text = "Your daily tip!";
            // 
            // tipLinkLabel
            // 
            this.tipLinkLabel.AutoSize = true;
            this.tipLinkLabel.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.tipLinkLabel.Location = new System.Drawing.Point(224, 51);
            this.tipLinkLabel.Name = "tipLinkLabel";
            this.tipLinkLabel.Size = new System.Drawing.Size(50, 30);
            this.tipLinkLabel.TabIndex = 2;
            this.tipLinkLabel.TabStop = true;
            this.tipLinkLabel.Text = "Link";
            this.tipLinkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.tipLinkLabel_LinkClicked);
            // 
            // TipWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(488, 121);
            this.Controls.Add(this.tipLinkLabel);
            this.Controls.Add(this.textLabel);
            this.Name = "TipWindow";
            this.Text = "TipWindow";
            this.Load += new System.EventHandler(this.TipWindow_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label textLabel;
        private System.Windows.Forms.LinkLabel tipLinkLabel;
    }
}