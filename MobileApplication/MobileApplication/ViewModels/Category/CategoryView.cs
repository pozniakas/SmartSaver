using DbEntities.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MobileApplication.ViewModels
{
    public class CategoryView : Category
    {
        public CategoryView (Category category)
        {
            Title = category.Title;
            DedicatedAmount = category.DedicatedAmount;
            Id = category.Id;
            User = category.User;
            Transactions = category.Transactions;
        }

        public decimal? CurrentlySpent { get; set; }
        public decimal? AvailableAmount { get; set; }
    }
}
