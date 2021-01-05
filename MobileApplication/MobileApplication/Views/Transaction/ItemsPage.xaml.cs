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
using Xamarin.Essentials;
using System.Net.Http;
using MobileApplication.Configuration;

namespace MobileApplication.Views
{
    public partial class ItemsPage : ContentPage
    {
        ItemsViewModel _viewModel;
        MediaUploader add = new MediaUploader();

        public ItemsPage()
        {
            InitializeComponent();
            
            BindingContext = _viewModel = new ItemsViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }

        async void UploadImageButton_Clicked(object sender, EventArgs e)
        {
             add.UploadReceipt(sender, e);
        }

        async void UploadCSVButton_Clicked(object sender, EventArgs e)
        {
            add.UploadBankStatement(sender,e);
        }
    }
}