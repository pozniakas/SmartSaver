using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

using Xamarin.Forms;
using DbEntities.Entities;
using MobileApplication.Models;
using MobileApplication.Views;
using MobileApplication.Services.Rest;
using System.Collections.Generic;

namespace MobileApplication.ViewModels
{
    public class GoalsViewModel : BaseViewModel
    {
        private Goal _selectedItem;

        private List<Goal> _goals { get; set; }
        private List<Transaction> _transactions { get; set; }
        public ObservableCollection<GoalView> GoalViews { get; }
        public Command LoadItemsCommand { get; }
        public Command AddItemCommand { get; }
        public Command<Goal> ItemTapped { get; }

        private readonly IRestService<Goal> RestService;
        private readonly IRestService<Transaction> TransactionRestService;

        public GoalsViewModel()
        {
            RestService = new RestService<Goal>("api/Goals");
            TransactionRestService = new RestService<Transaction>("api/Transactions");
            Title = "Goals";

            GoalViews = new ObservableCollection<GoalView>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            ItemTapped = new Command<Goal>(OnItemSelected);

            AddItemCommand = new Command(OnAddItem);
        }

        async Task ExecuteLoadItemsCommand()
        {
            IsBusy = true;
            GoalViews.Clear();

            try
            {
                _goals = await RestService.RefreshDataAsync();
                _transactions = await TransactionRestService.RefreshDataAsync();

                _goals.ForEach(goal =>{
                    decimal savePerMonth;
                    decimal profitPerMonth;
                    DateTime DeadlineDate = (DateTime)goal.Deadlinedate;
                    decimal months = DeadlineDate.Subtract(goal.Creationdate).Days / 31;
                    if (months > 1)
                    {
                        savePerMonth = goal.Amount / months;
                    }
                    else
                    {
                        savePerMonth = goal.Amount;
                    }

                    
                    var goalView = new GoalView(goal);
                    profitPerMonth = CalculateProfitPerMonth();
                    goalView.Possibility = GoalPossibility(savePerMonth, profitPerMonth);
                    goalView.MonthlyGoalAmount = Math.Round(savePerMonth,2);

                    GoalViews.Add(goalView);
                });
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        public void OnAppearing()
        {
            IsBusy = true;
            SelectedItem = null;
        }

        public Goal SelectedItem
        {
            get => _selectedItem;
            set
            {
                SetProperty(ref _selectedItem, value);
                OnItemSelected(value);
            }
        }

        private async void OnAddItem(object obj)
        {
            await Shell.Current.GoToAsync(nameof(NewGoalPage));
        }

        async void OnItemSelected(Goal goal)
        {
            if (goal == null)
                return;

            await Shell.Current.Navigation.PushAsync(new GoalDetailPage(goal));
        }

        public decimal CalculateProfitPerMonth()
        {
            decimal totalProfit = 0;
            List<string> uniqueMonths = new List<string>();
            foreach (Transaction transaction in _transactions)
            {
                string date = transaction.TrTime.ToString("yyyyMM");
                if (!uniqueMonths.Contains(date))
                {
                    uniqueMonths.Add(date);
                }
                totalProfit += transaction.Amount;

            }
            return totalProfit /= uniqueMonths.Count;
        }

        public string GoalPossibility(decimal savePerMonth, decimal profitPerMonth)
        {
            decimal possibilityRate = savePerMonth / profitPerMonth;

            if (possibilityRate < 0)
            {
                return "Not real";
            }
            if (possibilityRate < (decimal)0.5)
            {
                return "Huge";
            }

            if (possibilityRate < (decimal)0.8)
            {
                return "Real";
            }

            if (possibilityRate < (decimal)1.2)
            {
                return "Small";
            }

            return "Not real";
        }
    }
}