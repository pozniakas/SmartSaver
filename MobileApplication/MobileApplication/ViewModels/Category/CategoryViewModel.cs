using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

using Xamarin.Forms;
using DbEntities.Entities;
using MobileApplication.Models;
using MobileApplication.Views;
using MobileApplication.Services;
using MobileApplication.Services.Rest;
using System.Collections.Generic;

namespace MobileApplication.ViewModels
{
    public class CategoryViewModel : BaseViewModel
    {
        private Category _selectedItem;

        private List<Category> _items { get; set; }
        public ObservableCollection<CategoryView> CategoryViews { get; }
        private List<Transaction> _transactions { get; }
        public Command LoadItemsCommand { get; }
        public Command AddItemCommand { get; }
        public Command<Category> ItemTapped { get; }

        private readonly IRestService<Category> RestService;
        private readonly IRestService<Transaction> TransactionRestService;

        public CategoryViewModel()
        {
            RestService = new RestService<Category>("api/Categories");
            TransactionRestService = new RestService<Transaction>("api/Transactions");
            Title = "Categories";
       
            CategoryViews = new ObservableCollection<CategoryView>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            ItemTapped = new Command<Category>(OnItemSelected);

            AddItemCommand = new Command(OnAddItem);
        }
        //public void Calculation(List<Category> items, List<Transaction> transactions)
        //{
        //    foreach (var category in items)
        //    {
        //        decimal calc = 0;
        //        foreach (var transaction in transactions)
        //        {
        //            if (category.Title == transaction.Category.Title)
        //            {
        //                calc += transaction.Amount;
        //            }
        //        }

        //        var newcategoryview = new CategoryView()
        //        {
        //            Title = category.Title,
        //            DedicatedAmount = category.DedicatedAmount,
        //            BudgetedAmount = Math.Abs(calc),
        //            AvailableAmount = category.DedicatedAmount - Math.Abs(calc)
        //        };

        //        CategoryViews.Add(newcategoryview);
                
        //    }
        //}
        public async Task ExecuteLoadItemsCommand()
        {
            IsBusy = true;
            CategoryViews.Clear();

            try
            {
                var items = await RestService.RefreshDataAsync();
                var transactions = await TransactionRestService.RefreshDataAsync();

                foreach (var category in items)
                {
                    decimal calc = 0;
                    foreach (var transaction in transactions)
                    {

                        if (transaction.Category != null && category.Title == transaction.Category.Title)
                        {
                            calc += transaction.Amount;
                        }
                    }

                    var newcategoryview = new CategoryView()
                    {
                        Title = category.Title,
                        DedicatedAmount = category.DedicatedAmount,
                        BudgetedAmount = Math.Abs(calc),
                        AvailableAmount = category.DedicatedAmount - Math.Abs(calc)
                    };

                    CategoryViews.Add(newcategoryview);

                }
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

        public Category SelectedItem
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
            await Shell.Current.GoToAsync(nameof(NewCategoryPage));
        }

        async void OnItemSelected(Category category)
        {
            if (category == null)
                return;

            await Shell.Current.Navigation.PushAsync(new CategoryDetailPage(category));
        }
    }
}