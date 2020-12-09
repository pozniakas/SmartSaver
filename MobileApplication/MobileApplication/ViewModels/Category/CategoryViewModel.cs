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
using System.Linq;

namespace MobileApplication.ViewModels
{
    public class CategoryViewModel : BaseViewModel
    {
        private Category _selectedItem;

        public delegate void SumValues<T>(int value);
        private List<Category> _items { get; set; }
        private List<Transaction> _transactions { get; set; }

        public ObservableCollection<CategoryView> CategoryViews { get; }
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

        public async Task ExecuteLoadItemsCommand()
        {
            IsBusy = true;
            CategoryViews.Clear();

            try
            {
                _items = await RestService.RefreshDataAsync();
                _transactions = await TransactionRestService.RefreshDataAsync();

                _items.ForEach(category => {
                    var sum = _transactions
                        .Where(delegate (Transaction tr) { return tr.Category != null && category.Title == tr.Category.Title; })
                        .Select(tr => tr.Amount)
                        .Sum();

                    var categoryView = new CategoryView(category);
                    categoryView.CurrentlySpent = Math.Abs(sum);
                    categoryView.AvailableAmount = category.DedicatedAmount - Math.Abs(sum);

                    CategoryViews.Add(categoryView);
                });
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