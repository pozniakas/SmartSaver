using DbEntities.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using MobileApplication.Configuration;

namespace MobileApplication.Services.Rest
{
    public class RestService<T> : IRestService<T>
    {
        HttpClient client;
        public List<T> Items { get; private set; }
        private string Url;

        public RestService(string apiUrl)
        {
            Url = apiUrl;
            client = new HttpClient(new HttpClientHandler())
            {
                BaseAddress = new Uri(AppSettingsManager.Settings["ApiBaseAddress"])
            };
        }

        public async Task<List<T>> RefreshDataAsync()
        {
            Items = new List<T>();

            try
            {
                HttpResponseMessage response = await client.GetAsync(Url);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    Items = JsonConvert.DeserializeObject<List<T>>(content);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"ERROR: {ex.Message}");
            }

            return Items;
        }

        public async Task SaveTodoItemAsync(T item, bool isNewItem = false)
        {
            try
            {
                string json = JsonConvert.SerializeObject(item);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = null;
                if (isNewItem)
                {
                    response = await client.PostAsync(Url, content);
                }
                else
                {
                    response = await client.PutAsync(Url, content);
                }

                if (response.IsSuccessStatusCode)
                {
                    Debug.WriteLine($"Item \"{json}\" successfully saved.");
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine($"ERROR: {ex.Message}");
            }
        }

        public async Task DeleteTodoItemAsync(long id)
        {
            try
            {
                HttpResponseMessage response = await client.DeleteAsync(Url);

                if (response.IsSuccessStatusCode)
                {
                    var deletedItem = await response.Content.ReadAsStringAsync();
                    Debug.WriteLine($"Item \"{deletedItem}\" uccessfully deleted.");
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine($"ERROR: {ex.Message}");
            }
        }

        // To remove
        public async Task DeleteTodoItemAsync(string id) { }
    }
}
