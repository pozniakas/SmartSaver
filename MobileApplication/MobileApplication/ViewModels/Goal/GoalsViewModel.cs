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
using System.Linq;

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
                        savePerMonth = decimal.Divide(goal.Amount, months);
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
            List<string> months = new List<string>();
            _transactions.ForEach(transaction => months.Add(transaction.TrTime.ToString("yyyyMM")));
            uniqueMonths = months.GroupBy(date => date).Select(grp => grp.First()).ToList();
            totalProfit = _transactions.Sum(transaction => transaction.Amount);
            return totalProfit /= uniqueMonths.Count;
        }

        public string GoalPossibility(decimal savePerMonth, decimal profitPerMonth)
        {
            decimal possibilityRate = profitPerMonth / savePerMonth;

            if (possibilityRate >= 2)
            {
                return "Huge";
            }

            if (possibilityRate >= 01.25m)
            {
                return "Real";
            }

            if (possibilityRate >= 0.8m)
            {
                return "Small";
            }

            return "Not real";
        }
    }
}