using MobileApplication.Configuration;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Xamarin.Essentials;

namespace MobileApplication.ViewModels
{
    public  class AddFileAndImage
    {
        public Lazy<string> _baseUrl = new Lazy<string>(() => AppSettingsManager.Settings["ApiBaseAddress"]);

        async public void UploadImageButton(object sender, EventArgs e)
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

        async public void UploadCSVButton(object sender, EventArgs e)
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
