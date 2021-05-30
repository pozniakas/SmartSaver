using Xamarin.Forms;
using MobileApplication.ViewModels;
using MobileApplication.Services;
using System;
using MobileApplication.Services.Rest;

namespace MobileApplication.Views
{
    public partial class ScanQRCode : ContentPage
    {
        ScanQRCodeViewModel _viewModel;
        private readonly QRCodeAPI qrCodeApi;
        NewTransactionViewModel newTransactionViewModel;
        public ScanQRCode()
        {
            InitializeComponent();

            BindingContext = _viewModel = new ScanQRCodeViewModel();
            qrCodeApi = new QRCodeAPI();
            newTransactionViewModel = new NewTransactionViewModel();
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
                var transaction = await qrCodeApi.PostQRCodeTransaction(result);
                newTransactionViewModel.Amount = transaction.Amount.ToString();
                newTransactionViewModel.CounterParty = transaction.CounterParty;
                newTransactionViewModel.Details = transaction.Details;
                newTransactionViewModel.TrTime = transaction.TrTime;
                await Shell.Current.GoToAsync($"{nameof(NewTransactionViewModel)}");
                }       
        }
    }
}