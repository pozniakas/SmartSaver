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
using static MobileApplication.Views.NewCategoryPage;

namespace MobileApplication.Views
{
    public partial class NewTransactionPage : ContentPage
    {

        public delegate void StatusUpdateHandler(object sender, ProgressEventArgs e);

        public static event StatusUpdateHandler OnUpdateStatusTransaction;

        private Lazy<string> _baseUrl = new Lazy<string>(() => AppSettingsManager.Settings["ApiBaseAddress"]);
        public Transaction _Transaction { get; set; }

        public NewTransactionPage()
        {
            InitializeComponent();
            BindingContext = new NewTransactionViewModel();
            SaveButton.Clicked += SaveButton_Clicked;
        }
        private void SaveButton_Clicked(object sender, EventArgs e)
        {
            UpdateStatus("transaction");
        }
        private void UpdateStatus(string status)
        {
            var h = OnUpdateStatusTransaction;
            if (h == null) return;

            ProgressEventArgs args = new ProgressEventArgs(status);
            OnUpdateStatusTransaction(this, args);
        }

        async void UploadImageButton_Clicked(object sender, EventArgs e)
        {
            var file = await MediaPicker.PickPhotoAsync();

            if (file == null)
                return;

            var content = new MultipartFormDataContent();
            content.Add(new StreamContent(await file.OpenReadAsync()), "file", file.FileName);

            var httpClient = new HttpClient();
            var url = _baseUrl.Value + "api/Transactions/file";
            var response = await httpClient.PostAsync(url, content);

        }

        async void UploadCSVButton_Clicked(object sender, EventArgs e)
        {
            var file = await FilePicker.PickAsync();

            if (file == null)
                return;

            var content = new MultipartFormDataContent();
            content.Add(new StreamContent(await file.OpenReadAsync()), "file", file.FileName);

            var httpClient = new HttpClient();
            var url = _baseUrl.Value + "api/Transactions/file";
            var response = await httpClient.PostAsync(url, content);
        }

    }
}