using DbEntities.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MobileApplication.Services.Rest
{
    class QRCodeAPI
    {
        private readonly HttpClient client;
        private readonly string baseUrl = "http://192.168.1.161:45457/";
        public QRCodeAPI()
        {
            var clientHandler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }
            };
            client = new HttpClient(clientHandler);
        }


        public async Task<Transaction> GetQRCodeTransaction(string id)
        {
            using (var cts = new CancellationTokenSource())
            {
                var body = new { Data = id};
                var json = JsonConvert.SerializeObject(body);
                var input = new StringContent(json.ToString(), Encoding.UTF8, "application/json");

                var res = await client.PostAsync(baseUrl + "api/digital-receipt", input);
                var content = res.Content;
                var data = await content.ReadAsStringAsync();
                try
                {
                    var response = JsonConvert.DeserializeObject<Dictionary<string, string>>(data);
                    DateTime dateTime = DateTime.Parse(response["trTime"].ToString());

                    var transaction = new Transaction() { TrTime = dateTime, Amount = decimal.Parse(response["amount"].ToString()), CounterParty = response["counterParty"].ToString() };
                    return transaction;
                }
                catch
                {
                    var response = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, string>>>(data);
                    throw new ArgumentException(response["Error"]["Message"].ToString());
                }
            }
        }
        
    }
}
