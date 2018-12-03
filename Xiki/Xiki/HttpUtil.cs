using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Xiki
{
    class HttpUtil
    {

        public static async Task<JObject> PostAsync(string uri, object data)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://10.130.216.144"); // 10.130.216.144
            string json = JsonConvert.SerializeObject(data);

            var content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage result = await client.PostAsync(uri, content);
            string responseJSON = await result.Content.ReadAsStringAsync();

            return JObject.Parse(responseJSON);
        }
    }

    public class Response
    {
        public bool status { get; set; }
        public string message { get; set; }
    }

    public class TokenResponse : Response
    {
        public string token { get; set; }
        public int accountID { get; set; }
    }

    public class GetWikiResponse : Response
    {
        public Wiki wiki { get; set; }
    }
    public class Wiki
    {
        public int wikiID { get; set; }
        public string name { get; set; }
        public string description { get; set; }
    }
}
