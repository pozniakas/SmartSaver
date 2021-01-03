using System;
using System.Diagnostics;
using System.Threading.Tasks;
using MobileApplication.Models;
using Xamarin.Forms;
using DbEntities.Entities;
using MobileApplication.Services.Rest;
using System.Collections.ObjectModel;

namespace MobileApplication.ViewModels
{
    public class ItemDetailViewModel : BaseViewModel
    {
        private readonly IRestService<Transaction> RestService;
        private readonly IRestService<Category> CategoryRestService;
        public ObservableCollection<Category> Categories { get; set; }

        private Category category;
        public ItemDetailViewModel(Transaction transaction) {
            Id = transaction.Id.ToString();
            Amount = transaction.Amount.ToString();
            Details = transaction.Details;
            TrTime = transaction.TrTime;
                Categor = transaction.Category;
            if (Categor == null) CategoryName = "not set";
            else CategoryName = Categor.Title;
            CounterParty = transaction.CounterParty;


            RestService = new RestService<Transaction>("api/Transactions/");
            CategoryRestService = new RestService<Category>("api/Categories");
            Categories = new ObservableCollection<Category>();
            _ = ExecuteLoadCategoryItemsCommand();

            SaveCommand = new Command(OnSave,ValidateSave);
            DeleteCommand = new Command(OnDelete);
            this.PropertyChanged +=
                (_, __) => SaveCommand.ChangeCanExecute();
        }
        async Task ExecuteLoadCategoryItemsCommand()
        {
            IsBusy = true;
            Categories.Clear();

            try
            {
                var items = await CategoryRestService.RefreshDataAsync();
                items.ForEach(category => Categories.Add(category));
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

        public Command SaveCommand { get; }
        public Command DeleteCommand { get; }

        private string amount;
        private string details;
        private DateTime trTime;
        private string counterParty;
        private string categoryName;
        public string Id { get; set; }

        public string Amount
        {
            get => amount;
            set => SetProperty(ref amount, value);
        }
        public string CategoryName
        {
            get => categoryName;
            set => SetProperty(ref categoryName, value);
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

        public Category Categor
        {
            get {
                return category; }
            set
            {
                category = value;
                OnPropertyChanged("Categor");
            }
        }
        public string CounterParty
        {
            get => counterParty;
            set => SetProperty(ref counterParty, value);
        }

        private bool ValidateSave()
        {
            return !string.IsNullOrEmpty(details)
                && !string.IsNullOrEmpty(counterParty)
                && !string.IsNullOrEmpty(amount)
                && decimal.TryParse(amount, out _);
        }
        private async void OnSave()
        {
            var amountInDecimal = decimal.Parse(Amount);
            var longId = long.Parse(Id);

            var transaction = new Transaction()
            {
                Id = longId,
                TrTime = TrTime,
                Amount = amountInDecimal,
                Details = Details,
                CounterParty = CounterParty,
                Category = Categor  
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
