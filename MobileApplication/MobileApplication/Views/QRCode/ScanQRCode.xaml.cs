using Xamarin.Forms;
using MobileApplication.ViewModels;
using MobileApplication.Services;
using System;

namespace MobileApplication.Views
{
    public partial class ScanQRCode : ContentPage
    {
        ScanQRCodeViewModel _viewModel;

        public ScanQRCode()
        {
            InitializeComponent();

            BindingContext = _viewModel = new ScanQRCodeViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }

        private async void btnScan_Clicked(object sender, EventArgs e)
        {

                var scanner = DependencyService.Get<IQRScanningService>();
                var result = await scanner.ScanAsync();
                if (result != null)
                {
                    txtBarcode.Text = result;
                }
                else
                {
                    await Shell.Current.Navigation.PopAsync();
                    txtBarcode.Text = "";
                }
       
        }
    }
}