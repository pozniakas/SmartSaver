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
        public QRCodeAPI()
        {
            var clientHandler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }
            };
            client = new HttpClient(clientHandler);
        }


        public async Task<Transaction> PostQRCodeTransaction(string id)
        {

            using (var cts = new CancellationTokenSource())
            {
                var baseUrl = "http://192.168.1.208:45455/";

                var body = new { Id = id};
                var json = JsonConvert.SerializeObject(body);
                var input = new StringContent(json.ToString(), Encoding.UTF8, "application/json");

                var res = await client.PostAsync(baseUrl + "api/digital-receipt", input);
                var content = res.Content;
                var data = await content.ReadAsStringAsync();
                try
                {
                    var response = JsonConvert.DeserializeObject<Dictionary<string, object>>(data);

                    var transaction = response["response"];
                    var createdTransaction = new Transaction();
                    return createdTransaction;
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
