using System;
using System.Collections.Generic;
using System.Text;
using MobileApplication.Services.Rest;
using Xamarin.Forms;
using DbEntities.Models;
namespace MobileApplication.ViewModels
{
    public class NewCategoryViewModel : BaseViewModel
    {
        private string title;
        private string dedicatedAmount;

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
            return !String.IsNullOrEmpty(title)
                && !String.IsNullOrEmpty(dedicatedAmount);
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
            var amountInDecimal = decimal.Parse(DedicatedAmount);

            Category newCategory = new Category()
            {
                Title = CategoryTitle,
                DedicatedAmount = amountInDecimal
            };

            IsBusy = true;

            await  RestService.SaveTodoItemAsync(newCategory, true);

            await Shell.Current.GoToAsync("..");

            IsBusy = false;
        }
    }
}
