using SmartSaver.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.CompilerServices;
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

        public static int index = new Random().Next(0, 6);
        public static Tip[] ValidateTips()
        {
            var tips = new Tip[]
            {
                new Tip ("15 Practical Budget Tips","https://www.daveramsey.com/blog/the-truth-about-budgeting"),
                new Tip ("Basic Budget Tips Everyone Should Know","https://www.thebalance.com/budgeting-101-1289589"),
                new Tip ("8 Simple Ways To Save Money","https://bettermoneyhabits.bankofamerica.com/en/saving-budgeting/ways-to-save-money"),
                new Tip ("7 Tips For Effective And Stress-Free Budgeting","https://www.forbes.com/sites/robertberger/2015/07/26/7-tips-for-effective-and-stress-free-budgeting/"),
                new Tip ("What Is The 50/20/30 Budget Rule?","https://www.investopedia.com/ask/answers/022916/what-502030-budget-rule.asp"),
                new Tip ("10 Best Ways To Save Money","https://www.regions.com/Insights/Personal/Personal-Finances/budgeting-and-saving/10-Best-Ways-to-Save-Money"),
                new Tip ("10 Budgeting Tips That Really Work", "https://www.solveyourdebts.com/blog/10-budgeting-tips-that-really-work/")
            };

            return tips;
        }

        public void LoadTipsAndLinks()
        {
            textLabel.Left = (this.Width - textLabel.Width) / 2;
            tipLinkLabel.Text = ValidateTips()[index].tipName;
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
                    FileName = ValidateTips()[index].tipLink,
                    UseShellExecute = true
                };

                this.tipLinkLabel.LinkVisited = true;
                Process.Start(psi);
            }
            catch (Exception)
            {
                MessageBox.Show("Unable to open link that was clicked.");
            }
        }


    }
}
