﻿using System;
using System.Diagnostics;
using System.Threading.Tasks;
using MobileApplication.Models;
using Xamarin.Forms;
using DbEntities.Entities;
using MobileApplication.Services.Rest;

namespace MobileApplication.ViewModels
{
    class GoalDetailViewModel : BaseViewModel
    {
        private readonly IRestService<Goal> RestService;
        public GoalDetailViewModel(Goal goal)
        {
            Id = goal.Id.ToString();
            Amount = goal.Amount.ToString();
            Description = goal.Description;
            Title = goal.Title;
            DeadlineDate = (DateTime)goal.Deadlinedate;
            CreationDate = goal.Creationdate;
            CreationDateString = goal.Creationdate.ToString("dd/MM/yyyy");

            RestService = new RestService<Goal>("api/Goals/");

            SaveCommand = new Command(OnSave, ValidateSave);
            DeleteCommand = new Command(OnDelete);
            this.PropertyChanged +=
                (_, __) => SaveCommand.ChangeCanExecute();
        }

        public Command SaveCommand { get; }
        public Command DeleteCommand { get; }

        private string amount;
        private string description;
        private string title;
        private DateTime creationDate;
        private string creationDateString;
        private DateTime deadlineDate;
        public string Id { get; set; }

        public string Amount
        {
            get => amount;
            set => SetProperty(ref amount, value);
        }

        public string Title
        {
            get => title;
            set => SetProperty(ref title, value);
        }

        public string Description
        {
            get => description;
            set => SetProperty(ref description, value);
        }

        public string CreationDateString
        {
            get => creationDateString;
            set => SetProperty(ref creationDateString, value);
        }

        public DateTime CreationDate
        {
            get => creationDate;
            set => SetProperty(ref creationDate, value);
        }

        public DateTime DeadlineDate
        {
            get => deadlineDate;
            set => SetProperty(ref deadlineDate, value);
        }

        private bool ValidateSave()
        {
            return !String.IsNullOrEmpty(description)
                && !String.IsNullOrEmpty(title)
                && !String.IsNullOrEmpty(amount)
                && decimal.TryParse(amount, out _)
                && (CreationDate.Date <= DeadlineDate.Date);
        }
        private async void OnSave()
        {
            var amountInDecimal = decimal.Parse(Amount);
            var longId = long.Parse(Id);

            Goal goal = new Goal()
            {
                Id = longId,
                Creationdate = CreationDate,
                Deadlinedate = DeadlineDate,
                Amount = amountInDecimal,
                Description = Description,
                Title = Title
            };

            IsBusy = true;

            await RestService.UpdateItemAsync(goal, longId);

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

