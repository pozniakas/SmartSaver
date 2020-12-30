using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;
using DbEntities.Entities;
using MobileApplication.Services.Rest;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace MobileApplication.ViewModels
{
    class GoalDetailViewModel : BaseViewModel
    {
        private readonly IRestService<Goal> RestService;
        private readonly IRestService<Transaction> TransactionRestService;
        public ObservableCollection<Transaction> Transactions { get; }
        public GoalDetailViewModel(Goal goal)
        {
            Id = goal.Id.ToString();
            Amount = goal.Amount.ToString();
            Description = goal.Description;
            Title = goal.Title;
            DeadlineDate = (DateTime)goal.Deadlinedate;
            CreationDate = goal.Creationdate;
            CreationDateString = goal.Creationdate.ToString("dd/MM/yyyy");

            RestService = new RestService<Goal>("api/Goals/");
            TransactionRestService = new RestService<Transaction>("api/Transactions/");

            Transactions = new ObservableCollection<Transaction>();
            _ = ExecuteLoadTransactionItemsCommand();

            SaveCommand = new Command(OnSave, ValidateSave);
            DeleteCommand = new Command(OnDelete);
            PropertyChanged +=
                (_, __) => SaveCommand.ChangeCanExecute();
        }

        public Command SaveCommand { get; }
        public Command DeleteCommand { get; }

        private string amount;
        private string description;
        private string title;
        private string possibility;
        private DateTime creationDate;
        private string creationDateString;
        private DateTime deadlineDate;

        public string Id { get; set; }

        public string Amount
        {
            get => amount;
            set => SetProperty(ref amount, value);
        }

        public string Title
        {
            get => title;
            set => SetProperty(ref title, value);
        }

        public string Possibility
        {
            get => possibility;
            set => SetProperty(ref possibility, value);
        }

        public string Description
        {
            get => description;
            set => SetProperty(ref description, value);
        }

        public string CreationDateString
        {
            get => creationDateString;
            set => SetProperty(ref creationDateString, value);
        }

        public DateTime CreationDate
        {
            get => creationDate;
            set => SetProperty(ref creationDate, value);
        }

        public DateTime DeadlineDate
        {
            get => deadlineDate;
            set => SetProperty(ref deadlineDate, value);
        }

        private bool ValidateSave()
        {
            return !string.IsNullOrEmpty(description)
                && !string.IsNullOrEmpty(title)
                && !string.IsNullOrEmpty(amount)
                && decimal.TryParse(amount, out _)
                && (CreationDate.Date <= DeadlineDate.Date);
        }
        private async void OnSave()
        {
            var amountInDecimal = decimal.Parse(Amount);
            var longId = long.Parse(Id);

            var goal = new Goal()
            {
                Id = longId,
                Creationdate = CreationDate,
                Deadlinedate = DeadlineDate,
                Amount = amountInDecimal,
                Description = Description,
                Title = Title
            };

            IsBusy = true;

            await RestService.UpdateItemAsync(goal, longId);

            await Shell.Current.GoToAsync("..");

            IsBusy = false;
        }

        private async void OnDelete()
        {
            IsBusy = true;

            await RestService.DeleteItemAsync(long.Parse(Id));

            await Shell.Current.GoToAsync("..");

            IsBusy = false;
        }

        public void CalculatePossibility()
        {
            decimal savePerMonth;
            decimal profitPerMonth;
            decimal months = DeadlineDate.Subtract(CreationDate).Days / 31;
            if (months>1)
            {
                savePerMonth = decimal.Parse(Amount) / months;
            }
            else
            {
                savePerMonth = decimal.Parse(Amount);
            }

            profitPerMonth = CalculateProfitPerMonth();
            Possibility = GoalPossibility(savePerMonth, profitPerMonth);
        }

        public decimal CalculateProfitPerMonth()
        {
            decimal totalProfit = 0;
            List<string> uniqueMonths = new List<string>();
            foreach (Transaction transaction in Transactions)
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
            Debug.WriteLine(savePerMonth);
            Debug.WriteLine(profitPerMonth);
            Debug.WriteLine(possibilityRate);

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

        async Task ExecuteLoadTransactionItemsCommand()
        {
            IsBusy = true;
            Transactions.Clear();

            try
            {
                var items = await TransactionRestService.RefreshDataAsync();
                items.ForEach(transaction => Transactions.Add(transaction));
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
                CalculatePossibility();
            }
        }
    }
}

