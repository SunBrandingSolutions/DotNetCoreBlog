using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace System.Net.Http
{
    public static class HttpClientExtensions
    {
        public static async Task<TObject> GetJsonAsync<TObject>(this HttpClient httpClient, string requestUri)
        {
            var response = await httpClient.GetAsync(requestUri);
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<TObject>(json);
        }

        public static async Task<TObject> PostAndGetJsonAsync<TObject>(this HttpClient httpClient, string requestUri, HttpContent model)
        {
            var response = await httpClient.PostAsync(requestUri, model);
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<TObject>(json);
        }

        public static StringContent ToJsonContent(this object o)
        {
            var json = JsonConvert.SerializeObject(o);
            return new StringContent(json, Encoding.UTF8, "application/json");
        }

        public static FormUrlEncodedContent ToFormContent(this object o)
        {
            var jo = JObject.FromObject(o);
            var dic = new Dictionary<string, string>();
            foreach (var key in jo)
            {
                if (key.Value is JArray)
                {
                    dic[key.Key] = string.Join(",", key.Value.ToObject<List<object>>());
                }
                else
                {
                    dic[key.Key] = key.Value.ToString();
                }
            }

            return new FormUrlEncodedContent(dic);
        }
    }
}
