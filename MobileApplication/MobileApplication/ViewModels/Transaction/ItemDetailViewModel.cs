using System;
using System.Diagnostics;
using System.Threading.Tasks;
using MobileApplication.Models;
using Xamarin.Forms;
using DbEntities.Entities;
using MobileApplication.Services.Rest;

namespace MobileApplication.ViewModels
{
    public class ItemDetailViewModel : BaseViewModel
    {
        private readonly IRestService<Transaction> RestService;
        public ItemDetailViewModel(Transaction transaction) {
            Id = transaction.Id.ToString();
            Amount = transaction.Amount.ToString();
            Details = transaction.Details;
            TrTime = transaction.TrTime;
            if (transaction.Category != null)
            {
                Category = transaction.Category.Title;
            }
            else
            {
                Category = "not set";
            }
            CounterParty = transaction.CounterParty;

            RestService = new RestService<Transaction>("api/Transactions/");

            SaveCommand = new Command(OnSave,ValidateSave);
            DeleteCommand = new Command(OnDelete);
            this.PropertyChanged +=
                (_, __) => SaveCommand.ChangeCanExecute();
        }

        public Command SaveCommand { get; }
        public Command DeleteCommand { get; }

        private string amount;
        private string details;
        private DateTime trTime;
        private string category;
        private string counterParty;
        public string Id { get; set; }

        public string Amount
        {
            get => amount;
            set => SetProperty(ref amount, value);
        }

        public string Details
        {
            get => details;
            set => SetProperty(ref details, value);
        }

        public DateTime TrTime
        {
            get => trTime;
            set => SetProperty(ref trTime, value);
        }

        public string Category
        {
            get => category;
            set => SetProperty(ref category, value);
        }
        public string CounterParty
        {
            get => counterParty;
            set => SetProperty(ref counterParty, value);
        }

        private bool ValidateSave()
        {
            return !String.IsNullOrEmpty(details)
                && !String.IsNullOrEmpty(counterParty)
                && !String.IsNullOrEmpty(amount)
                && decimal.TryParse(amount, out _);
        }
        private async void OnSave()
        {
            var amountInDecimal = decimal.Parse(Amount);
            var longId = long.Parse(Id);

            Transaction transaction = new Transaction()
            {
                Id = longId,
                TrTime = TrTime,
                Amount = amountInDecimal,
                Details = Details,
                CounterParty = CounterParty
            };

            IsBusy = true;

            await RestService.UpdateItemAsync(transaction, longId);

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
    }
}
