﻿using System;
using System.Collections.Generic;
using System.Text;
using MobileApplication.Services.Rest;
using Xamarin.Forms;
using DbEntities.Entities;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

namespace MobileApplication.ViewModels
{
    [QueryProperty(nameof(Amount), nameof(Amount))]
    [QueryProperty(nameof(CounterParty), nameof(CounterParty))]
    [QueryProperty(nameof(TrTimeString), nameof(TrTimeString))]
    public class NewTransactionViewModel : BaseViewModel
    {
        private string details;
        private string counterParty;
        private string amount;
        private DateTime trTime = DateTime.Today;
        private decimal decimalAmount;
        private bool toggled;
        public Command LoadItemsCommand { get; }
        public ObservableCollection<Category> Items { get; set; }

        private Category category;

        private readonly IRestService<Transaction> RestService;
        private readonly IRestService<Category> CategoryRestService;
        public NewTransactionViewModel()
        {
            RestService = new RestService<Transaction>("api/Transactions");
            CategoryRestService = new RestService<Category>("api/Categories");
            Items = new ObservableCollection<Category>();
            _ = ExecuteLoadCategoryItemsCommand();

            SaveCommand = new Command(OnSave, ValidateSave);
            CancelCommand = new Command(OnCancel);
            this.PropertyChanged +=
                (_, __) => SaveCommand.ChangeCanExecute();
        }
        async Task ExecuteLoadCategoryItemsCommand()
        {
            Items.Clear();

            try
            {
                var items = await CategoryRestService.RefreshDataAsync();
                items.ForEach(category => Items.Add(category));
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

        private bool ValidateSave()
        {

            return !string.IsNullOrEmpty(details)
                && !string.IsNullOrEmpty(amount)
                && decimal.TryParse(amount, out decimalAmount);
        }

        public bool Toggled
        {
            get => toggled;
            set => SetProperty(ref toggled, value, nameof(Toggled));
            
        }
        public Category Categor
        {
            get { return category; }
            set
            {
                category = value;
                OnPropertyChanged();
            }
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

        public String TrTimeString
        {
            set => setTrTime(DateTime.Parse(value));
        }

        public void setTrTime(DateTime date)
        {
            TrTime = date;
        }

        public Command SaveCommand { get; }
        public Command CancelCommand { get; }

        private async void OnCancel()
        {
            await Shell.Current.GoToAsync("..");
        }
 
        private async void OnSave()
        {
            decimalAmount *= Toggled ? 1 : -1;
            var newTransaction = new Transaction()
            {
                TrTime = TrTime,
                Amount = decimalAmount,
                Details = Details,
                CounterParty = CounterParty,
                Category = Categor
            };

            IsBusy = true;

            await RestService.SaveItemAsync(newTransaction);

            await Shell.Current.GoToAsync("..");

            IsBusy = false;
        }


    }
}
