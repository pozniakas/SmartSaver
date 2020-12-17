using System;
using System.Diagnostics;
using System.Threading.Tasks;
using MobileApplication.Models;
using Xamarin.Forms;
using DbEntities.Entities;
using MobileApplication.Services.Rest;
using System;
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
        public decimal profit;

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

        public string GoalPossibility(decimal worth)
        {
            decimal profitAWeek = profit / 4;
            decimal possibilityRate = worth / profitAWeek;
            Debug.WriteLine(possibilityRate);

            if (possibilityRate < (decimal)0.5)
            {
                return "Huge";
            }

            if (possibilityRate < (decimal)0.8)
            {
                return "Real";
            }

            if (possibilityRate < 1)
            {
                return "Small";
            }

            return "Not real";
        }


        public void CalculatePossibility()
        {
            decimal money;
            profit = 0;
            if (DeadlineDate.Subtract(DateTime.UtcNow).Days > 7)
            {
                money = Decimal.Parse(Amount) / (DeadlineDate.Subtract(DateTime.UtcNow).Days / 7);
            }
            else
            {
                money = Decimal.Parse(Amount);
            }

            CalculateProfit();
            Possibility = GoalPossibility(money);
        }

        public void CalculateProfit()
        {
            try
            {
                List<string> months = new List<string>();
                foreach (Transaction transaction in Transactions)
                {
                    string date = transaction.TrTime.ToString("yyyyMM");
                    if (!months.Contains(date))
                    {
                        months.Add(date);
                    }
                    profit += transaction.Amount;
                    profit /= months.Count;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
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

