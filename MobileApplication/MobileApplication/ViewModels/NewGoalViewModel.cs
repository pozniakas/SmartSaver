using System;
using System.Collections.Generic;
using System.Text;
using MobileApplication.Services.Rest;
using Xamarin.Forms;
using DbEntities.Entities;
namespace MobileApplication.ViewModels
{
    public class NewGoalViewModel : BaseViewModel
    {
        private string title;
        private string description;
        private string amount;
        private DateTime deadlinedate;
        private DateTime creationdate;

        private readonly IRestService<Goal> RestService;

        public NewGoalViewModel()
        {
            RestService = new RestService<Goal>("api/Goals");

            SaveCommand = new Command(OnSave, ValidateSave);
            CancelCommand = new Command(OnCancel);
            this.PropertyChanged +=
                (_, __) => SaveCommand.ChangeCanExecute();
        }

        private bool ValidateSave()
        {
            return !String.IsNullOrEmpty(title)
                && !String.IsNullOrEmpty(description)
                && !String.IsNullOrEmpty(amount);
        }

        public string GoalTitle
        {
            get => title;
            set => SetProperty(ref title, value);
        }

        public string Description
        {
            get => description;
            set => SetProperty(ref description, value);
        }
        public string Amount
        {
            get => amount;
            set => SetProperty(ref amount, value);
        }

        public DateTime DeadlineDate
        {
            get => deadlinedate;
            set => SetProperty(ref deadlinedate, value);
        }
        public DateTime CreationDate
        {
            get => creationdate;
            set => SetProperty(ref creationdate, value);
        }

        public Command SaveCommand { get; }
        public Command CancelCommand { get; }

        private async void OnCancel()
        {
            await Shell.Current.GoToAsync("..");
        }

        private async void OnSave()
        {
            var amountInDecimal = decimal.Parse(Amount);

            Goal newGoal = new Goal()
            {
                Title = GoalTitle,
                Description = Description,
                Amount = amountInDecimal,
                Deadlinedate = DeadlineDate,
                Creationdate = CreationDate
            };

            IsBusy = true;

            await RestService.SaveTodoItemAsync(newGoal, true);

            await Shell.Current.GoToAsync("..");

            IsBusy = false;
        }
    }
}
