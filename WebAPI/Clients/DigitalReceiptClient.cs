using DbEntities.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WebAPI.Clients
{
    class DigitalReceiptClient
    {
        private readonly HttpClient client;
        private readonly string baseUrl = "http://localhost:3000/";

        public DigitalReceiptClient()
        {
            var clientHandler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }
            };
            client = new HttpClient(clientHandler);
            client.DefaultRequestHeaders.Add("AUTH_SECRET", "thisIsASecret");
        }

        public async Task<string> ValidateData(string data)
        {

            var body = new { data = data };
            var json = JsonConvert.SerializeObject(body);
            var input = new StringContent(json.ToString(), Encoding.UTF8, "application/json");

            var res = await client.PostAsync(baseUrl+ "validate", input);

            var contentObject = res.Content;
            var content = await contentObject.ReadAsStringAsync();
            return content;
        }

        public async Task<Transaction> GetDigitalReceiptTransaction(string id)
        {
            var res = await client.GetAsync(baseUrl + "digital-receipt/" + id);
            var contentObject = res.Content;
            var content = await contentObject.ReadAsStringAsync();
            var response = JsonConvert.DeserializeObject<Dictionary<string, string>>(content);
            DateTime dateTime = DateTime.Parse(response["trTime"].ToString());

            var transaction = new Transaction() { TrTime = dateTime, Amount = decimal.Parse(response["amount"].ToString()), CounterParty = response["counterParty"].ToString() };
            return transaction;
        }
    }
}