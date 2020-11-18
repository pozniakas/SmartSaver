using System;
using System.Collections.Generic;
using System.Text;
using MobileApplication.Services.Rest;
using Xamarin.Forms;
using DbEntities.Models;

namespace MobileApplication.ViewModels
{
    public class NewTransactionViewModel : BaseViewModel
    {
        private string details;
        private string counterParty;
        private string amount;
        private DateTime trTime;

        private readonly IRestService<Transaction> RestService;

        public NewTransactionViewModel()
        {
            RestService = new RestService<Transaction>("api/Transactions");

            SaveCommand = new Command(OnSave, ValidateSave);
            CancelCommand = new Command(OnCancel);
            this.PropertyChanged +=
                (_, __) => SaveCommand.ChangeCanExecute();
        }

        private bool ValidateSave()
        {
            return !String.IsNullOrEmpty(details)
                && !String.IsNullOrEmpty(counterParty)
                && !String.IsNullOrEmpty(amount);
        }

        public string Details
        {
            get => details;
            set => SetProperty(ref details, value);
        }

        public string CounterParty
        {
            get => counterParty;
            set => SetProperty(ref counterParty, value);
        }
        public string Amount
        {
            get => amount;
            set => SetProperty(ref amount, value);
        }

        public DateTime TrTime
        {
            get => trTime;
            set => SetProperty(ref trTime, value);
        }

        public Command SaveCommand { get; }
        public Command CancelCommand { get; }

        private async void OnCancel()
        {
            await Shell.Current.GoToAsync("..");
        }

        private async void OnSave()
        {
            var amountInDecimal = decimal.Parse(Amount);

            Transaction newTransaction = new Transaction()
            {
                TrTime = TrTime,
                Amount = amountInDecimal,
                Details = Details,
                CounterParty = CounterParty
            };

            IsBusy = true;
            await RestService.SaveTodoItemAsync(newTransaction, true);

            await Shell.Current.GoToAsync("..");
        }
    }
}
