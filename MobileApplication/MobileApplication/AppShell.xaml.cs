using System;
using System.Collections.Generic;
using MobileApplication.ViewModels;
using MobileApplication.Views;
using Xamarin.Forms;

namespace MobileApplication
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(ItemDetailPage), typeof(ItemDetailPage));
            Routing.RegisterRoute(nameof(GoalDetailPage), typeof(GoalDetailPage));
            Routing.RegisterRoute(nameof(CategoryDetailPage), typeof(CategoryDetailPage));
            Routing.RegisterRoute(nameof(NewItemPage), typeof(NewItemPage));
            Routing.RegisterRoute(nameof(NewCategoryPage), typeof(NewCategoryPage));
            Routing.RegisterRoute(nameof(NewGoalPage), typeof(NewGoalPage));
            Routing.RegisterRoute(nameof(NewTransactionPage), typeof(NewTransactionPage));
        }

        private async void OnMenuItemClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//LoginPage");
        }
    }
}
