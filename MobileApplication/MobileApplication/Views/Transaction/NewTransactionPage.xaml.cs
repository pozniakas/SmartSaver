using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using DbEntities.Entities;
using MobileApplication.Configuration;
using MobileApplication.ViewModels;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileApplication.Views
{
    public partial class NewTransactionPage : ContentPage
    {
        private Lazy<string> _baseUrl = new Lazy<string>(() => AppSettingsManager.Settings["ApiBaseAddress"]);
        public Transaction Transaction { get; set; }

        public NewTransactionPage()
        {
            InitializeComponent();
            BindingContext = new NewTransactionViewModel();
        }

        async void UploadImageButton_Clicked(object sender, EventArgs e)
        {
            var file = await MediaPicker.PickPhotoAsync();

            if (file == null)
                return;

            var content = new MultipartFormDataContent();
            content.Add(new StreamContent(await file.OpenReadAsync()), "file", file.FileName);

            var httpClient = new HttpClient();
            var response = await httpClient.PostAsync("http://192.168.8.106:45455/api/Transactions/file", content);
            //var response = await httpClient.PostAsync(_baseUrl + "api/Transactions/file", content);

        }

        async void UploadCSVButton_Clicked(object sender, EventArgs e)
        {
            var file = await FilePicker.PickAsync();

            if (file == null)
                return;

            var content = new MultipartFormDataContent();
            content.Add(new StreamContent(await file.OpenReadAsync()), "file", file.FileName);

            var httpClient = new HttpClient();
            var response = await httpClient.PostAsync("http://192.168.8.106:45455/api/Transactions/file", content);
            //var response = await httpClient.PostAsync(_baseUrl + "api/Transactions/file", content);
        }

    }
}