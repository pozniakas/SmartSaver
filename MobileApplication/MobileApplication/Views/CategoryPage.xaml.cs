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
using static MobileApplication.Views.NewCategoryPage;

namespace MobileApplication.Views
{
    public partial class CategoryPage : ContentPage
    {
        CategoryViewModel _viewModel;

        public CategoryPage()
        {
            InitializeComponent();

            BindingContext = _viewModel = new CategoryViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }
    }
}