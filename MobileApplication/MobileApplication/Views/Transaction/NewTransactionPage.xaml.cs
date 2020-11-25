using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DbEntities.Models;
using MobileApplication.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileApplication.Views
{
    public partial class NewTransactionPage : ContentPage
    {
        public Transaction Transaction { get; set; }
        public NewTransactionPage()
        {
            InitializeComponent();
            BindingContext = new NewTransactionViewModel();
        }
    }
}