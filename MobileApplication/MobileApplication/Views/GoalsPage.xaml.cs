using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using MobileApplication.Models;
using MobileApplication.Views;
using MobileApplication.ViewModels;

namespace MobileApplication.Views
{
    public partial class GoalsPage : ContentPage
    {
        GoalsViewModel _viewModel;

        public GoalsPage()
        {
            InitializeComponent();

            BindingContext = _viewModel = new GoalsViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }
    }
}