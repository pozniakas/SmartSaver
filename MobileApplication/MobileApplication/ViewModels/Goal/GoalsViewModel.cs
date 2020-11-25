using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

using Xamarin.Forms;
using DbEntities.Models;
using MobileApplication.Models;
using MobileApplication.Views;
using MobileApplication.Services.Rest;

namespace MobileApplication.ViewModels
{
    public class GoalsViewModel : BaseViewModel
    {
        private Goal _selectedItem;

        public ObservableCollection<Goal> Items { get; }
        public Command LoadItemsCommand { get; }
        public Command AddItemCommand { get; }
        public Command<Goal> ItemTapped { get; }

        private readonly IRestService<Goal> RestService;

        public GoalsViewModel()
        {
            RestService = new RestService<Goal>("api/Goals");
            Title = "Goals";
            Items = new ObservableCollection<Goal>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            ItemTapped = new Command<Goal>(OnItemSelected);

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

        public Goal SelectedItem
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
            await Shell.Current.GoToAsync(nameof(NewGoalPage));
        }

        async void OnItemSelected(Goal goal)
        {
            if (goal == null)
                return;

            // This will push the ItemDetailPage onto the navigation stack
            Item item = new Item(goal.Id.ToString(), goal.Title,
                                string.Concat(goal.Amount.ToString(), '\n',
                                goal.Description, '\n',
                                goal.Creationdate.ToString(), '\n',
                                goal.Deadlinedate.ToString()));
            var itemDetailPage = new ItemDetailPage();
            itemDetailPage.BindingContext = item;
            await Shell.Current.Navigation.PushAsync(itemDetailPage);
        }
    }
}