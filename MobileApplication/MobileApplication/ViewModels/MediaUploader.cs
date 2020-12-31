using MobileApplication.Configuration;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Xamarin.Essentials;

namespace MobileApplication.ViewModels
{
    public  class MediaUploader
    {
        public Lazy<string> _baseUrl = new Lazy<string>(() => AppSettingsManager.Settings["ApiBaseAddress"]);

        async public void UploadReceipt(object sender, EventArgs e)
        {
            var file = await MediaPicker.PickPhotoAsync();

            if (file == null)
                return;

            var content = new MultipartFormDataContent();
            content.Add(new StreamContent(await file.OpenReadAsync()), "image", file.FileName);

            var httpClient = new HttpClient();
            var url = _baseUrl.Value + "api/Transactions/receiptImage";

            await App.Current.MainPage.DisplayAlert("Loading", "Image recognition started...", "Ok");
            var response = await httpClient.PostAsync(url, content);
            await App.Current.MainPage.DisplayAlert("Finished", "Image recognition finished.", "Ok");

        }

        async public void UploadBankStatement(object sender, EventArgs e)
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
