using System;
using System.Diagnostics;
using System.Threading.Tasks;
using MobileApplication.Models;
using Xamarin.Forms;
using DbEntities.Entities;
using MobileApplication.Services.Rest;

namespace MobileApplication.ViewModels
{
    class CategoryDetailViewModel : BaseViewModel
    {
        private readonly IRestService<Category> RestService;
        public CategoryDetailViewModel(Category category)
        {
            Id = category.Id.ToString();
            DedicatedAmount = category.DedicatedAmount.ToString();
            CategoryTitle = category.Title;

            RestService = new RestService<Category>("api/Categories/");

            SaveCommand = new Command(OnSave, ValidateSave);
            DeleteCommand = new Command(OnDelete);
            this.PropertyChanged +=
                (_, __) => SaveCommand.ChangeCanExecute();
        }

        public Command SaveCommand { get; }
        public Command DeleteCommand { get; }

        private string dedicatedAmount;
        private string title;
        public string Id { get; set; }

        public string DedicatedAmount
        {
            get => dedicatedAmount;
            set => SetProperty(ref dedicatedAmount, value);
        }

        public string CategoryTitle
        {
            get => title;
            set => SetProperty(ref title, value);
        }

        private bool ValidateSave()
        {
            return !string.IsNullOrEmpty(dedicatedAmount)
                && !string.IsNullOrEmpty(title)
                && decimal.TryParse(dedicatedAmount, out _);
        }
        private async void OnSave()
        {
            var amountInDecimal = decimal.Parse(DedicatedAmount);
            var longId = long.Parse(Id);

            var category = new Category()
            {
                Id = longId,
                DedicatedAmount = amountInDecimal,
                Title = CategoryTitle
            };

            IsBusy = true;

            await RestService.UpdateItemAsync(category, longId);

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