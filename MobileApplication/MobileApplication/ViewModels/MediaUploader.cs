﻿using MobileApplication.Configuration;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Xamarin.Essentials;

namespace MobileApplication.ViewModels
{
    public  class MediaUploader
    {
        private HttpClient _httpClient
        {
            get
            {
                var httpClient = new HttpClient
                {
                    BaseAddress = new Uri(AppSettingsManager.Settings["ApiBaseAddress"])
                };
                var byteArray = Encoding.ASCII.GetBytes("default:password");
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
                return httpClient;
            }
        }

        async public void UploadReceipt(object sender, EventArgs e)
        {
            var file = await MediaPicker.PickPhotoAsync();

            if (file == null)
                return;

            var content = new MultipartFormDataContent();
            content.Add(new StreamContent(await file.OpenReadAsync()), "image", file.FileName);

            await _httpClient.PostAsync("api/Transactions/receiptImage", content);
        }

        async public void UploadBankStatement(object sender, EventArgs e)
        {
            var file = await FilePicker.PickAsync();

            if (file == null)
                return;

            var content = new MultipartFormDataContent();
            content.Add(new StreamContent(await file.OpenReadAsync()), "file", file.FileName);
            await _httpClient.PostAsync("api/Transactions/file", content);
        }
    }
}
