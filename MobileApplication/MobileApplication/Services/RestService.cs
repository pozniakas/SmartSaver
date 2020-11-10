using MobileApplication.Models;
using DbEntities.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using MobileApplication.Configuration;

namespace MobileApplication.Services
{
    public class RestService : IRestService
    {
        HttpClient client;
        public List<Transaction> Items { get; private set; }
        private string TransactionsUrl = "api/Transactions";

        public RestService()
        {
            client = new HttpClient(new HttpClientHandler());
            client.BaseAddress = new Uri(AppSettingsManager.Settings["ApiBaseAddress"]);
        }

        public async Task<List<Transaction>> RefreshDataAsync()
        {
            Items = new List<Transaction>();

            try
            {
                HttpResponseMessage response = await client.GetAsync(TransactionsUrl);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    Items = JsonConvert.DeserializeObject<List<Transaction>>(content);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }

            return Items;
        }

        public async Task SaveTodoItemAsync(TodoItem item, bool isNewItem = false)
        {
            try
            {
                string json = JsonConvert.SerializeObject(item);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = null;
                if (isNewItem)
                {
                    response = await client.PostAsync(TransactionsUrl, content);
                }
                else
                {
                    response = await client.PutAsync(TransactionsUrl, content);
                }

                if (response.IsSuccessStatusCode)
                {
                    Debug.WriteLine(@"\tTodoItem successfully saved.");
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }
        }

        public async Task DeleteTodoItemAsync(string id)
        {
            try
            {
                HttpResponseMessage response = await client.DeleteAsync(TransactionsUrl);

                if (response.IsSuccessStatusCode)
                {
                    Debug.WriteLine(@"\tTodoItem successfully deleted.");
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }
        }
    }
}
