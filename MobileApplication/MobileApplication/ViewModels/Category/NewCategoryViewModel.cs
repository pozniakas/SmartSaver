using System;
using System.Collections.Generic;
using System.Text;
using MobileApplication.Services.Rest;
using Xamarin.Forms;
using DbEntities.Entities;
namespace MobileApplication.ViewModels
{
    public class NewCategoryViewModel : BaseViewModel
    {
        private string title;
        private string dedicatedAmount;
        private decimal decimalDedicatedAmount;

        private readonly IRestService<Category> RestService;
        public NewCategoryViewModel()
        {
            RestService = new RestService<Category>("api/Categories");

            SaveCommand = new Command(OnSave, ValidateSave);
            CancelCommand = new Command(OnCancel);
            this.PropertyChanged +=
                (_, __) => SaveCommand.ChangeCanExecute();
        }

        private bool ValidateSave()
        {
            return !string.IsNullOrEmpty(title)
                && !string.IsNullOrEmpty(dedicatedAmount)
                && Decimal.TryParse(dedicatedAmount, out decimalDedicatedAmount);
        }

        public string CategoryTitle
        {
            get => title;
            set => SetProperty(ref title, value);
        }

        public string DedicatedAmount
        {
            get => dedicatedAmount;
            set => SetProperty(ref dedicatedAmount, value);
        }

        public Command SaveCommand { get; }
        public Command CancelCommand { get; }

        private async void OnCancel()
        {
            await Shell.Current.GoToAsync("..");
        }

        private async void OnSave()
        {
            Category newCategory = new Category()
            {
                Title = CategoryTitle,
                DedicatedAmount = decimalDedicatedAmount
            };

            IsBusy = true;

            await  RestService.SaveItemAsync(newCategory, true);

            await Shell.Current.GoToAsync("..");

            IsBusy = false;
        }
    }
}
