using System;
using System.Diagnostics;
using System.Threading.Tasks;
using MobileApplication.Models;
using Xamarin.Forms;
using DbEntities.Entities;

namespace MobileApplication.ViewModels
{
    public class ItemDetailViewModel : BaseViewModel
    {
        public ItemDetailViewModel(Transaction transaction) {
            Id = transaction.Id.ToString();
            Amount = transaction.Amount.ToString();
            Details = transaction.Details;
            TrTime = transaction.TrTime.ToString();
            if (transaction.Category != null)
            {
                Category = transaction.Category.Title;
            }
            else
            {
                Category = "not set";
            }
            CounterParty = transaction.CounterParty;
        }

        private string amount;
        private string details;
        private string trTime;
        private string category;
        private string counterParty;
        public string Id { get; set; }

        public string Amount
        {
            get => amount;
            set => SetProperty(ref amount, value);
        }

        public string Details
        {
            get => details;
            set => SetProperty(ref details, value);
        }

        public string TrTime
        {
            get => trTime;
            set => SetProperty(ref trTime, value);
        }

        public string Category
        {
            get => category;
            set => SetProperty(ref category, value);
        }
        public string CounterParty
        {
            get => counterParty;
            set => SetProperty(ref counterParty, value);
        }
    }
}
