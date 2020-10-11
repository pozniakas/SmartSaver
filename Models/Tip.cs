using System;
using System.Collections.Generic;
using System.Text;

namespace SmartSaver.Models
{
    class Tip
    {
        private string[] tipNames;
        private string[] tipLinks;

        public string[] TipNames
        {
            get
            {
                return tipNames;
            }
            set
            {
                tipNames = value;
            }
        }

        public string[] TipLinks
        {
            get
            {
                return tipLinks;
            }
            set
            {
                tipLinks = value;
            }
        }

        public Tip()
        {
            tipNames = new string[] {
                "15 Practical Budget Tips",
                "Basic Budget Tips Everyone Should Know",
                "8 Simple Ways To Save Money",
                "7 Tips For Effective And Stress-Free Budgeting",
                "What Is The 50/20/30 Budget Rule?",
                "10 Best Ways To Save Money",
                "10 Budgeting Tips That Really Work"

            };

            tipLinks = new string[]
            {
                "https://www.daveramsey.com/blog/the-truth-about-budgeting",
                "https://www.thebalance.com/budgeting-101-1289589",
                "https://bettermoneyhabits.bankofamerica.com/en/saving-budgeting/ways-to-save-money",
                "https://www.forbes.com/sites/robertberger/2015/07/26/7-tips-for-effective-and-stress-free-budgeting/",
                "https://www.investopedia.com/ask/answers/022916/what-502030-budget-rule.asp",
                "https://www.regions.com/Insights/Personal/Personal-Finances/budgeting-and-saving/10-Best-Ways-to-Save-Money",
                "https://www.solveyourdebts.com/blog/10-budgeting-tips-that-really-work/"
            };
        }
    
    }
}
