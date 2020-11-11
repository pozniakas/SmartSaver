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
    public class GoalsRestService : IRestServiceGoals
    {
        HttpClient client;
        string RestUrl = "http://192.168.0.163:45455/api/Goals";

        public List<Goal> Items { get; private set; }

        public GoalsRestService()
        {
            client = new HttpClient();
            client = new HttpClient(new HttpClientHandler());
            //#if DEBUG
            //            client = new HttpClient(DependencyService.Get<IHttpClientHandlerService>().GetInsecureHandler());
            //#else
            //            client = new HttpClient();
            //#endif
        }

        public async Task<List<Goal>> RefreshDataAsync()
        {
            Items = new List<Goal>();

            Uri uri = new Uri(string.Format(RestUrl, string.Empty));
            Debug.WriteLine($"Getting: {RestUrl}");
            try
            {
                HttpResponseMessage response = await client.GetAsync(uri);
                Debug.WriteLine($"Response Status: {response.StatusCode}");
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

        public async Task SaveTodoItemAsync(TodoItem item, bool isNewItem = false)
        {
            Uri uri = new Uri(string.Format(RestUrl, string.Empty));

            try
            {
                string json = JsonConvert.SerializeObject(item);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = null;
                if (isNewItem)
                {
                    response = await client.PostAsync(uri, content);
                }
                else
                {
                    response = await client.PutAsync(uri, content);
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
            Uri uri = new Uri(string.Format(RestUrl, id));

            try
            {
                HttpResponseMessage response = await client.DeleteAsync(uri);

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
