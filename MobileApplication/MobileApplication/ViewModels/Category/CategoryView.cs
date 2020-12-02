using DbEntities.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MobileApplication.ViewModels
{
    public class CategoryView : Category
    {
        public decimal? BudgetedAmount { get; set; }
        public decimal? AvailableAmount { get; set; }
    }
}
