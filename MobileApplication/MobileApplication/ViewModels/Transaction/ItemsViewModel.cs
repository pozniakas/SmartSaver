using DbEntities.Entities;
using MobileApplication.Models;
using MobileApplication.Services.Rest;
using MobileApplication.Views;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MobileApplication.ViewModels
{
    public class ItemsViewModel : BaseViewModel
    {
        private Transaction _selectedItem;

        public ObservableCollection<Transaction> Items { get; }
        public Command LoadItemsCommand { get; }
        public Command AddItemCommand { get; }
        public Command<Transaction> ItemTapped { get; }

        private readonly IRestService<Transaction> RestService;
    
        public ItemsViewModel()
        {
            RestService = new RestService<Transaction>("api/Transactions");

            Title = "Transactions";
            Items = new ObservableCollection<Transaction>();

            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
            ItemTapped = new Command<Transaction>(OnItemSelected);
            AddItemCommand = new Command(OnAddItem);
        }

        async Task ExecuteLoadItemsCommand()
        {
            IsBusy = true;
            Items.Clear();

            try
            {
                var items = await RestService.RefreshDataAsync();

                items.ForEach(transaction => Items.Add(transaction));
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
    }
}