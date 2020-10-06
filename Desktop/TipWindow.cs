using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SmartSaver.Desktop
{
    
    public partial class TipWindow : Form
    {

        public TipWindow()
        {
            InitializeComponent();
        }

        public static string[] tipNames =
           {
                "15 Practical Budget Tips",
                "Basic Budget Tips Everyone Should Know",
                "8 Simple Ways To Save Money",
                "7 Tips For Effective And Stress-Free Budgeting",
                "What Is The 50/20/30 Budget Rule?",
                "10 Best Ways To Save Money",
                "10 Budgeting Tips That Really Work"
            };

        public static string[] tipLinks =
           {
                "https://www.daveramsey.com/blog/the-truth-about-budgeting",
                "https://www.thebalance.com/budgeting-101-1289589",
                "https://bettermoneyhabits.bankofamerica.com/en/saving-budgeting/ways-to-save-money",
                "https://www.forbes.com/sites/robertberger/2015/07/26/7-tips-for-effective-and-stress-free-budgeting/",
                "https://www.investopedia.com/ask/answers/022916/what-502030-budget-rule.asp",
                "https://www.regions.com/Insights/Personal/Personal-Finances/budgeting-and-saving/10-Best-Ways-to-Save-Money",
                "https://www.solveyourdebts.com/blog/10-budgeting-tips-that-really-work/"
            };

        
        public static int index = new Random().Next(0, 6);

        public void LoadTipsAndLinks()
        {
            textLabel.Left = (this.Width - textLabel.Width) / 2;

            tipLinkLabel.Text = tipNames[index]; 
            tipLinkLabel.Left = (this.Width - tipLinkLabel.Width) / 2;
        }

  

        private void TipWindow_Load(object sender, EventArgs e)
        {
          
            LoadTipsAndLinks();
        }

        private void tipLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            

            try
            {
                ProcessStartInfo psi = new ProcessStartInfo
                {
                    FileName = tipLinks[index],
                    UseShellExecute = true
                };

                this.tipLinkLabel.LinkVisited = true;
                Process.Start(psi);
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to open link that was clicked.");
            }
        }


    }
}
