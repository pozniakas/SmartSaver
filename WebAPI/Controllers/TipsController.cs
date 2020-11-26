using DbEntities.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipsController : ControllerBase
    {
        private Func<int> _random = delegate ()
        {
            return (int) DateTime.UtcNow.DayOfWeek; 
        };

        private Lazy<Tip[]> _tips = new Lazy<Tip[]>(() => {
            return new Tip[]
            {
                new Tip { Title = "15 Practical Budget Tips", Link = "https://www.daveramsey.com/blog/the-truth-about-budgeting" },
                new Tip { Title = "Basic Budget Tips Everyone Should Know", Link = "https://www.thebalance.com/budgeting-101-1289589"},
                new Tip { Title = "8 Simple Ways To Save Money", Link = "https://bettermoneyhabits.bankofamerica.com/en/saving-budgeting/ways-to-save-money"},
                new Tip { Title = "7 Tips For Effective And Stress-Free Budgeting", Link = "https://www.forbes.com/sites/robertberger/2015/07/26/7-tips-for-effective-and-stress-free-budgeting/"},
                new Tip { Title = "What Is The 50/20/30 Budget Rule?", Link = "https://www.investopedia.com/ask/answers/022916/what-502030-budget-rule.asp"},
                new Tip { Title = "10 Best Ways To Save Money", Link = "https://www.regions.com/Insights/Personal/Personal-Finances/budgeting-and-saving/10-Best-Ways-to-Save-Money"},
                new Tip { Title = "10 Budgeting Tips That Really Work", Link = "https://www.solveyourdebts.com/blog/10-budgeting-tips-that-really-work/"}
            };
        });

        public TipsController() { }

        // GET: api/Tips
        [HttpGet("daily")]
        public Tip GetTip()
        {
            return _tips.Value[_random()];
        }
    }
}
