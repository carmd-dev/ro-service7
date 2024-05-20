//Code sample from: https://stackoverflow.com/questions/9620278/how-do-i-make-calls-to-a-rest-api-using-c
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Innova.Web
{
    /// <summary>
    ///
    /// </summary>
    public static class HttpHelper
    {
        /*
         * To post, use something like this:

            await HttpHelper.Post<Setting>($"/api/values/{id}", setting);

            Example for delete:

            await HttpHelper.Delete($"/api/values/{id}");

            Example to get a list:

            List<ClaimTerm> claimTerms = await HttpHelper.Get<List<ClaimTerm>>("/api/values/");

            Example to get only one:

            ClaimTerm processedClaimImage = await HttpHelper.Get<ClaimTerm>($"/api/values/{id}");

         */

        // In my case this is https://localhost:44366/
        private static readonly string apiBasicUri = "http://216.23.168.235:8003"; //ConfigurationManager.AppSettings["apiBasicUri"];

        /// <summary>
        ///
        /// </summary>
        public static HttpResponseMessage Post<T>(string url, T contentValue, int connectionTimeout = 30, params string[] headers)
        {
            using (var client = new HttpClient())
            {
                client.Timeout = TimeSpan.FromSeconds(connectionTimeout);
                client.BaseAddress = new Uri(apiBasicUri);
                if (headers.Any())
                {
                    foreach (var header in headers)
                    {
                        var keyValue = header.Split('=');
                        client.DefaultRequestHeaders.Add(keyValue[0], keyValue[1]);
                    }
                }
                var content = new StringContent(JsonConvert.SerializeObject(contentValue), Encoding.UTF8, "application/json");
                var result = client.PostAsync(url, content).Result;

                return result;
            }
        }

        /// <summary>
        ///
        /// </summary>
        public static void Put<T>(string url, T stringValue, params string[] headers)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiBasicUri);
                if (headers.Any())
                {
                    foreach (var header in headers)
                    {
                        var keyValue = header.Split('=');
                        client.DefaultRequestHeaders.Add(keyValue[0], keyValue[1]);
                    }
                }
                var content = new StringContent(JsonConvert.SerializeObject(stringValue), Encoding.UTF8, "application/json");
                var result = client.PutAsync(url, content).Result;
                result.EnsureSuccessStatusCode();
            }
        }

        /// <summary>
        ///
        /// </summary>
        public static T Get<T>(string url, params string[] headers)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiBasicUri);

                if (headers.Any())
                {
                    foreach (var header in headers)
                    {
                        var keyValue = header.Split('=');
                        client.DefaultRequestHeaders.Add(keyValue[0], keyValue[1]);
                    }
                }

                HttpResponseMessage result = client.GetAsync(url).Result;
                result.EnsureSuccessStatusCode();
                string resultContentString = result.Content.ReadAsStringAsync().Result;
                T resultContent = JsonConvert.DeserializeObject<T>(resultContentString);
                return resultContent;
            }
        }

        /// <summary>
        ///
        /// </summary>
        public static async Task Delete(string url, params string[] headers)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiBasicUri);
                if (headers.Any())
                {
                    foreach (var header in headers)
                    {
                        var keyValue = header.Split('=');
                        client.DefaultRequestHeaders.Add(keyValue[0], keyValue[1]);
                    }
                }
                var result = await client.DeleteAsync(url);
                result.EnsureSuccessStatusCode();
            }
        }
    }
}