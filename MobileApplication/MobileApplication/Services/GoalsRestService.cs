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
using MobileApplication.Services.Rest;

namespace MobileApplication.Services
{
    public class GoalsRestService : IRestService<Goal>
    {
        HttpClient client;
        public List<Goal> Items { get; private set; }
        private string GoalsUrl = "api/Goals";

        public GoalsRestService()
        {
            client = new HttpClient(new HttpClientHandler());
            client.BaseAddress = new Uri(AppSettingsManager.Settings["ApiBaseAddress"]);
        }

        public async Task<List<Goal>> RefreshDataAsync()
        {
            Items = new List<Goal>();

            try
            {
                HttpResponseMessage response = await client.GetAsync(GoalsUrl);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    Items = JsonConvert.DeserializeObject<List<Goal>>(content);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }

            return Items;
        }

        public async Task SaveTodoItemAsync(Goal item, bool isNewItem = false)
        {
            try
            {
                string json = JsonConvert.SerializeObject(item);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = null;
                if (isNewItem)
                {
                    response = await client.PostAsync(GoalsUrl, content);
                }
                else
                {
                    response = await client.PutAsync(GoalsUrl, content);
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
                HttpResponseMessage response = await client.DeleteAsync(GoalsUrl);

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
