﻿using MobileApplication.Services.Rest;
using MobileApplication.Views;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MobileApplication.ViewModels
{
    public class ListViewModel<Transaction> : BaseViewModel
    {
        private Transaction _selectedItem;
        public ObservableCollection<Transaction> Items { get; }
        private readonly IRestService<Transaction> RestService;

        public Command LoadItemsCommand { get; }
        public Command AddItemCommand { get; }
        public Command<Transaction> ItemTapped { get; }
        private Task<Action<Transaction>> OnItemClick { get; }

        public ListViewModel()
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
            SelectedItem = default;
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
            await Shell.Current.GoToAsync(nameof(NewItemPage));
        }

        async void OnItemSelected(Transaction item)
        {
            if (item == null)
                return;

            // This will push the ItemDetailPage onto the navigation stack
            //await Shell.Current.GoToAsync($"{nameof(ItemDetailPage)}?{nameof(ItemDetailViewModel.ItemId)}={item.ToString()}");
        }
    }
}