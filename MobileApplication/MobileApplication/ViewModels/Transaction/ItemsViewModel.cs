using DbEntities.Entities;
using MobileApplication.Models;
using MobileApplication.Services.Rest;
using MobileApplication.Views;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Linq;
using System.Collections.Generic;

namespace MobileApplication.ViewModels
{
    public class ItemsViewModel : BaseViewModel
    {
        private Transaction _selectedItem;
        private DateTime dateFrom;
        private DateTime dateTo;
        private List<Transaction> _transactions;

        public ObservableCollection<Transaction> Items { get; set; }
        public Command LoadItemsCommand { get; }
        public Command AddItemCommand { get; }
        public Command<Transaction> ItemTapped { get; }
        public Command FilterCommand { get; }
        public Command ResetFilterCommand { get; }

        private readonly IRestService<Transaction> RestService;

        public ItemsViewModel()
        {
            RestService = new RestService<Transaction>("api/Transactions");

            Title = "Transactions";
            Items = new ObservableCollection<Transaction>();

            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
            ItemTapped = new Command<Transaction>(OnItemSelected);
            AddItemCommand = new Command(OnAddItem);

            DateTo = DateTime.Now;

            FilterCommand = new Command(OnFilter, ValidateFilter);
            ResetFilterCommand = new Command(OnResetFilter);
            this.PropertyChanged +=
                (_, __) => FilterCommand.ChangeCanExecute();
        }

        async Task ExecuteLoadItemsCommand()
        {
            IsBusy = true;
            Items.Clear();

            try
            {
                _transactions = await RestService.RefreshDataAsync();

                _transactions.OrderByDescending(x => x.TrTime).ToList().ForEach(transaction => Items.Add(transaction));

                DateFrom = Items.Min(item => item.TrTime);
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

        public Transaction SelectedItem
        {
            get => _selectedItem;
            set
            {
                SetProperty(ref _selectedItem, value);
                OnItemSelected(value);
            }
        }

        public DateTime DateFrom
        {
            get => dateFrom;
            set => SetProperty(ref dateFrom, value);
        }

        public DateTime DateTo
        {
            get => dateTo;
            set => SetProperty(ref dateTo, value);
        }

        private async void OnAddItem(object obj)
        {
            await Shell.Current.GoToAsync(nameof(NewTransactionPage));
        }

        async void OnItemSelected(Transaction transaction)
        {
            if (transaction == null)
                return;

            // This will push the ItemDetailPage onto the navigation stack
            await Shell.Current.Navigation.PushAsync(new ItemDetailPage(transaction));
        }

        private void OnResetFilter()
        {
            Items.Clear();
            _transactions.ForEach(transaction => Items.Add(transaction));

            DateFrom = Items.Min(item => item.TrTime);
            DateTo = DateTime.Now;
        }

        private void OnFilter()
        {
            Items.Clear();

            _transactions
                .Where(item => item.TrTime >= dateFrom && item.TrTime <= dateTo)
                .ToList()
                .ForEach(transaction => Items.Add(transaction));
        }
        private bool ValidateFilter()
        {
            return DateFrom.Date <= DateTo.Date;
        }
    }
}