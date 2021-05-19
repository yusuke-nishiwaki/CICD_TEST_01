using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Hosting;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace ETCMemberTest.Moq
{
    public static class ApiRequest
    {
        public static async Task<(T, HttpStatusCode)> ExcuteAsync<T>(string url,
                                                                        FormUrlEncodedContent content,
                                                                        string bearerToken = ""
            ) where T : class
        {
            var hostBuilder = CreateHostBuilder.Create();

            // Build and start the IHost
            var host = await hostBuilder.StartAsync();

            //Create an HttpClient to send requests to the TestServer
            var client = host.GetTestClient();

            if (!string.IsNullOrEmpty(bearerToken))
            {
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + bearerToken);
            }

            var response = await client.PostAsync(url, content);
            var responseString = await response.Content.ReadAsStringAsync();

            if (response.StatusCode == HttpStatusCode.OK)
            {
                return (JsonSerializer.Deserialize<T>(responseString), response.StatusCode);
            }
            else
            {
                return (null, response.StatusCode);
            }
        }

        public static async Task<(string, HttpStatusCode)> GetAsync(string url,
                                                                    FormUrlEncodedContent content)
        {
            var hostBuilder = CreateHostBuilder.Create();

            // Build and start the IHost
            var host = await hostBuilder.StartAsync();

            //Create an HttpClient to send requests to the TestServer
            var client = host.GetTestClient();

            var response = await client.GetAsync($"{url}?{await content.ReadAsStringAsync()}");
            var responseString = await response.Content.ReadAsStringAsync();

            if (response.StatusCode == HttpStatusCode.OK)
            {
                return (responseString, response.StatusCode);
            }
            else
            {
                return (null, response.StatusCode);
            }
        }
    }
}
