using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace ProjetoServicoWork.Lib
{
    public class HttpService
    {
        public static async Task<IEnumerable<T>> SendHttpRequestAsyncList<T>(
        IHttpClientFactory httpClientFactory,
        IConfiguration configuration,
        string apiPath,
        string token)
            {
                var apiBase = configuration.GetValue<string>("ServiceUri:ProductApi");

                if (!Uri.TryCreate(apiBase, UriKind.Absolute, out var baseUri))
                {
                    Console.WriteLine("A base da API não é uma URI válida.");
                    return Enumerable.Empty<T>();
                }

                var requestUri = new Uri(baseUri, apiPath);
                var client = httpClientFactory.CreateClient("ProductApi");

                // Log the request URI for debugging purposes
                Console.WriteLine($"Request URI: {requestUri}");

                try
                {
                    var response = await client.GetAsync(requestUri);

                    if (response.IsSuccessStatusCode)
                    {
                        var apiResponse = await response.Content.ReadAsStringAsync();
                        return JsonSerializer.Deserialize<IEnumerable<T>>(apiResponse, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                    }

                    Console.WriteLine($"HTTP GET request to {requestUri} failed with status code {response.StatusCode}");
                }
                catch (HttpRequestException ex)
                {
                    Console.WriteLine($"Request error: {ex.Message}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Unexpected error: {ex.Message}");
                }

                return Enumerable.Empty<T>();
            }

        public static async Task<T> SendHttpRequestAsync<T>(
            IHttpClientFactory httpClientFactory,
            IConfiguration configuration,
            string apiPath,
            string token,
            HttpMethod method,
            object content = null)
        {
            var apiBase = configuration.GetValue<string>("ServiceUri:ProductApi");

            if (!Uri.TryCreate(apiBase, UriKind.Absolute, out var baseUri))
            {
                Console.WriteLine("A base da API não é uma URI válida.");
                return default;
            }

            var requestUri = new Uri(baseUri, apiPath);
            var client = httpClientFactory.CreateClient("ProductApi");
            AddAuthorizationHeader(client, token);

            HttpResponseMessage response = method switch
            {
                HttpMethod m when m == HttpMethod.Post => await client.PostAsync(requestUri, CreateStringContent(content)),
                HttpMethod m when m == HttpMethod.Put => await client.PutAsync(requestUri, CreateStringContent(content)),
                HttpMethod m when m == HttpMethod.Delete => await client.DeleteAsync(requestUri),
                _ => await client.GetAsync(requestUri),
            };

            if (response.IsSuccessStatusCode)
            {
                var apiResponse = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<T>(apiResponse, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            }

            Console.WriteLine($"HTTP {method.Method} request to {requestUri} failed with status code {response.StatusCode}");
            return default;
        }

        private static void AddAuthorizationHeader(HttpClient client, string token)
        {
            if (!string.IsNullOrEmpty(token))
            {
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            }
        }

        private static StringContent CreateStringContent(object content)
        {
            if (content == null) return null;

            var json = JsonSerializer.Serialize(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            return new StringContent(json, Encoding.UTF8, "application/json");
        }
    }
}
