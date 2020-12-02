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

namespace MobileApplication.ViewModels
{
    public class CategoryViewModel : BaseViewModel
    {
        private Category _selectedItem;

        public ObservableCollection<Category> Items { get; }
        public Command LoadItemsCommand { get; }
        public Command AddItemCommand { get; }
        public Command<Category> ItemTapped { get; }

        private readonly IRestService<Category> RestService;

        public CategoryViewModel()
        {
            RestService = new RestService<Category>("api/Categories");
            Title = "Categories";
            Items = new ObservableCollection<Category>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            ItemTapped = new Command<Category>(OnItemSelected);

            AddItemCommand = new Command(OnAddItem);
        }

        async Task ExecuteLoadItemsCommand()
        {
            IsBusy = true;
            Items.Clear();

            try
            {
                var items = await RestService.RefreshDataAsync();

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

            // This will push the ItemDetailPage onto the navigation stack
            Item item = new Item(category.Id.ToString(),category.Title,category.DedicatedAmount.ToString());
            var itemDetailPage = new ItemDetailPage();
            itemDetailPage.BindingContext = item;
            await Shell.Current.Navigation.PushAsync(itemDetailPage);
        }
    }
}