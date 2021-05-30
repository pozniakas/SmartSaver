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
                var transaction = await qrCodeApi.GetQRCodeTransaction(result);
                var amountParamString = $"{nameof(NewTransactionViewModel.Amount)}={ transaction.Amount}";
                var counterPartyParamString = $"{nameof(NewTransactionViewModel.CounterParty)}={ transaction.CounterParty}";

                var trTimeParamString = $"{nameof(NewTransactionViewModel.TrTimeString)}={transaction.TrTime.ToShortDateString()}";
                await Shell.Current.GoToAsync($"{nameof(NewTransactionPage)}?{amountParamString}&{counterPartyParamString}&{trTimeParamString}");
                }       
        }
    }
}