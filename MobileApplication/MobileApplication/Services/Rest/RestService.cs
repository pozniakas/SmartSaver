using DbEntities.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using MobileApplication.Configuration;
using System.Net.Http.Headers;

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
            var byteArray = Encoding.ASCII.GetBytes("default:password");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
        }

        public async Task<List<T>> RefreshDataAsync()
        {
            Items = new List<T>();

            try
            {
                var response = await client.GetAsync(Url);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    Items = JsonConvert.DeserializeObject<List<T>>(content);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"ERROR: {ex.Message}");
            }

            return Items;
        }

        public async Task DeleteItemAsync(long id)
        {
            Debug.WriteLine("deleting");
            try
            {
                var response = await client.DeleteAsync(Url + id);

                if (response.IsSuccessStatusCode)
                {
                    var deletedItem = await response.Content.ReadAsStringAsync();
                    Debug.WriteLine($"Item \"{deletedItem}\" uccessfully deleted.");
                }
                else Debug.WriteLine($"Item  not deleted.");

            }
            catch (Exception ex)
            {
                Debug.WriteLine($"ERROR: {ex.Message}");
            }
        }

        public async Task SaveItemAsync(T item)
        {
            try
            {
                var json = JsonConvert.SerializeObject(item);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = null;
                response = await client.PostAsync(Url, content);

                if (response.IsSuccessStatusCode)
                {
                    Debug.WriteLine($"Item \"{json}\" successfully saved.");
                }
                else Debug.WriteLine("not saved");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"ERROR: {ex.Message}");
            }
        }

        public async Task UpdateItemAsync(T item, long id)
        {
            try
            {
                var json = JsonConvert.SerializeObject(item);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = null;
                response = await client.PutAsync(Url + id, content);

                if (response.IsSuccessStatusCode)
                {
                    Debug.WriteLine($"Item \"{json}\" successfully saved.");
                }
                else Debug.WriteLine("not saved");

            }
            catch (Exception ex)
            {
                Debug.WriteLine($"ERROR: {ex.Message}");
            }
        }

        // To remove
        //public async Task DeleteTodoItemAsync(string id) { }
    }
}
