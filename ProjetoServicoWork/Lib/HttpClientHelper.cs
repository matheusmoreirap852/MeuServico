using System.Text.Json;

namespace ProjetoServicoWork.Lib
{
    public class HttpClientHelper
    {
        private static readonly JsonSerializerOptions _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

        public static async Task<T> SendHttpRequestAsync<T>(IHttpClientFactory httpClientFactory, IConfiguration configuration, string apiPath, string token)
        {
            var apiBase = configuration.GetValue<string>("ServiceUri:ProductApi");

            if (!Uri.TryCreate(apiBase, UriKind.Absolute, out var baseUri))
            {
                Console.WriteLine("A base da API não é uma URI válida.");
                return default;
            }

            var requestUri = new Uri(baseUri, apiPath);
            var client = httpClientFactory.CreateClient("ProductApi");
            HttpClientExtensions.PutTokenInHeaderAuthorization(token, client);

            var response = await client.GetAsync(requestUri);

            if (response.IsSuccessStatusCode)
            {
                var apiResponse = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<T>(apiResponse, _options);
            }

            // Handle error or return a default value, if necessary.
            return default;
        }

        public static async Task<IEnumerable<T>> SendHttpRequestAsyncList<T>(IHttpClientFactory httpClientFactory, IConfiguration configuration, string apiPath, string token)
        {
            var apiBase = configuration.GetValue<string>("ServiceUri:ProductApi");

            if (!Uri.TryCreate(apiBase, UriKind.Absolute, out var baseUri))
            {
                Console.WriteLine("A base da API não é uma URI válida.");
                return Enumerable.Empty<T>();
            }

            var requestUri = new Uri(baseUri, apiPath);
            var client = httpClientFactory.CreateClient("ProductApi");
            HttpClientExtensions.PutTokenInHeaderAuthorization(token, client);

            var response = await client.GetAsync(requestUri);

            if (response.IsSuccessStatusCode)
            {
                var apiResponse = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<IEnumerable<T>>(apiResponse, _options);
            }

            // Handle error or return an empty collection, if necessary.
            return Enumerable.Empty<T>();
        }
    }
}
